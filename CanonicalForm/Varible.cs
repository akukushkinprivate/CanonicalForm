namespace CanonicalForm
{
    internal class Varible
    {
        /// <summary>
        /// Degree
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public char Name { get; set; }

        public Varible(int degree, char name)
        {
            Degree = degree;
            Name = name;
        }

        public Varible()
        {
        }
    }
}
