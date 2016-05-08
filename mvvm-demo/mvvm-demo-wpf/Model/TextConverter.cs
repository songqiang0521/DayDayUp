using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mvvm_demo_wpf
{
    public class TextConverter
    {
        private readonly Func<string, string> _convertion;


        public TextConverter(Func<string, string> convertion)
        {
            _convertion = convertion;
        }

        public string Convert(string text)
        {
            return _convertion(text);
        }
    }
}
