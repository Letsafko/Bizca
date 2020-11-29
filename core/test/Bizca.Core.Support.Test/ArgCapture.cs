namespace Bizca.Core.Support.Test
{
    using NSubstitute;
    using System.Collections.Generic;
    public class ArgCapture<T>
    {
        private readonly List<T> m_arguments = new List<T>();

        public T Capture()
        {
            return Arg.Is<T>(obj => Add(obj));
        }

        public int Count => m_arguments.Count;

        public T this[int index] => m_arguments[index];

        public List<T> Values => new List<T>(m_arguments);

        private bool Add(T obj)
        {
            m_arguments.Add(obj);
            return true;
        }
    }
}
