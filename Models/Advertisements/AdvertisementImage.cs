﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Resallie.Models.Advertisements
{
    public class AdvertisementImage : Model
    {
        [Required]
        public string Path { get; set; }
        public /*u*/ int Order { get; set; }

        [JsonIgnore]
        [ForeignKey("Advertisement")]
        [Required] public int AdvertisementId { get; set; }
    }
}