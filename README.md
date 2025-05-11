### Bloody.Wallet

This framework is designed to add a virtual currency to VRising. This opens up a world of possibilities for other mods to integrate with it and serve as a bridge for other mods to use this functionality.

## [BepInEx 1.733.2 (RC2)](https://github.com/decaprime/VRising-Modding/releases/tag/1.733.2)

## Requirements

Ensure the following mods are installed for seamless integration:

1. [BepInEx 1.733.2 (RC2)](https://github.com/decaprime/VRising-Modding/releases/tag/1.733.2)
2. [VampireCommandFramework](https://thunderstore.io/c/v-rising/p/deca/VampireCommandFramework/)
3. [Bloody.Core](https://thunderstore.io/c/v-rising/p/Trodi/BloodyCore/)

<details>
<summary>Changelog</summary>

`1.0.0`
- Updated to Oakveil

`0.1.2`
- Fixed error when you make a negative money transfer it doubles the amount in the wallet

`0.1.0`
- First Release


- 
</details>

## Instructions for using it with your mod

You must include it in your `.csproj` file as a nuget package

```c#
<PackageReference Include="Bloody.Wallet" Version="1.0.*" />
```

You must include it as a project dependency in your `Plugin.cs` file

```c#
[BepInDependency("trodi.Bloody.Wallet")]
```

If you want to make the use of Bloody.Wallet an optional dependency, you can do so by including it as an optional dependency in your `Plugin.cs` file

```c#
[BepInDependency("trodi.Bloody.Wallet", BepInDependency.DependencyFlags.SoftDependency)]
```

## API

You have several methods available in the API that you can use, as detailed below:

```c#
using BloodyWallet.API;
```

Add a certain amount of tokens to a user.

```c#
bool AddTokenToUser(int _amount, string _method, Entity playerReciviedTokens, Entity userEntityExecuteFunction, out string message)
```

Remove a certain amount of tokens from a user.

```c#
bool RemoveToken(int _amount, string _method, Entity playerReciviedTokens, Entity userEntityExecuteFunction, out string message)
```

Returns the list of tokens for all users on the server, and if we specify a player's name, it will only return the tokens for that user.

```c#
bool listToken(string _playerName, out List<string> message)
```

Returns the number of tokens for a user as an integer.

```c#
bool GetTotalTokensForUser(string _playerName, out int tokens)
```

Transfers tokens from one user to another.

```c#
bool TranferTokenFromOtherUser(int _amount, string _method, Entity playerReciviedTokens, Entity fromUserSendToken, out string message)
```

## Installing on your server

1. Copy `Bloody.Wallets.dll` to your `BepInEx/Plugins` directory.
2. Launch the server once to generate the config file; configurations will be located in the `BepInEx/Config` directory.

## Configuration

In the configuration file **trodi.bloody.Wallet** you have several options to configure the mod to your liking

```ini
## Settings file was created by plugin BloodyWallet v0.0.9999
## Plugin GUID: trodi.Bloody.Wallet

[General]

## Name of your virtual currency
# Setting type: String
# Default value: BloodyTokens
Name = BloodyTokens

## Enable admin commands
# Setting type: Boolean
# Default value: true
adminCommand = true

## Enable users commands
# Setting type: Boolean
# Default value: true
usersCommand = true

## PrfabGUID for exchange tokens
# Setting type: Int32
# Default value: -77477508
prefabGUIDExchange = -257494203
```

## Available Commands

From the mod options, you can enable or disable commands for admins and/or users as detailed below

### Admins

```ansi
.bwa add <PlayerName> <Amount>
```
- Add tokens to a player
  - Example: `.bwa add Trodi 5`

```ansi
.bwa remove <PlayerName> <Amount>
```
- Remove tokens from a player.
  - Example: `.bwa remove Trodi 5`

```ansi
.bwa list <PlayerName>
```
- List of players with their respective tokens. If <PlayerName> is specified, it only returns that player's tokens.
  - Examples: `.bwa list` or `.bwa list Trodi`

### Players

```ansi
.bw transfer <PlayerName> <Amount>
```
- Transfer tokens from your wallet to another player
  - Example: `.bw transfer Trodi 5`

```ansi
.bw me 
```
- Show your tokens.
  - Example: `.bw me`

```ansi
.bwa exchange <Amount>
```
- Exchange your tokens for a game item.
  - Examples: `.bwa exchange 5`

## Credits

The [V Rising Mod Community](https://discord.gg/vrisingmods) is the premier community for V Rising mods.

[@Deca](https://github.com/decaprime), thank you for the exceptional frameworks [VampireCommandFramework](https://github.com/decaprime/VampireCommandFramework)

**Special thanks to the testers and supporters of the project:**

- @Vex, owner & founder of the [Vexor RPG](https://discord.gg/JpVsKVvKNR) server, a tester and great supporter who provided his server as a test platform and took care of all the graphics and documentation.