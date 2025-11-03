using System;
using System.Collections.Generic;

namespace Clones;

class Clone
{
	public Stack<string> ReadyProgramms;
	public Stack<string> CanceledPrograms; 

	public Clone()
    {
        ReadyProgramms = new Stack<string>();
        CanceledPrograms = new Stack<string>();
    }

	public Clone MakeClone()
	{
		return new Clone {ReadyProgramms = ReadyProgramms.MakeCopy(), CanceledPrograms = CanceledPrograms.MakeCopy()};
	}

	public void Learn(string Prog)
	{
		ReadyProgramms.Push(Prog);
	}

	public void Rollback()
	{
		CanceledPrograms.Push(ReadyProgramms.Pop());
	}

	public void Relearn()
	{
		ReadyProgramms.Push(CanceledPrograms.Pop());
	}

	public string Check()
	{
		return ReadyProgramms.IsEmpty() ? "basic" : ReadyProgramms.GetTop();
	}
}

public class CloneVersionSystem : ICloneVersionSystem
{
	private	 Dictionary<int, Clone> ClonesDictionary = new Dictionary<int, Clone>();
	public string Execute(string query)
	{
		var command = query.Split();
        string operation = command[0];
        int cloneId = int.Parse(command[1]);
        if (!ClonesDictionary.ContainsKey(cloneId))
            ClonesDictionary[cloneId] = new Clone();
        switch (operation)
        {
            case "learn":
                ClonesDictionary[cloneId].Learn(command[2]);
                break;
            case "rollback":
                ClonesDictionary[cloneId].Rollback();
                break;
            case "relearn":
                ClonesDictionary[cloneId].Relearn();
                break;
            case "clone":
                ClonesDictionary.Add(ClonesDictionary.Count + 1, ClonesDictionary[cloneId].MakeClone());
                break;
            case "check":
                return ClonesDictionary[cloneId].Check();
		}
		return null;
	}
}

public class Stack<T>
{
    private StackItem<T> top;
	private int count;
	public int Count => count;
    public bool IsEmpty()
	{
		return count == 0;
	}

    public void Push(T value)
    {
        top = new StackItem<T>(value, top);
		count++;
    }

    public T Pop()
    {
        if (count == 0) throw new InvalidOperationException();
        var result = top.Value;
        top = top.Top;
		count--;
        return result;
    }

	public Stack<T> MakeCopy()
	{
		return new Stack<T> { top = top, count = count };
	}
	
	public T GetTop()
	{
		return top.Value;
	}
}

public class StackItem<T>
{
    public T Value { get; }
    public StackItem<T> Top { get; }
	public StackItem(T value, StackItem<T> top)
	{
		Value = value;
		Top = top;
	}
}



	