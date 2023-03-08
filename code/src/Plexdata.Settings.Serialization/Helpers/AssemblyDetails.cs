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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Plexdata.Utilities.Settings.Helpers
{
    /// <summary>
    /// A helper class to determine a set of assembly details.
    /// </summary>
    /// <remarks>
    /// This class represents a helper class to be used determine 
    /// a set of assembly details.
    /// </remarks>
    internal class AssemblyDetails
    {
        #region Private Fields

        /// <summary>
        /// A set of characters that has to be excluded when a property value is used 
        /// as part of a path.
        /// </summary>
        /// <remarks>
        /// This list contains a set of characters that should never appear a path part.
        /// </remarks>
        /// <seealso cref="AssemblyDetails.ToPathCompatibleValue(String)"/>
        /// <seealso cref="Path.GetInvalidPathChars()"/>
        /// <seealso cref="Path.GetInvalidFileNameChars()"/>
        private static readonly List<Char> excludes = Path.GetInvalidPathChars().Concat(Path.GetInvalidFileNameChars()).Distinct().ToList();

        #endregion

        #region Construction

        /// <summary>
        /// Initializes the static fields of this class.
        /// </summary>
        /// <remarks>
        /// This constructor does actually nothing.
        /// </remarks>
        static AssemblyDetails()
        {
        }

        /// <summary>
        /// The only supported constructor.
        /// </summary>
        /// <remarks>
        /// This class may never be created externally.
        /// </remarks>
        /// <param name="company">
        /// The company descriptor taken from an assembly.
        /// </param>
        /// <param name="product">
        /// The product descriptor taken from an assembly.
        /// </param>
        /// <param name="version">
        /// The version descriptor taken from an assembly.
        /// </param>
        private AssemblyDetails(String company, String product, String version)
            : base()
        {
            this.Company = company;
            this.Product = product;
            this.Version = version;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The company descriptor of an assembly.
        /// </summary>
        /// <remarks>
        /// The company descriptor usually comes from <see cref="AssemblyCompanyAttribute"/>.
        /// </remarks>
        /// <value>
        /// A string representing the company descriptor.
        /// </value>
        public String Company { get; }

        /// <summary>
        /// The product descriptor of an assembly.
        /// </summary>
        /// <remarks>
        /// The product descriptor usually comes from <see cref="AssemblyProductAttribute"/>.
        /// </remarks>
        /// <value>
        /// A string representing the product descriptor.
        /// </value>
        public String Product { get; }

        /// <summary>
        /// The version descriptor of an assembly.
        /// </summary>
        /// <remarks>
        /// The version descriptor usually comes from <see cref="AssemblyVersionAttribute"/>.
        /// </remarks>
        /// <value>
        /// A string representing the version descriptor.
        /// </value>
        public String Version { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method creates an instance of this class and initializes its properties from 
        /// the details of provided <paramref name="assembly"/>. What is taken in detail is 
        /// shown in the bullet point list below.
        /// </para>
        /// <list type="bullet">
        /// <item><description>
        /// The company descriptor either comes from attribute <see cref="AssemblyCompanyAttribute"/> 
        /// or (if not exist) from the first part of the namespace of the declaring type of the 
        /// entry point (usually class <c>Program</c>). 
        /// </description></item>
        /// <item><description>
        /// The product descriptor either comes from attribute <see cref="AssemblyProductAttribute"/> 
        /// or (if not exist) is the file name of the <paramref name="assembly"/> but without its 
        /// file extension. 
        /// </description></item>
        /// <item><description>
        /// The version descriptor either comes from attribute <see cref="AssemblyVersionAttribute"/> 
        /// or (if not exist) from attribute <see cref="AssemblyFileVersionAttribute"/>. The version 
        /// descriptor <c>"0.0.0.0"</c> is taken in case of none of these attributes exists.
        /// </description></item>
        /// </list>
        /// <para>
        /// No matter which of these alternatives is taken in detail, any whitespace, any control 
        /// character, any invalid path name character as well as any invalid file name character 
        /// is removed. See also <see cref="AssemblyDetails.ToPathCompatibleValue(String)"/>.
        /// </para>
        /// </remarks>
        /// <param name="assembly">
        /// This assembly to initialize all class properties from.
        /// </param>
        /// <returns>
        /// A proper initialized instance of this class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// This exception is thrown if parameter <paramref name="assembly"/> is null.
        /// </exception>
        /// <seealso cref="AssemblyDetails.GetCompanyName(Assembly)"/>
        /// <seealso cref="AssemblyDetails.GetProductName(Assembly)"/>
        /// <seealso cref="AssemblyDetails.GetVersionName(Assembly)"/>
        public static AssemblyDetails Create(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            // After some investigations it came up that the "ConfigurationManager" either
            // uses value of attribute "AssemblyCompany" (if exists) or the first part of
            // namespace of the "Program.cs". The "Product" instead is always some kind of
            // executable name (maybe some kind of strong name). Hence, it should be done
            // in a similar way.

            String company = AssemblyDetails.GetCompanyName(assembly);
            String product = AssemblyDetails.GetProductName(assembly);
            String version = AssemblyDetails.GetVersionName(assembly);

            return new AssemblyDetails(company, product, version);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <remarks>
        /// This method returns a string that represents the current object.
        /// </remarks>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override String ToString()
        {
            StringBuilder builder = new StringBuilder(256);

            builder.AppendFormat("{0}: {1}, ", nameof(this.Company), this.Company);
            builder.AppendFormat("{0}: {1}, ", nameof(this.Product), this.Product);
            builder.AppendFormat("{0}: {1}", nameof(this.Version), this.Version);

            return builder.ToString();
        }

        /// <summary>
        /// Returns a string to be used as part of a path.
        /// </summary>
        /// <remarks>
        /// This method returns a string that can be used as part of a path.
        /// </remarks>
        /// <param name="versionized">
        /// If true the version descriptor is included. Otherwise the version 
        /// descriptor is ignored.
        /// </param>
        /// <returns>
        /// A string to be used as part of a path.
        /// </returns>
        public String ToString(Boolean versionized)
        {
            if (versionized)
            {
                return Path.Combine(this.Company, this.Product, this.Version);
            }

            return Path.Combine(this.Company, this.Product);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the company descriptor.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The company descriptor either comes from attribute <see cref="AssemblyCompanyAttribute"/> 
        /// or (if not exist) from the first part of the namespace of the declaring type of the 
        /// entry point (usually class <c>Program</c>). 
        /// </para>
        /// <para>
        /// Any whitespace, any control character, any invalid path name character as well as any invalid 
        /// file name character is removed no matter which alternative is taken.
        /// </para>
        /// </remarks>
        /// <param name="assembly">
        /// The assembly to get the company descriptor from.
        /// </param>
        /// <returns>
        /// A string representing the company descriptor.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// This exception is thrown if no company descriptor could be determined.
        /// </exception>
        /// <seealso cref="AssemblyDetails.Create(Assembly)"/>
        /// <seealso cref="AssemblyDetails.ToPathCompatibleValue(String)"/>
        private static String GetCompanyName(Assembly assembly)
        {
            String company = (assembly.GetCustomAttribute(typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute)?.Company;

            if (String.IsNullOrWhiteSpace(company))
            {
                company = assembly.EntryPoint.DeclaringType.Namespace.Split('.').FirstOrDefault();

                if (String.IsNullOrWhiteSpace(company))
                {
                    // No namespace defined. Should be impossible because of getting
                    // a compiler when removing the namespace from entry point class.
                    throw new InvalidOperationException("Unable to determine the company descriptor.");
                }
            }

            return AssemblyDetails.ToPathCompatibleValue(company);
        }

        /// <summary>
        /// Gets the product descriptor.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The product descriptor either comes from attribute <see cref="AssemblyProductAttribute"/> 
        /// or (if not exist) is the file name of the <paramref name="assembly"/> but without its 
        /// file extension. 
        /// </para>
        /// <para>
        /// Any whitespace, any control character, any invalid path name character as well as any invalid 
        /// file name character is removed no matter which alternative is taken.
        /// </para>
        /// </remarks>
        /// <param name="assembly">
        /// The assembly to get the product descriptor from.
        /// </param>
        /// <returns>
        /// A string representing the product descriptor.
        /// </returns>
        /// <seealso cref="AssemblyDetails.Create(Assembly)"/>
        /// <seealso cref="AssemblyDetails.ToPathCompatibleValue(String)"/>
        private static String GetProductName(Assembly assembly)
        {
            String product = (assembly.GetCustomAttribute(typeof(AssemblyProductAttribute)) as AssemblyProductAttribute)?.Product;

            if (String.IsNullOrWhiteSpace(product))
            {
                product = Path.GetFileNameWithoutExtension(assembly.Location);
            }

            return AssemblyDetails.ToPathCompatibleValue(product);
        }

        /// <summary>
        /// Gets the version descriptor.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The version descriptor either comes from attribute <see cref="AssemblyVersionAttribute"/> 
        /// or (if not exist) from attribute <see cref="AssemblyFileVersionAttribute"/>. The version 
        /// descriptor <c>"0.0.0.0"</c> is taken in case of none of these attributes exists.
        /// </para>
        /// <para>
        /// Any whitespace, any control character, any invalid path name character as well as any invalid 
        /// file name character is removed no matter which alternative is taken.
        /// </para>
        /// </remarks>
        /// <param name="assembly">
        /// The assembly to get the version descriptor from.
        /// </param>
        /// <returns>
        /// A string representing the version descriptor.
        /// </returns>
        /// <seealso cref="AssemblyDetails.Create(Assembly)"/>
        /// <seealso cref="AssemblyDetails.ToPathCompatibleValue(String)"/>
        private static String GetVersionName(Assembly assembly)
        {
            String version = assembly.GetName()?.Version?.ToString();

            if (String.IsNullOrWhiteSpace(version) || version == "0.0.0.0")
            {
                version = (assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute)) as AssemblyFileVersionAttribute)?.Version;

                if (String.IsNullOrWhiteSpace(version))
                {
                    version = "0.0.0.0";
                }
            }

            return AssemblyDetails.ToPathCompatibleValue(version);
        }

        /// <summary>
        /// Generates a string that is path compatible.
        /// </summary>
        /// <remarks>
        /// This method generates a string that is path compatible by skipping all white spaces, all 
        /// control characters, all invalid path characters and all invalid file name characters.
        /// </remarks>
        /// <param name="value">
        /// A string to convert into a path compatible value.
        /// </param>
        /// <returns>
        /// A path compatible string.
        /// </returns>
        /// <seealso cref="AssemblyDetails.excludes"/>
        private static String ToPathCompatibleValue(String value)
        {
            StringBuilder result = new StringBuilder(value.Length);

            foreach (Char current in value)
            {
                if (Char.IsWhiteSpace(current) || Char.IsControl(current) || AssemblyDetails.excludes.Contains(current))
                {
                    continue;
                }

                result.Append(current);
            }

            return result.ToString();
        }

        #endregion
    }
}
