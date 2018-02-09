using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.PowerManage
{
    public class PowerStationRepository : Repository<Power_Stations>
    {
        DataContext _db;
        public PowerStationRepository(DataContext db) :base (db)
        {
            _db = db;
        }
    }
}
