using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeyLapse.Session
{
    [Table]
    public class SessionProfile
    {
        [Column(IsPrimaryKey = true,
            IsDbGenerated = false,
            DbType = "NVARCHAR(50) NOT NULL",
            CanBeNull = false,
            AutoSync = AutoSync.OnInsert)]
        public string Key { get; set; }

        [Column]
        public string Value { get; set; }
    }
}
