using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.VariantTypes;

namespace Moudou.WordHelper
{
    // Ref Link  https://msdn.microsoft.com/en-us/library/office/hh674468.aspx

    /// <summary> word property write helper </summary>
    internal class WordPropertyHelper
    {
        /// <summary> Write property into word file </summary>
        /// <param name="fileName"> file path and name (ex. d:\doc1.docx) </param>
        /// <param name="PropertyName"> PropertyName </param>
        /// <param name="PropertyValue"> PropertyValue </param>
        /// <param name="PropertyType"> PropertyType </param>
        private static void SetCustomProperty(string fileName, string PropertyName, object PropertyValue, PropertyTypes PropertyType)
        {
            var newProp = WordPropertyHelper.parseCustomProperty(PropertyName, PropertyValue, PropertyType);

            if (newProp == null)
                throw new ArgumentException(" No property to write into file. ");

            List<CustomDocumentProperty> willFillPropertyList = new List<CustomDocumentProperty>() { newProp };


            WordPropertyHelper.writeProperty(fileName, willFillPropertyList);
        }


        /// <summary> Write propery list into word file </summary>
        /// <param name="fileName"> file path and name (ex. d:\doc1.docx) </param>
        /// <param name="list"> Property list </param>
        internal static void setCustomProperty(string fileName, List<CustomProperty> list)
        {
            List<CustomDocumentProperty> willFillPropertyList = new List<CustomDocumentProperty>();


            foreach (var cp in list)
            {
                var newProp = WordPropertyHelper.parseCustomProperty(cp.PropertyName, cp.PropertyValue, cp.PropertyType);


                if (newProp == null)
                    continue;


                willFillPropertyList.Add(newProp);
            }


            if (willFillPropertyList.Count <= 0)
                throw new ArgumentException(" No property to write into file. ");


            WordPropertyHelper.writeProperty(fileName, willFillPropertyList.ToArray());
        }


        /// <summary> convert values to object </summary>
        /// <param name="PropertyName"> PropertyName </param>
        /// <param name="PropertyValue"> PropertyValue </param>
        /// <param name="PropertyType"> PropertyType </param>
        /// <returns></returns>
        private static CustomDocumentProperty parseCustomProperty(string PropertyName, object PropertyValue, PropertyTypes PropertyType)
        {
            var newProp = new CustomDocumentProperty();
            bool propSet = false;

            // Calculate the correct type.
            switch (PropertyType)
            {
                case PropertyTypes.DateTime:

                    // Be sure you were passed a real date, 
                    // and if so, format in the correct way. 
                    // The date/time value passed in should 
                    // represent a UTC date/time.
                    if ((PropertyValue) is DateTime)
                    {
                        newProp.VTFileTime =
                            new VTFileTime(string.Format("{0:s}Z",
                                Convert.ToDateTime(PropertyValue)));
                        propSet = true;
                    }

                    break;

                case PropertyTypes.NumberInteger:
                    if ((PropertyValue) is int)
                    {
                        newProp.VTInt32 = new VTInt32(PropertyValue.ToString());
                        propSet = true;
                    }

                    break;

                case PropertyTypes.NumberDouble:
                    if (PropertyValue is double)
                    {
                        newProp.VTFloat = new VTFloat(PropertyValue.ToString());
                        propSet = true;
                    }

                    break;

                case PropertyTypes.Text:
                    newProp.VTLPWSTR = new VTLPWSTR(PropertyValue.ToString());
                    propSet = true;

                    break;

                case PropertyTypes.YesNo:
                    if (PropertyValue is bool)
                    {
                        // Must be lowercase.
                        newProp.VTBool = new VTBool(
                          Convert.ToBoolean(PropertyValue).ToString().ToLower());
                        propSet = true;
                    }
                    break;
            }


            if (!propSet)
            {
                return null;
            }


            // Now that you have handled the parameters, start
            // working on the document.
            newProp.FormatId = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}";
            newProp.Name = PropertyName;

            return newProp;
        }


        /// <summary> Actually write file </summary>
        /// <param name="fileName"> file path and name </param>
        /// <param name="newProps"> properties will be written </param>
        private static void writeProperty(string fileName, IEnumerable<CustomDocumentProperty> newProps)
        {
            using (var document = WordprocessingDocument.Open(fileName, true))
            {
                var customProps = document.CustomFilePropertiesPart;

                if (customProps == null)
                {
                    // No custom properties? Add the part, and the
                    // collection of properties now.
                    customProps = document.AddCustomFilePropertiesPart();
                    customProps.Properties = new DocumentFormat.OpenXml.CustomProperties.Properties();
                }


                var props = customProps.Properties;

                if (props == null)
                    return;



                foreach (CustomDocumentProperty newProp in newProps)
                {
                    // property is null, but if that happens, the property is damaged, 
                    // and probably should raise an exception.
                    var prop = props.Where(p => ((CustomDocumentProperty)p).Name.Value == newProp.Name).FirstOrDefault();

                    // Does the property exist? If so, get the return value, 
                    // and then delete the property.
                    if (prop != null)
                    {
                        prop.Remove();
                    }

                    // Append the new property, and 
                    // fix up all the property ID values. 
                    // The PropertyId value must start at 2.
                    props.AppendChild(newProp);
                    int pid = 2;
                    foreach (CustomDocumentProperty item in props)
                    {
                        item.PropertyId = pid++;
                    }
                }


                props.Save();
            }
        }
    }
}
