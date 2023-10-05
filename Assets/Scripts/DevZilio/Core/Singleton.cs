using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DevZilio.Core.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        private bool _persistent = false;

        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = GetComponent<T>();
            else
                Destroy(gameObject);

            if(_persistent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public Singleton(bool persistent)
        {
            _persistent = persistent;
        }

        public Singleton(){}

    }
}