using EtienneSibeaux.Fish;
using EtienneSibeaux.Manager;
using UnityEngine;

namespace EtienneSibeaux.Net
{
    public class C_NetCatchFish : MonoBehaviour
    {
        [Header("---References---")]
        [SerializeField] private C_Net _net;

        private bool _canCatch = true;
        private C_M_PlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = _net.PlayerManager;
        }

        public void SetCanCatch(bool value)
        {
            _canCatch = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canCatch)
                return;

            if (_playerManager.NumberOfFish >= _playerManager.MaxFish)
                return;

            if (!other.TryGetComponent(out C_Fish fish))
                return;

            fish.OnCaught(_net);
        }
    }
}