using Newtonsoft.Json;

namespace University.BL.DTOs
{
    public class ResponseDTO
    {
        [JsonProperty("Code")]
        public int Code { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public dynamic Data { get; set; }
    }
}
