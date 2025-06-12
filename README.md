# ASP.NET Core WebAPI  -RESTful Webservices mit C#
---
KursRepository zu Kurs ASP.NET Core Web API - RESTful Webservices mit C# der ppedv AG

## Modul 001 Einf�hrung WebAPI

-	[x] ASP.NET Core Empty: Hello World
-	[x] WheaterForecastAPI erstellt
-	[x] Projektstruktur erkl�rt
-	[x] [httpFiles](https://learn.microsoft.com/en-us/aspnet/core/test/http-files)

## Modul 002 Konfiguration

-	[ ] IOC mittels Dependency Injection
	- ServiceCollection
	- Dependency Tree
-	[ ] Logging in ASP.Net Core
	- Serilog, FileSink, OpenTelemetry
	- OpenTelemetry und datalust/seq

## Modul 003 Services & Controllers

-	[ ] BusinessLogic Class Library Project erstellt
	- Service und Domain Model
	- Contracts
-	[ ] Controller mit CRUD Operationen
	- Route constraints
	- [Model Binding](https://learn.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/)
-	[ ] Best Practices: DTOs, Mapper
-	[ ] ModelState & Validation Attributes
-	[ ] LAB: Movie Store Api

## M004 | Model View Controller (MVC)

-   [ ] Overview
-   [ ] Links setzen
-   [ ] Index und Details
-   [ ] ViewModel Mapping
-   [ ] Form Post & Validierung
-   [ ] ModelState
-   [ ] Lab: MovieService und MVC App mit Index und Details


## Modul 005 EF Core, Async/Await

-	[ ] Code First: VehicleManagement Datenbank
-	[ ] Datenklasse mit Attriuten versetzt
-	[ ] DbContext & Seeding
-	[ ] Abh�ngigkeiten via DI registriert
-	[ ] Async/Await Pattern
-	[ ] LAB: DB f�r Movie Store erstellen

```
dotnet tool install --global dotnet-ef
dotnet ef migrations add myInitialScript --project myProject
dotnet ef database update --project myProject
```
Alternativ DB erzeugen via Package Manager Console
```
Add-Migration MyInitialScriptName
Update-Database
```

-	[ ] Db First: Northwind Datenbank
-   [ ] [Northwind DB](https://github.com/microsoft/sql-server-samples/blob/master/samples/databases/northwind-pubs/instnwnd.sql)
-	[ ] VS Extension [EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools)
-	[ ] Controller erzeugen
-	[ ] LAB: Daten von Northwind abfragen
		* Alle Bestellungen
		* Alle Bestellungen innerhalb eines Zeitraumes (Parameter: StartDate, EndDate)
		* Bestellungen pro Kunde (Parameter: CustomerID)
		* Kunden pro Land (Parameter: Country)

## Modul 008 Authentication

-	[ ] Middleware f�r Authentication konfigurieren
-	[ ] IdentityDbContext verwenden
-	[ ] JwtToken erstellen


## Modul 011 Weitere Themen

-   [ ] Deployment 
	- [Docker](https://learn.microsoft.com/de-de/visualstudio/containers/container-build)

```bash
docker build -f ./<projectfolder>/Dockerfile -t meine-webapi .
docker run -d -p 8080:80 --name webapi-container meine-webapi
```
