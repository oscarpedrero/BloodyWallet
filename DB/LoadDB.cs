using BepInEx;
using BloodyWallet.DB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BloodyWallet.DB
{
    internal class LoadDB
    {

        internal static readonly string ConfigPath = Path.Combine(Paths.ConfigPath, "BloodyWallet");
        internal static string TokensDBFile = Path.Combine(ConfigPath, "tokens.json");
        internal static string LogDBFile = Path.Combine(ConfigPath, "log.json");

        internal static bool CreateAndLoadDBFiles()
        {
            CreateFilesConfig();
            LoadTokenFile();
            LoadLogFile();
            return true;
        }

        internal static bool CreateFilesConfig()
        {
            try
            {
                if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);
                if (!File.Exists(TokensDBFile)) File.WriteAllText(TokensDBFile, "[]");
                if (!File.Exists(LogDBFile)) File.WriteAllText(LogDBFile, "[]");
                return true;
            }
            catch (Exception error)
            {
                BloodyWalletPlugin.Logger.LogError($"Error: {error.Message}");
                return false;
            }
            

        }

        internal static bool LoadTokenFile()
        {

            try
            {
                string json = File.ReadAllText(TokensDBFile);

                var tokenDB = JsonSerializer.Deserialize<List<TokenModel>>(json);
                Database.SetTokensDB(tokenDB);
                return true;
            }
            catch (Exception error)
            {
                BloodyWalletPlugin.Logger.LogError($"Error: {error.Message}");
                return false;
            }

        }

        internal static bool LoadLogFile()
        {

            try
            {
                string json = File.ReadAllText(TokensDBFile);

                var logDB = JsonSerializer.Deserialize<List<LogModel>>(json);
                Database.SetLogDB(logDB);
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
