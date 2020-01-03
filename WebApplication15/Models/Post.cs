using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication15.Models;

namespace WebApplication15.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ScaffoldColumn(false)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        [StringLength(100)]
        public string  Subject { get; set; }
        [Required]
        [StringLength(500)]
        public string  Body { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [StringLength(50)]
        public string AuthorName  { get; set; }
        [ScaffoldColumn(false)]
        public int Readers { get; set; }

        public string  UrlImage { get; set; }
    }
}
