using e_okul.DataAccess.Data;
using e_okul.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_okul.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Student = new StudentRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Lecture = new LectureRepository(_db);
            LectureName = new LectureNameRepository(_db);
        }
        public IStudentRepository Student { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ILectureRepository Lecture { get; private set; }
        public ILectureNameRepository LectureName { get; set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
