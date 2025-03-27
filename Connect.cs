using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormTestConnect
{
    class Connect
    {
        public string ConnectToDatabase()
        {
            string connectionString = "Server=packhaidb.c1mvknt9lqgc.ap-southeast-1.rds.amazonaws.com;Database=PackhaiWMS_dev;User Id=rootpackhairds;Password='Dj;v^pR~:xR6>+CK'";
            return connectionString;
        }
    }
}
