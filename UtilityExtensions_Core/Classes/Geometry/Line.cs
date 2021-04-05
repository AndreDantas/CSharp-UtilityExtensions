using System;

namespace CSharpUtilityExtensions.Classes.Geometry
{
    public struct Line
    {
        private Vector2 _start;
        public Vector2 start { get => _start; private set => _start = value; }

        private Vector2 _end;
        public Vector2 end { get => _end; private set => _end = value; }

        public Line(Vector2 start, Vector2 end) : this()
        {
            this.start = start;
            this.end = end;
        }
    }
}