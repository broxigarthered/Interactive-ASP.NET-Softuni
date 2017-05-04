using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interactive.Models.EntityModels
{
    public class Post
    {
        public Post()
        {
            this.Date = DateTime.Now;
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("Author_Id")]
        public virtual ApplicationUser Author { get; set; }

        public string Author_Id { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}