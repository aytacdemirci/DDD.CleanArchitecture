using AppDomain.Common.Interfaces;
using AppDomain.Entities;
using Application.Tasks.Commands.CreateTask;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks.Validators
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        private readonly IRepository<ToDoTask, int> _taskRepository;

        public CreateTaskCommandValidator(IRepository<ToDoTask, int> taskRepository)
        {
            _taskRepository = taskRepository;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified name already exists.");
        }

        public async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
        {
            return await _taskRepository.All(l => l.Name != name);
        }
    }
}
