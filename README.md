# MovieClubClient
Ett exempel på Client mot Daniels api 

## Generera proxy class med hjälp av NSwag

```
.\ApiClientGenerator.exe https://app-dev-movie-api.azurewebsites.net/swagger/v1/swagger.json c:\temp\MovieClubApiClient.cs
```

## Console app som att anropa web api med hjälp av proxy classen

```
.\MovieClubClient.exe
```
