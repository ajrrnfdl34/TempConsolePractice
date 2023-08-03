using System.Collections.Specialized;
using System.Data.Common;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Xml.Serialization;
using static Program;
using System.Xml.Linq;

public class Program
{
    //public static async Task<List<T>> SelectAllAsync<T>(CancellationToken cancellationToken, int scanIndex) where T : class, new()
    //{
    //    var query = DTQuery.GetSelectAllQuery<T>(scanIndex);
    //    if (query == null)
    //        return null;

    //    return await SelectDbTableAsync<T>(query, cancellationToken);
    //}

    //public static async Task<List<T>> SelectAllAsync<T>(CancellationToken cancellationToken) where T : class, new()
    //{
    //    var query = DTQuery.GetSelectAllQuery<T>();
    //    if (query == null)
    //        return null;

    //    return await SelectDbTableAsync<T>(query, cancellationToken);
    //}

    //public static async Task<Dictionary<T, T2>> SelectAllAsync<T, T2>(CancellationToken cancellationToken, string keyPropertyName, int scanIndex) where T2 : class, new()
    //{
    //    var query = DTQuery.GetSelectAllQuery<T2>(scanIndex);
    //    if (query == null)
    //        return null;

    //    return await SelectDbTableAsync<T, T2>(query, keyPropertyName, cancellationToken);
    //}

    //private static async Task<List<T>> SelectDbTableAsync<T>(string query, CancellationToken cancellationToken) where T : class, new()
    //{
    //    var queryResult = new List<T>();

    //    try
    //    {
    //        using (DbDataReader dbReader = await ODPDotNetService.Instance.ExecuteReaderAsync(query, CommandType.Text, null, cancellationToken))
    //        {
    //            OracleDataReader reader = dbReader as OracleDataReader;
    //            reader.FetchSize = reader.RowSize * 1000;

    //            while (reader.Read())
    //            {
    //                cancellationToken.ThrowIfCancellationRequested();

    //                var dbRow = SelectDbRow<T>(reader);
    //                if (dbRow == null)
    //                    continue;

    //                queryResult.Add(dbRow);
    //            }

    //            reader.Close();
    //        }

    //        return queryResult;
    //    }
    //    catch (Exception e)
    //    {
    //        return HandleExceptionSelectDbTableAsync(e, queryResult);
    //    }
    //}

    //private static async Task<Dictionary<T, T2>> SelectDbTableAsync<T, T2>(string query, string keyPropertyName, CancellationToken cancellationToken) where T2 : class, new()
    //{
    //    var queryResult = new Dictionary<T, T2>();
    //    var type = typeof(T2);
    //    var keyPropertyInfo = type?.GetProperty(keyPropertyName);
    //    if (keyPropertyInfo == null)
    //        return queryResult;

    //    try
    //    {
    //        using (DbDataReader dbReader = await ODPDotNetService.Instance.ExecuteReaderAsync(query, CommandType.Text, null, cancellationToken))
    //        {
    //            OracleDataReader reader = dbReader as OracleDataReader;
    //            reader.FetchSize = reader.RowSize * 1000;

    //            while (reader.Read())
    //            {
    //                cancellationToken.ThrowIfCancellationRequested();

    //                var dbRow = SelectDbRow<T2>(reader);
    //                if (dbRow == null)
    //                    continue;

    //                var key = (T)keyPropertyInfo.GetValue(dbRow);
    //                if (key == null)
    //                    continue;

    //                queryResult.Add(key, dbRow);
    //            }

    //            reader.Close();
    //        }

    //        return queryResult;
    //    }
    //    catch (Exception e)
    //    {
    //        return HandleExceptionSelectDbTableAsync(e, queryResult);
    //    }
    //}

    //private static T SelectDbRow<T>(OracleDataReader reader) where T : class, new()
    //{
    //    var dbRow = new T();
    //    var type = typeof(T);
    //    var propertyInfoContainer = type?.GetProperties();
    //    if (dbRow == null || propertyInfoContainer == null)
    //        return null;

    //    var dbRowItemOrdianl = 0;
    //    foreach (var propertyInfo in propertyInfoContainer)
    //    {
    //        SelectDbRowItem(dbRow, propertyInfo, reader, dbRowItemOrdianl++);
    //    }

