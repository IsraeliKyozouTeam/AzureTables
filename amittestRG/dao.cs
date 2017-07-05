using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace amittestRG
{
    public class dao
    {
        public testdb1.FunctionsTreeDataTable GetData()
        {
            testdb1.FunctionsTreeDataTable data = new testdb1.FunctionsTreeDataTable();
            testdb1TableAdapters.FunctionsTreeTableAdapter adap = new testdb1TableAdapters.FunctionsTreeTableAdapter();
            testdb1 ds = new testdb1();
            adap.Fill(data);
            return data;
        }

        public DataTable GetDataTable(string query)
        {
            DataTable d = new DataTable();
            //exec query
            return d;
        }

    }

}
