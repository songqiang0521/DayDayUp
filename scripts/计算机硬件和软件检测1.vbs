'==========================================================================  
'  
' Date:2009/3/19  
' Name: ��ѯ�����Ӳ���б��嵥  
' Comment: blogs.itecn.net/smileruner  
' Author:Smileruner  
' www.overmcse.com  
' ��֧��Win2000��WinNT  
'  
' 3/19,������������ˡ�  
'==========================================================================  
'on error resume Next  
Dim strProductKey,strTmp,ProductId,g_objRegistry
const HKEY_LOCAL_MACHINE = &H80000002   
const UNINSTALL_ROOT = "Software\Microsoft\Windows\CurrentVersion\Uninstall"  
const REG_SZ = 1  
const WindowsNTInfoPath = "SOFTWARE\Microsoft\Windows NT\CurrentVersion"   
'Set wshshell=wscript.createobject("wscript.shell")  
' wshshell.run ("%comspec% /c regsvr32 /s scrrun.dll"),0,true  
' wshshell.run ("%comspec% /c sc config  winmgmt start= auto"),0,true  
' wshshell.run ("%comspec% /c net start winmgmt"),0  

'Wscript.Echo "����ͳ�Ʊ�����Ӳ����Ϣ����ȷ�����������Ӳ����[ȷ��]�ȴ���ɣ�" 
'��ȡ�������
strComputer = "."  
strComputerName="."
Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\cimv2")  
Set colComputers = objWMIService.ExecQuery("Select * from Win32_ComputerSystem")  
For Each objComputer in colComputers  
'Wscript.Echo objComputer.Name 
strComputerName=objComputer.Name
Next  

strNUMBER =Inputbox("������Ա������,���ȷ�Ϻ�ȴ�������ʾ��",,"")  

'strComputer =Inputbox("������������",,"")  
  
If strNUMBER = "" then  
        Wscript.Echo "��ֵ������û�ȡ������������ȷ�Ĺ��š�"  
        Wscript.Quit  
End If  
  
Set objswbemlocator = createobject("wbemscripting.swbemlocator")  
Set objswbemservices = objswbemlocator.connectserver(strComputer, "root\cimv2")  
If Err.number <> 0 then  
        Wscript.Echo "������޷����ӡ�����ļ����������Ŀ�����������˷���ǽ��RPC���񲻿��á�"  
        Err.number.clear  
        Wscript.Quit  
End If  
  
'swbemservices.security_.impersonationleobjvel = 3  
  
Set fso=createobject("scripting.filesystemobject")  
FileDate = Replace(date(), "/", "-")  
resoultfilepath= strComputerName & FileDate &".csv"   '���λ�ú�Ŀ¼ 
Set resultFile= fso.createtextfile(resoultfilepath,,true)  
Set g_objRegistry = GetObject("winmgmts:\\" & strComputer & "\root\default:StdRegProv")  
HtmlWriteHead()  
'Html�ĵ���ʼ  
  
TableHead strComputerName, "��Ӳ���嵥"  
'Html���ʼ  
str123456() 

OsWrite()  
             
						'д�����ϵͳ��Ϣ
ProductKeyWrite()       
						'д�����ϵͳ���к���Ϣ
strProductIdWrite()      
						'д�����ϵͳCOA Number��Ϣ
BoardWrite()            
						'д��������Ϣ                 
CpuWrite()         
                        'д��CPU��Ϣ  
MemoryWrite()          
                        'д���ڴ���Ϣ  
HarddiskWrite()  
                        'д��Ӳ����Ϣ  
CdromWrite()  
                        'д��CDROM��Ϣ  
VideoWrite()  
                        'д����ʾ����Ϣ  
NetcardWrite()  
                        'д��������Ϣ  
'TableEnd()  
                        'Html����β  
TableHead strComputerName,"����嵥"  
                        'Html���ͷ  
Softlist()  
                        'д�������Ϣ  
'TableEnd()  
                        'Html����β  
'HtmlWriteEnd()  
                        'Html�ĵ�����  
  
ResultFile.close  
'---------------------------------


'---------------------------------
 
Wscript.Echo "Ӳ�������������Ϣ�ռ���ɣ��뽫�ռ��õ��ļ�������IT����̨@hollysys.net"  
  
'=========�����Ǻ����б�==========  
 
Function ProductKeyWrite()
g_objRegistry.GetBinaryValue HKEY_LOCAL_MACHINE, WindowsNTInfoPath, "DigitalProductId",strTmp
If Not IsNull(strTmp) Then
strProductKey=GetKey(strTmp)
'WScript.echo strProductKey
WriteTable "���к�",strProductKey
End If
End Function


Function strProductIdWrite()
g_objRegistry.GetStringValue HKEY_LOCAL_MACHINE, WindowsNTInfoPath, "ProductId", strTmp
If Not IsNull(strTmp) Then
strProductId = strTmp
WriteTable "COA Number",strProductId
End If
End Function

