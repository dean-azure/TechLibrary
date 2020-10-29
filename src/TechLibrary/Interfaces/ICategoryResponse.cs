namespace TechLibrary.Interfaces
{
    public interface ICategoryResponse
    {
        int? Id { get; set; }
        string CategoryName { get; set; }

        int Count { get; set; }
        bool Selected { get; set; }
    }

    public interface ICategory
    {
        int? Id { get; set; }
        string CategoryName { get; set; }

    }
}