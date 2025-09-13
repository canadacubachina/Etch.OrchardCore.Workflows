using Etch.OrchardCore.Workflows.TemplateEmail.Workflows.Activities;
using Etch.OrchardCore.Workflows.TemplateEmail.Workflows.ViewModels;
using OrchardCore.Workflows.Display;
using OrchardCore.Workflows.Models;

namespace Etch.OrchardCore.Workflows.TemplateEmail.Workflows.Drivers
{
    public class TemplateEmailTaskDriver : ActivityDisplayDriver<TemplateEmailTask, TemlateEmailTaskViewModel>
    {
        #region Overrides

        protected override void EditActivity(TemplateEmailTask activity, TemlateEmailTaskViewModel model)
        {
            model.SenderExpression = activity.Sender.Expression;
            model.AuthorExpression = activity.Author.Expression;
            model.RecipientsExpression = activity.Recipients.Expression;
            model.ReplyToExpression = activity.ReplyTo.Expression;
            model.SubjectExpression = activity.Subject.Expression;
            //model.BodyFormat = activity.BodyFormat;
            //model.TextBody = activity.TextBody.Expression;
            //model.HtmlBody = activity.HtmlBody.Expression;
            model.BccExpression = activity.Bcc.Expression;
            model.CcExpression = activity.Cc.Expression;

            model.Body = activity.Body.Expression;
            //model.IsBodyHtml = activity.IsBodyHtml;
            model.TemplateName = activity.TemplateName;
        }

        protected override void UpdateActivity(TemlateEmailTaskViewModel model, TemplateEmailTask activity)
        {
            
            activity.Body = new WorkflowExpression<string>(model.Body);
           // activity.IsBodyHtml = model.IsBodyHtml;
            activity.TemplateName = model.TemplateName;

            activity.Sender = new WorkflowExpression<string>(model.SenderExpression);
            activity.Author = new WorkflowExpression<string>(model.AuthorExpression);
            activity.Recipients = new WorkflowExpression<string>(model.RecipientsExpression);
            activity.ReplyTo = new WorkflowExpression<string>(model.ReplyToExpression);
            activity.Subject = new WorkflowExpression<string>(model.SubjectExpression);
            //activity.BodyFormat = model.BodyFormat;
            //activity.TextBody = new WorkflowExpression<string>(model.TextBody);
            //activity.HtmlBody = new WorkflowExpression<string>(model.HtmlBody);
            activity.Bcc = new WorkflowExpression<string>(model.BccExpression);
            activity.Cc = new WorkflowExpression<string>(model.CcExpression);
        }

        #endregion
    }
}
