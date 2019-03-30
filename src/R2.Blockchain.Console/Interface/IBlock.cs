using System;
using System.Collections.Generic;
using System.Text;

namespace R2.Blockchain.Console.Interface
{
    public interface IBlock
    {
        byte[] Data { get; }
        byte[] Hash { get; set; }
        int Nonce { get; set; }
        byte[] PreviousHash { get; set; }
        DateTime Timestamp { get; }
    }
}
