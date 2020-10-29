namespace TechLibrary.Interfaces
{
    public interface IAppConfig
    {
        string AmazonURL { get; set; }
        int DefaultRecordsPerPage { get; set; }
    }
}