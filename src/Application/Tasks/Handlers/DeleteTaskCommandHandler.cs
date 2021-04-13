using AppDomain.Common.Interfaces;
using AppDomain.Entities;
using Application.Common.Exceptions;
using Application.Tasks.Commands.DeleteTask;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks.Handlers
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IRepository<ToDoTask, int> _taskRepository;

        public DeleteTaskCommandHandler(IRepository<ToDoTask, int> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _taskRepository.GetFirst(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ToDoTask), request.Id);
            }

            await _taskRepository.Delete(entity);

            await _taskRepository.Commit(cancellationToken);

            return Unit.Value;
        }
    }
}
