using HTTPDataCollectorAPI;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LogAnalytics.Extensions.Logging
{
	/// <summary>
	/// Azure Log Analytics Logger
	/// </summary>
	public class LogAnalyticsLogger : ILogger
	{
		private string _categoryName;
		private Func<string, LogLevel, bool> _filter;
		private ICollector _collector;
		private string _environmentName;

		public LogAnalyticsLogger(string categoryName, Func<string, LogLevel, bool> filter, ICollector collector, string environmentName)
		{
			_categoryName = categoryName;
			_filter = filter;
			_collector = collector;
			_environmentName = environmentName;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return (_filter == null || _filter(_categoryName, logLevel));
		}
		
		/// <summary>
		/// Sends logs to Azure Log Analytics
		/// </summary>
		/// <typeparam name="TState"></typeparam>
		/// <param name="logLevel"></param>
		/// <param name="eventId"></param>
		/// <param name="state"></param>
		/// <param name="exception"></param>
		/// <param name="formatter"></param>
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!IsEnabled(logLevel))
			{
				return;
			}

			if (formatter == null)
			{
				throw new ArgumentNullException(nameof(formatter));
			}
			var message = formatter(state, exception);

			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			if (exception != null)
			{
				message += $"{Environment.NewLine}{Environment.NewLine}{exception.ToString()}";
			}
			
			_collector.Collect($"{_environmentName}", new LogEvent() { CategoryName=_categoryName, LogLevel=logLevel.ToString(), Message=message })
				.ContinueWith(e => Debug.WriteLine(e.Exception), TaskContinuationOptions.OnlyOnFaulted);
		}

		/// <summary>
		/// Wrapper class to send a Json Payload
		/// </summary>
		private class LogEvent
		{
			public string CategoryName { get; set; }
			public string LogLevel { get; set; }
			public string Message { get; set; }
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}
	}
}
