using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO ;
using System.Globalization ;
using System.Runtime.Serialization ;
using System.Runtime.Serialization.Formatters.Binary ;





namespace CS_Note1
{
	/// <summary>
	/// FormJ 的摘要说明。
	/// </summary>

	
	public class FormJ : System.Windows.Forms.Form
	{
		#region field

		private System.Windows.Forms.Panel MdiTabOwner;
		private System.Windows.Forms.Panel MdiTabs;
		private System.Windows.Forms.RichTextBox TextEditPane;
		private System.Windows.Forms.TreeView DocTree;
		private System.Windows.Forms.Panel GenericPane;
		private System.Windows.Forms.Panel JChannel;
		private System.Windows.Forms.Panel MdiContainer;
		private System.ComponentModel.IContainer components;
		private System.Timers.Timer TrundleTimer;
		
		private int NewTextDI = 0;
		// Pen
		private Pen FramePen ;
		private Pen BlackPen ;
		// Brush
		private SolidBrush TabBrush ;
		private SolidBrush CurTabBrush ;
		// Font
		private Font TabFont ;
		private Font CurTabFont ;

		// 用于计时器
		private EventHandler ehTimeTab_Tick_L ;
		private EventHandler ehTimeTab_Tick_R ; 
		private EventHandler ehTimeTab_Tick = null ;
		private EventHandler ehTextEditPane_TextChanged;

		// picture 
		private Image ImgX ;
		private Image ImgGongJu ;
		private Image ImgNailFocusIn ;
		private Image ImgNailUnFocusIn ;
		private Image ImgNailFocusOut ;
		private Image ImgNailUnFocusOut ;
		// Mypath
		private string AppPath ;
		// Tab 是否更改 是否要重画 TextPane(richtextbox)
		// 在 MdiTabs_MouseDown 与 MdiTabs_MouseUp 中使用
		private bool TabChange = false ;
		//
		private System.Windows.Forms.Splitter JWndSpliter;

		// this.GenericPane state
		private WndState GenericPaneState = WndState.Outing ;
		private WndMouseState GenericPaneMouseState = WndMouseState.None ;
		private WndMouseState MdiContainerMouseState = WndMouseState.None ;


		private System.Windows.Forms.ImageList TreeVImgList;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem DTMenuOpen;
		private System.Windows.Forms.MenuItem DTMenuOpenInCurWnd;
		private System.Windows.Forms.MenuItem DTMenuDel;
		private System.Windows.Forms.ContextMenu DocTreeMenu;
		private System.Windows.Forms.MenuItem DTMenuDao;
		private System.Windows.Forms.MenuItem DTMenuDaoFile;
		private System.Windows.Forms.MenuItem DTMenuDaoFolder;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.Timer TimeTab;
		private System.Windows.Forms.ContextMenu TabsMenu;
		private System.Windows.Forms.MainMenu JMenu;
		private System.Windows.Forms.MenuItem JMenuFile;
		private System.Windows.Forms.MenuItem JMenuNew;
		private System.Windows.Forms.MenuItem JMenuOpen;
		private System.Windows.Forms.MenuItem JMenuLine1;
		private System.Windows.Forms.MenuItem JMenuSave;
		private System.Windows.Forms.MenuItem JMenuLine2;
		private System.Windows.Forms.MenuItem JMenuExit;
		private System.Windows.Forms.MenuItem TabsMenuSave;
		private System.Windows.Forms.MenuItem TabsMenuClose;
		private System.Windows.Forms.ContextMenu TPaneMenu;
		private System.Windows.Forms.MenuItem TPaneMenuUnDo;
		private System.Windows.Forms.MenuItem TPaneMenuReDo;
		private System.Windows.Forms.MenuItem TPaneMenuLine1;
		private System.Windows.Forms.MenuItem TPaneMenuCut;
		private System.Windows.Forms.MenuItem TPaneMenuCopy;
		private System.Windows.Forms.MenuItem TPaneMenuLine2;
		private System.Windows.Forms.MenuItem TPaneMenuSelectA;
		private System.Windows.Forms.MenuItem TPaneMenuPaste;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem JMenuWrap;
		private System.Windows.Forms.MenuItem JMenuFont;
		private System.Windows.Forms.MenuItem JMenuBColor;
		private System.Windows.Forms.MenuItem JMenuFColor;
		private System.Windows.Forms.MenuItem JMenuSaveAll;
		private System.Windows.Forms.MenuItem DTMenuNew;
		private System.Windows.Forms.MenuItem DTMenuReName;
		private System.Windows.Forms.MenuItem DTMenuNewF;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem JMenuHelpAbout;

		// One and only DocObject as 
		private MdiTextInBox MdiDocManager ;

		#endregion 

		public FormJ()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			FramePen = new Pen (Color.FromArgb (172,168,153), 1) ;
			BlackPen = new Pen (Color.Black, 1) ;
			TabFont = new Font (this.MdiTabs.Font.Name, (float)8.5) ;
			TabBrush = new SolidBrush (Color.FromArgb (129,126,115)) ;
			CurTabFont  = new Font (this.MdiTabs.Font.Name, (float)8.5, FontStyle.Bold) ; 
			CurTabBrush = new SolidBrush (Color.FromArgb (236,233,216)) ;

			
			this.AppPath = Path.GetDirectoryName (Application.ExecutablePath) ;
			// Load Picture 
			try 
			{
				ImgX = Image.FromFile (this.AppPath + @"\Res\X.bmp") ;
				ImgGongJu = Image.FromFile (this.AppPath+ @"\Res\GongJu.bmp") ;
				ImgNailFocusIn = Image.FromFile (this.AppPath + @"\Res\Nail1.bmp") ;
				ImgNailFocusOut = Image.FromFile (this.AppPath + @"\Res\Nail2.bmp") ;
				ImgNailUnFocusIn = Image.FromFile (this.AppPath + @"\Res\Nail3.bmp") ;
				ImgNailUnFocusOut = Image.FromFile (this.AppPath + @"\Res\Nail4.bmp") ;
			}
			catch (Exception e)
			{
				MessageBox.Show (e.Message + "\nPlease inspect the path--..\\Res.") ;
			}

			this.MdiDocManager = new MdiTextInBox () ;
			this.MdiDocManager.FirstTimeOpenFileMsg += new System.EventHandler(
				this.MdiDocManager_FirstTimeOpenFileMsg) ;
			this.MdiDocManager.AllClosedMsg += new System.EventHandler(
				this.MdiDocManager_AllClosedMsg) ;

