using System;
using System.Collections.Generic;

namespace Area_Trainer {
    class Globals {
        public int[] lengths = new int[2];
        public double answer;
        public int tries;
        public int questionNumber;
        public int totalScore = 0;
        public List<int> questions = new List<int>();
        public List<int> scores = new List<int>();
    }

    class Program : Globals {
        static void Main() {
            Console.Title = "Area Trainer";
            Program p = new Program();
            bool flashControl = false;
            do {
                Console.Clear();
                if (flashControl) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("     Area Trainer     ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("Press any key to start");
                    System.Threading.Thread.Sleep(600);
                }
                else if (!flashControl) {
                    Console.WriteLine("     Area Trainer     ");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Press any key to start");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Threading.Thread.Sleep(600);
                }
                flashControl = !flashControl;
            } while (!Console.KeyAvailable);
            Console.ReadKey(true);
            p.MainMenu();
        }

        void Error(int code) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error:");
            Console.ForegroundColor = ConsoleColor.White;
            switch (code) {
                case 1:
                    Console.Write("Please enter a number!\n");
                    break;
                case 2:
                    Console.Write("Please enter a valid number! (1 - 4)\n");
                    break;
                default:
                    Console.Write("Unknown error code.\n");
                    break;
            }
            System.Threading.Thread.Sleep(600);
        }

        void MainMenu() {
            if (questionNumber <= 10) {
                bool quit = false;
                int choice = 0;
                do {
                    Console.Clear();
                    Console.WriteLine("Please choose one of the following shapes:\n 1. Square\n 2. Rectangle\n 3. Circle");
                    Console.WriteLine("\nEnter 4 to quit.");
                    Console.WriteLine("\n\nPlease enter the number of your choice:");
                    string input = Console.ReadLine();
                    bool numeric = int.TryParse(input, out choice);
                    if (numeric) {
                        if (choice <= 3 && choice >= 1) Train(choice);
                        else if (choice == 4) quit = true;
                        else Error(2);
                    }
                    else Error(1);
                } while (!quit);
            }
            else Console.WriteLine("You can only complete 10 questions per session!");
            System.Threading.Thread.Sleep(600);
            if (questions.Count != 0) DisplayResults();
        }

        void DisplayResults() {
            Console.Clear();
            Console.WriteLine("Session Round-up: \n");
            for (int i = 0; i < questions.Count; i++)
                Console.WriteLine($"Question: {questions[0]}    Score: {scores[0]}");
            Console.WriteLine($"\nYou scored {totalScore} point(s) from {questionNumber} question(s)!");
            System.Threading.Thread.Sleep(4000);
            Console.WriteLine("Press any key to quit the game.");
            Console.ReadKey(true);
        }

        void Train(int gameType) {
            int guess = Init(gameType);
            bool inputValid = false;
            bool correct = false;
            do {
                Console.Clear();
                tries++;
                if (tries == 3) {
                    Console.WriteLine($"No more tries left! The correct answer was {answer}");
                    scores.Add(0);
                    System.Threading.Thread.Sleep(600);
                    break;
                }
                inputValid = false;
                do {
                    Console.WriteLine($"Question {questionNumber}:");
                    switch (gameType) {
                        case 1:
                            Console.WriteLine($"Square\nSide length: {lengths[0]}");
                            break;
                        case 2:
                            Console.WriteLine($"Rectangle:\nHeight: {lengths[0]}\nWidth: {lengths[1]}");
                            break;
                        case 3:
                            Console.WriteLine($"Radius of circle: {lengths[0]}");
                            Console.WriteLine("Use 3.14 instead of Pi for calculations!");
                            break;
                    }
                    Console.WriteLine("\nEnter your guess:");
                    string input = Console.ReadLine();
                    bool numeric = int.TryParse(input, out guess);
                    if (numeric) inputValid = true;
                    else Error(1);
                } while (!inputValid);
                Console.Clear();
                if (guess == answer) {
                    correct = true;
                    if (tries == 1) {
                        Console.WriteLine("You scored two points on this question!");
                        scores.Add(2);
                    }
                    else if (tries == 2) {
                        Console.WriteLine("You scored one point on this question.");
                        scores.Add(1);
                    }
                }
                else if (guess < answer) Console.WriteLine("Higher!");
                else if (guess > answer) Console.WriteLine("Lower!");
                System.Threading.Thread.Sleep(600);
                totalScore += scores[questionNumber - 1];
            } while (!correct);
        }

        int Init(int gameType) {
            Random r = new Random();
            lengths[0] = r.Next(1, 20);
            lengths[1] = r.Next(1, 20);
            answer = GetArea(gameType, lengths);
            tries = 0;
            questionNumber++;
            questions.Add(questionNumber);
            return 0;
        }

        double GetArea(int gameType, int[] lengths) {
            switch (gameType) {
                case 1: return (lengths[0] * lengths[0]);
                case 2: return (lengths[0] * lengths[1]);
                case 3: return (lengths[0] * lengths[0] * 3.14);
                default: return -1;
            }
        }
    }
}