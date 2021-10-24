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

using Plexdata.Utilities.Settings.Options;
using Plexdata.Utilities.Settings.Processing;
using System;
using System.Diagnostics;
using System.Linq;

namespace Plexdata.Utilities.Settings
{
    /// <summary>
    /// A factory that can create instances used by this serializer.
    /// </summary>
    /// <remarks>
    /// This factory provides methods to create instances used by this serializer.
    /// </remarks>
    public static class SettingsFactory
    {
        #region Public Methods

        /// <summary>
        /// Tries creating an instance of <typeparamref name="TInterface"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method tries to create an instance of <typeparamref name="TInterface"/> 
        /// using the parameterless constructor.
        /// </para>
        /// <para>
        /// At the moment, the supported interface types are <see cref="ISettingsOptions"/>, 
        /// <see cref="ISettingsReader{TSettings}"/> and <see cref="ISettingsWriter{TSettings}"/>.
        /// </para>
        /// </remarks>
        /// <typeparam name="TInterface">
        /// The interface type to create an instance for.
        /// </typeparam>
        /// <param name="instance">
        /// The result value of the created instance.
        /// </param>
        /// <returns>
        /// True, if instance creation was successful and false otherwise.
        /// </returns>
        /// <seealso cref="TryCreate{TInterface}(out TInterface, Object[])"/>
        /// <seealso cref="Create{TInterface}()"/>
        /// <seealso cref="Create{TInterface}(Object[])"/>
        public static Boolean TryCreate<TInterface>(out TInterface instance)
        {
            return SettingsFactory.TryCreate(out instance, null);
        }

        /// <summary>
        /// Tries creating an instance of <typeparamref name="TInterface"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method tries to create an instance of <typeparamref name="TInterface"/> 
        /// using an appropriated parameterized constructor.
        /// </para>
        /// <para>
        /// At the moment, the supported interface types are <see cref="ISettingsOptions"/>, 
        /// <see cref="ISettingsReader{TSettings}"/> and <see cref="ISettingsWriter{TSettings}"/>.
        /// </para>
        /// </remarks>
        /// <typeparam name="TInterface">
        /// The interface type to create an instance for.
        /// </typeparam>
        /// <param name="instance">
        /// The result value of the created instance.
        /// </param>
        /// <param name="arguments">
        /// The list of constructor arguments. This list of arguments must fit the order 
        /// of types for a particular constructor.
        /// </param>
        /// <returns>
        /// True, if instance creation was successful and false otherwise.
        /// </returns>
        /// <seealso cref="TryCreate{TInterface}(out TInterface)"/>
        /// <seealso cref="Create{TInterface}()"/>
        /// <seealso cref="Create{TInterface}(Object[])"/>
        public static Boolean TryCreate<TInterface>(out TInterface instance, params Object[] arguments)
        {
            instance = default;

            try
            {
                instance = SettingsFactory.Create<TInterface>(arguments);

                return true;
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(SettingsFactory)}: {exception}");

                return false;
            }
        }

        /// <summary>
        /// Creates an instance of <typeparamref name="TInterface"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method creates an instance of <typeparamref name="TInterface"/> if possible.
        /// </para>
        /// <para>
        /// At the moment, the supported interface types are <see cref="ISettingsOptions"/>, 
        /// <see cref="ISettingsReader{TSettings}"/> and <see cref="ISettingsWriter{TSettings}"/>.
        /// </para>
        /// </remarks>
        /// <typeparam name="TInterface">
        /// The interface type to create an instance for.
        /// </typeparam>
        /// <returns>
        /// The result value of the created instance.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// This exception is thrown if requested interface type is not supported.
        /// </exception>
        /// <exception cref="Exception">
        /// This method may throw any of the exceptions either of <see cref="Activator.CreateInstance(Type)"/> 
        /// or of <see cref="Activator.CreateInstance(Type, Object[])"/> or any of the exceptions thrown by 
        /// <see cref="Type.MakeGenericType(Type[])"/> or any other exception that can occur in this context.
        /// </exception>
        /// <seealso cref="TryCreate{TInterface}(out TInterface)"/>
        /// <seealso cref="TryCreate{TInterface}(out TInterface, Object[])"/>
        /// <seealso cref="Create{TInterface}(Object[])"/>
        public static TInterface Create<TInterface>()
        {
            return SettingsFactory.Create<TInterface>(null);
        }

