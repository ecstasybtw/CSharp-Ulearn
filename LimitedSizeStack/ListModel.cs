using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace LimitedSizeStack;

public interface ICommand<TItem>
{
	public void Execute();
	public void Undo();
}

public class Add<TItem> : ICommand<TItem>
{
	public List<TItem> Items { get; }
    public TItem Item { get; }

    public Add(List<TItem> stack, TItem obj)
    {
        Items = stack;
        Item = obj;
    }

    public void Execute()
    {
		Items.Add(Item);
    }

    public void Undo()
    {
        Items.Remove(Item);
    }
}

public class Remove<TItem> : ICommand<TItem>
{
    public List<TItem> Items { get; }
    public TItem Item { get; }
    public int index { get; }

    public Remove(List<TItem> stack, int index)
    {
        Items = stack;
        Item = stack[index];
        this.index = index;
    }

    public void Execute()
    {
        Items.RemoveAt(index);
    }

    public void Undo()
    {
        Items.Insert(index, Item);
    }
}

public class ListModel<TItem>
{
	public LimitedSizeStack<ICommand<TItem>> UndoStack;
	public List<TItem> Items { get; }
	public int UndoLimit;
        
	public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
	{
	}

	public ListModel(List<TItem> items, int undoLimit)
	{
		Items = items;
		UndoLimit = undoLimit;
		UndoStack = new LimitedSizeStack<ICommand<TItem>>(undoLimit);
	}

	public void AddItem(TItem item)
	{
		var add = new Add<TItem> (Items, item);
		add.Execute();
		UndoStack.Push(add);
	}

	public void RemoveItem(int index)
	{
		var remove = new Remove<TItem> (Items, index);
		remove.Execute();
		UndoStack.Push(remove);

	}

	public bool CanUndo()
	{
		return UndoStack.Count != 0;
	}

	public void Undo()
	{
		var command = UndoStack.Pop();
		command.Undo();
	}
}