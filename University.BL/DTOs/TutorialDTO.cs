using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace University.BL.DTOs
{
    public class TutorialDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("lastUpdate")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("created")]
        public CreatedDTO Created { get; set; }

        [JsonProperty("reviews")]
        public List<ReviewDTO> Reviews { get; set; }

        [JsonProperty("documents")]
        public List<DocumentDTO> Documents { get; set; }
    }

    public class CreatedDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ReviewDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }
    }

    public class DocumentDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("filePath")]
        public string FilePath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
