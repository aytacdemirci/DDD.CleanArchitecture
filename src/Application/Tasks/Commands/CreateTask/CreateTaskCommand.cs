using AppDomain.Entities;
using AppDomain.Enums;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;

namespace Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<int>, IMapTo<ToDoTask>
    {
        public string Name { get; set; }

        public TaskState State { get; set; }

        public TaskPriority Priority { get; set; }

        public int? AssignedPersonId { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<CreateTaskCommand, ToDoTask>()
                .ForMember(d => d.AssignedPersonId, o => o.Ignore());
    }

 
}