			//
			ehTimeTab_Tick_L = new EventHandler (this.TimeTab_Tick_L) ;
			ehTimeTab_Tick_R = new EventHandler (this.TimeTab_Tick_R) ;
			// 

		}



		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormJ));
			this.TextEditPane = new System.Windows.Forms.RichTextBox();
			this.TPaneMenu = new System.Windows.Forms.ContextMenu();
			this.TPaneMenuUnDo = new System.Windows.Forms.MenuItem();
			this.TPaneMenuReDo = new System.Windows.Forms.MenuItem();
			this.TPaneMenuLine1 = new System.Windows.Forms.MenuItem();
			this.TPaneMenuCut = new System.Windows.Forms.MenuItem();
			this.TPaneMenuCopy = new System.Windows.Forms.MenuItem();
			this.TPaneMenuPaste = new System.Windows.Forms.MenuItem();
			this.TPaneMenuLine2 = new System.Windows.Forms.MenuItem();
			this.TPaneMenuSelectA = new System.Windows.Forms.MenuItem();
			this.MdiTabOwner = new System.Windows.Forms.Panel();
			this.MdiTabs = new System.Windows.Forms.Panel();
			this.TabsMenu = new System.Windows.Forms.ContextMenu();
			this.TabsMenuSave = new System.Windows.Forms.MenuItem();
			this.TabsMenuClose = new System.Windows.Forms.MenuItem();
			this.JMenu = new System.Windows.Forms.MainMenu();
			this.JMenuFile = new System.Windows.Forms.MenuItem();
			this.JMenuNew = new System.Windows.Forms.MenuItem();
			this.JMenuOpen = new System.Windows.Forms.MenuItem();
			this.JMenuLine1 = new System.Windows.Forms.MenuItem();
			this.JMenuSave = new System.Windows.Forms.MenuItem();
			this.JMenuSaveAll = new System.Windows.Forms.MenuItem();
			this.JMenuLine2 = new System.Windows.Forms.MenuItem();
			this.JMenuExit = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.JMenuWrap = new System.Windows.Forms.MenuItem();
			this.JMenuFont = new System.Windows.Forms.MenuItem();
			this.JMenuBColor = new System.Windows.Forms.MenuItem();
			this.JMenuFColor = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.JMenuHelpAbout = new System.Windows.Forms.MenuItem();
			this.JChannel = new System.Windows.Forms.Panel();
			this.GenericPane = new System.Windows.Forms.Panel();
			this.DocTree = new System.Windows.Forms.TreeView();
			this.DocTreeMenu = new System.Windows.Forms.ContextMenu();
			this.DTMenuNew = new System.Windows.Forms.MenuItem();
			this.DTMenuNewF = new System.Windows.Forms.MenuItem();
			this.DTMenuOpen = new System.Windows.Forms.MenuItem();
			this.DTMenuOpenInCurWnd = new System.Windows.Forms.MenuItem();
			this.DTMenuReName = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.DTMenuDao = new System.Windows.Forms.MenuItem();
			this.DTMenuDaoFile = new System.Windows.Forms.MenuItem();
			this.DTMenuDaoFolder = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.DTMenuDel = new System.Windows.Forms.MenuItem();
			this.TreeVImgList = new System.Windows.Forms.ImageList(this.components);
			this.MdiContainer = new System.Windows.Forms.Panel();
			this.TrundleTimer = new System.Timers.Timer();
			this.JWndSpliter = new System.Windows.Forms.Splitter();
			this.TimeTab = new System.Windows.Forms.Timer(this.components);
			this.MdiTabOwner.SuspendLayout();
			this.GenericPane.SuspendLayout();
			this.MdiContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TrundleTimer)).BeginInit();
			this.SuspendLayout();
			// 
			// TextEditPane
			// 
			this.TextEditPane.AcceptsTab = true;
			this.TextEditPane.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextEditPane.ContextMenu = this.TPaneMenu;
			this.TextEditPane.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TextEditPane.Location = new System.Drawing.Point(18, 26);
			this.TextEditPane.Name = "TextEditPane";
			this.TextEditPane.Size = new System.Drawing.Size(240, 112);
			this.TextEditPane.TabIndex = 1;
			this.TextEditPane.Tag = "";
			this.TextEditPane.Text = "";
			this.TextEditPane.WordWrap = false;
			this.TextEditPane.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextEditPane_KeyDown);
			this.TextEditPane.Enter += new System.EventHandler(this.TextEditPane_Enter);
			// 
			// TPaneMenu
			// 
			this.TPaneMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.TPaneMenuUnDo,
																					  this.TPaneMenuReDo,
																					  this.TPaneMenuLine1,
																					  this.TPaneMenuCut,
																					  this.TPaneMenuCopy,
																					  this.TPaneMenuPaste,
																					  this.TPaneMenuLine2,
																					  this.TPaneMenuSelectA});
			this.TPaneMenu.Popup += new System.EventHandler(this.TPaneMenu_Popup);
			// 
			// TPaneMenuUnDo
			// 
			this.TPaneMenuUnDo.Index = 0;
			this.TPaneMenuUnDo.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
			this.TPaneMenuUnDo.Text = "撤消(&U)";
			this.TPaneMenuUnDo.Click += new System.EventHandler(this.TPaneMenuUnDo_Click);
			// 
			// TPaneMenuReDo
			// 
			this.TPaneMenuReDo.Index = 1;
			this.TPaneMenuReDo.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.TPaneMenuReDo.Text = "重复(&R)";
			this.TPaneMenuReDo.Click += new System.EventHandler(this.TPaneMenuReDo_Click);
			// 
			// TPaneMenuLine1
			// 
			this.TPaneMenuLine1.Index = 2;
			this.TPaneMenuLine1.Text = "-";
			// 
			// TPaneMenuCut
			// 
			this.TPaneMenuCut.Index = 3;
			this.TPaneMenuCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.TPaneMenuCut.Text = "剪切(&T)";
			this.TPaneMenuCut.Click += new System.EventHandler(this.TPaneMenuCut_Click);
			// 
			// TPaneMenuCopy
			// 
			this.TPaneMenuCopy.Index = 4;
			this.TPaneMenuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.TPaneMenuCopy.Text = "复制(&C)";
			this.TPaneMenuCopy.Click += new System.EventHandler(this.TPaneMenuCopy_Click);
			// 
			// TPaneMenuPaste
			// 
			this.TPaneMenuPaste.Index = 5;
			this.TPaneMenuPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.TPaneMenuPaste.Text = "粘贴(&P)";
			this.TPaneMenuPaste.Click += new System.EventHandler(this.TPaneMenuPaste_Click);
			// 
			// TPaneMenuLine2
			// 
			this.TPaneMenuLine2.Index = 6;
			this.TPaneMenuLine2.Text = "-";
			// 
			// TPaneMenuSelectA
			// 
			this.TPaneMenuSelectA.Index = 7;
			this.TPaneMenuSelectA.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.TPaneMenuSelectA.Text = "全选(&S)";
			this.TPaneMenuSelectA.Click += new System.EventHandler(this.TPaneMenuSelectA_Click);
			// 
			// MdiTabOwner
			// 
			this.MdiTabOwner.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(251)), ((System.Byte)(233)));
			this.MdiTabOwner.Controls.Add(this.MdiTabs);
			this.MdiTabOwner.Location = new System.Drawing.Point(4, 1);
			this.MdiTabOwner.Name = "MdiTabOwner";
			this.MdiTabOwner.Size = new System.Drawing.Size(450, 22);
			this.MdiTabOwner.TabIndex = 3;
			this.MdiTabOwner.SizeChanged += new System.EventHandler(this.MdiTabOwner_SizeChanged);
			this.MdiTabOwner.Paint += new System.Windows.Forms.PaintEventHandler(this.MdiTabOwner_Paint);
			// 
			// MdiTabs
			// 
			this.MdiTabs.ContextMenu = this.TabsMenu;
			this.MdiTabs.Font = new System.Drawing.Font("Courier New", 8F);
			this.MdiTabs.Location = new System.Drawing.Point(0, 0);
			this.MdiTabs.Name = "MdiTabs";
			this.MdiTabs.Size = new System.Drawing.Size(22, 22);
			this.MdiTabs.TabIndex = 0;
			this.MdiTabs.SizeChanged += new System.EventHandler(this.MdiTabs_SizeChanged);
			this.MdiTabs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MdiTabs_MouseUp);
			this.MdiTabs.Paint += new System.Windows.Forms.PaintEventHandler(this.MdiTabs_Paint);
			this.MdiTabs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MdiTabs_MouseDown);
			// 
			// TabsMenu
			// 
			this.TabsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.TabsMenuSave,
																					 this.TabsMenuClose});
			// 
			// TabsMenuSave
			// 
			this.TabsMenuSave.Index = 0;
			this.TabsMenuSave.Text = "保存(&S)";
			this.TabsMenuSave.Click += new System.EventHandler(this.TabsMenuSave_Click);
			// 
			// TabsMenuClose
			// 
			this.TabsMenuClose.Index = 1;
			this.TabsMenuClose.Text = "关闭(&C)";
			this.TabsMenuClose.Click += new System.EventHandler(this.TabsMenuClose_Click);
			// 
			// JMenu
			// 
			this.JMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				  this.JMenuFile,
																				  this.menuItem1,
																				  this.menuItem2});
			// 
			// JMenuFile
			// 
			this.JMenuFile.Index = 0;
			this.JMenuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.JMenuNew,
																					  this.JMenuOpen,
																					  this.JMenuLine1,
																					  this.JMenuSave,
																					  this.JMenuSaveAll,
																					  this.JMenuLine2,
																					  this.JMenuExit});
			this.JMenuFile.Text = "文件(&F)";
			// 
			// JMenuNew
			// 
			this.JMenuNew.Index = 0;
			this.JMenuNew.Text = "新建文件(&N)";
			this.JMenuNew.Click += new System.EventHandler(this.JMenuNew_Click);
			// 
			// JMenuOpen
			// 
			this.JMenuOpen.Index = 1;
			this.JMenuOpen.Text = "打开其他(&O)...";
			this.JMenuOpen.Click += new System.EventHandler(this.JMenuOpen_Click);
			// 
			// JMenuLine1
			// 
			this.JMenuLine1.Index = 2;
			this.JMenuLine1.Text = "-";
			// 
			// JMenuSave
			// 
			this.JMenuSave.Index = 3;
			this.JMenuSave.Text = "保存(&S)";
			this.JMenuSave.Click += new System.EventHandler(this.TabsMenuSave_Click);
			// 
			// JMenuSaveAll
			// 
			this.JMenuSaveAll.Index = 4;
			this.JMenuSaveAll.Text = "全部保存(&L)";
			this.JMenuSaveAll.Click += new System.EventHandler(this.JMenuSaveAll_Click);
			// 
			// JMenuLine2
			// 
			this.JMenuLine2.Index = 5;
			this.JMenuLine2.Text = "-";
			// 
			// JMenuExit
			// 
			this.JMenuExit.Index = 6;
			this.JMenuExit.Text = "退出(&X)";
			this.JMenuExit.Click += new System.EventHandler(this.JMenuExit_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.JMenuWrap,
																					  this.JMenuFont,
																					  this.JMenuBColor,
																					  this.JMenuFColor});
			this.menuItem1.Text = "查看(&V)";
			// 
			// JMenuWrap
			// 
			this.JMenuWrap.Index = 0;
			this.JMenuWrap.Text = "自动换行(&W)";
			this.JMenuWrap.Click += new System.EventHandler(this.JMenuWrap_Click);
			// 
			// JMenuFont
			// 
			this.JMenuFont.Index = 1;
			this.JMenuFont.Text = "字体(&F)...";
			this.JMenuFont.Click += new System.EventHandler(this.JMenuFont_Click);
			// 
			// JMenuBColor
			// 
			this.JMenuBColor.Index = 2;
			this.JMenuBColor.Text = "背景色(&B)...";
			this.JMenuBColor.Click += new System.EventHandler(this.JMenuBColor_Click);
			// 
			// JMenuFColor
			// 
			this.JMenuFColor.Index = 3;
			this.JMenuFColor.Text = "前景色(&C)...";
			this.JMenuFColor.Click += new System.EventHandler(this.JMenuFColor_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.JMenuHelpAbout});
			this.menuItem2.Text = "帮助(&H)";
			// 
			// JMenuHelpAbout
			// 
			this.JMenuHelpAbout.Index = 0;
			this.JMenuHelpAbout.Text = "关于 &J_Note...";
			this.JMenuHelpAbout.Click += new System.EventHandler(this.JMenuHelpAbout_Click);
			// 
			// JChannel
			// 
			this.JChannel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(251)), ((System.Byte)(233)));
			this.JChannel.Dock = System.Windows.Forms.DockStyle.Left;
			this.JChannel.Location = new System.Drawing.Point(0, 0);
			this.JChannel.Name = "JChannel";
			this.JChannel.Size = new System.Drawing.Size(20, 417);
			this.JChannel.TabIndex = 4;
			this.JChannel.Paint += new System.Windows.Forms.PaintEventHandler(this.JChannel_Paint);
			this.JChannel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JChannel_MouseDown);
			// 
			// GenericPane
			// 
			this.GenericPane.BackColor = System.Drawing.SystemColors.Control;
			this.GenericPane.Controls.Add(this.DocTree);
			this.GenericPane.Location = new System.Drawing.Point(24, 0);
			this.GenericPane.Name = "GenericPane";
			this.GenericPane.Size = new System.Drawing.Size(149, 417);
			this.GenericPane.TabIndex = 6;
			this.GenericPane.SizeChanged += new System.EventHandler(this.GenericPane_SizeChanged);
			this.GenericPane.Enter += new System.EventHandler(this.GenericPane_Enter);
			this.GenericPane.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GenericPane_MouseUp);
			this.GenericPane.Paint += new System.Windows.Forms.PaintEventHandler(this.GenericPane_Paint);
			this.GenericPane.Leave += new System.EventHandler(this.GenericPane_Leave);
			this.GenericPane.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GenericPane_MouseMove);
			this.GenericPane.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GenericPane_MouseDown);
			// 
			// DocTree
			// 
			this.DocTree.ContextMenu = this.DocTreeMenu;
			this.DocTree.ImageList = this.TreeVImgList;
			this.DocTree.LabelEdit = true;
			this.DocTree.Location = new System.Drawing.Point(2, 24);
			this.DocTree.Name = "DocTree";
			this.DocTree.Size = new System.Drawing.Size(144, 392);
			this.DocTree.Sorted = true;
			this.DocTree.TabIndex = 2;
			this.DocTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DocTree_KeyDown);
			this.DocTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.DocTree_AfterExpand);
			this.DocTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.DocTree_AfterCollapse);
			this.DocTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DocTree_MouseUp);
			this.DocTree.DoubleClick += new System.EventHandler(this.DocTree_DoubleClick);
			this.DocTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.DocTree_AfterLabelEdit);
			// 
			// DocTreeMenu
			// 
			this.DocTreeMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.DTMenuNew,
																						this.DTMenuNewF,
																						this.DTMenuOpen,
																						this.DTMenuOpenInCurWnd,
																						this.DTMenuReName,
																						this.menuItem6,
																						this.DTMenuDao,
																						this.menuItem4,
																						this.DTMenuDel});
			this.DocTreeMenu.Popup += new System.EventHandler(this.DocTreeMenu_Popup);
			// 
			// DTMenuNew
			// 
			this.DTMenuNew.Index = 0;
			this.DTMenuNew.Text = "新建文件(&N)";
			this.DTMenuNew.Click += new System.EventHandler(this.JMenuNew_Click);
			// 
			// DTMenuNewF
			// 
			this.DTMenuNewF.Index = 1;
			this.DTMenuNewF.Text = "新建文件夹(F)";
			this.DTMenuNewF.Click += new System.EventHandler(this.DTMenuNewF_Click);
			// 
			// DTMenuOpen
			// 
			this.DTMenuOpen.Index = 2;
			this.DTMenuOpen.Text = "打开(&O)";
			this.DTMenuOpen.Click += new System.EventHandler(this.DTMenuOpen_Click);
			// 
			// DTMenuOpenInCurWnd
			// 
			this.DTMenuOpenInCurWnd.Index = 3;
			this.DTMenuOpenInCurWnd.Text = "在当前窗口打开(&C)";
			// 
			// DTMenuReName
			// 
			this.DTMenuReName.Index = 4;
			this.DTMenuReName.Text = "重命名(&M)";
			this.DTMenuReName.Click += new System.EventHandler(this.DTMenuReName_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 5;
			this.menuItem6.Text = "-";
			// 
			// DTMenuDao
			// 
			this.DTMenuDao.Index = 6;
			this.DTMenuDao.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.DTMenuDaoFile,
																					  this.DTMenuDaoFolder});
			this.DTMenuDao.Text = "导入(&I)";
			// 
			// DTMenuDaoFile
			// 
			this.DTMenuDaoFile.Index = 0;
			this.DTMenuDaoFile.Text = "文件(&F)..";
			this.DTMenuDaoFile.Click += new System.EventHandler(this.DTMenuDaoFile_Click);
			// 
			// DTMenuDaoFolder
			// 
			this.DTMenuDaoFolder.Index = 1;
			this.DTMenuDaoFolder.Text = "文件夹(&D)..";
			this.DTMenuDaoFolder.Click += new System.EventHandler(this.DTMenuDaoFolder_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 7;
			this.menuItem4.Text = "-";
			// 
			// DTMenuDel
			// 
			this.DTMenuDel.Index = 8;
			this.DTMenuDel.Text = "删除(&D)";
			this.DTMenuDel.Click += new System.EventHandler(this.DTMenuDel_Click);
			// 
			// TreeVImgList
			// 
			this.TreeVImgList.ImageSize = new System.Drawing.Size(16, 16);
			this.TreeVImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeVImgList.ImageStream")));
			this.TreeVImgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// MdiContainer
			// 
			this.MdiContainer.BackColor = System.Drawing.SystemColors.Control;
			this.MdiContainer.Controls.Add(this.TextEditPane);
			this.MdiContainer.Controls.Add(this.MdiTabOwner);
			this.MdiContainer.Location = new System.Drawing.Point(184, 16);
			this.MdiContainer.Name = "MdiContainer";
			this.MdiContainer.Size = new System.Drawing.Size(528, 240);
			this.MdiContainer.TabIndex = 5;
			this.MdiContainer.SizeChanged += new System.EventHandler(this.MdiContainer_SizeChanged);
			this.MdiContainer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MdiContainer_MouseUp);
			this.MdiContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.MdiContainer_Paint);
			this.MdiContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MdiContainer_MouseMove);
			this.MdiContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MdiContainer_MouseDown);
			// 
			// TrundleTimer
			// 
			this.TrundleTimer.Interval = 30;
			this.TrundleTimer.SynchronizingObject = this;
			this.TrundleTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.TrundleTimer_Elapsed);
			// 
			// JWndSpliter
			// 
			this.JWndSpliter.BackColor = System.Drawing.SystemColors.Control;
			this.JWndSpliter.Location = new System.Drawing.Point(169, 0);
			this.JWndSpliter.Name = "JWndSpliter";
			this.JWndSpliter.Size = new System.Drawing.Size(2, 417);
			this.JWndSpliter.TabIndex = 8;
			this.JWndSpliter.TabStop = false;
			// 
			// TimeTab
			// 
			this.TimeTab.Interval = 30;
			// 
			// FormJ
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(728, 417);
			this.Controls.Add(this.JChannel);
			this.Controls.Add(this.GenericPane);
			this.Controls.Add(this.MdiContainer);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.Menu = this.JMenu;
			this.Name = "FormJ";
			this.Text = "J";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormJ_Closing);
			this.SizeChanged += new System.EventHandler(this.FormJ_SizeChanged);
			this.Load += new System.EventHandler(this.FormJ_Load);
			this.MdiTabOwner.ResumeLayout(false);
			this.GenericPane.ResumeLayout(false);
			this.MdiContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TrundleTimer)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormJ());
		}

		private void FormJ_Load(object sender, System.EventArgs e)
		{
			this.GenericPane.Location = new Point (20 - GenericPane.Size.Width, 0) ;
			this.MdiContainer.Location = new Point (20, 0) ;
			this.DocTree.Size = new Size (GenericPane.Size.Width - 4, GenericPane.Size.Height -25) ;
			this.TreeViewLoadNote ();
			// set Controls's state
			this.GenericPaneState = WndState.Docking ;
			this.JChannel.Visible = false ;
			this.GenericPane.Dock = DockStyle.Left ;
			this.MdiContainer .Dock = DockStyle.Fill ;
		
			// Set controls's order in the set.
			this.Controls.Remove (this.GenericPane) ;
			this.Controls.Add (this.JWndSpliter) ;
			this.Controls.Add (this.GenericPane) ;
			this.DocTree.Size = new Size (GenericPane.Size.Width - 2, GenericPane.Size.Height -25) ;

		
			// open today txt
			this.OpenFileFromDocTree () ;
				//
			DeserializeTexePaneAtt () ;
			this.DocTree.ForeColor = this.TextEditPane.ForeColor ;

			this.ehTextEditPane_TextChanged = 
				new System.EventHandler	(this.TextEditPane_TextChanged) ;
			this.TextEditPane.TextChanged += this.ehTextEditPane_TextChanged ;
		}

		private void FormJ_SizeChanged(object sender, System.EventArgs e)
		{
			if (this.GenericPaneState == WndState.Docking) return ;
			this.MdiContainer.Size = new Size (this.ClientSize.Width - 20, this.ClientSize.Height) ; 
			this.GenericPane.Size = new Size (GenericPane.Size.Width, this.ClientSize.Height) ;
		}

		private void FormJ_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (MdiDocManager.IsEmpty ())
				return ;
			MdiDocManager.TextTakeIn (this.TextEditPane.Lines) ;
			ArrayList Files = MdiDocManager.GetAllFileNoteSave () ;
			// 这里不完善
			if (Files == null) 
				return ;
			if (Files.Count != 0)
			{
				FormSave formS = new FormSave () ;
				int i = 0 ;
				foreach (TextInFile tf in Files)
				{
					formS.LBFlieName.Items.Add (Path.GetFileName (tf.FileName)) ;
					formS.LBFlieName.SelectedIndex = i ++ ;
				}
				switch (formS.ShowDialog (this))
				{
					case DialogResult.Yes : 
						for (i = 0 ; i < formS.LBFlieName.SelectedIndices.Count  ; i ++)
						{
							((TextInFile)Files[formS.LBFlieName.SelectedIndices[i]]).Save () ;
						}
						break ;
					case DialogResult.No :
						break ;
					case DialogResult.Cancel :
						e.Cancel = true ;
						break ;
				}
				formS.Dispose () ;
			}
			if (!e.Cancel)
			{
				// Serialize  TextEditPane
				this.SerializeTexePaneAtt () ;
			}
		}


		private void JChannel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics dc = e.Graphics ; 
			dc.DrawImageUnscaled (this.ImgGongJu, 0,0) ;
		}
  
		private void JChannel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// 如果按下鼠标在工具箱图片上 
			// 并且 工具箱窗口正隐藏
			// 则 计时器开始 滚动窗口
			if (e.Y < 80 && this.GenericPane.Location.X <= GenericPane.Size.Width - 20
				&& this.GenericPaneState != WndState.Docking)
			{
				this.GenericPaneState = WndState.Outing ;
				this.TrundleTimer.Enabled = true ;
			}
		}


		private void GenericPane_SizeChanged(object sender, System.EventArgs e)
		{
			if (this.GenericPaneState == WndState.Docking)
				this.DocTree.Size = new Size (GenericPane.Size.Width - 2, GenericPane.Size.Height -25) ;
			else 
				this.DocTree.Size = new Size (GenericPane.Size.Width - 4, GenericPane.Size.Height -25) ;
			this.GenericPane.Invalidate () ;
		}

		private void GenericPane_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics dc = e.Graphics ;
			if (this.GenericPaneState != WndState.Docking)
			{
				dc.DrawLine (this.FramePen, GenericPane.Size.Width - 2, 0, 
					GenericPane.Size.Width - 2, GenericPane.Size.Height) ;
				dc.DrawLine (this.BlackPen, GenericPane.Size.Width - 1, 0, 
					GenericPane.Size.Width - 1, GenericPane.Size.Height) ;
				if (this.DocTree.Focused)
				{
					dc.FillRectangle (Brushes.Blue, 2, 2, this.GenericPane.Size.Width -6, 18) ;	
					dc.DrawImageUnscaled (this.ImgNailFocusOut, this.GenericPane.Size.Width -46, 2) ;
					// 标题
					dc.DrawString ("MY TEXT", this.TabFont, Brushes.White, 5, 5) ;
				}
				else 
				{	
					dc.DrawImageUnscaled (this.ImgNailUnFocusOut, this.GenericPane.Size.Width -46, 2) ;
					dc.DrawRectangle (this.FramePen, 2, 2, this.GenericPane.Size.Width -6, 18) ;
					// 标题
					dc.DrawString ("MY TEXT", this.TabFont, Brushes.Black, 5, 5) ;
				}
			}
			else  // Docking 
			{
				if (this.DocTree.Focused)
				{
					dc.FillRectangle (Brushes.Blue, 2, 2, this.GenericPane.Size.Width -3, 18) ;	
					dc.DrawImageUnscaled (this.ImgNailFocusIn, this.GenericPane.Size.Width -47, 2) ;
					// 标题
					dc.DrawString ("MY TEXT", this.TabFont, Brushes.White, 5, 5) ;
				}
				else 
				{	
					dc.DrawImageUnscaled (this.ImgNailUnFocusIn, this.GenericPane.Size.Width -47, 2) ;
					dc.DrawRectangle (this.FramePen, 2, 2, this.GenericPane.Size.Width -3, 18) ;
					// 标题
					dc.DrawString ("MY TEXT", this.TabFont, Brushes.Black, 5, 5) ;
				}
			}
	
			// 实现按钮阴影效果
			switch (this.GenericPaneMouseState)
			{
				case WndMouseState.MouseOnX :
					dc.DrawLine (Pens.White, this.GenericPane.Size.Width -10,5,
						this.GenericPane.Size.Width -22,5);
					dc.DrawLine (Pens.White, this.GenericPane.Size.Width -22,16,
						this.GenericPane.Size.Width -22,5);
					dc.DrawLine (Pens.Black, this.GenericPane.Size.Width -9,5,
						this.GenericPane.Size.Width -9,18);
					dc.DrawLine (Pens.Black, this.GenericPane.Size.Width -9,18,
						this.GenericPane.Size.Width -22,18);
					break ;
				case WndMouseState.MouseOnNail :
					dc.DrawLine (Pens.White, this.GenericPane.Size.Width -30,5,
						this.GenericPane.Size.Width -42,5);
					dc.DrawLine (Pens.White, this.GenericPane.Size.Width -42,16,
						this.GenericPane.Size.Width -42,5);
					dc.DrawLine (Pens.Black, this.GenericPane.Size.Width -29,5,
						this.GenericPane.Size.Width -29,18);
					dc.DrawLine (Pens.Black, this.GenericPane.Size.Width -29,18,
						this.GenericPane.Size.Width -42,18);
					break ;

				case WndMouseState.LButtonDownX :
					dc.DrawLine (Pens.Black, this.GenericPane.Size.Width -10,5,
						this.GenericPane.Size.Width -22,5);
					dc.DrawLine (Pens.Black, this.GenericPane.Size.Width -22,16,
						this.GenericPane.Size.Width -22,5);
					dc.DrawLine (Pens.White, this.GenericPane.Size.Width -9,5,
						this.GenericPane.Size.Width -9,18);
					dc.DrawLine (Pens.White, this.GenericPane.Size.Width -9,18,
						this.GenericPane.Size.Width -22,18);

					break ;
				case WndMouseState.LButtonDownNail :
					dc.DrawLine (Pens.Black, this.GenericPane.Size.Width -30,5,
						this.GenericPane.Size.Width -42,5);
					dc.DrawLine (Pens.Black, this.GenericPane.Size.Width -42,16,
						this.GenericPane.Size.Width -42,5);
					dc.DrawLine (Pens.White, this.GenericPane.Size.Width -29,5,
						this.GenericPane.Size.Width -29,18);
					dc.DrawLine (Pens.White, this.GenericPane.Size.Width -29,18,
						this.GenericPane.Size.Width -42,18);
					break;

			}
			
		}

		private void GenericPane_Leave(object sender, System.EventArgs e)
		{
			if (this.GenericPaneState != WndState.Docking) 
			{
				this.GenericPaneState = WndState.Ining ;
				this.TrundleTimer.Enabled = true ;
			}
			this.GenericPane.Invalidate () ;
			this.GenericPane.Update () ;		
		}

		private void GenericPane_Enter(object sender, System.EventArgs e)
		{
			this.GenericPane.Invalidate () ;
			this.GenericPane.Update () ;
		}

		private void GenericPane_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.GenericPaneMouseState == WndMouseState.LButtonDownNail)
			{
				if (this.GenericPaneState != WndState.Docking) 
				{
					// set Controls's state
					this.GenericPaneState = WndState.Docking ;
					this.JChannel.Visible = false ;
					this.GenericPane.Dock = DockStyle.Left ;
					this.MdiContainer .Dock = DockStyle.Fill ;
				
					// Set controls's order in the set.
					this.Controls.Remove (this.GenericPane) ;
					this.Controls.Add (this.JWndSpliter) ;
					this.Controls.Add (this.GenericPane) ;
					this.DocTree.Size = new Size (GenericPane.Size.Width - 2, GenericPane.Size.Height -25) ;
					//
					this.GenericPane.Invalidate () ;
				}
				else // Docking
				{
					// Resume controls's state
					this.GenericPaneState = WndState.Ining ;
					this.MdiContainer .Dock = DockStyle.None ;
					this.GenericPane.Dock = DockStyle.None ;
					this.GenericPane.Location = new Point (20, 0) ;

					// Reset the controls's order
					// 很笨的方法 控制 控件 Z 方向上的次序（即哪个控件窗口在上哪个在下）
					this.Controls.Remove (this.JWndSpliter) ;
					this.Controls.Remove (this.MdiContainer) ;
					this.Controls.Add(this.MdiContainer);
					this.JChannel.Visible = true ;
					this.DocTree.Size = new Size (GenericPane.Size.Width - 4, GenericPane.Size.Height -25) ;
					this.MdiContainer.Size = new Size (this.ClientSize.Width - 20, this.ClientSize.Height) ; 
					this.GenericPane.Size = new Size (GenericPane.Size.Width, this.ClientSize.Height) ;

					// Prepare scroll.
					this.TextEditPane.Focus () ;
					this.GenericPane.Invalidate () ;
					this.TrundleTimer.Enabled = true ;
				}
				this.GenericPaneMouseState = 0 ;
			}
		}

		private void GenericPane_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{ 
				if (this.GenericPaneMouseState == WndMouseState.MouseOnX) 
				{
					this.GenericPaneMouseState = WndMouseState.LButtonDownX ;
				}
				else if (this.GenericPaneMouseState == WndMouseState.MouseOnNail) 
				{
					this.GenericPaneMouseState = WndMouseState.LButtonDownNail;
				}
				this.GenericPane.Invalidate () ;
				this.Update () ;
			}
			this.DocTree.Focus () ;
		}

		private void GenericPane_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// 状态机
			if (e.Button == MouseButtons.Left)
			{
				switch (MouseIsOnNailOrX (e.X, e.Y))
				{
					case WndMouseState.None :
						if (this.GenericPaneMouseState != WndMouseState.None)
						{
							this.GenericPaneMouseState = WndMouseState.None ;
							this.GenericPane.Invalidate () ;
						}
						break ;
					case WndMouseState.MouseOnNail :
						if (this.GenericPaneMouseState != WndMouseState.LButtonDownNail)
						{
							this.GenericPaneMouseState = WndMouseState.LButtonDownNail ;
							this.GenericPane.Invalidate () ;
						}
						break ;
					case WndMouseState.MouseOnX :
						if (this.GenericPaneMouseState != WndMouseState.LButtonDownX)
						{
							this.GenericPaneMouseState = WndMouseState.LButtonDownX ;
							this.GenericPane.Invalidate () ;
						}
						break ;
				}
			}
			else if (e.Button == MouseButtons.None)
			{
				switch (MouseIsOnNailOrX (e.X, e.Y))
				{
					case WndMouseState.None :
						if (this.GenericPaneMouseState != WndMouseState.None)
						{
							this.GenericPaneMouseState = WndMouseState.None ;
							this.GenericPane.Invalidate () ;
						}
						break ;
					case WndMouseState.MouseOnNail :
						if (this.GenericPaneMouseState != WndMouseState.MouseOnNail)
						{
							this.GenericPaneMouseState = WndMouseState.MouseOnNail ;
							this.GenericPane.Invalidate () ;
						}
						break ;
					case WndMouseState.MouseOnX :
						if (this.GenericPaneMouseState != WndMouseState.MouseOnX)
						{
							this.GenericPaneMouseState = WndMouseState.MouseOnX ;
							this.GenericPane.Invalidate () ;
						}
						break ;
				}
			}
		}


		private void MdiDocManager_FirstTimeOpenFileMsg (object sender, EventArgs e)
		{
			this.MdiContainer.Visible = true ;
		}

		private void MdiDocManager_AllClosedMsg(object sender, EventArgs e)
		{
			this.MdiContainer.Visible = false ;
			this.Invalidate () ;
		}


		private void MdiContainer_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics dc = e.Graphics ;
			// Drow Frame
			dc.DrawRectangle (this.FramePen, 1, 0, 
				this.MdiContainer.Size.Width -2, 
				this.MdiContainer.Size.Height -1) ;
			dc.DrawRectangle (this.FramePen, 3, 25,
				this.MdiContainer.Size.Width -6, 
				this.MdiContainer.Size.Height -28) ;
			dc.FillRectangle (new SolidBrush (this.TextEditPane.BackColor), 4, 26,
				this.MdiContainer.Size.Width -8, 
				this.MdiContainer.Size.Height -30) ;
			// Show pic "X"
			dc.DrawImageUnscaled (this.ImgX, this.MdiContainer.Size.Width -76, 2) ;

			// Drow butten effect
			switch (this.MdiContainerMouseState)
			{
				case WndMouseState.MouseOnLeftBtn :
					if ((this.GetTabsState() & TabsState.OutRight) == 0) break ;
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -46,5,
						this.MdiContainer.Size.Width -34,5);
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -46,16,
						this.MdiContainer.Size.Width -46,5);
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -34,5,
						this.MdiContainer.Size.Width -34,18);
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -34,18,
						this.MdiContainer.Size.Width -46,18);
					break ;
				case WndMouseState.MouseOnRightBtn :
					if ((GetTabsState() & TabsState.OutLeft) == 0) break ;
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -32,5,
						this.MdiContainer.Size.Width -20,5);
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -32,16,
						this.MdiContainer.Size.Width -32,5);
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -20,5,
						this.MdiContainer.Size.Width -20,18);
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -20,18,
						this.MdiContainer.Size.Width -32,18);
					break ;
				case WndMouseState.MouseOnX :
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -18,5,
						this.MdiContainer.Size.Width -5,5);
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -18,16,
						this.MdiContainer.Size.Width -18,5);
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -5,5,
						this.MdiContainer.Size.Width -5,18);
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -5,18,
						this.MdiContainer.Size.Width -18,18);
					break ;
				case WndMouseState.LButtonDownLeftBtn :
					if ((GetTabsState() & TabsState.OutRight) == 0) break ;
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -46,5,
						this.MdiContainer.Size.Width -34,5);
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -46,16,
						this.MdiContainer.Size.Width -46,5);
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -34,5,
						this.MdiContainer.Size.Width -34,18);
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -34,18,
						this.MdiContainer.Size.Width -46,18);
					break ;
				case WndMouseState.LButtonDownRightBtn :
					if ((GetTabsState() & TabsState.OutLeft) == 0) break ;
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -32,5,
						this.MdiContainer.Size.Width -20,5);
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -32,16,
						this.MdiContainer.Size.Width -32,5);
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -20,5,
						this.MdiContainer.Size.Width -20,18);
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -20,18,
						this.MdiContainer.Size.Width -32,18);
					break ;
				case WndMouseState.LButtonDownX :
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -18,5,
						this.MdiContainer.Size.Width -5,5);
					dc.DrawLine (Pens.Black, this.MdiContainer.Size.Width -18,16,
						this.MdiContainer.Size.Width -18,5);
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -5,5,
						this.MdiContainer.Size.Width -5,18);
					dc.DrawLine (Pens.White, this.MdiContainer.Size.Width -5,18,
						this.MdiContainer.Size.Width -18,18);
					break ;
			}
			// 画图当 "<" ">" 按钮生效时 的效果
			// Point
			Point[] pointLs = 
			{
				new Point (MdiContainer.Size.Width -42, 12) ,
				new Point (MdiContainer.Size.Width -37, 7) ,
				new Point (MdiContainer.Size.Width -37, 16) ,
			} ;

			Point[] pointRs =
			{
				new Point (MdiContainer.Size.Width -23, 12) ,
				new Point (MdiContainer.Size.Width -28, 7) ,
				new Point (MdiContainer.Size.Width -28, 16) ,

			} ;
			// 画实心 ">"
			if ((GetTabsState() & TabsState.OutRight) !=0)
				dc.FillPolygon (this.TabBrush, pointLs) ;
			// 画实心 "<"
			if ((GetTabsState() & TabsState.OutLeft) !=0)
				dc.FillPolygon (this.TabBrush, pointRs) ;

		}

		private void MdiContainer_SizeChanged(object sender, System.EventArgs e)
		{
			this.TextEditPane.Size = new Size (this.MdiContainer.Size.Width -22, 
				this.MdiContainer.Size.Height -30) ;
			this.MdiTabOwner.Size = new Size (this.MdiContainer.Size.Width -76,22) ;
			this.MdiContainer.Invalidate () ;
		}

		private void MdiContainer_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// 状态机模型 实现按钮效果
			if (e.Button == MouseButtons.Left)
			{ 
				if (this.MdiContainerMouseState == WndMouseState.MouseOnX) 
				{
					this.MdiContainerMouseState = WndMouseState.LButtonDownX ;
				}
				else if (this.MdiContainerMouseState == WndMouseState.MouseOnLeftBtn) 
				{
					this.MdiContainerMouseState = WndMouseState.LButtonDownLeftBtn ;
					if ((GetTabsState() & TabsState.OutRight) != 0)
					{
						this.ehTimeTab_Tick = this.ehTimeTab_Tick_L ;
						this.TimeTab.Tick += this.ehTimeTab_Tick ;
						this.TimeTab.Enabled = true ;
					}
				}
				else if (this.MdiContainerMouseState == WndMouseState.MouseOnRightBtn) 
				{
					this.MdiContainerMouseState = WndMouseState.LButtonDownRightBtn ;
					if ((GetTabsState() & TabsState.OutLeft) != 0)
					{
						this.ehTimeTab_Tick = this.ehTimeTab_Tick_R ;
						this.TimeTab.Tick += this.ehTimeTab_Tick ;
						this.TimeTab.Enabled = true ;
					
					}
				}
				this.MdiContainer.Invalidate (
					new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;						
				this.Update () ;
			}
		}

		private void MdiContainer_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// 一个状态机模型 实现按钮效果
			if (e.Button == MouseButtons.Left)
			{
				switch (MouseIsOnLeftOrRightOrX (e.X, e.Y))
				{
					case WndMouseState.None :
						if (this.MdiContainerMouseState != WndMouseState.None)
						{
							this.MdiContainerMouseState = WndMouseState.None ;
							this.MdiContainer.Invalidate (
								new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;
						}
						break ;
					case WndMouseState.MouseOnLeftBtn :
						if (this.MdiContainerMouseState != WndMouseState.LButtonDownLeftBtn)
						{
							this.MdiContainerMouseState = WndMouseState.LButtonDownLeftBtn ;
							this.MdiContainer.Invalidate (
								new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;						
						}
						break ;
					case WndMouseState.MouseOnRightBtn :
						if (this.MdiContainerMouseState != WndMouseState.LButtonDownRightBtn)
						{
							this.MdiContainerMouseState = WndMouseState.LButtonDownRightBtn ;
							this.MdiContainer.Invalidate (
								new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;						
						}
						break ;
					case WndMouseState.MouseOnX :
						if (this.MdiContainerMouseState!= WndMouseState.LButtonDownX)
						{
							this.MdiContainerMouseState = WndMouseState.LButtonDownX ;
							this.MdiContainer.Invalidate (
								new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;						
						}
						break ;
				}
			}
			else if (e.Button == MouseButtons.None)
			{
				switch (MouseIsOnLeftOrRightOrX (e.X, e.Y))
				{
					case WndMouseState.None :
						if (this.MdiContainerMouseState != WndMouseState.None)
						{
							this.MdiContainerMouseState = WndMouseState.None ;
							this.MdiContainer.Invalidate (
								new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;						
						}
						break ;
					case WndMouseState.MouseOnLeftBtn :
						if (this.MdiContainerMouseState != WndMouseState.MouseOnLeftBtn)
						{
							this.MdiContainerMouseState = WndMouseState.MouseOnLeftBtn ;
							this.MdiContainer.Invalidate (
								new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;						
						}
						break ;
					case WndMouseState.MouseOnRightBtn :
						if (this.MdiContainerMouseState != WndMouseState.MouseOnRightBtn)
						{
							this.MdiContainerMouseState = WndMouseState.MouseOnRightBtn ;
							this.MdiContainer.Invalidate (
								new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;						
						}
						break ;
					case WndMouseState.MouseOnX :
						if (this.MdiContainerMouseState!= WndMouseState.MouseOnX)
						{
							this.MdiContainerMouseState = WndMouseState.MouseOnX ;
							this.MdiContainer.Invalidate (
								new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;						
						}
						break ;
				}
			}
		}

		private void MdiContainer_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			switch (MdiContainerMouseState)
			{
					// 单击 X 
					// 关闭当前文档
				case WndMouseState.LButtonDownX :
					TabsMenuClose_Click(sender, e) ;
					break ;
				case WndMouseState.LButtonDownLeftBtn :
				case WndMouseState.LButtonDownRightBtn :
					this.TimeTab.Enabled = false ;
					this.TimeTab.Tick -= this.ehTimeTab_Tick ; 
					break ;
			}
			this.MdiContainerMouseState = WndMouseState.None ;
			this.MdiContainer.Invalidate (
				new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;
			this.TextEditPane.Focus () ;
		}


		private void MdiTabs_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Graphics dc = this.CreateGraphics();
			int nTabX = 10 ;
			string[] arrTitles = MdiDocManager.GetTabsTitle () ;
			for (int i = 0 ; i < arrTitles.Length ; i++)
			{
				// 叠加标题文本的长度 为了搜索被单击的TAB 
				if (i == MdiDocManager.CurTab)
					nTabX += (int)(dc.MeasureString (arrTitles[i], CurTabFont).Width) + 10 ;
				else 
					nTabX += (int)(dc.MeasureString (arrTitles[i], TabFont).Width) + 10 ;
				// 与 e.X 比较 找到了被单击 的TAB 了
				if (nTabX > e.X )
				{
					// 如果 被单击的TAB 就是当前 TAB 
					// 则什么也不做
					if (i == MdiDocManager.CurTab) 
						break ;
					// 否则 
					// 中文本变化将其保存到 MdiDocManager 中
					this.MdiDocManager.TextTakeIn (this.TextEditPane.Lines) ;
					// 被单击的TAB 设置为 当前TAB 并重画TABS 窗口
					MdiDocManager.CurTab = i ;
					this.MdiTabs.Invalidate () ;
					// 设置 TabChange 标志 在MouseUp时更新 TextBox 的内容
					TabChange = true ;	
					break ;
				}
			}
			dc.Dispose () ;

			if (e.Button == MouseButtons.Right)
			{
				// 在出现菜单前 调用 MouseUp 更新 TextBox 的内容
				MdiTabs_MouseUp(sender, e) ;	
				// 为了不要更新 2 次设置标志 为假
				TabChange = false ;
			}
		}

		private void MdiTabs_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.TabChange)
			{
				this.TextPaneShowNewText () ;
				TabChange = false ;
			}
		}

		private void MdiTabs_SizeChanged(object sender, System.EventArgs e)
		{
			if (this.MdiTabs.Size.Width - MdiTabs.Location.X > this.MdiTabOwner.Size.Width)
			{
				this.MdiTabs.Location = new Point (this.MdiTabOwner.Size.Width
					- MdiTabs.Size.Width , 0) ;
				this.MdiContainer.Invalidate (
					new Rectangle (this.MdiContainer.Size.Width - 50,0,50,20)) ;
			}
		}

		private void MdiTabs_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics dc = e.Graphics ; 
			int nTabLen = 10 ;
			string[] arrTitles = null ;
			
			if (MdiDocManager.IsEmpty () ) return ;

			// Draw a white line on botton 
			dc.DrawLine (Pens.White, 0, MdiTabs.Size.Height -1, MdiTabs.Size.Width, MdiTabs.Size.Height -1) ;

			// Draw Tabs's text.
			arrTitles = MdiDocManager.GetTabsTitle () ;
			int i ; // for loop.
			for (i = 0 ; i < MdiDocManager.CurTab ; i ++)
			{
				dc.DrawString (arrTitles[i], TabFont, TabBrush, nTabLen, 7) ; 
				nTabLen += (int)(dc.MeasureString (arrTitles[i], TabFont).Width) + 10 ;
				dc.DrawLine (this.FramePen, nTabLen-5, 6, nTabLen-5, 20) ;
			}
			// Draw CurTab's Text
			// Tab effect
			Rectangle rect = new Rectangle (nTabLen-5, 4, 
				(int)(dc.MeasureString (arrTitles[i], CurTabFont).Width) + 10, 18) ;
			dc.FillRectangle (this.CurTabBrush, rect) ; 
			dc.DrawLine (Pens.Black, rect.X + rect.Width - 1 , rect.Y ,
				rect.X + rect.Width - 1, rect.Y + rect.Height - 1) ;
			dc.DrawLine (Pens.White, rect.X, rect.Y, rect.X , rect.Y + rect.Height - 1) ;
			dc.DrawLine (Pens.White, rect.X, rect.Y, rect.X + rect.Width - 1 , rect.Y ) ;
			// 
			dc.DrawString (arrTitles[i], CurTabFont, Brushes.Black, nTabLen, 7) ; 
			nTabLen += rect.Width ;
			i ++ ;
			// Draw other Tab.
			for (; i < arrTitles.Length ; i ++)
			{
				dc.DrawString (arrTitles[i], TabFont, TabBrush, nTabLen, 7) ; 
				nTabLen += (int)(dc.MeasureString (arrTitles[i], TabFont).Width) + 10 ;
				dc.DrawLine (this.FramePen, nTabLen-5, 6, nTabLen-5, 20) ;

			}
			// Reset pane's size.
			this.MdiTabs.Size = new Size (nTabLen, MdiTabs.Size.Height) ;
		}


		private void MdiTabOwner_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics dc = e.Graphics ;
			dc.DrawLine (Pens.White, 0, MdiTabOwner.Size.Height-1 , MdiTabOwner.Size.Width, MdiTabOwner.Size.Height-1 ) ;
		}

		private void MdiTabOwner_SizeChanged(object sender, System.EventArgs e)
		{
			if (MdiTabs.Location.X != 0) 
			{
				if (MdiTabs.Size.Width + MdiTabs.Location.X < MdiTabOwner.Size.Width) 
				{
					MdiTabs.Location = new Point (MdiTabOwner.Size.Width - MdiTabs.Size.Width, 0) ;
					if (MdiTabs.Location.X > 0)
					{
						MdiTabs.Location = new Point (0, 0) ;
					}
				}

			}
		}


		private void TextEditPane_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}

		private void TextEditPane_Enter(object sender, System.EventArgs e)
		{
			GenericPane_Leave (sender, e) ;	
		}

		private void TextEditPane_TextChanged(object sender, System.EventArgs e)
		{
			if (this.MdiDocManager.SetCurTabChangedFlag ())
			{
				this.MdiTabs.Invalidate () ;
			}
		}


		private void DocTree_DoubleClick(object sender, System.EventArgs e)
		{
			// 如果有新文件要打开...
			if (((FileTreeNode)this.DocTree.SelectedNode).NoteStyle == TreeNodeStyle.File)
			{
				this.OpenFileFromDocTree () ;
			}
		}

		private void DocTree_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (DocTree.GetNodeAt(e.X, e.Y) != null)
			{
				this.DocTree.SelectedNode = DocTree.GetNodeAt(e.X, e.Y) ;
			}
		}

		private void DocTree_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ImageIndex = (int)TreeImgIndex.ClosedFolder ;
		}

		private void DocTree_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ImageIndex = (int)TreeImgIndex.OpendFolder ;
		}

		private void DocTree_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			e.CancelEdit = true ;
			// 原结点
			FileTreeNode fnode = (FileTreeNode)e.Node ;
			// 新名字
			if (e.Label == null) return ;
			string sNewName = e.Label.Trim () ;
			// 新扩展名
			string sExtName = "" ;

			// 如果新名是空串
			if (Path.GetFileNameWithoutExtension(sNewName).Length == 0)
				return ;
			if (e.Node == DocTree.Nodes[0] || e.Node == DocTree.Nodes[1])
			{
				MessageBox.Show ("对不起 您不能从命名根结点！其实这样的设"
					+"计的确不太好，但目前的版本就是这样。", "Rename Error", 
					MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
				return ;
			}
			
			try 
			{
				if (fnode.NoteStyle == TreeNodeStyle.File)
				{
					sExtName = Path.GetExtension (sNewName) ;
					// 如果没写扩展名 则添加
					if (sExtName.Length == 0)
					{
						sNewName += Path.GetExtension (fnode.Text) ;
					}
						// 如果用户更改了扩展名 这不允许
					else if (sExtName != Path.GetExtension (fnode.Text))
					{
						MessageBox.Show ("对不起 您不能更改扩展名！", "Rename Error", 
							MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
						return ;
					}
				}
			}
				// 如果新名中有非法的 字符
			catch (ArgumentException  ae)
			{
				MessageBox.Show (ae.Message, "Rename Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
				return ;
			}
			try 
			{
				if (fnode.NoteStyle == TreeNodeStyle.File)
					this.ReNameFile (this.AppPath + "\\" + fnode.FullPath, 
						this.AppPath + "\\" + fnode.Parent.FullPath + "\\" + sNewName) ;
				else
					this.ReNameFolder (this.AppPath + "\\" + fnode.FullPath, 
						this.AppPath + "\\" + fnode.Parent.FullPath + "\\" + sNewName) ;

			}
			catch (IOException ioe)
			{
				MessageBox.Show (ioe.Message ,"Rename Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
				return ;
			}
			fnode.Text = sNewName ;
			this.MdiTabs.Invalidate () ;
				
		}

		private void DocTree_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				DTMenuDel_Click(sender, e) ;
		}


		private void DTMenuNewF_Click(object sender, System.EventArgs e)
		{
			if (this.DocTree.SelectedNode == null) 
				return ;
			FileTreeNode parentNode = null ;
			this.NewTextDI ++ ;	
			if (((FileTreeNode)this.DocTree.SelectedNode).NoteStyle == TreeNodeStyle.File)
			{
				parentNode = (FileTreeNode)this.DocTree.SelectedNode.Parent ;
			}
			else 
				parentNode = (FileTreeNode)this.DocTree.SelectedNode ;
			// 这里应该没有问题
			// 因为树结点是排序的并且没有重名的
			// 应该可以得到一个新名字
			foreach (TreeNode tn in parentNode.Nodes)
			{
				if (tn.Text == "New" + NewTextDI.ToString())
					NewTextDI ++ ;
			}
			FileTreeNode newftn = new FileTreeNode (TreeNodeStyle.Folder, "New" + this.NewTextDI.ToString()) ; 				
			parentNode.Nodes.Add (newftn) ;
			this.DocTree.SelectedNode = newftn ;
			Directory.CreateDirectory (this.AppPath + "\\" + newftn.FullPath) ;
			newftn.BeginEdit () ;
		}
		private void DTMenuOpen_Click(object sender, System.EventArgs e)
		{
			if (this.DocTree.SelectedNode == null)  return ;
			// Open file
			if (((FileTreeNode)this.DocTree.SelectedNode).NoteStyle == TreeNodeStyle.File)
			{
				this.OpenFileFromDocTree () ;
			}
			// Open 文件夹
			else
			{
				if (!this.DocTree.SelectedNode.IsEditing)
				{
					this.DocTree.SelectedNode.Expand() ;
				}
			}
		}

		private void DTMenuDaoFile_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openfileDlg = new OpenFileDialog () ;
			try 
			{
				if (openfileDlg.ShowDialog() == DialogResult.OK)
				{			
					
					string strFileName = Path.GetFileName (openfileDlg.FileName) ;
					FileTreeNode nodeNew = (FileTreeNode)this.IsBeNode ((FileTreeNode)(this.DocTree.SelectedNode),
																		strFileName, TreeNodeStyle.File) ;
					if (nodeNew != null)
					{
						switch (MessageBox.Show("文件已存在，要覆盖吗？", "J",
							MessageBoxButtons.YesNo, MessageBoxIcon.Question))
						{
							case DialogResult.Yes :
								break ;
							case DialogResult.No :
								return ;
						}
					}
					else 
					{ 
						// Tree add Note.
						nodeNew = new FileTreeNode (TreeNodeStyle.File, strFileName) ;							
						this.DocTree.SelectedNode.Nodes.Add (nodeNew) ;
					}
					// Copy 
					File.Copy (openfileDlg.FileName, 
						       this.AppPath + @"\" +this.DocTree.SelectedNode.FullPath +  @"\" +  strFileName,							  
							   true) ;
					this.DocTree.SelectedNode = nodeNew ;
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show (exc.ToString ()) ;
			}
		}

		private void DTMenuDaoFolder_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog folderDlg = new FolderBrowserDialog () ;
			folderDlg.ShowNewFolderButton = false ;
			folderDlg.Description = "Please Selete!" ;
			if (folderDlg.ShowDialog() == DialogResult.OK)
			{			
				string strFolder = Path.GetFileName (folderDlg.SelectedPath) ;
				TreeNode oldNode = this.IsBeNode ((FileTreeNode)(this.DocTree.SelectedNode), strFolder, TreeNodeStyle.Folder) ;
				
				if (oldNode != null)
				{
					if ((MessageBox.Show("文件夹已存在，要覆盖吗？", "J",
						MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.No )
						return ;
					oldNode.Remove () ;
				}
				FileTreeNode newNode = null ;
				try 
				{
					newNode = new FileTreeNode (TreeNodeStyle.Folder, strFolder) ;							
					this.DocTree.SelectedNode.Nodes.Add (newNode) ;
					// Copy  
					Directory.CreateDirectory (newNode.FullPath) ;
					this.CopyDirectory (folderDlg.SelectedPath, this.AppPath + "\\" + newNode.FullPath) ;
					// Add new node
					this.AddSubNodeFromIO (newNode) ;
					this.DocTree.SelectedNode = newNode ;
				}
				catch (Exception exc)
				{
					if (newNode != null)    newNode.Remove () ;
					if (oldNode != null)    this.DocTree.SelectedNode.Nodes.Add (oldNode) ;
					MessageBox.Show (exc.ToString ()) ;
				}
			}
		}

		private void DTMenuDel_Click(object sender, System.EventArgs e)
		{
			// 我的 Note 与我的 Text 不允许用户删除
			if (this.DocTree.SelectedNode == null) 
				return ;
			if (this.DocTree.SelectedNode.Equals(DocTree.Nodes[0]) || this.DocTree.SelectedNode.Equals(DocTree.Nodes[1]))
			{
				MessageBox.Show ("请不要试图删除跟结点！谢谢合作！","J-Delete error!",
					              MessageBoxButtons.OK, MessageBoxIcon.Error) ;
				return ;
			}
			
			if (MessageBox.Show(@"真的要从磁盘删除 ..\" + this.DocTree.SelectedNode.FullPath + @" 吗？", "J",
								MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				try 
				{
					// 删除文件
					if (((FileTreeNode)this.DocTree.SelectedNode).NoteStyle == TreeNodeStyle.Folder)
					{
						this.DeleteFolderForMdiDoc (this.AppPath + @"\" + this.DocTree.SelectedNode.FullPath) ;
						Directory.Delete (this.AppPath + @"\" + this.DocTree.SelectedNode.FullPath, true) ;
					}
					else if (((FileTreeNode)this.DocTree.SelectedNode).NoteStyle == TreeNodeStyle.File)
					{
						this.MdiDocManager.DeleteFile (this.AppPath + @"\" + this.DocTree.SelectedNode.FullPath) ;
						File.Delete (this.AppPath + @"\" + this.DocTree.SelectedNode.FullPath) ;
					}
					// 从DocTree 中移除结点
					this.DocTree.SelectedNode.Remove() ;
				}
				catch (Exception exc)
				{
					MessageBox.Show (exc.Message) ;
				}	
			}	
			this.MdiTabs.Invalidate () ;
			this.TextPaneShowNewText () ;
		}
		
		private void DTMenuReName_Click(object sender, System.EventArgs e)
		{
			this.DocTree.SelectedNode.BeginEdit () ;
		}

		private void DocTreeMenu_Popup(object sender, System.EventArgs e)
		{
			if (this.DocTree.SelectedNode == null) 
			{
				this.DocTree.SelectedNode = DocTree.SelectedNode ;
			}
			// 如果依然没有将操作的结点（就是 DocTree.SelectedNode 也为 null) 则返回。
			if (this.DocTree.SelectedNode == null)  return ;

			// 修改弹出式菜单项 Enabled 属性
			if (((FileTreeNode)this.DocTree.SelectedNode).NoteStyle == TreeNodeStyle.File)
			{
				DTMenuOpenInCurWnd.Enabled = true ;
				DTMenuDao.Enabled = false ;
			}
			else
			{
				DTMenuOpenInCurWnd.Enabled = false ;
				DTMenuDao.Enabled = true ;
			}
		}


		private void TimeTab_Tick_L(object sender, System.EventArgs e)
		{
			if (this.MdiTabs.Location.X < 0 && 
				this.MdiTabs.Size.Width + this.MdiTabs.Location.X -20
				< this.MdiTabOwner.Size.Width)
			{
				this.MdiTabs.Location = new Point (this.MdiTabOwner.Size.Width - this.MdiTabs.Size.Width, 0) ;
				this.TimeTab.Enabled = false ;
				this.TimeTab.Tick -= ehTimeTab_Tick_L ;
				return ;
			}
			this.MdiTabs.Location = new Point (this.MdiTabs.Location.X - 20, 0) ;
		}

		private void TimeTab_Tick_R(object sender, System.EventArgs e)
		{
			if (this.MdiTabs.Location.X + 20 > 0 )
			{
				this.MdiTabs.Location = new Point (0, 0) ;
				this.TimeTab.Enabled = false ;
				this.TimeTab.Tick -= ehTimeTab_Tick_R ;
				return ;
			}
			this.MdiTabs.Location = new Point (this.MdiTabs.Location.X + 20, 0) ;
		}

		private void TrundleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			// 将滚动出 窗口
			if (this.GenericPaneState == WndState.Outing)
			{	
				// 如果已经差 15 象素即滚动完毕 则 
				// 设置好窗口位置 
				// 计时器停止 ;
				if (this.GenericPane.Location.X + 15 >= 15)
				{
					this.GenericPane.Location = new Point (20, 0) ;				
					this.DocTree.Focus () ;
					this.TrundleTimer.Enabled = false ;
					this.GenericPane.Invalidate () ;
					return ;
				}
				else this.GenericPane.Location = new Point (GenericPane.Location.X + 15, 0) ;
			}
		
			// 缩回窗口
			else if (this.GenericPaneState == WndState.Ining)
			{
				// 如果已经全部隐藏了窗口
				// 则 计时器停止 停止滚动
				if (20 - this.GenericPane.Location.X  > this.GenericPane.Size.Width)
				{
					this.TrundleTimer.Enabled = false ;
					return ;
				}
				else this.GenericPane.Location = new Point (GenericPane.Location.X - 15, 0) ; 
			}
		}


		private void JMenuNew_Click(object sender, System.EventArgs e)
		{
			if (this.DocTree.SelectedNode == null) 
				return ;
			FileTreeNode parentNode = null ;
			this.NewTextDI ++ ;	
			if (((FileTreeNode)this.DocTree.SelectedNode).NoteStyle == TreeNodeStyle.File)
			{
				parentNode = (FileTreeNode)this.DocTree.SelectedNode.Parent ;
			}
			else 
				parentNode = (FileTreeNode)this.DocTree.SelectedNode ;
			// 这里应该没有问题
			// 因为树结点是排序的并且没有重名的
			// 应该可以得到一个新名字
			foreach (TreeNode tn in parentNode.Nodes)
			{
				if (tn.Text == "New" + NewTextDI.ToString() + ".txt")
					NewTextDI ++ ;
			}
			FileTreeNode newftn = new FileTreeNode (TreeNodeStyle.File, "New" + this.NewTextDI.ToString() 
													+ ".txt") ;
			parentNode.Nodes.Add (newftn) ;
			this.DocTree.SelectedNode = newftn ;
			OpenFileFromDocTree () ;
			newftn.BeginEdit () ;
		}

		private void JMenuOpen_Click(object sender, System.EventArgs e)
		{
			this.MdiDocManager.TextTakeIn (this.TextEditPane.Lines) ;
			OpenFileDialog ofDlg = new OpenFileDialog () ;
			ofDlg.Filter = "文本文件 (*.txt;*.ini)|*.txt;*.ini|所有文件 (*.*)|*.*" ;
			if(ofDlg.ShowDialog() == DialogResult.OK)
			{
				MdiDocManager.OpenFile (ofDlg.FileName) ;
				this.TextPaneShowNewText () ;
				this.MdiTabs.Invalidate () ;	
			}
			ofDlg.Dispose () ;
		}

		private void JMenuExit_Click(object sender, System.EventArgs e)
		{
			this.Close () ;
		}

		private void JMenuSaveAll_Click(object sender, System.EventArgs e)
		{
			MdiDocManager.TextTakeIn (this.TextEditPane.Lines) ;
			this.MdiDocManager.SaveAll () ;
			this.MdiTabs.Invalidate () ;
		}

		private void JMenuHelpAbout_Click(object sender, System.EventArgs e)
		{
			FormAbout fa = new FormAbout ();
			fa.ShowDialog (this) ;
			fa.Dispose () ;
		}

		private void JMenuFColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog cDlg = new ColorDialog () ;
			if (cDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )
			{
				this.TextEditPane.TextChanged -= this.ehTextEditPane_TextChanged ;
				this.TextEditPane.ForeColor= cDlg.Color ;
				this.DocTree.ForeColor = cDlg.Color ;
				this.TextEditPane.TextChanged += this.ehTextEditPane_TextChanged ;
			}
			cDlg.Dispose () ;
		}

		private void JMenuBColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog cDlg = new ColorDialog () ;
			if (cDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )
			{
				this.TextEditPane.BackColor = cDlg.Color ;
			}
			this.MdiContainer.Invalidate () ;
			cDlg.Dispose () ;
		}

		private void JMenuFont_Click(object sender, System.EventArgs e)
		{
			FontDialog fDlg = new FontDialog() ;
			fDlg.Font = this.TextEditPane.Font ;
			if (fDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )		
			{
				this.TextEditPane.TextChanged -= this.ehTextEditPane_TextChanged ;
				this.TextEditPane.Font = fDlg.Font ;
				this.TextEditPane.TextChanged += this.ehTextEditPane_TextChanged ;
			}
			fDlg.Dispose () ;
		}

		private void JMenuWrap_Click(object sender, System.EventArgs e)
		{
			this.JMenuWrap.Checked = !JMenuWrap.Checked ;
			this.TextEditPane.WordWrap = JMenuWrap.Checked ;
		}


		private void TabsMenuSave_Click(object sender, System.EventArgs e)
		{

			MdiDocManager.TextTakeIn (this.TextEditPane.Lines) ;
			this.MdiDocManager.SaveCurTab () ;
			this.MdiTabs.Invalidate () ;
		}

		private void TabsMenuClose_Click(object sender, System.EventArgs e)
		{
			if (MdiDocManager.IsEmpty ())
				return ;
			MdiDocManager.TextTakeIn (this.TextEditPane.Lines) ;
		 	ArrayList Files = MdiDocManager.GetCurFilesNotSave () ;
			// 这里不完善
			if (Files == null) 
				return ;
			if (Files.Count != 0)
			{
				FormSave formS = new FormSave () ;
				int i = 0 ;
				foreach (TextInFile tf in Files)
				{
					formS.LBFlieName.Items.Add (Path.GetFileName (tf.FileName)) ;
					formS.LBFlieName.SelectedIndex = i ++ ;
				}
				switch (formS.ShowDialog (this))
				{
					case DialogResult.Yes : 
						for (i = 0 ; i < formS.LBFlieName.SelectedIndices.Count  ; i ++)
						{
							((TextInFile)Files[formS.LBFlieName.SelectedIndices[i]]).Save () ;
						}
						this.MdiDocManager.CloseCurTab () ;
						this.TextPaneShowNewText () ;
						this.MdiTabs.Invalidate () ;
						break ;
					case DialogResult.No :
						this.MdiDocManager.CloseCurTab () ;
						this.TextPaneShowNewText () ; 
						this.MdiTabs.Invalidate () ;
						break ;
					case DialogResult.Cancel :
						break ;
				}
				formS.Dispose () ;
			}
			else 
			{
				this.MdiDocManager.CloseCurTab () ;
				this.TextPaneShowNewText () ;
				this.MdiTabs.Invalidate () ;
			}

		}


		private void TPaneMenu_Popup(object sender, System.EventArgs e)
		{
			if (this.TextEditPane.CanUndo) 
				this.TPaneMenuUnDo.Enabled = true ;
			else	
				this.TPaneMenuUnDo.Enabled = false ;
			if (this.TextEditPane.CanRedo) 
				this.TPaneMenuReDo.Enabled = true ;
			else     
				this.TPaneMenuReDo.Enabled = false ;
			if (this.TextEditPane.CanPaste (DataFormats.GetFormat(DataFormats.Text)))
				this.TPaneMenuPaste.Enabled = true ;
			else
				this.TPaneMenuPaste.Enabled = false ;

			if (this.TextEditPane.SelectedText.Length != 0) 
			{
				this.TPaneMenuCut.Enabled = true ;
				this.TPaneMenuCopy.Enabled = true ;
			}
			else
			{
				this.TPaneMenuCut.Enabled = false ;
				this.TPaneMenuCopy.Enabled = false ;
			}
		}

		private void TPaneMenuUnDo_Click(object sender, System.EventArgs e)
		{
			this.TextEditPane.Undo () ;
		}

		private void TPaneMenuReDo_Click(object sender, System.EventArgs e)
		{
			this.TextEditPane.Redo () ;
		}

		private void TPaneMenuPaste_Click(object sender, System.EventArgs e)
		{
			this.TextEditPane.Paste () ;
		}

		private void TPaneMenuCut_Click(object sender, System.EventArgs e)
		{
			this.TextEditPane.Cut () ;
		}

		private void TPaneMenuSelectA_Click(object sender, System.EventArgs e)
		{
			this.TextEditPane.SelectAll () ;
		}

		private void TPaneMenuCopy_Click(object sender, System.EventArgs e)
		{
			this.TextEditPane.Copy () ;
		}


		#region Private Method	
		// The Form private Funtion :

		// Judge Whether the Mouse on nail or X.
		// Return the const "WndMouseState.MouseOnX" , "WndMouseState.MouseOnNail" 
		// or "WndMouseState.None"
		private WndMouseState MouseIsOnNailOrX (int iPosX, int iPosY)
		{
			if (iPosY > 3 && iPosY < 18 )
			{
				if (iPosX > this.GenericPane .Size.Width  - 45 &&
					iPosX < this.GenericPane .Size.Width  - 25 )
				{
					return WndMouseState.MouseOnNail ;
				}
				else if (iPosX > this.GenericPane .Size.Width  - 25 &&
					iPosX < this.GenericPane .Size.Width  - 5)
				{
					return WndMouseState.MouseOnX ; 
				}
				return WndMouseState.None ;
			}
			return WndMouseState.None ;
		}

		//
		// Judge the Wnd "MdiContainer" 's Mouse On Where
		// It like the function "WndMouseState MouseIsOnNailOrX (int iPosX, int iPosY)" 
		private WndMouseState MouseIsOnLeftOrRightOrX (int iPosX, int iPosY)
		{
			if (iPosY > 2 && iPosY < 18 )
			{
				if (iPosX > this.MdiContainer.Size.Width  - 48 &&
					iPosX < this.MdiContainer.Size.Width  - 34 )
				{
					return WndMouseState.MouseOnLeftBtn ;
				}
				else if (iPosX > this.MdiContainer.Size.Width  - 34 &&
					iPosX < this.MdiContainer.Size.Width  - 20)
				{
					return WndMouseState.MouseOnRightBtn ; 
				}
				else if (iPosX > this.MdiContainer.Size.Width  - 20 &&
					iPosX < this.MdiContainer.Size.Width  - 3)
				{
					return WndMouseState.MouseOnX ;
				}

				return WndMouseState.None ;
			}
			return WndMouseState.None ;
		}

		// TreeView loads note
		private void TreeViewLoadNote ()
		{
			FileTreeNode NoteBoot = new FileTreeNode (TreeNodeStyle.Folder, @"我的 NOTE") ;
			this.DocTree.Nodes.Add (NoteBoot) ;
			FileTreeNode TextBoot = new FileTreeNode (TreeNodeStyle.Folder, @"我的 TEXT") ;
			this.DocTree.Nodes.Add (TextBoot) ;

			

			try 
			{
				AddSubNodeFromIO (this.DocTree.Nodes[0]) ;
				AddSubNodeFromIO (this.DocTree.Nodes[0].NextNode) ;

			}
			catch (Exception e)
			{
				MessageBox.Show (e.Message) ;
			}

			// 在 NOET 根的第一层子结点中寻找 "今年"
			bool fBeNode = false ;
			TreeNode TreeNodeYear = null ;
			TreeNode TreeNodeMonth = null ;
			TreeNode TreeNoteDay = null ;
			foreach (TreeNode note in this.DocTree.Nodes[0].Nodes)
			{
				if (note.Text == DateTime.Now.Year.ToString() )
				{
					fBeNode = true ;
					TreeNodeYear = note ;
					break ;
				}
			}
			// 没找到 "今年" 则创建 年月日 结点
			if (!fBeNode)
			{
				// Add Year note
				Directory.CreateDirectory (this.AppPath + 
										   @"\我的 NOTE\" + 
										   DateTime.Now.Year.ToString()) ;
				FileTreeNode noteYear = new FileTreeNode (TreeNodeStyle.Folder,  DateTime.Now.Year.ToString()) ;
				DocTree.Nodes[0].Nodes.Add (noteYear) ;

				// Add Month note
				Directory.CreateDirectory (this.AppPath + @"\" +
										   noteYear.FullPath + @"\" + 
										   DateTime.Now.Month.ToString()) ;
				FileTreeNode noteMonth = new FileTreeNode (TreeNodeStyle.Folder, DateTime.Now.Month.ToString()) ;
				noteYear.Nodes.Add (noteMonth) ;

				// Add Day note
				TreeNoteDay = new FileTreeNode (TreeNodeStyle.File, 
												DateTime.Now.Day.ToString() + @".txt") ;
				noteMonth.Nodes.Add (TreeNoteDay) ;
			}
			else 
			{
				// Find MonthNote
				fBeNode = false ;
				foreach (TreeNode note in TreeNodeYear.Nodes)
				{
					if (note.Text == DateTime.Now.Month.ToString() )
					{
						fBeNode = true ;
						TreeNodeMonth = note ;
						break ;
					}
				}

				if (!fBeNode)
				{
					// Add Month note
					Directory.CreateDirectory (this.AppPath + @"\" +
						TreeNodeYear.FullPath + @"\" + 
						DateTime.Now.Month.ToString()) ;
					FileTreeNode noteMonth = new FileTreeNode (TreeNodeStyle.Folder, DateTime.Now.Month.ToString()) ;
					TreeNodeYear.Nodes.Add (noteMonth) ;

					// Add Day note
					TreeNoteDay = new FileTreeNode (TreeNodeStyle.File, 
													DateTime.Now.Day.ToString() + @".txt") ;
					noteMonth.Nodes.Add (TreeNoteDay) ;
				}
				else 
				{
					// Find DayNote
					fBeNode = false ;
					foreach (TreeNode node in TreeNodeMonth.Nodes)
					{
						if (node.Text == DateTime.Now.Day.ToString()+@".txt" )
						{
							fBeNode = true ;
							TreeNoteDay = node ;
							break ;
						}
					}

					if (!fBeNode)
					{
						// Add Day note
						TreeNoteDay = new FileTreeNode (TreeNodeStyle.File, 
							                            DateTime.Now.Day.ToString() + @".txt") ;
						TreeNodeMonth.Nodes.Add (TreeNoteDay) ;
					}
				}
			}
			this.DocTree.SelectedNode = TreeNoteDay ;
		}

		// 递归的深度优先 搜索文件夹或者文件 添加到树中
		private void AddSubNodeFromIO  (TreeNode note)
		{
			if (((FileTreeNode)note).NoteStyle == TreeNodeStyle.File) 
				return ;
			DirectoryInfo dir = new DirectoryInfo (this.AppPath + @"\" + note.FullPath ) ;
			
			// 递归获得文件夹结点
			DirectoryInfo[] dirs = dir.GetDirectories() ;
			foreach (DirectoryInfo diNext in dirs) 
			{
				FileTreeNode subnote = new FileTreeNode (TreeNodeStyle.Folder, diNext.Name) ;
				note.Nodes.Add (subnote) ;
				AddSubNodeFromIO  (subnote) ;
			}
			// 获得所有文件结点
			FileInfo[] files = dir.GetFiles () ;
			foreach (FileInfo fileNext in files)
			{
				FileTreeNode subnote = new FileTreeNode (TreeNodeStyle.File, fileNext.Name) ;
				note.Nodes.Add (subnote) ;
			}
		}

		// 判断 parentNode 中是否有 给定名为strName 类型为noteStyle的结点
		// 如果有返回 该结点 否则返回 null.
		private TreeNode IsBeNode (FileTreeNode parentNode, string strName, TreeNodeStyle noteStyle) 
		{
			foreach (FileTreeNode tNode in parentNode.Nodes)
			{
				if (tNode.NoteStyle == noteStyle && tNode.Text == strName)
					return tNode ;
			}
			return null ;
		}
		
		// 复制文件夹中所有 文件/文件夹 到目标目录
		private void CopyDirectory (string sourceDirName, string destDirName) 
		{
			string[] strFileName = Directory.GetFiles (sourceDirName) ;
			foreach (string strName in strFileName)
			{
				File.Copy (strName, destDirName + "\\" + Path.GetFileName (strName), true) ;
			}
			string[] strFolderName = Directory.GetDirectories (sourceDirName) ;
			foreach (string strName in strFolderName)
			{
				Directory.CreateDirectory (destDirName + "\\" + Path.GetFileName (strName)) ;
				CopyDirectory (strName, destDirName + "\\" + Path.GetFileName (strName)) ;
			}
		}
			

		private void TextPaneShowNewText()
		{	
			if (MdiDocManager.IsEmpty ())			
				return ;
			// 新文档打开 此时 RichTextBox 的 TextChanged 事件不被响应
			this.TextEditPane.TextChanged -= this.ehTextEditPane_TextChanged ;

			this.TextEditPane.Clear () ;			
			string[] str = new string[MdiDocManager.CurTabLinesCnt] ; 
			this.MdiDocManager.TextOutTo(ref str) ;
			this.TextEditPane.Lines = str ;
			this.TextEditPane.SelectionStart = this.TextEditPane.TextLength ;
			this.TextEditPane.Focus() ;

			// 恢复TextChanged 事件的响应
			this.TextEditPane.TextChanged += this.ehTextEditPane_TextChanged ;

		}


		private TabsState GetTabsState()
		{
			TabsState rtn = TabsState.None ;
			if (MdiTabs.Location.X < 0)
				rtn = rtn | TabsState.OutLeft ;
			if (MdiTabs.Size.Width + MdiTabs.Location.X > MdiTabOwner.Size.Width)
				rtn = rtn | TabsState.OutRight ;
			return rtn ;
		}

		
		private void OpenFileFromDocTree ()
		{
			this.MdiDocManager.TextTakeIn (this.TextEditPane.Lines) ;
			this.MdiDocManager.OpenFile (this.AppPath + "\\" + this.DocTree.SelectedNode.FullPath) ;
			this.TextPaneShowNewText () ;
			this.MdiTabs.Invalidate () ;	
		}
		

		private void ReNameFile (string oldpath, string newpath) 
		{
			if (this.MdiDocManager.ReNameFile(oldpath, newpath))
				return ;
			File.Move (oldpath, newpath) ;
		}

		private void ReNameFolder (string oldpath, string newpath) 
		{ 	
			Directory.Move (oldpath, newpath) ;
			MdiDocManager.ForFolderNameChanged (oldpath, newpath) ;
		}
		private void DeleteFolderForMdiDoc (string path)
		{
			string[] sFileName = Directory.GetFiles (path) ;
			foreach (string sf in sFileName)
			{
				MdiDocManager.DeleteFile (sf) ;
			}
			string[] sFolderName = Directory.GetDirectories (path) ;
			foreach (string sd in sFolderName)
			{
				DeleteFolderForMdiDoc (sd) ;
			}
		}

		private void SerializeTexePaneAtt ()
		{
			// font
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(this.AppPath+"\\bin\\"+"font.bin", FileMode.Create, 
										   FileAccess.Write, FileShare.None);
			formatter.Serialize(stream, this.TextEditPane.Font);
			stream.Close () ;
			// forecolor
			stream = new FileStream(this.AppPath+"\\bin\\"+"forecolor.bin", FileMode.Create, 
									FileAccess.Write, FileShare.None);
			formatter.Serialize(stream, this.TextEditPane.ForeColor);
			stream.Close ();
			// backcolor
			stream = new FileStream(this.AppPath+"\\bin\\"+"backcolor.bin", FileMode.Create, 
				FileAccess.Write, FileShare.None);
			formatter.Serialize(stream, this.TextEditPane.BackColor);
			stream.Close ();
		}
		private void DeserializeTexePaneAtt ()
		{
			IFormatter formatter = null;
			Stream stream = null ;
			try 
			{

				formatter = new BinaryFormatter();
				stream = new FileStream(this.AppPath+"\\bin\\"+"font.bin", FileMode.Open , 
					FileAccess.Read, FileShare.None);
				this.TextEditPane.Font = (Font)formatter.Deserialize(stream);
			}
			catch{} // 忽略
			finally
			{
				if (stream != null) 
					stream.Close () ;
			}
			try {
				// forecolor
				stream = new FileStream(this.AppPath+"\\bin\\"+"forecolor.bin", FileMode.Open, 
					FileAccess.Read, FileShare.None);
				this.TextEditPane.ForeColor = Color.FromArgb 
					(((Color)formatter.Deserialize(stream)).ToArgb()) ;
			}
			catch{} // 忽略
			finally
			{
				if (stream != null) 
					stream.Close () ;
			}
			try
			{
				// backcolor
				stream = new FileStream(this.AppPath+"\\bin\\"+"backcolor.bin", FileMode.Open, 
					FileAccess.Read, FileShare.None);
				this.TextEditPane.BackColor =  Color.FromArgb 
					(((Color)formatter.Deserialize(stream)).ToArgb()) ;
			}
			catch{} // 忽略
			finally
			{
				if (stream != null) 
					stream.Close () ;
			}
		}

		#endregion

	



	}// Class FormJ
}// namespace CS_Note1
