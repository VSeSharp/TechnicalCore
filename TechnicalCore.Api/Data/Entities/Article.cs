using System;
using System.ComponentModel.DataAnnotations;

namespace TechnicalCore.Api.Data.Entities
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset PostedOn { get; set; }
    }
}
