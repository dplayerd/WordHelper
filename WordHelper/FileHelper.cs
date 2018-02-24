using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Moudou.WordHelper
{
    internal class FileHelper
    {
        internal static string[] getFilePaths(string InputPath, FileType fType)
        {
            //Filename Extension
            string Extension =
                (fType == FileType.JSON) ?
                    "*.json" :
                    "*.docx";

            
            List<string> retList = new List<string>();


            try
            {
                FileAttributes attr = File.GetAttributes(InputPath);



                // is directory
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    retList.AddRange(Directory.EnumerateFiles(InputPath, Extension));
                }
                else   // is file
                {
                    retList.Add(InputPath);
                }
            }
            catch (Exception ex)
            {

            }
            
            
            return retList.ToArray();
        }
    }
}
