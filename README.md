# MVCMusicStore
> Trang web Cửa hàng âm nhạc sử dụng ASP.NET MVC5

## Chạy dự án

- Lấy mã nguồn và mở bằng Visual Studio 2019
- Đợi cho nó load hết
- Nhấn F5

## Lưu ý

- Không phải máy nào cũng có thể chạy và đăng nhập thành công website đâu 😂

## Bước chuẩn bị

Bạn nên tạo tài khoản đăng nhập cho IIS qua các bước sau:
- Vào CMD (không phải quyền quản trị), copy và Enter: "C:\Program Files\IIS Express\iisexpress.exe" /path:c:\windows\Microsoft.NET\Framework\v4.0.30319\ASP.NETWebAdminFiles /vpath:"/asp.netwebadminfiles" /port:8089 /clr:4.0 /ntlm
- Vào trình duyệt web, vào URL sau: http://localhost:8089/asp.netwebadminfiles/default.aspx?applicationPhysicalPath=your_path_to_project_not_test_project&applicationUrl=/
- Nếu có hiện pop-up đăng nhập, điền thông tin đăng nhập Windows (do nó yêu cầu xác thực bằng Windows)
- Thêm Role là Administrator (tự tìm)
- Thêm User là admin hoặc administrator có role là Administrator, mật khẩu tuân thủ Chữ hoa + chữ thường + số + tối thiểu 7 ký tự.

## Yêu cầu

- Visual Studio 2019
- Microsoft SQL Compact Data Provider 4.0

## Tham khảo

[MVC Music Store Tutorial](https://web.csulb.edu/~pnguyen/cecs475/labs/mvc-music-store-tutorial-v30.pdf)

[Full project (MVC3)](https://archive.codeplex.com/?p=mvcmusicstore)

[MVCMusicStore.Main](https://bitbucket.org/tarwn/mvcmusicstore.main/downloads/)

## Tác giả

La Quốc Thắng - quocthang_0507@yahoo.com.vn