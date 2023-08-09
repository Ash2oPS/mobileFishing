using EtienneSibeaux.Fish;
using EtienneSibeaux.Net;
using EtienneSibeaux.Player;
using EtienneSibeaux.UI;
using System.Collections.Generic;
using UnityEngine;

namespace EtienneSibeaux.Manager
{
    public class C_M_PlayerManager : CA_Manager
    {
        [Header("---Data---")]
        [SerializeField] private SO_PlayerStatsTemplate _playerStatsTemplate;

        private C_UI_FishCounter _fishCounter;
        private C_UI_MoneyCounter _moneyCounter;

        private int _gold;
        private int _maxFish;
        private float _netStrength;

        private List<SO_FishAsset> _fishList;

        // getters

        public int Gold { get => _gold; }
        public int NumberOfFish { get => _fishList == null ? 0 : _fishList.Count; }
        public int MaxFish { get => _maxFish; }
        public float NetStrength { get => _netStrength; }
        public List<SO_FishAsset> FishList { get => _fishList; }
        public SO_PlayerStatsTemplate PlayerStatsTemplate { get => _playerStatsTemplate; }

        // refs

        private C_Player _player;
        private C_Net _net;

        #region Init

        private void Awake()
        {
            _player = FindObjectOfType<C_Player>();
            _net = FindObjectOfType<C_Net>();

            C_M_UIManager uiManager = C_GameManager.Instance.GetManager<C_M_UIManager>();
            _fishCounter = uiManager.GetUI<C_UI_FishCounter>();
            _moneyCounter = uiManager.GetUI<C_UI_MoneyCounter>();
        }

        private void Start()
        {
            SetAllStats();
        }

        private void SetAllStats()
        {
            if (_playerStatsTemplate == null)
                return;

            SO_PlayerStatsTemplate pst = _playerStatsTemplate;

            SetGold(pst.Gold);
        }

        #endregion Init

        #region Setters

        public void SetGold(int gold)
        {
            _gold = Mathf.Clamp(gold, 0, 99999999);
            _moneyCounter.SetValue(gold);
        }

        public void AddFish(SO_FishAsset fishAsset)
        {
            _fishList ??= new List<SO_FishAsset>();
            _fishList.Add(fishAsset);
            _fishCounter.SetValue(NumberOfFish);
        }

        public void SelAllFish()
        {
            int tempNumberOfFish = NumberOfFish;

            for (int i = 0; i < tempNumberOfFish; i++)
            {
                SellFish();
            }
        }

        private void SellFish()
        {
            SetGold(_gold + _fishList[0].FishPrice);
            RemoveFirstFish();
        }

        public void RemoveFirstFish()
        {
            _fishList.RemoveAt(0);
            _fishCounter.SetValue(NumberOfFish);
        }

        public void SetMaxFish(int maxFish)
        {
            _maxFish = Mathf.Clamp(maxFish, 0, 99999999);
            _fishCounter.SetMaxFish(maxFish);
        }

        public void SetSpeed(float speed)
        {
            _player.BoatMovement.SetSpeed(Mathf.Clamp(speed, 0f, 8f));
        }

        public void SetNetSize(float scale)
        {
            _net.NetSize.SetSize(scale);
        }

        #endregion Setters
    }
}