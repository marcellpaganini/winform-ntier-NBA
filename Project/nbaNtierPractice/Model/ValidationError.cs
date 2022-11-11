namespace Model
{
    public class ValidationError
    {
        public ValidationError(string desc)
        {
            Description = desc;
        }

        public string Description { get; set; }
    }
}
