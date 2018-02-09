using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.PowerManage
{
    public class PowerUserRoleRepository : Repository<Power_UserRole>
    {
        DataContext _db;
        public PowerUserRoleRepository(DataContext db) :base (db)
        {
            _db = db;
        }
    }
}
