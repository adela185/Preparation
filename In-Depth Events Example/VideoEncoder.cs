using System;
using System.Threading;

namespace In_Depth_Events_Example
{
    //custom class for sending a reference of the video used to subs
    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }

    class VideoEncoder
    {
        //1 - Need a delegate (signature) for the class (this) that published event
        //2 - Define event based based on that delegate
        //3 - Raise that event

        //Event Handler in the subscribers (convetion: VideoEncoded name of the event, appended by EventHandler)
        //delegate hold reference to a function that looks like the below
        //params: object is source and EventArgs includes any additonal data

        /*Long way of shorcut below this comment section*/
        //public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args /*EventArgs args*/);
        //public event VideoEncodedEventHandler VideoEncoded;*/

        public event EventHandler<VideoEventArgs> VideoEncoded;

        public void Encode(Video video)
        {
            //Encoding Logic ...


            /*So if these were extension to already established code 
             *we'll use events instead of calls like these to keep 
             *things loosely coupled*/
            //_mailService.Send(new Mail());
            //_messageService.Send(new Text());

            Console.WriteLine("Encoding Video...");
            Thread.Sleep(3000);

            OnVideoEncoded(video);
        }

        protected virtual void OnVideoEncoded(Video video)
        {
            /*we start by checking if they're any subscribers to this event*/
            //if (VideoEncoded != null)
            //    VideoEncoded(this, new VideoEventArgs() { Video = video } /*EventArgs.Empty*/);

            //or we can use this shorcut for null checking like this
            VideoEncoded?.Invoke(this, new VideoEventArgs() { Video = video });
        }
    }
}
