On Error Resume Next 
Dim Fso,IniFl,Str,IniFn
IniFn="macs2000.ini"      ' 在等号后面双引号里写上ini文件的文件名，例如：IniFn="configip.ini"
Set Fso = CreateObject("Scripting.FileSystemObject")
Set IniFl = Fso.OpenTextFile ("c:\Windows\"&IniFn,1)
Str = Replace ( IniFl.ReadAll,"InitAddress=03000000","InitAddress=04000000")
Set IniFl = Fso.OpenTextFile ("c:\Windows\"&IniFn,2)
IniFl.Write Str 
IniFl.Close