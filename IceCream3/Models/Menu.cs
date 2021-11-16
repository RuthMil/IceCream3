using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IceCream3.Controllers;

namespace IceCream3.Models
{
    public class Menu
    {
        public int Id { get; set; }

        [Required]
        public string Flavor { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; }
        
    }
}
