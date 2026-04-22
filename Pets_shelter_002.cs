
using System.Collections;
using System.Threading;


string animalSpecies = "";
string animalID = "";
string animalAge = "";
string animalPhysicalDescription = "";
string animalPersonalityDescription = "";
string animalNickname = "";
decimal animalDonation = 0.0M;

// variables that support data entry
int maxPets = 8;
string? readResult;
string menuSelection = "";

// array used to store runtime data, there is no persisted data
string[,] ourAnimals = new string[maxPets, 7];

string[] icons = { "|", "/", "-", "\\" };

// TODO: Convert the if-elseif-else construct to a switch statement

// create some initial ourAnimals array entries
for (int i = 0; i < maxPets; i++)
{
    switch (i)
    {
        case 0:
            animalSpecies = "dog";
            animalID = "d1";
            animalAge = "2";
            animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 65 pounds. housebroken.";
            animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
            animalNickname = "lola";
            animalDonation = 0m;
            break;

        case 1:

            animalSpecies = "dog";
            animalID = "d2";
            animalAge = "9";
            animalPhysicalDescription = "medium reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
            animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
            animalNickname = "loki";
            animalDonation = 25m;
            break;

        case 2:

            animalSpecies = "cat";
            animalID = "c3";
            animalAge = "1";
            animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
            animalPersonalityDescription = "friendly";
            animalNickname = "Puss";
            animalDonation = 45.6m;
            break;

        case 3:

            animalSpecies = "cat";
            animalID = "c4";
            animalAge = "?";
            animalPhysicalDescription = "white";
            animalPersonalityDescription = "";
            animalNickname = "";
            animalDonation = 0.0m;
            break;

        default:

            animalSpecies = "";
            animalID = "";
            animalAge = "";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "";
            animalDonation = 0.0m;
            break;
    }

    ourAnimals[i, 0] = "ID #: " + animalID;
    ourAnimals[i, 1] = "Species: " + animalSpecies;
    ourAnimals[i, 2] = "Age: " + animalAge;
    ourAnimals[i, 3] = "Nickname: " + animalNickname;
    ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
    ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;
    ourAnimals[i, 6] = "Donation: " + animalDonation;
}


