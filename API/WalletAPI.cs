using Bloody.Core.Models;
using Bloody.Core;
using BloodyWallet.DB.Models;
using BloodyWallet.DB;
using Unity.Entities;
using System.Linq;
using System.Collections.Generic;
using System;

namespace BloodyWallet.API;

public static class WalletAPI
{
    public static bool AddTokenToUser(string password, int _amount, string _method, Entity playerReciviedTokens, Entity userEntityExecuteFunction, out string message)
    {
        if (password != BloodyWalletPlugin.password.Value)
        {
            message = $"Password i not valid";
            return false;
        }

        try
        {
            UserModel user = Core.Users.FromEntity(userEntityExecuteFunction);
            UserModel player = Core.Users.FromEntity(playerReciviedTokens);

            string playerCharacterName = player.CharacterName;
            if (Database.AddTokenToPlayer(playerCharacterName, _amount))
            {
                message = $"Added {_amount} {BloodyWalletPlugin.nameToken.Value} to user {player.CharacterName}";
                Database.AddLog("", playerCharacterName, user.CharacterName, _method, TypesLog.Add, _amount);
                SaveDB.SaveDBTOJSON();
                return true;
            }
            else
            {
                message = $"There was an error trying to add {_amount} {BloodyWalletPlugin.nameToken} to user {user.CharacterName}";
                return false;
            }
        }
        catch (Exception e)
        {
            message = $"Error {e.Message}";
            return false;
        }
    }

    public static bool RemoveToken(string password, int _amount, string _method, Entity playerReciviedTokens, Entity userEntityExecuteFunction, out string message)
    {

        if (password != BloodyWalletPlugin.password.Value)
        {
            message = $"Password i not valid";
            return false;
        }

        try
        {
            UserModel user = Core.Users.FromEntity((Entity)userEntityExecuteFunction);
            UserModel player = Core.Users.FromEntity(playerReciviedTokens);

            string playerCharacterName = player.CharacterName;
            if (Database.RemoveTokenToPlayer(playerCharacterName, _amount))
            {
                message = $"Remove {_amount} {BloodyWalletPlugin.nameToken.Value} to user {playerCharacterName}";
                Database.AddLog("", playerCharacterName, user.CharacterName, _method, TypesLog.Remove, _amount);
                SaveDB.SaveDBTOJSON();
                return true;
            }
            else
            {
                message = $"There was an error trying to remove {_amount} {BloodyWalletPlugin.nameToken} to user {playerCharacterName}";
                return false;
            }
        }
        catch (Exception e)
        {
            message = $"Error {e.Message}";
            return false;
        }
    }

    public static bool listToken(string password, string _playerName, out List<string> message)
    {
        message = new();

        if (password != BloodyWalletPlugin.password.Value)
        {
            message.Add($"Password i not valid");
            return false;
        }

        try
        {
            if (_playerName == string.Empty)
            {
                foreach (TokenModel model in Database._TokensDB)
                {
                    message.Add($"------- {model.CharacterName}: {model.Tokens} {BloodyWalletPlugin.nameToken.Value} -------");
                }
                return true;
            }
            else
            {
                UserModel user = Core.Users.GetUserByCharacterName(_playerName);
                TokenModel? tokenModel = Database._TokensDB.Where(x => x.CharacterName == user.CharacterName).FirstOrDefault();
                if (tokenModel == null)
                {
                    message.Add($"------- {user.CharacterName}: 0 {BloodyWalletPlugin.nameToken.Value} -------");

                }
                else
                {
                    message.Add($"------- {user.CharacterName}: {tokenModel.Tokens} {BloodyWalletPlugin.nameToken.Value} -------");
                }
                return true;
            }
        } catch (Exception e)
        {
            message.Add($"Error {e.Message}");
            return false;
        }
    }

    public static bool TranferTokenFromOtherUser(string password, int _amount, string _method, Entity playerReciviedTokens, Entity fromUserSendToken, out string message)
    {
        if (password != BloodyWalletPlugin.password.Value)
        {
            message = $"Password i not valid";
            return false;
        }

        try
        {
            UserModel fromplayer = Core.Users.FromEntity(fromUserSendToken);
            UserModel player = Core.Users.FromEntity(playerReciviedTokens);

            string toPlayerCharacterName = player.CharacterName;
            string fromPlayerCharacterName = fromplayer.CharacterName;
            if (Database.TransferTokenToPlayer(fromPlayerCharacterName, toPlayerCharacterName, _amount))
            {
                message = $"Added {_amount} {BloodyWalletPlugin.nameToken.Value} to user {player.CharacterName}";
                Database.AddLog(fromPlayerCharacterName, toPlayerCharacterName, string.Empty, _method, TypesLog.Transfer, _amount);
                SaveDB.SaveDBTOJSON();
                return true;

            }
            else
            {
                message = $"There was an error trying to transfer {_amount} {BloodyWalletPlugin.nameToken.Value} from user {fromplayer.CharacterName} to user {player.CharacterName}";
                return false;
            }
        }
        catch (Exception e)
        {
            message = $"Error {e.Message}";
            return false;
        }
    }
}

