using System.Numerics;

namespace Lab_4
{
    internal class Program
    {
        static List<Planet> planets = new List<Planet>();
        static int maxPlanets = 0;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            maxPlanets = InputHelper.GetIntValue(
                "How many planets max do you want to create? ",
                "Enter a number >= 1.",
                1, int.MaxValue
            );

            int menu = 0;

            do
            {
                Console.WriteLine("\n1. Add object");
                Console.WriteLine("2. View all objects");
                Console.WriteLine("3. Find object");
                Console.WriteLine("4. Demonstrate behavior");
                Console.WriteLine("5. Delete object");
                Console.WriteLine("6. Demonstrate static methods");
                Console.WriteLine("0. Exit program\n");

                menu = InputHelper.GetIntValue("Choose your option: ", "Invalid choice.", 0, 6);

                switch (menu)
                {
                    case 1: AddObjectMenu(); break;
                    case 2: ShowPlanetsTable(); break;
                    case 3: FindObjectMenu(); break;
                    case 4: DemonstrateBehaviorMenu(); break;
                    case 5: DeleteObjectMenu(); break;
                    case 6: DemonstrateStaticMethodsMenu(); break;
                    case 0: Console.WriteLine("Exiting. Goodbye!"); break;
                }

            } while (menu != 0);
        }
        //menu methods
        private static void DemonstrateStaticMethodsMenu()
        {
            string galaxy = InputHelper.GetStringValue("Input galaxy: ", "invalid", s => s.Length != 0);
            Planet.Galaxy = galaxy;
            Console.WriteLine("\n--- Demonstrating static methods ---");
            Console.WriteLine(Planet.ShowUniverseInfo());

            if (planets.Count == 0)
            {
                Console.WriteLine("No planets to display.");
                return;
            }

            Console.WriteLine("\nPlanets (ToString format for parsing):");
            foreach (var p in planets)
            {
                Console.WriteLine(p.ToString());
            }
        }

