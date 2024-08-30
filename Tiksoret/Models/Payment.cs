using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace Tiksoret.Models
{   [Table("tblPayment")]
    public class Payment
    {
        [Key]
        public int PID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string ResID { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public string Digits { get; set; }
        [Required]
        public int ExD { get; set; }
        [Required]
        public int ExM { get; set; }
        [Required]
        public float Price { get; set; }

    }
}