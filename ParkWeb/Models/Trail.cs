using ParkApi.Utils;
using Services.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkWeb.Models
{
    public class Trail
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Distance { get; set; }
        public double Elevation { get; set; }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
        public NationalParkDTO NationalPark { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
