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
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "load")
                {
                    if(argument.Length == 2)
                    {
                        using (StreamReader sr = new StreamReader(argument[1])) // FIXME: System.IO.FileNotFoundException, kolla sökväg
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                }
                else if (command == "list")
                {
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if(argument.Length == 1) // FIXME: System.NullReferenceException när inge lista laddats
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedish = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string english = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(swedish, english));
                    }

                    // FIXME: Meddela om och varför inget lades till vid t ex endast 1 argument
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        { //FIXME: System.ArgumentOutOfRangeException vid ett felstavat argument
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedish = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string english = Console.ReadLine();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == swedish && gloss.word_eng == english)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    // FIXME: meddela varför inget tas bort vid t ex bara ett argument
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string swedish = Console.ReadLine();
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == swedish)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == swedish)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true); // FIXME: tills command = "quit"
        }
    }
}