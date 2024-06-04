using Bloody.Core;
using Bloody.Core.GameData.v1;
using Bloody.Core.Models;
using Bloody.Core.Models.v1;
using BloodyWallet.API;
using BloodyWallet.DB.Models;
using System.Collections.Generic;
using VampireCommandFramework;

namespace BloodyWallet.Command
{
    [CommandGroup("bwa")]
    public class WalletAdminCommand
    {
        [Command("add", usage: "<PlayerName> <Amount>", description: "Add token to a player", adminOnly: true)]
        public static void AddToken(ChatCommandContext ctx, string _playerName, int _amount)
        {
            UserModel userModel = GameData.Users.GetUserByCharacterName(ctx.User.CharacterName.Value);
            UserModel playerModel = GameData.Users.GetUserByCharacterName(_playerName);
            if (WalletAPI.AddTokenToUser(_amount, MethodsLog.FromCommandAdmin, playerModel.Entity, userModel.Entity, out string message))
            {
                ctx.Reply(message);
            }
            else
            {
                throw ctx.Error(message);
            }
        }

        [Command("remove", usage: "<PlayerName> <Amount>", description: "Remove token to a player.", adminOnly: true)]
        public static void RemoveToken(ChatCommandContext ctx, string _playerName, int _amount)
        {

            UserModel userModel = GameData.Users.GetUserByCharacterName(ctx.User.CharacterName.Value);
            UserModel playerModel = GameData.Users.GetUserByCharacterName(_playerName);
            if (WalletAPI.RemoveToken(_amount, MethodsLog.FromCommandAdmin, playerModel.Entity, userModel.Entity, out string message))
            {
                ctx.Reply(message);
            }
            else
            {
                throw ctx.Error(message);
            }
        }

        [Command("list", usage: "<PlayerName>", description: "List of players with their respective tokens. If the <PlayerName> is specified, it only returns the tokens of that player", adminOnly: true)]
        public static void listToken(ChatCommandContext ctx, string _playerName = null)
        {

            if (_playerName == null)
            {
                _playerName = string.Empty;
            }


            if (WalletAPI.listToken(_playerName, out List<string> messages))
            {
                foreach (var message in messages)
                {
                    ctx.Reply(message);
                }

            }
            else
            {
                foreach (var message in messages)
                {
                    throw ctx.Error(message);
                }
            }

        }
    }
}
