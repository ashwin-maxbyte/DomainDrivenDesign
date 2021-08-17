namespace DDD.Base.Queries.Pagination
{
    public class PaginationContext
    {
        public int Offset { get; }
        public int Limit { get; }

        private PaginationContext(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }

        public static PaginationContext NewPaginationContext(int? offset, int? limit)
        {
            if(offset == null && limit == null)
            {
                return null;
            }
            else if(offset == null || limit == null || limit <= 0 || offset < 0)
            {
                throw new InvalidPaginationContextException(offset, limit);
            }
            else
            {
                return new PaginationContext(offset.Value, limit.Value);
            }
        }
    }
}
