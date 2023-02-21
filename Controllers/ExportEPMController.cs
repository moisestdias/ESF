using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using EpmDashboard.Data;

namespace EpmDashboard.Controllers
{
    public partial class ExportEPMController : ExportController
    {
        private readonly EPMContext context;
        private readonly EPMService service;

        public ExportEPMController(EPMContext context, EPMService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/EPM/problems/csv")]
        [HttpGet("/export/EPM/problems/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProblemsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProblems(), Request.Query), fileName);
        }

        [HttpGet("/export/EPM/problems/excel")]
        [HttpGet("/export/EPM/problems/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProblemsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProblems(), Request.Query), fileName);
        }

        [HttpGet("/export/EPM/problemmakers/csv")]
        [HttpGet("/export/EPM/problemmakers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProblemMakersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProblemMakers(), Request.Query), fileName);
        }

        [HttpGet("/export/EPM/problemmakers/excel")]
        [HttpGet("/export/EPM/problemmakers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProblemMakersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProblemMakers(), Request.Query), fileName);
        }

        [HttpGet("/export/EPM/problemsolvers/csv")]
        [HttpGet("/export/EPM/problemsolvers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProblemSolversToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProblemSolvers(), Request.Query), fileName);
        }

        [HttpGet("/export/EPM/problemsolvers/excel")]
        [HttpGet("/export/EPM/problemsolvers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProblemSolversToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProblemSolvers(), Request.Query), fileName);
        }

        [HttpGet("/export/EPM/engineeringareas/csv")]
        [HttpGet("/export/EPM/engineeringareas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEngineeringAreasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEngineeringAreas(), Request.Query), fileName);
        }

        [HttpGet("/export/EPM/engineeringareas/excel")]
        [HttpGet("/export/EPM/engineeringareas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEngineeringAreasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEngineeringAreas(), Request.Query), fileName);
        }
    }
}
