namespace SimpleDemo.Domain.DbEntity.Common
{
    public interface IAuditable
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }
    }
}