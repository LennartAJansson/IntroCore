Design Patterns (Design Patterns: Elements of Reusable Object‑Oriented Software)
Gemma, Vlissides, Helm & Johnson

```csharp
public class Factory<T> : IFactory<T>
{
    private readonly Func<T> _initFunc;

    public Factory(Func<T> initFunc)
    {
        _initFunc = initFunc;
    }

    public T Create()
    {
        return _initFunc();
    }
}

public static class ServiceCollectionExtensions
{
    public static void AddFactory<TService, TImplementation>(this IServiceCollection services) 
    where TService : class
    where TImplementation : class, TService
    {
        services.AddTransient<TService, TImplementation>();
        services.AddSingleton<Func<TService>>(x => () => x.GetService<TService>());
        services.AddSingleton<IFactory<TService>, Factory<TService>>();
    }
}
```

https://github.com/Microsoft/AspNetCoreInjection.TypedFactories

https://espressocoder.com/2018/10/08/injecting-a-factory-service-in-asp-net-core/

```csharp
public class DefaultFooFactory: IFooFactory{
  public IFoo create(){return new DefaultFoo();}
}
```

https://gist.github.com/bbarry/ae9ac27e56306005ff2285a6d4c4344e

Your gist would simpler (and therefore better) if you are not required to separate your interface (TService) from an implementation (TImplementation). Where your gist would be very appropriate is when you aim to use the factory to generate a class that contains not much implementation code (for example a DAO).

https://github.com/jrob5756/PatternsOfToday/tree/master/Factory

https://gist.github.com/bbarry/ae9ac27e56306005ff2285a6d4c4344e

https://duckduckgo.com/?q=using+generics+c%23&t=chromentp&atb=v211-6__&ia=web

https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/