# SunamoMsSqlServer

Helpers and services for MS SQL Server.

## Target Framework Differences

### Main Library (SunamoMsSqlServer.csproj)
- **Target Frameworks**: net10.0, net9.0, net8.0
- **Reason**: NuGet packages must support multiple .NET versions for broad compatibility

### Test and Runner Projects
- **Target Framework**: net10.0 only
- **Affected projects**:
  - SunamoMsSqlServer.Tests
  - RunnerMsSqlServer
- **Reason**: Test and runner projects don't need multi-targeting. Using only the latest .NET version (net10.0) simplifies dependency management and avoids package compatibility issues.

## Package Version Changes (2026-02-04)

### Entity Framework Core
- **Changed from**: 10.0.2 to 9.0.1
- **Reason**: EF Core 10.0.2 requires .NET 10 only. Version 9.0.1 supports net8.0, net9.0, and net10.0.

### Microsoft.Extensions.Logging.Abstractions
- **Changed from**: 10.0.2 to * (latest compatible)
- **Reason**: Using wildcard ensures we get the latest compatible version automatically, avoiding version conflicts with dependencies.
