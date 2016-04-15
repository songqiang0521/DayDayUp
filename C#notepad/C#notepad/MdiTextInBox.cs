using System ;
using System.Collections ;

namespace CS_Note1
{
	/// <summary>
	/// 
	/// </summary>
	public class MdiTextInBox
	{
		// Primer ArrayList
		private ArrayList Tabs = new ArrayList (4) ;
		private int _CurTab =  -1 ;

		public event EventHandler FirstTimeOpenFileMsg ;
		public event EventHandler AllClosedMsg ;

		public MdiTextInBox()
		{
		}

		protected virtual void OnFirstTimeOpenFile (EventArgs e)
		{
			if (FirstTimeOpenFileMsg != null)
			{
				FirstTimeOpenFileMsg (this, e) ;
			}
		}

		protected virtual void OnAllClosed (EventArgs e)
		{
			if (AllClosedMsg != null)
			{
				AllClosedMsg (this, e) ;
			}
		}


		private void LoadFileToNewTab (string strFilePath)
		{
			this.Tabs.Add (new TextInBox ()) ;
			this._CurTab = Tabs.Count - 1 ;
			((TextInBox)Tabs[CurTab]).AddFile (strFilePath) ;
		}
		
		public void OpenFile (string path)
		{
			if (IsEmpty())
			{
				EventArgs e = new EventArgs () ;
				OnFirstTimeOpenFile (e) ;
			}
			else 
			{
				// 遍历 Tabs 查看文件是否已经打开
				for (int i = 0 ; i < Tabs.Count ; i++)
				{
					// 如果已打开 设置当前 TAB 后 返回
					if ( ((TextInBox)Tabs[i]).IsContainFile (path) )
					{
						this._CurTab = i ;
						return ;
					}
				}
			}
			// 的确是未打开的文件 
			this.LoadFileToNewTab (path) ;
			return ;
		}
 
		public string[] GetTabsTitle()
		{
			int len = this.Tabs.Count ;
			string[] tt = new string[len] ;
			for (int i = 0 ; i < len ; i ++)
			{
				tt[i] = ((TextInBox)Tabs[i]).Title ;
				if (((TextInBox)Tabs[i]).IsChanged)		tt[i] += "*" ;	
			}
			return tt ;
		}

		public bool IsEmpty()
		{	
			if (this.Tabs.Count > 0)
				return false ;
			return true ;
		}

		public void TextOutTo (ref string[] lines)
		{
			((TextInBox)this.Tabs[this.CurTab]).TextOutTo (ref lines) ;
		}

		// 从获得文本 到当前 TAB
		public void TextTakeIn (string[] LinesInBox)
		{
			if (this._CurTab < 0)
				return ;
			((TextInBox)this.Tabs[this.CurTab]).TextTakeIn (LinesInBox) ;
		}

		public ArrayList GetCurFilesNotSave ()
		{
			if (CurTab < 0) 
				return null ;
			return 	((TextInBox)this.Tabs[this.CurTab]).GetFilesNotSave() ;
		}

		public ArrayList GetAllFileNoteSave ()
		{
			ArrayList alRtn = new ArrayList () ;
			ArrayList alFiles = null ;
			foreach (TextInBox tb in this.Tabs)
			{
				alFiles = tb.GetFilesNotSave ()  ;
				if (alFiles != null)
					alRtn.AddRange (alFiles) ;
			}
			return alRtn ;
		}

		public void CloseCurTab ()
		{
			if (CurTab < 0) 
				return ;
			this.Tabs.RemoveAt (CurTab) ;
			if (CurTab == Tabs.Count)
				CurTab -- ;
		}

		public void SaveCurTab ()
		{
			((TextInBox)this.Tabs[this.CurTab]).Save() ;
		}

		public void SaveAll ()
		{
			foreach (TextInBox tb in this.Tabs)
			{
				tb.Save () ;
			}
		}

		public bool SetCurTabChangedFlag () 
		{
			if (((TextInBox)this.Tabs[this.CurTab]).IsChanged == true)
				return false ;
			((TextInBox)this.Tabs[this.CurTab]).IsChanged = true  ;
			return true ;
		}
		
		public void CancelCurTabChangedFlag () 
		{
			((TextInBox)this.Tabs[this.CurTab]).IsChanged = false ; ;
		}

		public bool ReNameFile (string oldpath, string newpath) 
		{
			foreach (TextInBox tb in Tabs)
			{
				if (tb.ReNameFile (oldpath, newpath))
					return true ;
			}
			return false ;
		}

		public int CurTab
		{
			get 
			{
				return _CurTab ;
			}
			set
			{
				_CurTab = value ;
				if (_CurTab < 0)
				{
					EventArgs e = new EventArgs () ;
					OnAllClosed (e) ;
				}			
			}
		}

		public int CurTabLinesCnt
		{
			get 
			{
				return ((TextInBox)Tabs[CurTab]).LineCount ;
			}
		}

		public void ForFolderNameChanged (string oldpath, string newpath) 
		{
			foreach (TextInBox tb in Tabs)
			{
				tb.ForFolderNameChanged (oldpath, newpath) ;
			}
		}
		public void DeleteFile (string path)
		{
			for (int i = 0 ; i < Tabs.Count ; i ++)
			{
				((TextInBox)this.Tabs[i]).DeleteFile (path) ;
				if (((TextInBox)this.Tabs[i]).FileCnt == 0)
				{
					Tabs.RemoveAt (i) ;
					if (this.CurTab == Tabs.Count)
						CurTab -- ;
				}
			}
		}


	}
}
