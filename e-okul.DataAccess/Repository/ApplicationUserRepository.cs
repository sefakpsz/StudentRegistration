using e_okul.DataAccess.Data;
using e_okul.DataAccess.Repository.IRepository;
using e_okul.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_okul.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser obj)
        {
            _db.Update(obj);
        }
    }
}
