# SunamoMsSqlServer

Helpers and services for MS Sql Server

## Overview

SunamoMsSqlServer is part of the Sunamo package ecosystem, providing modular, platform-independent utilities for .NET development.

## Main Components

### Key Classes

- **IdentityHelpers**
- **MsSqlConnectHelper**
- **MsSqlOneColumnService**
- **MsSqlService**
- **UniqueIdService**
- **ResultWithExceptionMsSqlServer**

### Key Methods

- `Open()`
- `Close()`
- `MsSqlOneColumnService()`
- `Int()`
- `MsSqlService()`
- `GetAndOpenConnection()`
- `DeleteAll()`
- `UniqueIdService()`
- `RevokeInsert()`
- `GrantInsert()`

## Installation

```bash
dotnet add package SunamoMsSqlServer
```

## Dependencies

- **Microsoft.Data.SqlClient** (v6.0.1)
- **Microsoft.EntityFrameworkCore** (v9.0.3)
- **Microsoft.EntityFrameworkCore.Relational** (v9.0.3)
- **Microsoft.Extensions.Logging.Abstractions** (v9.0.3)

## Package Information

- **Package Name**: SunamoMsSqlServer
- **Version**: 25.6.7.1
- **Target Framework**: net9.0
- **Category**: Platform-Independent NuGet Package
- **Source Files**: 7

## Target Framework Differences

### Main Library (SunamoMsSqlServer.csproj)
- **Target Frameworks**: net10.0, net9.0, net8.0
- **Reason**: NuGet packages must support multiple .NET versions for broad compatibility

### Test & Runner Projects
- **Target Framework**: net10.0 only
- **Projects affected**:
  - SunamoMsSqlServer.Tests
  - RunnerMsSqlServer
- **Reason**: Test and runner projects don't need multi-targeting. Using only the latest .NET version (net10.0) simplifies dependency management and avoids package compatibility issues.

## Package Version Changes (2026-02-04)

### Entity Framework Core
- **Changed from**: 10.0.2 → 9.0.1
- **Reason**: EF Core 10.0.2 requires .NET 10 only. Version 9.0.1 supports net8.0, net9.0, and net10.0.

### Microsoft.Extensions.Logging.Abstractions
- **Changed from**: 10.0.2 → * (latest compatible)
- **Reason**: Using wildcard ensures we get the latest compatible version automatically, avoiding version conflicts with dependencies.

## Related Packages

This package is part of the Sunamo package ecosystem. For more information about related packages, visit the main repository.

## License

See the repository root for license information.
