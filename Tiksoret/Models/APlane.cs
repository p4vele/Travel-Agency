using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tiksoret.Models
{
    public class APlane
    { 
        [Key]
        public int PlaneID { get; set; }

        [Required(ErrorMessage = "Aeroplane name")]
        [Display(Name = "Aeroplane name")]
        public string APlaneName { get; set; }

        [Required(ErrorMessage = "Seat Capacity ")]
        [Display(Name = "Seat Capacity")]
        public int SeatCapacity { get; set; }

        [Required(ErrorMessage = "Price ")]
        [Display(Name = "Price")]
        public float Price { get; set; }

        public virtual ICollection<TicketReserve_tbl> TicketReserve_tbls { get; set; }
    }
}