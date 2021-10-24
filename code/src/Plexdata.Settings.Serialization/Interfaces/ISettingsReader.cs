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

using System;
using System.IO;

namespace Plexdata.Utilities.Settings
{
    /// <summary>
    /// An interface to read data from a settings file.
    /// </summary>
    /// <remarks>
    /// This interface defines all needed functionality to 
    /// read data from a settings file.
    /// </remarks>
    /// <typeparam name="TSettings">
    /// The custom type to be deserialized.
    /// </typeparam>
    public interface ISettingsReader<TSettings>
    {
        /// <summary>
        /// Tries reading data from a settings file using default options.
        /// </summary>
        /// <remarks>
        /// The name of the settings file is fetched from default options.
        /// </remarks>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful.
        /// </param>
        /// <returns>
        /// True if reading settings data was successful and value of 
        /// <paramref name="settings"/> is not null, otherwise false.
        /// </returns>
        Boolean TryRead(out TSettings settings);

        /// <summary>
        /// Tries reading data from a settings file using provided options.
        /// </summary>
        /// <remarks>
        /// The name of the settings file is fetched from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful.
        /// </param>
        /// <returns>
        /// True if reading settings data was successful and value of 
        /// <paramref name="settings"/> is not null, otherwise false.
        /// </returns>
        Boolean TryRead(ISettingsOptions options, out TSettings settings);

        /// <summary>
        /// Tries reading data from a settings file referenced by <paramref name="filename"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="ISettingsReader{TSettings}.TryRead(out TSettings)"/> 
        /// this method takes provided filename as is. All other information are taken from 
        /// default options.
        /// </remarks>
        /// <param name="filename">
        /// The fully qualified path of the settings file to read.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful.
        /// </param>
        /// <returns>
        /// True if reading settings data was successful and value of 
        /// <paramref name="settings"/> is not null, otherwise false.
        /// </returns>
        Boolean TryRead(String filename, out TSettings settings);

        /// <summary>
        /// Tries reading data from a settings file referenced by <paramref name="filename"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="ISettingsReader{TSettings}.TryRead(ISettingsOptions, out TSettings)"/> 
        /// this method takes provided filename as is. All other information are taken from 
        /// provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="filename">
        /// The fully qualified path of the settings file to read.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful.
        /// </param>
        /// <returns>
        /// True if reading settings data was successful and value of 
        /// <paramref name="settings"/> is not null, otherwise false.
        /// </returns>
        Boolean TryRead(ISettingsOptions options, String filename, out TSettings settings);

        /// <summary>
        /// Tries reading data from a settings file referenced by <paramref name="reader"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// The instance of <see cref="StreamReader"/> should contain readable settings 
        /// data. All other information are taken from default options.
        /// </remarks>
        /// <param name="reader">
        /// An instance of <see cref="StreamReader"/> to read settings data from.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful.
        /// </param>
        /// <returns>
        /// True if reading settings data was successful and value of 
        /// <paramref name="settings"/> is not null, otherwise false.
        /// </returns>
        Boolean TryRead(StreamReader reader, out TSettings settings);

        /// <summary>
        /// Tries reading data from a settings file referenced by <paramref name="reader"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// The instance of <see cref="StreamReader"/> should contain readable settings 
        /// data. All other information are taken from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="reader">
        /// An instance of <see cref="StreamReader"/> to read settings data from.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful.
        /// </param>
        /// <returns>
        /// True if reading settings data was successful and value of 
        /// <paramref name="settings"/> is not null, otherwise false.
        /// </returns>
        Boolean TryRead(ISettingsOptions options, StreamReader reader, out TSettings settings);

        /// <summary>
        /// Reads data from a settings file using default options.
        /// </summary>
        /// <remarks>
        /// The name of the settings file is fetched from default options.
        /// </remarks>
        /// <returns>
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful. An exception is thrown in any other case.
        /// </returns>
        TSettings Read();

        /// <summary>
        /// Reads data from a settings file using provided options.
        /// </summary>
        /// <remarks>
        /// The name of the settings file is fetched from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful. An exception is thrown in any other case.
        /// </returns>
        TSettings Read(ISettingsOptions options);

        /// <summary>
        /// Reads data from a settings file referenced by <paramref name="filename"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="ISettingsReader{TSettings}.Read()"/> this method 
        /// takes provided filename as is. All other information are taken from default 
        /// options.
        /// </remarks>
        /// <param name="filename">
        /// The fully qualified path of the settings file to read.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful. An exception is thrown in any other case.
        /// </returns>
        TSettings Read(String filename);

        /// <summary>
        /// Reads data from a settings file referenced by <paramref name="filename"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="ISettingsReader{TSettings}.Read(ISettingsOptions)"/> 
        /// this method takes provided filename as is. All other information are taken from 
        /// provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="filename">
        /// The fully qualified path of the settings file to read.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful. An exception is thrown in any other case.
        /// </returns>
        TSettings Read(ISettingsOptions options, String filename);

        /// <summary>
        /// Reads data from a settings file referenced by <paramref name="reader"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// The instance of <see cref="StreamReader"/> should contain readable settings 
        /// data. All other information are taken from default options.
        /// </remarks>
        /// <param name="reader">
        /// An instance of <see cref="StreamReader"/> to read settings data from.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful. An exception is thrown in any other case.
        /// </returns>
        TSettings Read(StreamReader reader);

        /// <summary>
        /// Reads data from a settings file referenced by <paramref name="reader"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// The instance of <see cref="StreamReader"/> should contain readable settings 
        /// data. All other information are taken from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="reader">
        /// An instance of <see cref="StreamReader"/> to read settings data from.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TSettings"/> if reading file 
        /// data was successful. An exception is thrown in any other case.
        /// </returns>
        TSettings Read(ISettingsOptions options, StreamReader reader);
    }
}
