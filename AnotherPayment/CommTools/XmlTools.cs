using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Scrape.Core
{
    public class XmlTools
    {
        #region 全局变量
        /// <summary>
        /// XML存储路径
        /// </summary>
        private static String xmlPath = AppDomain.CurrentDomain.BaseDirectory;
        #endregion

        #region XML操作基础方法
        /// <summary>
        /// 将实体类转换生成xml文件
        /// </summary>
        /// <param name="data">分析后的实体类</param>
        public static String CreateXml(object data)
        {
            Type dataType = data.GetType();
            XmlSerializer serializer =
                new XmlSerializer(dataType);
            TextWriter tw =
                new StreamWriter(xmlPath, false, Encoding.Unicode);
            serializer.Serialize(tw, data);
            tw.Close();

            return xmlPath;
        }
        /// <summary>
        /// 根据路径加载XML
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static XmlDocument GetXmlDoc(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            return xmlDoc;
        }

        /// <summary>
        /// 加载Xml ( string → Xml )
        /// </summary>
        /// <param name="xmlString">Xml字符串</param>
        /// <returns>XmlDoc</returns>
        public static XmlDocument LoadXmlDoc(string xmlString) 
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            return doc;
        }

        /// <summary>
        /// 将实体类 entity 序列化成 Xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static XmlDocument ObjToXmlDoc<T>(T entity)
        {
            StringBuilder buffer = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StringWriter(buffer))
            {
                serializer.Serialize(writer, entity);
            }
            XmlDocument dom = new XmlDocument();
            dom.LoadXml(buffer.ToString());
            return dom;
        }
        /// <summary>
        /// 从XML中取根节点，返回类型 XmlNodeList
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static XmlNodeList GetXmlNodeList(XmlDocument xmlDoc)
        {
            XmlNodeList nodeList = xmlDoc.ChildNodes;
            return nodeList;
        }
        /// <summary>
        /// 从XML中取指定节点，返回类型 XmlNodeList
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="strNodeName"></param>
        /// <param name="isToLower">是否需要区分大小写【True,不需要;False,需要】</param>
        /// <param name="isGetParentNode">是否取父节点NodeList【True,父节点结果集;False,当前节点结果集】</param>
        /// <returns></returns>
        public static XmlNodeList GetXmlNodeList(XmlDocument xmlDoc, string strNodeName, bool isToLower, bool isGetParentNode)
        {
            XmlNodeList nodeList = xmlDoc.ChildNodes;
            XmlNode thisNode = null;
            foreach (XmlNode root in nodeList)
            {
                thisNode = findNodeByValue(root, strNodeName, isToLower);
                if (thisNode != null)
                {
                    if (isGetParentNode)
                    {
                        return thisNode.ParentNode.SelectNodes(strNodeName);
                    }
                    else
                    {
                        return thisNode.SelectNodes(strNodeName);
                    }
                }
            }
            return null;
        }
        #endregion


        #region 从xml根开始，一层层找到指定节点
        /// <summary>
        /// 从xml根开始，一层层找到指定节点
        /// </summary>
        /// <param name="root"></param>
        /// <param name="appointNodeName"></param>
        /// <param name="isToLower">是否需要区分大小写:【True,不需要;False,需要】</param>
        /// <returns></returns>
        public static XmlNode findNodeByValue(XmlNode root, string appointNodeName, bool isToLower)
        {
            foreach (XmlNode node in root.ChildNodes)
            {
                if (isToLower)
                {
                    if (node.Name.ToLower() == appointNodeName.ToLower())
                    {
                        return node;
                    }
                }
                else
                {
                    if (node.Name == appointNodeName)
                    {
                        return node;
                    }
                }
                XmlNode childNode = findNodeByValue(node, appointNodeName, isToLower);
                if (childNode != null)
                {
                    return childNode;
                }
                else
                {
                    continue;
                }

            }
            return null;
        }
        #endregion

        #region 从XML中取出指定对象
        /// <summary>
        /// 从XML里获取所需要的单个实体类数据
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">实体类</param>
        /// <param name="xmlPath">xml路径</param>
        /// <param name="beenName">实体类名称</param>
        /// <param name="isGetList">是否取集合：true ,取集合; false :不取集合</param>
        public static List<T> GetBeenFromXml<T>(T entity, XmlNodeList nodeList, string beenName, bool isToLower, bool isGetList)
        {

            List<T> listT = new List<T>();

            XmlNode thisNode = null;
            foreach (XmlNode root in nodeList)
            {
                thisNode = XmlTools.findNodeByValue(root, beenName, isToLower);
                if (thisNode != null)
                {
                    if (isGetList)
                    {
                        XmlNodeList xnl = thisNode.ParentNode.SelectNodes(beenName);
                        foreach (XmlNode xn in xnl)
                        {
                            entity = (T)XmlTools.DeserializeFromXml(typeof(T), xn.OuterXml);
                            listT.Add(entity);
                        }
                    }
                    else
                    {
                        entity = (T)XmlTools.DeserializeFromXml(typeof(T), thisNode.OuterXml);
                        listT.Add(entity);
                    }
                }
            }
            return listT;
        }
        #endregion

        #region XML转换操作
        /// <summary>
        /// 反序列化( Xml → Object )
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        ///// <summary>
        ///// 序列化 Xml → Json
        ///// </summary>
        //public string ConvertXmlToJson(XmlDocument doc)
        //{
        //    return Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
        //}

        /// <summary>
        /// 【Json → Xml】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="entity"></param>
        /// <param name="beenName"></param>
        /// <returns></returns>
        public XmlDocument GetXmlFromJson(string json)
        {
            XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);
            return xmlDoc;
        }

        /// <summary>
        /// 【Json → Xml → Been】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="entity"></param>
        /// <param name="beenName"></param>
        /// <returns></returns>
        public T GetXmlFromJson<T>(T entity, string json, string beenName)
        {
            XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);

            XmlNodeList nodeList = xmlDoc.ChildNodes;
            XmlNode thisNode = null;
            foreach (XmlNode root in nodeList)
            {
                thisNode = findNodeByValue(root, beenName, true);
                if (thisNode != null)
                {
                    break;
                }
            }

            if (thisNode != null)
            {
                //List的元素，在XML里Type = array ，节点名称为 Item ，序列化为实体类失败
                entity = (T)XmlTools.DeserializeFromXml(typeof(T), thisNode.InnerXml);
            }

            return entity;
        }
        /// <summary>
        /// 读取一个XML文件，并且添加节点示例（未完成，不可用）
        /// </summary>
        public void ReadXml()
        {
            #region 读取一个XML文件，并且添加节点
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load("bookstore.xml");
            //XmlNode root = xmlDoc.SelectSingleNode("bookstore");//查找<bookstore>

            //XmlElement xe1 = xmlDoc.CreateElement("book");//创建一个<book>节点
            //xe1.SetAttribute("genre", "李赞红");//设置该节点genre属性
            //xe1.SetAttribute("ISBN", "2-3631-4");//设置该节点ISBN属性

            //XmlElement xesub1 = xmlDoc.CreateElement("title");
            //xesub1.InnerText = "CS从入门到精通";//设置文本节点
            //xe1.AppendChild(xesub1);//添加到<book>节点中

            //XmlElement xesub2 = xmlDoc.CreateElement("author");
            //xesub2.InnerText = "候捷";
            //xe1.AppendChild(xesub2);

            //XmlElement xesub3 = xmlDoc.CreateElement("price");
            //xesub3.InnerText = "58.3";
            //xe1.AppendChild(xesub3);

            //root.AppendChild(xe1);//添加到<bookstore>节点中
            //xmlDoc.Save("bookstore.xml");

            /// <summary>
            /// 输出后的内容
            /// <summary>

            //<bookstore>
            //  <book genre="fantasy" ISBN="2-3631-4">
            //    <title>Oberon's Legacy</title>
            //    <author>Corets, Eva</author>
            //    <price>5.95</price>
            //  </book>
            //  <book genre="update李赞红" ISBN="2-3631-4">
            //    <title>CS从入门到精通</title>
            //    <author>亚胜</author>
            //    <price>58.3</price>
            //  </book>
            //</bookstore>
            #endregion
        }
        #endregion


        public static XmlDocument GetXMLNode(string key, ref XmlNode xn)
        {
            XmlDocument XmlDoc = XmlTools.GetXmlDoc(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "XmlDoc\\XmlDocHelper.xml");
            XmlNodeList Xnl = XmlTools.GetXmlNodeList(XmlDoc);
            foreach (XmlNode item in Xnl)
            {
                xn = XmlTools.findNodeByValue(item, key, true);
                if (xn != null)
                {
                    break;
                }
            }
            return XmlDoc;
        }
    }
}
