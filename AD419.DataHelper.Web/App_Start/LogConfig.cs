using AD419.DataHelper.Web.Helpers;
using Serilog;
using SerilogWeb.Classic.Enrichers;

namespace AD419.DataHelper.Web
{
    public class LogConfig
    {
        private static bool _loggingSetup;

        /// <summary>
        /// Configure Application Logging
        /// </summary>
        public static void ConfigureLogging()
        {
            if (_loggingSetup) return; //only setup logging once

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Stackify()
                .Enrich.With<HttpCorrelationIdEnricher>()
                .Enrich.With<HttpSessionIdEnricher>()
                .Enrich.With<UserNameEnricher>()
                .Enrich.FromLogContext()
                .CreateLogger();

            _loggingSetup = true;
        }
    }
}