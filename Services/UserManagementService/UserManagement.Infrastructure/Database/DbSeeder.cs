﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Helpers;
using UserManagement.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UserManagement.Infrastructure.Database
{
    public class DbSeeder
    {
        //dotnet ef migrations add InitialMigration -p../UserManagement.Infrastructure -o Database/Migrations
        public async static Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            ILogger<DbSeeder> logger = loggerFactory.CreateLogger<DbSeeder>();
            try
            {
                if (!context.Roles.Any()) {
                    await context.Roles.AddRangeAsync(DbSeeder.GetRoles());
                    await context.SaveChangesAsync();
                    logger.LogInformation("Successfully Migrated Roles");
                }

                if (!context.Genders.Any())
                {
                    await context.Genders.AddRangeAsync(DbSeeder.GetGenders());
                    await context.SaveChangesAsync();
                    logger.LogInformation("Successfully Migrated Genders");
                }
                if (!context.Users.Any())
                {
                   await context.Users.AddRangeAsync(DbSeeder.GetUsers());
                    await context.SaveChangesAsync();
                    logger.LogInformation("Successfully Migrated Roles");
                    if (!context.UserRoles.Any())
                    {

                        var admin = await context.Users.Where(u => u.Email == "admin@gmail.com").FirstOrDefaultAsync();
                        var moder = await context.Users.Where(u => u.Email == "moder@gmail.com").FirstOrDefaultAsync();
                        var admin_role = await context.Roles.Where(u => u.Code == DbConstants.GlobalAdminCode).FirstOrDefaultAsync();
                        var moder_role = await context.Roles.Where(u => u.Code == DbConstants.GlobalModerCode).FirstOrDefaultAsync();

                      if (admin != null && admin_role != null)
                        {
                            await context.UserRoles.AddAsync(
                                new UserRoleModel()
                                {
                                    UserId = admin.Id,
                                    RoleId = admin_role.Id,
                                    StartAt = new DateOnly(2023, 7, 23),
                                    EndAt = new DateOnly(2024, 7, 23),
                                    Status = 1
                                }
                                );
                            await context.SaveChangesAsync();
                            logger.LogInformation("Successfully Migrated User Role - Admin Role");
                        }
                       if(moder != null && moder_role != null)
                        {
                            await context.UserRoles.AddAsync(
                                new UserRoleModel()
                                {
                                    UserId = moder.Id,
                                    RoleId = moder_role.Id,
                                    Status = 1,
                                    StartAt = new DateOnly(2023, 7, 23),
                                    EndAt = new DateOnly(2024, 7, 23),
 
                                }
                            );
                            await context.SaveChangesAsync();
                            logger.LogInformation("Successfully Migrated User Role - Global Moder");
                        }
                    }
                }
                if (!context.Clients.Any())
                {
                    await context.Clients.AddRangeAsync(DbSeeder.GetClients());
                    await context.SaveChangesAsync();
                    logger.LogInformation("Successfully Migrated Clients");
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

            }
        }


        public static IEnumerable<RoleModel> GetRoles()
        {
            return new List<RoleModel> {
             new RoleModel()
                {
                    TitleRu = "Супер Администратор",
                    TitleEn = "Super Administrator",
                    TitleKk = "Супер Администратор",
                    Code = DbConstants.GlobalAdminCode,
                    Status = 1
                },
              new RoleModel()
                {
                    TitleRu = "Супер Модератор",
                    TitleEn = "Super Moderator",
                    TitleKk = "Супер Модератор",
                    Code = DbConstants.GlobalModerCode,
                    Status = 1
                },
             new RoleModel()
                {
                    TitleRu = "Администратор",
                    TitleEn = "Administrator",
                    TitleKk = "Администратор",
                    Code = DbConstants.LocalAdminCode,
                    Status = 1
                },
              new RoleModel()
                {
                    TitleRu = "Модератор",
                    TitleEn = "Moderator",
                    TitleKk = "Модератор",
                    Code = DbConstants.ModerCode,
                    Status = 1
                },
              new RoleModel()
                {
                    TitleRu = "Учитель",
                    TitleEn = "Teacher",
                    TitleKk = "Мұғалім",
                    Code = DbConstants.TeacherCode,
                    Status = 1
                },
              new RoleModel()
                {
                    TitleRu = "Обучающийся",
                    TitleEn = "Student",
                    TitleKk = "Оқушы",
                    Code = DbConstants.StudentCode,
                    Status = 1
                },
            };        
        }

        public static IEnumerable<UserModel> GetUsers()
        {


            return new List<UserModel> {
                new UserModel()
                {
                    Name = "Админ",
                    Surname = "Администраторов",
                    Phone = "+77777777777",
                    Email = "admin@gmail.com",
                    Password = SecurityHelper.EncryptPassword("admin123"),
                    BirthDate = new DateOnly(1990,1,1),
                    Status = 1,
                },
                new UserModel()
                {
                    Name = "Модератор",
                    Surname = "Модераторов",
                    Phone = "+77777777778",
                    Email = "moder@gmail.com",
                    Password = SecurityHelper.EncryptPassword("admin123"),
                    BirthDate = new DateOnly(1990,1,1),
                    Status = 1,
                }
            };
        }

        public static IEnumerable<GenderModel> GetGenders()
        {


            return new List<GenderModel> {
                new GenderModel()
                {
                    TitleRu = "Мужской",
                    TitleKk = "Ер",
                    TitleEn = "Male",
                    Code = DbConstants.MaleCode,
                    Status = 1
                },
                new GenderModel()
                {
                    TitleRu = "Женский",
                    TitleKk = "Әйел",
                    TitleEn = "Female",
                    Code = DbConstants.FemaleCode,
                    Status = 1
                },
                new GenderModel()
                {
                    TitleRu = "Не указано",
                    TitleKk = "Көрсетілмеген",
                    TitleEn = "Not indicated",
                    Code = DbConstants.NoGenderCode,
                    Status = 1
                },

            };
        }
        public static IEnumerable<ClientModel> GetClients()
        {
            return new List<ClientModel>
            {
                new ClientModel()
                {
                     RoleCode = DbConstants.GlobalAdminCode,
                     ClientId = "global_admin_client",
                     ClientSecret = SecurityHelper.EncryptPassword("global_admin_123"),
                },
                new ClientModel()
                {
                     RoleCode = DbConstants.GlobalModerCode,
                     ClientId = "global_moder_client",
                     ClientSecret = SecurityHelper.EncryptPassword("global_moder_123"),
                },
                new ClientModel()
                {
                     RoleCode = DbConstants.LocalAdminCode,
                     ClientId = "local_admin_client",
                     ClientSecret = SecurityHelper.EncryptPassword("local_admin_123"),
                },
                new ClientModel()
                {
                     RoleCode = DbConstants.ModerCode,
                     ClientId = "local_moder_client",
                     ClientSecret = SecurityHelper.EncryptPassword("local_moder_123"),
                },
                new ClientModel()
                {
                     RoleCode = DbConstants.TeacherCode,
                     ClientId = "teacher_client",
                     ClientSecret = SecurityHelper.EncryptPassword("teacher_123"),
                },
                new ClientModel()
                {
                     RoleCode = DbConstants.StudentCode,
                     ClientId = "student_client",
                     ClientSecret = SecurityHelper.EncryptPassword("student_123"),
                },
            };
        }

    }


}
