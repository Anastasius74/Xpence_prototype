using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XPence.Framework.XmlSerialization
{
    public static class DataSerializer
    {
        /// <summary>
        ///     Write all the object data to an xml string.
        /// </summary>
        /// <param name="value">Entity object</param>
        /// <returns>Xml structure of the entity object</returns>
        public static string EntityToXmlString(object value)
        {
            var serializer = new DataContractSerializer(value.GetType());
            using (var backing = new StringWriter())
            using (var writer = new XmlTextWriter(backing))
            {
                serializer.WriteObject(writer, value);
                return backing.ToString();
            }
        }

        /// <summary>
        ///     Serializes objects into the XML document.
        /// </summary>
        /// <typeparam name="T">Specify the type of object to be serialized</typeparam>
        /// <param name="data">Specify the data to be serialized</param>
        /// <param name="path">Specify the location of the xml document.</param>
        public static void Serialize<T>(T data, string path)
        {
            var folder = Path.GetDirectoryName(path);

            if (folder != null)
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var serializer = new XmlSerializer(typeof (T));
                using (TextWriter writer = new StreamWriter(path))
                {
                    serializer.Serialize(writer, data);
                }
            }
        }

        /// <summary>
        ///     Deserializes objects from the XML document.
        /// </summary>
        /// <typeparam name="T">Specify the type of object to be deserialized</typeparam>
        /// <param name="path">Specify the location of the xml document</param>
        /// <returns>Returns objects from the xml document.</returns>
        public static T Deserialize<T>(string path)
        {
            var deserializer = new XmlSerializer(typeof (T));
            TextReader reader = new StreamReader(path);

            var obj = deserializer.Deserialize(reader);
            var xmlData = (T) obj;
            reader.Close();

            return xmlData;
        }

        /// <summary>
        ///     Add new DatabaseInfo Item to the xml file.
        /// </summary>
        /// <typeparam name="T">Specify the type of object to be serialized</typeparam>
        /// <param name="data">Specify the data to be serialized</param>
        /// <param name="xmlFile">xml file path</param>
        public static void AddNewElement<T>(T data, string xmlFile)
        {
            AddGenericElement(data, xmlFile);
        }

        /// <summary>
        ///     Add new DatabaseInfo Item to the xml file.
        /// </summary>
        /// <typeparam name="T">Specify the type of object to be serialized</typeparam>
        /// <param name="elements">Specify the data to be serialized</param>
        /// <param name="xmlFile">xml file path</param>
        public static void AddElements<T>(List<T> elements, string xmlFile)
        {
            foreach (var element in elements)
            {
                AddGenericElement(element, xmlFile);
            }
        }

        private static void AddGenericElement<T>(T data, string xmlFile)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFile);
            SerializeAppend(xmlDocument.ChildNodes[1].LastChild, data);
            xmlDocument.Save(xmlFile);
        }

        private static void SerializeAppend(XmlNode parentNode, object obj)
        {
            var nav = parentNode.CreateNavigator();
            using (var writer = nav.AppendChild())
            {
                var serializer = new XmlSerializer(obj.GetType());
                writer.WriteWhitespace(string.Empty);
                serializer.Serialize(writer, obj);
                writer.Close();
            }
        }

        /// <summary>
        ///     Remove the last created new database info item.
        /// </summary>
        /// <typeparam name="T">the type for the serialized database info object.</typeparam>
        /// <param name="xmlFile">database file info.</param>
        /// <param name="data">the serialized database info object.</param>
        /// <param name="xPath">//DatabaseInfoManager/Children/DatabaseInfoItem</param>
        public static void RemoveNewElement<T>(string xmlFile, T data, string xPath)
        {
            var doc = new XmlDocument();
            doc.Load(xmlFile);
            var nodes = doc.SelectNodes(xPath);

            if (nodes != null)
            {
                for (var i = nodes.Count - 1; i >= 0; i--)
                {
                    var xmlNode = nodes[i];

                    if (xmlNode != null && xmlNode.InnerXml == data.ToXmlDocument<T>(doc).InnerXml)
                    {
                        if (xmlNode.ParentNode != null)
                        {
                            xmlNode.ParentNode.RemoveChild(nodes[i]);
                        }
                    }
                }
            }
            doc.Save(xmlFile);
        }

        /// <summary>
        /// Remove all elements from the xml file.
        /// </summary>
        /// /// <param name="xmlFile">database file info.</param>
        /// /// <param name="xPath">//DatabaseInfoManager/Children/DatabaseInfoItem</param>
        public static void RemoveAll(string xmlFile, string xPath)
        {
            var doc = new XmlDocument();
            doc.Load(xmlFile);
            var nodes = doc.SelectNodes(xPath);

            if (nodes != null)
            {
                for (var i = nodes.Count - 1; i >= 0; i--)
                {
                    var xmlNode = nodes[i];

                    if (xmlNode != null)
                    {
                        if (xmlNode.ParentNode != null)
                        {
                            xmlNode.ParentNode.RemoveChild(nodes[i]);
                        }
                    }
                }
            }
            doc.Save(xmlFile);
        }

        private static XmlElement ToXmlDocument<T>(this object obj, XmlDocument document)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);
            XmlElement returnVal = null;
            var serializer = new XmlSerializer(typeof (T));
            using (var ms = new MemoryStream())
            {
                using (var tw = new XmlTextWriter(ms, Encoding.UTF8))
                {
                    var doc = new XmlDocument();

                    tw.Formatting = Formatting.Indented;
                    tw.IndentChar = ' ';
                    serializer.Serialize(tw, obj);

                    ms.Seek(0, SeekOrigin.Begin);

                    doc.Load(ms);

                    if (doc.DocumentElement != null)
                    {
                        returnVal = document.ImportNode(doc.DocumentElement, true) as XmlElement;
                    }
                }
            }

            return returnVal;
        }
    }
}

