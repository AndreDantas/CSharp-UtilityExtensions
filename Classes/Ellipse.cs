namespace UtilityExtensions.Classes
{

    public struct Ellipse
    {

        private Vector2 center;
        private float width;
        private float height;
        private Angle rotation;

        public Ellipse(float height, float width) : this()
        {
            Height = height;
            Width = width;
        }

        public Ellipse(float height, float width, Angle rotation) : this()
        {
            Height = height;
            Width = width;
            Rotation = rotation;
        }

        public Ellipse(float height, float width, Angle rotation, Vector2 center) : this()
        {
            Height = height;
            Width = width;
            Rotation = rotation;
            Center = center;
        }

        public float Height { get => height; set => height = value; }
        public float Width { get => width; set => width = value; }
        public Angle Rotation { get => rotation; set => rotation = value; }
        public Vector2 Center { get => center; set => center = value; }
    }
}