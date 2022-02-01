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
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }
        
        [Required(ErrorMessage = "Comment required!")]
        public string Comment { get; set; }

        [Required]
        public string Commenter { get; set; }

        [Display(Name = "Blog")]
        public int BlogID { get; set; }
        [ForeignKey("BlogID")]
        public virtual Blog Blog { get; set; }
    }
}
