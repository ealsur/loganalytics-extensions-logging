# Azure Log Analytics ASP.NET Core Logging extension

## Get it

You can obtain this project as a [Nuget Package](https://www.nuget.org/packages/LogAnalytics.Extensions.Logging). 

    Install-Package LogAnalytics.Extensions.Logging

Or reference it and use it according to the [License](./LICENSE).

## Use it

Using it is simple, just reference the package and add it to the Logging pipeling in your `Startup.cs` file:

```cs
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			//Add Logger
			loggerFactory.AddLogAnalytics("{Your_Workspace_Id}", "{Your_Key_Id}", "{Your_Namespace}", LogLevel.Critical);
			app.UseMvcWithDefaultRoute();			
		}
```
Your **WorkspaceId** and **API Key** are part of your Azure Log Analytics subscription.

The **Namespace** is the name that _identifies your LogType_ in Azure Log Analytics, mostly used to identify your application or source.


## Issues

Please feel free to [report any issues](https://github.com/ealsur/loganalytics-extensions-logging/issues) you might encounter. Keep in mind that this library won't assure that your JSON payloads are being indexed, it will make sure that the HTTP Data Collection API [responds an Accept](https://azure.microsoft.com/en-us/documentation/articles/log-analytics-data-collector-api/#return-codes) but there is no way (right now) to know when has the payload been indexed completely. 

