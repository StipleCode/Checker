using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker
{
    public class Data
    {
        public string URL { get; set; }

        public string Username { get; set; }

        public string Pass { get; set; }

        public string Path { get; set; }

        public List<string> folderList { get; }

        public List<listDataStruct> listData { get; set; }

        public Data(string setURL, string setUsername, string setPass)
        {
            this.URL = setURL;
            this.Username = setUsername;
            this.Pass = setPass;
            this.folderList = new List<string>();
            listData = new List<listDataStruct>();
        }

        public void AddFolder(string folder)
        {
            if (this.folderList.Contains(folder))
                return;
            this.folderList.Add(folder);
        }

        public void DelFolder(string folder)
        {
            if (!this.folderList.Contains(folder))
                return;
            this.folderList.Remove(folder);
        }
    }

    public class listDataStruct
    {
       public  listDataStruct(string url, string login, string password)
        {
            URL = url;

            Username = login;
            Pass = password;
        }
        public string URL { get; set; }

        public string Username { get; set; }

        public string Pass { get; set; }
    }
}
