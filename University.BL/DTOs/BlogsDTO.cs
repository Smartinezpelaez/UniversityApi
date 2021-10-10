using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace University.BL.DTOs
{
    public class BlogsDTO
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("BloggerName")]
        public string BloggerName { get; set; }

        [JsonProperty("Posts")]
        public string Posts { get; set; }

    }

        public class PostDTO
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("DateCreate")]
            public DateTime DateCreate { get; set; }

            [JsonProperty("Content")]
            public string Content { get; set; }

        }
    
}
