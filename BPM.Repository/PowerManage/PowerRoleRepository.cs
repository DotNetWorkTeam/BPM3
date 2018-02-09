using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.PowerManage
{
    public class PowerRoleRepository : Repository<Power_Role>
    {
        DataContext _db;
        public PowerRoleRepository(DataContext db) :base (db)
        {
            _db = db;
        }
    }
}
