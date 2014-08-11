using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Scrape.Core
{
    public class JsonTools
    {
        public static String ToStrJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }

        public static T ToObj<T>(String strJson)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(strJson));
            return (T)serializer.ReadObject(mStream);
        }

        public static T ToObj<T>(String strJson, object Obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(Obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(strJson));
            return (T)serializer.ReadObject(mStream);
        }


        public static HttpResponseMessage ToHttpMsgJson(object obj)
        {
            if (obj != null)
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                MemoryStream stream = new MemoryStream();
                serializer.WriteObject(stream, obj);
                byte[] dataBytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(dataBytes, 0, (int)stream.Length);
                String strJson = Encoding.UTF8.GetString(dataBytes);
                HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(strJson, Encoding.GetEncoding("UTF-8"), "text/html") };
                return result;
            }
            else
            {
                HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent("{'err':'无结果集！'}", Encoding.GetEncoding("UTF-8"), "text/html") };
                return result;
            }
        }
    }
}
