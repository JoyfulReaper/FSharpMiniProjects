using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.ApiClient.Contracts;

namespace TaskTracker.ApiClient.HttpClients;
public interface ITaskClient
{
    Task<IEnumerable<TaskResponse>> GetAll();
    Task<TaskResponse> Get(Guid id);
    Task<TaskResponse> Create(TaskRequest taskCreateRequest);
    Task<TaskResponse> Update(TaskUpdateRequest taskUpdateRequest);
    Task Delete(Guid id);
}
