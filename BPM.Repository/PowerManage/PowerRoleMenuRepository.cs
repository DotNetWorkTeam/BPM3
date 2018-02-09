using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.PowerManage
{
    public class PowerRoleMenuRepository : Repository<Power_RoleMenu>
    {
        DataContext _db;
        public PowerRoleMenuRepository(DataContext db) :base (db)
        {
            _db = db;
        }
    }
}
