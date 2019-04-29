/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Dolittle.Build.MSBuild
{
    /// <summary>
    /// Represents a task that is capable of discovering plugins to the Dolittle build pipeline
    /// </summary>
    public class PluginAndConfigurationDiscoverer : Task
    {
        /// <summary>
        /// Gets or sets the aggregated plugins 
        /// </summary>
        [Required]
        public ITaskItem[] Plugins { get; set; }

        /// <summary>
        /// Path to the configuration file
        /// </summary>
        [Output]
        public string ConfigurationFile {  get; set; }

        /// <summary>
        /// Paths to assemblies that holds plugins
        /// </summary>
        [Output]
        public string[] PluginAssemblies {  get; set; }

        /// <inheritdoc/>
        public override bool Execute()
        {
            var pluginAssemblies = new List<string>();

            ConfigurationFile = Path.GetTempFileName();

            Log.LogMessage(MessageImportance.High, $"Plugin configurations will be stored in '{ConfigurationFile}'");

            var builder = new StringBuilder();
            var firstItem = true;
            builder.Append("{");
            foreach (var item in Plugins)
            {
                if (!firstItem) builder.Append(",");
                firstItem = false;

                var plugin = item.ItemSpec;

                var customMetadata = item.CloneCustomMetadata();
                builder.Append($"\"{plugin}\":{{");

                Log.LogMessage(MessageImportance.High, $"Handling plugin {plugin}");

                var pluginAssembly = GatherAllConfigKeys(builder, customMetadata);
                if (string.IsNullOrEmpty(pluginAssembly))
                {
                    Log.LogError($"Missing plugin assembly for plugin '{plugin}'");
                    return false;
                }
                pluginAssemblies.Add(pluginAssembly);
                builder.Append("}");
            }
            builder.Append("}");

            File.WriteAllText(ConfigurationFile, builder.ToString());

            foreach( var asm in pluginAssemblies ) Log.LogMessage(MessageImportance.High, $"Assembly : {asm}");

            PluginAssemblies = pluginAssemblies.ToArray();
            return true;
        }

        string GatherAllConfigKeys(StringBuilder builder, IDictionary customMetadata)
        {
            var firstPair = true;
            foreach (var key in customMetadata.Keys)
            {
                Log.LogMessage(MessageImportance.High, $"Key/Value : {key} - {customMetadata[key]}");
                if (!firstPair) builder.Append(",");

                if (key.ToString() == "Assembly")
                {
                    return customMetadata[key].ToString();
                }
                else
                {
                    firstPair = false;
                    builder.Append($"\"{key}\":\"{customMetadata[key]}\"");
                }
            }

            return string.Empty;
        }
    }
}