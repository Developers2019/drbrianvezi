using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Blog_Management
{
    public class BlogRepository : IBlogRepository, IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_BlogPost> _blogRepository;

        public BlogRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _blogRepository = new RepositoryService<Table_BlogPost>(_datacontext);

        }
        public void Delete(Table_BlogPost model)
        {
            _blogRepository.Delete(model);
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public IEnumerable<Table_BlogPost> Find(Func<Table_BlogPost, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Table_BlogPost> GetAll()
        {
            return _blogRepository.GetAll().ToList();
        }

        public Table_BlogPost GetById(int id)
        {
            return _blogRepository.GetById(id);
        }

 
        public void Insert(Table_BlogPost model)
        {
            _blogRepository.Insert(model);
        }

        public void Update(Table_BlogPost model)
        {
            _blogRepository.Update(model);
        }
    }
}