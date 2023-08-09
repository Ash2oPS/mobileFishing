using EtienneSibeaux.Debugger;
using EtienneSibeaux.UI;
using System;

namespace EtienneSibeaux.Manager
{
    public class C_M_UIManager : CA_Manager
    {
        private CA_UIClass[] _uiClasses;

        private void Awake()
        {
            _uiClasses = FindObjectsOfType<CA_UIClass>();
        }

        public T GetUI<T>() where T : CA_UIClass
        {
            T uiClass = Array.Find(_uiClasses, (m) => m.GetType() == typeof(T)) as T;

            if (uiClass == null)
            {
                string type = typeof(T).Name;
                this.ErrorMessage($"GetManager({type})", $"No Manager of type {type} is found.");
                return null;
            };

            return uiClass;
        }
    }
}