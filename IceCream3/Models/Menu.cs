using System;
using System.ComponentModel.DataAnnotations;

namespace IceCream3.Models
{
    public class Menu
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataAdded { get; set; }

    }
}
