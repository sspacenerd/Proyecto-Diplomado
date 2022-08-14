using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace UnityGamingServices
{
    public class RemoteSettingsManager : SimpleSingleton<RemoteSettingsManager>
    {
        public struct userAttributes{};
        public struct appAttributes
        {
            public string appName;
            public string appVersion;
        }

        public static bool IsReady { get; private set; }

        private void Start()
        {
            InitializeAsync();
        }

        // Usando referencias de:
        // https://docs.unity3d.com/Packages/com.unity.remote-config@3.1/manual/CodeIntegration.html

        private async void InitializeAsync()
        {
            await TaskUtils.WaitUntil(()=>UnityServicesManager.Instance.IsInitialized);

            // Add a listener to apply settings when successfully retrieved:
            RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;

            // Set the user’s unique ID:
            RemoteConfigService.Instance.SetCustomUserID(UnityServicesManager.Instance.UserId);
            // Set the environment ID:
            RemoteConfigService.Instance.SetEnvironmentID(UnityServicesManager.Instance.EnvId);

            // Fetch configuration settings from the remote service:
            RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        }

        // Usando referencias de:
        // https://docs.unity3d.com/Packages/com.unity.remote-config@3.1/manual/CodeIntegration.html#fetching-and-applying-settings-at-runtime
        private void ApplyRemoteSettings(ConfigResponse configResponse)
        {
            switch(configResponse.requestOrigin)
            {
                case ConfigOrigin.Default:
                    Debug.Log ("No settings loaded this session; using default values.");
                    break;
                case ConfigOrigin.Cached:
                    IsReady = true;
                    Debug.Log ("No settings loaded this session; using cached values from a previous session.");
                    break;
                case ConfigOrigin.Remote:
                    IsReady = true;
                    Debug.Log ("New settings loaded this session; update values accordingly.");
                    break;
            }
        }

        public static T GetConfig<T>(string key, T defaultValue = default(T)) where T : IConvertible
        {
            TypeCode typeCode = Type.GetTypeCode(typeof(T));
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    var boolDefaultValue = Convert.ToBoolean(defaultValue);
                    var result = RemoteConfigService.Instance.appConfig.GetBool(key, boolDefaultValue);
                    return (T)Convert.ChangeType(result, typeof(T));
                case TypeCode.Int32:
                    var intDefaultValue = Convert.ToInt32(defaultValue);
                    var intResult = RemoteConfigService.Instance.appConfig.GetInt(key, intDefaultValue);
                    return (T)Convert.ChangeType(intResult, typeof(T));
                case TypeCode.Int64:
                    var longDefaultValue = Convert.ToInt64(defaultValue);
                    var longResult = RemoteConfigService.Instance.appConfig.GetLong(key, longDefaultValue);
                    return (T)Convert.ChangeType(longResult, typeof(T));
                case TypeCode.String:
                    var stringDefaultValue = Convert.ToString(defaultValue);
                    var stringResult = RemoteConfigService.Instance.appConfig.GetString(key, stringDefaultValue);
                    return (T)Convert.ChangeType(stringResult, typeof(T));
                case TypeCode.Single:
                    var floatDefaultValue = Convert.ToSingle(defaultValue);
                    var floatResult = RemoteConfigService.Instance.appConfig.GetFloat(key, floatDefaultValue);
                    return (T)Convert.ChangeType(floatResult, typeof(T));
                case TypeCode.Double: // fallthrough, remote config doesn't support double
                    var doubleDefaultValue = Convert.ToSingle(defaultValue);
                    var doubleResult = RemoteConfigService.Instance.appConfig.GetFloat(key, doubleDefaultValue);
                    return (T)Convert.ChangeType(doubleResult, typeof(T));
                default:
                    Debug.LogError($"Unsupported type: {typeCode}. Returning default value.");
                    return defaultValue;
            }
        }

        protected override void BeforeDestroy()
        {
            IsReady = false; // set "ready" to false before destroying
        }
    }
}