using EtienneSibeaux.Debugger;
using EtienneSibeaux.Manager;
using UnityEngine;

namespace EtienneSibeaux.Player
{
    public class C_PlayerController : MonoBehaviour
    {
        [Header("---References---")]
        [SerializeField] private C_Player _player;

        private C_M_InputManager _inputManager;

        private void Awake()
        {
            _inputManager = C_GameManager.Instance.GetManager<C_M_InputManager>();
        }

        private void Update()
        {
            CheckForFingerOnScreen();
        }

        private void CheckForFingerOnScreen()
        {
            if (_inputManager.FingerScreenPosition == null || _inputManager.FingerScreenPosition == Vector2.zero)
            {
                _player.BoatMovement.RegisterMove(Vector2.zero);
                return;
            }

            Vector3 dirV3 = _inputManager.FingerWorldPosition - _player.transform.position;
            Vector2 dirV2 = new Vector2(dirV3.x, dirV3.z);
            _player.BoatMovement.RegisterMove(dirV2);
        }
    }
}