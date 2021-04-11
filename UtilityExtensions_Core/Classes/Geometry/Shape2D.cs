namespace CSharpUtilityExtensions.Classes.Geometry
{
    public abstract class Shape2D
    {
        public abstract double Area { get; }
        public abstract Line[] Lines { get; }
        public abstract Vector2[] Points { get; }
    }
}