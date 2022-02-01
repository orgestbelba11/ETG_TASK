using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETG_TASK.Models;

namespace ETG_TASK.Models.ViewModels
{
    //ViewModel to pass 2 databases at SinglePost view and a string UserComment to use for sending the comment string from view to action.
    public class BlogWithComment
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Comments> Comments { get; set; }
        public string UserComment { get; set; }
    }
}
