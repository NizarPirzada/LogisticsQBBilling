using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FTCommon.Utils
{
    public static class Helper
    {
        public static string GetEnumDescription(this System.Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : string.Empty;
        }
        public static string GetHTMLTags(List<string> ListData)
        {
            string html_vals = "";

            foreach (var item in ListData)
            {
                html_vals += "<label class='badge badge-primary p-1'>" + item + "</label> ";
            }
            return html_vals;
        }
        public static string GetStatusTags(bool status)
        {
            string html_vals = "";
            if (status)
            {
                html_vals = "<label class='badge badge-primary p-1'>Active</label> ";
            }
            else
            {
                html_vals = "<label class='badge badge-danger p-1'>In Active</label> ";
            }
            return html_vals;
        }
        public static string GetEnumDescriptionText(System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();

        }

        public static string ObjToString(object oBj)
        {
            XmlSerializer objXS = new XmlSerializer(oBj.GetType());
            StringWriter objSW = new StringWriter();

            try
            {
                objXS.Serialize(objSW, oBj);
                objSW.Dispose();
                return objSW.ToString();
            }
            catch (Exception ex)
            {
                objXS = null;
                return string.Empty;
            }
        }
        public static string ObjToJson(object oBj)
        {
            try
            {
                var result = JsonConvert.SerializeObject(oBj);
                return result;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string ToXML(object model)
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(model.GetType());
                serializer.Serialize(stringwriter, model);
                return stringwriter.ToString();
            }
        }
        public static object StringToObj(string strXML, object oBj)
        {
            StringReader string_reader = null;
            XmlSerializer xml_serializer = new XmlSerializer(oBj.GetType());
            try
            {
                string_reader = new StringReader(strXML);
                return (object)xml_serializer.Deserialize(string_reader);
            }
            catch (Exception ex)
            {
                xml_serializer = null/* TODO Change to default(_) if this is not a reference type */;
                return null;
            }
        }
        public static IEnumerable<SelectListItem> GetEnumList<TEnum>()
        {
            return System.Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(t => ((System.Enum)((object)t)).GetEnumDescription(), t => (int)(object)t).ToList()
                .Select(t => new SelectListItem { Text = t.Key, Value = t.Value.ToString() });
        }

        /// <summary>
        /// This operator uses deferred execution and streams the results, although
        /// a set of already-seen keys is retained. If a key is seen multiple times,
        /// only the first element with that key is returned.
        /// </summary>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
     (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }

}
