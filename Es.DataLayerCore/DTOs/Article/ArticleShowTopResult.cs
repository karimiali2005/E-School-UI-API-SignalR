using System;

namespace Es.DataLayerCore.DTOs.Article
{
    public class ArticleShowTopResult
    {
        public int ArticleID { get; set; }
        public string ArticlePic { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleBody { get; set; }
        public DateTime ArticleCreateDate { get; set; }
        public int ArticleCountor { get; set; }
        public string CreateUser { get; set; }
        public int ArticleStudyTime { get; set; }
    }

}
