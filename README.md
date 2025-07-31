# Ofqual Shutter Page

Ofqual shutter page shutter page is displayed when a service is temporarily unavailable.

It may be shown during:

- Scheduled maintenance
- Deployment
- Service outages

## Provider

[The Office of Qualifications and Examinations Regulation](https://www.gov.uk/government/organisations/ofqual)

## About this project

This is a static page used as a temporary placeholder for the main ASP.NET Core 8 web app.

The main application follows the MVC architecture and uses Docker for deployment. It is hosted on an App Service for Container Apps on Azure.

# Application Configuration Guide

This document outlines how the application is configured using `appsettings.json` files. These settings help manage behaviour across different environments and scenarios, including development, production and testing.

## Application Settings (`appsettings.json`)

The main application settings are defined in `appsettings.json` and can be tailored per environment using files like `appsettings.Development.json` or `appsettings.Production.json`.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "LogzIo": {
    "Environment": "",
    "Uri": ""
  }
}
```

### Setting Details

- **`LogzIo:Environment`**
  Identifies the current environment in the logs (e.g., `DEV`, `PREPROD`, `PROD`). This helps separate log entries across environments.

- **`LogzIo:Uri`**
  The endpoint URI for sending log data to an external logging service such as Logz.io.

> These settings should be environment-specific and managed through `appsettings.{Environment}.json` or overridden using environment variables in production scenarios.