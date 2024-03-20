using System;
using System.Runtime.Intrinsics.Arm;

namespace Checkpoint3
{
    class Program
    {
        private delegate double fMathOperation(double num1, double num2, int qnumber, int totalQuestions);

        static void Main(string[] args)
        {
          var operations = new Dictionary<int, fMathOperation>
           {
                {1, Add},
                {2, Subtract},
                {3, Multiply},
                {4, Divide},
          };

         var totalQuestions = (int)Rng(5, 15);
         var cnt = 1;
         var score = 0;

            do
            {
                var num1 = (int)Rng(0, 99);
                var num2 = (int)Rng(0, 99);
                var operation = (int)Rng(1, 4);

                if (operation == 4 && num2 == 0)
                {
                    // reroll num2
                    num2 = (int)Rng(1, 99);
                }
                var answer = operations[operation]((double)num1, (double)num2, cnt, totalQuestions);
              
                InputHandler<double>("your answer:", out double ianswer);
                string formatValue = answer.ToString("F2");
                double.TryParse(formatValue, out answer);

                score += answer == ianswer ? 1 : 0;
                Console.WriteLine(answer == ianswer ? "Correct!" : "Incorrect!" + $"Correct answer is: {answer}");

                cnt++;
            }
            while (cnt <= totalQuestions);
            var average = ((double)score / totalQuestions) * 100;
            Console.WriteLine($"Results:\r\nTotal Correct Answers: {score}/{totalQuestions}\r\nPercentage of Correct Answers: {(int)average}%");
        }


        private static double Rng(int minValue, int maxValue)
        {
            Random random = new Random();
            double randomNumber = random.NextDouble() * (maxValue - minValue) + minValue;

            return Math.Round(Convert.ToDouble(randomNumber), 2);
        }

        private static T InputHandler<T>(String inputName, out T value) where T : IConvertible
        {
            string input;
            try
            {
                Console.WriteLine("Enter the " + inputName + $" [TYPE] <{typeof(T).Name}>");
                input = Console.ReadLine() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    throw new Exception("Blank values are not allowed!");
                }

                if (typeof(T) != typeof(T))
                {
                    throw new Exception("Datatype Error!");
                }


                if (typeof(T) == typeof(string) && input.Any(char.IsDigit))
                {
                    throw new Exception("String cannot contain numbers!");
                }

                value = (T)Convert.ChangeType(input, typeof(T));

                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return InputHandler(inputName, out value);
            }
        }

        private static double Add(double num1, double num2, int qnumber, int totalQuestions)
        {
            Console.WriteLine($"Question {qnumber}/{totalQuestions}: What is {num1} + {num2}?");
            return num1 + num2;
        }

        private static double Subtract(double num1, double num2, int qnumber, int totalQuestions)
        {
            Console.WriteLine($"Question {qnumber}/{totalQuestions}: What is {num1} - {num2}?");
            return num1 - num2;
        }

        private static double Multiply(double num1, double num2, int qnumber, int totalQuestions)
        {
            Console.WriteLine($"Question {qnumber}/{totalQuestions}: What is {num1} * {num2}?");
            return num1 * num2;
        }

        private static double Divide(double num1, double num2, int qnumber, int totalQuestions)
        {
            Console.WriteLine($"Question {qnumber}/{totalQuestions}: What is {num1} / {num2}?");
            return num1 / num2;
        }
    }
}