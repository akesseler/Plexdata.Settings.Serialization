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

using Newtonsoft.Json;
using Plexdata.Utilities.Settings.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Plexdata.Utilities.Settings.Processing
{
    /// <summary>
    /// Implements interface <see cref="ISettingsWriter{TSettings}"/>.
    /// </summary>
    /// <remarks>
    /// This class implements interface <see cref="ISettingsWriter{TSettings}"/>
    /// and provides all functionality needed to to write into a settings file.
    /// </remarks>
    /// <typeparam name="TSettings">
    /// The custom type to be deserialized.
    /// </typeparam>
    internal class SettingsWriter<TSettings> : ISettingsWriter<TSettings>
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <remarks>
        /// This constructor does actually nothing.
        /// </remarks>
        public SettingsWriter()
            : base()
        {
        }

        #endregion

        #region Public Methods

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
        public Boolean TryWrite(TSettings settings)
        {
            try
            {
                this.Write(settings);

                return true;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsWriter<TSettings>)}: {exception}");

                return false;
            }
        }

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
        public Boolean TryWrite(ISettingsOptions options, TSettings settings)
        {
            try
            {
                this.Write(options, settings);

                return true;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsWriter<TSettings>)}: {exception}");

                return false;
            }
        }

        /// <summary>
        /// Tries writing data into a settings file referenced by <paramref name="filename"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="SettingsWriter{TSettings}.TryWrite(TSettings)"/> this method takes 
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
        public Boolean TryWrite(String filename, TSettings settings)
        {
            try
            {
                this.Write(filename, settings);

                return true;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsWriter<TSettings>)}: {exception}");

                return false;
            }
        }

        /// <summary>
        /// Tries writing data into a settings file referenced by <paramref name="filename"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="SettingsWriter{TSettings}.TryWrite(ISettingsOptions, TSettings)"/> 
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
        public Boolean TryWrite(ISettingsOptions options, String filename, TSettings settings)
        {
            try
            {
                this.Write(options, filename, settings);

                return true;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsWriter<TSettings>)}: {exception}");

                return false;
            }
        }

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
        public Boolean TryWrite(StreamWriter writer, TSettings settings)
        {
            try
            {
                this.Write(writer, settings);

                return true;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsWriter<TSettings>)}: {exception}");

                return false;
            }
        }

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
        public Boolean TryWrite(ISettingsOptions options, StreamWriter writer, TSettings settings)
        {
            try
            {
                this.Write(options, writer, settings);

                return true;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsWriter<TSettings>)}: {exception}");

                return false;
            }

        }

        /// <summary>
        /// Writes data into a settings file using default options.
        /// </summary>
        /// <remarks>
        /// The name of the settings file is fetched from default options.
        /// </remarks>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        public void Write(TSettings settings)
        {
            this.Write(this.GetDefaultOptions(), settings);
        }

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
        public void Write(ISettingsOptions options, TSettings settings)
        {
            this.Write(options, options?.FetchStorageLocation(), settings);
        }

        /// <summary>
        /// Writes data into a settings file referenced by <paramref name="filename"/> 
        /// using default options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="SettingsWriter{TSettings}.Write(TSettings)"/> this method 
        /// takes provided filename as is. All other information are taken from default 
        /// options.
        /// </remarks>
        /// <param name="filename">
        /// The fully qualified path of the settings file to write.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written into a settings file.
        /// </param>
        public void Write(String filename, TSettings settings)
        {
            this.Write(this.GetDefaultOptions(), filename, settings);
        }

        /// <summary>
        /// Writes data into a settings file referenced by <paramref name="filename"/> 
        /// using provided options.
        /// </summary>
        /// <remarks>
        /// In contrast to <see cref="SettingsWriter{TSettings}.Write(String, TSettings)"/> this method 
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
        /// <exception cref="ArgumentNullException">
        /// This exception is thrown if parameter <paramref name="options"/> or if parameter 
        /// <paramref name="settings"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if parameter <paramref name="filename"/> is null 
        /// or empty or consists of whitespaces only.
        /// </exception>
        /// <exception cref="Exception">
        /// Any other exception that can occur in conjunction with NuGet package 
        /// <c>Newtonsoft.Json</c>.
        /// </exception>
        public void Write(ISettingsOptions options, String filename, TSettings settings)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), $"Parameter {nameof(options)} must not be null.");
            }

            if (String.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Filename must not be null or empty or consists of whitespace only.", nameof(filename));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), $"Parameter {nameof(settings)} must not be null.");
            }

            Trace.WriteLine($"{nameof(SettingsWriter<TSettings>)}: Used filename \"{filename}\".");

            String result = null;

            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    this.Write(options, writer, settings);

                    writer.Flush();

                    result = writer.Encoding.GetString(stream.ToArray());
                }
            }

            if (String.IsNullOrWhiteSpace(result))
            {
                return;
            }

            FileInfo info = new FileInfo(filename);

            if (!info.Directory.Exists)
            {
                Directory.CreateDirectory(info.DirectoryName);
            }

            using (StreamWriter writer = File.CreateText(info.FullName))
            {
                writer.Write(result);
            }
        }

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
        public void Write(StreamWriter writer, TSettings settings)
        {
            this.Write(this.GetDefaultOptions(), writer, settings);
        }

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
        /// <exception cref="ArgumentNullException">
        /// This exception is thrown if parameter <paramref name="options"/> or if parameter 
        /// <paramref name="writer"/> or if parameter <paramref name="settings"/> is null.
        /// </exception>
        /// <exception cref="Exception">
        /// Any other exception that can occur in conjunction with NuGet package 
        /// <c>Newtonsoft.Json</c>.
        /// </exception>
        public void Write(ISettingsOptions options, StreamWriter writer, TSettings settings)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), $"Parameter {nameof(options)} must not be null.");
            }

            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer), $"Parameter {nameof(writer)} must not be null.");
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), $"Parameter {nameof(settings)} must not be null.");
            }

            switch (options.Pattern)
            {
                case SettingsPattern.JsonPattern:
                    this.WriteJson(writer, settings);
                    return;
                case SettingsPattern.XmlPattern:
                    this.WriteXml(writer, settings);
                    return;
                default:
                    // Does (should) never happen because of the property setter
                    // used a default value in case of an invalid value.
                    throw new NotSupportedException();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of <see cref="ISettingsOptions"/> used as default options.
        /// </summary>
        /// <remarks>
        /// The default options are created using <see cref="SettingsFactory.Create{TInterface}()"/>.
        /// </remarks>
        /// <returns>
        /// An instance of <see cref="ISettingsOptions"/> used as default options.
        /// </returns>
        private ISettingsOptions GetDefaultOptions()
        {
            return SettingsFactory.Create<ISettingsOptions>();
        }

        /// <summary>
        /// Writes the <paramref name="settings"/> data as JSON into the <paramref name="writer"/>.
        /// </summary>
        /// <remarks>
        /// This method writes the <paramref name="settings"/> data as JSON into the <paramref name="writer"/>.
        /// </remarks>
        /// <param name="writer">
        /// An instance of <see cref="StreamWriter"/> to write settings data into.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written.
        /// </param>
        private void WriteJson(StreamWriter writer, TSettings settings)
        {
            JsonSerializer
                .Create(this.GetSerializerSettings())
                .Serialize(writer, settings, typeof(TSettings));
        }

        /// <summary>
        /// Writes the <paramref name="settings"/> data as XML into the <paramref name="writer"/>.
        /// </summary>
        /// <remarks>
        /// This method writes the <paramref name="settings"/> data as XML into the <paramref name="writer"/>.
        /// </remarks>
        /// <param name="writer">
        /// An instance of <see cref="StreamWriter"/> to write settings data into.
        /// </param>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to be written.
        /// </param>
        private void WriteXml(StreamWriter writer, TSettings settings)
        {
            // Why this way? The XmlSerializer does not support IEnumerable!

            JsonConvert.DeserializeXNode(
                JsonConvert.SerializeObject(settings, typeof(TSettings), this.GetSerializerSettings()),
                this.GetRootName(settings)
            ).Save(writer, SaveOptions.None);
        }

        /// <summary>
        /// Returns the default JSON serialization settings.
        /// </summary>
        /// <remarks>
        /// This method creates and returns the default JSON serialization settings.
        /// </remarks>
        /// <returns>
        /// Default JSON serialization settings to be used.
        /// </returns>
        private JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }

        /// <summary>
        /// Gets the name of the root object.
        /// </summary>
        /// <remarks>
        /// This method determines the name of the root object and returns it. 
        /// This name is only necessary to serialize XML data.
        /// </remarks>
        /// <param name="settings">
        /// An instance of <typeparamref name="TSettings"/> to fetch the name of the root object from.
        /// </param>
        /// <returns>
        /// The name of the root object.
        /// </returns>
        private String GetRootName(TSettings settings)
        {
            String name = settings.GetType().Name;

            JsonObjectAttribute attribute = (JsonObjectAttribute)typeof(TSettings)
                .GetCustomAttributes(true)
                .FirstOrDefault(x => x is JsonObjectAttribute);

            if (attribute != null)
            {
                if (!String.IsNullOrWhiteSpace(attribute.Id))
                {
                    name = attribute.Id.Trim();
                }
                else if (!String.IsNullOrWhiteSpace(attribute.Title))
                {
                    name = attribute.Title.Trim();
                }
            }

            return name;
        }

        #endregion
    }
}
