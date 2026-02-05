# SunamoMsSqlServer

Pomocné metody a služby pro MS SQL Server.

## Rozdíly v Target Frameworku

### Hlavní Knihovna (SunamoMsSqlServer.csproj)
- **Target Frameworks**: net10.0, net9.0, net8.0
- **Důvod**: NuGet balíčky musí podporovat více verzí .NET pro širokou kompatibilitu

### Testovací a Runner Projekty
- **Target Framework**: pouze net10.0
- **Dotčené projekty**:
  - SunamoMsSqlServer.Tests
  - RunnerMsSqlServer
- **Důvod**: Testovací a runner projekty nepotřebují multi-targeting. Použití pouze nejnovější verze .NET (net10.0) zjednodušuje správu závislostí a vyhýbá se problémům s kompatibilitou balíčků.

## Změny Verzí Balíčků (2026-02-04)

### Entity Framework Core
- **Změněno z**: 10.0.2 → 9.0.1
- **Důvod**: EF Core 10.0.2 vyžaduje pouze .NET 10. Verze 9.0.1 podporuje net8.0, net9.0 i net10.0.

### Microsoft.Extensions.Logging.Abstractions
- **Změněno z**: 10.0.2 → * (nejnovější kompatibilní)
- **Důvod**: Použití wildcard zajistí že automaticky dostaneme nejnovější kompatibilní verzi, čímž se vyhneme konfliktům verzí se závislostmi.
