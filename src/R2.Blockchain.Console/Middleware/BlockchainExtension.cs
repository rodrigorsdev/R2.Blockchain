using R2.Blockchain.Console.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace R2.Blockchain.Console.Middleware
{
    public static class BlockchainExtension
    {
        public static byte[] GenerateHash(this IBlock block)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write(block.Data);
                bw.Write(block.Nonce);
                bw.Write(block.Timestamp.ToBinary());
                bw.Write(block.PreviousHash);

                var starr = ms.ToArray();
                return sha.ComputeHash(starr);
            }
        }

        public static byte[] MineHash(this IBlock block, byte[] difficulty)
        {
            if (difficulty == null)
                throw new ArgumentNullException(nameof(difficulty));

            byte[] hash = new byte[0];
            int d = difficulty.Length;

            while (!hash.Take(2).SequenceEqual(difficulty))
            {
                block.Nonce++;
                hash = block.GenerateHash();
            }

            return hash;
        }

        public static bool IsValid(this IBlock block)
        {
            var bk = block.GenerateHash();
            return block.Hash.SequenceEqual(bk);
        }

        public static bool IsValidPreviousBlock(this IBlock block, IBlock previousBlock)
        {
            if (previousBlock == null)
                throw new ArgumentNullException(nameof(previousBlock));

            var previous = previousBlock.GenerateHash();
            return previousBlock.IsValid() && block.PreviousHash.SequenceEqual(previous);
        }

        public static bool IsValid(this IEnumerable<IBlock> itens)
        {
            var enumerable = itens.ToList();
            return enumerable.Zip(enumerable.Skip(1), Tuple.Create).All(block => block.Item2.IsValid());
        }
    }
}
