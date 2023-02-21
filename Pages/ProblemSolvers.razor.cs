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
    public partial class ProblemSolvers
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

        protected IEnumerable<EpmDashboard.Models.EPM.ProblemSolver> problemSolvers;

        protected RadzenDataGrid<EpmDashboard.Models.EPM.ProblemSolver> grid0;
        protected override async Task OnInitializedAsync()
        {
            problemSolvers = await EPMService.GetProblemSolvers(new Query { Expand = "EngineeringArea" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddProblemSolver>("Add ProblemSolver", null);
            await grid0.Reload();
        }

        protected async Task EditRow(EpmDashboard.Models.EPM.ProblemSolver args)
        {
            await DialogService.OpenAsync<EditProblemSolver>("Edit ProblemSolver", new Dictionary<string, object> { {"id", args.id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EpmDashboard.Models.EPM.ProblemSolver problemSolver)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EPMService.DeleteProblemSolver(problemSolver.id);

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
                    Detail = $"Unable to delete ProblemSolver" 
                });
            }
        }
    }
}