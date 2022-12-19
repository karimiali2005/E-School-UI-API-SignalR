
using System.Collections.Generic;

namespace Es.DataLayerCore.DTOs.Article
{
    public class ArticleViewModel
    {
        public Model.Article Article { get; set; }
        public List<ArticleShowTopResult> ArticleShowTops { get; set; }
    }
}
