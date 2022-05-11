namespace Lab1.Models
{
    public class FunctionComparator : IComparer<Function>
    {
        public int Compare(Function? x, Function? y)
        {
            if (x is null || y is null)
                throw new InvalidOperationException();

            var result = x.GetType().Name.CompareTo(y.GetType().Name);
            return result != 0 
                ? result
                : x.ToString().CompareTo(y.ToString());
        }
    }
}