    //    return dbRow;
    //}

    //private static void SelectDbRowItem<T>(T dbRow, PropertyInfo propertyInfo, OracleDataReader reader, int dbRowItemOrdianl) where T : class
    //{
    //    var propertyType = propertyInfo?.PropertyType;
    //    if (propertyType == null)
    //        return;

    //    if (propertyType == typeof(string))
    //    {
    //        propertyInfo.SetValue(dbRow, ODPDotNetUtil.GetRows(reader, dbRowItemOrdianl));
    //    }
    //    else if (propertyType == typeof(double))
    //    {
    //        propertyInfo.SetValue(dbRow, ODPDotNetUtil.GetDouble(reader, dbRowItemOrdianl));
    //    }
    //    else if (propertyType == typeof(float))
    //    {
    //        propertyInfo.SetValue(dbRow, ODPDotNetUtil.GetFloat(reader, dbRowItemOrdianl));
    //    }
    //    else if (propertyType == typeof(int))
    //    {
    //        propertyInfo.SetValue(dbRow, ODPDotNetUtil.GetInt(reader, dbRowItemOrdianl));
    //    }
    //}

    //public static string GetSelectAllQuery<T>(int scanIndex) where T : class
    //{
    //    var query = GetSelectAllQuery<T>();
    //    if (query == null)
    //        return null;

    //    var where = ODPDotNetUtil.AddNewLine($"    AND SCAN_IDX = {scanIndex}");

    //    return query + where;
    //}

    //public static string GetSelectAllQuery<T>() where T : class
    //{
    //    var tableName = GetTableName<T>();
    //    if (tableName == null)
    //        return null;

    //    var type = typeof(T);
    //    var propertyInfoContainer = type?.GetProperties()?.ToList();
    //    var propertyNameContainer = propertyInfoContainer?.ConvertAll(i => (i.Name));

    //    var isNullOrEmpty = (propertyNameContainer?.Count() ?? 0) <= 0;
    //    if (isNullOrEmpty)
    //        return null;

    //    var distinct = " ";
    //    if (type == typeof(InspectionSliceInfo) || type == typeof(LinkInspectionSliceInfo))
    //        distinct = " distinct ";

    //    var select = ODPDotNetUtil.AddNewLine($" SELECT{distinct}{propertyNameContainer.First()}");

    //    for (var i = 1; i < propertyNameContainer.Count(); ++i)
    //        select += $", {propertyNameContainer[i]}";

    //    select += ODPDotNetUtil.AddNewLine("");

    //    var from = ODPDotNetUtil.AddNewLine($" FROM {tableName.ToString()}");

    //    var where = ODPDotNetUtil.AddNewLine(" WHERE 1=1");

    //    var query = select + from + where;
    //    return query;
    //}

    //private static string GetTableName<T>() where T : class
    //{
    //    var type = typeof(T);

    //    if (type == typeof(InspectionMask))
    //        return TableName.INSPECTION.ToString();
    //    else if (type == typeof(InspectionSetup))
    //        return TableName.INSP_SETUP.ToString();
    //    else if (type == typeof(InspectionSliceInfo))
    //        return TableName.INSP_SLICE_INFO.ToString();
    //    else if (type == typeof(LinkInspectionSliceInfo))
    //        return TableName.LNK_INSP_SLICE_INFO.ToString();
    //    else if (type == typeof(InspectionArea))
    //        return TableName.INSP_AREA.ToString();
    //    else if (type == typeof(InspectionSdf))
    //        return TableName.INSP_SDF.ToString();
    //    else if (type == typeof(SummarySize))
    //        return TableName.SMY_SIZE.ToString();
    //    else if (type == typeof(PdmResult))
    //        return TableName.PDM_RESULT.ToString();
    //    else if (type == typeof(StdDefectCode))
    //        return TableName.STD_DEFECT_CODE.ToString();

    //    return null;
    //}

    //private enum TableName
    //{
    //    INSPECTION,
    //    INSP_SETUP,
    //    INSP_SLICE_INFO,
    //    LNK_INSP_SLICE_INFO,
    //    INSP_AREA,
    //    INSP_SDF,
    //    SMY_SIZE,
    //    PDM_RESULT,
    //    STD_DEFECT_CODE
    //}

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

