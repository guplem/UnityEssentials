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


using System;
using System.IO;
using FullSerializer;
using System.Text;
using UnityEngine;

namespace Essentials.SaveData
{
	/// <summary>
	/// Json Serializer used by the SaveData class.
	/// </summary>
	public class SD_JsonSerializer
	{

		/// <summary>
		/// Serialize the specified object to stream with encoding.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="stream">Stream.</param>
		/// <param name="encoding">Encoding.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void Serialize<T>(T obj, Stream stream, Encoding encoding)
		{
			#if !UNITY_WSA || !UNITY_WINRT
			try
			{
				StreamWriter writer = new StreamWriter(stream, encoding);
				fsSerializer serializer = new fsSerializer();
				fsData data = new fsData();
				serializer.TrySerialize(obj, out data);
				writer.Write(fsJsonPrinter.CompressedJson(data));
				writer.Dispose();
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			#else
			StreamWriter writer = new StreamWriter ( stream, encoding );
			writer.Write ( JsonUtility.ToJson ( obj ) );
			writer.Dispose ();
			#endif
		}

		/// <summary>
		/// Deserialize the specified object from stream using the encoding.
		/// </summary>
		/// <param name="stream">Stream.</param>
		/// <param name="encoding">Encoding.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Deserialize<T>(Stream stream, Encoding encoding)
		{
			T result = default(T);
			#if !UNITY_WSA || !UNITY_WINRT
			try
			{
				StreamReader reader = new StreamReader(stream, encoding);
				fsSerializer serializer = new fsSerializer();
				fsData data = fsJsonParser.Parse(reader.ReadToEnd());
				serializer.TryDeserialize(data, ref result);
				if (result == null)
				{
					result = default(T);
				}
				reader.Dispose();
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			#else
			StreamReader reader = new StreamReader ( stream, encoding );
			result = JsonUtility.FromJson<T> ( reader.ReadToEnd () );
			reader.Dispose ();
			#endif
			return result;
		}

	}

}