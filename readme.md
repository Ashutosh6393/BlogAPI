
# Implementing Cache

- client side cache (browser)
- InMemory cache
- Redis

## InMemory Cache

1. In program.cs add 
	`builder.Services.AddInMemoryCache()`

2. In Service or Controller constructor
```cs
private readonly IMemoryCache _cache;
private const int CacheDurationMinutes = 1;
public ProductsController(IMemoryCache cache)
{
    _cache = cache;
}


//  _cache has all the methods to set, delete

```

## Browser Cache

1. Only need to mention this before a particular which you want to cache
 ```
  [ResponseCache(Duration =10, Location =ResponseCacheLocation.Client)]
  [HttpGet]
 ```



 what I have Implemented in Asp.NET core mvc web api

- Data base connection (code first apporach)
- DTO
- Authentication
- JWT
- cookies
- Password hashing
- Repository Pattern (Controller, Services, Repository)
- Middleware??
- logging??
- validation??
- cache??
- cors??
- api versioning?



moc
signalR
testing: xunit- DI test
identity
microservices- ocelot





 