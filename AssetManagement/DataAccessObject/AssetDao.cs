using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AssetManagement.Models;
using Newtonsoft.Json;

namespace AssetManagement.DataAccessObject
{
    public class AssetDao
    {
        private static readonly byte[] LockByte = new byte[0];

        public static void WriteToFile(IEnumerable<Asset> assets)
        {
            lock (LockByte)
            {
                using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/Data/data.txt"), false))
                {
                    foreach (var a in assets)
                    {
                        sw.WriteLine(JsonConvert.SerializeObject(a));
                    }
                }
            }
            
        }

        //read from file, csv, deserialize
        public static IEnumerable<Asset> ReadFromFile()
        {
            lock (LockByte)
            {
                using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/Data/data.txt"), true))
                {
                    string line;
                    var data = new List<Asset>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            data.Add(JsonConvert.DeserializeObject<Asset>(line));
                        }
                        catch
                        {
                            //ignored
                        }

                    }

                    return data;
                }
            }
        }
    }
}