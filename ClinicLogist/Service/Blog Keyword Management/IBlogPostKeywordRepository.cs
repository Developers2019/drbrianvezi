using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Blog_Keyword_Management
{
   public interface IBlogPostKeywordRepository
    {
        Table_BlogPostKeyword GetById(int id);


        List<Table_BlogPostKeyword> GetAll();
        void Insert(Table_BlogPostKeyword model);
        void Update(Table_BlogPostKeyword model);
        void Delete(Table_BlogPostKeyword model);
        IEnumerable<Table_BlogPostKeyword> Find(Func<Table_BlogPostKeyword, bool> predicate);
    }
}
