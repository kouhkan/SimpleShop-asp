using Microsoft.AspNetCore.Identity;

namespace TeddyCourseYT.Models;

public class AppUser: IdentityUser
{
    public List<Basket> Baskets { get; set; } = new List<Basket>();
}