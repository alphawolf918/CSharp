#define DEBUG
using System;
using System.Xml.Serialization;
using System.IO;
using static System.ConsoleKey;
using static System.Environment;

namespace Game1
{
	[Serializable()]
	public struct Character
	{
		public string name;
		public string mainClass;
		public string subClass;
		public string race;
		public int atk;
		public int def;
		public int xp;
		public int lvl;
	}

	class Program
	{
		public static Character player;
		static SpecialFolder myDocuments = SpecialFolder.MyDocuments;

		static void Main(string[] args)
		{
			gameStart();
			ConsoleKeyInfo keyInfo;
			while ((keyInfo = Console.ReadKey(true)).Key != Escape)
			{
				//do things
			}
		}

		static void gameStart()
		{
			writeLine("Welcome. Please type a number from the list below.");
			dividerLine();
			writeLine("1: Create Character");
			writeLine("2: Load Character");
			dividerLine();
			writeLine("Press ESC to quit.");
			ConsoleKey keyPressed = Console.ReadKey(true).Key;
			if (keyPressed.Equals(D1))
			{
				createCharacter();
			}
			else if (keyPressed.Equals(D2))
			{
				loadCharacter();
			}
			else
			{
				writeLine("Unrecognized input.");
				gameStart();
			}
		}

		#region
		/**
		<summary>
		Character creation happens here.
		</summary>
		**/
		static void createCharacter()
		{
			writeLine("Character Creation");
			dividerLine();
			writeLine("What would you like to name your character?");
			dividerLine(true);
			player = new Character();
			string playerName = Console.ReadLine();
			player.name = playerName;
			dividerLine(true);
			writeLine("Your character will be called " + playerName);
			createCharacterRace();
		}

		static void createCharacterRace()
		{
			writeLine("What race would you like for your character?");
			dividerLine(true);
			dividerLine();
			writeLine("1: Human");
			writeLine("2: Kree");
			writeLine("3: Inhuman");
			writeLine("4: Awoken");
			writeLine("5: Exo");
			dividerLine();
			string playerRace;
			ConsoleKey keyPressed = Console.ReadKey(true).Key;
			if (keyPressed.Equals(D1))
			{
				playerRace = "Human";
			}
			else if (keyPressed.Equals(D2))
			{
				playerRace = "Kree";
			}
			else if (keyPressed.Equals(D3))
			{
				playerRace = "Inhuman";
			}
			else if (keyPressed.Equals(D4))
			{
				playerRace = "Awoken";
			}
			else if (keyPressed.Equals(D5))
			{
				playerRace = "Exo";
			}
			else
			{
				playerRace = "";
				writeLine("Unrecognized input.");
				dividerLine(true);
				createCharacterRace();
			}
			player.race = playerRace;
			writeLine("Your character's race has been chosen as: " + playerRace);
			createCharacterClass();
		}

		static void createCharacterClass()
		{
			writeLine("What will be your character's class? Choose one from the options below.");
			dividerLine();
			writeLine("1: Hunter");
			writeLine("2: Titan");
			writeLine("3: Warlock");
			dividerLine();
			string playerClass;
			ConsoleKey keyPressed = Console.ReadKey(true).Key;
			if (keyPressed.Equals(D1))
			{
				playerClass = "Hunter";
			}
			else if (keyPressed.Equals(D2))
			{
				playerClass = "Titan";
			}
			else if (keyPressed.Equals(D3))
			{
				playerClass = "Warlock";
			}
			else
			{
				playerClass = "";
				writeLine("Unrecognized input.");
				createCharacterClass();
			}
			player.mainClass = playerClass;
			writeLine("Your class was chosen as: " + playerClass);
			saveCharacter();
			gameStart();
		}
		#endregion

		static void writeLine(string line)
		{
			Console.WriteLine(line);
		}

		#region
		/**
		<summary>Loading and saving happens here.</summary>
			**/
		static void loadCharacter()
		{
			writeLine("Load Character");
			writeLine("Please type your character's name, and then press Enter:");
			string characterName = Console.ReadLine();
			XmlSerializer reader = new XmlSerializer(typeof(Character));
			var folderPath = GetFolderPath(myDocuments);
			string fullPath = folderPath + "//" + characterName + ".xml";
			if (!File.Exists(fullPath))
			{
				writeLine("Error: This character does not exist or its data cannot be found.");
				loadCharacter();
			}
			dividerLine(true);
			writeLine("Loading character..");
			StreamReader file = new StreamReader(fullPath);
			player = (Character)reader.Deserialize(file);
			file.Close();
			dividerLine(true);
			writeLine(player.name + " loaded. Character data:");
			writeLine("Name: " + player.name);
			writeLine("Main Class: " + player.mainClass);
			writeLine("Level: "+player.lvl);
			writeLine("Attack: " + player.atk);
			writeLine("Defense: " + player.def);
			writeLine("XP: " + player.xp);
			dividerLine(true);
			//gameStart();
		}

		static void saveCharacter()
		{
			writeLine("Saving Character..");
			XmlSerializer writer = new XmlSerializer(typeof(Character));
			var path = GetFolderPath(myDocuments) + "//" + player.name + ".xml";
			FileStream file = File.Create(path);
			writer.Serialize(file, player);
			file.Close();
			writeLine("Character data saved to " + player.name + ".xml in your My Documents folder.");
		}
		#endregion

		static void dividerLine(bool empty = false)
		{
			if (!empty)
				writeLine("-------------------");
			else
				writeLine(" ");
		}
	}
}
