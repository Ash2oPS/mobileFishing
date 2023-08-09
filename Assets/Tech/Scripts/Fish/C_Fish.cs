using EtienneSibeaux.Interfaces;
using EtienneSibeaux.Manager;
using EtienneSibeaux.Misc;
using EtienneSibeaux.Net;
using UnityEngine;

namespace EtienneSibeaux.Fish
{
    public class C_Fish : CA_ReferenceGetter, ICatchable
    {
        [Header("---References---")]
        [SerializeField] private C_FishStats _fishStats;

        [SerializeField] private C_FishMovement _fishMovement;

        private C_M_PlayerManager _playerManager;
        private C_M_FishManager _fishManager;

        public C_FishStats FishStats { get => _fishStats; }
        public C_FishMovement FishMovement { get => _fishMovement; }

        private void Awake()
        {
            _playerManager = C_GameManager.Instance.GetManager<C_M_PlayerManager>();
            _fishManager = C_GameManager.Instance.GetManager<C_M_FishManager>();
        }

        public void OnCaught(C_Net catchingNet)
        {
            _playerManager.AddFish(_fishStats.FishAsset);
            Destroy(gameObject);
            _fishManager.DecrementFishCount();
        }
    }
}