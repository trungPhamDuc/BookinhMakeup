using System.ComponentModel.DataAnnotations;

namespace www_Blush_Brush.Dto
{
    public class ArtistDto
    {
        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập số năm kinh nghiệm.")]
        [Range(0, 100, ErrorMessage = "Số năm kinh nghiệm phải từ 0 đến 100.")]
        public int Experience { get; set; }

        [Required(ErrorMessage = "Địa điểm là bắt buộc.")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng viết đôi nét về bạn.")]
        public string Bio { get; set; } = null!;
    }
}
