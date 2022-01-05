// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using ModernFlyouts.Settings.Interfaces;
using ModernFlyouts.Settings.Utilities;

namespace ModernFlyouts.Settings
{
    public class GeneralSettings : ISettingsConfig
    {
        // Gets or sets a value indicating whether run ModernFlyouts on start-up.
        [JsonPropertyName("startup")]
        public bool Startup { get; set; }

        // Gets or sets a value indicating whether the powertoy elevated.
        [JsonPropertyName("is_elevated")]
        public bool IsElevated { get; set; }

        // Gets or sets a value indicating whether ModernFlyouts should run elevated.
        [JsonPropertyName("run_elevated")]
        public bool RunElevated { get; set; }

        // Gets or sets a value indicating whether is admin.
        [JsonPropertyName("is_admin")]
        public bool IsAdmin { get; set; }

        // Gets or sets theme Name.
        [JsonPropertyName("theme")]
        public string Theme { get; set; }

        // Gets or sets system theme name.
        [JsonPropertyName("system_theme")]
        public string SystemTheme { get; set; }

        // Gets or sets ModernFlyouts version number.
        [JsonPropertyName("ModernFlyouts_version")]
        public string ModernFlyoutsVersion { get; set; }

        [JsonPropertyName("action_name")]
        public string CustomActionName { get; set; }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Any error from calling interop code should not prevent the program from loading.")]
        public GeneralSettings()
        {
            Startup = false;
            IsAdmin = false;
            IsElevated = false;
            Theme = "system";
            SystemTheme = "light";
            try
            {
               // ModernFlyoutsVersion = DefaultModernFlyoutsVersion();
            }
            catch (Exception e)
            {
                Logger.LogError("Exception encountered when getting ModernFlyouts version", e);
                ModernFlyoutsVersion = "v0.0.0";
            }

//            Enabled = new EnabledModules();
            CustomActionName = string.Empty;
        }

        // converts the current to a json string.
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }

        // This function is to implement the ISettingsConfig interface.
        // This interface helps in getting the settings configurations.
        public string GetModuleName()
        {
            // The SettingsUtils functions access general settings when the module name is an empty string.
            return string.Empty;
        }

        public bool UpgradeSettingsConfiguration()
        {
            try
            {
                if (Helper.CompareVersions(ModernFlyoutsVersion, Helper.GetProductVersion()) != 0)
                {
                    // Update settings
                    ModernFlyoutsVersion = Helper.GetProductVersion();
                    return true;
                }
            }
            catch (FormatException)
            {
                // If there is an issue with the version number format, don't migrate settings.
            }

            return false;
        }
    }
}
