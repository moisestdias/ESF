using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace EpmDashboard.Pages
{
    public partial class ProblemMakers
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public EPMService EPMService { get; set; }

        protected IEnumerable<EpmDashboard.Models.EPM.ProblemMaker> problemMakers;

        protected RadzenDataGrid<EpmDashboard.Models.EPM.ProblemMaker> grid0;
        protected override async Task OnInitializedAsync()
        {
            problemMakers = await EPMService.GetProblemMakers();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddProblemMaker>("Add ProblemMaker", null);
            await grid0.Reload();
        }

        protected async Task EditRow(EpmDashboard.Models.EPM.ProblemMaker args)
        {
            await DialogService.OpenAsync<EditProblemMaker>("Edit ProblemMaker", new Dictionary<string, object> { {"id", args.id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EpmDashboard.Models.EPM.ProblemMaker problemMaker)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EPMService.DeleteProblemMaker(problemMaker.id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                { 
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error", 
                    Detail = $"Unable to delete ProblemMaker" 
                });
            }
        }
    }
}