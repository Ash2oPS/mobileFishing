using EtienneSibeaux.Debugger;
using EtienneSibeaux.Player;
using UnityEngine;

namespace EtienneSibeaux.Manager
{
    public class C_M_InputManager : CA_Manager
    {
        [Header("---Parameters---")]
        [SerializeField] private LayerMask _waterLayer;

        [Header("---References---")]
        [SerializeField] private Transform _previewCube;

        private Camera _camera;
        private C_Player _player;

        private Vector2 _fingerScreenPosition;
        private Vector3 _fingerWorldPosition;
        public Vector2 FingerScreenPosition { get => _fingerScreenPosition; }

        public Vector3 FingerWorldPosition { get => _fingerWorldPosition; }

        private void Awake()
        {
            _camera = Camera.main;
            _player = FindObjectOfType<C_Player>();
        }

        private void Update()
        {
#if UNITY_EDITOR

            if (!Input.GetMouseButton(0))
            {
                SetFingerPosition(Vector2.zero);
                return;
            }

            SetFingerPosition(Input.mousePosition);

#else

            if (Input.touchCount == 0)
            {
                SetFingerPosition(Vector2.zero);
                return;
            }

            Touch touch = Input.GetTouch(0);
            SetFingerPosition(touch.position);

#endif
        }

        private void SetFingerPosition(Vector2 screenPosition)
        {
            _fingerScreenPosition = screenPosition;

            Ray ray = _camera.ScreenPointToRay(new Vector3(_fingerScreenPosition.x, _fingerScreenPosition.y, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _waterLayer))
                _fingerWorldPosition = hit.point;

            _previewCube.transform.position = _fingerWorldPosition;
        }
    }
}