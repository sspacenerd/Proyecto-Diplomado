using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace UnityGamingServices
{
    public class UnityServicesManager : Singleton<UnityServicesManager>
    {
        [Tooltip("The userId to use for analytics/remote config")]
        [SerializeField] private string _userId;
        [Tooltip("Environment to use for analytics/remote config")]
        [SerializeField] private string _envId = "production";

        public string UserId => _userId;
        public string EnvId => _envId;

        public bool IsInitialized { get; private set; }

        private void Start()
        {
            if (string.IsNullOrWhiteSpace(_userId))
            {
                _userId = SystemInfo.deviceUniqueIdentifier;
            }

            InitializeAsync();
        }

        // Usando referencias de:
        // https://docs.unity3d.com/Packages/com.unity.remote-config@3.1/manual/CodeIntegration.html
        // https://docs.unity.com/analytics/AnalyticsSDKGuide.html

        private async void InitializeAsync()
        {
            try
            {
                var options = new InitializationOptions();
                if (!string.IsNullOrEmpty(_userId))
                {
                    options.SetAnalyticsUserId(_userId);
                    options.SetOption("com.unity.services.core.environment-name", _envId);
                }
                await UnityServices.InitializeAsync(options);
                List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();

                await TryLogIn();
                
                IsInitialized = true;
            }
            catch(ConsentCheckException e)
            {
                Debug.LogError($"Unable to check user consent for analytics tracking: {e.Message}");
            }                           
        }

        // Usando referencias de:
        // https://docs.unity3d.com/Packages/com.unity.remote-config@3.1/manual/CodeIntegration.html
        

        private async Task TryLogIn()
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {   // remote config requires authentication for managing environment information
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }
    }
}