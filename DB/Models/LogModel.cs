using System;

namespace BloodyWallet.DB.Models
{
    [Serializable]
    internal class LogModel
    {

        public string From { get; set; } = "";
        public string To { get; set; } = "";
        public string Method { get; set; } = "";
        public string By { get; set; } = "";
        public string Type { get; set; } = "";
        public int Amount { get; set; } = 0;
    }
}
