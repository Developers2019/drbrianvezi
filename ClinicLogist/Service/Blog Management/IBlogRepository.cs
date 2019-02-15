using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Blog_Management
{
    public interface IBlogRepository
    {
        Table_BlogPost GetById(int id);

  
        List<Table_BlogPost> GetAll();
        void Insert(Table_BlogPost model);
        void Update(Table_BlogPost model);
        void Delete(Table_BlogPost model);
        IEnumerable<Table_BlogPost> Find(Func<Table_BlogPost, bool> predicate);
    }
}
