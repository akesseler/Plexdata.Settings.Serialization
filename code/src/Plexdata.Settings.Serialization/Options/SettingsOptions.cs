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

using Plexdata.Utilities.Settings.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Plexdata.Utilities.Settings.Options
{
    /// <summary>
    /// Implements interface <see cref="ISettingsOptions"/>.
    /// </summary>
    /// <remarks>
    /// This class implements interface <see cref="ISettingsOptions"/>
    /// and provides all functionality to manage settings options.
    /// </remarks>
    internal class SettingsOptions : ISettingsOptions
    {
        #region Private Constants

        /// <summary>
        /// The default settings file pattern.
        /// </summary>
        /// <remarks>
        /// JSON format is used as default settings file pattern.
        /// </remarks>
        /// <seealso cref="SettingsPattern.JsonPattern"/>
        private const SettingsPattern defaultPattern = SettingsPattern.JsonPattern;

        /// <summary>
        /// The default storage location.
        /// </summary>
        /// <remarks>
        /// The executable folder is used as default storage location.
        /// </remarks>
        /// <seealso cref="StorageLocation.DefaultLocation"/>
        /// <seealso cref="StorageLocation.ExecutableFolder"/>
        private const StorageLocation defaultLocation = StorageLocation.DefaultLocation;

        /// <summary>
        /// The default settings file extension.
        /// </summary>
        /// <remarks>
        /// Extension <c>.conf</c> is used as default settings file extension.
        /// </remarks>
        private const String defaultExtension = ".conf";

        #endregion

        #region Private Fields

        /// <summary>
        /// The settings file pattern currently used.
        /// </summary>
        /// <remarks>
        /// This field holds the currently used settings file pattern.
        /// </remarks>
        /// <seealso cref="SettingsOptions.Pattern"/>
        /// <seealso cref="SettingsOptions.defaultPattern"/>
        private SettingsPattern pattern = SettingsOptions.defaultPattern;

        /// <summary>
        /// The settings file storage location currently used.
        /// </summary>
        /// <remarks>
        /// This field holds the currently used settings file storage location.
        /// </remarks>
        /// <seealso cref="SettingsOptions.Location"/>
        /// <seealso cref="SettingsOptions.defaultLocation"/>
        private StorageLocation location = SettingsOptions.defaultLocation;

        /// <summary>
        /// The settings file extension currently used.
        /// </summary>
        /// <remarks>
        /// This field holds the currently used settings file extension.
        /// </remarks>
        /// <seealso cref="SettingsOptions.Extension"/>
        /// <seealso cref="SettingsOptions.defaultExtension"/>
        private String extension = SettingsOptions.defaultExtension;

        /// <summary>
        /// The settings file path is currently versionized or not.
        /// </summary>
        /// <remarks>
        /// This field holds the state whether the settings file path currently 
        /// includes the version number or not. The default value is false.
        /// </remarks>
        /// <seealso cref="SettingsOptions.IsVersionized"/>
        private Boolean versionized = false;

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <remarks>
        /// This event occurs when one of the property values has changed.
        /// </remarks>
        /// <seealso cref="SettingsOptions.RaisePropertyChanged(String)"/>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Construction

        /// <summary>
        /// Default construction.
        /// </summary>
        /// <remarks>
        /// The default constructor just initializes its properties with their 
        /// default values.
        /// </remarks>
        public SettingsOptions()
            : this(null)
        {
        }

        /// <summary>
        /// Parameterized construction.
        /// </summary>
        /// <remarks>
        /// This constructor initializes its properties from the objects within 
        /// <paramref name="values"/> array.
        /// </remarks>
        /// <param name="values">
        /// An array with typed values to initialize the properties.
        /// </param>
        /// <seealso cref="SettingsOptions.TryAssignProperty(Object)"/>
        public SettingsOptions(params Object[] values)
            : base()
        {
            if (values == null)
            {
                return;
            }

            foreach (Object value in values)
            {
                this.TryAssignProperty(value);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets and sets the pattern of the settings file.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property allows to control the type of the settings file. At the 
        /// moment two types are supported, JSON format and XML format.
        /// </para>
        /// <para>
        /// The default value of <see cref="SettingsOptions.defaultPattern"/> is used 
        /// in case of <c>value</c> is not included in <see cref="SettingsPattern"/>.
        /// </para>
        /// </remarks>
        /// <value>
        /// The pattern (type) of the settings file.
        /// </value>
        /// <seealso cref="SettingsOptions.PropertyChanged"/>
        /// <seealso cref="SettingsOptions.RaisePropertyChanged(String)"/>
        public SettingsPattern Pattern
        {
            get
            {
                return this.pattern;
            }
            set
            {
                if (!Enum.IsDefined(typeof(SettingsPattern), value))
                {
                    value = SettingsOptions.defaultPattern;
                }

                if (this.pattern != value)
                {
                    this.pattern = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets and sets the location of the settings file.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property allows to control the settings file location.
        /// </para>
        /// <para>
        /// The default value of <see cref="SettingsOptions.defaultLocation"/> is used 
        /// in case of <c>value</c> is not included in <see cref="StorageLocation"/>.
        /// </para>
        /// </remarks>
        /// <value>
        /// The location of the settings file.
        /// </value>
        /// <seealso cref="SettingsOptions.PropertyChanged"/>
        /// <seealso cref="SettingsOptions.RaisePropertyChanged(String)"/>
        public StorageLocation Location
        {
            get
            {
                return this.location;
            }
            set
            {
                if (!Enum.IsDefined(typeof(StorageLocation), value))
                {
                    value = SettingsOptions.defaultLocation;
                }

                if (this.location != value)
                {
                    this.location = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets and sets the settings file extension.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property allows to control the extension of the settings file.
        /// </para>
        /// <para>
        /// The default value of <see cref="SettingsOptions.defaultExtension"/> is 
        /// used in case of <c>value</c> is null, empty or whitespace. Additionally, 
        /// the value is trimmed and a dot is prepended if it is missing.
        /// </para>
        /// </remarks>
        /// <value>
        /// The settings file extension.
        /// </value>
        /// <seealso cref="SettingsOptions.PropertyChanged"/>
        /// <seealso cref="SettingsOptions.RaisePropertyChanged(String)"/>
        public String Extension
        {
            get
            {
                return this.extension;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = SettingsOptions.defaultExtension;
                }

                value = value.Trim();

                if (!value.StartsWith("."))
                {
                    value = $".{value}";
                }

                if (this.extension != value)
                {
                    this.extension = value;
                    this.RaisePropertyChanged();
                }
            }
        }

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
        /// <seealso cref="SettingsOptions.PropertyChanged"/>
        /// <seealso cref="SettingsOptions.RaisePropertyChanged(String)"/>
        public Boolean IsVersionized
        {
            get
            {
                return this.versionized;
            }
            set
            {
                if (this.versionized != value)
                {
                    this.versionized = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the full path of the settings file.
        /// </summary>
        /// <remarks>
        /// This method allows to get the full path of the settings file that will 
        /// be generated from this options instance.
        /// </remarks>
        /// <returns>
        /// The full path of the settings file.
        /// </returns>
        /// <exception cref="Exception">
        /// This method may throw an exception.
        /// </exception>
        /// <seealso cref="OptionsExtension.FetchStorageLocation(ISettingsOptions)"/>
        public String GetFullPath()
        {
            try
            {
                return this.FetchStorageLocation();
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsOptions)}: {exception}");

                throw;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Tries assigning a particular property.
        /// </summary>
        /// <remarks>
        /// This method tries to assign a particular property. Which property 
        /// has to be assigned will be fetched from the object type.
        /// </remarks>
        /// <param name="value">
        /// The value to assign.
        /// </param>
        /// <seealso cref="SettingsOptions(Object[])"/>
        private void TryAssignProperty(Object value)
        {
            if (value is SettingsPattern pattern)
            {
                this.Pattern = pattern;
            }
            else if (value is StorageLocation location)
            {
                this.Location = location;
            }
            else if (value is String extension)
            {
                this.Extension = extension;
            }
            else if (value is Boolean versionized)
            {
                this.IsVersionized = versionized;
            }
        }

        /// <summary>
        /// Raises the <see cref="SettingsOptions.PropertyChanged"/> event.
        /// </summary>
        /// <remarks>
        /// This method tries to raise the <see cref="SettingsOptions.PropertyChanged"/> 
        /// event and catches all occurring exceptions.
        /// </remarks>
        /// <param name="property">
        /// The property name that has been changed.
        /// </param>
        private void RaisePropertyChanged([CallerMemberName] String property = null)
        {
            try
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsOptions)}: {exception}");
            }
        }

        #endregion
    }
}
