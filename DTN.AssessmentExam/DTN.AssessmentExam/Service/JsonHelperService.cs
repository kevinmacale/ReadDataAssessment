using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DTN.AssessmentExam.Service
{
    /// <summary>
    /// Json Helper service
    /// </summary>
    public class JsonHelperService : IJsonHelperService
    {
        /// <summary>
        /// Deserialize json from current directory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T DeserializeJsonString<T>(string fileName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var jsonString = File.ReadAllText($"{currentDirectory}/{fileName}");
            var model = JsonSerializer.Deserialize<T>(jsonString);
            return model;
        }
    }
}
