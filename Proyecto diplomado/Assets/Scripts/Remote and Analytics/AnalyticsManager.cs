using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

namespace UnityGamingServices
{
    public partial class AnalyticsManager : Singleton<AnalyticsManager>
    {
        [Tooltip("If enabled, analytics events will be send right after they are created")]
        [SerializeField] private bool _autoSendEvents = true;
        
        private static void TrackEvent(string eventId, Dictionary<string, object> parameters)
        {
            if (Instance == null) {
                Debug.LogError($"{Instance.GetType()} is not initialized");
                return; 
            }
            
            if (!UnityServicesManager.Instance.IsInitialized)
            {
                Debug.LogWarning($"{Instance.GetType()}.{nameof(TrackEvent)}: UnityServicesManager is not initialized");
                return;
            }
            
            AnalyticsService.Instance.CustomData(eventId, parameters);

            if (Instance._autoSendEvents)
            {   // send events right after they are created
                AnalyticsService.Instance.Flush();
            }
        }
    }
}