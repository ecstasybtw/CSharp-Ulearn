using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });
			vm.RegisterCommand('+', b => { b.Memory[b.MemoryPointer] = (byte)((b.Memory[b.MemoryPointer] + 1) % 256); });
			vm.RegisterCommand('-', b => { b.Memory[b.MemoryPointer] = (byte)((b.Memory[b.MemoryPointer] - 1 + 256) % 256); });
			vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte)(read() % 256); });
			vm.RegisterCommand('>', b => { b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length; });
			vm.RegisterCommand('<', b => { b.MemoryPointer = (b.MemoryPointer - 1 + b.Memory.Length) % b.Memory.Length; });
			for (char c = 'A'; c <= 'Z'; c++)
			{
				char currentChar = c;
				vm.RegisterCommand(c, b => { b.Memory[b.MemoryPointer] = (byte)(int)currentChar; });
			}
			for (char c = 'a'; c <= 'z'; c++)
			{
				char currentChar = c;
				vm.RegisterCommand(c, b => { b.Memory[b.MemoryPointer] = (byte)(int)currentChar; });
			}
			for (char c = '0'; c <= '9'; c++)
			{
				char currentChar = c;
				vm.RegisterCommand(c, b => { b.Memory[b.MemoryPointer] = (byte)(int)currentChar; });
			}
		}

    }
}
