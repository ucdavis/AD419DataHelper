using System;
using Serilog;
using SerilogWeb.Classic.Enrichers;

using AD_419_DataHelperWebApp.Helpers;

namespace AD_419_DataHelperWebApp
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