using EtienneSibeaux.Fish;
using EtienneSibeaux.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EtienneSibeaux.UI
{
    public class C_UI_SoldFishBox : CA_UIClass
    {
        [Header("---Data---")]
        [SerializeField] private SO_FishAsset _fishAsset;

        [Header("---References---")]
        [SerializeField] private Image _fishImage;

        [SerializeField] private TextMeshProUGUI _priceTMP;
        [SerializeField] private TextMeshProUGUI _nameOfFishTMP;
        [SerializeField] private TextMeshProUGUI _numberOfSoldTMP;
        [SerializeField] private RectTransform _newTextTransform;
        [SerializeField] private Sprite _questionMarkSprite;

        private bool _isNew;
        private bool _isDiscovered;

        public SO_FishAsset FishAsset { get => _fishAsset; }
        public bool IsNew { get => _isNew; }
        public bool IsDiscovered { get => _isDiscovered; }

        private void Awake()
        {
            SetAsUnknown();
        }

        private void SetAsUnknown()
        {
            _fishImage.sprite = _questionMarkSprite;
            _priceTMP.text = "???";
            _nameOfFishTMP.text = "???";
        }

        public void SetQuantity(int quantity)
        {
            _numberOfSoldTMP.text = quantity.ToString("000") + " x";
        }

        public void DiscoverAsset()
        {
            _isNew = true;
            _fishImage.sprite = _fishAsset.FishSprite;
            _priceTMP.text = _fishAsset.FishPrice + " $";
            _nameOfFishTMP.text = _fishAsset.FishName;
            _newTextTransform.gameObject.SetActive(true);
            _isDiscovered = true;
        }

        public void SetAsNotNew()
        {
            _isNew = false;
            _newTextTransform.gameObject.SetActive(false);
        }
    }
}