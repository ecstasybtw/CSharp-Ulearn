using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public delegate Action<IVirtualMachine> action();
		public Dictionary<char, Action<IVirtualMachine>> commandDict {get;}
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			Memory = new byte[memorySize];
			MemoryPointer = Memory[0];
			InstructionPointer = 0;
			commandDict = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			commandDict.Add(symbol, execute);
		}

		public void Run()
		{
			for (var i = InstructionPointer; i < Instructions.Length; i = InstructionPointer)
			{
				var instruction = Instructions[InstructionPointer];
				if (commandDict.ContainsKey(instruction))
				{
					commandDict[instruction](this);
					InstructionPointer++;
				}
				else
				{
					InstructionPointer++;
					continue;
				}
			}
		}
	}
}