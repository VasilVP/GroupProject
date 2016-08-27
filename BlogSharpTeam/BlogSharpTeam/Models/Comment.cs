using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogSharpTeam.Models
{
    public class Comment
    {

        public Comment()
        {
            this.Date = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Post Post { get; set; }
        
        public ApplicationUser Author { get; set; }

        [Required]
        public DateTime Date { get; set; }


    }
}