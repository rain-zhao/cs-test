using System;
using System.Xml.Serialization;

namespace TestDelegateAndEvent
{

    class TestDelagateAndEvent
    {

        public static void innerStaticSayHello(String name)
        {
            Console.WriteLine($"innerStaticSayHello:hello {name}");
        }

        public void innerSayHello(String name)
        {
            Console.WriteLine($"innerSayHello:hello {name}");
        }

        /*
         * 声明delegate 
         */
        public delegate void SayHelloDelegate(String name);

        /*
         * 声明delegate对象
         */
        public SayHelloDelegate sayHelloDelegate;

        public event SayHelloDelegate sayHeloEvent;

        public void activateSayHelloDelegate(String name)
        {
            sayHelloDelegate?.Invoke(name);
        }

        public void activateSayHelloEvent(String name)
        {
            sayHeloEvent?.Invoke(name);
        }


    }
    class Test
    {
        public static void outerSayHello(String name)
        {
            Console.WriteLine($"outerSayHello:hello {name}");
        }
        static void Main(string[] args)
        {
            TestDelagateAndEvent obj = new TestDelagateAndEvent();

            /*
             * test delagate
             */
            //delegate 能使用 "="
            obj.sayHelloDelegate = obj.innerSayHello;
            obj.sayHelloDelegate += TestDelagateAndEvent.innerStaticSayHello;
            obj.sayHelloDelegate += Test.outerSayHello;
            obj.sayHelloDelegate += (name) =>
            {
                Console.WriteLine($"Lambda:hello {name}");
            };
            //delegate 能作为对象直接调用invoke
            Console.WriteLine("delegate 直接invoke");
            obj.sayHelloDelegate("小明");
            Console.WriteLine("delegate 在对象内部invoke");
            obj.activateSayHelloDelegate("小明");

            /*
             * test event
             */
            //event事件只能使用"+="或"-="
            //test.sayHeloEvent = test.innerSayHello;
            obj.sayHeloEvent += TestDelagateAndEvent.innerStaticSayHello;
            obj.sayHeloEvent += Test.outerSayHello;
            obj.sayHeloEvent += (name) =>
            {
                Console.WriteLine($"Lambda:hello {name}");
            };
            //event 只能在对象内部invoke
            //test.sayHeloEvent("小明");
            Console.WriteLine("event 在对象内部invoke");
            obj.activateSayHelloEvent("小明");



        }
    }
}