        private static void AddObjectMenu()
        {
            if (planets.Count >= maxPlanets)
            {
                Console.WriteLine("Max planets reached.");
                return;
            }

            Console.WriteLine("1. Add manually");
            Console.WriteLine("2. Add random planet");
            Console.WriteLine("3. Generate default (only type and name)");
            Console.WriteLine("4. Add Parse (string input)");

            int choice = InputHelper.GetIntValue("Choose: ", "Invalid.", 1, 4);

            if (choice == 1)
            {
                var planet = new Planet();

                while (true)
                {
                    try
                    {
                        Console.Write("Choose planet type (0 - Terrestrial, 1 - GasGiant, 2 - IceGiant, 3 - Dwarf): ");
                        int typeInt = int.Parse(Console.ReadLine()!);
                        planet.Type = (PlanetType)typeInt;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Enter name: ");
                        planet.Name = Console.ReadLine();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Enter mass (kg): ");
                        planet.Mass = double.Parse(Console.ReadLine()!);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Enter radius (m): ");
                        planet.Radius = double.Parse(Console.ReadLine()!);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Enter distance from Sun (mln km): ");
                        planet.DistanceFromSun = double.Parse(Console.ReadLine()!);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Has life? (y/n): ");
                        string input = Console.ReadLine()!.ToLower();
                        if (input == "y" || input == "yes") planet.HasLife = true;
                        else if (input == "n" || input == "no") planet.HasLife = false;
                        else throw new ArgumentException("Enter Y or N.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                planets.Add(planet);
                Console.WriteLine("Planet added successfully!");
            }
            else if (choice == 2)
            {
                Planet p = new Planet(rnd);
                planets.Add(p);
                Console.WriteLine($"Random planet generated: {p.Name}");
            }
            else if (choice == 3)
            {
                int typeInt = InputHelper.GetIntValue("Choose planet type (0 - Terrestrial, 1 - GasGiant, 2 - IceGiant, 3 - Dwarf): ", "Invalid choice.", 0, 3);
                PlanetType type = (PlanetType)typeInt;

                string name = InputHelper.GetStringValue("Input name (1-30): ", "Invalid input.", s => s.Length > 0 && s.Length < 31);

                planets.Add(new Planet(type, name!));
                Console.WriteLine("Planet added successfully!");
            }
            else if (choice == 4)
            {
                Console.WriteLine("Enter planet data in format:");
                Console.WriteLine("type|name|mass|radius|distance|hasLife");
                Console.Write("Example: Terrestrial|Earth|5,97E24|6371000|150|true\n\n");

                string input = Console.ReadLine()!;
                if (Planet.TryParse(input, out Planet parsedPlanet, out string error))
                {
                    planets.Add(parsedPlanet);
                    Console.WriteLine("Planet added successfully!");
                }
                else
                {
                    Console.WriteLine(error);
                }
            }
        }

        private static void ShowPlanetsTable()
        {
            if (planets.Count == 0)
            {
                Console.WriteLine("No planets available.");
                return;
            }

            Console.WriteLine($"\n{"#",3} {"Name",-12} | {"Type",-12} | {"Mass (kg)",12} | {"Radius (m)",12} | {"Dist (mln km)",12} | {"Life",-3} | {"Age",5} | {"Speed (km/s)",12} | {"Traveled (km)",12}\n");
            for (int i = 0; i < planets.Count; i++)
            {
                Console.WriteLine($"{i + 1,3} {planets[i].ToTableString()}");
            }
        }

        private static void FindObjectMenu()
        {
            if (planets.Count == 0) { Console.WriteLine("No planets."); return; }

            int choice = InputHelper.GetIntValue("Find by: 1 - Name, 2 - Type: ", "Invalid.", 1, 2);

            if (choice == 1)
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine()!;
                List<Planet> found = new List<Planet>();

                foreach (var p in planets)
                {
                    if (p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        found.Add(p);
                    }
                }

                if (found.Count == 0) Console.WriteLine("No matches.");
                else
                {
                    foreach (var p in found)
                    {
                        Console.WriteLine(p.ToTableString());
                    }
                }
            }
            else
            {
                int typeInt = InputHelper.GetIntValue("Choose type (0 - Terrestrial, 1 - GasGiant, 2 - IceGiant, 3 - Dwarf): ", "Invalid.", 0, 3);
                List<Planet> found = new List<Planet>();

                foreach (var p in planets)
                {
                    if (p.Type == (PlanetType)typeInt)
                    {
                        found.Add(p);
                    }
                }

                if (found.Count == 0) Console.WriteLine("No matches.");
                else
                {
                    foreach (var p in found)
                    {
                        Console.WriteLine(p.ToTableString());
                    }
                }
            }
        }

        private static void DemonstrateBehaviorMenu()
        {
            if (planets.Count == 0) { Console.WriteLine("No planets."); return; }

            Console.WriteLine("1. Show gravity");
            Console.WriteLine("2. Make year pass");
            Console.WriteLine("3. Toggle life");

            int choice = InputHelper.GetIntValue("Choose: ", "Invalid.", 1, 3);

            if (choice == 1)
            {
                foreach (var p in planets)
                {
                    Console.WriteLine($"{p.Name} gravity: {p.GetGravity()} m/s^2");
                }
            }
            else if (choice == 2)
            {
                foreach (var p in planets)
                {
                    Console.WriteLine(p.MakeYearPass());
                }

                int years = InputHelper.GetIntValue("Input years: ", "Invalid input.", 1, int.MaxValue);

                foreach (var p in planets)
                {
                    Console.WriteLine(p.MakeYearPass(years));
                }
            }

            else if (choice == 3)
            {
                Console.Write("Enter planet name: ");
                string name = Console.ReadLine();
                Planet planet = null;

                foreach (var p in planets)
                {
                    if (p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        planet = p;
                        break;
                    }
                }
                if (planet == null) { Console.WriteLine("Planet not found."); return; }

                bool create = InputHelper.GetYesNo("Do you want to create life? (y = create / n = destroy): ");
                Console.WriteLine(create ? planet.BirthLife() : planet.DestroyLife());
            }
        }

        private static void DeleteObjectMenu()
        {
            if (planets.Count == 0) { Console.WriteLine("No planets."); return; }

            Console.WriteLine("Delete by: 1 - Name, 2 - Type, 3 - Number in list");
            int choice = InputHelper.GetIntValue("Choose: ", "Invalid.", 1, 3);

            if (choice == 1)
            {
                Console.Write("Enter name: ");
                string name = Console.ReadLine()!;
                int removed = 0;

                for (int i = planets.Count - 1; i >= 0; i--)
                {
                    if (planets[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        planets.RemoveAt(i);
                        removed++;
                    }
                }
                for (int i = 0; i < removed; i++)
                {
                    Planet.PlanetDestroyed();
                }

                Console.WriteLine(removed == 0 ? "No planet found." : $"{removed} planet(s) deleted.");
            }
            else if (choice == 2)
            {
                int typeInt = InputHelper.GetIntValue("Choose type (0 - Terrestrial, 1 - GasGiant, 2 - IceGiant, 3 - Dwarf): ", "Invalid.", 0, 3);
                int removed = 0;

                for (int i = planets.Count - 1; i >= 0; i--)
                {
                    if (planets[i].Type == (PlanetType)typeInt)
                    {
                        planets.RemoveAt(i);
                        removed++;
                    }
                }
                for (int i = 0; i < removed; i++)
                {
                    Planet.PlanetDestroyed();
                }
                Console.WriteLine(removed == 0 ? "No planets of this type found." : $"{removed} planet(s) deleted.");
            }
            else
            {
                ShowPlanetsTable();
                int number = InputHelper.GetIntValue("Enter planet number to delete: ", "Invalid.", 1, planets.Count);
                Planet removed = planets[number - 1];
                planets.RemoveAt(number - 1);
                Planet.PlanetDestroyed();
                Console.WriteLine($"Planet {removed.Name} deleted.");
            }
        }
    }
}
