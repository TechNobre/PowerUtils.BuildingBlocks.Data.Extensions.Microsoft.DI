# PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI

![Logo](https://raw.githubusercontent.com/TechNobre/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI/main/assets/logo/logo_128x128.png)

***Scans assemblies and add the repositories in container. To use, with an IServiceCollection***

![Tests](https://github.com/TechNobre/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI/actions/workflows/tests.yml/badge.svg)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=TechNobre_PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=TechNobre_PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=TechNobre_PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI&metric=coverage)](https://sonarcloud.io/summary/new_code?id=TechNobre_PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI)

[![NuGet](https://img.shields.io/nuget/v/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI.svg)](https://www.nuget.org/packages/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI)
[![Nuget](https://img.shields.io/nuget/dt/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI.svg)](https://www.nuget.org/packages/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI)
[![License: MIT](https://img.shields.io/github/license/TechNobre/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI.svg)](https://github.com/TechNobre/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI/blob/main/LICENSE)

- [Support](#support-to)
- [Dependencies](#dependencies)
- [How to use](#how-to-use)
    - [Installation](#installation)
    - [ServiceCollectionExtensions](#ServiceCollectionExtensions)
      - [AddRepositories](#ServiceCollectionExtensions.AddRepositories)
- [Contribution](#contribution)
- [License](./LICENSE)
- [Changelog](./CHANGELOG.md)



## Support to <a name="support-to"></a>
- .NET 3.1 or more
- .NET Standard 2.1



## Dependencies <a name="dependencies"></a>

- PowerUtils.BuildingBlocks.Data [NuGet](https://www.nuget.org/packages/PowerUtils.BuildingBlocks.Data/)
- Microsoft.Extensions.DependencyInjection.Abstractions [NuGet](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions/)



## How to use <a name="how-to-use"></a>

### Install NuGet package <a name="installation"></a>
This package is available through Nuget Packages: https://www.nuget.org/packages/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI

**Nuget**
```bash
Install-Package PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI
```

**.NET CLI**
```
dotnet add package PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI
```



### ServiceCollectionExtensions <a name="ServiceCollectionExtensions"></a>

#### AddRepositories(); <a name="ServiceCollectionExtensions.AddRepositories"></a>
Clean extra spaces. Replace tabs to one space and double spaces to one space

```csharp
services.AddRepositories();
services.AddRepositories(typeof(Program).Assembly);
services.AddRepositories(ServiceLifetime.Transient, typeof(Program).Assembly);
```



## Contribution <a name="contribution"></a>

If you have any questions, comments, or suggestions, please open an [issue](https://github.com/TechNobre/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI/issues/new/choose) or create a [pull request](https://github.com/TechNobre/PowerUtils.BuildingBlocks.Data.Extensions.Microsoft.DI/compare)