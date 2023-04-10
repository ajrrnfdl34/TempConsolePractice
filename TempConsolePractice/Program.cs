using System.Drawing;

public class Program
{
    public static void Main(string[] args)
    {
        var obj = new Program();
        var pointContainer = obj.CreateRegularPolygon(new PointDouble(1, 1), 4, 5);

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

    public PointDouble[] CreateRegularPolygon(PointDouble center, double circumRadius, int totalSides)
    {
        // CircumCircle, InCircle, CircumCenter, InCenter, CircumRadius, InRadius.
        var regularPolygon = new PointDouble[totalSides];
        var centralAngleRadian = 2 * Math.PI / totalSides;

        for (var i = 0; i < totalSides; ++i)
        {
            // [Problem]
            // Reflecteing the coordinate over y = x will cuase problem with WriteableBitmap because the order of points is changed.
            // In shorts. don't use (sinθ, cosθ) instead of (cosθ, sinθ).
            // Translate by center(x,y).
            var x = circumRadius * Math.Sin(i * centralAngleRadian) + center.X;
            var y = circumRadius * Math.Cos(i * centralAngleRadian) + center.Y;
            regularPolygon[i] = new PointDouble(x, y);
        }

        return regularPolygon;
    }

    // Rotate clockwise if pixel coordinate is given because pixel coordinate system is top-bottom order.
    public PointDouble[] RotatePointContainer(PointDouble[] pointContainer, PointDouble center, double angleRadian)
    {
        var rotatedPointContainer = new PointDouble[pointContainer.Length];

        for (var i = 0; i < pointContainer.Length; ++i)
        {
            var rotatedPoint = RotatePoint(pointContainer[i], center, angleRadian);
            rotatedPointContainer[i] = rotatedPoint;
        }

        return rotatedPointContainer;
    }

    public PointDouble RotatePoint(PointDouble point, PointDouble center, double angleRadian)
    {
        var translatedX = point.X - center.X;
        var translatedY = point.Y - center.Y;
        var cos = Math.Cos(angleRadian);
        var sin = Math.Sin(angleRadian);

        var rotatedX = translatedX * cos - translatedY * sin + center.X;
        var rotatedY = translatedX * sin + translatedY * cos + center.Y;

        return new(rotatedX, rotatedY);
    }

    public int[] ConvertToRegularPolygonInt(PointDouble[] regularPolygon)
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
}