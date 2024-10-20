using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities.Ready
{
    [Table("tb_Stock")]
    public class StockEntity : AbstractEntity<StockEntity>
    {
        public int Quantity { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
        public StockEntity()
        {
            Products = new HashSet<ProductEntity>();
        }
    }
}
