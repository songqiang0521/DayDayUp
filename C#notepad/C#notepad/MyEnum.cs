using System;

namespace CS_Note1
{
	public enum WndState
	{
		None, Ining, Outing, Docking, 
	} 
	public enum WndMouseState 
	{
		None, MouseOnX, MouseOnNail, LButtonDownX, LButtonDownNail,
		MouseOnLeftBtn, MouseOnRightBtn, LButtonDownLeftBtn,
		LButtonDownRightBtn, 
	} 
	public enum TreeImgIndex
	{
		OpendFolder = 1,
		ClosedFolder = 0 ,
		txt = 2 ,
	}
	public enum TreeNodeStyle 
	{
		File, Folder
	}

	[Flags]
	public enum TabsState
	{
		None      = 0x0000,
		OutLeft   = 0x0001,
		OutRight  = 0x0002,
		OutAll    = OutRight | OutLeft,
	}
} 
