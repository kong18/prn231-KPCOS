using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service
{
    public interface IProjectService
    {
        Task<IProjectResult> GetAll();
        Task<IProjectResult> GetById(string code);
        Task<IProjectResult> Create(Project project );
        Task<IProjectResult> Update(Project project);
        Task<IProjectResult> Save(Project project);
        Task<IProjectResult> DeleteById(string code);
    }

    public class ProjectService : IProjectService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProjectService() { 
            _unitOfWork ??= new UnitOfWork();
        } 

        public Task<IProjectResult> Create(Project project)
        {
            throw new NotImplementedException();
        }

        public Task<IProjectResult> DeleteById(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<IProjectResult> GetAll()
        {
           var project = await _unitOfWork.Project.GetAllAsync();
            if(project == null) {
                return new ProjectResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            
            
        }

        public Task<IProjectResult> GetById(string code)
        {
            throw new NotImplementedException();
        }

        public Task<IProjectResult> Save(Project project)
        {
            throw new NotImplementedException();
        }

        public Task<IProjectResult> Update(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
