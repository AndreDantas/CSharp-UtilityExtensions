namespace UtilityExtensions.Classes
{
    public struct Triangle
    {
        private Vector2 _v3;
        private Vector2 _v1;
        private Vector2 _v2;

        public Vector2 v1 { get => _v1; set => _v1 = value; }
        public Vector2 v2 { get => _v2; set => _v2 = value; }
        public Vector2 v3 { get => _v3; set => _v3 = value; }

        public Triangle(Vector2 v1) : this()
        {
            this.v1 = v1;
        }

        public Triangle(Vector2 v1, Vector2 v2) : this()
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public Triangle(Vector2 v1, Vector2 v2, Vector2 v3) : this()
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }

}