using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amittestRG
{
    public struct Column
    {

        //public string columnName;
        string data;
        Type type;

        public Column(string _data, Type dataOriginalType) 
        {
            //columnName = _ColumnName;
            data = _data;
            type = dataOriginalType;
        }


    }
}
