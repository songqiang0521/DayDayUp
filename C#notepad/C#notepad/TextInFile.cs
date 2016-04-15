using System ;
using System.IO ;
using System.Collections ;
using System.Windows.Forms ;
using System.Globalization ;
using System.Text ;

namespace CS_Note1
{
	/// <summary>
	/// 
	/// </summary>
	public class TextInFile
	{
		private bool _IsChanged = false ;
		private string _FileName = null ;
		private ArrayList _Lines = new ArrayList (8) ;
		public TextInFile()
		{
			_FileName = "" ;
		}
		public TextInFile (string path)
		{
			this._FileName = path ;
			LoadFile (_FileName) ;
		}
		private void LoadFile (string path)
		{
			
			if (File.Exists(path))
			{	
				
				StreamReader sr = new StreamReader (path, Encoding.Default) ; 
				string strNextLine = null ;
				try 
				{
					while ((strNextLine = sr.ReadLine ()) != null)
					{
						_Lines.Add (strNextLine) ;
					}
				}
				catch (Exception exc)
				{
					MessageBox.Show (exc.ToString(), "error - LoadFile()") ;
				}
				finally
				{
					sr.Close () ;
				}
			}
			else
			{
				FileStream fs = null ;
				try 
				{
					fs = File.Create (path) ;
				}
				catch (DirectoryNotFoundException dire)
				{
					MessageBox.Show (dire.Message + "\n文件可能在其他程序中被修改，强烈建议保存其他文件后重起JNote", 
						"CreateFile Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) ;
				}
				finally
				{
					if (fs != null) 
						fs.Close () ;
				}
			}
			
		}
    
		public void TextTakeIn (string[] LinesInBox)
		{
			this._Lines.Clear ();
			foreach (string str in LinesInBox) 
			{
				this._Lines.Add (str) ;
			}
			this._IsChanged = true ;
		}

		public void Save() 
		{
			StreamWriter sw = null ;
			try 
			{
				sw = new StreamWriter (this._FileName, false, Encoding.Default) ;
				foreach (string line in this._Lines)
				{
					sw.WriteLine (line) ;
				}
			}
			catch (DirectoryNotFoundException dire)
			{
				MessageBox.Show (dire.Message + "\n文件可能在其他程序中被修改，请重起JNote", 
					"SaveFile Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) ;
			}
			finally
			{
				if (sw != null) 
					sw.Close () ;
			}
			this._IsChanged = false ; 
		}

		public void ReName (string newpath) 
		{
			string sOld = _FileName ;
			try 
			{
				File.Move (_FileName, newpath) ;
				_FileName = newpath ;
			}
			catch (IOException ioe)
			{
				_FileName = sOld ;
				throw ioe ;
			}
		}

		public void ReFolderName (string oldpath, string newpath) 
		{
			if (this._FileName.StartsWith (oldpath))
			{
				this._FileName = newpath + _FileName.Substring(oldpath.Length) ; ;
				MessageBox .Show (_FileName) ;
			}
		}
		// Attribute
		// 完整的文件路径
		public string FileName
		{
			get
			{
				return _FileName ;
			}
		}

		public object[] Lines 
		{
			get 
			{
				return this._Lines.ToArray() ;
			}
		}

		public int LineCount
		{
			get
			{
				return this.Lines.Length ;
			}
		}
		
		public bool IsChanged
		{
			get
			{
				return this._IsChanged ;
			}
		}

	
	}

}
