using Data_Access.Repositories;

namespace Data_Access.Repositories
{
    // Mục đích của Interaface này là Quản lý giao dịch, chịu trách nhiệm cung cấp và quản lý các repository
    //Được tạo dựa trên mẫu thiết kế Unit of Work, bằng cách gom DbContext và Repository
    //Giúp đảm bảo các thao tác lên CSDL được thực hiện trong 1 lần "giao tiếp" với CSDL. Tất cả các thao tác thêm, sửa, xóa,
    //lấy dữ liệu trên các repository sẽ được gộp lại và khi gọi SaveChangesAsync thì tất cả sẽ được lưu vào CSDL trong 1 lần
    public interface IRepositoryManager
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        // Cần cân nhắc dùng hàm này!
        Task SaveChangeAsync();
    }
}