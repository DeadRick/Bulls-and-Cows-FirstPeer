using System;
using System.Linq;
using System.Threading.Channels;
using Microsoft.VisualBasic;

namespace Bull_and_Cow
{
    class Program
    {
        static void WelcomeIntro() // Начальное меню.
        {
            Console.WriteLine("╔╗ ┬ ┬┬  ┬  ┌─┐");
            Console.WriteLine("╠╩╗│ ││  │  └─┐");
            Console.WriteLine("╚═╝└─┘┴─┘┴─┘└─┘");
            Console.WriteLine("┌─┐┌┐┌┌┬┐      ");
            Console.WriteLine("├─┤│││ ││      ");
            Console.WriteLine("┴ ┴┘└┘─┴┘      ");
            Console.WriteLine("┌─┐┌─┐┬ ┬┌─┐┬  ");
            Console.WriteLine("│  │ ││││└─┐│  ");
            Console.WriteLine("└─┘└─┘└┴┘└─┘o  ");
            Console.WriteLine("Нажмите на клаватуре \"1\" чтобы узнать об обозначениях.");
            Console.WriteLine("Нажмите ENTER для начала!");
            
            if(Console.ReadKey().Key == ConsoleKey.D1)
            {
                Console.WriteLine();
                Console.WriteLine("COWs - это коровы, сколько цифр угадано, но не расположено на своих местах.");
                Console.WriteLine("BULLs - это быки, сколько цифр угадано инаходится на своих местах.");
                Console.WriteLine();
            }
            
            return;
        }

        static int CowsBullsCounter(string num, string guess) // Метод для подсчета быков и коров.
        {
            int bull = 0, cow = 0;
            for (int go = 0; go < num.Length; go++)
            {
                
                if ((num.Contains(guess[go])) && (guess[go] != num[go]))
                {
                    cow += 1;
                }
                else if (guess[go] == num[go])
                {
                    bull += 1;
                }
            }
            Console.WriteLine($"COWs - {cow}. BULLs - {bull}");
            return 1;
        }
        static string Reverse(string text) // Создан для уникальноо случая, если число начинается на 0. 
        {
            char[] arr = text.ToCharArray();
            string reverse = string.Empty;
            for (int i = arr.Length - 1; i > -1; i--)
            {
                reverse += arr[i];
            }

            return reverse;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static string RandomNum(int n) // Генерация рандомного числа.
        {
            var rand = new Random();
            string final = "";

            int[] arr = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}; 
            arr = arr.OrderBy(x => rand.Next()).ToArray(); // Перемешивает массив. 
            for (int go = 0; go <= n; go++)
            {
                final += arr[go];
            }
            return final;
        }

        static string EqualNum(string num, string guess) // Сравнение длины рандомного числа и пользовательского.
        {
            string danger = String.Empty;
            if (num.Length != guess.Length)
            {
                danger = "WARNING!";
                return danger;
            }

            return danger;
        }

        static int RepeatSymb(string guess) // Проверка повторяющихся символов в введённом пользователем числе.
        {
            int cnt_flag = 0;
            for (int go = 0; go < guess.Length; go++)
            {
                if (guess.Count(guess[go].Equals) > 1)
                {
                    cnt_flag = 1;
                }
            }

            return cnt_flag;
        }

        static string
            CheckRange(int len_of_num, string len_of_num_string) // Проверка длины числа, запрашиваемого пользователем.
        {
            while ((len_of_num < 2) || (len_of_num > 10))
            {
                do
                {
                    Console.WriteLine("Длина числа должна быть от 2 до 10!");
                    Console.Write("Число от 2 до 10: ");
                    len_of_num_string = Console.ReadLine();
                } while ((!int.TryParse(len_of_num_string, out len_of_num)) && (len_of_num < 2 || len_of_num > 10));
            }
           
            return len_of_num_string;
        }

        static void WarningMoments(string guess, string number, string result, int cnt_of_try = 0)
        { // Ведёт финальный подсчет, также выводятся предупреждения.
            do
            {
                ulong empty_long;
                Console.Write($"{number.Length}-х значное число: ");
                guess = Console.ReadLine();
                result = EqualNum(number, guess); // Если длина не совпадает, return "WARNING!"
                var flag = RepeatSymb(guess); // Проверка повторений в пользовательском числе.
                if (result == "WARNING!") // Если числа не совпадают, то...
                {
                    Console.WriteLine($"ПРЕДУПРЕЖДЕНИЕ! Вы ввели {guess.Length} символа, а надо {number.Length}!");
                }
                else if (flag == 1) // Иначе если символы повторяются, то...
                {
                    Console.WriteLine("ПРЕДУПРЕЖДЕНИЕ! У вас есть повторяющиеся символы.");
                } 
                else if (guess[0] == '-')
                {
                    Console.WriteLine("Введите целое число!");
                } else if (!ulong.TryParse(guess, out empty_long))
                {
                    Console.WriteLine("Нужно вводить цифры, а не что-то другое -_-...");
                }
                else // Иначе запускается подсчет быков и коров. Также прибавляется счетчик попыток.
                {
                    CowsBullsCounter(number, guess);
                    cnt_of_try++;
                } 
            } while (number != guess);
            Console.WriteLine($"Поздравляем! Вы отгдали число! Это число - {number}.");
            Console.WriteLine($"Количество попыток:  {cnt_of_try}");
            Console.WriteLine("Для выхода нажмите ESCAPE. Чтобы сыграть снова нажмите ENTER.");
        } 

        static void Game() // Основной функционал игры.
        {
            string l_num, number, out_num; 
            int len, len_out;

            do
            {
                Console.Write("Число от 2 до 10: ");
                l_num = Console.ReadLine();
            } while (!int.TryParse(l_num, out len));

            int.TryParse(l_num, out len);
            out_num = CheckRange(len, l_num); // Присваиваем новую переменную, если число меньше двух и больше 10.
            int.TryParse(out_num, out len_out);
            if (out_num == "NO") // Если с длиной всё нормально, то пропускаем создание новой переменной.
            {
                number = RandomNum(len - 1); 
            }
            else
            {
                number = RandomNum(len_out - 1); 
            }

            if (number[0] == '0') // При генерации числа с 0 в начале, число меняется.
            {
                number = Reverse(number);
            }

            Console.WriteLine("Попробуй угадать число.");

            WarningMoments("", number, "");
            
        }
        static void Main(string[] args)
        {
            
            WelcomeIntro(); 
            Console.WriteLine("Добро пожаловать в игру \"Bulls and Cows\"!");
            Console.WriteLine("Тебе нужно ввести число от 2 до 10.");
            do
            {
                Game();
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        } // P.S. Игра не от 1 до 10, так как при длине в 1 символ игра не имеет смысла. 
    }     // То есть это уже игра "Угадай число".    
}         // Именно поэтому я посчитал правильным не включать 1 символьное число в игровой процесс.