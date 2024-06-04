using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Bloody.Core.API.v1;
using Bloody.Core.Helper.v1;
using BloodyWallet.Command;
using BloodyWallet.DB;
using VampireCommandFramework;

namespace BloodyWallet;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("trodi.Bloody.Core")]
[BepInDependency("gg.deca.VampireCommandFramework")]
public class BloodyWalletPlugin : BasePlugin
{
#nullable disable
    internal static ManualLogSource Logger { get; private set; }
    internal static BloodyWalletPlugin Instance { get; private set; }
    internal static ConfigEntry<string> nameToken;
    internal static ConfigEntry<bool> enabledAdminCommand;
    internal static ConfigEntry<bool> enabledUsersCommand;
    internal static ConfigEntry<int> prefabGUIDExchange;
#nullable enable

    public BloodyWalletPlugin() : base()
    {
        Logger = Log;
        Instance = this;

        nameToken = Config.Bind("General", "Name", "BloodyTokens", "Name of your virtual currency");
        enabledAdminCommand = Config.Bind("General", "adminCommand", true, "Enable admin commands");
        enabledUsersCommand = Config.Bind("General", "usersCommand", true, "Enable users commands");
        prefabGUIDExchange = Config.Bind("General", "prefabGUIDExchange", -77477508, "PrfabGUID for exchange tokens");
    }

    public override void Load()
    {

        LoadDB.CreateAndLoadDBFiles();

        EventsHandlerSystem.OnSaveWorld += SaveDB.SaveDBTOJSON;
        // Register all commands in the assembly with VCF
        if (enabledAdminCommand.Value) CommandRegistry.RegisterCommandType(typeof(WalletAdminCommand));
        if (enabledUsersCommand.Value) CommandRegistry.RegisterCommandType(typeof(WalletUserCommand));

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");
    }

    public override bool Unload()
    {
        EventsHandlerSystem.OnSaveWorld -= SaveDB.SaveDBTOJSON;
        return true;
    }

}
