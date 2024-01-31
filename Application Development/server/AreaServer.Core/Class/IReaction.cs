public interface IReaction
{
    public void SendEmail(string from, string to, string subject, string body, double temperature);
}