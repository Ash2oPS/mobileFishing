using UnityEngine;

namespace EtienneSibeaux.Net
{
    public class C_NetMovement : MonoBehaviour
    {
        [Header("---Parameters---")]
        [SerializeField] private float _maxDistanceFromBoat;

        [Header("---References---")]
        [SerializeField] private C_Net _net;

        private float _distanceFromBoat;
        private float _turnSpeed = 0.1f;

        public void UpdateTransform()
        {
            _distanceFromBoat = Vector3.Distance(_net.Player.NetParent.transform.position,
                new Vector3(
                    _net.transform.position.x,
                    _net.Player.NetParent.transform.position.y,
                    _net.transform.position.z));

            if (_distanceFromBoat < _maxDistanceFromBoat)
                return;

            UpdatePosition();
            Rotate();
        }

        private void UpdatePosition()
        {
            _net.transform.position = Vector3.Lerp(_net.transform.position,
                new Vector3(_net.Player.NetParent.transform.position.x, _net.transform.position.y, _net.Player.NetParent.transform.position.z),
                0.02f);
            //transform.position += _direction * 3 * Time.deltaTime;
        }

        private void Rotate()
        {
            Vector3 dirV3 = (_net.Player.NetParent.transform.position - _net.transform.position);
            Vector2 dirV2 = new Vector2(dirV3.x, dirV3.z).normalized;

            Quaternion destRotation = Quaternion.LookRotation(new Vector3(dirV2.x, _net.transform.position.y, dirV2.y));
            Quaternion newRot = Quaternion.Slerp(_net.transform.rotation, destRotation, _turnSpeed);

            _net.transform.rotation = Quaternion.Euler(new Vector3(
                _net.transform.rotation.eulerAngles.x,
                newRot.eulerAngles.y,
                _net.transform.rotation.eulerAngles.z));
        }
    }
}