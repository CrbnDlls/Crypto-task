using System.Collections.Generic;

namespace Crypto_task.Core.Models
{
    public class CoinCapResponce<T>
    {
        public CoinCapResponce() { }

        public List<T> Data;

        public string Timestamp;
    }
}
