using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In_Depth_Events_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var video = new Video() {Title = "Die Hard"};
            var videoEncoder = new VideoEncoder();//publisher
            var mailService = new MailService();//subscriber
            var messageService = new MessageService();//subscriber

            //When video encoder wants to publish event, and checks who is "subscribed",
            //meaning a pointer to an eventhandler method
            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;

            videoEncoder.Encode(video);

            Console.ReadLine();
        }
    }

    public class MessageService
    {
        //notice how signature here is the same as the delegate;
        //you can't return anything other than void
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("Text Sent..." + e.Video.Title);
        }
    }
}
