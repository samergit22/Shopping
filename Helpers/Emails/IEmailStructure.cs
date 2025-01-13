namespace HirePlatform.Helpers.Emails
{
    public interface IEmailStructure
    {
        public string To { get; }
        public string Body { get; }
        public string Subject { get; }
    }
}
