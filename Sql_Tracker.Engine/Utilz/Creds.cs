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
using Sql_Tracker.Engine.Interfaces;

namespace Sql_Tracker.Engine.Utilz
{
    
    public class Creds : ICreds
    {

        private ILogger<Creds> log;
        private ISettings Setting;

        private PwDatabase _pdb;
        private PwEntry _currentEntry = new PwEntry(false, false);

        private NullStatusLogger _IStatusLogger = new NullStatusLogger();

        public Creds(ILogger<Creds> logger, ISettings settings)
        {
            log = logger;
            Setting = settings;

            log.LogDebug("Opening Cred File");
            IOConnectionInfo m_ioInfo = new IOConnectionInfo();
            m_ioInfo.Path = settings.CredFile;

            log.LogDebug("Opening Cred Key File");
            CompositeKey m_pKey = new CompositeKey();
            m_pKey.AddUserKey(new KcpKeyFile(Setting.CredKeyFile));

            log.LogDebug("Opening Credential Database");
            _pdb = new PwDatabase();
            _pdb.Open(m_ioInfo, m_pKey, _IStatusLogger);
        
        }

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

        public string GetPassword(string source)
        {
            _GetCurrentItem(source);
            return _currentEntry.Strings.ReadSafe("Password");
        }

        public string GetUsername(string source)
        {
            _GetCurrentItem(source);
            return _currentEntry.Strings.ReadSafe("Username");
        }

        public void SetPassword(string source, string username, string password)
        {
            PwEntry pwEntry = new PwEntry(true, true);
            pwEntry.Strings.Set("Title", new KeePassLib.Security.ProtectedString(true, source));
            pwEntry.Strings.Set("Username", new KeePassLib.Security.ProtectedString(true, username));
            pwEntry.Strings.Set("Password", new KeePassLib.Security.ProtectedString(true, password));

            _pdb.RootGroup.AddEntry(pwEntry, true);

            _pdb.Save(_IStatusLogger);
        }
    }


}
