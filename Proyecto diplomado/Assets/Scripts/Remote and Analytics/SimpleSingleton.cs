using UnityEngine;

namespace UnityGamingServices
{
    public abstract class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance => _instance;
        
        private void Awake()
        {
            if (_instance == null)
            {   // mark this object as singleton instance
                _instance = this as T;
                AfterAwake();
            }
            else
            {   // destroy this object. T already exists
                Destroy(gameObject);
            }
        }

        protected virtual void AfterAwake() {}

        protected virtual void BeforeDestroy(){}

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {   // remove this object from singleton instance
                BeforeDestroy();
                _instance = null;
            }
        }
    }
}
