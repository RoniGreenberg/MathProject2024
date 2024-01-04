using System;

namespace MathProject
{
    internal class OneGame
    {
        private  int number1;
        private  int number2;
        private  char operation;

        public int Number1 { get { return number1; } }
        public int Number2 { get { return number2; } }

        public int CorrectAnswer
        {
            get
            {
                switch (operation)
                {
                    case '+':
                        return number1 + number2;
                    case '-':
                        return number1 - number2;
                    case '*':
                        return number1 * number2;
                    case '/':
                        return number1 / number2;
                    default:
                        throw new ArgumentException("Invalid operation");
                }
            }
        }

        public OneGame(int grade, char operation)
        {
            Random rnd = new Random();
            this.operation = operation;

            switch (grade)
            {
                case 1:
                    if (operation == '+') //חיבור
                    {
                        number1 = rnd.Next(1, 11); 
                        number2 = rnd.Next(1, 11);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid operation for Grade 1");
                    }
                    break;
                case 2:
                    if (operation == '-') //חיסור
                    {
                        number1 = rnd.Next(1, 11);
                        number2 = rnd.Next(1, number1 + 1);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid operation for Grade 2");
                    }
                    break;
                case 3:
                    if (operation == '*')  //כפל
                    {
                        number1 = rnd.Next(1, 11);
                        number2 = rnd.Next(1, 11);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid operation for Grade 3");
                    }
                    break;
                case 4:
                    if (operation == '/')  //חילוק
                    {
                        number2 = rnd.Next(1, 11);
                        number1 = number2 * rnd.Next(1, 11);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid operation for Grade 4");
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid grade parameter");
            }
        }

        //(הגרלת מספרים רנדומליים מאחד עד עשר ואני עושה ארבעה מקרים עבור כל כיתה עם סימן אחר (חיבור חיסור כפל או חילוק
        public void GenerateNewProblem()
        {
            Random rnd = new Random();
            switch (operation)
            {
                case '+':
                    number1 = rnd.Next(1, 11);
                    number2 = rnd.Next(1, 11);
                    break;
                case '-':
                    number1 = rnd.Next(1, 11);
                    number2 = rnd.Next(1, number1 + 1);
                    break;
                case '*':
                    number1 = rnd.Next(1, 11);
                    number2 = rnd.Next(1, 11);
                    break;
                case '/':
                    number2 = rnd.Next(1, 11);
                    number1 = number2 * rnd.Next(1, 11);
                    break;
                default:
                    throw new ArgumentException("Invalid operation");
            }
        }

        //פעולה המחזירה את האופרטור שלפני כן היה כתו
        public char GetOperationSymbol()
        {
            return operation;
        }
    }
}
