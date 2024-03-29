﻿using AppDomain.Enums;
using Application.Tasks.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Tasks.ViewModels
{
    public class TasksVm
    {
        public IList<EnumValueDto> TaskPriorities { get; } =
            Enum.GetValues(typeof(TaskPriority))
                .Cast<TaskPriority>()
                .Select(p => new EnumValueDto { Value = (int)p, Name = p.ToString() })
                .ToList();

        public IList<EnumValueDto> TaskStates =
            Enum.GetValues(typeof(TaskState))
                .Cast<TaskState>()
                .Select(p => new EnumValueDto { Value = (int)p, Name = p.ToString() })
                .ToList();

        public IList<TaskDto> Tasks { get; set; }
    }
}
