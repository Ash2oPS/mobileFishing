using System.Collections;
using UnityEngine;

namespace EtienneSibeaux.Fish
{
    [RequireComponent(typeof(Rigidbody))]
    public class C_FishMovement : MonoBehaviour
    {
        private const float MIN_DELAY = 2.1f;
        private const float MAX_DELAY = 5f;

        [Header("---Parameters---")]
        [SerializeField] private AnimationCurve _moveCurve;

        [Header("---References---")]
        [SerializeField] private C_Fish _fish;

        [SerializeField] private Rigidbody _rigidbody;

        private float _speed;
        private Coroutine _moveCoroutine;

        private void Start()
        {
            StartMoving();
        }

        public void SetSpeed(float value)
        {
            _speed = value;
        }

        public void StartMoving()
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(MoveLoop());
        }

        public void StopMoving()
        {
            if (_moveCoroutine == null)
                return;

            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }

        private IEnumerator MoveLoop()
        {
            float delay;
            while (true)
            {
                delay = Random.Range(MIN_DELAY, MAX_DELAY);
                yield return new WaitForSeconds(delay);
                StartCoroutine(
                    Moving(
                        new Vector2(
                            Random.Range(-1f, 1f),
                            Random.Range(-1f, 1f)),
                        Random.Range(1f, 3f)));
            }
        }

        private IEnumerator Moving(Vector2 dir, float duration)
        {
            float timer = 0f;
            float factor;
            float currentSpeed;

            while (timer < duration)
            {
                factor = timer / duration;
                currentSpeed = _moveCurve.Evaluate(factor) * _speed;
                _rigidbody.velocity = new Vector3(dir.x * currentSpeed, 0f, dir.y * currentSpeed);
                yield return new WaitForEndOfFrame();
                timer = Mathf.Clamp(timer + Time.deltaTime, 0f, duration);
            }
        }
    }
}