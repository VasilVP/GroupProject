using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSharpTeam.Models.Comments
{
    public class AddComment
    {
        public int PostId { get; set; }

        public string Text { get; set; }

        public string AuthorId  { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}