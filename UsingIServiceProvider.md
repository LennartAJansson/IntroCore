## Using IServiceProvider
If you would like to use dependency injection from a traditional Console application you can do so by adding an IServiceCollection and build an IServiceProvider from that.  

The IServiceCollection is where you do all the registration of interfaces, implementations, scopes etc.  

Once you have built the IServiceProvider you create the initial object by using the serviceproviders GetService method.  

All the other interfaces/classes that are injected down in the hierarchy will automatically be resolved by the serviceprovider if they are registered.  

This sample use the IConfiguration and injects that into the registered classes.  

Check the references that are made in this project and also inspect the code.  

Run the application and you will see output read from the settings files.  