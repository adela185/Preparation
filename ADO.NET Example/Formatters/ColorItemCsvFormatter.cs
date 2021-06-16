using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.IO;
using ADO.NET_Class_Library_Ex;

namespace ADO.NET_Example.Formatters
{
    public class ColorItemCsvFormatter:BufferedMediaTypeFormatter
    {
        public ColorItemCsvFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv-coloritem"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(Color) || typeof(IEnumerable<Color>).IsAssignableFrom(type);
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (var writer = new StreamWriter(writeStream))
            {
                writer.WriteLine("Name,Hex,ColorID");
                var list = value as IEnumerable<Color>;
                if(list != null)
                {
                    foreach(var item in list)
                    {
                        WriteColorItem(item, writer);
                    }
                }
                else
                {
                    var item = value as Color;
                    if(item == null)
                    {
                        throw new InvalidOperationException("Type not supported type.");
                    }
                    WriteColorItem(item, writer);
                }
            }
        }

        private void WriteColorItem(Color item, StreamWriter writer)
        {
            writer.WriteLine("\"{0}\",\"{1}\",{2}", item.nm, item.hex, item.ColorID);
        }
    }
}