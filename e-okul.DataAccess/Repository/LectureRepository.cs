﻿using e_okul.DataAccess.Data;
using e_okul.DataAccess.Repository.IRepository;
using e_okul.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_okul.DataAccess.Repository
{
    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        private readonly ApplicationDbContext _db;
        public LectureRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public void Update(Lecture obj)
        {
            _db.Update(obj);
        }
    }
}
