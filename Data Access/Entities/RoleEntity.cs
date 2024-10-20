using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    [Table("role")]
    public class RoleEntity : AbstractEntity<RoleEntity>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        // Đại diện cho mối quan hệ 1-n giữa RoleEntity và UserEntity
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
