using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Moudou.WordHelper
{
    internal class JsonReader
    {
        /// <summary> parse all json file into list </summary>
        /// <param name="propertyJsonPath"></param>
        /// <returns></returns>
        internal static CustomProperty[] readAllJSON(string propertyJsonPath)
        {
            List<string> jsonContentList = new List<string>();
            FileAttributes attr = File.GetAttributes(propertyJsonPath);



            // is directory
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                jsonContentList.AddRange(JsonReader.readDirectoryJsonFile(propertyJsonPath));
            }
            else   // is file
            {
                jsonContentList.Add(JsonReader.readJsonFile(propertyJsonPath));
            }



            List<CustomProperty> cpList = new List<CustomProperty>();


            foreach (string JsonContent in jsonContentList)
            {
                CustomProperty[] cpArr = JsonConvert.DeserializeObject<CustomProperty[]>(JsonContent);
                cpList.AddRange(cpArr);
            }


            return cpList.ToArray();
        }



        /// <summary> read all JSON file in directory </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        private static string[] readDirectoryJsonFile(string directoryPath)
        {
            if(!Directory.Exists( directoryPath))
                return new string[] { };


            List<string> jsonContentList = new List<string>();

            foreach (string filePath in Directory.EnumerateFiles(directoryPath, "*.json"))
            {
                string fileContent = File.ReadAllText(filePath);
                jsonContentList.Add(fileContent);
            }

            return jsonContentList.ToArray();
        }


        /// <summary> read a JSON file </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string readJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
                return string.Empty;


            string fileContent = File.ReadAllText(filePath);
            return fileContent;
        }
    }
}
