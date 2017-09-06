using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp3.Interfaces
{
    public interface IConfig
    {
        string DirectoryDB { get;  }

        ISQLitePlatform  Platform{ get;  }
    }
}
