using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EtienneSibeaux.UI
{
    public abstract class CA_UI_Counter : CA_UIClass
    {
        [Header("---References---")]
        [SerializeField] protected TextMeshProUGUI _counterText;

        [SerializeField] protected Image _counterImage;
        [SerializeField] protected Animator _animator;

        protected int _currentValue = 0;

        public int CurrentValue { get => _currentValue; }

        public void SetValue(int value)
        {
            if (value > _currentValue)
                _animator.SetTrigger("Increment");

            _currentValue = value;
            UpdateText();
        }

        public void IncrementValue()
        {
            SetValue(_currentValue + 1);
        }

        protected virtual void UpdateText()
        {
            _counterText.text = _currentValue.ToString();
        }
    }
}