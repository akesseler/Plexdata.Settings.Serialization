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
using System.ComponentModel;

namespace Plexdata.Utilities.Settings
{
    /// <summary>
    /// An interface representing all settings to be used with settings files.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface represents all possible settings to be used together with 
    /// reading and writing settings files.
    /// </para>
    /// <para>
    /// For a construction using the factory it is possible to provide initial values 
    /// in the order of the properties defined in this interface.
    /// </para>
    /// </remarks>
    public interface ISettingsOptions : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets and sets the pattern of the settings file.
        /// </summary>
        /// <remarks>
        /// This property allows to control the type of the settings file. At 
        /// the moment two types are supported, JSON format and XML format.
        /// </remarks>
        /// <value>
        /// The pattern (type) of the settings file.
        /// </value>
        SettingsPattern Pattern { get; set; }

        /// <summary>
        /// Gets and sets the location of the settings file.
        /// </summary>
        /// <remarks>
        /// This property allows to control the settings file location.
        /// </remarks>
        /// <value>
        /// The location of the settings file.
        /// </value>
        StorageLocation Location { get; set; }

        /// <summary>
        /// Gets and sets the settings file extension.
        /// </summary>
        /// <remarks>
        /// This property allows to control the extension of the settings file.
        /// </remarks>
        /// <value>
        /// The settings file extension.
        /// </value>
        String Extension { get; set; }

        /// <summary>
        /// Controls the usage of a version number within the settings file path.
        /// </summary>
        /// <remarks>
        /// Sometimes it might be useful to tag a settings file path by a version 
        /// number. For this purpose this property can be used.
        /// </remarks>
        /// <value>
        /// True to enable the usage of the version number and false to disable it.
        /// </value>
        Boolean IsVersionized { get; set; }

        /// <summary>
        /// Gets the full path of the settings file.
        /// </summary>
        /// <remarks>
        /// This method allows to get the full path of the settings file that will 
        /// be generated from these options instance.
        /// </remarks>
        /// <returns>
        /// The full path of the settings file.
        /// </returns>
        /// <exception cref="Exception">
        /// This method may throw an exception.
        /// </exception>
        String GetFullPath();
    }
}
