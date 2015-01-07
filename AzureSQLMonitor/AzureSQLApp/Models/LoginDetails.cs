using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureSQLApp.Common;
using System.ComponentModel;

public class LoginDetails : INotifyPropertyChanged
{

    private string _servername;

    private string _loginid;

    private string _password;



    public event PropertyChangedEventHandler PropertyChanged;



    public string ServerName
    {

        get
        {

            return _servername;

        }

        set
        {

            if (_servername != value)
            {

                _servername = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs("ServerName"));

                }

            }

        }

    }



    public string LoginId
    {

        get
        {

            return _loginid;

        }

        set
        {

            if (_loginid != value)
            {

                _loginid = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs("loginId"));

                }

            }

        }

    }



    public string Password
    {

        get
        {

            return _password;

        }

        set
        {

            if (_password != value)
            {

                _password = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs("Password"));

                }

            }

        }

    }

}