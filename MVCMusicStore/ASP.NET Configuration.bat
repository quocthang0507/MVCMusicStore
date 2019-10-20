@echo off
cd C:\Program Files\IIS Express
start "" "http://localhost:12345/webadmin/default.aspx?applicationPhysicalPath=%~dp0&applicationUrl=/"
iisexpress.exe /path:C:\Windows\Microsoft.NET\Framework\v4.0.30319\ASP.NETWebAdminFiles /vpath:"/webadmin" /port:12345 /clr:4.0 /ntlm