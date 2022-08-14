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
        /*
        public static void RegistrarDanioJugador(int cantidad, string nombreEnemigo)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("Cantidad", cantidad);
            parametros.Add("nombreEnemigo", nombreEnemigo);
            TrackEvent("DanoJugador", parametros);
        }

        public static void RegistrarCompraJugador(int cantidadCompra, string nombreProducto)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("CantidadCompra", cantidadCompra);
            parametros.Add("NombreProducto", nombreProducto);
            TrackEvent("CompraJugador", parametros);

        }
        */
    }
}