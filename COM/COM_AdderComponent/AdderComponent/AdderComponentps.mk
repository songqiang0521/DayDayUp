
AdderComponentps.dll: dlldata.obj AdderComponent_p.obj AdderComponent_i.obj
	link /dll /out:AdderComponentps.dll /def:AdderComponentps.def /entry:DllMain dlldata.obj AdderComponent_p.obj AdderComponent_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del AdderComponentps.dll
	@del AdderComponentps.lib
	@del AdderComponentps.exp
	@del dlldata.obj
	@del AdderComponent_p.obj
	@del AdderComponent_i.obj
