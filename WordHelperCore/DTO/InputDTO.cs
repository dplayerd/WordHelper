using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudou.WordHelperCore.DTO
{
    /// <summary> 輸入用物件 </summary>
    public class InputDTO
    {
        /// <summary> WORD 的路徑 </summary>
        public string WordPath { get; set; }

        /// <summary> JSON 的路徑 </summary>
        public string JsonPath { get; set; }
    }
}
