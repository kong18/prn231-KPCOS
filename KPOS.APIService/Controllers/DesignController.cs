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
    public class DesignController : ControllerBase
    {
        //private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context;
        private readonly IDesignService _service;

        public DesignController(IDesignService context)
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
        public async Task<IProjectResult> UpdateProject(Design design)
        {
           return  await _service.Save(design);
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IProjectResult> CreateProject(Design design)
        {
            return await _service.Create(design);
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
