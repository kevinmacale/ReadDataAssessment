namespace DTN.AssessmentExam.Service
{
    public interface IJsonHelperService
    {
        T DeserializeJsonString<T>(string fileName);
    }
}