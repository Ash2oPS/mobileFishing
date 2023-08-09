using EtienneSibeaux.Player;
using UnityEngine;

namespace EtienneSibeaux.MyCamera
{
    [RequireComponent(typeof(Camera))]
    public class C_CameraController : MonoBehaviour
    {
        private const float MIN_HEIGHT = 10f;
        private const float MAX_HEIGHT = 19f;

        [Header("---Parameters---")]
        [SerializeField] private Vector2 _positionOffset;

        [Header("---References---")]
        [SerializeField] private Camera _camera;

        private C_Player _player;

        private void Awake()
        {
            _player = FindObjectOfType<C_Player>();
        }

        public void UpdateCameraTransform()
        {
            transform.forward = new Vector3(
                0f,
                _player.transform.position.y - transform.position.y,
                _player.transform.position.z - transform.position.z);

            transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, _player.transform.position.x + _positionOffset.x, 0.05f),
                transform.position.y,
                Mathf.Lerp(transform.position.z, _player.transform.position.z + _positionOffset.y, 0.05f));
        }

        public void SetHeight(float factor)
        {
            float height = Mathf.Lerp(MIN_HEIGHT, MAX_HEIGHT, factor);

            transform.position = new Vector3(
                transform.position.x,
                height,
                transform.position.z);

            UpdateCameraTransform();
        }
    }
}