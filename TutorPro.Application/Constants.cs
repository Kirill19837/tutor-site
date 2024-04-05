using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorPro.Application
{
    public class Constants
    {
        public static class EmailTemplates
        {
            public const string BaseFolder = "EmailTemplates";
            public const string RequestTemplateEmail = BaseFolder + "/EmailTemplate.html";
        }

        public static class EmailProperty
        {
            public const string SenderName = "[SENDER_NAME]";
            public const string SenderEmail = "[SENDER_EMAIL]";
            public const string SenderPhone = "[SENDER_PHONE]";
        }
       
    }
}
