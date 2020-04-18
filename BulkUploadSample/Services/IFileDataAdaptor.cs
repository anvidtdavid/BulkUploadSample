using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BulkUploadSample.Services
{
    /// <summary>
    /// Implements the data reading the parsing mechanism
    /// </summary>
    public interface IFileDataAdaptor
    {
        /// <summary>
        /// Reads the data from the file and converts it a collection of the specified entities
        /// </summary>
        /// <typeparam name="T">Type of the entity</typeparam>
        /// <param name="fileName">Name of the file</param>
        /// <returns>Returns the collection of the entities</returns>
        Task<IEnumerable<T>> ReadFromFile<T>(string fileName) where T : class, new();

        /// <summary>
        /// Reads the data from the file stream and converts it a collection of the specified entities
        /// </summary>
        /// <typeparam name="T">Type of the entity</typeparam>
        /// <param name="fileName">Name of the file</param>
        /// <returns>Returns the collection of the entities</returns>
        Task<IEnumerable<T>> ReadFromFile<T>(Stream stream) where T : class, new();
    }
}
