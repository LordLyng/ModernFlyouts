using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModernFlyouts.Settings
{
    public class OutGoingGeneralSettings
    {
        [JsonPropertyName("general")]
        public GeneralSettings GeneralSettings { get; set; }

        public OutGoingGeneralSettings()
        {
        }

        public OutGoingGeneralSettings(GeneralSettings generalSettings)
        {
            GeneralSettings = generalSettings;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
