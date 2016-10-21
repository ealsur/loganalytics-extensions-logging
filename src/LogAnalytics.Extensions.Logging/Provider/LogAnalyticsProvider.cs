using System;
using Microsoft.Extensions.Logging;
using HTTPDataCollectorAPI;

namespace LogAnalytics.Extensions.Logging
{
	/// <summary>
	/// Azure Log Analytics Logger provider.
	/// </summary>
	public class LogAnalyticsLoggerProvider : ILoggerProvider
	{
		private readonly Func<string, LogLevel, bool> _filter;
		private readonly ICollector _collector;
		private readonly string _environmentName;
		public LogAnalyticsLoggerProvider(Func<string, LogLevel, bool> filter, string workspaceId , string key, string environmentName)
		{
			_collector = new Collector(workspaceId, key);
			_filter = filter;
			_environmentName = environmentName;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new LogAnalyticsLogger(categoryName, _filter, _collector, _environmentName);
		}

		public void Dispose()
		{
		}
	}
}