    private int FindNoMatch(List<string> actualContainer, ref List<string> expectedContainer, List<string> noMatchActualContainer, List<string> noMatchExpectedContainer)
    {
        actualContainer.Sort();
        expectedContainer.Sort();
        expectedContainer = expectedContainer.Distinct().ToList();

        var prevOverlapIndex = -1;
        var lastOverlapIndex = -1;
        var totalOverlap = 0;
        for (var i = 0; i < actualContainer.Count; ++i)
        {
            var isOverlap = false;

            for (var j = lastOverlapIndex + 1; j < expectedContainer.Count; ++j)
            {
                // If matched once, no need to search previous index because both are sorted in same order(asceding, descending).
                if (actualContainer[i] == expectedContainer[j])
                {
                    isOverlap = true;
                    prevOverlapIndex = lastOverlapIndex;
                    lastOverlapIndex = j;
                    ++totalOverlap;
                    break;
                }
            }

            if (isOverlap == false)
                noMatchActualContainer.Add(actualContainer[i]);
            else
            {
                noMatchExpectedContainer.AddRange(expectedContainer.GetRange(prevOverlapIndex + 1, lastOverlapIndex - prevOverlapIndex - 1));
            }
        }

        return totalOverlap;
    }

    public class Sentence
    {
        public string word;
        public int dimension;
        public List<Sentence>? sentenceContainer;

        public Sentence(string word, int dimension, List<Sentence>? sentenceContainer)
        {
            this.word = word;
            this.dimension = dimension;
            this.sentenceContainer = sentenceContainer;
        }
    }

    //private T Temp<T>(object item) where T : class, new()
    //{
    //    var item = new T();
    //    var type = typeof(T);
    //    var propertyInfoContainer = type.GetProperties();
    //    foreach (var i in propertyInfoContainer)
    //    {
    //        if (i.PropertyType.IsClass)
    //        {
    //            if (i.PropertyType.IsGenericType) { }
    //            else
    //            {
    //                i.SetValue(item, Temp());
    //            }
    //        }
    //        else
    //            i.SetValue(item, 0);
    //    }

    //    return item;
    //}

    public class FooT
    {
        public int Data;
    }

