namespace CSharpUtilityExtensions.Geometry.Classes
{
    public abstract class Shape2D
    {
        public abstract double Area { get; }
        public abstract Line[] Lines { get; }
    }
}