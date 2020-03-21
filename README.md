# C# REST client for Edipost API

## Nuget
This package is also available from Nuget
https://www.nuget.org/packages/EDIPost.EDIPostService

## Integration tests

Make sure constants points to an existing consignee and consignment. Make the needed modifications in EDIPostServiceTests\EDIPostService.cs

```
public const int DEFAULT_CONSIGNOR_ID = 3311;
public const int CONSIGNEE_ID = 3270125;
public const int CONSIGNMENT_ID = 3331708;
public const int CONSIGNMENT_ZPL_ID = 3334708;
```
