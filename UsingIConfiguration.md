## Using IConfiguration
If you would like to access configuration from a traditional Console application you can do so by using a ConfigurationBuilder to generate an IConfigurationRoot, which is the same as an IConfiguration.   

With this builder you can choose what kind of configuration you would like to include, in this sample we can see how to read appsettings files, environment variables and commandline but we could also read User Secrets and other configuration sources.  

Check the references that are made in this project and also inspect the code.  

Run the application and you will see output read from the settings files.  