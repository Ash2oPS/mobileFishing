using UnityEngine;
using EtienneSibeaux.Boat;
using EtienneSibeaux.Misc;
using EtienneSibeaux.Net;

namespace EtienneSibeaux.Player
{
    public class C_Player : CA_ReferenceGetter
    {
        // Manual Refs
        [Header("---References---")]
        [SerializeField] private C_PlayerController _playerController;

        [SerializeField] private C_BoatMovement _BoatMovement;
        [SerializeField] private Transform _netParent;

        // Getters

        public C_PlayerController PlayerController { get => _playerController; }
        public C_BoatMovement BoatMovement { get => _BoatMovement; }
        public Transform NetParent { get => _netParent; }
    }
}