Function str123456()
strProductId = strNUMBER
WriteTable "����",strProductId
End Function



Function OsWrite()   
                '����,д�����ϵͳ��Ϣ  
        Set colOs =objswbemservices.execquery("select * from win32_operatingsystem",,48)  
        For Each Ositem In colOs  
                oscaption = Ositem.caption  
                OsVersion = oscaption & Ositem.version  
                WriteTable "����ϵͳ",OsVersion  
        Next  
End Function   
  
  
Function BoardWrite()  
                '������д��������Ϣ  
        Set colBoard = objswbemservices.execquery("select * from win32_baseboard")  
        For Each Bditem In colBoard  
                boardname = Bditem.product  
                WriteTable "����",boardname  
        Next  
End Function   
  
  
Function CpuWrite()  
                '������д��CPU��Ϣ  
        Set colCpu =objswbemservices.execquery("select * from win32_processor")  
        For Each item in colCpu  
                cpuname =  (trim(item.name))  
                WriteTable "���봦����",cpuname  
        Next  
End Function   
  
Function MemoryWrite()  
                '������д���ڴ���Ϣ  
mtotal        = 0  
num         = 0  
mill         = 0  
        Set colMemory = objswbemservices.execquery("select * from win32_physicalmemory",,48)  
        For Each objitem In colMemory  
                mill = objitem.capacity/1048576  
                WriteTable "�����ڴ�����",mill & "M"  
                mtotal = mtotal+mill  
                num = num + 1  
        Next  
        WriteTable "�ܼ��ڴ�",num & "��" & "һ��" & mtotal & "M"  
End Function   
  
Function HarddiskWrite()  
                '������д��Ӳ����Ϣ  
        Set colDisk = objswbemservices.execquery("select * from win32_diskdrive", , 48)  
        For Each objitem In colDisk  
                diskname= objitem.caption  
                disksize= fix(objitem.size/1073741824)  
                WriteTable "Ӳ��",diskname & " ������" & disksize & "G"  
        Next  
End Function   
  
  
  
Function CdromWrite()  
                '������д��CDROM��Ϣ  
        Set colCdrom = objswbemservices.execquery("select * from win32_cdromdrive where scsitargetid=0")  
        For Each objitem In colCdrom  
                cdname = objitem.name  
                WriteTable "����",cdname  
        Next  
End Function  
  
Function videoWrite()  
                '������д����ʾ����Ϣ  
        Set colVideo = objswbemservices.execquery("select * from win32_videocontroller", , 48)  
        For Each objitem in colVideo  
                videoname = (trim(objitem.caption) & (objitem.videomodedescription))   
                WriteTable "��ʾ��",videoname  
        Next  
End Function   
  
  
Function netcardWrite()  
                '��������ѯ������Ϣ  
        Set colNetcards = objswbemservices.execquery("select * from win32_networkadapter")  
                For Each objNetcard in colNetcards                          
                If Not IsNull(objNetcard.NetConnectionID) Then  
                        NetCardName         =  objNetcard.productname  
                               WriteTable "��������",NetCardName  
                   
                        If objNetcard.NetConnectionStatus = 2 Then                           
                        NetCardMac         =  objNetcard.macaddress  
                        WriteTable "����Mac",NetCardMac  
  
                        strQueryIp ="select * from win32_networkadapterconfiguration" &_  
                                         " where IPEnabled = true" &_  
                                         " and macaddress = '" & objNetcard.macaddress & "'"  
                        Set colNetcardCfgs = objswbemservices.execquery(strQueryIp)   
                        For Each objNetcardCfg in colNetcardCfgs                  
                                For Each CfgAdrress in objNetcardCfg.IPAddress  
                                        IpAdrress = CfgAdrress  
                                        WriteTable "IP��ַ",IpAdrress  
                                Next  
                        Next          
  
                        Else  
  
                        NetCardMac = "���������û�δ���ӡ�"  
                        WriteTable "����Mac",NetCardMac  
                        IpAdrress = "���������û�δ���ӡ�"      
                        WriteTable "IP��ַ",IpAdrress          
                          
                        End If  
                                  
                End if  
                          
                Next  
End Function   
  
Function softlist()  
                '������д�������Ϣ  
Set StdOut = WScript.StdOut   
Set oReg=GetObject("winmgmts:{impersonationLevel=impersonate}!\\" &_   
strComputer & "\root\default:StdRegProv")   
  
strKeyPath = UNINSTALL_ROOT   
  
oReg.EnumKey HKEY_LOCAL_MACHINE, strKeyPath, arrSubKeys   
  
