using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    [Table("category")]
    public class CategoryEntity : AbstractEntity<CategoryEntity>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        // Đại diện cho mối quan hệ 1-n giữa CategoryEntity và ProductEntity
        public ICollection<ProductEntity> Products { get; set; }
    }
}
