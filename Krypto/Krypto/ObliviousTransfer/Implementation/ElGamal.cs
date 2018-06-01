using ObliviousTransfer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ObliviousTransfer.Implementation
{
    public interface IElGamal
    {
        ElGamalModel GenerateElGamalModel(); 
        ElGamalData Encrypt(ElGamalModel elGamalModel, string data);
        string Decrypt(ElGamalModel elGamalModel, ElGamalData data);
        BigInteger GenerateC(ElGamalModel elGamalModel);
    }



    public class ElGamal : IElGamal
    {
        private IBigIntegerRandomGenerator _bigIntegerRandomGenerator;

        public ElGamal(): this(new BigIntegerRandomGenerator()) { }

        public ElGamal(IBigIntegerRandomGenerator bigIntegerRandomGenerator)
        {
            this._bigIntegerRandomGenerator = bigIntegerRandomGenerator;
        }

        public ElGamalModel GenerateElGamalModel()
        {
            var result = new ElGamalModel
            {
                P = 1263154214185307,
                G = 4,
                Private = 1354,
            };
            result.Public = BigInteger.ModPow(result.G, result.Private, result.P);
            return result;
        }

        public string Decrypt(ElGamalModel elGamalModel, ElGamalData data)
        {
            throw new NotImplementedException();
        }

        public ElGamalData Encrypt(ElGamalModel elGamalModel, string data)
        {
            var byteArray =  Encoding.UTF8.GetBytes(data);

            var rand = _bigIntegerRandomGenerator.Generate(elGamalModel.P.ToByteArray().Length - 2);
            throw new NotImplementedException();
        }

        public BigInteger GenerateC(ElGamalModel elGamalModel)
        {
            return _bigIntegerRandomGenerator.Generate(elGamalModel.P.ToByteArray().Length - 2);
        }
    }

}
