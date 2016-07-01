namespace MathematicalModel
{
    /// <summary>
    /// Varible
    /// </summary>
    public class Varible
    {
        /// <summary>
        /// Degree
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public char Name { get; set; }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var v = obj as Varible;
            return Equals(v);
        }

        protected bool Equals(Varible other)
        {
            if (other == null)
            {
                return false;
            }
            return Degree == other.Degree && Name == other.Name;
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Degree*397) ^ Name.GetHashCode();
            }
        }
    }
}
