using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AssetManagement.Models
{
    public class Asset
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public int Count { get; set; }

        [ValidEnumValue]
        public Group Group { get; set; }

        [Range(0, double.MaxValue)]
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

    public class ValidEnumValueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Type enumType = value.GetType();
            bool valid = Enum.IsDefined(enumType, value);
            if(!valid)
                return new ValidationResult($"{value} is not a valid value for type {enumType.Name}");

            return ValidationResult.Success;
        }
    }
}