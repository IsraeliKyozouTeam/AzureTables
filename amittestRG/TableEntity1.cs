using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amittestRG
{
    public class TableEntity1 : TableEntity
    {
        private int id;
        private Int16 level;
        private int calledFuncId;
        private string funcCode;
        private string fileName;
        private int lineNumber;
        private string funcName;
        private string pattern;
        private string freeText1;
        private string freeText2;
        private string freeText3;
        private string canBeReWriten;
        private string fixedFunction;
        Int16 runId;

        public TableEntity1(int id,Int16 level,int calledFuncId,string funcCode, string fileName, int lineNumber,string funcName,string pattern, string freeText1, string freeText2, string freeText3,string canBeReWriten,string fixedFunction,Int16 runId)
        {
            this.id = id;
            this.level = level;
            this.calledFuncId = calledFuncId;
            this.funcCode = funcCode;
            this.fileName = fileName;
            this.lineNumber = lineNumber;
            this.funcName = funcName;
            this.pattern = pattern;
            this.freeText1 = freeText1;
            this.freeText2 = freeText2;
            this.freeText3 = freeText3;
            this.canBeReWriten = canBeReWriten;
            this.fixedFunction = fixedFunction;
            this.runId = runId;
        }
        public TableEntity1(){ }
        public TableEntity1(int id, Int16 level, int calledFuncId)
        {
            this.id = id;
            this.level = level;
            this.calledFuncId = calledFuncId;
        }
        public string FuncCode
        {
            get { return funcCode; }
            set { funcCode = value; }
        }
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public string FuncName
        {
            get { return funcName; }
            set { funcName = value; }
        }
        public string Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }
        public string FreeText1
        {
            get { return freeText1; }
            set { freeText1 = value; }
        }
        public string FreeText2
        {
            get { return freeText2; }
            set { freeText2 = value; }
        }
        public string FreeText3
        {
            get { return freeText3; }
            set { freeText3 = value; }
        }
        public string CanBeReWriten
        {
            get { return canBeReWriten; }
            set { canBeReWriten = value; }
        }
        public string FixedFunction
        {
            get { return fixedFunction; }
            set { fixedFunction = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int CalledFuncId
        {
            get { return calledFuncId; }
            set { calledFuncId = value; }
        }
        public Int16 Level
        {
            get { return level; }
            set { level = value; }
        }
        public Int16 RunId
        {
            get { return runId; }
            set { runId = value; }
        }

    }
}
