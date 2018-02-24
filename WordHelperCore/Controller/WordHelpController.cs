using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moudou.WordHelperCore.DTO;
using Moudou.WordHelperCore.Helper;
using Moudou.WordHelperCore.VO;

namespace Moudou.WordHelperCore.Controller
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
