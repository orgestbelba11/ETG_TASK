using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ETG_TASK.Data;

namespace ETG_TASK.Models
{
    public class Account
    {
        [Key]
        public int UserID { get; set; }
       
        [Required(ErrorMessage ="Username is required!")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
    }
}
