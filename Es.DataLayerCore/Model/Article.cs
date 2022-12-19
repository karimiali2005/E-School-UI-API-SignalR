using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.Model
{
    public partial class Article
    {
        [Key]
        [Column("ArticleID")]
        public int ArticleId { get; set; }
        [Required]
        [StringLength(50)]
        public string ArticlePic { get; set; }
        [Required]
        [StringLength(100)]
        public string ArticleTitle { get; set; }
        [Required]
        public string ArticleBody { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ArticleCreateDate { get; set; }
        public int ArticleCountor { get; set; }
        [Required]
        [StringLength(50)]
        public string CreateUser { get; set; }
        public int ArticleStudyTime { get; set; }
    }
}
