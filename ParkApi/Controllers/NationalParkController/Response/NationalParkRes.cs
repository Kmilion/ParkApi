using System;
using System.ComponentModel.DataAnnotations;

namespace ParkApi.Controllers.NationalParkController.ViewModels
{
    public class NationalParkRes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime Established { get; set; }
        public byte[] Picture { get; set; }
    }
}
