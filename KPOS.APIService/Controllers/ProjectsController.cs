using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPCOS.Data.Models;
using KPCOS.Service;
using KPCOS.Service.Base;

namespace KPOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        //private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context;
        private readonly IProjectService _service;

        public ProjectsController(IProjectService context)
        {
            _service = context;
        }

        // GET: api/Projects
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        //{
        //    return await _service.GetAll()

        //}
        public async Task<IProjectResult> GetProjects()
        {
            return await _service.GetAll();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<IProjectResult> GetProjectById(string id)
        {
           return await _service.GetById(id);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IProjectResult> UpdateProject( Project project)
        {
           return  await _service.Save(project);
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IProjectResult> CreateProject(Project project)
        {
            return await _service.Create(project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IProjectResult> DeleteProject(string id)
        {
            return await _service.DeleteById(id);
        }

        //private bool ProjectExists(string id)
        //{
        //    return _context.Projects.Any(e => e.Id == id);
        //}
    }
}
