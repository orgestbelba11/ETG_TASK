using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETG_TASK.Models
{
    public class Blog
    {
        [Key]
        public int BlogID { get; set; }
       
        [Required(ErrorMessage = "Category is required!")]
        public string Category { get; set; }
        
        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Description is required!")]
        public string Text { get; set; }
        
        [Required(ErrorMessage = "Image is required!.")]
        public string ImagePath { get; set; }

        [Required]
        public string Admin { get; set; }
    }
}
