using Session1Try6.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Session1Try6.Classes
{
    public static class Helper
    {
        public static SessionOneTry5Entities Db = new SessionOneTry5Entities();

        public static DispatcherTimer GlobalTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(60)};

        public static int GlobalCount = 0;
        public static bool ExitFlag = false;
             
    }
}
