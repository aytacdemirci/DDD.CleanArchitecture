using AppDomain.Common.Interfaces;
using AppDomain.Entities;
using Application.Tasks.Commands.UpdateTask;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks.Validators
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        private readonly IRepository<ToDoTask, int> _todoListRepository;

        public UpdateTaskCommandValidator(IRepository<ToDoTask, int> todoListRepository)
        {
            _todoListRepository = todoListRepository;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 50 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified name is already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdateTaskCommand model, string name, CancellationToken cancellationToken)
        {
            return await _todoListRepository.GetAll()
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Name != name, cancellationToken);
        }
    }
}
