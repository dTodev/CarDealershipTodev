﻿namespace CarDealership.Models.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int BrandId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
