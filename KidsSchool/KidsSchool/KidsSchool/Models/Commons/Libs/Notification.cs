using System;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Services.Description;
using System.Text;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Threading.Tasks;

namespace KidsSchool.Models.Commons.Libs
{
    public class NotificationService
    {
        private string FireBase_URL = "https://fcm.googleapis.com/fcm/send";
        private string key_server = "AAAAb8Z4Mxc:APA91bHJfP6cSCiojU2OysZIfDooLNJjF3IhXtO2opSu91V04E72lFagUpG_1qi0VQM1YNtz6nXLpn1ZbLVtFLOCwEHBeScDtML2WrfOK5nVBs2p9EsjxMRMY7g9Ksihb9yfOsGVs4EX";
        private string senderId = "480071136023";

        public NotificationService()
        {

        }

        public NotificationService(String Key_Server)
        {
            this.key_server = Key_Server;
        }





        public async Task<dynamic>  SendPush(PushMessage message)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FireBase_URL);
            request.Method = "POST";
            request.Headers.Add("Authorization", "key=" + this.key_server);
            request.Headers.Add(string.Format("Sender: id={0}", "480071136023"));
            request.ContentType = "application/json";
            string json = JsonConvert.SerializeObject(message);
            //json = json.Replace("content_available", "content-available");
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            HttpWebResponse respuesta = (HttpWebResponse)request.GetResponse();
            if (respuesta.StatusCode == HttpStatusCode.Accepted || respuesta.StatusCode == HttpStatusCode.OK || respuesta.StatusCode == HttpStatusCode.Created)
            {
                StreamReader read = new StreamReader(respuesta.GetResponseStream());
                String result = read.ReadToEnd();
                read.Close();
                respuesta.Close();
                dynamic stuff = JsonConvert.DeserializeObject(result);

                return stuff;
            }
            else
            {
                throw new Exception("Ocurrio un error al obtener la respuesta del servidor: " + respuesta.StatusCode);
            }
        }



        public void SendNotificationFireBase(PushMessage message)
        {
            try
            {
                dynamic data;
                if (message.registration_ids != null)
                {
                     data = new
                    {
                        registration_ids = message.registration_ids, // this is for multiple user 
                        notification = new
                        {
                            title = message.notification.title,     // Notification title
                            body = message.notification.body,    // Notification body data
                            link = message.notification.link       // When click on notification user redirect to this link
                        }
                    };
                }else
                {
                     data = new
                    {
                        registration_ids = message.to, 
                        notification = new
                        {
                            title = message.notification.title,     // Notification title
                            body = message.notification.body,    // Notification body data
                            link = message.notification.link       // When click on notification user redirect to this link
                        }
                    };
                }

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);


                WebRequest tRequest;
                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", this.key_server));

                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));

                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}

    public class PushMessage
    {
        private string _to;
        private string[] _registration_ids;
        private PushMessageData _notification;

        private dynamic _data;
        private dynamic _click_action;
        public dynamic data
        {
            get { return _data; }
            set { _data = value; }
        }
    

        public string to
        {
            get { return _to; }
            set { _to = value; }
        }

        public string[] registration_ids
        {
            get { return _registration_ids; }
            set { _registration_ids = value; }
         }

         public PushMessageData notification
        {
            get { return _notification; }
            set { _notification = value; }
        }

        public dynamic click_action
        {
            get
            {
                return _click_action;
            }

            set
            {
                _click_action = value;
            }
        }
    }

    public class PushMessageData
    {
        private string _title;
        private string _icon;
        private string _image;
        private string _body;
        private string _text;
        private string _sound = "default";
        private string _link;

        public string Sound
        {
            get { return _sound; }
            set { _sound = value; }
        }

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public string image
        {
            get { return _image; }
            set { _image = value; }
        }

        public string body
        {
            get { return _body; }
            set { _body = value; }
        }

        public string link
        {
            get { return _link; }
            set { _link = value; }
        }
    }

