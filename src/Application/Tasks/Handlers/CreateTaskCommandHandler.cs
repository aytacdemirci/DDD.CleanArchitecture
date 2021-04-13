using AppDomain.Common.Interfaces;
using AppDomain.Entities;
using AppDomain.Enums;
using AppDomain.Services;
using Application.Common.Mappings;
using Application.Tasks.Commands.CreateTask;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks.Handlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IRepository<ToDoTask, int> _taskRepository;
        private readonly IRepository<Person, int> _personRepository;
        private readonly ITaskManger _taskManager;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(IRepository<ToDoTask, int> taskRepository, IRepository<Person, int> personRepository, ITaskManger taskManager, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
            _taskManager = taskManager;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<ToDoTask>(request);

            if (request.AssignedPersonId != null)
            {
                var person = await _personRepository.GetFirst(request.AssignedPersonId.Value);
                await _taskManager.AssignTaskToPerson(task, person);
            }

            await _taskRepository.Add(task);

            await _taskRepository.Commit(cancellationToken);

            return task.Id;
        }
    }
}
