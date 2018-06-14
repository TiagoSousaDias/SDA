using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace TBMails
{
    class IniFile
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

        public List<string []> GetKeys( string category)
        {

            byte[] buffer = new byte[2048];

            GetPrivateProfileSection(category, buffer, 2048, Path);

          

            String[] tmp = Encoding.UTF8.GetString(buffer).Trim('\0').Split('\0');

            List<string[]> result = new List<string[]>();
            foreach (String entry in tmp)
            {
                byte[] b = Encoding.Unicode.GetBytes(entry);
                string i = Encoding.Unicode.GetString(b);
                string[] t2 = {  i.Substring(0, i.IndexOf("=")),
                    i.Substring(i.IndexOf("=") + 1, i.Length-i.IndexOf("=")-1)
                    //entry.Substring(entry.IndexOf(";") + 1, entry.Length - entry.IndexOf(";") - 1)
                };
                string[] t = {  entry.Substring(0, entry.IndexOf("=")),
                    entry.Substring(entry.IndexOf("=") + 1, entry.Length-entry.IndexOf("=")-1)
                    //entry.Substring(entry.IndexOf(";") + 1, entry.Length - entry.IndexOf(";") - 1)
                };
                result.Add(t2);
            }

            return result;
        }

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {

            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

    }
}
