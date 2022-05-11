using Lab1.Models;
using System.Xml.Serialization;

namespace Lab1.Repositories
{
    public class XmlFunctionsRepository : IFunctionsRepository
    {
        private string StorageFileName { get; set; } = "functions.xml";

        private List<Function> _functions;

        public XmlFunctionsRepository() 
        {
            _functions = ReadFromFile();
        }
        public XmlFunctionsRepository(string storageFileName, List<Function> functions)
        {
            StorageFileName = storageFileName;
            _functions = functions;
            WriteToFile();
        }

        private List<Function> ReadFromFile()
        {
            if (!File.Exists(StorageFileName))
            {
                _functions = new List<Function>();
                return _functions;
            }
            var xmlSerializer = new XmlSerializer(typeof(List<Function>));
            using var fileStream = File.OpenRead(StorageFileName);
            var result = (List<Function>?)xmlSerializer.Deserialize(fileStream);
            if (result is null)
                throw new InvalidOperationException();
            _functions = result;
            if (_functions == null)
                throw new ArgumentNullException(nameof(_functions));
            return _functions;
        }

        private void WriteToFile()
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Function>));
            using var fileStream = new FileStream(StorageFileName, FileMode.Create);
            xmlSerializer.Serialize(fileStream, _functions);
        }

        public void InsertFunction(int index, Function function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            if (index >= _functions.Count)
                _functions.Add(function);
            else
                _functions.Insert(index, function);
            WriteToFile();
        }

        public void RemoveFunction(int index)
        {
            _functions = ReadFromFile();
            _functions.RemoveAt(index);
            WriteToFile();
        }

        public void Clear()
        {
            _functions = ReadFromFile();
            _functions.Clear();
            WriteToFile();
        }

        public List<Function> GetFunctions()
        {
            _functions = ReadFromFile();
            return _functions;
        }
    }
}
