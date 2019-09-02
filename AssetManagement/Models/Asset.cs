using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AssetManagement.Models
{
    public class Asset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public Group Group { get; set; }
        public float UnitPrice { get; set; }

        public float TotalValue
        {
            get => Count * UnitPrice;
        }

        public Asset()
        {
            Id = "";
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Asset Deserialize(string serialized)
        {
            var asset = JsonConvert.DeserializeObject<Asset>(serialized);
            return asset;
        }
    }
}