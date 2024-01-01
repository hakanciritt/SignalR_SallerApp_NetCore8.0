namespace SignalR_App.Domain.Entitites
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? LastModificationDate { get; set; }
    }
    public abstract class BaseEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        public TPrimaryKey Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? LastModificationDate { get; set; }
    }
}
