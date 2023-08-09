using EtienneSibeaux.Debugger;
using EtienneSibeaux.Manager;
using EtienneSibeaux.Player;
using EtienneSibeaux.UI;
using UnityEngine;

namespace EtienneSibeaux.Sale
{
    public class C_SaleZone : MonoBehaviour
    {
        [Header("---References---")]
        [SerializeField] private Transform _buttonParent;

        private C_UI_SellFishButton _sellFishButton;

        private void Awake()
        {
            _sellFishButton = C_GameManager.Instance.GetManager<C_M_UIManager>().GetUI<C_UI_SellFishButton>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out C_Player player))
                return;

            _sellFishButton.SetButtonEnabled(true, _buttonParent);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out C_Player player))
                return;

            _sellFishButton.SetButtonEnabled(false, _buttonParent);
        }
    }
}