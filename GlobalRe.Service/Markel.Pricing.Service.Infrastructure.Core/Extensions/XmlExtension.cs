using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    /// <summary>
    /// Enumeration of available conversion types
    /// </summary>
    public enum ConversionType
    {
        XMLSerializer,
        DataContractSerializer,
        DataContractSerializerNoNamespace,
    }

    /// <summary>
    /// Xml test reader which ignores namespaces when reading elements
    /// </summary>
    public class NoNamespaceXmltextReader : XmlTextReader
    {
        public NoNamespaceXmltextReader(Stream stream) : base(stream)
        {
        }
        public override string this[string name, string namespaceURI]
        {
            get { return base[name, string.Empty]; }
        }
        public override void ReadStartElement(string localname, string ns)
        {
            base.ReadStartElement(localname, string.Empty);
        }
        public override string ReadElementString(string localname, string ns)
        {
            return base.ReadElementString(localname, string.Empty);
        }
        public override bool IsStartElement(string localname, string ns)
        {
            return base.IsStartElement(localname, string.Empty);
        }
        public override string GetAttribute(string localName, string namespaceURI)
        {
            return base.GetAttribute(localName, string.Empty);
        }
        public override bool MoveToAttribute(string localName, string namespaceURI)
        {
            return base.MoveToAttribute(localName, string.Empty);
        }
        public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
        {
            return base.ReadContentAs(returnType, namespaceResolver);
        }
        public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
        {
            return base.ReadElementContentAs(returnType, namespaceResolver);
        }
        public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
        {
            return base.ReadElementContentAs(returnType, namespaceResolver, localName, string.Empty);
        }
        public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsBoolean(localName, string.Empty);
        }
        public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsDateTime(localName, string.Empty);
        }
        public override decimal ReadElementContentAsDecimal(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsDecimal(localName, string.Empty);
        }
        public override double ReadElementContentAsDouble(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsDouble(localName, string.Empty);
        }
        public override long ReadElementContentAsLong(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsLong(localName, string.Empty);
        }
        public override float ReadElementContentAsFloat(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsFloat(localName, string.Empty);
        }
        public override int ReadElementContentAsInt(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsInt(localName, string.Empty);
        }
        public override object ReadElementContentAsObject(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsObject(localName, string.Empty);
        }
        public override string ReadElementContentAsString(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsString(localName, string.Empty);
        }
    }

    /// <summary>
    /// Xml text writer which does not output element namespaces. 
    /// </summary>
    public class NoNameSpaceXmlTextWriter : XmlTextWriter
    {
        public NoNameSpaceXmlTextWriter(Stream stream) : base(stream, null)
        {
        }
        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            base.WriteStartElement(null, localName, string.Empty);
        }
        public override void WriteQualifiedName(string localName, string ns)
        {
            base.WriteQualifiedName(localName, string.Empty);
        }
        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            base.WriteStartAttribute(prefix, localName, ns);
        }
    }

    /// <summary>
    /// Represents a set of useful extension methods for xml and xslt.
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// Converts object type to XML using the specified conversion type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="conversionType">Type of the conversion.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">obj</exception>
        public static string ConvertToXml<T>(this T obj, ConversionType conversionType = ConversionType.DataContractSerializerNoNamespace)
        {
            string xmlString = string.Empty;
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (conversionType == ConversionType.XMLSerializer)
            {
                using (StringWriter writer = new StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    XmlSerializerNamespaces nameSpaces = new XmlSerializerNamespaces();
                    nameSpaces.Add("", "");
                    serializer.Serialize(writer, obj, nameSpaces);
                    xmlString = serializer.ToString();
                }
            }
            else if (conversionType == ConversionType.DataContractSerializerNoNamespace)
            {
                var serializer = new DataContractSerializer(obj.GetType());
                MemoryStream stream = new MemoryStream();
                using (var writer = new NoNameSpaceXmlTextWriter(stream))
                {
                    serializer.WriteObject(writer, obj);
                    writer.Flush();
                    stream.Seek(0L, SeekOrigin.Begin);
                    xmlString = Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                using (var writer = XmlDictionaryWriter.CreateTextWriter(stream, System.Text.Encoding.UTF8))
                {
                    new DataContractSerializer(obj.GetType()).WriteObject(stream, obj);
                    xmlString = Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            return xmlString;
        }

        /// <summary>
        /// Converts XML to object type using specified conversion type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <param name="conversionType">Type of the conversion.</param>
        /// <returns></returns>
        public static T ConvertFromXml<T>(this string xml, ConversionType conversionType = ConversionType.DataContractSerializerNoNamespace)
        {
            var returnObj = default(T);
            if (conversionType == ConversionType.XMLSerializer)
            {
                using (StringReader reader = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    returnObj = (T)serializer.Deserialize(reader);
                }
            }
            else if (conversionType == ConversionType.DataContractSerializerNoNamespace)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
                using (var reader = new NoNamespaceXmltextReader(stream))
                {
                    returnObj = (T)serializer.ReadObject(reader);
                }
            }
            else
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
                    returnObj = (T)serializer.ReadObject(reader);
                }
            }
            return returnObj;
        }

        /// <summary>
        /// Transforms the object type XSLT using the specified transform string and conversion type.
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="xslt">String containing filename and path or XSLT transform.</param>
        /// <param name="conversionType">Type of the conversion.</param>
        /// <returns></returns>
        public static T TransformXslt<T>(this T obj, string xsltString, ConversionType conversionType = ConversionType.DataContractSerializerNoNamespace)
        {
            return TransformXslt<T, T>(obj, xsltString, conversionType);
        }
        
        /// <summary>
        /// Transforms the object type XSLT using the specified transform string and conversion type.
        /// </summary>
        /// <typeparam name="I">Input Object Type</typeparam>
        /// <typeparam name="O">Output Object Type</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="xsltString">XSLT String</param>
        /// <param name="conversionType">Conversion Type</param>
        /// <returns>Converted Object</returns>
        public static O TransformXslt<I, O>(this I obj, string xsltString, ConversionType conversionType = ConversionType.DataContractSerializerNoNamespace)
        {
            O transformedObj = default(O);

            MemoryStream memStream = obj.TransformXsltStream(xsltString, conversionType);
            using (StreamReader reader = new StreamReader(memStream))
            {
                string transformedString = reader.ReadToEnd();
                transformedObj = transformedString.ConvertFromXml<O>(conversionType);
            }

            return transformedObj;
        }

        public static MemoryStream TransformXsltStream<I>(this I obj, string xsltString, ConversionType conversionType = ConversionType.DataContractSerializerNoNamespace)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();

            if (xsltString.StartsWith("<?xml") == false)
                xslt.Load(xsltString);
            else
            {
                XmlDocument xmlXsltDoc = new XmlDocument();
                xmlXsltDoc.LoadXml(xsltString);
                xslt.Load(xmlXsltDoc);
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(obj.ConvertToXml(conversionType));

            MemoryStream memStream = new MemoryStream();
            xslt.Transform(xmlDoc, null, memStream);
            memStream.Position = 0;

            return memStream;
        }
    }
}
