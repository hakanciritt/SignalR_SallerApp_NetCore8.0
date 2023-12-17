using AutoMapper;

namespace SignalR_App.Application.Mappings
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(config =>
            {
                config.AddProfiles(new List<Profile>()
                {
                    new BookingMap(),
                    new CategoryMap(),
                    new MetaMap(),
                    new ProductMap(),
                    new SettingMap(),
                    new TestimonialMap(),
                    new TextContentMap(),
                    new BaseEntityMap(),
                    new BaseFullEntityMap(),
                    new OrderItemMap(),
                    new OrderMap(),
                });
            });

            return config.CreateMapper();
        });

        public static IMapper Map => lazy.Value;
    }
}
