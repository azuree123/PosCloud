﻿using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Repositories
{
    public interface IDineTableRepository
    {
        IEnumerable<DineTable> GetDineTables(int storeid);
        DineTable GetDineTableById(int id, int storeid);
        void AddDineTable(DineTable DineTable);
        void UpdateDineTable(int id, DineTable DineTable, int storeid);
        void DeleteDineTable(int id, int storeid);
    }
}