Dim WSHShell

Set WSHShell = WScript.CreateObject("WScript.Shell")

sTitle1 = "SSH=0"

sTitle2 = "SSH=1"

if WshSHell.RegRead("HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\Hidden") = 1 then

WshSHell.RegWrite "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\Hidden", "2", "REG_DWORD"

WshSHell.SendKeys "{F5}+{F10}e"

else

WshSHell.RegWrite "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\Hidden", "1", "REG_DWORD"

WshSHell.SendKeys "{F5}+{F10}e"

end if

Set WSHShell = Nothing

WScript.Quit(0)