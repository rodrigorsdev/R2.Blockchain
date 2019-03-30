using R2.Blockchain.Console.Interface;
using R2.Blockchain.Console.Middleware;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace R2.Blockchain.Console.Implementation
{
    public class Blockchain : IEnumerable<IBlock>
    {
        private List<IBlock> _itens = new List<IBlock>();

        public Blockchain(byte[] difficulty, IBlock genesis)
        {
            Difficulty = difficulty;
            genesis.Hash = genesis.MineHash(difficulty);
            Itens.Add(genesis);
        }

        public void Add(IBlock block)
        {
            var lastBlock = Itens.LastOrDefault();

            if (lastBlock != null)
                block.PreviousHash = lastBlock.Hash;

            block.Hash = block.MineHash(Difficulty);
            Itens.Add(block);
        }

        public int Count => Itens.Count;

        public IBlock this[int index]
        {
            get => Itens[index];
            set => Itens[index] = value;
        }

        public List<IBlock> Itens
        {
            get => _itens;
            set => _itens = value;
        }

        public byte[] Difficulty { get; }

        public IEnumerator<IBlock> GetEnumerator()
        {
            return Itens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}