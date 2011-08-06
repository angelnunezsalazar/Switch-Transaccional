namespace DataAccess.Errors
{
    public interface IErrorStorage
    {
        string ErrorMessage { get; }

        void Save(string errorMessage);
    }
}