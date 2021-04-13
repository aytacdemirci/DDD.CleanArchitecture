using AppDomain.Common.Interfaces;
using AppDomain.Entities;
using Application.Common.Exceptions;
using Application.Tasks.Commands.UpdateTask;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks.Handlers
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IRepository<ToDoTask, int> _taskRepository;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IRepository<ToDoTask, int> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetFirst(request.Id);

            if (task == null)
            {
                throw new NotFoundException(nameof(ToDoTask), request.Id);
            }

            _mapper.Map(request, task);


            if (request.IsComplete)
                task.MarkComplete();
            else
                task.MarkUnComplete();

            await _taskRepository.Commit(cancellationToken);

            return Unit.Value;
        }
    }
}
