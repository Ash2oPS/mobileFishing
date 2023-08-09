using EtienneSibeaux.Debugger;
using System;
using UnityEngine;

namespace EtienneSibeaux.Manager
{
    public class C_GameManager : MonoBehaviour
    {
        private static C_GameManager _instance;
        private CA_Manager[] _managers;

        public static C_GameManager Instance => _instance;

        #region Initialization

        private void Awake()
        {
            SingletonInit();
            ApplicationSettings();
            GetManagerComponents();
        }

        private void SingletonInit()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        private void ApplicationSettings()
        {
            Application.targetFrameRate = 60;
        }

        private void GetManagerComponents()
        {
            _managers = GetComponentsInChildren<CA_Manager>();

            foreach (CA_Manager manager in _managers)
            {
                manager.OnCreated();
            }
        }

        #endregion Initialization

        public T GetManager<T>() where T : CA_Manager
        {
            T manager = Array.Find(_managers, (m) => m.GetType() == typeof(T)) as T;

            if (manager == null)
            {
                string type = typeof(T).Name;
                this.ErrorMessage($"GetManager({type})", $"No Manager of type {type} is found.");
                return null;
            };

            return manager;
        }
    }
}