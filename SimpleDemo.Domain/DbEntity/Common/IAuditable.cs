namespace SimpleDemo.Domain.DbEntity.Common
{
    public interface IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }
    }
}