using e_okul.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_okul.DataAccess.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        void Update(Student obj);
    }
}
