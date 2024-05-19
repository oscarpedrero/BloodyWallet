using BloodyWallet.DB.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BloodyWallet.DB
{
    internal static class Database
    {
        internal static List<TokenModel> _TokensDB = [];
        internal static List<LogModel> _LogDB = [];

        internal static List<TokenModel> getTokensDB()
        {
            return _TokensDB;
        }

        internal static List<TokenModel> SetTokensDB(List<TokenModel>? _tokenDB)
        {
            if(_tokenDB != null)
            {
                _TokensDB = _tokenDB;
            }
            return _TokensDB;
        }

        internal static List<LogModel> GetLogDB()
        {
            return _LogDB;
        }

        internal static List<LogModel> SetLogDB(List<LogModel>? _logDB)
        {
            if(_logDB != null)
            {
                _LogDB = _logDB;
            }
            return _LogDB;
        }

        internal static List<TokenModel> AddToken(TokenModel? _tokenModel)
        {
            if (_tokenModel != null)
            {
                _TokensDB.Add(_tokenModel);
            }
            return _TokensDB;
        }

        internal static List<LogModel> addLogDB(LogModel? _logDB)
        {
            if(_logDB != null)
            {
                _LogDB.Add(_logDB);
            }
            return _LogDB;
        }

        internal static bool AddLog(string _from, string _to, string _by, string _method, string _type, int _amount)
        {
            LogModel model = new()
            {
                From = _from,
                To = _to,
                By = _by,
                Method = _method,
                Type = _type,
                Amount = _amount
            };

            _LogDB?.Add(model);


            return true;
        }

        internal static bool AddTokenToPlayer(string _characterName, int _tokens)
        {
            TokenModel? model = FindBySteamID(_characterName);
            if(model == null) {
                model = new()
                {
                    CharacterName = _characterName,
                    Tokens = _tokens
                };
                _TokensDB.Add(model);
                return true;
            }
            model.Tokens += _tokens;

            return true;
        }

        internal static bool RemoveTokenToPlayer(string _characterName, int _tokens)
        {
            TokenModel? model = FindBySteamID(_characterName);
            if(model == null) {
                model = new()
                {
                    CharacterName = _characterName,
                    Tokens = 0
                };
                _TokensDB.Add(model);
            } else
            {
                if(model.Tokens - _tokens > 0)
                {
                    model.Tokens -= _tokens;
                } else
                {
                    model.Tokens = 0;
                }
            }

            return true;
        }

        internal static bool TransferTokenToPlayer(string _characterNameFrom, string _characterNameTo, int _tokens)
        {
            TokenModel? modelFrom = FindBySteamID(_characterNameFrom);
            if (modelFrom == null)
            {
                return false;
            }
            if (modelFrom.Tokens - _tokens < 0)
            {
                return false;
            }
            modelFrom.Tokens -= _tokens;

            TokenModel? modelTo = FindBySteamID(_characterNameTo);
            if (modelTo == null)
            {
                modelTo = new()
                {
                    CharacterName = _characterNameTo,
                    Tokens = _tokens
                };
                _TokensDB.Add(modelTo);
                return true;
            }
            modelTo.Tokens += _tokens;

            return true;
        }

        private static TokenModel? FindBySteamID(string _characterName)
        {
            return _TokensDB.Where(x => x.CharacterName == _characterName).FirstOrDefault();
        }
    }
}
