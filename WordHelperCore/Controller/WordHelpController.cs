using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordHelperCore.DTO;
using WordHelperCore.Helper;
using WordHelperCore.VO;

namespace WordHelperCore.Controller
{
    public class WordHelpController
    {

        /// <summary> call Helper </summary>
        /// <param name="wordPath"> filePath </param>
        /// <param name="propertyJsonPath"> properties </param>
        public void fillProperties(InputDTO dto)
        {
            CustomProperty[] cpList = JsonReader.readAllJSON(dto.JsonPath);
            string[] filePathList = WordPathReader.getWordPaths(dto.WordPath);


            foreach (string fileName in filePathList)
            {
                WordPropertyHelper.setCustomProperty(fileName, cpList.ToList());
            }
        }
    }
}
