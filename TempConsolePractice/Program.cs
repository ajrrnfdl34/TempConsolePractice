using System.Drawing;
using static Program;

public class Program
{
    public static void Main(string[] args)
    {
        //var obj = new Program();
        //var pointContainer = obj.createRegularPolygon(new PointDouble(1, 1), 4, 5);

        //foreach (var point in pointContainer)
        //{
        //    Console.WriteLine($"{point.X}, {point.Y}");
        //}

        Console.WriteLine(8.0 % 3.0);
        Console.WriteLine(-8.0 % 3.0);
        Console.WriteLine(8.0 % -3.0);
        Console.WriteLine(-8.0 % -3.0);
        Console.WriteLine(5.0 % 16.0);
        Console.WriteLine(-5.0 % 16.0);
        Console.WriteLine(5.0 % -16.0);
        Console.WriteLine(-5.0 % -16.0);
    }

    public struct PointDouble
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointDouble(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public struct Figure
    {
        private PointDouble mCircumCenter;

        public PointDouble CircumCenter
        {
            readonly get => mCircumCenter;
            set => mCircumCenter = value;
        }

        public double CircumRadius { get; set; }

        public int TotalSides { get; set; }

        public double RotationRadian { get; set; }

        public Figure(PointDouble circumCenter, double circumRadius, int totalSides, double rotationRadian)
        {
            mCircumCenter = new(circumCenter.X, circumCenter.Y);
            CircumRadius = circumRadius;
            TotalSides = totalSides;
            RotationRadian = rotationRadian;
        }
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
            var x = circumRadius * Math.Sin(i * centralRadian) + center.X;
            var y = circumRadius * Math.Cos(i * centralRadian) + center.Y;
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
        var translatedX = point.X - center.X;
        var translatedY = point.Y - center.Y;
        var cos = Math.Cos(radian);
        var sin = Math.Sin(radian);

        var rotatedX = translatedX * cos - translatedY * sin + center.X;
        var rotatedY = translatedX * sin + translatedY * cos + center.Y;

        return new(rotatedX, rotatedY);
    }

    public int[] convertToRegularPolygonInt(PointDouble[] regularPolygon)
    {
        var regularPolygonInt = new int[2 * regularPolygon.Length];

        // [WIP]
        for (var i = 0; i < regularPolygon.Length; ++i)
        {
            var x = (int)Math.Round(regularPolygon[i].X);
            var y = (int)Math.Round(regularPolygon[i].Y);
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