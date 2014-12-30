using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSQLApp.Models
{
    public class Sysinfo
    {
            string server_name;
            string login_name;
            string version_info;
            string datetime_info;

            
            public string ServerName
            {
                get { return server_name; }
                set { server_name = value; }

            }

            
            public string LoginName
            {
                get { return login_name; }
                set
                {
                    login_name = value;
                }

            }

            public string VersionInfo
            {
                get { return version_info; }
                set
                {
                    version_info = value;
                }

            }

            public string DatetimeInfo
            {
                get { return datetime_info; }
                set
                {
                    datetime_info = value;
                }

            }

        }
    }

