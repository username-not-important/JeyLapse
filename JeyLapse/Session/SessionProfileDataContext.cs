using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeyLapse.Session
{
    public class SessionProfileDataContext : DataContext
    {
        public Table<SessionProfile> Items;

        public SessionProfileDataContext(string connectionString)
            : base(connectionString)
        {

        }
    }
}
