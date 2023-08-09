using EtienneSibeaux.Debugger;
using EtienneSibeaux.Manager;
using EtienneSibeaux.Sale;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EtienneSibeaux.UI
{
    public class C_UI_SellFishButton : CA_UIClass
    {
        private const string NO_FISH_TEXT = "You don't have any Fish to sell!";
        private const string YES_FISH_TEXT = "Sell Fish";

        [Header("---References---")]
        [SerializeField] private Button _button;

        [SerializeField] private Image _buttonImage;

        [SerializeField] private TextMeshProUGUI _buttonText;

        private Camera _camera;
        private C_M_PlayerManager _playerManager;
        private Transform _buttonWorldTransform;
        private C_UI_SoldFishWindow _soldFishWindow;

        private bool _isEnabled = false;

        public bool IsEnabled { get => _isEnabled; }

        private void Awake()
        {
            _playerManager = C_GameManager.Instance.GetManager<C_M_PlayerManager>();
            _soldFishWindow = C_GameManager.Instance.GetManager<C_M_UIManager>().GetUI<C_UI_SoldFishWindow>();

            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            if (!_isEnabled)
                return;

            UpdateButtonPostion();
        }

        private void UpdateButtonPostion()
        {
            Vector2 onScreenPosition = _camera.WorldToScreenPoint(_buttonWorldTransform.position);
            _button.transform.position = onScreenPosition;
        }

        public void SetButtonEnabled(bool value, Transform buttonWorldTransform)
        {
            _isEnabled = value;
            _buttonWorldTransform ??= buttonWorldTransform;
            _button.enabled = value;
            _buttonImage.enabled = value;
            _buttonText.enabled = value;

            if (!value) return;

            UpdateButtonPostion();
            CheckNumberOfFish();
        }

        public void SetButtonEnabled(bool value)
        {
            SetButtonEnabled(value, _buttonWorldTransform);
        }

        public void CheckNumberOfFish()
        {
            bool noFish = _playerManager.NumberOfFish == 0;

            _button.interactable = !noFish;
            _buttonText.text = noFish ? NO_FISH_TEXT : YES_FISH_TEXT;
        }

        public void SellFish()
        {
            _soldFishWindow.DisplaySalesInfos(_playerManager.FishList.ToArray());
        }
    }
}