        /// <summary>
        /// Creates an instance of <typeparamref name="TInterface"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method creates an instance of <typeparamref name="TInterface"/> if possible.
        /// </para>
        /// <para>
        /// At the moment, the supported interface types are <see cref="ISettingsOptions"/>, 
        /// <see cref="ISettingsReader{TSettings}"/> and <see cref="ISettingsWriter{TSettings}"/>.
        /// </para>
        /// </remarks>
        /// <typeparam name="TInterface">
        /// The interface type to create an instance for.
        /// </typeparam>
        /// <param name="arguments">
        /// The list of constructor arguments. This list of arguments must fit the order 
        /// of types for a particular constructor.
        /// </param>
        /// <returns>
        /// The result value of the created instance.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// This exception is thrown if requested interface type is not supported.
        /// </exception>
        /// <exception cref="Exception">
        /// This method may throw any of the exceptions either of <see cref="Activator.CreateInstance(Type)"/> 
        /// or of <see cref="Activator.CreateInstance(Type, Object[])"/> or any of the exceptions thrown by 
        /// <see cref="Type.MakeGenericType(Type[])"/> or any other exception that can occur in this context.
        /// </exception>
        /// <seealso cref="TryCreate{TInterface}(out TInterface)"/>
        /// <seealso cref="TryCreate{TInterface}(out TInterface, Object[])"/>
        /// <seealso cref="Create{TInterface}()"/>
        public static TInterface Create<TInterface>(params Object[] arguments)
        {
            if (SettingsFactory.IsTargetOfRegularType<TInterface>(typeof(ISettingsOptions)))
            {
                return SettingsFactory.GetInstanceOfRegularType<TInterface>(typeof(SettingsOptions), arguments);
            }

            if (SettingsFactory.IsTargetOfGenericType<TInterface>(typeof(ISettingsReader<>)))
            {
                return SettingsFactory.GetInstanceOfGenericType<TInterface>(typeof(SettingsReader<>), arguments);
            }

            if (SettingsFactory.IsTargetOfGenericType<TInterface>(typeof(ISettingsWriter<>)))
            {
                return SettingsFactory.GetInstanceOfGenericType<TInterface>(typeof(SettingsWriter<>), arguments);
            }

            throw new NotSupportedException();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Determines if <typeparamref name="TInterface"/> is a non-generic interface.
        /// </summary>
        /// <remarks>
        /// This method determines if <typeparamref name="TInterface"/> represents a non-generic 
        /// interface.
        /// </remarks>
        /// <typeparam name="TInterface">
        /// The interface type to be determined.
        /// </typeparam>
        /// <param name="target">
        /// The expected target type to be determined.
        /// </param>
        /// <returns>
        /// True, if requested type can be considered as target type and false otherwise.
        /// </returns>
        private static Boolean IsTargetOfRegularType<TInterface>(Type target)
        {
            Type source = typeof(TInterface);

            return source == target;
        }

        /// <summary>
        /// Determines if <typeparamref name="TInterface"/> is a generic interface.
        /// </summary>
        /// <remarks>
        /// This method determines if <typeparamref name="TInterface"/> represents a generic 
        /// interface.
        /// </remarks>
        /// <typeparam name="TInterface">
        /// The interface type to be determined.
        /// </typeparam>
        /// <param name="target">
        /// The expected target type to be determined.
        /// </param>
        /// <returns>
        /// True, if requested type can be considered as target type and false otherwise.
        /// </returns>
        private static Boolean IsTargetOfGenericType<TInterface>(Type target)
        {
            Type source = typeof(TInterface);

            return source.IsGenericType && source.GetGenericTypeDefinition() == target;
        }

        /// <summary>
        /// Creates an instance of a non-generic interface of type <typeparamref name="TInterface"/>.
        /// </summary>
        /// <remarks>
        /// This method creates an instance of a non-generic interface of type 
        /// <typeparamref name="TInterface"/> either using <see cref="Activator.CreateInstance(Type)"/> 
        /// or using <see cref="Activator.CreateInstance(Type, Object[])"/>.
        /// </remarks>
        /// <typeparam name="TInterface">
        /// The interface type to be created.
        /// </typeparam>
        /// <param name="target">
        /// The expected target type to be created.
        /// </param>
        /// <param name="arguments">
        /// The list of constructor arguments. This list of arguments must fit the order 
        /// of types for a particular constructor.
        /// </param>
        /// <returns>
        /// An instance of created non-generic interface.
        /// </returns>
        /// <exception cref="Exception">
        /// This method may throw any of the exceptions either of <see cref="Activator.CreateInstance(Type)"/> 
        /// or of <see cref="Activator.CreateInstance(Type, Object[])"/>.
        /// </exception>
        private static TInterface GetInstanceOfRegularType<TInterface>(Type target, Object[] arguments)
        {
            if (!arguments?.Any() ?? true)
            {
                return (TInterface)Activator.CreateInstance(target);
            }
            else
            {
                return (TInterface)Activator.CreateInstance(target, arguments);
            }
        }

        /// <summary>
        /// Creates an instance of a generic interface of type <typeparamref name="TInterface"/>.
        /// </summary>
        /// <remarks>
        /// This method creates an instance of a generic interface of type 
        /// <typeparamref name="TInterface"/> either using <see cref="Activator.CreateInstance(Type)"/> 
        /// or using <see cref="Activator.CreateInstance(Type, Object[])"/>.
        /// </remarks>
        /// <typeparam name="TInterface">
        /// The interface type to be created.
        /// </typeparam>
        /// <param name="target">
        /// The expected target type to be created.
        /// </param>
        /// <param name="arguments">
        /// The list of constructor arguments. This list of arguments must fit the order 
        /// of types for a particular constructor.
        /// </param>
        /// <returns>
        /// An instance of created generic interface.
        /// </returns>
        /// <exception cref="Exception">
        /// This method may throw any of the exceptions either of <see cref="Activator.CreateInstance(Type)"/> 
        /// or of <see cref="Activator.CreateInstance(Type, Object[])"/> or any of the exceptions thrown by 
        /// <see cref="Type.MakeGenericType(Type[])"/> or any other exception that can occur in this context.
        /// </exception>
        private static TInterface GetInstanceOfGenericType<TInterface>(Type target, Object[] arguments)
        {
            Type[] generics = typeof(TInterface).GetGenericArguments();

            Type result = target.MakeGenericType(generics);

            if (!arguments?.Any() ?? true)
            {
                return (TInterface)Activator.CreateInstance(result);
            }
            else
            {
                return (TInterface)Activator.CreateInstance(result, arguments);
            }
        }

        #endregion
    }
}
