using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_App.Application.Dtos.BasketDtos
{
    public class CreateBasketDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int MenuTableId { get; set; }
    }
}
