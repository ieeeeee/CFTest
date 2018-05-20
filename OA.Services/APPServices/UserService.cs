using AutoMapper;
using Mehdime.Entity;
using OA.Basis.Extentions;
using OA.Basis.Utilities;
using OA.Data;
using OA.Data.Entity;
using OA.Interfaces;
using OA.Models;
using OA.Models.Enum;
using System.Data.Entity;
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

    }
}