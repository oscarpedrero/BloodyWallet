using System;

namespace BloodyWallet.DB.Models
{
    [Serializable]
    internal class TokenModel
    {

        public string CharacterName { get; set; } = "";
        public int Tokens { get; set; }

    }
}
