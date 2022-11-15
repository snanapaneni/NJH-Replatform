# NJH - National Jewish Health - nationaljewishhealth.org

## Purpose:

## Developer Orientation

- [Main Document]({replace with technical google doc}) **All Devs Must read through it**

## Installation Instructions:

### Pre-requisites

- .NET Framework 4.8
- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- Visual Studio 2022 (major/minor: 17.0.2+)
- Node ^18.12.1
- NPM ^6.4.1

Clone this repo somewhere on your machine to a folder called e.g. NJH.

### IIS

#### Admin site

- Create a site in IIS called `NJH-Admin`
- Set physical path to `<Local Project Git Folder Root>\Njh_Admin\CMS`
- Set host name to `njhmvc-admin-lh.reasononeinc.com`
- Ensure to setup with https using reasonone SSL certificate.

- Add the following line to your `hosts` file:

  ```
  127.0.0.1      njhmvc-admin-lh.reasononeinc.com
  ```

- Navigate to `<Local Project Git Folder Root>\Njh_Admin\CMS\`

- Configure your connection string:

  - Create a new config file named `ConnectionStrings.config`.
  - Copy the content of `ConnectionStrings.config.template` to the new file you just created.
  - Set the connection string with the SQL Server for local development. The connection string for local development can be found in [MyGlue] (https://app.myglue.com/22980/passwords/19588024)

- Configure your local web farm name:

  - Copy & paste the file `LocalSettings.config.template`

  - Rename the copy as `LocalSettings.config` and open it

  - Modify the `CMSWebFarmServerName` value to replace XX with your initials, e.g. for John Ramamoorthy you would modify "NJHAdminXX" to "NJHAdminJR" _(Note that the name of the web farm is different for Admin and MVC sites)_

- Open the VS solution at `<Local Project Git Folder Root>\Njh_Admin\Njh_Admin.sln` (in Administrator mode) and rebuild it

  - Set up RO Nuget feed https://dev.azure.com/reasonone/K12%20MVC%20Starter%20Template/_wiki/wikis/K12-MVC-Starter-Template.wiki/11/Adding-Reason-One-Nuget-Feed

  - Make sure that automatic restoring of Nuget packages is enabled in your VS instance

- Verify that the Admin site is setup correctly by browsing over to `njhmvc-admin-lh.reasononeinc.com`

#### MVC site

- Add the following line to your `hosts` file:

  ```
  127.0.0.1  njh-lh.reasononeinc.com
  ```

- Configure your connection string:

  - **Don't modify the** `appsettings.json` **file**. Instead, configure the user secrets for the Njh project.

  - Add the section below to the user secrets:

    ```
    "ConnectionStrings": {
       "CMSConnectionString": ""
    }
    ```

  - Set the connection string with the SQL Server for local development.
    The connection string for local development can be found in [MyGlue](https://app.myglue.com/22980/passwords/19588024).

- Configure your local web farm name:

  - **Don't modify the** `appsettings.json` **file**. Instead, configure the user secrets for the Njh project.

  - Add the `CMSWebFarmServerName` key to the user secrets: `"CMSWebFarmServerName": "NJHMvcXX",`

  - Replace the XX with your initials, e.g. for John Ramamoorthy you would modify "NJHMvcXX" to "NJHMvcJR"

  _(Note that the name of the web farm is different for Admin and MVC sites)_

- Open the VS solution at `<Local Project Git Folder Root>\Njh\Njh.sln` (in Administrator mode) and rebuild it

  - Set up RO Nuget feed https://dev.azure.com/reasonone/K12%20MVC%20Starter%20Template/_wiki/wikis/K12-MVC-Starter-Template.wiki/11/Adding-Reason-One-Nuget-Feed

  - Make sure that automatic restoring of Nuget packages is enabled in your VS instance

- Either run the project through IIS Express or Kestrel

##### User Secrets

User secrets can be managed using Visual Studio:

- In Visual Studio, in the Solution Explorer window, right-click the `Njh` project

- From the popup menu, click `Manage User Secrets`

See the [official documentation](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows)
for more info.

- Set the path to the certificate file (with the `*.pfx` extension) to the `Path` key.
  **See the SSL section to download the Reason One certificate**.

- Set the password for the certificate to the `Password` key.

- `cd` into the Njh project folder

- `dotnet run` or `dotnet watch run`

- Navigate to https://njhmvc-lh.reasononeinc.com:44326

#### SSL

The SSL certificate for the `*.reasononeinc.com` domain and the password can be downloaded
from [Confluence](https://reasonone.atlassian.net/wiki/spaces/ROD/pages/1639252003/Reason+One+Wildcard+Certificate).

## Web Config Transforms

The project is set up with multiple web config transforms that automatically get applied per environment level.

This allows us to configure web config variables / attributes per environment within the repo.

See https://dev.azure.com/reasonone/K12%20MVC%20Starter%20Template/_wiki/wikis/K12-MVC-Starter-Template.wiki/2/Azure-Dev-Ops-Pipeline-Configurations
for more info.

## NJH
