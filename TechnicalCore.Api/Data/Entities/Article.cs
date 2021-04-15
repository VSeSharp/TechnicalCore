using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalCore.Api.Data.Entities
{
    [Table("Article")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset PostedOn { get; set; }
        
        [ForeignKey("ArticleId")]

        public List<ArticleReview> ArticleReviews { get; set; }
    }
}
