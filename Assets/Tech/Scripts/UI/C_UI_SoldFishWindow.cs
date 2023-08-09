using EtienneSibeaux.Fish;
using EtienneSibeaux.Manager;
using EtienneSibeaux.Net;
using EtienneSibeaux.Player;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EtienneSibeaux.UI
{
    public class C_UI_SoldFishWindow : CA_UIClass
    {
        [Header("---References---")]
        [SerializeField] private RectTransform _windowParent;

        [SerializeField] private C_UI_SoldFishBox[] _allSoldFishBoxes;
        [SerializeField] private TextMeshProUGUI _totalTMP;

        private C_Player _player;
        private C_Net _net;
        private C_M_PlayerManager _playerManager;
        private C_UI_SellFishButton _sellFishButton;
        private C_UI_Market _market;

        private SO_FishAsset[] _allFishAssets;
        private List<SO_FishAsset>[] _allSortedSoldFish;

        #region Init

        private void Awake()
        {
            _net = FindObjectOfType<C_Net>();
            _player = FindObjectOfType<C_Player>();
            _playerManager = C_GameManager.Instance.GetManager<C_M_PlayerManager>();
            _sellFishButton = C_GameManager.Instance.GetManager<C_M_UIManager>().GetUI<C_UI_SellFishButton>();
            _market = C_GameManager.Instance.GetManager<C_M_UIManager>().GetUI<C_UI_Market>();
            GetAllFishAssets();
        }

        private void GetAllFishAssets()
        {
            _allFishAssets = new SO_FishAsset[9];

            for (int i = 0; i < _allFishAssets.Length; i++)
            {
                _allFishAssets[i] = _allSoldFishBoxes[i].FishAsset;
            }
        }

        #endregion Init

        #region Sales

        public void DisplaySalesInfos(SO_FishAsset[] caughtFish)
        {
            _player.BoatMovement.SetCanMove(false);
            _net.NetCatchFish.SetCanCatch(false);
            _market.MainButton.interactable = false;

            _windowParent.gameObject.SetActive(true);

            SortFish(caughtFish);
            EditAllDisplayedInfos();
        }

        private void SortFish(SO_FishAsset[] caughtFish)
        {
            _allSortedSoldFish = new List<SO_FishAsset>[9];

            for (int i = 0; i < caughtFish.Length; i++)
            {
                SO_FishAsset curFish = caughtFish[i];
                _allSortedSoldFish[curFish.Index] ??= new List<SO_FishAsset>();
                _allSortedSoldFish[curFish.Index].Add(curFish);
            }
        }

        private void EditAllDisplayedInfos()
        {
            int priceSum = 0;

            for (int i = 0; i < _allSoldFishBoxes.Length; i++)
            {
                if (_allSortedSoldFish[i] == null)
                {
                    _allSoldFishBoxes[i].SetQuantity(0);
                    continue;
                }

                if (!_allSoldFishBoxes[i].IsDiscovered)
                    _allSoldFishBoxes[i].DiscoverAsset();

                _allSoldFishBoxes[i].SetQuantity(_allSortedSoldFish[i].Count);
                priceSum += _allSortedSoldFish[i].Count * _allFishAssets[i].FishPrice;
            }

            _totalTMP.text = $"TOTAL: {priceSum.ToString()} $";
        }

  

        #endregion Sales

        public void Dismiss()
        {
            for (int i = 0; i < _allSoldFishBoxes.Length; i++)
            {
                if (_allSoldFishBoxes[i].IsNew)
                    _allSoldFishBoxes[i].SetAsNotNew();
            }

            _playerManager.SelAllFish();

            _market.MainButton.interactable = true;
            _windowParent.gameObject.SetActive(false);
            _player.BoatMovement.SetCanMove(true);
            _net.NetCatchFish.SetCanCatch(true);
            _sellFishButton.CheckNumberOfFish();
        }
    }
}