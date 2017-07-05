using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace amittestRG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - write; 2 - read; 3 - delete; Enter action number:");
            int action = int.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    Write();
                    break;
                case 2:
                    Read();
                    break;
                case 3:
                    Delete();
                    break;
            }
        }
        public static void Read()
        {
            WriteToFile wf = new WriteToFile();
            while (true)
            {
                Console.WriteLine("Enter Row key:");
                wf.ReadFromTable(Console.ReadLine());
                if (Console.ReadKey().Key == ConsoleKey.Escape) return;
            }
        }
        public static void Write()
        {
            WriteToFile wf = new WriteToFile();
            wf.writeData1();
        }
        public static void Delete()
        {
            WriteToFile wf = new WriteToFile();
            wf.DeleteLogsNotFromLast30Days();
        }
    }
}
