using System;
using System.ComponentModel.DataAnnotations;

namespace IceCream3.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeOrdered { get; set; }

        public string Flavor { get; set; }

        public int? Quantity {get; set;}

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int HouseNum { get; set; }

        public int TemperatureId { get; set; }

    }
}
