using EtienneSibeaux.Debugger;
using EtienneSibeaux.Manager;
using EtienneSibeaux.MyCamera;
using EtienneSibeaux.Net;
using EtienneSibeaux.Player;
using EtienneSibeaux.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace EtienneSibeaux.UI
{
    public class C_UI_Market : CA_UIClass
    {
        #region Attributes & Variables

        [Header("---Parameters---")]
        [SerializeField] private int[] _levelPrices;

        [SerializeField] private float[] _netSizeValues;
        [SerializeField] private float[] _boatSpeedValues;
        [SerializeField] private int[] _maxFishValues;
        [SerializeField] private int[] _maxFishInWorldValues;

        [Header("---References---")]
        [SerializeField] private Button _mainButton;

        [SerializeField] private RectTransform _windowParent;

        /// <summary>
        /// 0 -> Net, 1 -> Boat, 2-> MaxFIsh, 3 -> Fish
        /// </summary>
        [SerializeField] private Button[] _allUpgradesButtons;

        /// <summary>
        /// 0 -> Net, 1 -> Boat, 2-> MaxFIsh, 3 -> Fish
        /// </summary>
        [SerializeField] private TextMeshProUGUI[] _allUpgradesPriceTexts;

        /// <summary>
        /// 0 -> Net, 1 -> Boat, 2-> MaxFIsh, 3 -> Fish
        /// </summary>
        [SerializeField] private TextMeshProUGUI[] _allUpgradesNameTexts;

        private C_Player _player;
        private C_Net _net;
        private C_CameraController _camController;
        private C_M_PlayerManager _playerManager;
        private C_M_FishManager _fishManager;
        private C_UI_SellFishButton _sellFishButton;
        private C_UI_Market _market;

        // variables

        private bool _hasToEnableSaleFishButton;

        /// <summary>
        /// 0 -> Net, 1 -> Boat, 2-> MaxFIsh, 3 -> Fish
        /// </summary>
        private int[] _allCurrentLevels = new int[4] { 1, 1, 1, 1 };

        // getters
        public Button MainButton { get => _mainButton; }

        #endregion Attributes & Variables

        #region Init

        private void Awake()
        {
            _net = FindObjectOfType<C_Net>();
            _player = FindObjectOfType<C_Player>();
            _camController = FindObjectOfType<C_CameraController>();
            _sellFishButton = C_GameManager.Instance.GetManager<C_M_UIManager>().GetUI<C_UI_SellFishButton>();
            _playerManager = C_GameManager.Instance.GetManager<C_M_PlayerManager>();
            _fishManager = C_GameManager.Instance.GetManager<C_M_FishManager>();
            _market = C_GameManager.Instance.GetManager<C_M_UIManager>().GetUI<C_UI_Market>();
        }

        private void Start()
        {
            SO_PlayerStatsTemplate pst = _playerManager.PlayerStatsTemplate;

            _allCurrentLevels[0] = pst.NetSizeLevel;
            _allCurrentLevels[1] = pst.BoatSpeedLevel;
            _allCurrentLevels[2] = pst.MaxFishLevel;
            _allCurrentLevels[3] = pst.MaxFishOnMapLevel;

            ApplyNewUpgradeValue(0);
            ApplyNewUpgradeValue(1);
            ApplyNewUpgradeValue(2);
            ApplyNewUpgradeValue(3);

            CheckIfHasToSpawnNewTypeOfFish();
        }

        #endregion Init

        #region Button Methods

        public void DisplayMarket()
        {
            _player.BoatMovement.SetCanMove(false);
            _net.NetCatchFish.SetCanCatch(false);
            _market.MainButton.interactable = false;
            _windowParent.gameObject.SetActive(true);
            if (_sellFishButton.IsEnabled)
            {
                _hasToEnableSaleFishButton = true;
                _sellFishButton.SetButtonEnabled(false);
            }

            for (int i = 0; i < 4; i++)
            {
                EditDisplayedPrice(i);
                EditDisplayedLevel(i);
            }

            CheckIfTooExpensive();
        }

        public void Dismiss()
        {
            _market.MainButton.interactable = true;
            _windowParent.gameObject.SetActive(false);
            _player.BoatMovement.SetCanMove(true);
            _net.NetCatchFish.SetCanCatch(true);

            if (_hasToEnableSaleFishButton)
            {
                _hasToEnableSaleFishButton = false;
                _sellFishButton.SetButtonEnabled(true);
            }
        }

        public void Buy(int index)
        {
            _playerManager.SetGold(_playerManager.Gold - GetPrice(index));
            _allCurrentLevels[index]++;
            ApplyNewUpgradeValue(index);

            CheckIfTooExpensive();
            EditDisplayedPrice(index);
            EditDisplayedLevel(index);

            CheckIfHasToSpawnNewTypeOfFish();
        }

        private void CheckIfHasToSpawnNewTypeOfFish()
        {
            int smallestLvl = int.MaxValue;

            for (int i = 0; i < 4; i++)
            {
                if (_allCurrentLevels[i] < smallestLvl)
                    smallestLvl = _allCurrentLevels[i];
            }

            if (smallestLvl == 1 || smallestLvl == 10)
                return;

            if (smallestLvl > _fishManager.MaxFishIndex + 1)
                _fishManager.SetMaxFishIndex(smallestLvl - 1);
        }

        private void ApplyNewUpgradeValue(int index)
        {
            switch (index)
            {
                case 0:
                    _playerManager.SetNetSize(_netSizeValues[_allCurrentLevels[index] - 1]);
                    break;

                case 1:
                    float minValue = _boatSpeedValues[0];
                    float maxValue = _boatSpeedValues[9];
                    float curValue = _boatSpeedValues[_allCurrentLevels[index] - 1];
                    float factor = (curValue - minValue) / (maxValue - minValue);

                    _camController.SetHeight(factor);
                    _playerManager.SetSpeed(curValue);
                    break;

                case 2:
                    _playerManager.SetMaxFish(_maxFishValues[_allCurrentLevels[index] - 1]);
                    break;

                case 3:
                    _fishManager.SetMaxFishOnMap(_maxFishInWorldValues[_allCurrentLevels[index] - 1]);
                    break;
            }
        }

        #endregion Button Methods

        #region Edit Displayed Infos

        private void CheckIfTooExpensive()
        {
            int priceToCheck;

            for (int i = 0; i < 4; i++)
            {
                priceToCheck = GetPrice(i);

                if (_playerManager.Gold < priceToCheck || priceToCheck == 0)
                {
                    _allUpgradesButtons[i].interactable = false;
                    continue;
                }

                _allUpgradesButtons[i].interactable = true;
            }
        }

        private void EditDisplayedPrice(int index)
        {
            int price = GetPrice(index);
            _allUpgradesPriceTexts[index].text = price.ToString() + " $";
        }

        private void EditDisplayedLevel(int index)
        {
            int level = _allCurrentLevels[index];
            string currentString = _allUpgradesNameTexts[index].text;
            int indexOfDot = currentString.IndexOf('.');
            currentString = currentString.Remove(indexOfDot + 1);

            if (level == 10)
                currentString += "MAX";
            else
                currentString += level.ToString();

            _allUpgradesNameTexts[index].text = currentString;
        }

        #endregion Edit Displayed Infos

        #region Misc

        private int GetPrice(int index)
        {
            int output = 0;

            int level = _allCurrentLevels[index];
            output = _levelPrices[level - 1];

            return output;
        }

        #endregion Misc
    }
}