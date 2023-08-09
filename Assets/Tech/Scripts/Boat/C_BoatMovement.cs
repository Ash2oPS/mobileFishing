using EtienneSibeaux.Debugger;
using UnityEngine;
using EtienneSibeaux.Net;
using EtienneSibeaux.MyCamera;
using EtienneSibeaux.Player;

namespace EtienneSibeaux.Boat
{
    public class C_BoatMovement : MonoBehaviour
    {
        #region Attributes

        [Header("---Parameters---")]
        [SerializeField] private float _speed;

        [SerializeField] private AnimationCurve _speedCurve;
        [SerializeField] private float _accelerationFactor;
        [SerializeField] private float _decelerationFactor;

        [Header("---References---")]
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private Transform _boatMesh;

        private C_Net _net;
        private C_CameraController _cameraController;

        // private

        private Vector2 _inputDirection;
        private float _currentAcceleration;
        private float _turnSpeed = 0.05f;
        private float _accelMaxRotation = -25f;
        private bool _hasToMove;
        private bool _canMove = true;

        // lambda

        private float _currentSpeed => _speed * _speedCurve.Evaluate(_currentAcceleration);

        #endregion Attributes

        private void Awake()
        {
            _net = FindObjectOfType<C_Net>();
            _cameraController = FindObjectOfType<C_CameraController>();
        }

        #region Methods

        public void SetSpeed(float value)
        {
            _speed = value;
        }

        public void SetCanMove(bool value)
        {
            _canMove = value;
        }

        public void RegisterMove(Vector2 direction)
        {
            if (direction.magnitude < 0.1f)
            {
                _currentAcceleration = Mathf.Clamp(_currentAcceleration - _decelerationFactor * Time.deltaTime, 0f, 1f);
                _hasToMove = false;
                return;
            }

            _hasToMove = true;
            _inputDirection = direction;
            _inputDirection.Normalize();

            _currentAcceleration = Mathf.Clamp(_currentAcceleration + _accelerationFactor * Time.deltaTime, 0f, 1f);
        }

        private void FixedUpdate()
        {
            Move();
            AccelRotation();
            Rotate();
            UpdateNet();
            UpdateCamera();
        }

        private void Move()
        {
            if (!_canMove)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            _rigidbody.velocity = new Vector3(_inputDirection.x, 0f, _inputDirection.y) * _currentSpeed;
        }

        private void AccelRotation()
        {
            float angle = Mathf.Lerp(0f, _accelMaxRotation, _currentAcceleration);
            _boatMesh.localRotation = Quaternion.Euler(angle, 0f, 0f);
        }

        private void Rotate()
        {
            if (!_hasToMove)
                return;

            Quaternion destRotation = Quaternion.LookRotation(new Vector3(_inputDirection.x, transform.position.y, _inputDirection.y));
            Quaternion newRot = Quaternion.Slerp(transform.rotation, destRotation, _turnSpeed);

            transform.rotation = Quaternion.Euler(new Vector3(0f, newRot.eulerAngles.y, 0f));
        }

        private void UpdateNet()
        {
            _net.NetMovement.UpdateTransform();
            _net.TopLeftLineRenderer.UpdateLineRendererPositions();
            _net.TopRightLineRenderer.UpdateLineRendererPositions();
            _net.BotLeftLineRenderer.UpdateLineRendererPositions();
            _net.BotRightLineRenderer.UpdateLineRendererPositions();
        }

        private void UpdateCamera()
        {
            _cameraController.UpdateCameraTransform();
        }

        #endregion Methods
    }
}