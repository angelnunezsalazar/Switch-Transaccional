namespace DataAccess.Errors
{
    public class ErrorStorage : IErrorStorage
    {
        private string errorMessage = "";

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
        }

        public void Save(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}