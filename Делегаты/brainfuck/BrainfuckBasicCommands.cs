using System;
using System.Collections.Generic;
using System.Linq;
namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });
			vm.RegisterCommand('+', b => { unchecked { b.Memory[b.MemoryPointer]++; } }); //Используем unchecked для игнорирования переполнения
			vm.RegisterCommand('-', b => { unchecked { b.Memory[b.MemoryPointer]--; } });
			vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte)read(); });
            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer < 1) //Если выходим за нижнюю границу типа byte, присваиваем верхнюю 
                    b.MemoryPointer = b.Memory.Length - 1;
                else
                    b.MemoryPointer--;
            });
            vm.RegisterCommand('>', b =>
            {
                if (b.MemoryPointer == b.Memory.Length - 1) //Аналогично для верхней границы
                    b.MemoryPointer = 0;
                else
                    b.MemoryPointer++;
            });
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                byte asciiCode = (byte)ch;
                vm.RegisterCommand(ch, b => { b.Memory[b.MemoryPointer] = asciiCode; });
            }
            for (char ch = 'a'; ch <= 'z'; ch++)
            {
                byte asciiCode = (byte)ch;
                vm.RegisterCommand(ch, b => { b.Memory[b.MemoryPointer] = asciiCode; });
            }
            for (char ch = '0'; ch <= '9'; ch++)
            {
                byte asciiCode = (byte)ch;
                vm.RegisterCommand(ch, b => { b.Memory[b.MemoryPointer] = asciiCode; });
            }
        }
	}
}