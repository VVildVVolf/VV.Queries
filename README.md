# VV.Queries

More usecases you can find in /Usecases/Program.cs

## Registration of dependencies

* If all your context have default constructors, you can use basic registration:
```
    serviceCollection.AddQueries();
```

* Also you can define default context
```
    serviceCollection.AddQueries<MyContext>();
```

* It is possible to use a context with a complex constructor (and mark it as default)
```
    serviceCollection.AddQueries<CustomContext>();
    s.AddContextFactoryForQueries(() => new CustomContext(param1, param2));
```

## Readonly use

After registration all dependencies IReadonlyQueryRunner<ContextType> (or IReadonlyQueryRunner if you have defined default context) instance will be available from DI. You can use its RunAsync method to get an IConnection instance (context wrapper). IConnection has Entities<T>() method (context.Set<T>() analogue).
```
    IReadonlyQueryRunner runner = // from DI
    runner.RunAsync( async connection => {
        //you can use directly
        Console.WriteLine(conection.Entities<Item>().ToList());

        //or pass to a provider
        _provider.GetActualOnlyItems(connection);
    });
```

**WARNING:** outside RunAsync call the connection will be closed (context.Dispose() will be called).

## Write use

To make writes you can use ICommitableQueryRunner<ContextType> (or ICommitableQueryRunner if you have defined default context)
```
    ICommitableQueryRunner commitableRunner = // from DI
    commitableRunner.RunAsync( async (connection, commiter) => {
        //you can use directly
        conection.Entities<Item>().Add(new Item);
        await commiter.CommitAsync();

        //or pass to a provider
        _provider.DoSomething(connection);
        await commiter.CommitAsync();
    });
```

Also you can use boolean-function to not bother about commiting
```
    ICommitableQueryRunner commitableRunner = // from DI
    commitableRunner.RunAsync( async (connection) => {
        //you can use directly
        conection.Entities<Item>().Add(new Item());

        //or pass to a provider
        _provider.DoSomething(connection);

        return true; //true means commit. If it returns false - context.SaveChanges will not be called.
    });
```

## Connection without query runners

Sometimes we cannot put all logic to a lambda (for example when we use an OData controller). In this case we can use IUnsafeConnectionFactory<MyContext> (or IUnsafeConnectionFactory if the default context type is defined).
**Note:** Do not forget to call Dispose in this case
```
    var unsafeConnectionFactory = // from DI
    using(var connection = unsafeConnectionFactory.NewConnection) {
        // do queries
    }
```