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
    public partial class AddProblemMaker
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

        protected override async Task OnInitializedAsync()
        {
            problemMaker = new EpmDashboard.Models.EPM.ProblemMaker();
        }
        protected bool errorVisible;
        protected EpmDashboard.Models.EPM.ProblemMaker problemMaker;

        protected async Task FormSubmit()
        {
            try
            {
                await EPMService.CreateProblemMaker(problemMaker);
                DialogService.Close(problemMaker);
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