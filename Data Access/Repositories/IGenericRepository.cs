using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    // Là interface repository chung, dùng để thao tác CRUD (Create, Read, Update, Delete) với các Entity trong CSDL

    // Cần có <T> where T : class là để ám chỉ GenericRepository là 1 Generic Class. Tức là class chung chung. T có thể thay bằng bất 
    //kì lớp nào phù hợp với các thao tác bên trong. VD: khi gọi interface này: IGenericRepository<CategoryEntity> repository , thì mọi chữ T trong
    //Interface IGenericRepository sẽ được thay bằng CategoryEntity. Cụ thể hơn Task Create(T entity); sẽ thay bằng Task Create(CategoryEntity entity);
    public interface IGenericRepository<T> where T : class
    {
        // Hàm find: Tìm kiếm các Entity trong CSDL với điều kiện tùy chọn. Từ đó, các Repository kế thừa sẽ dựa vào các điều kiện để tạo ra
        //các method mong muốn. VD như từ method Find có thể được sử dụng lại trong method mong muốn như FindAll, FindOne, FindByName,...

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression = null, bool findOne = false);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task Commit();
    }
}
