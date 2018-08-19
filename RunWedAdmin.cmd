@echo off

cd "C:\Program Files\IIS Express\"
call iisexpress.exe /path:"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ASP.NETWebAdminFiles" /vpath:"/ASP.NETWebAdminFiles" /port:8082 /clr:4.0 /ntlm