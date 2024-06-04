using Bloody.Core;
using Bloody.Core.API;
using Bloody.Core.API.v1;
using Bloody.Core.GameData.v1;
using Bloody.Core.Methods;
using Bloody.Core.Models;
using Bloody.Core.Models.v1;
using BloodyWallet.API;
using BloodyWallet.DB.Models;
using Stunlock.Core;
using System.Collections.Generic;
using VampireCommandFramework;

namespace BloodyWallet.Command
{
    [CommandGroup("bw")]
    public class WalletUserCommand
    {
        [Command("transfer", usage: "<PlayerName> <Amount>", description: "Transfer token from your wallet to a another player", adminOnly: false)]
        public static void TransferToken(ChatCommandContext ctx, string _playerName, int _amount)
        {
            UserModel userModel = GameData.Users.GetUserByCharacterName(ctx.User.CharacterName.Value);
            UserModel playerModel = GameData.Users.GetUserByCharacterName(_playerName);
            if (WalletAPI.TranferTokenFromOtherUser(_amount, MethodsLog.FromCommandAdmin, playerModel.Entity, userModel.Entity, out string message))
            {
                ctx.Reply(message);
                playerModel.SendSystemMessage($"You just received a transfer of {_amount} tokens from player {userModel.CharacterName}");
            }
            else
            {
                throw ctx.Error(message);
            }
        }

        [Command("me", usage: "", description: "Show your tokens.", adminOnly: false)]
        public static void listToken(ChatCommandContext ctx)
        {

            if (WalletAPI.listToken(ctx.User.CharacterName.Value, out List<string> messages))
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

        [Command("exchange", usage: "<Amount>", description: "Exchange yours tokens for game item.", adminOnly: false)]
        public static void ExchangeToken(ChatCommandContext ctx, int amount)
        {

            if (WalletAPI.GetTotalTokensForUser(ctx.User.CharacterName.Value, out int tokens))
            {
                UserModel userModel = GameData.Users.GetUserByCharacterName(ctx.User.CharacterName.Value);

                if (tokens < amount)
                {
                    throw ctx.Error($"You don't have enough token to exchange [Tokens: {tokens}] ");
                }
                if (WalletAPI.RemoveToken(amount, "exchanmge", userModel.Entity, userModel.Entity, out string message))
                {
                    UserSystem.TryAddInventoryItemOrDrop(userModel.Entity, new PrefabGUID(BloodyWalletPlugin.prefabGUIDExchange.Value), amount);
                    ctx.Reply("Change made correctly");
                }
                else
                {
                    throw ctx.Error($"You don't have enough token to exchange [Tokens: {tokens}] ");
                }


            }
            else
            {

                throw ctx.Error($"You don't have enough token to exchange [Tokens: {tokens}] ");

            }

        }
    }
}
