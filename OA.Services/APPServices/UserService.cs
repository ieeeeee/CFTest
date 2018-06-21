using AutoMapper;
using Mehdime.Entity;
using OA.Basis;
using OA.Basis.Extentions;
using OA.Basis.Utilities;
using OA.Data;
using OA.Data.Entity;
using OA.Interfaces;
using OA.Models;
using OA.Models.Enum;
using OA.Models.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace OA.Services.AppServices
{
    //用户契约实现类
    public class UserService:IUserService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory; //using Mehdime.Entity;
        private readonly IMapper _mapper; //using AutoMapper; AutoMapper是一个对象映射器，可以将一个一种类型的对象转换为另一种类型的对象 实现类字段的赋值及转换，在App_Start 中配置

        public UserService(IDbContextScopeFactory dbContextScopeFactory,IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }
        //增加
        public async Task<string> AddASync(UserAddDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = _mapper.Map<UserAddDto,B_UserEntity>(dto);
                entity.Create();
                entity.CreateDateTime = DateTime.Now;
                entity.Password = EnDecryption.MD5Encrypt(dto.PlainCode);
                db.B_Users.Add(entity);
                return await scope.SaveChangesAsync() > 0 ? entity.UserID.ToString() : string.Empty;
                
            }
        }

        //删
        public async Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities = db.B_Users.Where(x => x.IsDeleted != 1);
                foreach(var entity in entities)
                {
                    entity.IsDeleted = 1;
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }

        //查
        public async Task<UserDto> FindAsync(int ID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_Users.FindAsync(ID);
                var dto = _mapper.Map<B_UserEntity, UserDto>(entity);
                if (dto == null)
                    return new UserDto();
                return dto;
            }
        }

        //查列表
        public async Task<PagedResult<UserDto>> SearchAsync(UserFilter filter)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_Users.Where(x => x.IsDeleted != 1)
                    .WhereIf(filter.EntID>0,x=>x.EntID==filter.EntID)
                    .WhereIf(filter.keywords.IsNotBlank(), x => x.UserNo.Contains(filter.keywords) || x.UserName.Contains(filter.keywords) || x.NickName.Contains(filter.keywords));
                return await query.OrderByCustom(filter.sidx, filter.sord)
                    .Select(item => new UserDto
                    {
                        UserID = item.UserID,
                        UserNo=item.UserNo,
                        UserName=item.UserName,
                        NickName=item.NickName,
                        EntID=item.EntID,
                        EntName=item.B_Enterprise.EntName,
                        DeptID=item.DeptID,
                        DeptName=item.B_Department.DeptName,
                        Gender=item.Gender,
                        Tel=item.Tel,
                        Address=item.Address,
                        Age=item.Age,
                        Email=item.Email,
                        Password=item.Password,
                        PlainCode=item.PlainCode,
                        Position=item.Position,
                        Remark=item.Remark,
                        IsDeleted=item.IsDeleted 
                    }).PagingAsync(filter.page, filter.rows);
            }
        }

        //改
        public async Task<bool> UpdateAsync(UserAddDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_Users.LoadAsync(dto.UserID);
                entity.UserNo = dto.UserNo;
                entity.NickName = dto.NickName;
                entity.Tel = dto.Tel;
                entity.Address = dto.Address;
                entity.Age = dto.Age;
                entity.Gender = dto.Gender;
                entity.Position = dto.Position;
                entity.Remark = dto.Remark;
                entity.IsDeleted = dto.IsDeleted;
                entity.EntID = dto.EntID;
                entity.DeptID = dto.DeptID;
                await scope.SaveChangesAsync();
                return true;

            }
        }


        public async Task<UserLoginDto> Login(LoginDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var result = new UserLoginDto();
                var db = scope.DbContexts.Get<OAContext>(); //using OA.Data;
                var entity=await db.B_Users.FirstOrDefaultAsync(x => x.UserName == dto.LoginName.Trim()); //using System.Data.Entity;
                var loginLog = new B_LogEntity
                {
                    UserName = dto.LoginName,
                    IP = dto.LoginIP,
                    LogType = 1,
                    MenuID = "0",
                    Remark = BaseIdGenerator.Instance.GetNo()
                };
                if(entity==null)
                {
                    result.Message = "账号不存在";
                    result.Result = LoginResult.AccountNotExists;
                    loginLog.UserID = "0";
                }
                else
                {
                    if(entity.Password== EnDecryption.MD5Encrypt(dto.Password))
                    {
                        result.LoginSuccess = true;
                        result.Message = "登录成功";
                        result.Result = LoginResult.Success;
                        result.User = _mapper.Map<B_UserEntity, UserDto>(entity);//实现Entity 到DTO 的映射（类型转换）
                    }
                    else if(entity.IsDeleted==3)
                    {
                        result.Message = "账号已被禁用";
                        result.Result = LoginResult.IsDisabled;
                    }
                    else
                    {
                        result.Message = "登录密码错误";
                        result.Result = LoginResult.WrongPassword;
                    }
                    loginLog.UserID = entity.UserID.ToString();
                }
                loginLog.Mac = result.Message;
                db.B_Logs.Add(loginLog);
                await scope.SaveChangesAsync();
                return result;
            }
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public async Task<bool> AddRightsAsync(int userID, int roleID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var user = await db.B_Users.LoadAsync(userID);
                if (user.B_Roles.Any(x => x.RoleID == roleID))
                    return true;
                var role = await db.B_Roles.LoadAsync(roleID);
                user.B_Roles.Add(role);
                await scope.SaveChangesAsync();
                return true;
            }
        }
        /// <summary>
        /// 用户角色取消授权
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public async Task<bool> CancelRightsAsync(int userID, int roleID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var user = await db.B_Users.LoadAsync(userID);
                var role = await db.B_Roles.LoadAsync(roleID);
                user.B_Roles.Remove(role);
                await scope.SaveChangesAsync();
                return true;
            }
        }
    }
}