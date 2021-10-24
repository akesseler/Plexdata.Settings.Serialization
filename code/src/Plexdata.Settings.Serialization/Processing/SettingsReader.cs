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

using Newtonsoft.Json;
using Plexdata.Utilities.Settings.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace Plexdata.Utilities.Settings.Processing
{
    /// <summary>
    /// Implements interface <see cref="ISettingsReader{TSettings}"/>.
    /// </summary>
    /// <remarks>
    /// This class implements interface <see cref="ISettingsReader{TSettings}"/>
    /// and provides all functionality needed to read data from a settings file.
    /// </remarks>
    /// <typeparam name="TSettings">
    /// The custom type to be deserialized.
    /// </typeparam>
    internal class SettingsReader<TSettings> : ISettingsReader<TSettings>
    {
        #region Construction

        /// <summary>
        /// Default construction.
        /// </summary>
        /// <remarks>
        /// The default constructor just initializes this instance.
        /// </remarks>
        public SettingsReader()
            : base()
        {
        }

        #endregion

        #region Public Methods

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
        public Boolean TryRead(out TSettings settings)
        {
            settings = default;

            try
            {
                settings = this.Read();

                return settings != null;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsReader<TSettings>)}: {exception}");

                return false;
            }
        }

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
        public Boolean TryRead(ISettingsOptions options, out TSettings settings)
        {
            settings = default;

            try
            {
                settings = this.Read(options);

                return settings != null;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsReader<TSettings>)}: {exception}");

                return false;
            }
        }

        /// <summary>
        /// Tries reading data from a settings file referenced by <paramref name="filename"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="SettingsReader{TSettings}.TryRead(out TSettings)"/> 
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
        public Boolean TryRead(String filename, out TSettings settings)
        {
            settings = default;

            try
            {
                settings = this.Read(filename);

                return settings != null;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsReader<TSettings>)}: {exception}");

                return false;
            }
        }

        /// <summary>
        /// Tries reading data from a settings file referenced by <paramref name="filename"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="SettingsReader{TSettings}.TryRead(ISettingsOptions, out TSettings)"/> 
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
        public Boolean TryRead(ISettingsOptions options, String filename, out TSettings settings)
        {
            settings = default;

            try
            {
                settings = this.Read(options, filename);

                return settings != null;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsReader<TSettings>)}: {exception}");

                return false;
            }
        }

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
        public Boolean TryRead(StreamReader reader, out TSettings settings)
        {
            settings = default;

            try
            {
                settings = this.Read(reader);

                return settings != null;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsReader<TSettings>)}: {exception}");

                return false;
            }
        }

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
        public Boolean TryRead(ISettingsOptions options, StreamReader reader, out TSettings settings)
        {
            settings = default;

            try
            {
                settings = this.Read(options, reader);

                return settings != null;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsReader<TSettings>)}: {exception}");

                return false;
            }
        }

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
        public TSettings Read()
        {
            return this.Read(this.GetDefaultOptions());
        }

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
        public TSettings Read(ISettingsOptions options)
        {
            return this.Read(options, options?.FetchStorageLocation());
        }

        /// <summary>
        /// Reads data from a settings file referenced by <paramref name="filename"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="SettingsReader{TSettings}.Read()"/> this method 
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
        public TSettings Read(String filename)
        {
            return this.Read(this.GetDefaultOptions(), filename);
        }

        /// <summary>
        /// Reads data from a settings file referenced by <paramref name="filename"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="SettingsReader{TSettings}.Read(ISettingsOptions)"/> 
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
        /// <exception cref="ArgumentNullException">
        /// This exception is thrown if parameter <paramref name="options"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if parameter <paramref name="filename"/> is null 
        /// or empty or consists of whitespaces only.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// This exception is thrown if <paramref name="filename"/> could not be found.
        /// </exception>
        /// <exception cref="Exception">
        /// Any other exception that can occur in conjunction with NuGet package 
        /// <c>Newtonsoft.Json</c>.
        /// </exception>
        public TSettings Read(ISettingsOptions options, String filename)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), $"Parameter {nameof(options)} must not be null.");
            }

            if (String.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Filename must not be null or empty or consists of whitespace only.", nameof(filename));
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Unable to read a non-existing file.", filename);
            }

            Trace.WriteLine($"{nameof(SettingsReader<TSettings>)}: Used filename \"{filename}\".");

            using (StreamReader reader = File.OpenText(filename))
            {
                return this.Read(options, reader);
            }
        }

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
        public TSettings Read(StreamReader reader)
        {
            return this.Read(this.GetDefaultOptions(), reader);
        }

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
        /// <exception cref="ArgumentNullException">
        /// This exception is thrown either if parameter <paramref name="options"/> 
        /// or parameter <paramref name="reader"/> is null.
        /// </exception>
        /// <exception cref="Exception">
        /// Any other exception that can occur in conjunction with NuGet package 
        /// <c>Newtonsoft.Json</c>.
        /// </exception>
        public TSettings Read(ISettingsOptions options, StreamReader reader)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), $"Parameter {nameof(options)} must not be null.");
            }

            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader), $"Parameter {nameof(reader)} must not be null.");
            }

            switch (options.Pattern)
            {
                case SettingsPattern.JsonPattern:
                    return this.ReadJson(reader);
                case SettingsPattern.XmlPattern:
                    return this.ReadXml(reader);
                default:
                    // Does (should) never happen because of the property setter
                    // used a default value in case of an invalid value.
                    throw new NotSupportedException();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of default options.
        /// </summary>
        /// <remarks>
        /// This method creates an instance of default options using 
        /// <see cref="SettingsFactory.Create{TInterface}()"/>.
        /// </remarks>
        /// <returns>
        /// An instance of default options.
        /// </returns>
        private ISettingsOptions GetDefaultOptions()
        {
            return SettingsFactory.Create<ISettingsOptions>();
        }

        /// <summary>
        /// Reads JSON data from provided <paramref name="reader"/> and deserializes 
        /// them into an instance of <typeparamref name="TSettings"/>.
        /// </summary>
        /// <remarks>
        /// This method reads JSON data from provided <paramref name="reader"/> and 
        /// deserializes them into an instance of <typeparamref name="TSettings"/>.
        /// </remarks>
        /// <param name="reader">
        /// An instance of <see cref="StreamReader"/> reading data from.
        /// </param>
        /// <returns>
        /// An initialized instance of <typeparamref name="TSettings"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Any exception either of <see cref="JsonSerializer.Create()"/> or 
        /// of <see cref="JsonSerializer.Deserialize(TextReader, Type)"/>.
        /// </exception>
        private TSettings ReadJson(StreamReader reader)
        {
            return (TSettings)JsonSerializer.Create()
                .Deserialize(reader, typeof(TSettings));
        }

        /// <summary>
        /// Reads XML data from provided <paramref name="reader"/> and deserializes 
        /// them into an instance of <typeparamref name="TSettings"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method reads XML data from provided <paramref name="reader"/> and 
        /// deserializes them into an instance of <typeparamref name="TSettings"/>.
        /// </para>
        /// <para>
        /// Why is <c>Newtonsoft.Json</c> used to process XML data? The 
        /// <see cref="System.Xml.Serialization.XmlSerializer"/> does not support 
        /// <see cref="System.Collections.Generic.IEnumerable{T}"/>!
        /// </para>
        /// </remarks>
        /// <param name="reader">
        /// An instance of <see cref="StreamReader"/> reading data from.
        /// </param>
        /// <returns>
        /// An initialized instance of <typeparamref name="TSettings"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Any exception either of <see cref="JsonConvert.DeserializeObject{T}(String)"/> or 
        /// of <see cref="JsonConvert.SerializeXNode(XObject, Formatting, Boolean)"/> or of
        /// <see cref="XDocument.Load(TextReader, LoadOptions)"/>.
        /// </exception>
        private TSettings ReadXml(StreamReader reader)
        {
            // Why this way? The XmlSerializer does not support IEnumerable!

            return JsonConvert.DeserializeObject<TSettings>(
                JsonConvert.SerializeXNode(XDocument.Load(reader, LoadOptions.None)?.Root, Formatting.None, true)
            );
        }

        #endregion
    }
}
