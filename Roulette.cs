using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
  
    internal class Roulette:Games
    {
        private AccountManager accountManager;
        private Player player;

        public Roulette(AccountManager manager)
        {
            this.accountManager = manager;
        }
        private List<string> bets = new List<string>() { "Bets on Dozens", "Straight Bet", "Bets on Color",
        "Even/Odd","High/Low"};
        public override void Play()
        {
           
        Random random = new Random();
            bool playAgain = true;
            float countWin = 0;
            float countGame = 0;
            Console.WriteLine("Приветствуе Вас в игре, в зависимости от соотношения Ваших побед" +
                " к количеству сыграных игр Ваш ранг будет изменятся от 'Наш клиент' до 'Везучий игрок'.");
            Console.WriteLine("Если Вы одержите 5 побед за одну игровую сессию получите бонус 200 fsp.");
            Console.WriteLine("Введите ваше имя:");
            string username = Console.ReadLine();
            while (playAgain)
            {
                Console.WriteLine("Добро пожаловать на игровой стол 'Roulette'.");
                countGame++;
                int[] ranks = {0,32,15,19,4,21,2,25,17,34,6,27,13,36,11,30,8,23,10,5,24,16,33,1,20,14,31,9,22,18,
                29,7,28,12,35,3,26};
                bool[] colors = new bool[ranks.Length];
                colors[1] = true; colors[2] = false;
                colors[3] = true; colors[4] = false;
                colors[5] = true; colors[6] = false;
                colors[7] = true; colors[8] = false;
                colors[9] = true; colors[10] = false;
                colors[11] = true; colors[12] = false;
                colors[13] = true; colors[14] = false;
                colors[15] = true; colors[16] = false;
                colors[17] = true; colors[18] = false;
                colors[19] = true; colors[20] = false;
                colors[21] = true; colors[22] = false;
                colors[23] = true; colors[24] = false;
                colors[25] = true; colors[26] = false;
                colors[27] = true; colors[28] = false;
                colors[29] = true; colors[30] = false;
                colors[31] = true; colors[32] = false;
                colors[33] = true; colors[34] = false;
                colors[35] = true; colors[36] = false;
                Console.WriteLine("Сделайте вашу ставку:");
                decimal bet = Convert.ToDecimal(Console.ReadLine());

                this.player = accountManager.GetPlayer(username);

                if (this.player == null)
                {

                    return;
                }

                if (player.Balance < bet)
                {
                    Console.WriteLine("Недостаточно средств для ставки.");
                    return;
                }

                // Снятие ставки с баланса
                accountManager.Withdraw(player.Username, bet);
                player.TransactionHistory.Add(new Transaction(-bet, TransactionType.Bet, DateTime.Now));
                Console.WriteLine("Выберите вид игры:");
                for (int i = 0; i < bets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {bets[i]}");
                }
                int x = Convert.ToInt32(Console.ReadLine());

                switch (x)
                {
                    case 1:
                        Console.WriteLine("Вы выбрали 'Ставки на Дюжины'");
                        Console.WriteLine("На какую дюжину ставите:");
                        Console.WriteLine("1 - (1-12). 2 - (13-24). 3 - (25-36).");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        bool one = false;
                        bool two = false;
                        bool three = false;
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("Вы поставили на первую дюжину (1-12)");
                                one = true;
                                break;
                            case 2:
                                Console.WriteLine("Вы поставили на вторую дюжину (13-24)");
                                two = true;
                                break;
                            case 3:
                                Console.WriteLine("Вы поставили на третью дюжину (25-36)");
                                three = true;
                                break;
                            default:
                                Console.WriteLine("Unknown number");
                                break;
                        }
                        Console.WriteLine("Крупье вращает рулетку и бросает шарик.");
                        void game1Win()
                        {
                            decimal winAmount = bet * 3; 
                            accountManager.Deposit(player.Username, winAmount);
                            player.TransactionHistory.Add(new Transaction(winAmount, TransactionType.Deposit, DateTime.Now));
                            player.GameHistory.Add(new GameResult("Roulette", winAmount, DateTime.Now));
                            Console.WriteLine($"Сумма Вашего выигрыша {winAmount}!");
                            countWin++;
                        }
                        int number1 = ranks[random.Next(ranks.Length)];
                        if (one && number1 >= 1 && number1 <= 12)
                        {
                            Console.WriteLine($"Выпал номер - {number1}");
                            Console.WriteLine("Ваша ставка сыграла.");
                            game1Win();
                        }
                        else if (two && number1 >= 13 && number1 <= 24)
                        {
                            Console.WriteLine($"Выпал номер - {number1}");
                            Console.WriteLine("Ваша ставка сыграла.");
                            game1Win();
                        }
                        else if (three && number1 >= 25 && number1 <= 36)
                        {
                            Console.WriteLine($"Выпал номер - {number1}");
                            Console.WriteLine("Ваша ставка сыграла.");
                            game1Win();
                        }
                        else
                        {
                            Console.WriteLine($"Выпал номер - {number1}");
                            if (number1 == 0)
                            {
                                Console.WriteLine("Выпало Зеро.");
                                Console.WriteLine("Ваша ставка не сыграла.");
                                player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));
                            }
                            else
                            {
                                Console.WriteLine("Ваша ставка не сыграла.");
                                player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("Вы выбрали 'Прямая Ставка'");
                        Console.WriteLine("Сделайте ставку на число от 1 до 36");
                        bool isValidInput = false;
                        int yourbet;
                        void game2Win()
                        {
                            decimal winAmount = bet * 35; 
                            accountManager.Deposit(player.Username, winAmount);
                            player.TransactionHistory.Add(new Transaction(winAmount, TransactionType.Deposit, DateTime.Now));
                            player.GameHistory.Add(new GameResult("Roulette", winAmount, DateTime.Now));
                            Console.WriteLine($"Сумма Вашего выигрыша {winAmount}!");
                            countWin++;
                        }
                        while (!isValidInput)
                        {

                            yourbet = Convert.ToInt32(Console.ReadLine());
                            if (yourbet < 0 || yourbet > 36)
                            {
                                Console.WriteLine("Выберите номер сектора рулетки от 1 до 36");
                            }
                            else
                            {
                                isValidInput = true;
                                int number2 = ranks[random.Next(ranks.Length)];
                                if (number2 == yourbet)
                                {
                                    Console.WriteLine($"Выпал номер - {number2}");
                                    Console.WriteLine("Ваша ставка сыграла.");
                                    game2Win();
                                }
                                else
                                {
                                    Console.WriteLine($"Выпал номер - {number2}");
                                    Console.WriteLine("Вы проиграли.");
                                    player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));
                                }
                            }
                        }

                        break;
                    case 3:
                        Console.WriteLine("Вы выбрали 'Ставки на Цвет'");
                        Console.WriteLine("Выберите цвет. 1 - Красное. 2 - Черное.");
                        int col = Convert.ToInt32(Console.ReadLine());
                        bool colred = false;
                        bool colblack = false;
                        void game3Win()
                        {
                            decimal winAmount = bet * 2; 
                            accountManager.Deposit(player.Username, winAmount);
                            player.TransactionHistory.Add(new Transaction(winAmount, TransactionType.Deposit, DateTime.Now));
                            player.GameHistory.Add(new GameResult("Roulette", winAmount, DateTime.Now));
                            Console.WriteLine($"Сумма Вашего выигрыша {winAmount}!");
                            countWin++;
                        }
                        switch (col)
                        {
                            case 1:
                                Console.WriteLine("Вы поставили на красное.");
                                colred = true;
                                break;
                            case 2:
                                Console.WriteLine("Вы поставили на черное.");
                                colblack = true;
                                break;
                            default:
                                Console.WriteLine("Unknown number");
                                break;
                        }
                        Console.WriteLine("Крупье вращает рулетку и бросает шарик.");
                        int number3 = ranks[random.Next(ranks.Length)];
                        int index = Array.IndexOf(ranks, number3);
                        if (index != 0)
                        {
                            bool isRed = colors[index];
                            if (isRed)
                            {
                                Console.WriteLine($"Выпал номер - {number3} красный сектор");
                                if (colred)
                                {
                                    Console.WriteLine("Ваша ставка сыграла.");
                                    game3Win();
                                }
                                else
                                    Console.WriteLine("Вы проиграли.");
                                player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));
                            }
                            else
                            {
                                Console.WriteLine($"Выпал номер - {number3} черный сектор");
                                if (colblack)
                                {
                                    Console.WriteLine("Ваша ставка сыграла.");
                                    game3Win();
                                }
                                else
                                {
                                    Console.WriteLine("Вы проиграли.");
                                    player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Выпало Зеро.");
                            Console.WriteLine("Вы проиграли.");
                            player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));
                        }

                        break;
                    case 4:
                        Console.WriteLine("Вы выбрали 'Чёт/Нечёт'");
                        Console.WriteLine("Выберите на что ставите. 1 - Чет. 2 - Нечёт.");
                        int chnch = Convert.ToInt32(Console.ReadLine());
                        bool chet = false;
                        bool nechet = false;
                        switch (chnch)
                        {
                            case 1:
                                Console.WriteLine("Вы поставили на выпадение четного числа.");
                                chet = true;
                                break;
                            case 2:
                                Console.WriteLine("Вы поставили на выпадание нечетного числа.");
                                nechet = true;
                                break;
                            default:
                                Console.WriteLine("Unknown number");
                                break;
                        }
                        Console.WriteLine("Крупье вращает рулетку и бросает шарик.");
                        int number4 = ranks[random.Next(ranks.Length)];
                        if (number4 % 2 == 0 && chet == true)
                        {
                            Console.WriteLine($"Выпал номер - {number4} четное число");
                            Console.WriteLine("Ваша ставка сыграла.");
                            game3Win();
                        }
                        else if (number4 % 2 != 0 && nechet == true)
                        {
                            Console.WriteLine($"Выпал номер - {number4} нечетное число");
                            Console.WriteLine("Ваша ставка сыграла.");
                            game3Win();
                        }
                        else
                        {
                            Console.WriteLine($"Выпал номер - {number4}");
                            if (number4 == 0)
                            {
                                Console.WriteLine("Выпало Зеро.");
                                Console.WriteLine("Ваша ставка не сыграла.");
                                player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));

                            }
                            else
                            {
                                Console.WriteLine("Ваша ставка не сыграла.");
                                player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));
                            }
                        }
                        break;
                    case 5:
                        Console.WriteLine("Вы выбрали 'Высокие/Низкие'");
                        Console.WriteLine("Выберите на что ставите. 1 - Высокие. 2 - Низкие.");
                        int choice5 = Convert.ToInt32(Console.ReadLine());
                        bool high = false;
                        bool low = false;
                        switch (choice5)
                        {
                            case 1:
                                Console.WriteLine("Вы поставили на выпадение числа от 18 до 36.");
                                high = true;
                                break;
                            case 2:
                                Console.WriteLine("Вы поставили на выпадание числа от 1 до 18.");
                                low = true;
                                break;
                            default:
                                Console.WriteLine("Unknown number");
                                break;
                        }
                        Console.WriteLine("Крупье вращает рулетку и бросает шарик.");
                        int number5 = ranks[random.Next(ranks.Length)];
                        if (number5 >= 18 && high == true)
                        {
                            Console.WriteLine($"Выпал номер - {number5} находящееся в верхнем диапазоне");
                            Console.WriteLine("Ваша ставка сыграла.");
                            game3Win();
                        }
                        else if (number5 < 18 && low == true)
                        {
                            Console.WriteLine($"Выпал номер - {number5} находящееся в нижнем диапазоне");
                            Console.WriteLine("Ваша ставка сыграла.");
                            game3Win();
                        }
                        else
                        {
                            Console.WriteLine($"Выпал номер - {number5}");
                            if (number5 == 0)
                            {
                                Console.WriteLine("Выпало Зеро.");
                                Console.WriteLine("Ваша ставка не сыграла.");
                                player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));
                            }
                            else
                            {
                                Console.WriteLine("Ваша ставка не сыграла.");
                                player.GameHistory.Add(new GameResult("Roulette", 0, DateTime.Now));
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown number");
                        break;
                }
                if (countWin == 5)
                {
                    player.Balance += 200;
                    Console.WriteLine($"Вы получили бонус за 5 побед, Ваш новый баланс - {player.Balance} ");
                }
                else if (countWin == 10)
                {
                    player.Balance += 200;
                    Console.WriteLine($"Вы получили бонус за 5 побед, Ваш новый баланс - {player.Balance} ");
                }
                if (countWin / countGame >= 0.5f && countGame > 5)
                {
                    Console.WriteLine("Ваш ранг изменен на 'Везучий игрок'.");
                }
                else
                    Console.WriteLine("Ваш ранг  'Наш клиент'.");
                Console.WriteLine("Хотите сыграть еще? (1 - да/2 - нет)");
                int response = Convert.ToInt32(Console.ReadLine());
                if (response != 1)
                {
                    playAgain = false;
                }
            }
        }
        public override void DisplayRules()
        {
            Console.WriteLine("Правила ставок в рулетке:");
            Console.WriteLine("1. Ставки на Дюжины:");
            Console.WriteLine("   - Ставка на первую дюжину чисел (1-12). Выплата: 2 к 1.");
            Console.WriteLine("   - Ставка на вторую дюжину чисел (13-24). Выплата: 2 к 1.");
            Console.WriteLine("   - Ставка на третью дюжину чисел (25-36). Выплата: 2 к 1.");
            Console.WriteLine("_________________________________________________________________");
            Console.WriteLine("2. Прямая Ставка:");
            Console.WriteLine("   - Ставка на любое одно число, включая 0. Выплата: 35 к 1.");
            Console.WriteLine("_________________________________________________________________");
            Console.WriteLine("3. Ставки на Цвет:");
            Console.WriteLine("   - Ставка на все красные числа. Выплата: 1 к 1.");
            Console.WriteLine("   - Ставка на все чёрные числа. Выплата: 1 к 1.");
            Console.WriteLine("_________________________________________________________________");
            Console.WriteLine("4. Чёт/Нечёт:");
            Console.WriteLine("   - Ставка на все чётные числа. Выплата: 1 к 1.");
            Console.WriteLine("   - Ставка на все нечётные числа. Выплата: 1 к 1.");
            Console.WriteLine("_________________________________________________________________");
            Console.WriteLine("5. Высокие/Низкие:");
            Console.WriteLine("   - Ставка на низкие числа (1-18). Выплата: 1 к 1.");
            Console.WriteLine("   - Ставка на высокие числа (19-36). Выплата: 1 к 1.");
            Console.WriteLine("_________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("Выберите тип ставки и введите сумму ставки перед началом игры.");
        }

    }
}
