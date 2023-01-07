using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace freeArve.util;

public class ConfigFile
{
    public string path { get; }

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

    public ConfigFile(string path)
    {
        this.path = path;
    }

    public string Read(string Key, string Section = null)
    {
        var RetVal = new StringBuilder(255);
        GetPrivateProfileString(Section ?? path, Key, "", RetVal, 255, path);
        return RetVal.ToString();
    }

    public void Write(string Key, string Value, string Section = null)
    {
        WritePrivateProfileString(Section ?? path, Key, Value, path);
    }

    public void DeleteKey(string Key, string Section = null)
    {
        Write(Key, null, Section ?? path);
    }

    public void DeleteSection(string Section = null)
    {
        Write(null, null, Section ?? path);
    }

    public bool KeyExists(string Key, string Section = null)
    {
        return Read(Key, Section).Length > 0;
    }
}