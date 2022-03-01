using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ParkWeb.Models.ViewModels
{
    public class TrailsVM
    {
        public IEnumerable<SelectListItem> NationalParkList { get; set; }
        public Trail Trail { get; set; }
    }
}
