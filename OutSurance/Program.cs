using System;
using System.IO;
using CsvHelper;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace Outsurance
{
    class Program
    {
        static string loc;
        
        static void Main(string[] args)
        {
            GetCsvData();
        }

        static void GetCsvData()
        {
            loc = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            StreamReader sr = new StreamReader(loc + @"\Csv\data.csv");

            CsvReader csvread = new CsvReader(sr);

            IEnumerable<Data> records = csvread.GetRecords<Data>();

            List<Data> newData = new List<Data>();

            foreach (var rec in records) // Each record will be fetched and printed on the screen
            {
                newData.Add(rec);
            }

            sr.Close();

            PrintNameAndSurname(newData);

            PrintAddress(newData);
        }

        static void PrintNameAndSurname(List<Data> newData)
        {
            var outputLoc = @"\Output\NameAndSurname.txt";

            var Names = newData.OrderBy(i => i.FirstName).ThenBy(i => i.LastName);
            var NameGroup = Names.GroupBy(i => i.FirstName);
            IDictionary<dynamic, int> fNameVal = new Dictionary<dynamic, int>();

            var lastName = newData.GroupBy(i => i.LastName).OrderByDescending(i => i.Key);
            IDictionary<string, int> lNameVal = new Dictionary<string, int>();

            if (!File.Exists(loc + outputLoc))
            {
                File.Create(loc + outputLoc).Dispose();
            }

            using (StreamWriter file =
            new StreamWriter(loc + outputLoc))
            {
                foreach (var grp in NameGroup)
                {
                    fNameVal.Add(grp.Key, grp.Count());
                }

                var a = fNameVal.OrderBy(i => i.Key).ThenBy(i => i.Value);

                foreach (var key in a)
                {
                    file.WriteLine("{0}", key);
                }
                file.WriteLine();

                foreach (var grp in lastName)
                {
                    lNameVal.Add(grp.Key, grp.Count());
                }

                var b = lNameVal.OrderBy(i => i.Key).ThenBy(i => i.Value);
                foreach (var key in b)
                {
                    file.WriteLine("{0}", key);
                }
                file.WriteLine();

                foreach (var key in Names)
                {
                    file.WriteLine("{0} {1}", key.FirstName, key.LastName);
                }
            }

        }

        static void PrintAddress(List<Data> newData)
        {
            var outputLoc = @"\Output\Address.txt";

            var Addresses = newData.OrderBy(i => i.Address.Split(' ')[1]);
            var AddressGroup = Addresses.GroupBy(i => i.Address);

            if (!File.Exists(loc + outputLoc))
            {
                File.Create(loc + outputLoc).Dispose();
            }

            using (StreamWriter file =
            new StreamWriter(loc + outputLoc))
            {
                foreach (var key in AddressGroup)
                {
                    file.WriteLine("{0}", key.Key);
                }
            }

        }
    }

}




