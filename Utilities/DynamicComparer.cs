using System.Collections.Generic;

namespace TodoList.Utilities
{
    internal class DynamicComparer<T> : IComparer<T>
    {
        private string _propertyName;

        public DynamicComparer(string propertyName)
        {
            _propertyName = propertyName;
        }

        public int Compare(T x, T y)
        {
            var property = typeof(T).GetProperty(_propertyName);

            var valueX = property.GetValue(x);
            var valueY = property.GetValue(y);

            return Comparer<object>.Default.Compare(valueX, valueY);
        }
    }
}
