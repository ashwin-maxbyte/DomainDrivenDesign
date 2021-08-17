using DDD.Base.Extensions;
using System;

namespace DDD.Base.Queries.Pagination
{
    public class InvalidPaginationContextException : Exception
    {
        public int? Offset { get; }
        public int? Limit { get; }
        public override string Message { get; }

        public InvalidPaginationContextException(int? offset, int? limit)
        {
            Offset = offset;
            Limit = limit;
            Message = "Invalid Pagination Context Provided with limit @0 and offset @1".AttachParams(offset, limit);
        }
    }
}
