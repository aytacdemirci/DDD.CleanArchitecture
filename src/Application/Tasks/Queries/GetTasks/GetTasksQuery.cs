
using Application.Tasks.ViewModels;
using MediatR;

namespace Application.Tasks.Queries.GetTasks
{
    public class GetTasksQuery : IRequest<TasksVm>
    {
        public string Name { get; set; }
    }
}
