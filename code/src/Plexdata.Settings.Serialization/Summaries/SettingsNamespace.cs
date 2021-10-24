/*
 * MIT License
 * 
 * Copyright (c) 2021 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Plexdata.Utilities.Settings
{
    /// <summary>
    /// The <see cref="Plexdata.Utilities.Settings"/> namespace contains all public classes and enumerations 
    /// supported by the <em>Plexdata Settings Serialization</em>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// All interfaces, classes and enumerations in this namespace represent the public interface of the 
    /// <em>Plexdata Settings Serialization</em>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// The example below shows how to use the functionality of the <em>Plexdata Settings Serialization</em> 
    /// library. Keep in mind, this example assumes a user defined data model named <c>ProgramSettings</c> 
    /// with one property 
    /// named <c>Title</c>.
    /// </para>
    /// <code>
    /// var options = SettingsFactory.Create&lt;ISettingsOptions&gt;(
    ///     SettingsPattern.JsonPattern, StorageLocation.ExecutableFolder, "program.settings");
    /// 
    /// Console.WriteLine(options.GetFullPath());
    /// 
    /// var reader = SettingsFactory.Create&lt;ISettingsReader&lt;ProgramSettings&gt;&gt;();
    /// 
    /// if (!reader.TryRead(options, out ProgramSettings settings))
    /// {
    ///     settings = new ProgramSettings() { Title = "My Title" };
    /// }
    /// 
    /// Console.WriteLine(settings.Title);
    /// 
    /// var writer = SettingsFactory.Create&lt;ISettingsWriter&lt;ProgramSettings&gt;&gt;();
    /// 
    /// writer.TryWrite(options, settings);
    /// </code>
    /// <para>
    /// As shown in example above, <em>Plexdata Settings Serialization</em> firstly tries to read a settings file 
    /// of type JSON, with an extension of <c>program.settings</c> and within the executable's directory.
    /// </para>
    /// <para>
    /// If this was not possible (because of that file does not yet exist) an instance of the user defined data model 
    /// is created manually.
    /// </para>
    /// <para>
    /// After that it is tried to save that data model into the settings file referenced by created <c>options</c>.
    /// </para>
    /// <para>
    /// If the code above is executed twice, the previously created settings file will be loaded from the executable's 
    /// directory.
    /// </para>
    /// </example>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc { }
}
