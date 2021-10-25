<p align="center">
  <a href="https://github.com/akesseler/Plexdata.Settings.Serialization/blob/master/LICENSE.md" alt="license">
    <img src="https://img.shields.io/github/license/akesseler/Plexdata.Settings.Serialization.svg" />
  </a>
  <a href="https://github.com/akesseler/Plexdata.Settings.Serialization/releases/latest" alt="latest">
    <img src="https://img.shields.io/github/release/akesseler/Plexdata.Settings.Serialization.svg" />
  </a>
  <a href="https://github.com/akesseler/Plexdata.Settings.Serialization/archive/master.zip" alt="master">
    <img src="https://img.shields.io/github/languages/code-size/akesseler/Plexdata.Settings.Serialization.svg" />
  </a>
  <a href="https://akesseler.github.io/Plexdata.Settings.Serialization" alt="docs">
    <img src="https://img.shields.io/badge/docs-guide-orange.svg" />
  </a>
  <a href="https://github.com/akesseler/Plexdata.Settings.Serialization/wiki" alt="wiki">
    <img src="https://img.shields.io/badge/wiki-API-orange.svg" />
  </a>
</p>

## Plexdata Settings Serialization

The _Plexdata Settings Serialization_ represents an alternative to the standard settings serialization mechanism.

Main feature of this library is that users are able to define the path as well as the extension of their settings file.

As extension to the standard settings handling, this library allows to put the settings into the directory of the executable. 
This is very interesting for so called _Portable Apps_, which usually manage all their data within one and the same directory.

Another feature of this library is the possibility to control the format of the settings file. At the moment, reading and 
writing of settings files in JSON format as well as in XML format is supported. Other formats like for example _old-school_ 
INI files may follow in a later version.

### Examples

#### Simple Usage

The example below shows how to use the functionality of the _Plexdata Settings Serialization_ library. Keep in mind, this 
example assumes a user defined data model named `ProgramSettings` with one property named `Title`.

```
var options = SettingsFactory.Create<ISettingsOptions>(
    SettingsPattern.JsonPattern, StorageLocation.ExecutableFolder, "program.settings");

Console.WriteLine(options.GetFullPath());

var reader = SettingsFactory.Create<ISettingsReader<ProgramSettings>>();

if (!reader.TryRead(options, out ProgramSettings settings))
{
    settings = new ProgramSettings() { Title = "My Title" };
}

Console.WriteLine(settings.Title);

var writer = SettingsFactory.Create<ISettingsWriter<ProgramSettings>>();

writer.TryWrite(options, settings);
```

As shown in example above, _Plexdata Settings Serialization_ firstly tries to read a settings file of type JSON, with an 
extension of `program.settings` and within the executable´s directory.

If this was not possible (because of that file does not yet exist) an instance of the user defined data model is created 
manually.

After that it is tried to save that data model into the settings file referenced by created `options`.

If the code above is executed twice, the previously created settings file will be loaded from the executable´s directory.

#### Dependency Injection

In this section it is wanted to show how _Plexdata Settings Serialization_ can be used together with _Dependency Injection_.
The example below uses Unity as dependency container as well as as a typical container registration extension method. The 
scenario discussed in this example shows the registration of a settings file that only includes special project settings.
Furthermore, the model `ProjectSettings` is assumed. This model is indeed not a part of the _Plexdata Settings Serialization_ 
library.

```
public static IUnityContainer PerformRegistration(this IUnityContainer container)
{
    if (container == null)
    {
        return container;
    }

    container.RegisterFactory<ISettingsOptions>("ProjectSettingsOptions",
        x => SettingsFactory.Create<ISettingsOptions>(
            SettingsPattern.JsonPattern, StorageLocation.ExecutableFolder, "project.data"
        ));

    container.RegisterFactory<ISettingsReader<ProjectSettings>>("ProjectSettingsReader",
        x => SettingsFactory.Create<ISettingsReader<ProjectSettings>>());

    container.RegisterFactory<ISettingsWriter<ProjectSettings>>("ProjectSettingsWriter",
        x => SettingsFactory.Create<ISettingsWriter<ProjectSettings>>());

    return container;
}
```

As show above first of all the factory for `ISettingsOptions` is registered with a specific name called `"ProjectSettingsOptions"` 
is registered. The factory method `SettingsFactory.Create()` contains a list of parameters that are used to initialize each 
instance of `ISettingsOptions`. With a closer look at these parameters the underlying settings file is of type JSON, and is 
located inside the directory of the executing assembly, and uses the extension `"project.data"`.

As next the factories for `ISettingsReader` and `ISettingsWriter` are registered. Both registrations using a custom defined 
data model maned `ProjectSettings`. Additionally, these factories are registered under the names `"ProjectSettingsReader"` 
and `"ProjectSettingsWriter"`. Finally, both registrations do not take any additional parameter.

With the above dependency container registration in mind, it becomes possible to inject these interface into other classes.

Here an example of constructor injection.

```
public MyProjectSettingsManager(
    [Dependency("ProjectSettingsOptions")] ISettingsOptions options,
    [Dependency("ProjectSettingsReader")] ISettingsReader<ProjectSettings> reader,
    [Dependency("ProjectSettingsWriter")] ISettingsWriter<ProjectSettings> writer)
{
    this.options = options ?? throw new ArgumentNullException(nameof(options));
    this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
    this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
}
```

As next an example of property injection, that works pretty much alike.

```
[Dependency("ProjectSettingsOptions")]
public ISettingsOptions Options { get; set; }

[Dependency("ProjectSettingsReader")] 
public ISettingsReader<ProjectSettings> Reader { get; set; }

[Dependency("ProjectSettingsWriter")] 
public ISettingsWriter<ProjectSettings> Writer { get; set; }
```

### Framework

Current target framework of this library is the _.NET Standard v2.0_.

### Licensing

The software has been published under the terms of _MIT License_.

### Downloads

The latest release can be obtained from [https://github.com/akesseler/Plexdata.Settings.Serialization/releases/latest](https://github.com/akesseler/Plexdata.Settings.Serialization/releases/latest).

The main branch can be downloaded as ZIP from [https://github.com/akesseler/Plexdata.Settings.Serialization/archive/master.zip](https://github.com/akesseler/Plexdata.Settings.Serialization/archive/master.zip).

### Documentation

The documentation with an overview, an introduction as well as examples is available under [https://akesseler.github.io/Plexdata.Settings.Serialization](https://akesseler.github.io/Plexdata.Settings.Serialization/).

The full API documentation is available as Wiki and can be read under [https://github.com/akesseler/Plexdata.Settings.Serialization/wiki](https://github.com/akesseler/Plexdata.Settings.Serialization/wiki).

