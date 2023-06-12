using System.Collections.Specialized;
using System.Drawing;
using System.Reflection;
using System.Xml.Serialization;
using static Program;

public class Program
{
    public static async void tempAsync()
    {
        Console.WriteLine("start");

        var t = Task.Run(() =>
        {
            for (var i = 0; i < 10; ++i)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"async {i}");
            }
        });

        for (var i = 0; i < 10; ++i)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"synchronous {i}");
        }

        await t;

        Console.WriteLine("end");
    }

    public static class StaticFoo
    {
        public static string value = "aaa";
        public static string value2 = "bbb";

        public static string Value { get; set; } = "bbb";
    }

    public static void inner()
    {
        for (int i = 0; i < 5; ++i)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"inner {i}");
        }
    }

    public static async Task asyncCaller()
    {
        for (var i = 0; i < 5; ++i)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"before asyncCaller {i}");
        }

        await Task.Run(inner);

        for (var i = 0; i < 5; ++i)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"middle asyncCaller {i}");
        }

        Console.WriteLine($"end asyncCaller");
    }

    public static async Task nestedAsyncCallerAsync()
    {
        for (var i = 0; i < 5; ++i)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"before nestedAsyncCaller {i}");
        }

        await asyncCaller();

        for (var i = 0; i < 5; ++i)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"middle nestedAsyncCaller {i}");
        }

        Console.WriteLine("nestedAsyncCaller end");
    }

    public static void testException()
    {
        try
        {
            var a = new int[3];

            var b = a[-1];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine("boo");
    }

    public class Animal
    {
        public void bark()
        {
            Console.WriteLine("fff");
        }
    }

    public class Dog : Animal
    {
        public void bark()
        {
            Console.WriteLine("aww");
        }

        public void eat()
        {
            Console.WriteLine("mmmmmm");
        }
    }

    public static int calc()
    {
        var i = 0;

        Thread.Sleep(500);
        for (; i < 10_000; ++i)
        { }

        return i;
    }

    public static async Task<int[]> testStaticAsyncFunc()
    {
        var taskContainer = new List<Task<int>>();
        taskContainer.Add(Task.Run(calc));
        taskContainer.Add(Task.Run(calc));

        var returnTask = await Task.WhenAll(taskContainer);

        foreach (var task in taskContainer)
        {
            Console.WriteLine(task.Result);
        }

        return returnTask;
    }

    public class Person
    {
        public string name;
        public int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public Person()
        { }

        public override string ToString()
        {
            return $"name: {name} age: {age}";
        }
    }

    public class Employee : Person
    {
        public int salary;

        public Employee(string name, int age, int salary) : base(name, age)
        {
            this.salary = salary;
        }

        public Employee()
        { }

        public string show()
        {
            return base.ToString() + $" salary: {salary}";
        }
    }

    public class Phone
    {
        public int number;

        public Phone(int number)
        {
            this.number = number;
        }

        public Phone()
        { }

        public override string ToString()
        {
            return $"number: {number}";
        }
    }

    [XmlInclude(typeof(Person)), XmlInclude(typeof(Employee)), XmlInclude(typeof(Phone))]
    public class Box
    {
        public List<object> container;
    }

    public static void Main(string[] args)
    {
        var container = new List<object>();
        container.Add(new Person("a", 12));
        container.Add(new Phone(3));
        container.Add(new Employee("san", 144, 11));
        container.Add(new Person("b", 13));
        container.Add(new Phone(14));
        container.Add(new Employee("lee", 111, 41));

        var box = new Box();
        box.container = container;

        var serializer = new XmlSerializer(typeof(Box));

        var path = @"C:\Users\wj.lee\Desktop\test2.xml";
        using (var writer = new StreamWriter(path))
        {
            serializer.Serialize(writer, box);
        }

        Box deserializedBox;

        using (var reader = new StreamReader(path))
        {
            deserializedBox = serializer.Deserialize(reader) as Box ?? new Box();
        }

        foreach (var item in deserializedBox.container)
        {
            if (item is Employee)
                Console.WriteLine(((Employee)item).show());
            else if (item is Person)
                Console.WriteLine(item as Person);
            else if (item is Phone)
                Console.WriteLine(item as Phone);
        }

        //var a = 12.12345678f;
        //Console.WriteLine(a);

        //var task = testStaticAsyncFunc();

        //for (var i = 0; i < 10; ++i)
        //{
        //    Console.WriteLine(i);
        //    Thread.Sleep(100);
        //}

        //task.Wait();

        //var a = DateTime.Now;

        //Console.Write(a.ToString("yyyyMMddHHmmss"));

        //Animal animal = new Dog();

        //animal.bark();

        //testException();

        //string filePath = @"C:\MyDir\MySubDir\myfile.ext";
        //string directoryName;
        //int i = 0;

        //while (filePath != null)
        //{
        //    directoryName = Path.GetDirectoryName(filePath);
        //    Console.WriteLine("GetDirectoryName('{0}') returns '{1}'",
        //        filePath, directoryName);
        //    filePath = directoryName;
        //    if (i == 1)
        //    {
        //        filePath = directoryName + @"\";  // this will preserve the previous path
        //    }
        //    i++;
        //}

        //var task = nestedAsyncCallerAsync();

        //for (var i = 0; i < 5; ++i)
        //{
        //    Thread.Sleep(1000);
        //    Console.WriteLine($"main {i}");
        //}

        //task.Wait();

        //tempAsync();
        //var obj = new Program();
        //var pointContainer = obj.createRegularPolygon(new PointDouble(1, 1), 4, 5);

        //foreach (var point in pointContainer)
        //{
        //    Console.WriteLine($"{point.X}, {point.Y}");
        //}

        //Console.WriteLine(8.0 % 3.0);
        //Console.WriteLine(-8.0 % 3.0);
        //Console.WriteLine(8.0 % -3.0);
        //Console.WriteLine(-8.0 % -3.0);
        //Console.WriteLine(5.0 % 16.0);
        //Console.WriteLine(-5.0 % 16.0);
        //Console.WriteLine(5.0 % -16.0);
        //Console.WriteLine(-5.0 % -16.0);

        //uint a = -14;

        //Animal cat = new Cat();
        //Animal dog = new Dog();

        //cat.MakeSound();
        //dog.MakeSound();

        //var foo = F.b;

        //Console.WriteLine(foo.ToString());
    }

    public enum F
    {
        a, b, c
    }

    public struct Foo
    {
        public int value;

        public Foo(int value)
        { }
    }

    public struct Figure
    {
        public PointDouble circumCenter;
        public double circumRadius;
        public int totalSides;
        public double rotationRadian;

        public Figure(PointDouble circumCenter, double circumRadius, int totalSides = 0, double rotationRadian = 0)
        {
            this.circumCenter = circumCenter;
            this.circumRadius = circumRadius;
            this.totalSides = totalSides;
            this.rotationRadian = rotationRadian;
        }
    }

    public struct PointDouble
    {
        public double x;
        public double y;

        public PointDouble(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    //public void DrawFigure(WriteableBitmap bitmap, Figure figure, System.Windows.Media.Color color)
    //{
    //    const double TOLERANCE = 1e-10;
    //    if (bitmap != null && figure.circumRadius > TOLERANCE)
    //    {
    //        if (figure.totalSides == 0)
    //        {
    //            var topLeftX = (int)(figure.circumCenter.x - figure.circumRadius);
    //            var topLeftY = (int)(figure.circumCenter.y - figure.circumRadius);
    //            var bottomRightX = (int)(figure.circumCenter.x + figure.circumRadius);
    //            var bottomRightY = (int)(figure.circumCenter.y + figure.circumRadius);
    //            bitmap.FillEllipse(topLeftX, topLeftY, bottomRightX, bottomRightY, color);
    //        }
    //        else
    //        {
    //            var regularPolygonInt = CreateRegularPolygonInt(figure);
    //            bitmap.FillPolygon(regularPolygonInt, color);
    //        }
    //    }
    //}

    public int[] CreateRegularPolygonInt(Figure figure)
    {
        var regularPolygon = createRegularPolygon(figure.circumCenter, figure.circumRadius, figure.totalSides);
        PointDouble[] rotatedPolygon = regularPolygon;

        const double TOLERANCE = 1e-10;
        var isRotationRadianApproximateZero = Math.Abs(figure.rotationRadian % (2 * Math.PI)) < TOLERANCE;
        if (isRotationRadianApproximateZero == false)
            rotatedPolygon = rotatePointContainer(regularPolygon, figure.circumCenter, figure.rotationRadian);

        return convertToRegularPolygonInt(rotatedPolygon);
    }

    public PointDouble[] createRegularPolygon(PointDouble center, double circumRadius, int totalSides)
    {
        // CircumCircle, InCircle, CircumCenter, InCenter, CircumRadius, InRadius.
        var regularPolygon = new PointDouble[totalSides];
        var centralRadian = 2 * Math.PI / totalSides;

        for (var i = 0; i < totalSides; ++i)
        {
            // [Problem]
            // Reflecteing the coordinate over y = x will cuase problem with WriteableBitmap because the order of points is changed.
            // In shorts. don't use (sinθ, cosθ) instead of (cosθ, sinθ).
            // Translate by center(x,y).
            var x = circumRadius * Math.Sin(i * centralRadian) + center.x;
            var y = circumRadius * Math.Cos(i * centralRadian) + center.y;
            regularPolygon[i] = new PointDouble(x, y);
        }

        return regularPolygon;
    }

    // Rotate clockwise if pixel coordinate is given because pixel coordinate system is top-bottom order.
    public PointDouble[] rotatePointContainer(PointDouble[] pointContainer, PointDouble center, double radian)
    {
        var rotatedPointContainer = new PointDouble[pointContainer.Length];

        for (var i = 0; i < pointContainer.Length; ++i)
        {
            var rotatedPoint = rotatePoint(pointContainer[i], center, radian);
            rotatedPointContainer[i] = rotatedPoint;
        }

        return rotatedPointContainer;
    }

    public PointDouble rotatePoint(PointDouble point, PointDouble center, double radian)
    {
        var translatedX = point.x - center.x;
        var translatedY = point.y - center.y;
        var cos = Math.Cos(radian);
        var sin = Math.Sin(radian);

        var rotatedX = translatedX * cos - translatedY * sin + center.x;
        var rotatedY = translatedX * sin + translatedY * cos + center.y;

        return new(rotatedX, rotatedY);
    }

    public int[] convertToRegularPolygonInt(PointDouble[] regularPolygon)
    {
        var regularPolygonInt = new int[2 * regularPolygon.Length];

        for (var i = 0; i < regularPolygon.Length; ++i)
        {
            var x = (int)Math.Round(regularPolygon[i].x);
            var y = (int)Math.Round(regularPolygon[i].y);
            regularPolygonInt[2 * i] = x;
            regularPolygonInt[2 * i + 1] = y;
        }

        return regularPolygonInt;
    }

    private int realQuotient(double dividend, double divisor)
    {
        var quotient = 0;

        if (divisor != 0)
        {
            if (divisor > 0)
                quotient = (int)Math.Floor(dividend / divisor);
            else
                quotient = (int)Math.Ceiling(dividend / divisor);
        }

        return quotient;
    }

    private double realRemainder(double dividend, double divisor)
        => dividend - realQuotient(dividend, divisor) * divisor;
}