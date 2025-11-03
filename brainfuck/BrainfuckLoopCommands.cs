using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            var openToClose = new Dictionary<int, int>();
            var closeToOpen = new Dictionary<int, int>();
            var stack = new Stack<int>();

            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[')
                    stack.Push(i);
                else if (vm.Instructions[i] == ']')
                {
                    int openIndex = stack.Pop();
                    openToClose[openIndex] = i;
                    closeToOpen[i] = openIndex;
                }
            }

            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = openToClose[b.InstructionPointer];
            });

            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = closeToOpen[b.InstructionPointer];
            });
        }
    }
}
