using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Workflows.Export
{
    public class AdminMenu :  AdminNavigationProvider
    {
        #region Dependencies

        public IStringLocalizer T { get; set; }

        #endregion Dependencies

        #region Constructor

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        #endregion Constructor

        #region Implementation

        protected override ValueTask BuildAsync(NavigationBuilder builder)
        {
            //if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            //{
            //    return Task.CompletedTask;
            //}

            builder.Add(T["Workflow Export"], "15.1", workflowExport => workflowExport
                .AddClass("workflowexport").Id("workflowexport").Action("Index", "Export", new { area = "Etch.OrchardCore.Workflows" })
                .Permission(Permissions.ExportWorkflows)
                .LocalNav());

            return ValueTask.CompletedTask;
        }

        #endregion Implementation
    }
}
