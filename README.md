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

## Related Packages

This package is part of the Sunamo package ecosystem. For more information about related packages, visit the main repository.

## License

See the repository root for license information.
