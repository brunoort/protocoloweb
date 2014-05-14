using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.DirectoryServices;

namespace ProtocoloWeb
{
    public class AD
    {
        private DirectoryEntry _directoryEntry;

        private String LDAPPath
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPPath"];
            }
        }
        private String LDAPUser
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPUser"];
            }
        }
        private String LDAPPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["LDAPPassword"];
            }
        }

        private DirectoryEntry SearchRoot
        {
            get
            {
                if (_directoryEntry == null)
                {
                    string path = LDAPPath;
                    string user = LDAPUser;
                    string pass = LDAPPassword;

                    _directoryEntry = new DirectoryEntry(LDAPPath, LDAPUser, LDAPPassword, AuthenticationTypes.Secure);
                }
                return _directoryEntry;
            }
        }

        public ADUsuario GetUserByCredentials(String userName, string pass)
        {
            try
            {
                _directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=user)(samaccountname=" + userName + "))";
                SearchResult results = directorySearch.FindOne();

                if (results != null)
                {
                    DirectoryEntry user = new DirectoryEntry(results.Path, userName, pass);

                    return ADUsuario.GetUser(user);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}