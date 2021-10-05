using DTN.AssessmentExam.Model;
using System.Collections.Generic;

namespace DTN.AssessmentExam.Service
{
    public interface IJsonHelperService
    {
        T DeserializeJsonString<T>(string fileName);
        Dictionary<string, AssetModel> DeserializeAssetsToDictionary(string fileName);
    }
}