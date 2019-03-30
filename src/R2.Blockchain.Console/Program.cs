using R2.Blockchain.Console.Implementation;
using R2.Blockchain.Console.Interface;
using R2.Blockchain.Console.Middleware;
using System;
using System.Linq;

namespace R2.Blockchain.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(DateTime.UtcNow.Millisecond);
            IBlock genesis = new Block(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 });
            byte[] difficulty = new byte[] { 0x00, 0x00 };

            Implementation.Blockchain chain = new Implementation.Blockchain(difficulty, genesis);

            for (int i = 0; i < 1000; i++)
            {
                var data = Enumerable.Range(0, 2256).Select(a => (byte)random.Next());
                chain.Add(new Block(data.ToArray()));
                System.Console.WriteLine(chain.LastOrDefault()?.ToString());

                System.Console.WriteLine($"Chain is valid: {chain.IsValid()}");

                System.Console.Write(Environment.NewLine);
            }

            System.Console.ReadKey();
        }
    }
}