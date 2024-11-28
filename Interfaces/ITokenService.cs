using TeddyCourseYT.Models;

namespace TeddyCourseYT.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}