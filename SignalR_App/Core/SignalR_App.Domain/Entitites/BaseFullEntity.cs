namespace SignalR_App.Domain.Entitites
{
    public class BaseFullEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DelationTime { get; set; }
    }

    public abstract class BaseFullEntity<TPrimaryKey> : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        public bool IsDeleted { get; set; }
        public DateTime? DelationTime { get; set; }
    }

}
