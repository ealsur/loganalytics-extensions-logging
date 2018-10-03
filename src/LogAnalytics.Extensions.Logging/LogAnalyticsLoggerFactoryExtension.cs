using System;
using Microsoft.Extensions.Logging;

namespace LogAnalytics.Extensions.Logging
{
    
    /// <summary>
    /// Extends <see cref="ILoggerFactory"/> with Serilog configuration methods.
    /// </summary>
    public static class LogAnalyticsLoggerFactoryExtension
    {
        /// <summary>
        /// Add Azure LogAnalytics to the logging pipeline.
        /// </summary>
        /// <param name="factory">The logger factory to configure.</param>
        /// <param name="workspaceId">The Azure Log Analytics Workspace Id.</param>
        /// <param name="key">The API Key of the Azure Log Analytics endpoint</param>
        /// <param name="serviceNamespace">Optional. Allows to change the service endpoint to use the library in different clouds.</param>
        /// <returns>The logger factory.</returns>
        public static ILoggerFactory AddLogAnalytics(this ILoggerFactory factory, string workspaceId, string key, string environmentName, Func<string, LogLevel, bool> filter = null, string serviceNamespace = null)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            factory.AddProvider(new LogAnalyticsLoggerProvider(filter, workspaceId, key, environmentName, serviceNamespace));

            return factory;
        }

        /// <summary>
        /// Add Azure LogAnalytics to the logging pipeline.
        /// </summary>
        /// <param name="factory">The logger factory to configure.</param>
        /// <param name="workspaceId">The Azure Log Analytics Workspace Id.</param>
        /// <param name="key">The API Key of the Azure Log Analytics endpoint</param>
        /// <param name="serviceNamespace">Optional. Allows to change the service endpoint to use the library in different clouds.</param>
        /// <returns>The logger factory.</returns>
        public static ILoggerFactory AddLogAnalytics(this ILoggerFactory factory, string workspaceId, string key, string environmentName, LogLevel minLevel, string serviceNamespace = null)
		{
			return AddLogAnalytics(factory, workspaceId, key, environmentName,(_, logLevel) => logLevel >= minLevel, serviceNamespace);
		}
	}

}
