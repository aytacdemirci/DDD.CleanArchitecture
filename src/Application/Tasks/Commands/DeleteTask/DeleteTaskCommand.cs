using System.Threading;
using System.Threading.Tasks;
using AppDomain.Common.Interfaces;
using AppDomain.Entities;
using Application.Common.Exceptions;
using MediatR;

namespace Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest
    {
        public int Id { get; set; }
    }

}
