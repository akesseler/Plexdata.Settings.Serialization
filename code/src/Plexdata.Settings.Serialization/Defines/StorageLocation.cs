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
    /// The possible storage locations.
    /// </summary>
    /// <remarks>
    /// This enumeration provides all supported storage locations.
    /// </remarks>
    public enum StorageLocation
    {
        /// <summary>
        /// The default storage location. In this case the folder of <see cref="StorageLocation.ExecutableFolder"/> is used.
        /// </summary>
        DefaultLocation,

        /// <summary>
        /// The folder of the executing assembly. This folder is used only if writing files is permitted. In case of writing 
        /// is not permitted the folder referenced by <see cref="StorageLocation.FallbackLocation"/> is taken instead.
        /// </summary>
        ExecutableFolder,

        /// <summary>
        /// The fallback storage location. This folder is used in case of writing files into chosen folder is not permitted.
        /// The folder of <see cref="StorageLocation.LocalApplicationData"/> is used for this purpose.
        /// </summary>
        FallbackLocation,

        /// <summary>
        /// The local application data folder. The resulting path should look like: 
        /// <c>&lt;drive&gt;\Users\&lt;username&gt;\AppData\Local\&lt;company&gt;\&lt;product&gt;\&lt;filename&gt;.&lt;extension&gt;</c>
        /// </summary>
        LocalApplicationData,

        /// <summary>
        /// The standard application data folder. The resulting path should look like: 
        /// <c>&lt;drive&gt;\Users\&lt;username&gt;\AppData\Roaming\&lt;company&gt;\&lt;product&gt;\&lt;filename&gt;.&lt;extension&gt;</c>
        /// </summary>
        ApplicationData,

        /// <summary>
        /// The common application data folder. The resulting path should look like: 
        /// <c>&lt;drive&gt;\ProgramData\&lt;company&gt;\&lt;product&gt;\&lt;filename&gt;.&lt;extension&gt;</c>
        /// </summary>
        CommonApplicationData,
    }
}
