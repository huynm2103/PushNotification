using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace PushNotification.Helpers
{
    public static class GCMessage
    {
        public static void Send(string regId, string sendMgs)
        {
            const string applicationID = "bb9ca398bbd1713b5f2babe3e21aa70f0b59f6a3";
            const string senderId = "push-notification-demo-140502";

            var value = sendMgs;
            var tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add($"Authorization: key={applicationID}");

            tRequest.Headers.Add($"Sender: id={senderId}");

            // string postData = "{ 'registration_id': [ '" + regId + "' ], 'data': {'message': '" + txtMsg.Text + "'}}";
            var postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "&registration_id=" + regId + "";
            Console.WriteLine(postData);
            var byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            var dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            var tReader = new StreamReader(dataStream);

            var sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream?.Close();
            tResponse.Close();
        }
    }
}