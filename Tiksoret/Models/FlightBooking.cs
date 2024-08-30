using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Tiksoret.Models
{
    [Table("tblFlightBook")]
    public class FlightBooking
    {
        [Key]
        public int BookingID { get; set; }

        [Required(ErrorMessage = "Costumer Name")]
        [Display(Name = "Costumer Name")]
        public string bCusName { get; set; }

        [Required(ErrorMessage = "Costumer Adress")]
        [Display(Name = "Costumer Adress")]
        public string bCusAdress { get; set; }

        [Required(ErrorMessage = "Costumer Email")]
        [Display(Name = "Costumer Email")]
        public string bCusEmail { get; set; }

        [Required(ErrorMessage = "number of seats")]
        [Display(Name = "number of seats")]
        public int bCusSeats { get; set; }

        [Required(ErrorMessage = "Phone number")]
        [Display(Name = "Phone number")]
        public string bPhoneNum { get; set; }
        [Required(ErrorMessage = "ID")]
        [Display(Name = "ID")]
        public string bCusID { get; set; }
        public float TotalPrice { get; set; }
        public int ResID { get; set; }

        public virtual TicketReserve_tbl TicketReserve_tbls { get; set; }
    }

    public class TicketReserve_tbl
    {
        [Key]
        public int ResID { get; set; }

        [Required(ErrorMessage = "From")]
        [Display(Name = "From ")]
        public string ResFrom { get; set; }

        [Required(ErrorMessage = "To")]
        [Display(Name = "To")]
        public string ResTo { get; set; }

        [Required(ErrorMessage = "Departure Date")]
        [Display(Name = "Departure Date")]
        public string ResDepDate { get; set; }

        [Required(ErrorMessage = "Flight Time")]
        [Display(Name = "Flight Time")]
        public string ResTime { get; set; }

        [Required(ErrorMessage = "Plane ID")]
        [Display(Name = "Plane ID")]
        public string PlaneID { get; set; }

        public virtual APlane Plane_tbls { get; set; }

        [Required(ErrorMessage = "seats avilable")]
        [Display(Name = "seats avilable")]
        public int PlaneSeat { get; set; }

        [Required(ErrorMessage = "Price")]
        [Display(Name = "Price")]
        public float ResTicketPrice { get; set; }

        [Required(ErrorMessage = "Plane Type")]
        [Display(Name = "Plane Type")]
        public string ResPlaneType { get; set; }

        public virtual ICollection<FlightBooking> tblFlightBookings { get; set; }
         public void changePlaneSeat(int x) { PlaneSeat = x; }
    }
   
}