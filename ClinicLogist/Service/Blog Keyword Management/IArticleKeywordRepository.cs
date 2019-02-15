using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Blog_Keyword_Management
{
    public interface IArticleKeywordRepository
    {
        Table_ArticleKeyword GetById(int id);
        List<Table_ArticleKeyword> GetAll();
        void Insert(Table_ArticleKeyword model);
        void Update(Table_ArticleKeyword model);
        void Delete(Table_ArticleKeyword model);
        IEnumerable<Table_ArticleKeyword> Find(Func<Table_ArticleKeyword, bool> predicate);
    }
}
