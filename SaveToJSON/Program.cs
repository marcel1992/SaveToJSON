using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SaveToJSON
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<TimeTable> lstTimeTable = new List<TimeTable>();
            string fileName = "timeTable.json";
            string pathToJSON = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}");//$@"D:\{fileName}";

            if (File.Exists(pathToJSON))
            {
                //read from json file
                using (var streamReader = new StreamReader(pathToJSON))
                {
                    //convert json to a specific class, in this case TimeTable class
                    lstTimeTable = await JsonSerializer.DeserializeAsync<List<TimeTable>>(streamReader.BaseStream);
                }
            }

            if (lstTimeTable.Any())//an existing file exists
            {

            }
            else
            {
                var monday = new TimeTable
                {
                    Name = "Luni",
                    Date = DateTime.Today
                };
                var mondayCourse1 = new TimeTableProgram
                {
                    Name = "Romana",
                    Duration = 2
                };
                monday.Courses.Add(mondayCourse1);
                var mondayCourse2 = new TimeTableProgram
                {
                    Name = "Info",
                    Duration = 1.5d//daca ii pui d stie ca e double, daca ii pui m stie ca e decimal
                };
                monday.Courses.Add(mondayCourse2);


                var tuesday = new TimeTable
                {
                    Name = "Marti",
                    Date = DateTime.Today.AddDays(1)
                };

                lstTimeTable.Add(monday);
                lstTimeTable.Add(tuesday);
            }
            //save json to file
            using (StreamWriter file = File.CreateText(pathToJSON))
            {
                await JsonSerializer.SerializeAsync(file.BaseStream, lstTimeTable);
            }

            Console.Read();
        }

        //TimeTable => orar
        class TimeTable
        {
            public string Name { get; set; }
            public List<TimeTableProgram> Courses { get; set; } = new List<TimeTableProgram>();
            public DateTime Date { get; set; }
        }

        // Luni poate sa contina o lista de astfel de materii
        // adica luni poate avea romana, matematica, fizica
        class TimeTableProgram
        {
            public string Name { get; set; }// romana sau matematica sau fizica
            public double Duration { get; set; }//2 ore sau 3 ore
        }
    }
}
