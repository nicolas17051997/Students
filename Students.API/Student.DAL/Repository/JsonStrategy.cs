using Newtonsoft.Json;
using Student.DAL.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Student.DAL.Repository
{
    public class JsonStrategy<T> : IFileStrategy<T> where T : class
    {
        public string PathDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("Students.API\\bin")[0] + "Student.DAL\\DataStorage\\students.json";

        public List<T> EntityCollection { get; set; } = new List<T>();

        public async Task<List<T>> ReadFromFileAsync()
        {
            JsonSerializer serializer = new JsonSerializer();
            List<T> collection;

            var value = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(PathDirectory, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                using (JsonReader jReader = new JsonTextReader(sr))
                {
                    collection = serializer.Deserialize<List<T>>(jReader);
                    fs.Close();
                }
                return collection;
            });
        }

        public async Task<bool> WriteToFileAsync(T value)
        {
            EntityCollection.Add(value);
            return await WriteValue();
        }

        public async Task<bool> WriteToFileCollectionAsync(List<T> data)
        {
            EntityCollection.AddRange(data);
            return await WriteValue();
        }

        public async Task<bool> WriteValue()
        {
            FileStream fileStream;
            var listToWrite = new List<T>();

            if (File.Exists(PathDirectory))
            {
                File.Delete(PathDirectory);
                fileStream = File.OpenWrite(PathDirectory);
            }
            else
                fileStream = File.Create(PathDirectory);

            return await Task.Run(() =>
            {

                using (fileStream)
                {
                    var writer = new DataContractJsonSerializer(typeof(List<T>));
                    writer.WriteObject(fileStream, EntityCollection);
                    fileStream.Close();
                    return true;
                }
            });
        }
    }
}
