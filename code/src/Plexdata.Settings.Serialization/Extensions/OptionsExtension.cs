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

using Plexdata.Utilities.Settings.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Plexdata.Utilities.Settings.Extensions
{
    /// <summary>
    /// This class serves as extension of interface <see cref="ISettingsOptions"/>.
    /// </summary>
    /// <remarks>
    /// This extension class provides methods that extend the functionality of 
    /// interface <see cref="ISettingsOptions"/>.
    /// </remarks>
    internal static class OptionsExtension
    {
        #region Public Methods

        /// <summary>
        /// Determines the storage location.
        /// </summary>
        /// <remarks>
        /// This method tries to determine the fully qualified path of the settings 
        /// file to be used.
        /// </remarks>
        /// <param name="options">
        /// An instance of <see cref="ISettingsOptions"/>.
        /// </param>
        /// <returns>
        /// The fully qualified path of the settings file to be used.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// This exception is thrown in case of parameter <paramref name="options"/> 
        /// is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if property <see cref="ISettingsOptions.Extension"/> of 
        /// parameter <paramref name="options"/> does not provide a valid file extension.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// This exception is thrown in case of the entry assembly could not be determined.
        /// </exception>
        /// <exception cref="NotImplementedException">
        /// This exception is thrown if property <see cref="ISettingsOptions.Location"/> of 
        /// parameter <paramref name="options"/> does not provide a valid storage location.
        /// </exception>
        /// <seealso cref="ISettingsOptions"/>
        /// <seealso cref="StorageLocation"/>
        /// <seealso cref="Assembly.GetEntryAssembly()"/>
        public static String FetchStorageLocation(this ISettingsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), $"The value of {nameof(options)} may not be null.");
            }

            if (String.IsNullOrWhiteSpace(options.Extension))
            {
                throw new ArgumentException("Unable to fetch a filename without any valid extension.");
            }

            Assembly assembly = Assembly.GetEntryAssembly();

            if (assembly == null)
            {
                throw new InvalidOperationException("Unable to determine calling assembly.");
            }

            switch (options.Location)
            {
                case StorageLocation.DefaultLocation:
                case StorageLocation.ExecutableFolder:
                    return options.GetFilePathForExecutableFolder(assembly);

                case StorageLocation.FallbackLocation:
                case StorageLocation.LocalApplicationData:
                    // <drive>\Users\<username>\AppData\Local\<company>\<product>\<filename>.<extension>
                    return options.GetFilePathForSpecialFolder(assembly, Environment.SpecialFolder.LocalApplicationData);

                case StorageLocation.ApplicationData:
                    // <drive>\Users\<username>\AppData\Roaming\<company>\<product>\<filename>.<extension>
                    return options.GetFilePathForSpecialFolder(assembly, Environment.SpecialFolder.ApplicationData);

                case StorageLocation.CommonApplicationData:
                    // <drive>\ProgramData\<company>\<product>\<filename>.<extension>
                    return options.GetFilePathForSpecialFolder(assembly, Environment.SpecialFolder.CommonApplicationData);
            }

            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the settings file path located in the same folder as the executable.
        /// </summary>
        /// <remarks>
        /// This method tries to get the fully qualified settings file path base on 
        /// the path of the entry assembly. In case of that path does not allow writing 
        /// files the resulting settings file name points to the fallback location.
        /// </remarks>
        /// <param name="options">
        /// An instance of <see cref="ISettingsOptions"/>.
        /// </param>
        /// <param name="assembly">
        /// An instance of <see cref="Assembly"/> representing the entry assembly.
        /// </param>
        /// <returns>
        /// The fully qualified path of the settings file to be used.
        /// </returns>
        /// <seealso cref="IsWritingPermitted(String)"/>
        /// <seealso cref="GetFilePathForFallbackLocation(ISettingsOptions, Assembly)"/>
        private static String GetFilePathForExecutableFolder(this ISettingsOptions options, Assembly assembly)
        {
            String path = Path.GetDirectoryName(assembly.Location);

            if (OptionsExtension.IsWritingPermitted(path))
            {
                return Path.Combine(path, Path.ChangeExtension(Path.GetFileName(assembly.Location), options.Extension));
            }

            return OptionsExtension.GetFilePathForFallbackLocation(options, assembly);
        }

        /// <summary>
        /// Gets the fallback settings file path.
        /// </summary>
        /// <remarks>
        /// This method determines the fallback settings file path.
        /// </remarks>
        /// <param name="options">
        /// An instance of <see cref="ISettingsOptions"/>.
        /// </param>
        /// <param name="assembly">
        /// An instance of <see cref="Assembly"/> representing the entry assembly.
        /// </param>
        /// <returns>
        /// The fully qualified path of the settings file to be used.
        /// </returns>
        /// <seealso cref="GetFilePathForExecutableFolder(ISettingsOptions, Assembly)"/>
        /// <seealso cref="GetFilePathForSpecialFolder(ISettingsOptions, Assembly, Environment.SpecialFolder)"/>
        /// <seealso cref="Environment.SpecialFolder.LocalApplicationData"/>
        private static String GetFilePathForFallbackLocation(this ISettingsOptions options, Assembly assembly)
        {
            return options.GetFilePathForSpecialFolder(assembly, Environment.SpecialFolder.LocalApplicationData);
        }

        /// <summary>
        /// Gets the settings file path located in a special folder.
        /// </summary>
        /// <remarks>
        /// This method determines the settings file path located in a special folder.
        /// </remarks>
        /// <param name="options">
        /// An instance of <see cref="ISettingsOptions"/>.
        /// </param>
        /// <param name="assembly">
        /// An instance of <see cref="Assembly"/> representing the entry assembly.
        /// </param>
        /// <param name="special">
        /// The special folder descriptor to be used as base.
        /// </param>
        /// <returns>
        /// The fully qualified path of the settings file to be used.
        /// </returns>
        /// <seealso cref="GetFilePathForExecutableFolder(ISettingsOptions, Assembly)"/>
        /// <seealso cref="GetFilePathForFallbackLocation(ISettingsOptions, Assembly)"/>
        /// <seealso cref="Environment.SpecialFolder.ApplicationData"/>
        /// <seealso cref="Environment.SpecialFolder.LocalApplicationData"/>
        /// <seealso cref="Environment.SpecialFolder.CommonApplicationData"/>
        /// <seealso cref="StorageLocation.ApplicationData"/>
        /// <seealso cref="StorageLocation.LocalApplicationData"/>
        /// <seealso cref="StorageLocation.CommonApplicationData"/>
        /// <seealso cref="AssemblyDetails"/>
        private static String GetFilePathForSpecialFolder(this ISettingsOptions options, Assembly assembly, Environment.SpecialFolder special)
        {
            AssemblyDetails details = AssemblyDetails.Create(assembly);

            String path = Path.Combine(Environment.GetFolderPath(special), details.ToString(options.IsVersionized));

            return Path.Combine(path, Path.ChangeExtension(Path.GetFileName(assembly.Location), options.Extension));
        }

        /// <summary>
        /// Determines if write permission is granted.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method determines if write permission is granted or not.
        /// </para>
        /// <para>
        /// Unfortunately, with this .NET Standard project is was not really possible 
        /// to determine the folder write access by using Windows security. Therefore, 
        /// write permission is checked by trying to create a temporary file and caching 
        /// a possible <see cref="UnauthorizedAccessException"/>. 
        /// </para>
        /// <para>
        /// For sure, this <em>brute force</em> way is pretty ugly but it figures out the wanted 
        /// result, write permission is granted or not.
        /// </para>
        /// </remarks>
        /// <param name="folder">
        /// The folder to check write permission for.
        /// </param>
        /// <returns>
        /// True, if write permission is granted and false if an <see cref="UnauthorizedAccessException"/> 
        /// occurs.
        /// </returns>
        /// <exception cref="Exception">
        /// Any exception (except <see cref="UnauthorizedAccessException"/>) that can occur 
        /// when trying to create and/or delete a file.
        /// </exception>
        /// <seealso cref="File.Create(String)"/>
        /// <seealso cref="File.Delete(String)"/>
        private static Boolean IsWritingPermitted(String folder)
        {
            try
            {
                // Well, this is for sure pretty ugly. But after spending a lot of hours it came
                // up that using NuGet package "System.IO.FileSystem.AccessControl" seems to be
                // impossible within this .NET Standard project. Therefore, the "brute force" way
                // is taken to check for current user's permission.
                //
                // See here for an example how it should work: https://stackoverflow.com/a/61368988

                String filename = Guid.NewGuid().ToString("N");
                String fullpath = Path.Combine(folder, filename);

                FileStream stream = File.Create(fullpath);
                stream.Dispose();

                File.Delete(fullpath);

                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Trace.WriteLine($"{nameof(OptionsExtension)}: Unauthorized access for folder \"{folder}\"");
                return false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"{nameof(OptionsExtension)}: {exception}");
                throw;
            }
        }

        #endregion
    }
}
