using System;
using System.Collections.Generic;
using System.IO;

namespace TreeTraverser
{
    class TreeTraverser
    {
        //args
        private string _path;
        private bool _showFullPath;
        private bool _showHiden;


        private const string SYMBOL_PIPE = "|   ";
        private const string SYMBOL_SPACE = "    ";
        private const string SYMBOL_FOLDER = "+---";
        private const string SYMBOL_FILE = "|---";


        private Stack<int> _pipeMarkers = new Stack<int>(4);
        private int _depth;
        private int _folderCount;
        private int _fileCount;


        public TreeTraverser(string path, bool showFullPath, bool showHidden)
        {
            _path = path;
            _showFullPath = showFullPath;
            _showHiden = showHidden;
        }

        public void Traverse()
        {
            if (!Directory.Exists(_path))
            {
                throw new DirectoryNotFoundException(_path);
            }

            TraverseFolder(_path);
        }

        /// <summary>
        /// 打印当前行的前半部分
        /// </summary>
        private void PrintPrefix()
        {
            for (int i = 0; i < _depth - 1; i++)
            {
                if (i == 0 || _pipeMarkers.Contains(i))
                {
                    Console.Write(SYMBOL_PIPE);
                }
                else
                {
                    Console.Write(SYMBOL_SPACE);
                }
            }
        }

        private void TraverseFolder(string folderPath)
        {
            _depth++;

            _folderCount++;

            PrintPrefix();

            PrintFolderSymbol();

            PrintFolderName(folderPath);
            string[] dirs = null;
            string[] files = null;
            try
            {
                dirs = Directory.GetDirectories(folderPath);
                files = Directory.GetFiles(folderPath);
            }
            catch (Exception)
            {
            }

            if (dirs == null || files == null)
            {
                _depth--;
                return;
            }

            bool needPipeSymbol = dirs.Length > 1 || files.Length > 1;
            if (needPipeSymbol)
            {
                _pipeMarkers.Push(_depth);
            }

            foreach (var file in files)
            {
                if (!_showHiden)
                {
                    if ((new FileInfo(file).Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue;
                    }
                }
                TraverseFile(file);
            }

            foreach (var dir in dirs)
            {
                if (!_showHiden)
                {
                    if ((new FileInfo(dir).Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue;
                    }
                }
                TraverseFolder(dir);
            }

            if (needPipeSymbol)
            {
                _pipeMarkers.Pop();
            }

            _depth--;
        }

        private void TraverseFile(string file)
        {
            _depth++;

            _fileCount++;

            PrintPrefix();

            PrintFileSymbol();

            PrintFileName(file);

            _depth--;
        }

        private void PrintFileName(string file)
        {
            //显示全路径
            if (_showFullPath)
            {
                Console.WriteLine(file);
            }
            else
            {
                string fileName = GetFileName(file);
                Console.WriteLine(fileName);
            }
        }

        private string GetFileName(string file)
        {
            return Path.GetFileName(file);
        }

        private void PrintFileSymbol()
        {
            Console.Write(SYMBOL_FILE);
        }

        private void PrintFolderName(string folderPath)
        {
            //显示全路径
            if (_showFullPath)
            {
                Console.WriteLine(folderPath);
            }
            else
            {
                string fileName = GetFileName(folderPath);
                Console.WriteLine(fileName);
            }
        }

        private void PrintFolderSymbol()
        {
            Console.Write(SYMBOL_FOLDER);
        }
    }
}
