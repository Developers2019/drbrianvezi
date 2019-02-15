using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Blog_Keyword_Management
{
    public class ArticleKeywordRepository : IArticleKeywordRepository, IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_ArticleKeyword> _blogRepository;

        public ArticleKeywordRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _blogRepository = new RepositoryService<Table_ArticleKeyword>(_datacontext);

        }
       

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public IEnumerable<Table_ArticleKeyword> Find(Func<Table_ArticleKeyword, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Table_ArticleKeyword> GetAll()
        {
            return _blogRepository.GetAll().ToList();
        }

        public Table_ArticleKeyword GetById(int id)
        {
            return _blogRepository.GetById(id);
        }

        public void Insert(Table_ArticleKeyword model)
        {
            _blogRepository.Insert(model);
        }

        public void Update(Table_ArticleKeyword model)
        {
            _blogRepository.Update(model);
        }
        public void Delete(Table_ArticleKeyword model)
        {
            _blogRepository.Delete(model);
        }
    }
}