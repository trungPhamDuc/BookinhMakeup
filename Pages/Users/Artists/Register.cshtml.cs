using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using www_Blush_Brush.Dto;
using www_Blush_Brush.Models;
using www_Blush_Brush.Services;
using www_Blush_Brush.Utills;

namespace www_Blush_Brush.Pages.Users.Artists
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public ArtistDto ArtistDto { get; set; }
        private readonly MakeupArtistService makeupArtistService;
        private readonly UserService userService;
        public RegisterModel(MakeupArtistService makeupArtistService, UserService userService)
        {
            this.makeupArtistService = makeupArtistService;
            this.userService = userService;
        }

        public List<SelectListItem> Provinces { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("Hà Nội", "Hà Nội"),
            new SelectListItem("Hồ Chí Minh", "Hồ Chí Minh"),
            new SelectListItem("Đà Nẵng", "Đà Nẵng"),
            new SelectListItem("Hải Phòng", "Hải Phòng"),
            new SelectListItem("Cần Thơ", "Cần Thơ"),
            new SelectListItem("Bắc Ninh", "Bắc Ninh"),
            new SelectListItem("Hưng Yên", "Hưng Yên"),
            new SelectListItem("Quảng Ninh", "Quảng Ninh"),
            new SelectListItem("Thừa Thiên Huế", "Thừa Thiên Huế"),
            new SelectListItem("Nghệ An", "Nghệ An"),
            new SelectListItem("Thanh Hóa", "Thanh Hóa"),
            new SelectListItem("Bình Dương", "Bình Dương"),
            new SelectListItem("Đồng Nai", "Đồng Nai"),
            new SelectListItem("Vĩnh Phúc", "Vĩnh Phúc"),
            new SelectListItem("Khánh Hòa", "Khánh Hòa"),
        };

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ArtistDto != null)
            {
                var user = new User
                {
                    Name = ArtistDto.Name,
                    Email = ArtistDto.Email,
                    Phone = ArtistDto.Phone,
                    Password = RandomPassword.GenerateRandomPassword(),
                    Role = "artist",
                    IsActive = false,
                };

                var u = userService.AddUser(user);
                if (u != null)
                {
                    var artist = new MakeupArtist
                    {
                        UserId = u.Id,
                        Bio = ArtistDto.Bio,
                        Experience = ArtistDto.Experience,
                        Location = ArtistDto.Location,
                    };

                    if (!makeupArtistService.AddMakeupArtist(artist))
                    {
                        return Page();
                    }
                }
            }
            ViewData["registerErrSucces"] = "Đã nộp đơn thành công!";
            return Page();
        }
    }
}
