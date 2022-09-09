# MovieClubClient
Ett exempel på Client mot Daniels api 

## Generera proxy class med hjälp av NSwag


**Delta**
```

.\ApiClientGenerator.exe https://app-dev-movie-api.azurewebsites.net/swagger/v1/swagger.json c:\temp\MovieClubApiClient.cs
```

**Gamma**
```
.\ApiClientGenerator.exe https://app-lab-codingdojo-backend.azurewebsites.net/swagger/v1/swagger.json c:\temp\MovieClubApiClient.cs
```

## Console app som att anropa web api med hjälp av proxy classen

```
.\MovieClubClient.exe
```
