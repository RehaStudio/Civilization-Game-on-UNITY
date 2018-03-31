using System.Xml.Serialization;
using System.IO;

public static class DeSerialize  {

	// Use this for initialization
	public static string Serialize<T>(this T toSerialize)
	{
		XmlSerializer xml = new XmlSerializer (typeof(T));
		StringWriter sw = new StringWriter ();
		xml.Serialize (sw, toSerialize);
		return sw.ToString ();
	}
	public static T Deserialize<T>(this string toDeserialize)
	{
		XmlSerializer xml = new XmlSerializer (typeof(T));
		StringReader sr = new StringReader (toDeserialize);
		return (T)xml.Deserialize(sr);
	}
}
