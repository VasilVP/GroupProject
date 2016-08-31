using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSharpTeam.Models.Comments
{
    public class AddComment
    {
        public AddComment()
        {
            this.Date = DateTime.Now;
        }
        public int PostId { get; set; }

        public string Text { get; set; }

        public string AuthorId  { get; set; }

        [Required]
        
        public DateTime Date { get; set; }
    }
}