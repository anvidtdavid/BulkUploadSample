using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BulkUploadSample.Services
{
    public class CSVFileAdaptor : IFileDataAdaptor
    {
        private const string CSV_EXTENSION = ".csv";
        private const string ERROR_MESSAGE = "File name cannot be empty";
        private const string INVALID_FILE_TYPE = "Invalid file type. Only CSV files are supported";
        private const string INVALID_DATA = "Invalid data present in the line - {0}. See inner exception for more details.";
        private const string STREAM_IS_NULL = "File stream cannot be null";

        public async Task<IEnumerable<T>> ReadFromFile<T>(string fileName) where T : class, new()
        {
            // Reading all lines from the file

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new Exception(ERROR_MESSAGE);
            }

            if (!Path.GetExtension(fileName).Equals(CSV_EXTENSION, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(INVALID_FILE_TYPE);
            }

            var lines = await File.ReadAllLinesAsync(fileName);

            var entities = new List<T>();

            foreach (var line in lines) // Reading each line by line from the file and parsing it to the entity
            {
                try
                {
                    entities.Add(ParseTo<T>(line));
                }
                catch (Exception exception)
                {
                    throw new Exception(string.Format(INVALID_DATA, line), exception);
                }
            }

            return await Task.FromResult(entities);
        }

        public async Task<IEnumerable<T>> ReadFromFile<T>(Stream stream) where T : class, new()
        {
            if (stream == null)
            {
                throw new Exception(STREAM_IS_NULL);
            }

            var entities = new List<T>();

            using (stream)
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();

                        try
                        {
                            entities.Add(ParseTo<T>(line));
                        }
                        catch (Exception exception)
                        {
                            throw new Exception(string.Format(INVALID_DATA, line), exception);
                        }
                    }
                }
            }

            return await Task.FromResult(entities);
        }

        /// <summary>
        /// Parsing the string to the entity
        /// </summary>
        /// <typeparam name="T">Specifies the entity</typeparam>
        /// <param name="line">Specifies the line</param>
        /// <returns>Returns an instance of the entity</returns>
        private T ParseTo<T>(string line) where T : class, new()
        {
            // Selecting only the properties with out CSVIgnore attribute from the data entity 

            var properties = typeof(T).GetProperties();

            var values = line.Split(',', properties.Length);
            var obj = new T();
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                if (!property.CustomAttributes.Any(a => a.AttributeType == typeof(CSVIgnoreAttribute)))
                {
                    property.SetValue(obj, Convert.ChangeType(values[i], property.PropertyType)); 
                }
            }

            return obj;
        }
    }
}
