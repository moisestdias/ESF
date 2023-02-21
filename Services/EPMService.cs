using System.Linq.Dynamic.Core;
using System.Text.Encodings.Web;
using EpmDashboard.Data;
using EpmDashboard.Models.EPM;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace EpmDashboard
{
    public partial class EPMService
    {
        EPMContext Context
        {
            get { return this.context; }
        }

        private readonly EPMContext context;
        private readonly NavigationManager navigationManager;

        public EPMService(EPMContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList()
            .ForEach(e => e.State = EntityState.Detached);


        public async Task ExportProblemsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(
                query != null
                    ? query.ToUrl(
                        $"export/epm/problems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')")
                    : $"export/epm/problems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')",
                true);
        }

        public async Task ExportProblemsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(
                query != null
                    ? query.ToUrl(
                        $"export/epm/problems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')")
                    : $"export/epm/problems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')",
                true);
        }

        partial void OnProblemsRead(ref IQueryable<Problem> items);

        public async Task<IQueryable<Problem>> GetProblems(Query query = null)
        {
            var items = Context.Problems.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProblemsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProblemGet(Problem item);

        public async Task<Problem> GetProblemById(int id)
        {
            var items = Context.Problems
                .AsNoTracking()
                .Where(i => i.id == id);

            items = items.Include(i => i.ProblemMaker);
            items = items.Include(i => i.ProblemSolver);

            var itemToReturn = items.FirstOrDefault();

            OnProblemGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnProblemCreated(Problem item);
        partial void OnAfterProblemCreated(Problem item);

        public async Task<Problem> CreateProblem(Problem problem)
        {
            OnProblemCreated(problem);

            var existingItem = Context.Problems
                .Where(i => i.id == problem.id)
                .FirstOrDefault();

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.Problems.Add(problem);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(problem).State = EntityState.Detached;
                throw;
            }

            OnAfterProblemCreated(problem);

            return problem;
        }

        public async Task<Problem> CancelProblemChanges(Problem item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProblemUpdated(Problem item);
        partial void OnAfterProblemUpdated(Problem item);

        public async Task<Problem> UpdateProblem(int id, Problem problem)
        {
            OnProblemUpdated(problem);

            var itemToUpdate = Context.Problems
                .Where(i => i.id == problem.id)
                .FirstOrDefault();

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(problem);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterProblemUpdated(problem);

            return problem;
        }

        partial void OnProblemDeleted(Problem item);
        partial void OnAfterProblemDeleted(Problem item);

        public async Task<Problem> DeleteProblem(int id)
        {
            var itemToDelete = Context.Problems
                .Where(i => i.id == id)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnProblemDeleted(itemToDelete);


            Context.Problems.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProblemDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportProblemMakersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(
                query != null
                    ? query.ToUrl(
                        $"export/epm/problemmakers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')")
                    : $"export/epm/problemmakers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')",
                true);
        }

        public async Task ExportProblemMakersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(
                query != null
                    ? query.ToUrl(
                        $"export/epm/problemmakers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')")
                    : $"export/epm/problemmakers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')",
                true);
        }

        partial void OnProblemMakersRead(ref IQueryable<ProblemMaker> items);

        public async Task<IQueryable<ProblemMaker>> GetProblemMakers(Query query = null)
        {
            var items = Context.ProblemMakers
                .Include(x => x.ApplicationUser).AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProblemMakersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProblemMakerGet(ProblemMaker item);

        public async Task<ProblemMaker> GetProblemMakerById(int id)
        {
            var items = Context.ProblemMakers
                .AsNoTracking()
                .Where(i => i.id == id);


            var itemToReturn = items.FirstOrDefault();

            OnProblemMakerGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnProblemMakerCreated(ProblemMaker item);
        partial void OnAfterProblemMakerCreated(ProblemMaker item);

        public async Task<ProblemMaker> CreateProblemMaker(ProblemMaker problemmaker)
        {
            OnProblemMakerCreated(problemmaker);

            var existingItem = Context.ProblemMakers
                .Where(i => i.id == problemmaker.id)
                .FirstOrDefault();

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.ProblemMakers.Add(problemmaker);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(problemmaker).State = EntityState.Detached;
                throw;
            }

            OnAfterProblemMakerCreated(problemmaker);

            return problemmaker;
        }

        public async Task<ProblemMaker> CancelProblemMakerChanges(ProblemMaker item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProblemMakerUpdated(ProblemMaker item);
        partial void OnAfterProblemMakerUpdated(ProblemMaker item);

        public async Task<ProblemMaker> UpdateProblemMaker(int id, ProblemMaker problemmaker)
        {
            OnProblemMakerUpdated(problemmaker);

            var itemToUpdate = Context.ProblemMakers
                .Where(i => i.id == problemmaker.id)
                .FirstOrDefault();

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(problemmaker);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterProblemMakerUpdated(problemmaker);

            return problemmaker;
        }

        partial void OnProblemMakerDeleted(ProblemMaker item);
        partial void OnAfterProblemMakerDeleted(ProblemMaker item);

        public async Task<ProblemMaker> DeleteProblemMaker(int id)
        {
            var itemToDelete = Context.ProblemMakers
                .Where(i => i.id == id)
                .Include(i => i.Problems)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnProblemMakerDeleted(itemToDelete);


            Context.ProblemMakers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProblemMakerDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportProblemSolversToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(
                query != null
                    ? query.ToUrl(
                        $"export/epm/problemsolvers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')")
                    : $"export/epm/problemsolvers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')",
                true);
        }

        public async Task ExportProblemSolversToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(
                query != null
                    ? query.ToUrl(
                        $"export/epm/problemsolvers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')")
                    : $"export/epm/problemsolvers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')",
                true);
        }

        partial void OnProblemSolversRead(ref IQueryable<ProblemSolver> items);

        public async Task<IQueryable<ProblemSolver>> GetProblemSolvers(Query query = null)
        {
            var items = Context.ProblemSolvers.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProblemSolversRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProblemSolverGet(ProblemSolver item);

        public async Task<ProblemSolver> GetProblemSolverById(int id)
        {
            var items = Context.ProblemSolvers
                .AsNoTracking()
                .Where(i => i.id == id);

            items = items.Include(i => i.EngineeringArea);

            var itemToReturn = items.FirstOrDefault();

            OnProblemSolverGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnProblemSolverCreated(ProblemSolver item);
        partial void OnAfterProblemSolverCreated(ProblemSolver item);

        public async Task<ProblemSolver> CreateProblemSolver(ProblemSolver problemsolver)
        {
            OnProblemSolverCreated(problemsolver);

            var existingItem = Context.ProblemSolvers
                .Where(i => i.id == problemsolver.id)
                .FirstOrDefault();

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.ProblemSolvers.Add(problemsolver);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(problemsolver).State = EntityState.Detached;
                throw;
            }

            OnAfterProblemSolverCreated(problemsolver);

            return problemsolver;
        }

        public async Task<ProblemSolver> CancelProblemSolverChanges(ProblemSolver item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProblemSolverUpdated(ProblemSolver item);
        partial void OnAfterProblemSolverUpdated(ProblemSolver item);

        public async Task<ProblemSolver> UpdateProblemSolver(int id, ProblemSolver problemsolver)
        {
            OnProblemSolverUpdated(problemsolver);

            var itemToUpdate = Context.ProblemSolvers
                .Where(i => i.id == problemsolver.id)
                .FirstOrDefault();

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(problemsolver);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterProblemSolverUpdated(problemsolver);

            return problemsolver;
        }

        partial void OnProblemSolverDeleted(ProblemSolver item);
        partial void OnAfterProblemSolverDeleted(ProblemSolver item);

        public async Task<ProblemSolver> DeleteProblemSolver(int id)
        {
            var itemToDelete = Context.ProblemSolvers
                .Where(i => i.id == id)
                .Include(i => i.Problems)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnProblemSolverDeleted(itemToDelete);


            Context.ProblemSolvers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProblemSolverDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportEngineeringAreasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(
                query != null
                    ? query.ToUrl(
                        $"export/epm/engineeringareas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')")
                    : $"export/epm/engineeringareas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')",
                true);
        }

        public async Task ExportEngineeringAreasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(
                query != null
                    ? query.ToUrl(
                        $"export/epm/engineeringareas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')")
                    : $"export/epm/engineeringareas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')",
                true);
        }

        partial void OnEngineeringAreasRead(ref IQueryable<EngineeringArea> items);

        public async Task<IQueryable<EngineeringArea>> GetEngineeringAreas(Query query = null)
        {
            var items = Context.EngineeringAreas.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnEngineeringAreasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEngineeringAreaGet(EngineeringArea item);

        public async Task<EngineeringArea> GetEngineeringAreaById(int id)
        {
            var items = Context.EngineeringAreas
                .AsNoTracking()
                .Where(i => i.id == id);


            var itemToReturn = items.FirstOrDefault();

            OnEngineeringAreaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEngineeringAreaCreated(EngineeringArea item);
        partial void OnAfterEngineeringAreaCreated(EngineeringArea item);

        public async Task<EngineeringArea> CreateEngineeringArea(EngineeringArea engineeringarea)
        {
            OnEngineeringAreaCreated(engineeringarea);

            var existingItem = Context.EngineeringAreas
                .Where(i => i.id == engineeringarea.id)
                .FirstOrDefault();

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.EngineeringAreas.Add(engineeringarea);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(engineeringarea).State = EntityState.Detached;
                throw;
            }

            OnAfterEngineeringAreaCreated(engineeringarea);

            return engineeringarea;
        }

        public async Task<EngineeringArea> CancelEngineeringAreaChanges(EngineeringArea item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEngineeringAreaUpdated(EngineeringArea item);
        partial void OnAfterEngineeringAreaUpdated(EngineeringArea item);

        public async Task<EngineeringArea> UpdateEngineeringArea(int id, EngineeringArea engineeringarea)
        {
            OnEngineeringAreaUpdated(engineeringarea);

            var itemToUpdate = Context.EngineeringAreas
                .Where(i => i.id == engineeringarea.id)
                .FirstOrDefault();

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(engineeringarea);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEngineeringAreaUpdated(engineeringarea);

            return engineeringarea;
        }

        partial void OnEngineeringAreaDeleted(EngineeringArea item);
        partial void OnAfterEngineeringAreaDeleted(EngineeringArea item);

        public async Task<EngineeringArea> DeleteEngineeringArea(int id)
        {
            var itemToDelete = Context.EngineeringAreas
                .Where(i => i.id == id)
                //.Include(i => i.ProblemSolvers)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnEngineeringAreaDeleted(itemToDelete);


            Context.EngineeringAreas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEngineeringAreaDeleted(itemToDelete);

            return itemToDelete;
        }
    }
}