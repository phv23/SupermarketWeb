using Microsoft.EntityFrameworkCore;
using Data_Access.Context;
using Data_Access.Entities;

namespace Data_Access.Repositories.Impl
{
    public class CategoryRepository : GenericRepository<CategoryEntity>, ICategoryRepository
    {
        private readonly MyDbContext _context;
        public CategoryRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryEntity>> FindAll()
        {
            return await Find();
        }

        public async Task<CategoryEntity?> FindOne(int Id)
        {
            var result = await Find(x => x.Id == Id, true);
            return result.FirstOrDefault();
        }
        public async Task<CategoryEntity?> FindByCode(string Code)
        {
            var result = await Find(x => x.Code == Code, true);
            return result.FirstOrDefault();
        }

        public async Task<int?> Save(CategoryEntity category)
        {
            await _context.Categories.AddAsync(category);
            // Lưu các thay đổi vào CSDL. Khi đó categoryEntity.id sẽ được gán giá trị mới - đồng bộ với dữ liệu có trong CSDL
            await _context.SaveChangesAsync();
            // Vì trường id tự động tăng trong CSDL, mà id trong categoryEntity đồng bộ với bảng trong CSDL thông qua
            //hàm SaveChangesAsync nên tự tin return ra id mà không cần truy vấn lại
            return category.Id;
        }
        public async Task<int?> Update(CategoryEntity category)
        {
            _context.Categories.Update(category);
            // Lưu các thay đổi vào CSDL. Khi đó categoryEntity.id sẽ được gán giá trị mới - đồng bộ với dữ liệu có trong CSDL
            await _context.SaveChangesAsync();
            // Vì trường id tự động tăng trong CSDL, mà id trong categoryEntity đồng bộ với bảng trong CSDL thông qua
            //hàm SaveChangesAsync nên tự tin return ra id mà không cần truy vấn lại
            return category.Id;
        }

        public async Task Delete(CategoryEntity category)
        {
            _context.Categories.Remove(category); // Ensure _context is used correctly
        }
    }
}
