using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using ClientsRCabuyao;

Client myClient = new Client();
List<Client> listOfClients = new List<Client>();

LoadFileValuesToMemory(listOfClients);

bool loopAgain = true;
while (loopAgain)
{
	try
	{
		DisplayMainMenu();
		string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
		if (mainMenuChoice == "N")
			myClient = NewClient();
		if (mainMenuChoice == "S")
			ShowClientInfo(myClient);

		if (mainMenuChoice == "A")
			AddClientToList(myClient, listOfClients);
		if (mainMenuChoice == "F")
			myClient = FindClientInList(listOfClients);
		if (mainMenuChoice == "R")
			RemoveClientFromList(myClient, listOfClients);
		if (mainMenuChoice == "D")
			DisplayAllClientsInList(listOfClients);
		if (mainMenuChoice == "Q")
		{
			SaveMemoryValuesToFile(listOfClients);
			loopAgain = false;
			throw new Exception("Bye, hope to see you again.");
		}
		if (mainMenuChoice == "E")
		{
			while (true)
			{
				DisplayEditMenu();
				string editMenuChoice = Prompt("\nEnter a Edit Menu Choice: ").ToUpper();
				if (editMenuChoice == "T")
					GetTag(myClient);
				if (editMenuChoice == "N")
					GetName(myClient);
				if (editMenuChoice == "A")
					GetAge(myClient);
				if (editMenuChoice == "W")
					GetWeight(myClient);
				if (editMenuChoice == "R")
					throw new Exception("Returning to Main Menu");
			}
		}
	}
	catch (Exception ex)
	{
		Console.WriteLine($"{ex.Message}");
	}
}

void DisplayMainMenu()
{
	Console.WriteLine("\nMain Menu");
	Console.WriteLine("N) New Pet PartA");
	Console.WriteLine("S) Show Client Info PartA");
	Console.WriteLine("E) Edit Client Info PartA");
	Console.WriteLine("A) Add Client To List PartB");
	Console.WriteLine("F) Find Client In List PartB");
	Console.WriteLine("R) Remove Client From List PartB");
	Console.WriteLine("D) Display all Clients in List PartB");
	Console.WriteLine("Q) Quit");
}

void DisplayEditMenu()
{
	Console.WriteLine("Edit Menu");
	Console.WriteLine("T) Tag");
	Console.WriteLine("N) Name");
	Console.WriteLine("A) Age");
	Console.WriteLine("W) Weight");
	Console.WriteLine("R) Return to Main Menu");
}

void ShowClientInfo(Client client)
{
	if(client == null)
		throw new Exception($"No Client In Memory");
	Console.WriteLine($"\n{client.ToString()}");
}

string Prompt(string prompt)
{
	string myString = "";
	while (true)
	{
		try
		{
		Console.Write(prompt);
		myString = Console.ReadLine().Trim();
		if(string.IsNullOrEmpty(myString))
			throw new Exception($"Empty Input: Please enter something.");
		break;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	return myString;
}

double PromptDoubleBetweenMinMax(String msg, double min, double max)
{
	double num = 0;
	while (true)
	{
		try
		{
			Console.Write($"{msg} between {min} and {max} inclusive: ");
			num = double.Parse(Console.ReadLine());
			if (num < min || num > max)
				throw new Exception($"Must be between {min:n2} and {max:n2}");
			break;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Invalid: {ex.Message}");
		}
	}
	return num;
}

Client NewClient()
{
	Client myClient = new Client();
	GetTag(myClient);
	GetName(myClient);
	GetAge(myClient);
	GetWeight(myClient);
	return myClient;
}

void GetTag(Client client)
{
	string myString = Prompt($"Enter Tag: ");
	client.Tag = myString;
}

void GetName(Client client)
{
	string myString = Prompt($"Enter Name: ");
	client.Name = myString;
}

void GetAge(Client client)
{
	double myDouble = PromptDoubleBetweenMinMax("Enter Height in inches: ", 0, 25);
	client.Age = myDouble;
}

void GetWeight(Client client)
{
	double myDouble = PromptDoubleBetweenMinMax("Enter Weight in pounds: ", 0, 200);
	client.Weight = myDouble;
}


void AddClientToList(Client myClient, List<Client> listOfClients)
{
	if(myClient == null)
		throw new Exception($"No Pet provided to add to list");
	listOfClients.Add(myClient);
	Console.WriteLine($"Client Added");
}

Client FindClientInList(List<Client> listOfClients)
{
	string myString = Prompt($"Enter Partial Client Name: ");
	foreach(Client client in listOfClients)
		if(client.Name.Contains(myString))
			return client;
	Console.WriteLine($"No Clients Match");
	return null;
}

void RemoveClientFromList(Client myClient, List<Client> listOfClients)
{
	if(myClient == null)
		throw new Exception($"No Client provided to remove from list");
	listOfClients.Remove(myClient);
	Console.WriteLine($"Client Removed");
}

void DisplayAllClientsInList(List<Client> listOfClients)
{
	foreach(Client client in listOfClients)
		ShowClientInfo(client);
}

void LoadFileValuesToMemory(List<Client> listOfClients)
{
	while(true){
		try
		{
			string fileName = "regin.csv";
			string filePath = $"./data/{fileName}";
			if (!File.Exists(filePath))
				throw new Exception($"The file {fileName} does not exist.");
			string[] csvFileInput = File.ReadAllLines(filePath);
			for(int i = 0; i < csvFileInput.Length; i++)
			{
				string[] items = csvFileInput[i].Split(',');
				for(int j = 0; j < items.Length; j++)
				{
					//Console.WriteLine($"itemIndex: {j}; item: {items[j]}");
				}
				Client myClient = new Client(items[0], items[1], double.Parse(items[2]), double.Parse(items[3]), items[4]);
				listOfClients.Add(myClient);
			}
			Console.WriteLine($"Load complete. {fileName} has {listOfClients.Count} data entries");
			break;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Message}");
		}
	}
}

void SaveMemoryValuesToFile(List<Client> listOfClients)
{
	string fileName = "regout.csv";
	string filePath = $"./data/{fileName}";
	string[] csvLines = new string[listOfClients.Count];
	for (int i = 0; i < listOfClients.Count; i++)
	{
		csvLines[i] = listOfClients[i].ToString();
	}
	File.WriteAllLines(filePath, csvLines);
	Console.WriteLine($"Save complete. {fileName} has {listOfClients.Count} entries.");
}