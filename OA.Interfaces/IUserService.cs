using OA.Models;
using System.Threading.Tasks;

//用户契约
namespace OA.Interfaces
{
    public interface IUserService
    {
        //登录
        Task<UserLoginDto> Login(LoginDto dto);
    }
}