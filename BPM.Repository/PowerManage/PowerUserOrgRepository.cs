using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.PowerManage
{
    public class PowerUserOrgRepository : Repository<Power_UserOrg>
    {
        DataContext _db;
        public PowerUserOrgRepository(DataContext db) : base(db)
        {
            _db = db;
        }
    }
}
