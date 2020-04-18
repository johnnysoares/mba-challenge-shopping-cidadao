using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UTILCommon.Extensions.Xml.Entities;

namespace UTILCommon.Extensions.Xml {

    public static class XMLExtensions {

        /// <summary>
        /// Converter um objeto em uma string xml
        /// </summary>
        /// <typeparam name="T">Generic Class</typeparam>
        public static string toXML<T>(this T Objeto, bool flagRemoverTagEncoding = false, bool flagLimparNamespaces = false) where T : class {

            string xml = "";
            
            var xmlSerializer = new XmlSerializer(Objeto.GetType());
			
            using(var sww = new StringWriterWithEncoding(Encoding.UTF8)) {
				
                using(XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings { Encoding = Encoding.UTF8, OmitXmlDeclaration = flagRemoverTagEncoding })) {

                    if (flagLimparNamespaces) {
                        var xns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                        xmlSerializer.Serialize(writer, Objeto, xns);
                    }
                    
                    if (!flagLimparNamespaces) {
                        xmlSerializer.Serialize(writer, Objeto);   
                    }
					
                    xml = sww.ToString();
                }
            }
            
            return xml;
        }

        public static T xmlToClass<T>(this string xmlText, string xmlRootElement = "") where T : class {

            var stringReader = new StringReader(xmlText);

            var serializer = new XmlSerializer(typeof(T));

            if (!xmlRootElement.isEmpty()) {

                serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootElement));
            }

            return serializer.Deserialize(stringReader) as T;
        }

    }
}
