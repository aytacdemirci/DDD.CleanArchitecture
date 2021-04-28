using System;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        long? UserId { get; }
        public String GetUserId { get; }
    }
}
