using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.PowerManage
{
    public class PowerAreaRepository : Repository<Power_Area>
    {
        DataContext _db;
        public PowerAreaRepository(DataContext db) :base (db)
        {
            _db = db;
        }
    }
}
