On Error Resume Next 
Dim Fso,IniFl,Str,IniFn
IniFn="macs2000.ini"      ' �ڵȺź���˫������д��ini�ļ����ļ��������磺IniFn="configip.ini"
Set Fso = CreateObject("Scripting.FileSystemObject")
Set IniFl = Fso.OpenTextFile ("c:\Windows\"&IniFn,1)
Str = Replace ( IniFl.ReadAll,"InitAddress=03000000","InitAddress=04000000")
Set IniFl = Fso.OpenTextFile ("c:\Windows\"&IniFn,2)
IniFl.Write Str 
IniFl.Close