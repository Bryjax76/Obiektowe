using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace proj_zal
{
    class Program
    {
        static void Main(string[] args)
        {
            //ImportData();
            //GettGames("Files\\SteamCharts.txt");
            //Console.WriteLine("==============================================================================================");
            //SortGames();
            //Console.WriteLine("==============================================================================================");
            //Dota2();
            //Console.WriteLine("==============================================================================================");
            //Gain();
            //Console.WriteLine("==============================================================================================");
            //Search();
            //Console.WriteLine("==============================================================================================");
            //Compare();
            //Console.WriteLine("==============================================================================================");
            //AddData();
            Console.ReadKey();
        }
        public static void ImportData()
        {
            List<Games> tSteamChart = GettGames("Files\\SteamCharts.txt");
            List<Games> all = new List<Games>();
            all.AddRange(tSteamChart);
            using (var db = new GamesDbContext())
            {
                db.tSteamChart.AddRange(all);
                db.SaveChanges();
            }
        }
        public static List<Games> GettGames(string path)
        {
            List<Games> returnValue = null;
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Games>();
                    returnValue = new List<Games>();
                    foreach (var f in records)
                    {
                        returnValue.Add(f);
                    }
                }
            }
            return returnValue;
        }
        public static void AddData()
        {
            using (var db = new GamesDb())
            {
                var game = new Games()
                {
                    gamename = "TEST",
                    year = 6666,
                    month = "TEST",
                    avg = 666666,
                    gain = "66.66",
                    peak = 666666,
                    avg_peak_perc = "66.6666%"
                };
                db.tSteamChart.Add(game);
                db.SaveChanges();
            }
        }
        public static void SortGames()
        {
            using (var db = new GamesDbContext())
            { 
                List<Games> sortList = db.tSteamChart.Where(p => p.peak >= 1000000).OrderByDescending(p => p.peak).ToList();
                Console.WriteLine("Gry przekraczające liczbę graczy = 1.000.000 w danym roku i miesiącu");
                foreach (var i in sortList)
                {
                    Console.WriteLine(i.gamename + " || " + i.year + " - " + i.month + "|| Liczba graczy: " + i.peak);
                }
            }
        }
        public static void Dota2()
        {
            using (var db = new GamesDbContext())
            {
                List<Games> date1 = db.tSteamChart.Where(p => p.gamename == "Dota 2").ToList();
                Console.WriteLine("O ile wzrosła liczba graczy w Dota 2 z 2012r. do 2020r.");
                int x = 0;
                foreach (var i in date1)
                {
                    x += i.peak;
                }
                Console.WriteLine(x / date1.Count);
            }
        }
        public static void Gain()
        {
            using (var db = new GamesDbContext())
            {
                Console.WriteLine("Podaj pełną nazwę gry");
                string fullName = Console.ReadLine();
                List<Games> gain = db.tSteamChart.Where(p => p.gamename == fullName).ToList();             
                foreach(var i in gain)
                {
                    if (fullName == i.gamename)
                    {
                        Console.WriteLine($"Wykaz maksymalnej wartości przybytych graczy po miesiącu dla {fullName}.");
                        Console.WriteLine(i.gain + " || " + i.year + " - " + i.month);
                    }
                    else
                    {
                        Console.WriteLine("Podałeś złą nazwę lub nie ma takiej gry");
                        break;
                    }
                }
            }
        }
        public static void Search()
        {
            using (var db = new GamesDbContext())
            {
                Console.WriteLine("Wyszukaj swoją grę");
                string fullName = Console.ReadLine();               
                List<Games> gain = db.tSteamChart.Where(p => p.gamename == fullName).ToList();
                foreach (var i in gain)
                {
                    if (fullName == i.gamename)
                    {
                        Console.WriteLine($"Czy pokazać wszystkie dane gry {fullName}?");
                        Console.WriteLine("Tak / Nie");
                        string answer = Console.ReadLine();
                        if (answer == "Tak")
                        {
                            Console.WriteLine(i.gamename + " || " + i.year + " || " + i.month + " || " + i.avg + " || " + i.gain + " || " + i.peak + " || " + i.avg_peak_perc);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("( ´･･)ﾉ(._.`)");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Gra o nazwie {fullName} nie istnieje...");
                        break;
                    }
                }
            }
        }
        public static void Compare()
        {
            using (var db = new GamesDbContext())
            {
                Console.WriteLine("Porównanie gier");
                Console.WriteLine("Podaj pełną nazwę gry nr 1:");
                string fullName1 = Console.ReadLine();
                Console.WriteLine("Podaj pełną nazwę gry nr 2:");
                string fullName2 = Console.ReadLine();
                List<Games> gain1 = db.tSteamChart.Where(p => p.gamename == fullName1).OrderByDescending(p => p.year).ToList();
                List<Games> gain2 = db.tSteamChart.Where(p => p.gamename == fullName2).OrderByDescending(p => p.year).ToList();
                Console.WriteLine($"Porównanie gry {fullName1} oraz {fullName2}.");
                foreach (var i in gain1)
                {
                    if (fullName1 == i.gamename)
                    {
                        Console.WriteLine(i.gamename + " ====> Liczba graczy: " + i.peak + " || " + i.year + " - " + i.month);
                    }
                    else
                    {
                        Console.WriteLine("Podałeś złą nazwę lub nie ma takiej gry");
                        break;
                    }
                    foreach (var x in gain2)
                        {
                            Console.WriteLine("=========================================");
                            if (fullName2 == i.gamename)
                            {
                                
                                Console.WriteLine(x.gamename + " ====> Liczba graczy: " + x.peak + " || " + x.year + " - " + x.month);                      
                            }
                            else
                            {
                                Console.WriteLine("Podałeś złą nazwę lub nie ma takiej gry");
                                break;
                            }
                        }
                    
                }
            }
        }
    }
}
