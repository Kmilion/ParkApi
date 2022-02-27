using ParkApi.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkApi.Controllers.NationalParkController.ViewModels
{
    public class TrailReq
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
    }
}

