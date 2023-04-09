using System.Drawing;

public class Program
{
    public static void Main(string[] args)
    {
        var obj = new Program();
        var pointContainer = obj.CreateRegularPolygonPointContainer(new PointDouble(1, 1), 4, 5);

        foreach (var point in pointContainer)
        {
            Console.WriteLine($"{point.X}, {point.Y}");
        }
    }

    public struct PointDouble
    {
        private double _x;
        private double _y;

        public double X
        {
            readonly get => _x;
            set => _x = value;
        }

        public double Y
        {
            readonly get => _y;
            set => _y = value;
        }

        public PointDouble(double x, double y)
        {
            _x = x;
            _y = y;
        }
    }

    public PointDouble[] CreateRegularPolygonPointContainer(PointDouble center, double circumCircleRadius, int totalSides)
    {
        // CircumCircle, InCircle, CircumCenter, InCenter, CircumRadius, InRadius.
        var pointContainer = new PointDouble[totalSides];
        var centralAngleRadian = 2 * Math.PI / totalSides;

        for (var i = 0; i < totalSides; ++i)
        {
            // [Problem]
            // Reflecteing the coordinate over y = x will cuase problem with WriteableBitmap because the order of points is changed.
            // In shorts. don't use (sinθ, cosθ) instead of (cosθ, sinθ).
            // Translate by center(x,y).
            var x = circumCircleRadius * Math.Sin(i * centralAngleRadian) + center.X;
            var y = circumCircleRadius * Math.Cos(i * centralAngleRadian) + center.Y;
            pointContainer[i] = new PointDouble(x, y);
        }

        return pointContainer;
    }
}