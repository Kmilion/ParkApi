using ParkApi.Utils;
using Services.DTOs;
using System;

namespace ParkWeb.Models
{
    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public double Elevation { get; set; }
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; }
        public NationalParkDTO NationalPark { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
