# Proteus.Utility.Configuration #

## Overview ##
`Proteus.Utility.Configuration` is a library that can be used to address some of the challenges of managing settings and configuration values as code is deployed to different environments.  Specifically, this library:

 * provides a functional, drop-in replacement for the `System.Configuration.ConfigurationManager` class from the .NET BCL
 * supports reading configuration values from _multiple_ arbitrary sources
 * includes built-in support for `app.config`/`web.config` file sources, `local.settings.json` file sources, and environment variable sources
 * supports overriding existing configuration values from one source with values from another source at run-time in a declarative manner
 * can be extended to support user-added configuration sources via a simple adapter/plug-in pattern _without_ the user having to implement custom interfaces

 ## Functional Replacement for ConfigurationManager ##
 The functionality of the `Proteus.Utility.Configuration` library revolves around the single static class, `ExtensibleSourceConfigurationManager`.  By default, this class is a functional replacement for anywhere in your code that you would typically resort to using the .NET Base Class Library `System.Configuration.ConfigurationManager` type.

 Given the following sample `app.config` file snippet...
 ```xml
<appSettings>
    <add key="theUrl" value="http://someurl.com"/>
</appSettings>
<connectionStrings>
    <add name="theConnection" connectionString="Data Source=.;Initial Catalog=MyDatabase;Integrated Security=True" providerName="System.Data.SqlServer"/>
</connectionStrings>
 ```
...reading these values via the `System.Configuration.ConfigurationManager` class would typically be accomplished as follows:
```csharp
var url = ConfigurationManager.AppSettings["theUrl"];
var connString = ConfigurationManager.ConnectionStrings["theConnection"];
```
Reading the same value via the `ExtensibleSourceConfigurationManager` is _nearly_ identical and requires only that the indexer accessor operation `[key]` is replaced with equivalent method calls `(key)` as shown in the following code: 
```csharp
var url = ExtensibleSourceConfigurationManager.AppSettings("theUrl");
var connString = ExtensibleSourceConfigurationManager.ConnectionStrings("theConnection");
```
This makes `ExtensibleSourceConfigurationManager` a drop-in replacement for any situation where you would typically use `ConfigurationManager`, meaning that integrating it into existing code is very straightforward.

## Overriding Configuration Values ##
 Consider the common scenario where during e.g., development on their local system, a developer needs a _different_ value than is stored in e.g., the `app.config` file above.  The .NET BCL provides a bewildering array of strategies for addressing this situation (`.config` file transforms, etc.), but most of these are typically overly-complicated and/or fail to address the 'different developer' scenario, focusing instead on the comparatively simplistic e.g.,'debug' vs. 'release' as the active discriminator between configuration values.  Further, as you begin to introduce ever more configuration overrides, these solutions all tend to become increasingly unwieldy.

By default, `ExtensibleSourceConfigurationManager` is itself configured only to read values from the `app.config`/`web.config` file, just as `ConfigurationManager` does.  There is of course little value in using _only_ the same functionality already provided by `ConfigurationManager`.  The real value of `ExtensibleSourceConfigurationManager` is realized when it is configured to read values from _multiple_ sources.  In this configuration, the values in one source can _override_ the values in another source.

By way of a simple example, let's return to our sample `app.config` file:
```xml
<appSettings>
    <add key="theUrl" value="http://someurl.com"/>
</appSettings>
<connectionStrings>
    <add name="theConnection" connectionString="Data Source=.;Initial Catalog=MyDatabase;Integrated Security=True" providerName="System.Data.SqlServer"/>
</connectionStrings>
 ```
 By registering the `EnvironmentVariableReader` with the `ExtensibleSourceConfigurationManager`, the developer can use Environment Variables on their local system to override values in the `app.config` file.
 
 First, register the `EnvironmentVariableReader`:
 ```csharp
 ExtensibleSourceConfigurationManager.AppSettingReaders.Add(EnvironmentVariableReader.GetAppSetting);
 ```
 Second, use any of multiple mechanisms supported by your Operating System to set an environment variable with the same name as the key in `app.config`:
 ```
 set theUrl="http://someotherurl.com"
 ```
 Once this is complete, the value of the environment variable will  _override_ the value that is present in the `app.config` when retrieved as in the following code:
 ```csharp
 var url = ExtensibleSourceConfigurationManager.AppSettings("theUrl");
 //value of 'url' variable is now "http://someotherurl.com" instead of "http://someurl.com"
 ```
 This behavior of overriding values is possible because `ExtensibleSourceConfigurationManager` respects the _order_ of its configured readers when retrieving values from its configured sources.  ___The value will be returned from the first source found to contain the value even if the value is also present in subsequently inspected sources___.

 ## Multiple Overrides ##
 Multiple readers can be registered with `ExtensibleSourceConfigurationManager` to support multiple or _cascading_ overrides of values.  Included in `Proteus.Utility.Configuration` are readers for the following configuration sources:

### The `app.config`/`web.config` file ###
This is the standard .NET configuration file supported by `System.Configuration.ConfigurationManager`.

### The `local.settings.json` file ###
A valid `.json` file that contains simple key-value pairs as in e.g.,
```javascript
{
    "theUrl": "http://someothervalueagain.com",
    "theConnection": "connectionString=\"Data Source=.;Initial Catalog=SomeOtherDatabase;Integrated Security=True\" providerName=\"System.Data.SqlServer\""
}
```
This file must be named `local.settings.json` and must be located in the start directory of the AppDomain.   _Typically_ in .NET projects, this can be most easily accomplished by setting this file's `Build Action` property to `Copy Always` in Visual Studio.

