namespace EmadKala.Services;

public interface ISmsService
{
    public void Otp(string phoneNumber, string password);
}

public class SmsService : ISmsService
{
    public void Otp(string phoneNumber, string password)
    {
        RawSend(phoneNumber, $"کد ورود شما به عماد کالا\n {password}");
    }

    private static void RawSend(string phoneNumber, string message)
    {
        // bypass sending sms and print it to web app stdout.
        Console.WriteLine($"SmsService:'{phoneNumber}': {message}");
        return;
        
        // uses API to send sms.
        using var httpClient = new HttpClient();
        httpClient.Send(new HttpRequestMessage()
        {
            RequestUri = new Uri($"http://api.payamak-panel.com/post/sendsms.ashx?username=&password=&from=&to={phoneNumber}&text={message}\nلغو۱۱")
        });
    }
}