For Each strSubKey In arrSubKeys   
        If NotHotfix(strSubKey) Then                  
                SoftNameAndVersion = getProgNameAndVersion(oReg,strKeyPath & "\" & strSubKey)  
                If SoftNameAndVersion<>"0" Then   
                WriteTable "���",SoftNameAndVersion                  
                End If   
        End If  
Next                   
End Function  
  
Function NotHotfix(sSubKey)          
        If Left(sSubkey,2) = "KB" And len(sSubkey) = 8 Then  
                NotHotfix = 0  
        Else NotHotfix = 1  
        End if  
End Function  
  
Function getProgNameAndVersion(oReg,sKeyRoot)  
Dim sKeyValuesAry, iKeyTypesAry, nCnt, sValue, sDisplayName, sDisplayVersion  
oReg.EnumValues HKEY_LOCAL_MACHINE, sKeyRoot, sKeyValuesAry, iKeyTypesAry  
        If NOT IsArray(sKeyValuesAry) Then   
                getProgNameAndVersion = 0  
                Exit Function    
        End If  
  
        For nCnt = 0 To UBound(sKeyValuesAry)  
                If InStr(1, sKeyValuesAry(nCnt), "DisplayName", vbTextCompare) Then  
                        If iKeyTypesAry(nCnt) = REG_SZ Then  
                                oReg.GetStringValue HKEY_LOCAL_MACHINE, sKeyRoot, sKeyValuesAry(nCnt), sValue  
                                If sValue<>"" Then   
                                        sDisplayName = sValue                                  
                                        sDisplayName = Replace(sDisplayName, "[", "(")  
                                        sDisplayName = Replace(sDisplayName, "]", ")")  
                                End If  
                        End If  
                ElseIf InStr(1, sKeyValuesAry(nCnt), "DisplayVersion", vbTextCompare) Then  
                        If iKeyTypesAry(nCnt) = REG_SZ Then  
                                oReg.GetStringValue HKEY_LOCAL_MACHINE, sKeyRoot, sKeyValuesAry(nCnt), sValue  
                                If sValue<>"" Then sDisplayVersion = sValue  
                        End If  
                End If  
  
                If (sDisplayName<>"") AND (sDisplayVersion<>"") Then   
                        getProgNameAndVersion = sDisplayName & " --�汾��: " & sDisplayVersion  
                        Exit Function  
                Else         getProgNameAndVersion = 0                          
                End If  
        Next  
  
        If sDisplayName<>"" Then   
                getProgNameAndVersion = sDisplayName  
                Exit Function                                          
        End If  
End Function  
  
  
Function WriteTable(caption,value)  
                '������������д��HTML��Ԫ��  
'resultFile.Writeline "<tr>"  
resultFile.Writeline "<" & caption & ">" & "=" & value ' align=""left"" width=""30%"" height=""25"" bgcolor=""#ffffff"" scope=""row""> 
'resultFile.Writeline "<td"">  " & value & "</td>"  
'resultFile.Writeline "</tr>"  
End Function   
  
Function HtmlWriteHead()  
                '������д��THML�ļ�ͷ  
'resultFile.Writeline "<html>"   
'resultFile.Writeline "<head>"   
resultFile.Writeline "<��Ӳ�������嵥>" '</title>"  
'resultFile.Writeline "</head>"   
'resultFile.Writeline "<body>"   
End Function   
  
  
'Function HtmlWriteEnd()  
                '������д��Html�ļ�β  
'resultFile.Writeline "</body>"   
'resultFile.Writeline "<END>"   
'End Function   
  
Function TableHead(pcname,str)  
                '������д��Html����β  
resultFile.Writeline "<" & pcname & str & " -- date:"&now()&">" '& VbCrLf  
'resultFile.Writeline "<table"">"   ' width=""90%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""1"" bgcolor=""#0000ff
'resultFile.Writeline "<tr>"   
resultFile.Writeline "<�ʲ�����>" & "<��ѯ���ֵ>" ' width=""30%"" height=""25"" bgcolor=""#ffffff"" scope=""col
'resultFile.Writeline "<th"">��ѯ���ֵ</th>"  'bgcolor=""#ffffff"" scope=""col"
'resultFile.Writeline "</tr>"   
'strstyle = "<th"">"  ' width=""30%"" height=""25"" bgcolor=""#ffffff"" scope=""row
End Function   
  
'Function TableEnd()  
                '������Html����β  
'resultFile.Writeline "</table>"   
'End Function 

Function GetKey(rpk)   'Decode the product key
Const rpkOffset=52
Dim dwAccumulator, szPossibleChars, szProductKey
dim i,j
i=28 : szPossibleChars="BCDFGHJKMPQRTVWXY2346789"
Do 'Rep1
    dwAccumulator=0 : j=14
    Do
        dwAccumulator=dwAccumulator*256
        dwAccumulator=rpk(j+rpkOffset)+dwAccumulator
        rpk(j+rpkOffset)=(dwAccumulator\24) and 255
        dwAccumulator=dwAccumulator Mod 24
        j=j-1
    Loop While j>=0
    i=i-1 : szProductKey=mid(szPossibleChars,dwAccumulator+1,1)&szProductKey
    if (((29-i) Mod 6)=0) and (i<>-1) then
        i=i-1 : szProductKey="-"&szProductKey
    end if
Loop While i>=0 'Goto Rep1
GetKey=szProductKey
End Function 
 
 
 
 
