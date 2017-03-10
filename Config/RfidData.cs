using System;
using Newtonsoft.Json;

namespace Config
{
    public class RfidData
    {
        [JsonProperty("deviceId")]
        public string DeviceId;

        [JsonProperty("deviceName")]
        public string DeviceName;

        [JsonProperty("datetime")]
        public DateTime Datetime;

        [JsonProperty("rfidTag")]
        public string RfidTag;
    }
}
