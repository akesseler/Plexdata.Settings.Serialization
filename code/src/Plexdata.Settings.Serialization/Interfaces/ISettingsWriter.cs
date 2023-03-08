/*
 * MIT License
 * 
 * Copyright (c) 2023 plexdata.de
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
    /// An interface to write data into a settings file.
    /// </summary>
    /// <remarks>
    /// This interface defines all needed functionality to 
    /// write data into a settings file.
    /// </remarks>
    /// <typeparam name="TSettings">
    /// The custom type to be serialized.
    /// </typeparam>
    public interface ISettingsWriter<TSettings>
    {
        /// <summary>
        /// Tries writing data into a settings file using default options.
        /// </summary>
        /// <remarks>
        /// The name of the settings file is fetched from default options.
        /// </remarks>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written 
        /// into a settings file.
        /// </param>
        /// <returns>
        /// True, if writing settings data was successful and false otherwise.
        /// </returns>
        Boolean TryWrite(TSettings settings);

        /// <summary>
        /// Tries writing data into a settings file using provided options.
        /// </summary>
        /// <remarks>
        /// The name of the settings file is fetched from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        /// <returns>
        /// True, if writing settings data was successful and false otherwise.
        /// </returns>
        Boolean TryWrite(ISettingsOptions options, TSettings settings);

        /// <summary>
        /// Tries writing data into a settings file referenced by <paramref name="filename"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="ISettingsWriter{TSettings}.TryWrite(TSettings)"/> this method takes 
        /// provided filename as is. All other information are taken from default options.
        /// </remarks>
        /// <param name="filename">
        /// The fully qualified path of the settings file to write.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        /// <returns>
        /// True, if writing settings data was successful and false otherwise.
        /// </returns>
        Boolean TryWrite(String filename, TSettings settings);

        /// <summary>
        /// Tries writing data into a settings file referenced by <paramref name="filename"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="ISettingsWriter{TSettings}.TryWrite(ISettingsOptions, TSettings)"/> 
        /// this method takes provided filename as is. All other information are taken from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="filename">
        /// The fully qualified path of the settings file to write.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        /// <returns>
        /// True, if writing settings data was successful and false otherwise.
        /// </returns>
        Boolean TryWrite(ISettingsOptions options, String filename, TSettings settings);

        /// <summary>
        /// Tries writing data into a settings file referenced by <paramref name="writer"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// All necessary information to write settings data are taken from default options.
        /// </remarks>
        /// <param name="writer">
        /// An instance of <see cref="StreamWriter"/> to write settings data into.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        /// <returns>
        /// True, if writing settings data was successful and false otherwise.
        /// </returns>
        Boolean TryWrite(StreamWriter writer, TSettings settings);

        /// <summary>
        /// Tries writing data into a settings file referenced by <paramref name="writer"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// All necessary information to write settings data are taken from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="writer">
        /// An instance of <see cref="StreamWriter"/> to write settings data into.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        /// <returns>
        /// True, if writing settings data was successful and false otherwise.
        /// </returns>
        Boolean TryWrite(ISettingsOptions options, StreamWriter writer, TSettings settings);

        /// <summary>
        /// Writes data into a settings file using default options.
        /// </summary>
        /// <remarks>
        /// The name of the settings file is fetched from default options.
        /// </remarks>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        void Write(TSettings settings);

        /// <summary>
        /// Writes data into a settings file using provided options.
        /// </summary>
        /// <remarks>
        /// The name of the settings file is fetched from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        void Write(ISettingsOptions options, TSettings settings);

        /// <summary>
        /// Writes data into a settings file referenced by <paramref name="filename"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="ISettingsWriter{TSettings}.Write(TSettings)"/> this method 
        /// takes provided filename as is. All other information are taken from default 
        /// options.
        /// </remarks>
        /// <param name="filename">
        /// The fully qualified path of the settings file to write.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        void Write(String filename, TSettings settings);

        /// <summary>
        /// Writes data into a settings file referenced by <paramref name="filename"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="ISettingsWriter{TSettings}.Write(String, TSettings)"/> this method 
        /// takes provided filename as is. All other information are taken from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="filename">
        /// The fully qualified path of the settings file to write.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        void Write(ISettingsOptions options, String filename, TSettings settings);

        /// <summary>
        /// Writes data into a settings file referenced by <paramref name="writer"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// All necessary information to write settings data are taken from default options.
        /// </remarks>
        /// <param name="writer">
        /// An instance of <see cref="StreamWriter"/> to write settings data into.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        void Write(StreamWriter writer, TSettings settings);

        /// <summary>
        /// Writes data into a settings file referenced by <paramref name="writer"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// All necessary information to write settings data are taken from provided options.
        /// </remarks>
        /// <param name="options">
        /// The options to be used.
        /// </param>
        /// <param name="writer">
        /// An instance of <see cref="StreamWriter"/> to write settings data into.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        void Write(ISettingsOptions options, StreamWriter writer, TSettings settings);
    }
}
