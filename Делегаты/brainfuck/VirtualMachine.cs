using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		private Dictionary<char, Action<IVirtualMachine>> InstructionDict { get; set; } //������� ��� �������� ���������� 

		public VirtualMachine(string program, int memorySize) //�������������� ���� � ������������ 
		{
			Instructions = program;
			Memory = new byte[memorySize];
			MemoryPointer = 0;
			InstructionPointer = 0;
			InstructionDict = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			InstructionDict.Add(symbol, execute); //��������� � ������� �������� ���������� � �� ����
		}

		public void Run()
		{
			while (InstructionPointer < Instructions.Length)
			{
				if (InstructionDict.TryGetValue(Instructions[InstructionPointer], out var instruction)) //���� ����� � ������� ��������������� ����������
					instruction(this); //��������� ��
                InstructionPointer++; //� ����������� ������� �� �����������
            }	
		}
	}
}