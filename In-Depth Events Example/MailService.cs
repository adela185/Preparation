using System;

namespace In_Depth_Events_Example
{
    public class MailService
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("Email Sent..." + e.Video.Title);
        }
    }
}
