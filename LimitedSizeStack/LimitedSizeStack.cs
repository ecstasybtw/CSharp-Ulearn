using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{
	public LinkedList<T> stack;
	public int limit;
	public LimitedSizeStack(int undoLimit)
	{
		
		limit = undoLimit;
		stack = new LinkedList<T>();
	}

	public void Push(T item)
	{
		if (limit == 0) return;
		if (stack.Count == limit)
			stack.RemoveFirst();
		stack.AddLast(item);
	}

	public T Pop()
	{
		if (stack.Count == 0)
			throw new Exception("stack is empty");
		T lastItem = stack.Last.Value;
		stack.RemoveLast();
		return lastItem;
	}

	public int Count => stack.Count;
}