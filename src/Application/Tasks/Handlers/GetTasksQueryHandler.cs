﻿using AppDomain.Common.Interfaces;
using AppDomain.Entities;
using Application.Tasks.DTO;
using Application.Tasks.Queries.GetTasks;
using Application.Tasks.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks.Handlers
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, TasksVm>
    {
        private readonly IRepository<ToDoTask, int> _taskRepository;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(IMapper mapper, IRepository<ToDoTask, int> taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<TasksVm> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var vm = new TasksVm
            {
                Tasks = await _taskRepository.GetAll().Where(t => t.Name.Contains(request.Name))
                    .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)
            };

            return vm;
        }
    }
}
