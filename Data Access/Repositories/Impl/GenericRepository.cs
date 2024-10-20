using Microsoft.EntityFrameworkCore;
using Data_Access.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories.Impl
{
    // Là triển khai của interface IGenericRepository
    public class GenericRepository<T> where T : class
    {
        private readonly MyDbContext _myDbContext;

        public GenericRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }


        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression = null, bool findOne = false)
        {
            if (expression == null)
            {
                // Trả về tất cả Entity tương ứng
                return await _myDbContext.Set<T>().ToListAsync();
            }

            if (findOne)
            {
                // Nếu findOne là true, trả về 1 Entity hoặc null nếu không thấy
                var result = await _myDbContext.Set<T>().SingleOrDefaultAsync(expression);
                return result == null ? Enumerable.Empty<T>() : new List<T> { result };
            }

            // Trả về list Entity thỏa điều kiện
            return await _myDbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task Create(T entity)
        {
            await _myDbContext.Set<T>().AddAsync(entity);

        }

        public void Update(T entity)
        {
            // Gán entity vào DbContext để EF bắt đầu theo dõi. Để gán được thì cần entity này phải
            //có trước đó. Mặc định sự thay đổi đó là Unchanged - không có bất kì thay đổi nào. Nhưng ở hàm này cần cập nhật entity
            //nên cần phải thay đổi state thành Modified, để báo cho EF biết rằng entity đã có sự thay đổi và EF sẽ tạo lệnh SQL
            //để cập nhật lại
            _myDbContext.Set<T>().Attach(entity);
            _myDbContext.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            // Gán entity vào DbContext để EF bắt đầu theo dõi. Để gán được thì cần entity này phải
            //có trước đó. Mặc định sự thay đổi đó là Unchanged - không có bất kì thay đổi nào. Nhưng ở hàm này cần xóa entity
            //nên cần phải thay đổi state thành Deleted, để báo cho EF biết rằng không cần entity này nữa và EF sẽ tạo lệnh SQL
            //để xóa
            _myDbContext.Set<T>().Attach(entity);
            _myDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task Commit()
        {
            // Chỉ khi gọi lệnh SaveChangesAsync thì EF mới thực sự lưu các thay đổi vào CSDL
            await _myDbContext.SaveChangesAsync();
        }
    }
}
