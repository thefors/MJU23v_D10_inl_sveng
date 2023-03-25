﻿namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "sweeng.lis"; // default filename without path
            string defaultRoot = "..\\..\\..\\dict\\"; // default file root to help refactored LoadFile method
            Console.WriteLine("Welcome to the dictionary app!");
            string command;
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "load")
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    if (argument.Length == 2)
                    {
                        LoadFile(defaultRoot, argument[1]);
                    }
                    else if(argument.Length == 1)
                    {
                        LoadFile(defaultRoot, defaultFile);
                    }
                }
                else if (command == "list")
                {
                    ListDictionary();
                }
                else if (command == "new")
                {
                    NewEntry(argument);
                }
                else if (command == "delete")
                {
                    DeleteEntry(argument);
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        LookUp(argument[1]);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string word = Console.ReadLine();
                        LookUp(word);
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (command != "quit"); // FIXME: tills command = "quit" - FIXAT
        }

        private static void LookUp(string argument)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == argument)
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == argument)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }

        private static void DeleteEntry(string[] argument)
        {
            if (argument.Length == 3)
            {
                FindAndRemove(argument[1], argument[2]);
            }
            else if (argument.Length == 1)
            {
                Console.WriteLine("Write word in Swedish: ");
                string swedish = Console.ReadLine();
                Console.Write("Write word in English: ");
                string english = Console.ReadLine();
                FindAndRemove(swedish, english);
            }
            // FIXME: meddela varför inget tas bort vid t ex bara ett argument
        }

        private static void FindAndRemove(string swedish, string english)
        {
            //FIXME: System.ArgumentOutOfRangeException vid ett felstavat argument

            int index = -1;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == swedish && gloss.word_eng == english)
                    index = i;
            }
            dictionary.RemoveAt(index);
        }

        private static void NewEntry(string[] argument)
        {
            if (argument.Length == 3)
            {
                dictionary.Add(new SweEngGloss(argument[1], argument[2]));
            }
            else if (argument.Length == 1) // FIXME: System.NullReferenceException när inge lista laddats
            {
                Console.WriteLine("Write word in Swedish: ");
                string swedish = Console.ReadLine();
                Console.Write("Write word in English: ");
                string english = Console.ReadLine();
                dictionary.Add(new SweEngGloss(swedish, english));
            }

            // FIXME: Meddela om och varför inget lades till vid t ex endast 1 argument
        }

        private static void ListDictionary()
        {
            try
            {
                foreach (SweEngGloss gloss in dictionary)
                {
                    Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}"); // FIXME: System.NullReferenceException när inge lista laddats - FIXAT
                }
            } catch (System.NullReferenceException)
            {
                Console.WriteLine("No wordlist in memory. Might not have been loaded.");
            }
        }

        private static void LoadFile(string defaultRoot, string argument)
        {
            try {
                using (StreamReader sr = new StreamReader(defaultRoot + argument)) // FIXME: System.IO.FileNotFoundException, kolla sökväg (try/catch med meddelande) - FIXAT
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
             }catch(System.IO.FileNotFoundException) {
                Console.WriteLine("No such file or path.");
            }
        }
    }
}