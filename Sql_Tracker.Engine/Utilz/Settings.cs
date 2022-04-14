using Sql_Tracker.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeePassLib;
using KeePassLib.Interfaces;
using KeePassLib.Keys;
using KeePassLib.Serialization;
using Microsoft.Extensions.Logging;

namespace Sql_Tracker.Engine.Utilz
{
    internal class Settings : ISettings
    {
        private ILogger<Settings> log;
        IConfig _config;

        private PwDatabase _pdb;
        private PwEntry _currentEntry = new PwEntry(false, false);
        private NullStatusLogger _IStatusLogger = new NullStatusLogger();

        public Settings(ILogger<Settings> logger, IConfig config)
        {
            log = logger;
            _config = config;
            SqlFiles = _config.GetString("sqlfiles");
            CredKeyFile = _config.GetString("credkeyfile");
            CredFile = _config.GetString("credfile");
            
            ConnectionString = _config.GetString("connectionstring");

            log.LogDebug("Opening Cred File");
            IOConnectionInfo m_ioInfo = new IOConnectionInfo();
            m_ioInfo.Path = CredFile;

            log.LogDebug("Opening Cred Key File");
            CompositeKey m_pKey = new CompositeKey();
            m_pKey.AddUserKey(new KcpKeyFile(CredKeyFile));

            log.LogDebug("Opening Credential Database");
            _pdb = new PwDatabase();
            _pdb.Open(m_ioInfo, m_pKey, _IStatusLogger);

            _GetCurrentItem(_config.GetString("connectionstring"));

            ConnectionString = _currentEntry.Strings.ReadSafe("connectionstring");
        }

        public string ConnectionString { get; private set; }
        public string SqlFiles { get; set; }
        public string CredKeyFile { get; private set; }
        public string CredFile { get; private set; }

        private void _GetCurrentItem(string source)
        {
            if (_currentEntry.Strings.ReadSafe("Title") == source) return;

            var pwItems = _pdb.RootGroup.GetObjects(true, true);

            foreach (PwEntry pwItem in pwItems)
            {
                if (pwItem.Strings.ReadSafe("Title") == source)
                {
                    _currentEntry = pwItem;
                    break;
                }
            }
        }

        public void ReadAllFromCurrent(string source)
        {
            _GetCurrentItem(source);
            foreach (var item in _currentEntry.Strings)
            {
                log.LogInformation("{0} - {1}", item.Key, item.Value);
            }            
        }

        public string GetPassword(string source)
        {
            _GetCurrentItem(source);
            return _currentEntry.Strings.ReadSafe("Password");
        }

        public string GetUsername(string source)
        {
            _GetCurrentItem(source);
            return _currentEntry.Strings.ReadSafe("UserName");
        }

        public void SetPassword(string source, string username, string password)
        {
            PwEntry pwEntry = new PwEntry(true, true);
            pwEntry.Strings.Set("Title", new KeePassLib.Security.ProtectedString(true, source));
            pwEntry.Strings.Set("UserName", new KeePassLib.Security.ProtectedString(true, username));
            pwEntry.Strings.Set("Password", new KeePassLib.Security.ProtectedString(true, password));

            _pdb.RootGroup.AddEntry(pwEntry, true);

            _pdb.Save(_IStatusLogger);
        }

    }
}