do
{
    // display the top-level menu options

    Console.Clear();

    Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
    Console.WriteLine(" 1. List all of our current pet information");
    Console.WriteLine(" 2. Add a new animal friend to the ourAnimals array");
    Console.WriteLine(" 3. Ensure animal ages and physical descriptions are complete");
    Console.WriteLine(" 4. Ensure animal nicknames and personality descriptions are complete");
    Console.WriteLine(" 5. Edit an animal’s age");
    Console.WriteLine(" 6. Edit an animal’s personality description");
    Console.WriteLine(" 7. Display all cats with a specified characteristic");
    Console.WriteLine(" 8. Display all dogs with a specified characteristic");
    Console.WriteLine(" 9. Donate for anymal ");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

    readResult = Console.ReadLine();
    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }

    // Console.WriteLine($"You selected menu option {menuSelection}.");
    // Console.WriteLine("Press the Enter key to continue");

    // // pause code execution
    // readResult = Console.ReadLine();


    switch (menuSelection)
    {
        case "1":
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: ")
                {
                    Console.WriteLine();
                    for (int j = 0; j < 7; j++)
                    {
                        Console.WriteLine(ourAnimals[i, j]);
                    }
                }
            }
            Console.WriteLine("Press Enter to continue");
            readResult = Console.ReadLine();
            break;

        case "2":
            string anotherPet = "y";
            int petCount = 0;

            // Рахуємо існуючих
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: " && ourAnimals[i, 0] != null && ourAnimals[i, 0] != "")
                {
                    petCount += 1;
                }
            }

            // Якщо є місце, починаємо цикл додавання
            while (anotherPet == "y" && petCount < maxPets)
            {
                bool validEntry = false;

                // --- 1. ВИД ТВАРИНИ ---
                do
                {
                    Console.WriteLine("\n\rEnter 'dog' or 'cat' to begin a new entry");
                    readResult = Console.ReadLine();
                    if (readResult != null)
                    {
                        animalSpecies = readResult.ToLower().Trim();
                        if (animalSpecies == "dog" || animalSpecies == "cat") validEntry = true;
                    }
                } while (validEntry == false);

                // --- 2. ГЕНЕРУЄМО ID ---
                animalID = animalSpecies.Substring(0, 1) + (petCount + 1).ToString();

                // --- 3. ВІК ---
                validEntry = false;
                do
                {
                    Console.WriteLine("Enter the pet's age or enter ? if unknown");
                    readResult = Console.ReadLine();
                    if (readResult != null)
                    {
                        animalAge = readResult.Trim();
                        if (animalAge == "?") validEntry = true;
                        else validEntry = int.TryParse(animalAge, out _);
                    }
                } while (validEntry == false);

                // --- 4. ОПИС ТА ПРІЗВИЩЕ (спростимо для прикладу) ---
                Console.WriteLine("Enter a nickname (or ENTER if unknown)");
                readResult = Console.ReadLine();
                animalNickname = (readResult == null || readResult == "") ? "tbd" : readResult;

                // --- КРИТИЧНИЙ КРОК: ЗАПИСУЄМО ВСЕ В МАСИВ ---
                ourAnimals[petCount, 0] = "ID #: " + animalID;
                ourAnimals[petCount, 1] = "Species: " + animalSpecies;
                ourAnimals[petCount, 2] = "Age: " + animalAge;
                ourAnimals[petCount, 3] = "Nickname: " + animalNickname;
                ourAnimals[petCount, 4] = "Physical description: tbd" + animalPhysicalDescription; // тут додай свій ввід
                ourAnimals[petCount, 5] = "Personality: tbd" + animalPersonalityDescription;

                petCount++; // Переходимо до наступного рядка таблиці

                // Питаємо, чи хоче ще
                if (petCount < maxPets)
                {
                    Console.WriteLine("Do you want to enter info for another pet (y/n)");
                    do
                    {
                        readResult = Console.ReadLine();
                        anotherPet = readResult?.ToLower() ?? "n";
                    } while (anotherPet != "y" && anotherPet != "n");
                }
            }

            if (petCount >= maxPets) Console.WriteLine("The animal shelter is full.");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            break;

        case "3":
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: " && ourAnimals[i, 0] != null && ourAnimals[i, 0] != "")
                {
                    if (ourAnimals[i, 2] == "Age: ?" || ourAnimals[i, 2] == "Age: ")
                    {
                        bool validEntry = false;
                        do
                        {
                            Console.WriteLine($"Enter an age for {ourAnimals[i, 0]} ({ourAnimals[i, 3]})");
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                animalAge = readResult;
                                validEntry = int.TryParse(animalAge, out _);
                            }
                        } while (validEntry == false);

                        ourAnimals[i, 2] = "Age: " + animalAge;
                    }

                    if (ourAnimals[i, 4] == "Physical description: " || ourAnimals[i, 4] == "Physical description: tbd" || ourAnimals[i, 4] == "")
                    {
                        do
                        {
                            Console.WriteLine($"Enter a physical description for {ourAnimals[i, 0]} ({ourAnimals[i, 3]})");
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                animalPhysicalDescription = readResult.Trim();

                            }

                        } while (animalPhysicalDescription == "");

                        ourAnimals[i, 4] = "physical description: " + animalPhysicalDescription;
                    }
                }

            }


            // Console.WriteLine("Challenge Project - please check back soon to see progress.");
            // Console.WriteLine("Press the Enter key to continue.");
            // readResult = Console.ReadLine();

            Console.WriteLine("\nAge and physical description fields are complete for all of our friends. \nPress the Enter key to continue");
            readResult = Console.ReadLine();
            break;

        case "4":
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: " && ourAnimals[i, 0] != null && ourAnimals[i, 0] != "")
                {
                    if (ourAnimals[i, 3] == "Nickname: " || ourAnimals[i, 3] == "Nickname: tbd" || ourAnimals[i, 3] == "Nickname: ?" || ourAnimals[i, 3] == "")
                    {
                        do
                        {
                            Console.WriteLine($"ENter a nicname for this {ourAnimals[i, 0]} ({ourAnimals[i, 1]})");
                            readResult = Console.ReadLine();
                            animalNickname = readResult?.Trim() ?? "";
                        } while (animalNickname == "");

                        ourAnimals[i, 3] = "Nickname: " + animalNickname;

                    }
                    if (ourAnimals[i, 5] == "Personality: " || ourAnimals[i, 5] == "Personality: tbd" || ourAnimals[i, 5] == "Personality: ?" || ourAnimals[i, 5] == "")
                    {
                        do
                        {
                            Console.WriteLine($"Enter a personality for this {ourAnimals[i, 0]} ({ourAnimals[i, 1]})");
                            readResult = Console.ReadLine();
                            animalPersonalityDescription = readResult?.Trim() ?? "";
                        } while (animalPersonalityDescription == "");

                        ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;
                    }
                }
            }

            // Console.WriteLine("Challenge Project - please check back soon to see progress.");
            // Console.WriteLine("Press the Enter key to continue.");
            // readResult = Console.ReadLine();
            Console.WriteLine("\nNickname and personality description fields are complete for all of our friends. \nPress the Enter key to continue");
            readResult = Console.ReadLine();
            break;

        case "5":
            {

                string? petID = "";
                bool petFound = false;
                Console.WriteLine("Enter a pet id u want to update");
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    petID = readResult.ToLower().Trim();

                }

                for (int i = 0; i < maxPets; i++)
                {
                    if (ourAnimals[i, 0].Contains(petID) && petID != "")
                    {
                        petFound = true;
                        Console.WriteLine($"Found pet as {ourAnimals[i, 3]} for id is {ourAnimals[i, 0]}");
                        bool validEntry = false;
                        string newAge = "";

                        do
                        {
                            Console.WriteLine("Enter a new age - ");
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                newAge = readResult.Trim();
                                validEntry = int.TryParse(newAge, out _);

                            }
                        } while (validEntry == false);

                        ourAnimals[i, 2] = "Age: " + newAge;
                        Console.WriteLine("Age has been updated successfylly");

                        break;
                    }
                }


                if (!petFound)
                {
                    Console.WriteLine($"Pet with id - {petID} not found");
                }

                Console.WriteLine("Press the Enter key to continue.");
                readResult = Console.ReadLine();

                // Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
                // Console.WriteLine("Press the Enter key to continue.");
                // readResult = Console.ReadLine();

                break;
            }
        case "6":
            {

                string? petID = "";
                bool petFound = false;

                Console.WriteLine("ENter pet id -");
                readResult = Console.ReadLine();

                if (readResult != null)
                {
                    petID = readResult.ToLower().Trim();
                }
                for (int i = 0; i < maxPets; i++)
                {
                    if (ourAnimals[i, 0].Contains(petID) && petID != "")
                    {
                        petFound = true;
                        Console.WriteLine($"Found a pet is {ourAnimals[i, 3]} id - {ourAnimals[i, 0]}");
                        string newPersonality = "";
                        do
                        {
                            Console.WriteLine("Enter a new personality - ");
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                newPersonality = readResult;
                            }
                        } while (newPersonality == "");

                        ourAnimals[i, 5] = "Personality: " + newPersonality;
                        Console.WriteLine("New personality has been updated");

                        break;

                    }
                }

                if (!petFound)
                {
                    Console.WriteLine($"PEt with id {petID} not found");
                }
                Console.WriteLine("Press the Enter key to continue");
                readResult = Console.ReadLine();
                // Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
                // Console.WriteLine("Press the Enter key to continue.");
                // readResult = Console.ReadLine();
                break;
            }
        case "7":
            {
                Console.WriteLine("ENter a pet characteristic (shy, fat, active)");
                string? petCharacteristick = Console.ReadLine()?.Trim().ToLower();
                bool petFound = false;

                if (petCharacteristick != "")
                {
                    for (int i = 0; i < maxPets; i++)
                    {
                        if (ourAnimals[i, 1].Contains("cat"))
                        {
                            if (ourAnimals[i, 4].Contains(petCharacteristick!) || ourAnimals[i, 5].Contains(petCharacteristick!))
                            {
                                petFound = true;

                                Console.WriteLine($"Found cat ({ourAnimals[i, 3]}) id - ({ourAnimals[i, 0]})");
                                Console.WriteLine($"{ourAnimals[i, 4]}");
                                Console.WriteLine($"{ourAnimals[i, 5]}");
                                Console.WriteLine("---------------------------");
                            }
                        }
                    }
                }
                if (!petFound)
                {
                    Console.WriteLine($"cat with {petCharacteristick} not found");
                }
                Console.WriteLine("Press the Enter key to continue");
                readResult = Console.ReadLine();

                // Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
                // Console.WriteLine("Press the Enter key to continue.");
                // readResult = Console.ReadLine();
                break;
            }
        case "8":
            {
                Console.WriteLine("ENter a pet characteristic (shy, fat, active)");
                string? petCharacteristick = Console.ReadLine()?.Trim().ToLower();
                bool petFound = false;

                if (petCharacteristick != "")
                {
                    for (int i = 0; i < maxPets; i++)
                    {
                        if (ourAnimals[i, 1].Contains("dog"))
                        {
                            if (ourAnimals[i, 4].Contains(petCharacteristick!) || ourAnimals[i, 5].Contains(petCharacteristick!))
                            {
                                petFound = true;

                                Console.WriteLine($"Found dog ({ourAnimals[i, 3]}) id - ({ourAnimals[i, 0]})");
                                Console.WriteLine($"{ourAnimals[i, 4]}");
                                Console.WriteLine($"{ourAnimals[i, 5]}");
                                Console.WriteLine("---------------------------");
                            }
                        }
                    }
                }
                if (!petFound)
                {
                    Console.WriteLine($"cat with {petCharacteristick} not found");
                }
                Console.WriteLine("Press the Enter key to continue");
                readResult = Console.ReadLine();
                break;
            }
        case "9":
            {
                Console.WriteLine("Search by: 1 - Nickname, 2 - ID");
                string? searchType = Console.ReadLine();

                Console.WriteLine("Enter search term:");
                string? searchTerm = Console.ReadLine();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Console.Write("Searching... ");
                    for (int j = 0; j < 5; j++)
                    {
                        foreach (string icon in icons)
                        {
                            Console.Write($"\rSearching... {icon}");
                            Thread.Sleep(100);
                        }
                    }
                    Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");

                    bool petFound = false;
                    for (int i = 0; i < maxPets; i++)
                    {
                        if (ourAnimals[i, 0] != null && ourAnimals[i, 0] != "ID #: ")
                        {
                            bool match = false;
                            if (searchType == "1")
                                match = ourAnimals[i, 3].Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                            else if (searchType == "2")
                                match = ourAnimals[i, 0].Contains(searchTerm, StringComparison.OrdinalIgnoreCase);

                            if (match)
                            {
                                petFound = true;
                                Console.WriteLine($"Found: {ourAnimals[i, 3]} (ID: {ourAnimals[i, 0]})");

                                decimal newDonation = 0;
                                bool validEntry = false;
                                do
                                {
                                    Console.WriteLine("Enter a donation amount (e.g., 12.50):");
                                    string? donationInput = Console.ReadLine();
                                    if (decimal.TryParse(donationInput, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out newDonation))
                                    {
                                        validEntry = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid amount.");
                                    }
                                } while (!validEntry);

                                ourAnimals[i, 6] = String.Format("{0:C2}", newDonation);
                                Console.WriteLine("Donation saved successfully!");
                            }
                        }
                    }
                    if (!petFound) Console.WriteLine($"No pet found matching \"{searchTerm}\"");
                }
                Console.WriteLine("\nPress the Enter key to continue");
                Console.ReadLine();
                break;
            } 

        default:
            break;
    } 

} while (menuSelection != "exit");