_Note: common `.gitignore` file contents often explicitly exclude `local.settings.json` from being managed by Git by default, precisely to avoid it being committed to the repository and to support the concept of each developer maintaining their own isolated copy of the file.  In order to avoid each developer's own `local.settings.json` colliding with their peers, you may want to consider adding `local.settings.json` to your `.gitignore` if you are pursuing this approach to overriding configuration settings and `local.settings.json` is not already excluded from Git change tracking._

### Environment Variables ###
The name of the environment variable must exactly match the name of the setting being retrieved.

There are a number of different ways to set Environment Variables in Windows.  See [this post on StackOverflow](https://stackoverflow.com/questions/5898131/set-a-persistent-environment-variable-from-cmd-exe) for just some of the possibilities (and potential pitfalls to watch out for when doing so).

## Recommended Reader Registration Order ##
 As mentioned, the default configuration for `ExtensibleSourceConfigurationManager` reads values _only_ from the `app.config`/`web.config` file to match the behavior of `ConfigurationManager` in the .NET BCL.  However the recommended usage pattern for `ExtensibleSourceConfigurationManager` is to register the provided configuration readers in the following order:

| Registration Order | Class | Configuration Source | Notes |
|-|-|-|-|-|
| 1. | `AppConfigReader` | `app.config`/`web.config` | automatically registered by default|
| 2. | `LocalSettingsJsonReader` | `local.settings.json` | registered optionally |
| 3. | `EnvironmentVariableReader` | environment variables | registered optionally |

Following this registration sequence, values in `local.settings.json` will override values in `app.config`/`web.config` and values in environment variables will in turn override values in both `local.settings.json` _and_ `app.config`/`web.config`.

 In this way, 'core' settings can be codified in the `app.config`/`web.config` file.  Values that need to be _consistently_ overridden can then be placed into the `local.settings.json` file.  Any values that need to be _periodically_ or _temporarily_ overridden can then be set as environment variables. 

 This recommended registration order can be seen in the following code:
 ```csharp
 //register readers for AppSettings values in order
 ExtensibleSourceConfigurationManager.AppSettingReaders.Add(LocalSettingsJsonReader.GetAppSetting);
ExtensibleSourceConfigurationManager.AppSettingReaders.Add(EnvironmentVariableReader.GetAppSetting);

//register readers for ConnectionString values in order
ExtensibleSourceConfigurationManager.ConnectionStringReaders.Add(LocalSettingsJsonReader.GetConnectionString);
ExtensibleSourceConfigurationManager.ConnectionStringReaders.Add(EnvironmentVariableReader.GetConnectionString);
 ```

## AppSetting String Values as Specific Types ##
One significant shortcoming of the `ConfigurationManager` class in the .NET BCL is that all values retrieved via e.g., `ConfigurationManager.AppSettings["key"]` are returned as type `string`, often requiring the consumer to cast the value to another type after retrieving the string value.  `ExtensibleSourceConfigurationManager` offers an override to its `.AppSettings(...)` method that can perform this casting internally to return non-`string` types as shown in the following scenario.

```xml
<!-- sample app.config snippet -->
<appSettings>
    <add key="key1" value="http://someurl.com"/>
    <add key="key2" value="true"/>
    <add key="key3" value="30"/>
</appSettings>
```
```csharp

//using .NET ConfigurationManager from BCL

var value1 = ConfigurationManager.AppSettings["key1"];
//'value1' is type string, value="http://someurl.com"

var value2 = ConfigurationManager.AppSettings["key2"];
//'value2' is type string, value="true"

var value3 = ConfigurationManager.AppSettings["key3"];
//'value3' is type string, value="30"

//using Proteus.Utility.Configuration library
var betterValue1 = ExtensibleSourceConfigurationManager.AppSettings<Uri>("key1");
//'betterValue1' is type Uri

var betterValue2 = ExtensibleSourceConfigurationManager.AppSettings<bool>("key2");
//'betterValue2' is type bool, value=true

var betterValue3 = ExtensibleSourceConfigurationManager.AppSettings<int>("key3");
//'betterValue3' is type int, value=30
```
Note that this attempted cast will throw a `ConfigurationErrorsException` if the `string` value retrieved cannot be converted into the requested `Type`.

## Custom Configuration Source Readers ##
In any case where the built-in readers (`app.config`/`web.config`, `local.settings.json`, environment variables) offer insufficient support for configuration sources, its simple to extend the behavior of `ExtensibleSourceConfigurationManager` by developing additional configuration source reader(s) and registering them with `ExtensibleSourceConfigurationManager` at runtime.

### AppSettings Reader Signature ###
All readers for app settings must register a method with the following signature, a Func that takes a `string` parameter arg (the 'key') and returns a `string` result (the 'value'):
```csharp
Func<string, string>
```
### ConnectionString Reader Signature ###
All readers for connection strings must register a method with the following signature, a Func that takes a `string` parameter arg (the 'key') and returns a `ConnectionStringSettings` result (the 'value'):
```csharp
Func<string, ConnectionStringSettings>
```
It is _not_ required that readers implement any specific .NET Interface.  _Any_ method(s) that satisfy the above signature(s) may be registered with the `ExtensibleSourceConfigurationManager` at runtime.  However, to make it as simple as possible to implement custom readers, `Proteus.Utility.Configuration` provides a simple interface, `IConfigurationReader`, that when implemented will ensure compliance with the requirements for delegate signatures identified above.