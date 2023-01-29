using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_task.Core.Models
{
    public class CoinCapResponce<T>
    {
        public CoinCapResponce() { }

        public List<T> Data;

        public string Timestamp;
    }
}
