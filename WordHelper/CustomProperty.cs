using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moudou.WordHelper
{
    /// <summary> Property container </summary>
    internal class CustomProperty
    {
        /// <summary> PropertyName </summary>
        public string PropertyName { get; set; }


        /// <summary> PropertyValue </summary>
        public object PropertyValue { get; set; }


        /// <summary> PropertyType </summary>
        public PropertyTypes PropertyType { get; set; }
    }
}
