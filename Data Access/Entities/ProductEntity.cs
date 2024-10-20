using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entities.Ready;

namespace Data_Access.Entities
{
    [Table("product")]
    public class ProductEntity : AbstractEntity<ProductEntity>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Detail { get; set; }
        public string ImageProduct { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public int Status { get; set; }
        public int CategoryID { get; set; }
        // Đại diện cho mối quan hệ n-1 giữa ProductEntity và CategoryEntity
        public virtual CategoryEntity Category { get; set; }

    }
}
