﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkApi.Controllers.NationalParkController.ViewModels
{
    public class NationalParkReq
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public DateTime Established { get; set; }
        public byte[] Picture { get; set; }
    }
}
