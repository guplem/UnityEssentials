//      MIT License
//      
//      Copyright (c) 2020 Bronson Zgeb
//      
//      Permission is hereby granted, free of charge, to any person obtaining a copy
//          of this software and associated documentation files (the "Software"), to deal
//          in the Software without restriction, including without limitation the rights
//      to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//      copies of the Software, and to permit persons to whom the Software is
//          furnished to do so, subject to the following conditions:
//      
//      The above copyright notice and this permission notice shall be included in all
//          copies or substantial portions of the Software.
//      
//          THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//          IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//          FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//          AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//          LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//      OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//      SOFTWARE.


//  Significant parts of the code are extracted from this GitHub repository: https://github.com/BayatGames/SaveGameFree and https://github.com/UnityTechnologies/UniteNow20-Persistent-Data


using System.IO;
using System.Text;
using Essentials.SaveData;
using UnityEditor;

namespace UnityEngine
{
    /// <summary>
    /// A class to save almost any kind of data on almost all devices (not heavily tested).
    /// </summary>
    public static class SaveDataManager
    {
        public static string saveDataPath = Application.persistentDataPath;

        #if UNITY_EDITOR
        /// <summary>
        /// Opens/reveals the folder containing the saved data.
        /// </summary>
        ///
        [MenuItem("File/Open saves data folder", false, 300)]
        #endif
        public static void OpenSavedDataFolder()
        {
            #if UNITY_EDITOR
            EditorUtility.RevealInFinder(saveDataPath);
            #else
            Debug.LogError("The folder containing the saved data can not be opened using the method OpenSavedDataFolder from the class SaveDataManager outside the Unity Editor.");
            #endif
        }
        

        /// <summary>
        /// Saves data using the identifier.
        /// </summary>
        /// <param name="filename">Identifier of the file containing the data. Can route to a folder relative to the Application.persistentDataPath.</param>
        /// <param name="objectToSave">Object to save.</param>
        /// <param name="encryptionPassword">Encryption Password.</param>
        /// <param name="encoding">Encoding.</param>
        /// <typeparam name="T">The objectToSave's type.</typeparam>
        public static void Save<T>(T objectToSave, string filename, string encryptionPassword = null, Encoding encoding = null)
        {
            // Setup
            SD_JsonSerializer serializer = new SD_JsonSerializer();
            SD_Encoder encoder = new SD_Encoder();
            encoding ??= Encoding.UTF8;
            objectToSave ??= default(T);
            if (string.IsNullOrEmpty(filename))
                throw new System.ArgumentNullException(nameof(filename));
            string filePath = GetFilePath(filename);
            
            // Build IO stream
            Stream stream;
            #if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
            #if UNITY_WSA || UNITY_WINRT
			UnityEngine.Windows.Directory.CreateDirectory ( filePath );
            #else
            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);
            #endif
            #endif
            if (!encryptionPassword.IsNullEmptyOrWhiteSpace())
            {
                stream = new MemoryStream();
            }
            else
            {
            #if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
                if (Utils.IsIOSupported())
                {
            #if UNITY_WSA || UNITY_WINRT
					stream = new MemoryStream ();
            #else
                    stream = File.Create(filePath);
            #endif
                }
                else
                {
                    stream = new MemoryStream();
                }
            #else
				stream = new MemoryStream ();
            #endif
            }
            
