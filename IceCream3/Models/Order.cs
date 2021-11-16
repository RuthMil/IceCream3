using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IceCream3.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Time Ordered")]
        public DateTime TimeOrdered { get; set; }

        public string Flavor { get; set; }

        public int? Quantity {get; set;}

        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        [DisplayName("House Number")]
        public int HouseNum { get; set; }

        public int TemperatureId { get; set; }

    }
}
