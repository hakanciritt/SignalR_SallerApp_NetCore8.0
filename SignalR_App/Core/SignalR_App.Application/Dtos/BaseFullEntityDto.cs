namespace SignalR_App.Application.Dtos
{
    public class BaseFullEntityDto : BaseEntityDto
    {
        public bool IsDeleted { get; set; }
        public DateTime? DelationTime { get; set; }
    }

    public abstract class BaseFullEntityDto<TPrimaryKey> : BaseEntityDto<TPrimaryKey> where TPrimaryKey : struct
    {
        public bool IsDeleted { get; set; }
        public DateTime? DelationTime { get; set; }
    }
}
