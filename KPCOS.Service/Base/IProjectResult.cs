using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPCOS.Service.Base
{
    public interface IProjectResult
    {
        int Status { get; set; }
        string? Message { get; set; }
        object? Data { get; set; }

    }

    public class ProjectResult : IProjectResult
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public ProjectResult()
        {
            Status = -1;
            Message = "Action fail";
        }

        public ProjectResult(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public ProjectResult(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
