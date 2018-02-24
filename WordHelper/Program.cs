using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moudou.WordHelperCore.Controller;
using Moudou.WordHelperCore.DTO;

namespace Moudou.WordHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            string wordPath;
            string propertyJsonPath;
            Program.readArgs(args, out wordPath, out propertyJsonPath);


            if (string.IsNullOrEmpty(wordPath) || string.IsNullOrEmpty(propertyJsonPath))
            {
                Console.WriteLine(" Path Error. ");
                return;
            }


            Program.Fill(wordPath, propertyJsonPath);
            Console.WriteLine(" Write completed. ");
        }


        /// <summary> find filepath from args </summary>
        /// <param name="args"> input args </param>
        /// <param name="wordPath"> out </param>
        /// <param name="propertyJsonPath"> out </param>
        private static void readArgs(string[] args, out string wordPath, out string propertyJsonPath)
        {
            wordPath = string.Empty;
            bool isReadWordPath = false;

            propertyJsonPath = string.Empty;
            bool isReadJsonPath = false;


            foreach (string arg in args)
            {
                if (isReadWordPath)
                {
                    wordPath = arg;
                    isReadWordPath = false;
                }


                if (isReadJsonPath)
                {
                    propertyJsonPath = arg;
                    isReadJsonPath = false;
                }



                if (arg.ToLower() == "-w" || arg.ToLower() == "-word")
                {
                    isReadWordPath = true;
                    isReadJsonPath = false;
                }


                if (arg.ToLower() == "-p" || arg.ToLower() == "-property")
                {
                    isReadWordPath = false;
                    isReadJsonPath = true;
                }
            }
        }


        /// <summary> call Helper </summary>
        /// <param name="wordPath"> filePath </param>
        /// <param name="propertyJsonPath"> properties </param>
        private static void Fill(string wordPath, string propertyJsonPath)
        {
            new WordHelpController().fillProperties(new InputDTO() { JsonPath = propertyJsonPath, WordPath = wordPath });


            //CustomProperty[] cpList = JsonReader.readAllJSON(propertyJsonPath);
            //string[] filePathList = WordPathReader.getWordPaths(wordPath);


            //foreach(string fileName in filePathList)
            //{ 
            //    WordPropertyHelper.setCustomProperty(fileName, cpList.ToList());
            //}
        }
    }
}
