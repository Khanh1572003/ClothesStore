﻿using ClothesShoping.Models;

namespace ClothesShoping.Logic
{
    public interface IMailLogic
    {
        Task GoiEmail(MailInfo mailInfo);
        Task GoiEmailDatHangThanhCong(DatHang datHang, MailInfo mailInfo);
    }
}
