using Producto.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Producto.Common
{
    public class ServicioExterno
    {
        public string jsonGetComment()
        {
            string jsonResult = "";
            string linkServicio = String.Format("https://jsonplaceholder.typicode.com/posts/1/comments");
            try
            {
                WebRequest requestObject = WebRequest.Create(linkServicio);
                requestObject.Method = "GET";
                HttpWebResponse responseObjectGet = null;
                responseObjectGet = (HttpWebResponse)requestObject.GetResponse();

                using (Stream stream = responseObjectGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    jsonResult = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                jsonResult = "";
            }
            return jsonResult;
        }


        public string enviarJsonGet()
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string ruta= String.Format("https://jsonplaceholder.typicode.com/posts/1/comments");
                    wc.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
                    var respuesta = wc.DownloadString(ruta);
                    return respuesta;
                }
                catch (WebException x)
                {
                    if (x.Response != null)
                    {
                        return new StreamReader(x.Response.GetResponseStream()).ReadToEnd();
                    }
                    else
                    {
                        string _ret = x.StackTrace;
                        return _ret;
                    }

                }
            }
        }

    }
}
