using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_okul.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IStudentRepository Student { get; }
        public IApplicationUserRepository ApplicationUser { get; }
        public ILectureRepository Lecture { get; }
        public ILectureNameRepository LectureName { get; set; }
        public void Save();
    }
}
