using Data_Access.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entities;

namespace Data_Access.Repositories
{
    // Kế thừa IGenericRepository<CategoryEntity> vì cần các method chung và bắt buộc class triển khai của ICategoryRepository
    //phải triển khai các method chung đó. Tuy nhiên, vì CategoryRepository kế thừa lại GenericRepository<CategoryEntity> nên khi 
    //CategoryRepository triển khai lại của ICategoryRepository, thì không cần phải code lại các method chung nữa, vì nó đã kế thừa
    //từ GenericRepository<CategoryEntity> rồi
    public interface ICategoryRepository : IGenericRepository<CategoryEntity>
    {
        // Tìm 1 entity Category theo Id
        Task<CategoryEntity?> FindOne(int id);

        // Tìm 1 entity Category dựa theo Code
        Task<CategoryEntity?> FindByCode(string Code);

        //Tìm tất cả entity Category
        Task<IEnumerable<CategoryEntity>> FindAll();

        // Lưu Entity mới vào CSDL. Cần trả về int là vì cần có id để lấy thông tin xác thực, kiểm định lại data đã thực sự
        //được lưu đúng hay chưa
        Task<int?> Save(CategoryEntity category);

        // Cập nhật entity. Cần trả về int là vì cần có id để lấy thông tin xác thực, kiểm định lại data đã thực sự
        //được lưu đúng hay chưa
        Task<int?> Update(CategoryEntity category);

        // Xóa entity
        Task Delete(CategoryEntity category);
    }
}
