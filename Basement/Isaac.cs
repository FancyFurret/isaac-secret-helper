using System.Text.Json.Serialization;

namespace Basement
{
    public class Isaac
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Dlc
        {
            Rebirth,
            Afterbirth,
            AfterbirthPlus,
            Repentance
        }
    }
}