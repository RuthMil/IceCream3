using System;
using System.ComponentModel.DataAnnotations;

namespace IceCream3.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeOrdered { get; set; }

        public string Flavor { get; set; }

        public int? Quantity {get; set;}

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNum { get; set; }

        public Temperature MeasuredTemp { get; set; }

    }
}
