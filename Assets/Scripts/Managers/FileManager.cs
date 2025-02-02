using UnityEngine;
using System.IO;

namespace Managers
{
    public class FileManager : MonoBehaviour
    {
        public static string GetUserDataDirectory()
        {
            var appDataDir = Path.Combine(Application.persistentDataPath, "Shadow of Roles", "data");
    
            if (!Directory.Exists(appDataDir))
            {
                try
                {
                    Directory.CreateDirectory(appDataDir);
                }
                catch (IOException e)
                {
                    Debug.LogError(e);
                }
            }
    
            return appDataDir;
        }
    }
}
