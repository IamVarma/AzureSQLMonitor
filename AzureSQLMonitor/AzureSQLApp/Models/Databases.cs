﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSQLApp.Models
{
    public class Databases
    {


        public string DatabaseName
        {
            get;
            set;

        }


        public string DatabaseState
        {
            get;
            set;

        }

        public string DatabaseSlo
        { get; set; }


    

        public string AccessDesc
        { get; set; }


      

        public string IsEncrypt
        { get; set; }

       
        public string IsReadOnly
        { get; set; }

       
       
        public string DatabaseSize
        {
            get;
            set;
        }

        public string Greenvisibility
        {
            get;
            set;
        }

        public string Redvisibility
        {
            get;
            set;
        }

    }
}
