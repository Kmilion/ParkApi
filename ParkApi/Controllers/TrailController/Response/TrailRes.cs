﻿using ParkApi.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkApi.Controllers.NationalParkController.ViewModels
{
    public class TrailRes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public double Elevation { get; set; }
        public string Difficulty { get; set; }
        public DateTime DateCreated { get; set; }
        public NationalParkRes NationalPark { get; set; }
    }
}
