using Business_Logic.DTOs;

namespace Business_Logic.Services
{
    // Interface quản lý logic cho Category
    public interface ICategoryService
    {
        // Logic tìm 1 DTO Category theo Id
        Task<CategoryDTO> FindOne(int id);

        // Logic tìm tất cả DTO Category
        Task<List<CategoryDTO>> FindAll();

        // Logic Lưu DTO mới vào CSDL. 
        Task<CategoryDTO> Save(CategoryDTO categoryDTO);

        // Logic Cập nhật DTO.
        Task<CategoryDTO> Update(CategoryDTO categoryDTO);
        //Task<bool> DeleteAsync(int id);
    }
}
