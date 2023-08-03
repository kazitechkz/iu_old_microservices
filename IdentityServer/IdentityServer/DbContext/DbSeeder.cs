using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer.DbContext
{
    public class DbSeeder : IDbSeeder
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DbSeeder> _logger;

        public DbSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DbSeeder> logger, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public async Task  SeedAsync()
        {
            try
            {

                if(_roleManager.FindByNameAsync(AppConstants.Superadmin).Result == null)
                {
                   await _roleManager.CreateAsync(new IdentityRole(AppConstants.Superadmin));
                    await _roleManager.CreateAsync(new IdentityRole(AppConstants.Methodist));
                  await  _roleManager.CreateAsync(new IdentityRole(AppConstants.Admin));
                  await  _roleManager.CreateAsync(new IdentityRole(AppConstants.Moder));
                 await   _roleManager.CreateAsync(new IdentityRole(AppConstants.Teacher));
                 await   _roleManager.CreateAsync(new IdentityRole(AppConstants.Student));
                    _logger.LogInformation("Role created successfully");
                }

                if (!_userManager.Users.Any())
                {
                    //Create SuperAdmin
                    ApplicationUser superAdmin = new ApplicationUser
                    {
                        UserName = "superadmin@gmail.com",
                        Name = "Админ",
                        Surname = "Админов",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "superadmin@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777777",
                        IIN = "000000000000",
                        Status = 1,
                        GenderId = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(superAdmin, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(superAdmin, AppConstants.Superadmin).GetAwaiter().GetResult();

                    var superadmin_claim = _userManager.AddClaimsAsync(superAdmin, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Superadmin),
                        new Claim(JwtClaimTypes.Name,superAdmin.UserName),
                        new Claim(JwtClaimTypes.Email,superAdmin.Email)
                    }).Result;
                    //Create Methodist
                    ApplicationUser methodist = new ApplicationUser
                    {
                        UserName = "methodist@gmail.com",
                        Name = "Метадолог",
                        Surname = "Метадологов",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "methodist@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777778",
                        IIN = "111111111111",
                        Status = 1,
                        GenderId = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(methodist, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(methodist, AppConstants.Methodist).GetAwaiter().GetResult();

                    var methodist_claim = _userManager.AddClaimsAsync(methodist, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Methodist),
                        new Claim(JwtClaimTypes.Name,methodist.UserName),
                        new Claim(JwtClaimTypes.Email,methodist.Email)
                    }).Result;

                    //Create Administrator - Director of the school
                    ApplicationUser administrator = new ApplicationUser
                    {
                        UserName = "admin@gmail.com",
                        Name = "ЛокальныйАдмин",
                        Surname = "ЛокальныйАдминов",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777779",
                        IIN = "222222222222",
                        Status = 1,
                        GenderId = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(administrator, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(administrator, AppConstants.Admin).GetAwaiter().GetResult();

                    var administrator_claim = _userManager.AddClaimsAsync(administrator, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Admin),
                        new Claim(JwtClaimTypes.Name,administrator.UserName),
                        new Claim(JwtClaimTypes.Email,administrator.Email)
                    }).Result;
                    //Create Administrator - Director of the school
                    ApplicationUser moderator = new ApplicationUser
                    {
                        UserName = "moderator@gmail.com",
                        Name = "Модер",
                        Surname = "Модеров",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "moderator@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777780",
                        IIN = "333333333333",
                        Status = 1,
                        GenderId = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(moderator, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(moderator, AppConstants.Moder).GetAwaiter().GetResult();

                    var moderator_claim = _userManager.AddClaimsAsync(moderator, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Moder),
                        new Claim(JwtClaimTypes.Name,moderator.UserName),
                        new Claim(JwtClaimTypes.Email,moderator.Email)
                    }).Result;
                    //Create Teacher
                    ApplicationUser teacher = new ApplicationUser
                    {
                        UserName = "teacher@gmail.com",
                        Name = "Учитель",
                        Surname = "Учителев",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "teacher@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777781",
                        IIN = "444444444444",
                        Status = 1,
                        GenderId = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(teacher, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(teacher, AppConstants.Teacher).GetAwaiter().GetResult();

                    var teacher_claim = _userManager.AddClaimsAsync(teacher, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Teacher),
                        new Claim(JwtClaimTypes.Name,teacher.UserName),
                        new Claim(JwtClaimTypes.Email,teacher.Email)
                    }).Result;

                    //Add Student
                    ApplicationUser student = new ApplicationUser
                    {
                        UserName = "student@gmail.com",
                        Name = "Студент",
                        Surname = "Студентов",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Email = "student@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+77777777782",
                        IIN = "555555555555",
                        Status = 1,
                        GenderId = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    _userManager.CreateAsync(student, "Admin123*").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(student, AppConstants.Student).GetAwaiter().GetResult();

                    var student_claim = _userManager.AddClaimsAsync(student, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Role,AppConstants.Student),
                        new Claim(JwtClaimTypes.Name,student.UserName),
                        new Claim(JwtClaimTypes.Email,student.Email)
                    }).Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(),ex.ToString());
            }


        }
    }
}
