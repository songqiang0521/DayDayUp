using System ;
using System.IO ;
using System.Collections ;


namespace CS_Note1
{
	/// <summary>
	/// 
	/// </summary>
	public class TextInBox
	{
		private ArrayList Files = null ;
		private bool _IsChanged = false ;
		public TextInBox()
		{
			Files= new ArrayList (4) ;
		}
		public void AddFile (string strPath)
		{
			TextInFile TextKuai = new TextInFile (strPath) ;;
			Files.Add (TextKuai) ;
		} 
		public bool IsContainFile (string path)
		{
			foreach (TextInFile tf in Files)
			{
				if (tf.FileName == path)
					return true ;
			}
			return false ;
		}

		public void TextOutTo (ref string[] lines)
		{
			int i = 0 ; // For lines.

			foreach (TextInFile tf in this.Files)
			{
				foreach (string str in tf.Lines)
				{
					lines[i ++] = str ;
				}
			}
		}

		public ArrayList GetFilesNotSave() 
		{
			ArrayList arrRtn = new ArrayList () ;
			foreach (TextInFile tf in Files)
			{
				if (tf.IsChanged)
					arrRtn.Add (tf);
			}
			return arrRtn ;
		}

		public void Save ()
		{
			foreach (TextInFile tf in Files)
			{
				tf.Save() ;
			}
			this.IsChanged = false ;
		}

		// 从获得文本
		// 注意 ：此函数是不完善的
		public void TextTakeIn (string[] LinesInBox)
		{
			if (!this.IsChanged)
				return ;
			((TextInFile)Files[0]).TextTakeIn (LinesInBox) ;
		}

		public string Title
		{
			get
			{
				if (Files.Count == 1)
					return
						Path.GetFileName(((TextInFile)Files[0]).FileName) ;
				else if (Files.Count > 1)
					return
						Path.GetFileName(((TextInFile)Files[0]).FileName) +
						@"..." + Path.GetFileName(((TextInFile)Files[Files.Count - 1]).FileName) ;
				else
					return "" ;

			}
		}

		public int LineCount
		{
			get 
			{
				int nRtn = 0 ;
				foreach (TextInFile tf in this.Files)
				{
					nRtn += tf.LineCount ;
				}
				return nRtn ;
			}
		}

		public bool ReNameFile (string oldpath, string newpath) 
		{
			foreach (TextInFile tf in Files)
			{
				if (tf.FileName == oldpath)
				{
					tf.ReName (newpath) ;
					return true ;
				}
			}
			return false ;
		}

		public int FileCnt
		{
			get
			{
				return this.Files.Count ;
			}
		}

		public bool IsChanged
		{
			get
			{
				return this._IsChanged ;
			}
			set
			{
				this._IsChanged = value ;
			}
		}
		public void ForFolderNameChanged (string oldpath, string newpath) 
		{
			foreach (TextInFile tf in Files)
			{
				tf.ReFolderName (oldpath, newpath) ;
			}
		}
		public void DeleteFile (string path)
		{
			for (int i = 0 ; i < Files.Count ; i ++)
			{
				if (((TextInFile)this.Files[i]).FileName == path)
					this.Files.RemoveAt (i) ;
			}
		}
	}
}
