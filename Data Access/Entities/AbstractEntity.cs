using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    // Là lớp Entity chứa các thuộc tính chung, mà các Entity khác có thể kế thừa. Nhằm tránh tình trạng code nhiều
    //VD: User có trường Id mà Product cũng có trường Id. Như vậy cần phải code 2 lần. Thay vì thế, tạo 1 lớp chứa Id và cho User và Product kế thừa
    public class AbstractEntity<T> where T : class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
