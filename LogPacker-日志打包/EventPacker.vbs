
strComputer = "."
Set objWMIService = GetObject("winmgmts:" _
    & "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2")


Set colLoggedEvents = objWMIService.ExecQuery _
    ("Select * from Win32_NTLogEvent Where Logfile = 'Application'")  '获取应用程序日志,其它类推


'Application
Dim fileSystemObj, fileSpec
Const ForReading = 1, ForWriting = 2, ForAppending = 8
Set fileSystemObj =CreateObject("Scripting.FileSystemObject")

fileSpec ="Application.txt" 'change this according to your directory
Set logFile = fileSystemObj.CreateTextFile(fileSpec, ForWriting, True) 


For Each objEvent in colLoggedEvents
	logFile.WriteLine ("Category: " & objEvent.Category) 
	logFile.WriteLine ("Event Code: " & objEvent.EventCode)
	logFile.WriteLine ("Message: " & objEvent.Message)
	logFile.WriteLine ("Record Number: " & objEvent.RecordNumber)
	logFile.WriteLine ("Source Name: " & objEvent.SourceName)
	logFile.WriteLine ("Time Written: " & objEvent.TimeWritten)
	logFile.WriteLine ("Event Type: " & objEvent.Type)
	logFile.WriteLine (" ")
Next



Set colLoggedEvents_system = objWMIService.ExecQuery _
    ("Select * from Win32_NTLogEvent Where Logfile = 'System'")  '获取系统日志,其它类推

fileSpec ="System.txt" 'change this according to your directory
Set logFile = fileSystemObj.CreateTextFile(fileSpec, ForWriting, True) 

For Each objEvent in colLoggedEvents_system
	logFile.WriteLine ("Category: " & objEvent.Category) 
	logFile.WriteLine ("Event Code: " & objEvent.EventCode)
	logFile.WriteLine ("Message: " & objEvent.Message)
	logFile.WriteLine ("Record Number: " & objEvent.RecordNumber)
	logFile.WriteLine ("Source Name: " & objEvent.SourceName)
	logFile.WriteLine ("Time Written: " & objEvent.TimeWritten)
	logFile.WriteLine ("Event Type: " & objEvent.Type)
	logFile.WriteLine (" ")
Next






