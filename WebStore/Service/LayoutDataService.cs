using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Service
{
    public interface ILayoutDataService
    {
        int GetProductCount();
    }
    public class LayoutDataService : ILayoutDataService
    {
        public int GetProductCount()
        {
            throw new NotImplementedException();
        }
    }
}
