using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;


namespace amittestRG
{
    public class TableEntity2 : TableEntity
    {

        public List<Column> columnList { get; protected set; }

        public TableEntity2(List<Column> _columnList)
        {

            this.columnList = _columnList;

        }
    }
}
