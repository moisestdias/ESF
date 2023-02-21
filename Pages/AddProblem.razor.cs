using EpmDashboard.Models.EPM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;

namespace EpmDashboard.Pages
{
    public partial class AddProblem
    {
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        [Inject] protected NavigationManager NavigationManager { get; set; }

        [Inject] protected DialogService DialogService { get; set; }

        [Inject] protected TooltipService TooltipService { get; set; }

        [Inject] protected ContextMenuService ContextMenuService { get; set; }

        [Inject] protected NotificationService NotificationService { get; set; }
        [Inject] public EPMService EPMService { get; set; }

        [Inject] protected SecurityService Security { get; set; }

        protected override async Task OnInitializedAsync()
        {
            problem = new Problem();

            problemMakersForProblemMakerid = await EPMService.GetProblemMakers();

            problemSolversForProblemSolverid = await EPMService.GetProblemSolvers();
        }

        protected bool errorVisible;
        protected Problem problem;

        protected IEnumerable<ProblemMaker> problemMakersForProblemMakerid;

        protected IEnumerable<ProblemSolver> problemSolversForProblemSolverid;

        protected async Task FormSubmit()
        {
            try
            {
                var problemMaker = problemMakersForProblemMakerid.Where(x => x.ApplicationUser.Id == Security.User.Id)
                    .FirstOrDefault();
                problem.ProblemMaker = problemMaker;
                problem.createtime = DateTime.UtcNow;
                problem.updatetime = DateTime.UtcNow;
                await EPMService.CreateProblem(problem);
                DialogService.Close(problem);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}