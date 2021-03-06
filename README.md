# VV.Queries

The simple to use light-weight open-source tool will help you to manage lifecycle of the Entity Framework Context. 
More usecases you can find in [/Usecases/Program.cs](https://github.com/VVildVVolf/VV.Queries/blob/master/Usecases/Program.cs)

## Readonly use

When all dependencies have been registered, the `IReadonlyQueryRunner<ContextType>` (or `IReadonlyQueryRunner` if you have defined default context) instance will be available from DI. You will be able to use its RunAsync method to get an IConnection instance (context wrapper). IConnection has `Entities<T>()` method (`context.Set<T>()` analogue).
```cs
    IReadonlyQueryRunner runner = // from DI
    runner.RunAsync( async connection => {
        //you can use directly
        Console.WriteLine(conection.Entities<Item>().ToList());

        //or pass to a provider
        _provider.GetActualOnlyItems(connection);
    });
```

Also if you want to wrap getting data from IRedonlyQueryRunner, you can use an extention:
```cs
    IReadonlyQueryRunner runner = // from DI
    var items = runner.RunAsync( async connection => {
        return conection.Entities<Item>().ToList();
    });
```


**WARNING:** outside RunAsync call the connection will be closed (`context.Dispose()` will have been already called).

## Write use

To make writes into IConnection you can use `ICommitableQueryRunner<ContextType>` (or `ICommitableQueryRunner` if you have defined default context)
```cs
    ICommitableQueryRunner commitableRunner = // from DI
    commitableRunner.RunAsync( async (connection, commiter) => {
        //you can use directly
        conection.Entities<Item>().Add(new Item());
        await commiter.CommitAsync();

        //or pass to a provider
        _provider.DoSomething(connection);
        await commiter.CommitAsync();
    });
```

Here you also can use an extention - boolean-function to not bother about commiting
```cs
    ICommitableQueryRunner commitableRunner = // from DI
    commitableRunner.RunAsync( async (connection) => {
        //you can use directly
        conection.Entities<Item>().Add(new Item());

        //or pass to a provider
        _provider.DoSomething(connection);

        return true; //true means commit. If it returns false - context.SaveChanges() will not be called.
    });
```

## Installing 

`dotnet add package VV.Queries`

## Registration of dependencies

* If all your context have default constructors, you can use basic registration:
```cs
    serviceCollection.AddQueries();
```

* Also you can define default context type
```cs
    serviceCollection.AddQueries<DefaultContext>();
```

* It is possible to use a context with a complex constructor (and mark it as default)
```cs
    serviceCollection.AddQueries<CustomContext>();
    s.AddContextFactoryForQueries(() => new CustomContext(param1, param2));
```

## Connection without query runners

Sometimes we cannot put all logic to a lambda (for example when we use an OData controller). In this case we can use `IUnsafeConnectionFactory<MyContext>` (or IUnsafeConnectionFactory if the default context type is defined).
**Note:** Do not forget to call Dispose in this case
```cs
    var unsafeConnectionFactory = // from DI
    using(var connection = unsafeConnectionFactory.NewConnection) {
        // do queries
    }
```