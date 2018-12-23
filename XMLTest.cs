/*
* Displays information about the state entered by the user.
* Uses XPath to search through statetax.xml.
* Test file for P21 business rule.
* By Paul Shannon on January 20th, 2016.
*/

using System;
using System.Xml;

namespace XMLTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Please enter the abbreviation for the state: ");
			string state = Console.ReadLine().ToUpper();
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load("statetax.xml");
				XmlElement root = xmlDoc.DocumentElement;
				XmlNamespaceManager xmlNameSpace = new XmlNamespaceManager(xmlDoc.NameTable);
				xmlNameSpace.AddNamespace("states", root.NamespaceURI);
				string customerClass = root.SelectSingleNode("//state[stateabbrv=\""+state+"\"]/customerclass", xmlNameSpace).InnerText;
				string jurisdictionId = root.SelectSingleNode("//state[stateabbrv=\"" + state + "\"]/jurisdiction_id", xmlNameSpace).InnerText;
				string jurisdictionDesc = root.SelectSingleNode("//state[stateabbrv=\"" + state + "\"]/jurisdiction_desc", xmlNameSpace).InnerText;
				Console.WriteLine("STATE: " + state);
				Console.WriteLine("CUSTOMER CLASS: " + customerClass);
				Console.WriteLine("JURISDICTION ID: " + jurisdictionId);
				Console.WriteLine("JURISDICTION DESC: " + jurisdictionDesc);
			}
			catch (NullReferenceException ex) {
				Console.WriteLine("Whoops.");
				Console.WriteLine(" ");
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.Data);
				Console.WriteLine(ex.StackTrace);
			}
			Console.ReadKey();
		}
	}
}