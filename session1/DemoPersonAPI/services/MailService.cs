namespace DemoPersonAPI.Services;

public interface IMailService
{
    void SendMail(string to, string subject, string body);
}
public class MailService : IMailService
{
    public void SendMail(string to, string subject, string body)
    {
        /* IRL you can use mailkit and connect to a server
        We are using sendgrit
        Imagine the boss doesn't like the sendgrit service, cause it's too expensive
        So we need to use google mail service*/
    }
}

public class GoogleMailService : IMailService
{
    public void SendMail(string to, string subject, string body)
    {
        /* 
        Imagine the boss doesn't want to get rid of the old mail service immediately
        If we do this, only one line of code needs to be changed.
        It's the AddTransient method in the Program.cs
        */
    }
}