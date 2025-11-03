using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
		public static readonly int OffsetBasis = unchecked((int)2166136261);
    	public static readonly int Prime = 16777619;
		private int hash;
		readonly byte[] bytes;
		private bool isCalculated = false;
		public ReadonlyBytes(params byte[] bytes)
		{
			if (bytes == null)
				throw new ArgumentNullException();
			
			this.bytes = bytes;
		}

		public int Length => bytes.Length;
        public override bool Equals(object obj)
        {
			if (obj == null || obj.GetType() != GetType())
				return false;
            var objBytes = (ReadonlyBytes)obj;
			if (bytes.Length != objBytes.bytes.Length) 
				return false;
			for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] != objBytes.bytes[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
		{
			unchecked
			{
				if (isCalculated)
				{
					return hash;
				}
				foreach (var item in bytes)
				{
				    hash = hash ^ item.GetHashCode();
     				hash = hash * Prime;
				}
				isCalculated = true;
				return hash;
			}
		}

        public override string ToString()
        {
            return "[" + string.Join(", ", bytes) + "]";
        }

        public byte this[int index]
		{
			get
			{
				if (index < 0 || index >= bytes.Length) throw new IndexOutOfRangeException();
				return bytes[index];
			}
		}
        
		public IEnumerator<byte> GetEnumerator()
        {
            foreach (var item in bytes)
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}