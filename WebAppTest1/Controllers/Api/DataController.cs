using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;


namespace WebAppTest1.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        
        public DataController()
        {
           
        }


        [HttpGet("GetThumb")]
        public async Task<bool> GetThumb()
        {
           
            var x = await MakeThumb();
            return x;
        }

        [HttpGet("ConverVideo")]
        public async Task<bool> ConverVideo()
        {

            var x = await ConvertVideo();
            return x;
        }



        public async Task< bool> MakeThumb()
        {
           try
            {
                FFmpeg.SetExecutablesPath(@"wwwroot/ffmpeg", "ffmpeg", "ffprobe");


                var inputMp4Path = @"D:\VideoTest\videotest1.mp4";
                var output = @"D:\VideoTest\videotest1.png";

                //var inputMp4Path = @"~/wwwroot/video/videotest1.mp4";
                //var output = @"~/wwwroot\video/videotest1.png";

                //IConversion conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(inputMp4Path, output, TimeSpan.FromSeconds(4));
                //IConversionResult result = await conversion.Start();

                // string output = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");

                IConversion conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(inputMp4Path, output, TimeSpan.FromSeconds(6));
                IConversionResult result = await conversion.Start();



                return true;
            } catch(Exception ex)
            {
                return false;
            }
        
        }

        public async Task<bool> ConvertVideo ()
        {
            try
            {
                var inputVideoPath = @"D:\VideoTest\videotest1.mp4";
                //var output = @"D:\VideoTest\videotest1.png";
                string outputPathMp4 = @"D:\VideoTest\videotest2.mp4";

                IMediaInfo info = await FFmpeg.GetMediaInfo(inputVideoPath);

                IStream videoStream = info.VideoStreams.FirstOrDefault()
                    ?.SetFramerate(29.97);
                IStream audioStream = info.AudioStreams.FirstOrDefault()
                ?.SetBitrate(6000);

                var x = FFmpeg.Conversions.New()
                    .AddStream(videoStream, audioStream)
                    .SetOutput(outputPathMp4);

                return true;
            } catch(Exception ex)
            {
                return false;
            }
         
        }

      
    }
}
