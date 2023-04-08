using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFBlogsPosts.Models
{
    public class Blog
    {
        public int BlogId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
