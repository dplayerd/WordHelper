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
        /// <param name="InputPath"></param>
        /// <returns></returns>
        internal static CustomProperty[] readAllJSON(string InputPath)
        {
            string[] jsonFilePathArr = FileHelper.getFilePaths(InputPath, FileType.JSON);


            List<CustomProperty> cpList = new List<CustomProperty>();


            foreach (string filePath in jsonFilePathArr)
            {
                string fileContent = File.ReadAllText(filePath);
                CustomProperty[] cpArr = JsonConvert.DeserializeObject<CustomProperty[]>(fileContent);
                cpList.AddRange(cpArr);
            }


            return cpList.ToArray();
        }
    }
}
