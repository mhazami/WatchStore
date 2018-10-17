using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO.ViewModels
{
    public class VideoProduct
    {
        public List<VideoHandler> VideoList { get; set; }

        public List<Product> ProductsList { get; set; }
    }
}
