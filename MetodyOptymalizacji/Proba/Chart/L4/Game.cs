using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart.L4
{
    public class Game
    {
        private double _success;
        private double _fail;
        private Coin _coin;

        public Game(double success, double fail) : this(success, fail, new Coin())
        {
        }

        public Game(double success, double fail, Coin coin)
        {
            _success = success;
            _fail = fail;
            _coin = coin;
        }

        public double Play(int times)
        {
            var result = 0.0;

            for(int i = 0; i < times; i++)
            {
                result += _coin.ThrowCoin() ? _success : -_fail;
            }

            return result;
        }


    }
}
