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
using Plexdata.Utilities.Settings;
using System;
using System.Drawing;

namespace Plexdata.Settings.Serialization.Tester
{
    [JsonObject("program-settings")]
    public class ProgramSettings
    {
        [JsonProperty("application-title")]
        public String Title { get; set; }

        [JsonProperty("main-window-position")]
        public Point Location { get; set; }

        [JsonProperty("main-window-size")]
        public Size Dimension { get; set; }
    }

    class Program
    {
        static void Main(String[] args)
        {
            var options = SettingsFactory.Create<ISettingsOptions>(
                SettingsPattern.JsonPattern, StorageLocation.ExecutableFolder, "program.settings");

            Console.WriteLine(options.GetFullPath());

            var reader = SettingsFactory.Create<ISettingsReader<ProgramSettings>>();

            if (!reader.TryRead(options, out ProgramSettings settings))
            {
                settings = new ProgramSettings()
                {
                    Title = "My Title",
                    Location = new Point(100, 100),
                    Dimension = new Size(800, 600)
                };
            }

            Console.WriteLine(settings.Title);
            Console.WriteLine(settings.Location);
            Console.WriteLine(settings.Dimension);

            var writer = SettingsFactory.Create<ISettingsWriter<ProgramSettings>>();

            writer.TryWrite(options, settings);

            Console.Write("Hit any key to finish... ");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
