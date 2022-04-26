﻿using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab1.Repositories
{
    public class XmlFunctionsRepository : IFunctionsRepository
    {
        private const string StorageFileName = "functions.xml";

        private List<Function>? _functions;

        private List<Function> ReadFromFile()
        {
            if (!File.Exists(StorageFileName))
            {
                _functions = new List<Function>();
                return _functions;
            }
            var xmlSerializer = new XmlSerializer(typeof(List<Function>));
            using var fileStream = File.OpenRead(StorageFileName);
            _functions = (List<Function>?)xmlSerializer.Deserialize(fileStream);
            return _functions!;
        }

        private void WriteToFile()
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Function>));
            using var fileStream = new FileStream(StorageFileName, FileMode.Create);
            xmlSerializer.Serialize(fileStream, _functions);
        }

        public void AddFunction(int index, Function function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            ReadFromFile();

            if (index >= _functions?.Count)
                _functions.Add(function);
            else
                _functions?.Insert(index, function);
            WriteToFile();
        }

        public void RemoveFunction(int index)
        {
            ReadFromFile();
            _functions.RemoveAt(index);
            WriteToFile();
        }

        public void Clear()
        {
            ReadFromFile();
            _functions.Clear();
            WriteToFile();
        }

        public List<Function> GetFunctions()
        {
            ReadFromFile();
            return _functions;
        }

        public Function? GetFunction(int index)
        {
            ReadFromFile();
            if (_functions == null)
                return null;
                //throw new ArgumentNullException(nameof(_functions));

            if (index < 0 || index > _functions.Count)
                return null;
                //throw new ArgumentNullException(index.ToString());

            return _functions[index];
        }

    }
}
