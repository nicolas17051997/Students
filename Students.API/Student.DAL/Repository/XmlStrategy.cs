using Student.DAL.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Student.DAL.Repository
{
    public class XmlStrategy<T> : IFileStrategy<T> where T : class
    {
        public string PathDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("Students.API\\bin")[0] + "Student.DAL\\DataStorage\\students.xml";

        public List<T> EntityCollection { get; set; } = new List<T>();

        public async Task<bool> WriteToFileAsync(T entity)
        {
            EntityCollection.Add(entity);
            return await WriteValue();
        }

        public async Task<List<T>> ReadFromFileAsync()
        {
            var value = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            List<T> collection;
            return await Task.Run(() =>
           {
               XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

               using (FileStream fs = new FileStream(PathDirectory, FileMode.Open))
               {
                   XmlReader reader = XmlReader.Create(fs);
                   collection = (List<T>)serializer.Deserialize(reader);
                   fs.Close();
               }

               return collection;
           });
        }

        public async Task<bool> WriteToFileCollectionAsync(List<T> data)
        {
            EntityCollection.AddRange(data);
            return await WriteValue();
        }

        public async Task<bool> WriteValue()
        {
            FileStream fileStream;

            if (File.Exists(PathDirectory))
                fileStream = File.OpenWrite(PathDirectory);
            else
                fileStream = File.Create(PathDirectory);

            return await Task.Run(() =>
             {
                 using (fileStream)
                 {
                     XmlSerializer writer = new XmlSerializer(typeof(List<T>));
                     writer.Serialize(fileStream, EntityCollection);
                     fileStream.Close();
                     return true;
                 }
             });
        }
    }
}
