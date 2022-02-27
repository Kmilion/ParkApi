using DataAccess.Models;
using ParkApi.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class TrailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; }
        public NationalParkDTO NationalPark { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
