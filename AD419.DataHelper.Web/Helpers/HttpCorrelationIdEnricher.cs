using System;
using System.Web;
using Serilog.Core;
using Serilog.Events;

namespace AD_419_DataHelperWebApp.Helpers
{
    public class HttpCorrelationIdEnricher : ILogEventEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string HttpRequestIdPropertyName = "HttpRequestId";

        private static readonly string RequestIdItemName = typeof(HttpCorrelationIdEnricher).Name + "+RequestId";

        /// <summary>
        /// Enrich the log event with an id assigned to the currently-executing HTTP request, if any.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            var requestId = GetCorrelationId();

            var requestIdProperty = new LogEventProperty(HttpRequestIdPropertyName, new ScalarValue(requestId));
            logEvent.AddPropertyIfAbsent(requestIdProperty);
        }

        /// <summary>
        /// Get correlation id from current context
        /// </summary>
        /// <returns></returns>
        public static Guid GetCorrelationId()
        {
            if (HttpContext.Current == null)
                return Guid.NewGuid();

            Guid correlationId;
            var requestIdItem = HttpContext.Current.Items[RequestIdItemName];
            if (requestIdItem == null)
            {
                correlationId = Guid.NewGuid();
                SetCorrelationId(correlationId);
            }
            else
                correlationId = (Guid)requestIdItem;

            return correlationId;
        }

        /// <summary>
        /// Set correlation id for current context
        /// </summary>
        /// <param name="id"></param>
        public static void SetCorrelationId(Guid id)
        {
            if (HttpContext.Current == null)
                return;

            HttpContext.Current.Items[RequestIdItemName] = id;
        }
    }
}