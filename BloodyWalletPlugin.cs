using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Bloody.Core.API;
using Bloody.Core.Helper;
using BloodyWallet.DB;

namespace BloodyWallet;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class BloodyWalletPlugin : BasePlugin
{
#nullable disable
    internal static ManualLogSource Logger { get; private set; }
    internal static BloodyWalletPlugin Instance { get; private set; }
    internal static ConfigEntry<string> nameToken;
    internal static ConfigEntry<string> password;
#nullable enable

    public BloodyWalletPlugin() : base()
    {
        Logger = Log;
        Instance = this;

        nameToken = Config.Bind("General", "Name", "BloodyTokens", "Name of your virtual currency");
        password = Config.Bind("General", "Password", UtilsCore.RandomString(20), "This password is the one you must configure in other mods that can use this framework. Every time you do a new installation on a server, a new one is generated automatically. you can change it if you want");
    }

    public override void Load()
    {

        LoadDB.CreateAndLoadDBFiles();

        EventsHandlerSystem.OnSaveWorld += SaveDB.SaveDBTOJSON;

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");
    }

    public override bool Unload()
    {
        EventsHandlerSystem.OnSaveWorld -= SaveDB.SaveDBTOJSON;
        return true;
    }

}
