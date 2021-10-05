using DTN.AssessmentExam.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var jsonString = GetFullDirectoryPath(fileName);
            var model = JsonSerializer.Deserialize<T>(jsonString);
            return model;
        }

        /// <summary>
        /// Deserialize json string to dictionary
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <typeparam name="Value"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Dictionary<string, AssetModel> DeserializeAssetsToDictionary(string fileName)
        {
            var jsonString = GetFullDirectoryPath(fileName);
            var assetModels = JsonSerializer.Deserialize<List<AssetModel>>(jsonString);

            if (assetModels == null || !assetModels.Any())
                return null;

            return new Dictionary<string, AssetModel>(assetModels.Select(x => new KeyValuePair<string, AssetModel>(x.quadKey, x)));
        }

        /// <summary>
        /// This will get combine the current path and file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetFullDirectoryPath(string fileName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var jsonString = File.ReadAllText($"{currentDirectory}/{fileName}");
            return jsonString;
        }
    }
}
