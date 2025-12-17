namespace PMP_LAB01_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task 1.

            // Display welcome message on the console
            Console.WriteLine("Hello, World!");

            // Wait until enter is not pressed
            Console.ReadLine();

            #endregion

            #region Task 2.

            Console.Clear(); // Remove all content from the console
            Console.WriteLine("Task 2.\n"); // Used \n for additional new line

            Console.BackgroundColor = ConsoleColor.White; // Set background color to white
            Console.ForegroundColor = ConsoleColor.DarkBlue; // Set font color to blue

            Console.WriteLine("Hello Word again!");

            Console.ReadKey(); // Wait for any character

            // Change the cursor position and visibility

            Console.Write("Can you write your name?");
            Console.CursorVisible = false; // Set off the cursor position
            Console.SetCursorPosition(13, 7); // Set the cursor position into the "middle" of the console

            Console.ReadLine();

            // Adjust the console sizes

            Console.WindowHeight = 14;
            Console.WindowWidth = 26;

            Console.ReadLine();

            #endregion

            #region Set back the sizes and color

            Console.WindowHeight = 25;
            Console.WindowWidth = 50;
            Console.ResetColor(); // May set back all color adjustment

            #endregion

            #region Task 3. - individual task

            Console.Clear();
            Console.WriteLine("Task 3.\n");

            Console.Write("Your name is: ");
            string name = Console.ReadLine();

            Console.WriteLine("Welcome " + name); // String concatenation
            Console.WriteLine("Üdvözöllek {0}", name); // Format string
            Console.WriteLine($"Willkommen {name}"); // String interpolation

            Console.ReadLine();

            #endregion

            #region Task 4.

            Console.Clear();
            Console.WriteLine("Task 4.\n");

            Console.Write("Your birth year is: ");
            string rawBirthYear = Console.ReadLine();
            int birthYear = int.Parse(rawBirthYear); // Convert integer from string
            //int birthYear2 = int.Parse(Console.ReadLine());

            //int currentYear = 2025;
            int currentYear = DateTime.Now.Year; // Gets the actual year

            //Console.WriteLine("Your age is {0} and soon you will be {1} years old", currentYear - birthYear, currentYear - birthYear + 1); // Dont repeat yourself (DRY)!

            int age = currentYear - birthYear;
            Console.WriteLine("Your age is {0} and soon you will be {1} years old", age, age + 1);

            Console.ReadLine();

            #endregion

            #region Task 5.

            Console.Clear();
            Console.WriteLine("Task 5.\n");

            Console.WriteLine("How tall are you?");
            double height = double.Parse(Console.ReadLine());

            Console.WriteLine("How much do you weight?");
            double weight = double.Parse(Console.ReadLine()); // Double precision
            //float weight2 = float.Parse(Console.ReadLine()); // Single precision
            //decimal weight3 = decimal.Parse(Console.ReadLine()); // "Quadruple" precision

            double bmi = weight / (height * height);
            double bmi2 = weight / Math.Pow(height, 2);

            Console.WriteLine($"Your BMI is: {bmi}");

            /* Note: the BMI value "meaning": https://gyuras.hu/testtomeg-index-bmi/
             *  16 vagy alatta: Súlyos soványság
             *  16.1 – 16.9: Mérsékelt soványság
             *  17 – 18.4: Soványság
             *  18.5 – 24.9: Normál testsúly
             *  25 – 29.9: Túlsúlyos
             *  30 – 34.9: I. fokú elhízás
             *  35 – 39.9: II. fokú elhízás (súlyos elhízás)
             *  40 vagy afölött: III. fokú elhízás (nagyfokú elhízás)
             */

            Console.ReadLine();

            #endregion

            #region Task 6.

            Console.Clear();
            Console.WriteLine("Task 6.\n");

            Console.Write("The duration in seconds: ");
            int totalSeconds = int.Parse(Console.ReadLine());

            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            Console.WriteLine("The duration in minutes: {0}:{1}", minutes, seconds.ToString().PadLeft(2, '0'));

            Console.ReadLine();

            #endregion

            #region Task 7.

            Console.Clear();
            Console.WriteLine("Task 7.\n");

            Console.Write("Password: ");
            string psw = Console.ReadLine();
            Console.Write("Password again: ");
            string pswa = Console.ReadLine();

            if (psw == pswa)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Same passwords");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The two passwords are different");
                Console.ResetColor();
            }

            Console.ReadLine();

            #endregion

            #region Task 9.

            Console.Clear();
            Console.WriteLine("Task 9.\n");

            Console.Write("First number: ");
            int first = int.Parse(Console.ReadLine());

            Console.Write("Second number: ");
            int second = int.Parse(Console.ReadLine());

            Console.Write("The operator: ");
            char opr = Console.ReadKey().KeyChar;

            double result;
            bool hasResult = true;

            if (opr == '+')
            {
                result = first + second;
            }
            else if (opr == '-')
            {
                result = first - second;
            }
            else if (opr == '*')
            {
                result = first * second;
            }
            else if (opr == '/')
            {
                if (second == 0)
                {
                    Console.WriteLine("No-no senior! We can't divide by zero!");
                    result = 0;
                    hasResult = false;
                }
                else
                {
                    result = first + second;
                }
            }
            else
            {
                Console.WriteLine("Not supported operation!");
                result = 0;
                hasResult = false;
            }

            if (hasResult) // Note: we suggested to use code blocks for every if statement but of course this style is also fine
                Console.WriteLine("\n{0} {1} {2} = {3}", first, opr, second, result);

            Console.ReadLine();

            #endregion

            #region Task 11. - individual task

            Console.Clear();
            Console.WriteLine("Task 11.\n");

            Console.WriteLine("Write a number between 0 an 9.");
            int givenNumber = int.Parse(Console.ReadLine());

            // Note: if we learn switch statement at this task we can use
            if (givenNumber == 0)
            {
                Console.WriteLine("zero");
            }
            else if (givenNumber == 1)
            {
                Console.WriteLine("one");
            }
            else if (givenNumber == 2)
            {
                Console.WriteLine("two");
            }
            else if (givenNumber == 3)
            {
                Console.WriteLine("three");
            }
            else if (givenNumber == 4)
            {
                Console.WriteLine("four");
            }
            else if (givenNumber == 5)
            {
                Console.WriteLine("five");
            }
            else if (givenNumber == 6)
            {
                Console.WriteLine("six");
            }
            else if (givenNumber == 7)
            {
                Console.WriteLine("seven");
            }
            else if (givenNumber == 8)
            {
                Console.WriteLine("eight");
            }
            else if (givenNumber == 9)
            {
                Console.WriteLine("nine");
            }
            else
            {
                Console.WriteLine("The given number is between the allowed range!");
            }

            Console.ReadLine();

            #endregion

            #region Task 12. - individual task

            Console.Clear();
            Console.WriteLine("Task 12.\n");

            Console.Write("Give me a letter: ");
            string ltr = Console.ReadLine().ToLower(); // Ensure the lower case

            if (ltr == "q" || ltr == "w" || ltr == "r" ||
                ltr == "z" || ltr == "p" || ltr == "s" ||
                ltr == "d" || ltr == "f" || ltr == "g" ||
                ltr == "j" || ltr == "k" || ltr == "l" ||
                ltr == "y" || ltr == "x" || ltr == "c" ||
                ltr == "v" || ltr == "b" || ltr == "n" ||
                ltr == "m")
            {
                Console.WriteLine($"The {ltr} is a consonant.");
            }
            else
            {
                Console.WriteLine($"The {ltr} is a vowel.");
            }

            // Note: this solution is not handle the not proper letters like *
            // Char.IsLetter(ltr[0]); // This call can decide that is this a letter or not. Takes the string first character

            Console.ReadLine();

            #endregion

            #region Task 13. - individual task

            Console.Clear();
            Console.WriteLine("Task 13.\n");

            Console.Write("V = ");
            int capacity = int.Parse(Console.ReadLine());

            Console.Write("R1 = ");
            int income1 = int.Parse(Console.ReadLine());

            Console.Write("R2 = ");
            int income2 = int.Parse(Console.ReadLine());

            Console.Write("T = ");
            float time = float.Parse(Console.ReadLine());

            float bothInput = income1 * time + income2 * time;
            float inputResult = capacity - bothInput;

            if (inputResult >= 0)
            {
                Console.WriteLine("The tank will be {0:F2}% full.", bothInput / capacity * 100); // Used here the String format based floating number limitation after decimal point
            }
            else
            {
                Console.WriteLine("The tank will be overfilled by {0} m3", -inputResult);
            }

            Console.ReadLine();

            #endregion
        }
    }
}
