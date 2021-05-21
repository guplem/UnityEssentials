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
    public static class SavedDataManager
    {
        public static string savedDataPath = Application.persistentDataPath;

        /// <summary>
        /// Opens/reveals the folder containing the saved data.
        /// </summary>
        [MenuItem("TESTS/Open data folder")]
        public static void OpenSavedDataFolder()
        {
            EditorUtility.RevealInFinder(savedDataPath);
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
            string filePath = $"{Application.persistentDataPath}/{filename.Trim('/')}.json";
            
            // Build IO stream
            Stream stream = null;
            #if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
            #if UNITY_WSA || UNITY_WINRT
			UnityEngine.Windows.Directory.CreateDirectory ( filePath );
            #else
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
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
        
        /// <summary>
        /// Loads data using identifier.
        /// </summary>
        /// <param name="filename">Identifier of the file containing the data. Can route to a folder relative to the Application.persistentDataPath.</param>
        /// <param name="defaultValue">Default Value. Used in case the saved data is not found.</param>
        /// <param name="encryptionPassword">Encryption Password (set it to the same password you used to save it).</param>
        /// <param name="encoding">Encoding.</param>
        /// <typeparam name="T">The objectToSave's type.</typeparam>
        public static T Load<T>(string filename, T defaultValue, string encryptionPassword = null, Encoding encoding = null)
        {
            // Setup
            SD_JsonSerializer serializer = new SD_JsonSerializer();
            SD_Encoder encoder = new SD_Encoder();
            encoding ??= Encoding.UTF8;
            defaultValue ??= default(T);
            if (string.IsNullOrEmpty(filename))
                throw new System.ArgumentNullException(nameof(filename));
            string filePath = $"{Application.persistentDataPath}/{filename.Trim('/')}.json";
            T result = defaultValue;
            
            #if !UNITY_SAMSUNGTV && !UNITY_TVOS && !UNITY_WEBGL
            if (!Exists(filePath))
            #else
			if ( !Exists ( filePath, path ) )
            #endif
            {
                Debug.LogWarningFormat(
                    "The specified identifier ({1}) does not exists. please use Exists () to check for existent before calling Load.\n" +
                    "returning the default(T) instance.",
                    filePath,
                    filename);
                return result;
            }
            Stream stream = null;
            if (!encryptionPassword.IsNullEmptyOrWhiteSpace())
            {
                string data = "";
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
        /// <param name="identifier">Identifier.</param>
        public static bool Exists(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new System.ArgumentNullException("identifier");
            }
            string filePath = "";
            if (!Utils.IsFilePath(identifier))
            {
                filePath = string.Format("{0}/{1}", Application.persistentDataPath, identifier);
            }
            else
            {
                filePath = identifier;
            }
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


    }

}
