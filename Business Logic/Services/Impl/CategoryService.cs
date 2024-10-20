using Business_Logic.Services;
using Business_Logic.DTOs;
using Data_Access.Repositories;
using Data_Access.Entities;

namespace Business_Logic.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        // Sử dụng repositoryManager để giao tiếp với tầng Data Access
        private readonly IRepositoryManager repositoryManager;

        public CategoryService(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        public async Task<List<CategoryDTO>> FindAll()
        {
            // Lấy danh sách CategoryEntity từ Repository
            var categoriesEntity = await repositoryManager.CategoryRepository.FindAll();

            // Chuyển đổi danh sách Entity thành DTO
            var categoriesDTO = categoriesEntity.Select(c => new CategoryDTO {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                Description = c.Description,
                CreateBy = c.CreateBy,
                CreateDate = c.CreateDate,
                ModifiedBy = c.ModifiedBy,
                ModifiedDate = c.ModifiedDate,
            }).ToList();

            // Trả về List<DTO>
            return categoriesDTO;
        }

        public async Task<CategoryDTO> FindOne(int id)
        {
            // Sử dụng Repository để tìm CategoryEntity có Id tương ứng
            var categoryEntity = await repositoryManager.CategoryRepository.FindOne(id);

            // Tạo DTO ngoài vòng if để khi categoryEntity = null thì hàm vẫn trả về categoryDTO
            CategoryDTO categoryDTO = new CategoryDTO();

            // Chuyển đổi Entity thành DTO
            if (categoryEntity != null) {

                categoryDTO.Id = categoryEntity.Id;
                categoryDTO.Name = categoryEntity.Name;
                categoryDTO.Code = categoryEntity.Code;
                categoryDTO.Description = categoryEntity.Description;
                categoryDTO.CreateDate = categoryEntity.CreateDate;
                categoryDTO.CreateBy = categoryEntity.CreateBy;
                categoryDTO.ModifiedDate = categoryEntity.ModifiedDate;
                categoryDTO.ModifiedBy = categoryEntity.ModifiedBy;
            }

            // Trả về DTO
            return categoryDTO;
        }

        public async Task<CategoryDTO> Save(CategoryDTO categoryDTO)
        {
            // Tạo Instance CategoryEntity mới dựa trên dữ liệu từ CategoryDTO
            CategoryEntity categoryEntity = new CategoryEntity();
            categoryEntity.Name = categoryDTO.Name;
            categoryEntity.Code = categoryDTO.Code;
            categoryEntity.Description = categoryDTO.Description;
            //Thiết lập CreateDate, CreateBy, ModifiedBy, ModifiedDate. Trong đó, CreateBy và ModifiedBy tạm thời cố định
            categoryEntity.CreateDate= DateTime.Now;
            categoryEntity.CreateBy = "Tom";
            categoryEntity.ModifiedBy = "Tom";
            categoryEntity.ModifiedDate = DateTime.Now;

            // Lưu đối tượng CategoryEntity vào CSDL thông qua Repository
            var id = await repositoryManager.CategoryRepository.Save(categoryEntity);

            //Trả về đối tượng DTO mới được cập nhật với Id của Category được lưu
            if (id != null) {
                categoryDTO.Id = id;
            }
            return categoryDTO;

        }

        public async Task<CategoryDTO> Update(CategoryDTO categoryDTO)
        {
            // Tạo CategoryDTO trước để đảm bảo hàm trả về có giá trị không phải là null
            CategoryDTO newCategoryDTO = new CategoryDTO();


            if (categoryDTO.Id != null)
            {
                // Tìm Category dựa trên Id
                var categoryEntity = await repositoryManager.CategoryRepository.FindOne(categoryDTO.Id.Value);

                // Cập nhật các thuộc tính của CategoryEntity với dữ liệu mới từ CategoryDTO
                categoryEntity.Name= categoryDTO.Name;
                categoryEntity.Code = categoryDTO.Code;
                categoryEntity.Description = categoryDTO.Description;

                //Thiết lập ModifiedBy, ModifiedDate. Trong đó, ModifiedBy tạm thời cố định
                categoryEntity.ModifiedDate = DateTime.Now;
                categoryEntity.ModifiedBy = "Tom";

                // Lưu thay đổi vào CSDL qua Repository
                var id = await repositoryManager.CategoryRepository.Update(categoryEntity);
                if (id != null)
                {
                    // Cập nhật lại thông qua categoryDTO
                    newCategoryDTO = categoryDTO;
                }
            }
            // Trả về DTO
            return newCategoryDTO;

        }

        //public async Task<bool> DeleteAsync(int id)
        //{
        //    try
        //    {
        //        var category = await _unitOfWork.CategoryRepository.GetById(id);
        //        if (category == null) return false;

        //        await _unitOfWork.CategoryRepository.DeleteAsync(category); // Ensure DeleteAsync is defined
        //        await _unitOfWork.SaveChangeAsync();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
