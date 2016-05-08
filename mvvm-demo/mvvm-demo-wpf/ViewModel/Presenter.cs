
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace mvvm_demo_wpf
{
    public class Presenter : ObservableObject
    {
        #region private fileds
        private TextConverter _textConverter;
        private string _someText;
        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();
        private ICommand _convertTextCommand;
        #endregion

        #region ctor

        public Presenter()
        {
            _textConverter = new TextConverter(s => s.ToUpper());
            _convertTextCommand = new DelegateCommand(ConvertText);
        }
        public Presenter(TextConverter converter)
        {
            _textConverter = converter;
            _convertTextCommand = new DelegateCommand(ConvertText);
        }
        #endregion

        #region public expose
        public string SomeText
        {
            get { return _someText; }
            set
            {
                if (_someText != value)
                {
                    RaisePropertyChangedEvent("SomeText");
                    _someText = value;
                }

            }

        }

        public IEnumerable<string> History
        {
            get { return _history; }
        }

        public ICommand ConvertTextCommand
        {
            get
            {
                return _convertTextCommand;
            }
        }
        #endregion


        #region private methods
        private void ConvertText()
        {
            AddToHistory(_textConverter.Convert(SomeText));
            SomeText = String.Empty;
        }

        private void AddToHistory(string item)
        {
            //if (!_history.Contains(item))
                _history.Add(item);
        }
        #endregion

    }
}
