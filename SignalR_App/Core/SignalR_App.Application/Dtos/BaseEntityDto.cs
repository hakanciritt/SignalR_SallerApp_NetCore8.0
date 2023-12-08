namespace SignalR_App.Application.Dtos
{
    public abstract class BaseEntityDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
    }
    public abstract class BaseEntityDto<TPrimaryKey> where TPrimaryKey : struct
    {
        public TPrimaryKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
    }
}
