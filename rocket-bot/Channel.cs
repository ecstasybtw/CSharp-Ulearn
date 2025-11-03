using System;
using System.Collections.Generic;
using System.Linq;

namespace rocket_bot;

public class Channel<T> where T : class
{
	private readonly List<T> _items = new();
	private readonly object _lock = new();

	public T this[int index]
	{
		get
		{
			lock (_lock)
			{
				if (index >= 0 && index < _items.Count)
					return _items[index];
				else
					return null;
			}
		}
		set
		{
			lock (_lock)
			{
				if (index < 0 || index > _items.Count)
					throw new ArgumentOutOfRangeException();

				if (index == _items.Count)
				{
					_items.Add(value);
				}
				else
				{
					if (!Equals(_items[index], value))
					{
						_items[index] = value;

						int removeFrom = index + 1;
						int removeCount = _items.Count - removeFrom;

						if (removeCount > 0)
							_items.RemoveRange(removeFrom, removeCount);
					}
				}
			}
		}
	}

	/// <summary>
	/// Добавляет item в конец только если lastItem является последним элементом
	/// </summary>
	public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
	{
		lock (_lock)
		{
			if (_items.Count == 0)
			{
				if (knownLastItem == null)
					_items.Add(item);
				return;
			}

			if (Equals(knownLastItem, _items[^1]))
				_items.Add(item);
		}
	}

	/// <summary>
	/// Возвращает последний элемент или null, если такого элемента нет
	/// </summary>
	public T LastItem()
	{
		lock (_lock)
		{
			if (_items.Count != 0)
				return _items[_items.Count - 1];
			else
				return null;
		}
	}

	/// <summary>
	/// Возвращает количество элементов в коллекции
	/// </summary>
	public int Count
	{
		get
		{
			lock (_lock)
			{
				return _items.Count;
			}
		}
	}
}
