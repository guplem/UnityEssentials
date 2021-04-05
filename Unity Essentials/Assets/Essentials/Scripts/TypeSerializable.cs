// Simple helper class that allows you to serialize System.Type objects.
// Written by Bryan Keiren (http://www.bryankeiren.com)
// Found in: https://forum.unity.com/threads/serializable-system-type-get-it-while-its-hot.187557/

using UnityEngine;
using System.Runtime.Serialization;

namespace UnityEngine
{
	[System.Serializable]
    public class TypeSerializable
    {
    	[SerializeField]
    	private string m_Name;
    	public string Name => m_Name;
    
    	[SerializeField]
    	private string m_AssemblyQualifiedName;
    	public string AssemblyQualifiedName => m_AssemblyQualifiedName;
    
    	[SerializeField]
    	private string m_AssemblyName;
    	public string AssemblyName => m_AssemblyName;
    
    	private System.Type m_SystemType;	
    	public System.Type SystemType
    	{
    		get 	
    		{
    			if (m_SystemType == null)	
    			{
    				GetSystemType();
    			}
    			return m_SystemType;
    		}
    	}
    	
    	private void GetSystemType()
    	{
    		m_SystemType = System.Type.GetType(m_AssemblyQualifiedName);
    	}
    	
    	public TypeSerializable( System.Type _SystemType )
    	{
    		m_SystemType = _SystemType;
    		m_Name = _SystemType.Name;
    		m_AssemblyQualifiedName = _SystemType.AssemblyQualifiedName;
    		m_AssemblyName = _SystemType.Assembly.FullName;
    	}
    	
    	public override bool Equals( System.Object obj )
    	{
    		TypeSerializable temp = obj as TypeSerializable;
    		if ((object)temp == null)
    		{
    			return false;
    		}
    		return this.Equals(temp);
    	}
    	
    	public bool Equals( TypeSerializable _Object )
    	{
    		//return m_AssemblyQualifiedName.Equals(_Object.m_AssemblyQualifiedName);
    		return _Object.SystemType.Equals(SystemType);
    	}
    	
    	public static bool operator ==( TypeSerializable a, TypeSerializable b )
    	{
    	    // If both are null, or both are same instance, return true.
    	    if (System.Object.ReferenceEquals(a, b))
    	    {
    	        return true;
    	    }
    	
    	    // If one is null, but not both, return false.
    	    if (((object)a == null) || ((object)b == null))
    	    {
    	        return false;
    	    }
    	
    	    return a.Equals(b);
    	}
    	
    	public static bool operator !=( TypeSerializable a, TypeSerializable b )
    	{
    	    return !(a == b);
    	}
    	
    	public override int GetHashCode()
    	{
    		return SystemType != null ? SystemType.GetHashCode() : 0;
    	}
    	
    }
}
