using OrchardCore.Email.Workflows;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Etch.OrchardCore.Workflows.TemplateEmail.Workflows.ViewModels
{
    public class TemlateEmailTaskViewModel
    {
        public string AuthorExpression { get; set; }

        public string SenderExpression { get; set; }

        public string ReplyToExpression { get; set; }

        public string CcExpression { get; set; }

        public string BccExpression { get; set; }

        public string RecipientsExpression { get; set; }

        public string SubjectExpression { get; set; }

       // public MailMessageBodyFormat BodyFormat { get; set; }

        //public string TextBody { get; set; }

        //public string HtmlBody { get; set; }

        public string Body { get; set; }
        //public bool IsBodyHtml { get; set; }
        
        public string TemplateName { get; set; }
    }
}
