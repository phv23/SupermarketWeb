using Data_Access.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Dùng để đăng ký các reponsitory ở đây thay vì trong Program.cs // ConfigurationService
namespace Data_Access.Repositories.Impl
{
    public class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly MyDbContext _myDbContext;

        private ICategoryRepository? _categoryRepository;
        private IProductRepository? _productRepository;

        public RepositoryManager(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        // Nếu chưa có instance của CategoryRepository - đối tượng được new từ contructor
        //( VD: User user= new User(); Sau lệnh này thì user đã trở thành 1 Instance ), sẽ tạo một instance mới,
        //Nếu đã có instance rồi thì dùng nó. Giúp không tạo nhiều đối tượng
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_myDbContext);

        // Tương tự như CategoryRepository
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_myDbContext);


        // Cần cân nhắc dùng hàm này!
        public async Task SaveChangeAsync()
        {
            await _myDbContext.SaveChangesAsync();
        }

        // Đảm bảo DbContext được giải phóng đúng cách khi RepositoryManager không còn được sử dụng, giúp tránh rò rỉ bộ nhớ.
        public void Dispose()
        {
            if (_myDbContext != null)
            {
                _myDbContext.Dispose();
            }
        }
    }
}
