using KPCOS.Common;
using KPCOS.Data;
using KPCOS.Data.Models;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
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

        public async Task<IProjectResult> Create(Project project)
        {
            try
            {
                var existingProject = _unitOfWork.Project.GetById(project.Id);
                if(existingProject == null) {
                    var isSuscces = await _unitOfWork.Project.CreateAsync(project);
                    if(isSuscces > 0)
                    {
                        return new ProjectResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);

                    }
                    else
                    {
                        return new ProjectResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }

                }
                else
                {
                    return new ProjectResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }catch (Exception ex)
            {
                return new ProjectResult(Const.FAIL_CREATE_CODE,Const.FAIL_CREATE_MSG,ex.ToString());  
            }
        }

        public async Task<IProjectResult> DeleteById(string code)
        {
            var existProject = await _unitOfWork.Project.GetByIdAsync(code);    
            if( existProject == null ) {
                return new ProjectResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                try {
                    var result = await _unitOfWork.Project.RemoveAsync(existProject);

                    if (result)
                    {
                        return new ProjectResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new ProjectResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                catch (Exception ex)
                {
                    return new ProjectResult(Const.ERROR_EXCEPTION,ex.ToString());
                }
            }
          
                
        }

        public async Task<IProjectResult> GetAll()
        {
           var project = await _unitOfWork.Project.GetAllAsync();
            if(project == null) {
                return new ProjectResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new ProjectResult(Const.SUCCESS_READ_CODE,Const.SUCCESS_READ_MSG,project);
        }

        public async Task<IProjectResult> GetById(string code)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(code);
            if (project == null)
            {
                return new ProjectResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            return new ProjectResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, project);
        }

        public async Task<IProjectResult> Save(Project project)
        {
            try
            {
                int result = -1;
                var projectTmp =_unitOfWork.Project.GetById(project.Id);
                if(projectTmp != null)
                {
                    result = await _unitOfWork.Project.UpdateAsync(projectTmp);
                    if(result > 0)
                    {
                        return new ProjectResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                    }
                    else
                    {
                        return new ProjectResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.Project.CreateAsync(project);
                    if(result > 0)
                    {
                        return new ProjectResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);

                    }
                    else
                    {
                        return new ProjectResult(Const.FAIL_CREATE_CODE,Const.FAIL_CREATE_MSG); 
                    }
                }

            }catch(Exception ex)
            {
                return new ProjectResult(Const.ERROR_EXCEPTION,ex.ToString());
            }
        }

        public async Task<IProjectResult> Update(Project project)
        {
            try
            {
               
                var existingProject = await _unitOfWork.Project.GetByIdAsync(project.Id);

                if (existingProject != null)
                {
                    
                    int result = await _unitOfWork.Project.UpdateAsync(existingProject);

                    if (result > 0)
                    {
                        return new ProjectResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                    }
                    else
                    {
                        return new ProjectResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    return new ProjectResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {
                
                return new ProjectResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

    }
}
