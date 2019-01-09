using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCC_Chatbot
{








   

    public class JsonData
    {

        public class Rootobject
        {
            public string type { get; set; }
            public string text { get; set; }
            public Attachment[] attachments { get; set; }
        }

        public class Attachment
        {
            public string contentType { get; set; }
            public Content content { get; set; }
        }

        public class Content
        {
            public string type { get; set; }
            public string version { get; set; }
            public Body[] body { get; set; }
            public Action[] actions { get; set; }
        }

        public class Body
        {
            public string type { get; set; }
            public string text { get; set; }
            public string size { get; set; }
            public string separation { get; set; }
        }

        public class Action
        {
            public string type { get; set; }
            public string url { get; set; }
            public string title { get; set; }
        }

    }
}




          