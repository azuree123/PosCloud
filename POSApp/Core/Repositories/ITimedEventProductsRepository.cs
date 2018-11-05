﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ITimedEventProductsRepository
    {
        IEnumerable<TimedEventProducts> GeTimedEventProducts(int storeId);
        TimedEventProducts GetTimedEventsById(int id, int storeId);
        void AddTimedEventProducts(TimedEventProducts tep);
        void UpdateTimedEventProducts(int id, TimedEventProducts tep, int storeId);
        void DeleteTimedEventProducts(int id, int storeId);
    }
}