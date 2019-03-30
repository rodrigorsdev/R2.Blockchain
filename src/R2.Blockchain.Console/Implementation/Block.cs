using R2.Blockchain.Console.Interface;
using System;

namespace R2.Blockchain.Console.Implementation
{
    public class Block : IBlock
    {
        public Block(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Nonce = 0;
            PreviousHash = new byte[] { 0x00 };
            Timestamp = DateTime.UtcNow;
        }

        public byte[] Data { get; }
        public byte[] Hash { get; set ; }
        public int Nonce { get ; set ; }
        public byte[] PreviousHash { get ; set; }
        public DateTime Timestamp { get; }
        
        public override string ToString()
        {
            return $"{BitConverter.ToString(Hash).Replace("-","")}:/n{BitConverter.ToString(PreviousHash).Replace("-", "")}:/n{Nonce}:/n{Timestamp}";
        }
    }
}