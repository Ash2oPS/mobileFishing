using UnityEngine;

namespace EtienneSibeaux.Net
{
    public class C_NetSize : MonoBehaviour
    {
        private const float MIN_POS_Y = -0.642f;
        private const float MAX_POS_Y = -4.72f;
        private const float MIN_NET_SIZE = 0.6f;
        private const float MAX_NET_SIZE = 6f;

        public void SetSize(float value)
        {
            float newScale = Mathf.Clamp(value, MIN_NET_SIZE, MAX_NET_SIZE);
            float newPos = Mathf.Lerp(MIN_POS_Y, MAX_POS_Y, (value - MIN_NET_SIZE) / (MAX_NET_SIZE - MIN_NET_SIZE));

            transform.localScale = new Vector3(newScale, newScale, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, newPos, transform.position.z);
        }
    }
}