using EtienneSibeaux.Manager;
using EtienneSibeaux.Misc;
using EtienneSibeaux.Player;
using UnityEngine;

namespace EtienneSibeaux.Net
{
    public class C_Net : CA_ReferenceGetter
    {
        // Manual Refs

        [Header("---References---")]
        [SerializeField] private C_NetMovement _netMovement;

        [SerializeField] private C_NetCatchFish _netCatchFish;
        [SerializeField] private C_NetSize _netSize;
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private C_NetLineRenderer _topLeftLineRenderer;
        [SerializeField] private C_NetLineRenderer _topRightLineRenderer;
        [SerializeField] private C_NetLineRenderer _botLeftLineRenderer;
        [SerializeField] private C_NetLineRenderer _botRightLineRenderer;

        // Auto Refs

        private C_Player _player;
        private C_M_PlayerManager _playerManager;

        // Getters

        public C_NetMovement NetMovement { get => _netMovement; }
        public C_NetCatchFish NetCatchFish { get => _netCatchFish; }
        public C_NetSize NetSize { get => _netSize; }
        public BoxCollider BoxCollider { get => _boxCollider; }
        public C_Player Player { get => _player; }
        public C_M_PlayerManager PlayerManager { get => _playerManager; }
        public C_NetLineRenderer TopLeftLineRenderer { get => _topLeftLineRenderer; }
        public C_NetLineRenderer TopRightLineRenderer { get => _topRightLineRenderer; }
        public C_NetLineRenderer BotLeftLineRenderer { get => _botLeftLineRenderer; }
        public C_NetLineRenderer BotRightLineRenderer { get => _botRightLineRenderer; }

        // Get Refs

        protected override void GetReferences()
        {
            _player = FindObjectOfType<C_Player>();
            _playerManager = C_GameManager.Instance.GetManager<C_M_PlayerManager>();
        }
    }
}