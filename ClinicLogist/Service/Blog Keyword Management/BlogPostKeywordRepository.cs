using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Blog_Keyword_Management
{
    public class BlogPostKeywordRepository : IBlogPostKeywordRepository, IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_BlogPostKeyword> _blogRepository;

        public BlogPostKeywordRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _blogRepository = new RepositoryService<Table_BlogPostKeyword>(_datacontext);

        }
        public void Delete(Table_BlogPostKeyword model)
        {
            _blogRepository.Delete(model);
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public IEnumerable<Table_BlogPostKeyword> Find(Func<Table_BlogPostKeyword, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Table_BlogPostKeyword> GetAll()
        {
            return _blogRepository.GetAll().ToList();
        }

        public Table_BlogPostKeyword GetById(int id)
        {
            return _blogRepository.GetById(id);
        }


        public void Insert(Table_BlogPostKeyword model)
        {
            _blogRepository.Insert(model);
        }

        public void Update(Table_BlogPostKeyword model)
        {
            _blogRepository.Update(model);
        }
    }
}