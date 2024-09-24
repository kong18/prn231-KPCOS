using KPCOS.Common;
using KPCOS.Data.Models;
using KPCOS.Data;
using KPCOS.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service
{
    public interface IDesignService
    {
        Task<IProjectResult> GetAll();
        Task<IProjectResult> GetById(string code);
        Task<IProjectResult> Create(Design design);
        Task<IProjectResult> Update(Design design);
        Task<IProjectResult> Save(Design design);
        Task<IProjectResult> DeleteById(string code);
    }

    public class DesignService : IDesignService
    {
        private readonly UnitOfWork _unitOfWork;

        public DesignService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IProjectResult> Create(Design design)
        {
            try
            {
                var existingProject = _unitOfWork.Design.GetById(design.Id);
                if (existingProject == null)
                {
                    var isSuscces = await _unitOfWork.Design.CreateAsync(design);
                    if (isSuscces > 0)
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
            }
            catch (Exception ex)
            {
                return new ProjectResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, ex.ToString());
            }
        }

        public async Task<IProjectResult> DeleteById(string code)
        {
            var existProject = await _unitOfWork.Design.GetByIdAsync(code);
            if (existProject == null)
            {
                return new ProjectResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                try
                {
                    var result = await _unitOfWork.Design.RemoveAsync(existProject);

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
                    return new ProjectResult(Const.ERROR_EXCEPTION, ex.ToString());
                }
            }


        }

        public async Task<IProjectResult> GetAll()
        {
            var project = await _unitOfWork.Design.GetAllAsync();
            if (project == null)
            {
                return new ProjectResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new ProjectResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, project);
        }

        public async Task<IProjectResult> GetById(string code)
        {
            var project = await _unitOfWork.Design.GetByIdAsync(code);
            if (project == null)
            {
                return new ProjectResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            return new ProjectResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, project);
        }

        public async Task<IProjectResult> Save(Design design)
        {
            try
            {
                int result = -1;
                var projectTmp = _unitOfWork.Project.GetById(design.Id);
                if (projectTmp != null)
                {
                    result = await _unitOfWork.Project.UpdateAsync(projectTmp);
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
                    result = await _unitOfWork.Design.CreateAsync(design);
                    if (result > 0)
                    {
                        return new ProjectResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);

                    }
                    else
                    {
                        return new ProjectResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }

            }
            catch (Exception ex)
            {
                return new ProjectResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IProjectResult> Update(Design design)
        {
            try
            {

                var existingProject = await _unitOfWork.Design.GetByIdAsync(design.Id);

                if (existingProject != null)
                {

                    int result = await _unitOfWork.Design.UpdateAsync(existingProject);

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
