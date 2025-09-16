using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Data.Json
{
    public class TableData
    {
        [JsonProperty("Card")]
        public List<Card> Card { get; set; }

    }
    public class Card
    {
        [JsonProperty("cardId")]
        public int CardId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("desc2")]
        public string Desc2 { get; set; }
    }


}