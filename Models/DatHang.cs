using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClothesShoping.Models
{
    public class DatHang
    {
        [DisplayName("Mã ĐH")]
        public int ID { get; set; }
        [DisplayName("Khách hàng")]
        [Required(ErrorMessage = "Khách hàng không được bỏ trống.")]

        public int NguoiDungID { get; set; }
        [DisplayName("Tình trạng")]
        [Required(ErrorMessage = "Tình trạng không được bỏ trống.")]
        public int TinhTrangID { get; set; }

        [DisplayName("Điện thoại giao hàng")]
        [Required(ErrorMessage = "Điện thoại giao hàng không được bỏ trống.")]
        [StringLength(20)]
        public string DienThoaiGiaoHang { get; set; }


        [StringLength(255)]
        [DisplayName("Địa chỉ giao hàng")]
        [Required(ErrorMessage = "Địa chỉ giao hàng không được bỏ trống.")]
        public string DiaChiGiaoHang { get; set; }

        [DisplayName("Ngày đặt hàng")]
        [Required(ErrorMessage = "Ngày đặt hàng không được bỏ trống.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime NgayDatHang { get; set; }

        public NguoiDung? NguoiDung { get; set; }
        public TinhTrang? TinhTrang { get; set; }
        public ICollection<DatHang_ChiTIet>? DatHang_ChiTiet { get; set; }
    }
}
