using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface ISizeRepository
    {
        IEnumerable<Size> GetSizes(int storeId);
        Size GetSizeById(int id, int storeId);
        Size GetSizeByName(string name, int storeId);
        void AddSize(Size Size);
        void UpdateSize(int id, Size Size, int storeid);
        void DeleteSize(int id, int storeid);
    }
}