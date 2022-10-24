using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomFileSystem
{
    private string dir = Application.streamingAssetsPath;
    private string location = "";
    private bool isFileExists = false;

    public CustomFileSystem(string file = "")
    {
        setFile(file);
    }

    public void setFile(string file = "")
    {
        location = System.IO.Path.Combine(dir, file);
        fileExists();
    }

    public string read(bool b64 = false)
    {
        string buff = "";
        List<string> list = getAllDatas();
        for (int i = 0; i < list.Count; i++)
        {
            buff = buff + list[i];
            if (i + 1 < list.Count)
            {
                buff = buff + '\n';
            }
        }
        if (b64)
        {
            buff = b64decode(buff);
        }
        return buff;
    }

    public List<string> getAllDatas()
    {
        fileExists();
        return (isFileExists)
        ?
            File.ReadAllLines(location).ToList()
        :
            new List<string>() { "File Not Found" }
        ;
    }

    public bool create()
    {
        fileExists();
        if (isFileExists)
        {
            return true;
        }
        else
        {
            try
            {
                string directory = location;
                directory = directory.Replace("\\", "/");
                directory = directory.Substring(0, directory.LastIndexOf("/"));
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
                File.Create(location).Dispose();
            }
            catch (IOException e)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    public string b64encode(string text)
    {
        return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(text));
    }

    public string b64decode(string text)
    {
        return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(text));
    }

    public bool write(string data, bool append = false, bool b64 = false)
    {
        fileExists();

        if (isFileExists)
        {
            if (append)
            {
                if (b64)
                {
                    string r = read(true);
                    data = r + data;
                    data = b64encode(data);
                    File.WriteAllText(location, data);

                }
                else
                {
                    File.AppendAllText(location, data);
                }
            }
            else
            {
                if (b64)
                {
                    data = b64encode(data);
                }
                File.WriteAllText(location, data);
            }
            return true;
        }

        return false;
    }

    public bool fileExists()
    {
        isFileExists = File.Exists(location);
        return (isFileExists);
    }

}
