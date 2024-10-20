//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.EntityFrameworkCore;
//using Business_Logic.DTOs;
//using Data_Access.Entites;

//namespace Business_Logic.Services.Impl
//{
//    public class UserService : IUserService
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        //private readonly IImageService _imageService;
//        private readonly IMapper _mapper;

//        public UserService(UserManager<ApplicationUser> userManager, 
//                           SignInManager<ApplicationUser> signInManager, 
//                           RoleManager<IdentityRole> roleManager, 
//                           //IImageService imageService, 
//                           IMapper mapper)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _roleManager = roleManager;
//            //_imageService = imageService;
//            _mapper = mapper;
//        }


//        public async Task<ResponseDatatable<UserModel>>GetUserByPagination(RequestDatatable request)
//        {
//            var users = await _userManager.Users.Where(x => x.IsActive && string.IsNullOrEmpty(request.Keyword) 
//                                                                           || (x.UserName.Contains(request.Keyword)
//                                                                           || x.FullName.Contains(request.Keyword)
//                                                                           || x.Email.Contains(request.Keyword)
//                                                                           || x.PhoneNumber.Contains(request.Keyword)
//                                                                           ))
//                                                            .Select(x => new UserModel
//                                                            {
//                                                                                Email = x.Email,
//                                                                                Fullname = x.FullName,
//                                                                                Phone = x.PhoneNumber,
//                                                                                Username = x.UserName,
//                                                                                IsActive = x.IsActive ? "Yes" : "No",
//                                                                                Id = x.Id
//                                                                                //Action =""
//                                                            }).ToListAsync();
//            //đếm số lượng items
//            int totalRecords = users.Count;

//            var result = users.Skip(request.SkipItems).Take(request.PageSize).ToList();

//            return new ResponseDatatable<UserModel>{
//                Draw = request.Draw,
//                RecordsTotal = totalRecords,
//                RecordsFiltered = totalRecords,
//                Data = result
//            };
//        }

//        public async Task<AccountDTO> GetUserById(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            var role = (await _userManager.GetRolesAsync(user)).First();
//            var userDto = _mapper.Map<AccountDTO>(user);
//            userDto.RoleName = role;
//            return userDto;
//        }

//        public async Task<ResponseModel> Save(AccountDTO accountDTO)
//        {
//            string errors = string.Empty;
//            IdentityResult identityResult = null; 

//            if (string.IsNullOrEmpty(accountDTO.Id)) {
//                var applicationUser = new ApplicationUser
//                {
//                    FullName = accountDTO.Fullname,
//                    UserName = accountDTO.Username,
//                    IsActive = true,
//                    Email = accountDTO.Email,
//                    PhoneNumber = accountDTO.PhoneNumber,
//                    Address = accountDTO.Address
//                };

//                 identityResult = await _userManager.CreateAsync(applicationUser, accountDTO.Password);

//                if (identityResult.Succeeded)
//                {
//                    await _userManager.AddToRoleAsync(applicationUser, accountDTO.RoleName);

//                    //await _imageService.SaveImage(new List<IFormFile> {accountDTO.Avatar}, "images/user", $"{applicationUser.Id}.png");

//                    return new ResponseModel
//                    {
//                        Action = Domain.Enums.ActionType.Insert,
//                        Message = "Insert Successful",
//                        Status = true,
//                    };
//                }
//                //errors = string.Join("<br />", identityResult.Errors.Select(x => x.Description));
//            }
//            else
//            {
//                var user = await _userManager.FindByIdAsync(accountDTO.Id);

//                user.FullName = accountDTO.Fullname;
//                user.IsActive = true;
//                user.Email = accountDTO.Email;
//                user.PhoneNumber = accountDTO.PhoneNumber;
//                user.Address = accountDTO.Address;

//                 identityResult = await _userManager.UpdateAsync(user);

//                if (identityResult.Succeeded) {
//                    //await _imageService.SaveImage(new List<IFormFile> { accountDTO.Avatar }, "images/user", $"{user.Id}.png");

//                    var hasRole = await _userManager.IsInRoleAsync(user, accountDTO.RoleName);

//                    if (!hasRole)
//                    {
//                        var oldRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

//                        var removeResult = await _userManager.RemoveFromRoleAsync(user, oldRole);
//                        if (removeResult.Succeeded)
//                        {
//                            await _userManager.AddToRoleAsync(user, accountDTO.RoleName);
//                        }
//                    }
//                    return new ResponseModel
//                    {
//                        Status = true,
//                        Message = "Update successful.",
//                        Action = Domain.Enums.ActionType.Update
//                    };
//                }
//            }
//            errors = string.Join("<br />", identityResult.Errors.Select(x => x.Description));
//            return new ResponseModel
//            {
//                Action = Domain.Enums.ActionType.Insert,
//                Message = $"{(string.IsNullOrEmpty(accountDTO.Id) ? "Insert" : "Update")} false. {errors}",
//                Status = false,
//            };
//        }
//        public async Task<bool> DeleteAsync(string id)
//        {
//           var user = await _userManager.FindByIdAsync(id);

//            if(user is not null)
//            {
//                user.IsActive = false;
//                //await _userManager.UpdateAsync(user);
//                await _userManager.DeleteAsync(user);

//                return true;
//            }
//            return false;
//        }        
//    }
//}
