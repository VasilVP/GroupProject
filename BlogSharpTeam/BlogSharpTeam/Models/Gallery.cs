using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogSharpTeam.Models
{
    public class Gallery
    {
        public Gallery()
        {
            this.Date = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string File { get; set; }

        [Required]
        public DateTime Date { get; set; }

        
        [ForeignKey("Author_Id")]
        public ApplicationUser Author { get; set; }

        public string Author_Id { get; set; }
    }
}