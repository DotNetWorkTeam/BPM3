using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.PowerManage
{
    public class PowerUserOrganizationRepository : Repository<Power_UserOrganization>
    {
        DataContext _db;
        public PowerUserOrganizationRepository(DataContext db) :base (db)
        {
            _db = db;
        }
    
}
}
