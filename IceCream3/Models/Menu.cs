using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IceCream3.Models
{
    public class Menu
    {
        public int Id { get; set; }

        public string Flavor { get; set; }

        public string Description { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }

        public string ImageUrl { get; set; }

        public float Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

    }
}
