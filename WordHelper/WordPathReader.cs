﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudou.WordHelper
{
    /// <summary> parse path into absolute paths </summary>
    internal class WordPathReader
    {
        /// <summary> find paths </summary>
        /// <param name="InputPath"></param>
        /// <returns></returns>
        internal static string[] getWordPaths(string InputPath)
        {
            string[] retArr = FileHelper.getFilePaths(InputPath, FileType.Word);

            return retArr;
        }
    }
}
