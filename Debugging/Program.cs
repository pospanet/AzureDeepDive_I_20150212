using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugging
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string[] stringArray =
			{
				"First string", "Second string", "Next string", "Another one", "Last string"
			};
			Task[] tasks = stringArray.Select(str => Task.Run(() => Foo(str))).ToArray();
			Task.WaitAll(tasks);
			System.Console.In.ReadLine();
		}

		private static void Foo(string str)
		{
			string output;
			output = str;
			output += str;
			output = ":-)";
			output = str;
			Console.Out.WriteLine(output);
		}
	}
}
