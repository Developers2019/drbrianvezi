using System.Collections.Generic;

namespace ClinicLogist.Models
{
    public interface IGenricInterface
    {
        object GetById(int id);
        List<object> GetAll();
        void Insert(object model);
        void Update(object model);
        void Delete(int id);
    }
}
