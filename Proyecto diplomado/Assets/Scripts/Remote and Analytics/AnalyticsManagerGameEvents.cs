using System.Collections.Generic;

namespace UnityGamingServices
{
    public partial class AnalyticsManager
    {
        public static void FlujoEscenas(string nombreEscena)
        {
            Dictionary<string, object> parametros = new Dictionary<string,object>();
            parametros.Add("nombreEscena", nombreEscena);
            TrackEvent("Flujo", parametros);
        }
    }
}