    public class Temp
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public FooT Foo { get; set; }
    }

    public enum Te
    {
        None = 14,
    }

    private void TestTrackout()
    {
        var actualPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "target3.txt");
        var actualContainer = File.ReadAllLines(actualPath).ToList();
        actualContainer = actualContainer.Distinct().Select(i => i.ToUpper()).ToList();
        var valueLine = "";

        foreach (var line in actualContainer)
        {
            if (line.Contains("DCDATA"))
            {
                var i = line.IndexOf("DCDATA=\"") + "DCDATA=\"".Length;
                valueLine = line.Substring(i);
                valueLine = valueLine.TrimEnd('"');
                break;
            }
        }

        var foundContainer = new Dictionary<string, string>();
        var missingContainer = new Dictionary<string, string>();
        foreach (var word in valueLine.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            var t = word.Split('=', StringSplitOptions.RemoveEmptyEntries);
            if (t.Length == 2)
            {
                if (t[1] == "0" || t[1] == "0.000000")
                    missingContainer.Add(t[0], t[1]);
                else
                    foundContainer.Add(t[0], t[1]);
            }
            else
                missingContainer.Add(t[0], "");
        }

        var testResultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "testResult3.txt");
        using (var fs = new FileStream(testResultPath, FileMode.Create))
        {
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine($"totalFound: {foundContainer.Count}");
                sw.WriteLine($"totalMissing: {missingContainer.Count}");

                sw.WriteLine();
                sw.WriteLine("-----------------------------------------");
                sw.WriteLine($"{nameof(foundContainer)}");
                sw.WriteLine();

                foreach (var item in foundContainer)
                {
                    sw.WriteLine(item);
                }

                sw.WriteLine();
                sw.WriteLine("-----------------------------------------");
                sw.WriteLine($"{nameof(missingContainer)}");
                sw.WriteLine();

                foreach (var item in missingContainer)
                {
                    sw.WriteLine(item);
                }
            }
        }
    }

    public class FooClass
    { }

    // naming: use fuctnion name Update when it needs to notify after setting the value otherwise use function name set.
    public static void Main(string[] args)
    {
        var a = 12.34;

        if (double.TryParse("-23", out a))
            Console.WriteLine(a);
        else
            Console.WriteLine("failed");

        // drawingTargetAreaTopLeft become the new (0,0) of bitmap.
        // So defect's relative coordinate withing bitmap is defect - drawingTargetAreaTopLeft
        // and since drawingTargetArea, itself is shrinked by 1 / zoomScale,
        // any defect within drawingTargetArea needs to be stretched to zoomScale.
        // Top-Bottom, bitmap
        //var drawingTargetAreaHalfWidth = ImageViewOption.ViewWidth / zoomScale / 2.0;
        //var drawingTargetAreaHalfHeight = ImageViewOption.ViewHeight / zoomScale / 2.0;
        //var drawingTargetAreaTopLeftX = ImageViewOption.CenterX - drawingTargetAreaHalfWidth;
        //var drawingTargetAreaTopLeftY = ImageViewOption.CenterY - drawingTargetAreaHalfHeight;
        //var drawingTargetAreaBottomRightX = ImageViewOption.CenterX + drawingTargetAreaHalfWidth;
        //var drawingTargetAreaBottomRightY = ImageViewOption.CenterY + drawingTargetAreaHalfHeight;
        //var circumCenterX = (defect.RealX - drawingTargetAreaTopLeftX) * zoomScale;

        //var a = $"{(int)Te.None}";
        //Console.WriteLine(a);
        //Console.WriteLine(Te.None);
        //var index = 0;
        //var parsedFile = new Sentence("FileA", 3, new List<Sentence>());
        //parsedFile.sentenceContainer.Add(new Sentence("Employee", 2, new List<Sentence>()));
        //parsedFile.sentenceContainer[index].sentenceContainer.Add(new Sentence("Name", 1, new List<Sentence>()));
        //var t = parsedFile.sentenceContainer[index].sentenceContainer.Where(i => i.word == "Name").FirstOrDefault();
        //t.sentenceContainer.Add(new Sentence("Lee", 0, null));
        //t.sentenceContainer.Add(new Sentence("Kim", 0, null));
        //parsedFile.sentenceContainer[index].sentenceContainer.Add(new Sentence("Salary", 1, new List<Sentence>()));
        //var t2 = parsedFile.sentenceContainer[index].sentenceContainer.Where(i => i.word == "Salary").FirstOrDefault();
        //t2.sentenceContainer.Add(new Sentence("14", 0, null));
        //t2.sentenceContainer.Add(new Sentence("20", 0, null));
        //parsedFile.sentenceContainer.Add(new Sentence("Anniversary", 1, new List<Sentence>()));
        //++index;
        //parsedFile.sentenceContainer[index].sentenceContainer.Add(new Sentence("2099-12-12", 0, null));

        //var c = new List<string>();
        //c.Add("123");
        //c.Add("234");

        //var c2 = new List<string>();
        //foreach (var item in c)
        //{
        //    c2.Add(item);
        //}

        //c.Remove("123");
        //c[0] = "555";

        //foreach (var item in c) { Console.WriteLine(item); }
        //foreach (var item in c2) { Console.WriteLine(item); }

        //var container = new List<object>();
        //container.Add(new Person("a", 12));
        //container.Add(new Phone(3));
        //container.Add(new Employee("san", 144, 11));
        //container.Add(new Person("b", 13));
        //container.Add(new Phone(14));
        //container.Add(new Employee("lee", 111, 41));

        //var box = new Box();
        //box.container = container;

        //var serializer = new XmlSerializer(typeof(Box));

        //var path = @"C:\Users\wj.lee\Desktop\test2.xml";
        //using (var writer = new StreamWriter(path))
        //{
        //    serializer.Serialize(writer, box);
        //}

        //Box deserializedBox;

        //using (var reader = new StreamReader(path))
        //{
        //    deserializedBox = serializer.Deserialize(reader) as Box ?? new Box();
        //}

        //foreach (var item in deserializedBox.container)
        //{
        //    if (item is Employee)
        //        Console.WriteLine(((Employee)item).show());
        //    else if (item is Person)
        //        Console.WriteLine(item as Person);
        //    else if (item is Phone)
        //        Console.WriteLine(item as Phone);
        //}

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