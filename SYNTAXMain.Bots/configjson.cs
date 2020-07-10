using Newtonsoft.Json;

namespace SYNTAXMain.Bots
{
    public struct Configjson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
    }
}
