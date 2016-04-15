using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//http://www.htmldog.com/guides/javascript/advanced/closures/
//A closure is a function that returns a function.
//The function that is returned (the inner function) is created inside the called function(the outer)
//so - due to the scoping rules we’ve seen -
//the inner has access to the variables and arguments of the outer.

//One way to use closures is for creating functions that act differently depending on what was passed to the outer function.
//For example, here’s an add function. 
//The value passed to the outer function is used to add to values passed to the inner.

////////////////////////////////////
//var add = function(a) {
//    return function(b)
//{
//    return a + b;
//};
//};
//var addFive = add(5);
//alert(addFive(10));

//output 15
////////////////////////////////////


namespace deletee
{
    class Program
    {

        public static Func<int, int> Add(int value)
        {
            return new Func<int, int>((item) => { return value + item; });
        }

        static void Main(string[] args)
        {
            Func<int, int> addFive = new Func<int, int>((item) => { return item + 5; });
            Console.WriteLine(addFive(10));
            var addTen = Add(10);
            var r = addTen(10000);
            Console.WriteLine(r);
            var add1 = Add(1);
            Console.WriteLine(add1(9));
        }
    }
}
