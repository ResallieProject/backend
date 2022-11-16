﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resallie.Models
{
    public abstract class Model
    {
        public int Id { get; set; }
        [Timestamp] public DateTime CreatedAt { get; set; }

        [Timestamp, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}