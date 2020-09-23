# ImageHash

Solution which consists of:
- a WebApi to store Image info
- a Processor which processes image files and saves their info through the WebApi

## Projects

- `Models`: domain models and WebApi resource models
- `Processor`: .NET Core 3.1 console application
- `Shared`: common utility classes
- `WebApi`: .NET Core 3.1 WebApi
    - `WebApi.Controllers`: API endpoint controllers
    - `WebApi.Mapping`: AutoMapper configuration classes
    - `WebApi.Repositories`: entity repositories (data access layer)
    - `WebApi.Services`: entity managers
- `WebApi.Client`: client for WebApi consumation
- `WebApi.Test`: tests