            // Serialize object
            serializer.Serialize(objectToSave, stream, encoding);
            if (!encryptionPassword.IsNullEmptyOrWhiteSpace())
            {
                string data = System.Convert.ToBase64String(((MemoryStream) stream).ToArray());
                string encoded = encoder.Encode(data, encryptionPassword);
            #if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
                if (Utils.IsIOSupported())
                {
            #if UNITY_WSA || UNITY_WINRT
					UnityEngine.Windows.File.WriteAllBytes ( filePath, encoding.GetBytes ( encoded ) );
            #else
                    File.WriteAllText(filePath, encoded, encoding);
            #endif
                }
                else
                {
                    PlayerPrefs.SetString(filePath, encoded);
                    PlayerPrefs.Save();
                }
            #else
				PlayerPrefs.SetString ( filePath, encoded );
				PlayerPrefs.Save ();
            #endif
            }
            else if (!Utils.IsIOSupported())
            {
                string data = encoding.GetString(((MemoryStream) stream).ToArray());
                PlayerPrefs.SetString(filePath, data);
                PlayerPrefs.Save();
            }
            stream.Dispose();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Loads data using identifier.
        /// </summary>
        /// <param name="filename">Identifier of the file containing the data. Can route to a folder relative to the Application.persistentDataPath.</param>
        /// <param name="defaultValue">Default Value. Used in case the saved data is not found.</param>
        /// <param name="encryptionPassword">Encryption Password (set it to the same password you used to save it).</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="supressFileNotFoundWarning">Should the warning be disabled so if the desired data is not found no warning is created? Set it to <c>True</c> for hiding the warning.</param>
        /// <typeparam name="T">The objectToSave's type.</typeparam>
        public static T Load<T>(string filename, T defaultValue, string encryptionPassword = null, Encoding encoding = null, bool supressFileNotFoundWarning = false)
        {
            // Setup
            SD_JsonSerializer serializer = new SD_JsonSerializer();
            SD_Encoder encoder = new SD_Encoder();
            encoding ??= Encoding.UTF8;
            defaultValue ??= default(T);
            if (string.IsNullOrEmpty(filename))
                throw new System.ArgumentNullException(nameof(filename));
            string filePath = GetFilePath(filename);
            T result = defaultValue;
            
            #if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
            if (!Exists(filename))
            #else
			if ( !Exists ( filePath, path ) )
            #endif
            {
                if (!supressFileNotFoundWarning)
                    Debug.LogWarning(
                        $"The file '{GetFilePath(filename)}' was not found.    You can use the parameter 'supressFileNotFoundWarning' to disable the warning or use 'Exists()' to check if such file exists or not before trying to load them.\n" +
                        "Returning the default(T) instance."
                    );
                return result;
            }
            Stream stream;
            if (!encryptionPassword.IsNullEmptyOrWhiteSpace())
            {
                string data;
            #if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
                if (Utils.IsIOSupported())
                {
            #if UNITY_WSA || UNITY_WINRT
					data = encoding.GetString ( UnityEngine.Windows.File.ReadAllBytes ( filePath ) );
            #else
                    data = File.ReadAllText(filePath, encoding);
            #endif
                }
                else
                {
                    data = PlayerPrefs.GetString(filePath);
                }
            #else
				data = PlayerPrefs.GetString ( filePath );
            #endif
                string decoded = encoder.Decode(data, encryptionPassword);
                stream = new MemoryStream(System.Convert.FromBase64String(decoded), true);
            }
            else
            {
            #if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
                if (Utils.IsIOSupported())
                {
            #if UNITY_WSA || UNITY_WINRT
					stream = new MemoryStream ( UnityEngine.Windows.File.ReadAllBytes ( filePath ) );
            #else
                    stream = File.OpenRead(filePath);
            #endif
                }
                else
                {
                    string data = PlayerPrefs.GetString(filePath);
                    stream = new MemoryStream(encoding.GetBytes(data));
                }
            #else
				string data = PlayerPrefs.GetString ( filePath );
				stream = new MemoryStream ( encoding.GetBytes ( data ) );
            #endif
            }
            result = serializer.Deserialize<T>(stream, encoding);
            stream.Dispose();
            if (result == null)
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Checks whether the specified identifier exists or not.
        /// </summary>
        /// <param name="filename">Identifier of the file to check. Can route to a folder relative to the Application.persistentDataPath.</param>
        public static bool Exists(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new System.ArgumentNullException("filename");
            }
            string filePath = GetFilePath(filename);
            
            #if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
            if (Utils.IsIOSupported())
            {
                bool exists = false;
            #if UNITY_WSA || UNITY_WINRT
				exists = UnityEngine.Windows.Directory.Exists ( filePath );
            #else
                exists = Directory.Exists(filePath);
            #endif
                if (!exists)
                {
            #if UNITY_WSA || UNITY_WINRT
					exists = UnityEngine.Windows.File.Exists ( filePath );
            #else
                    exists = File.Exists(filePath);
            #endif
                }
                return exists;
            }
            else
            {
                return PlayerPrefs.HasKey(filePath);
            }
            #else
			return PlayerPrefs.HasKey ( filePath );
            #endif
        }

        private static string GetFilePath(string filename)
        {

            return $"{saveDataPath}/{filename.Trim('/')}.json";
        }

        /// <summary>
        /// Delete the specified identifier and path.
        /// </summary>
        /// <param name="filename">Identifier of the file to delete. Can route to a folder relative to the Application.persistentDataPath.</param>
        public static void Delete(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new System.ArgumentNullException(nameof(filename));
            }
            string filePath = GetFilePath(filename);
            
#if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
            if (Utils.IsIOSupported())
            {
#if UNITY_WSA || UNITY_WINRT
				UnityEngine.Windows.File.Delete ( filePath );
#else
                File.Delete(filePath);
#endif
            }
            else
            {
                PlayerPrefs.DeleteKey(filePath);
            }
#else
			PlayerPrefs.DeleteKey ( filePath );
#endif
        }
        
        
        /// <summary>
        /// Deletes all.
        /// <para>Be aware, it will delete all files found in the '</para>>
        /// </summary>
        public static void DeleteAll()
        {
            string dirPath = saveDataPath;

#if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
            if (Utils.IsIOSupported())
            {
#if UNITY_WSA || UNITY_WINRT
				UnityEngine.Windows.Directory.Delete ( dirPath );
#else
                DirectoryInfo info = new DirectoryInfo(dirPath);
                FileInfo[] files = info.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    files[i].Delete();
                }
                DirectoryInfo[] dirs = info.GetDirectories();
                for (int i = 0; i < dirs.Length; i++)
                {
                    dirs[i].Delete(true);
                }
#endif
            }
            else
            {
                PlayerPrefs.DeleteAll();
            }
#else
			PlayerPrefs.DeleteAll ();
#endif
        }
        
    }

}
