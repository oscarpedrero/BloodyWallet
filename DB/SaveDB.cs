using System;
using System.IO;
using System.Text.Json;

namespace BloodyWallet.DB
{
    internal class SaveDB
    {
        internal static void SaveDBTOJSON()
        {
            SaveTokenList();
            SaveLogs();
            BloodyWalletPlugin.Logger.LogInfo($"Wallet Save");
        }

        internal static bool SaveTokenList()
        {
            try
            {
                var rewardList = Database._TokensDB;
                var jsonOutPut = JsonSerializer.Serialize(rewardList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(LoadDB.TokensDBFile, jsonOutPut);

                return true;
            }
            catch (Exception error)
            {
                BloodyWalletPlugin.Logger.LogError($"Error: {error.Message}");
                return false;
            }

        }

        internal static bool SaveLogs()
        {
            try
            {
                var usersRewardsPerDayList = Database._LogDB;
                var jsonOutPut = JsonSerializer.Serialize(usersRewardsPerDayList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(LoadDB.LogDBFile, jsonOutPut);

                return true;
            }
            catch (Exception error)
            {
                BloodyWalletPlugin.Logger.LogError($"Error: {error.Message}");
                return false;
            }

        }
    }
}
