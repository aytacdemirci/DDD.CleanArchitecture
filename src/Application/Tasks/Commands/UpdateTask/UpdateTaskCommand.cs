using System.Threading;
using System.Threading.Tasks;
using AppDomain.Common.Interfaces;
using AppDomain.Entities;
using AppDomain.Enums;
using Application.Common.Exceptions;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;

namespace Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest, IMapTo<ToDoTask>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TaskPriority Priority { get; set; }

        public bool IsComplete { get; set; }
    }

}
