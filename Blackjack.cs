using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    internal class Blackjack:Games
    {
        private AccountManager accountManager;
        private Player player;
        public Blackjack(AccountManager manager)
        {
            this.accountManager = manager;
        }

        public override void Play()
        {
            Random random = new Random();
            bool playAgain = true;
            float countWin = 0;
            float countGame = 0;
            Console.WriteLine("Приветствуе Вас в игре, в зависимости от соотношения Ваших побед"+
                " к количеству сыграных игр Ваш ранг будет изменятся от 'Наш клиент' до 'Везучий игрок'.");
            Console.WriteLine("Если Вы одержите 5 побед за одну игровую сессию получите бонус 200 fsp."+
                " За следующие 5 подед тоже предусмотрен бонус.");
            Console.WriteLine("Введите ваше имя:");
            string username = Console.ReadLine();
            while (playAgain)
            {
                string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            Dictionary<string, int> cardValues = new Dictionary<string, int>()
            {
                 {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5}, {"6", 6},
                 {"7", 7}, {"8", 8}, {"9", 9}, {"10", 10},
                 {"Jack", 10}, {"Queen", 10}, {"King", 10}, {"Ace", 11}
            };
            Console.WriteLine("Играем в Блэкджек");
            countGame++;
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
            Console.WriteLine();
            Console.WriteLine("Раздача карт игроку: ");
            int aceCountpl = 0;
            int aceCountdl = 0;
            bool winpl = false;
            bool windl = false;
            string playerCard1 = ranks[random.Next(ranks.Length)];
            int playerCard1Value = cardValues[playerCard1];
            Console.WriteLine(playerCard1);
            if (playerCard1 == "Ace")
            {
                aceCountpl++;
            }
            string playerCard2 = ranks[random.Next(ranks.Length)];
            Console.WriteLine(playerCard2);
            int playerCard2Value = cardValues[playerCard2];
            if (playerCard2 == "Ace")
            {
                aceCountpl++;
            }
            int totalsumpl = playerCard1Value + playerCard2Value;
            if (totalsumpl > 21)
            {
                totalsumpl -= 10;
                aceCountpl--;
            }
            // Карты крупье_____________________________________
            string dillerCard1 = ranks[random.Next(ranks.Length)];
            int dillerCard1Value = cardValues[dillerCard1];
            if (dillerCard1 == "Ace")
            {
                aceCountdl++;
            }
            string dillerCard2 = ranks[random.Next(ranks.Length)];
            int dillerCard2Value = cardValues[dillerCard2];
            if (dillerCard2 == "Ace")
            {
                aceCountdl++;
            }
            int totalsumdl = dillerCard1Value + dillerCard2Value;
            if (totalsumdl > 21)
            {
                totalsumdl -= 10;
                aceCountdl--;
            }
            //__________________________________________________
            Console.WriteLine($"Сумма выпавших Вам карт: {totalsumpl}");
            bool wantsMoreCards = true;

            while (totalsumpl <= 21 && wantsMoreCards)
            {
                Console.WriteLine("Желаете получить еще одну карту? 1-Да. 2-Нет.");
                int playerChoice = Convert.ToInt32(Console.ReadLine());

                if (playerChoice == 1)
                {
                    string newCard = ranks[random.Next(ranks.Length)];
                    Console.WriteLine(newCard);
                    int newplayerCardValue = cardValues[newCard];
                    if (newCard == "Ace")
                    {
                        aceCountpl++;
                    }

                    totalsumpl += newplayerCardValue;
                    Console.WriteLine($"Новая карта: {newCard}, новая сумма карт: {totalsumpl}");


                    if (totalsumpl > 21 && aceCountpl > 0)
                    {
                        Console.WriteLine("Согласно правил игры если в раздаче есть тузы" +
                            " и сумма очков более 21, любой из тузов можно считать за 1 очко.");
                        totalsumpl -= 10;
                        Console.WriteLine($"Сумма выпавших Вам карт: {totalsumpl}");
                        aceCountpl--;
                    }
                }
                else
                {
                    wantsMoreCards = false;
                }
            }

            if (totalsumpl > 21)
            {
                Console.WriteLine("Перебор! Вы проиграли.");
                    player.GameHistory.Add(new GameResult("Blackjack", 0, DateTime.Now));
                    windl = true;
            }
            else
            {
                Console.WriteLine(dillerCard1);
                Console.WriteLine(dillerCard2);
                Console.WriteLine($"Сумма выпавших Дилеру карт: {totalsumdl}");
                if (totalsumdl < 20 && totalsumdl < totalsumpl && totalsumpl <= 21)
                {
                    for (; totalsumdl < totalsumpl && totalsumdl < 20;)
                    {
                        string newCard = ranks[random.Next(ranks.Length)];
                        Console.WriteLine(newCard);
                        int newdillerrCardValue = cardValues[newCard];
                        if (newCard == "Ace")
                        {
                            aceCountdl++;
                        }
                        totalsumdl += newdillerrCardValue;
                        Console.WriteLine($"Новая карта: {newCard}, новая сумма карт: {totalsumdl}");
                    }
                }

                if (totalsumdl > 21)
                {
                    Console.WriteLine("Перебор! Дилер проиграл.");
                    Console.WriteLine("Поздравляю Вы победили!");
                    decimal winAmount = bet * 2;
                    accountManager.Deposit(player.Username, winAmount);
                    player.TransactionHistory.Add(new Transaction(winAmount, TransactionType.Deposit, DateTime.Now));

                    player.GameHistory.Add(new GameResult("Blackjack", winAmount, DateTime.Now));
                    Console.WriteLine($"Сумма Вашего выигрыша {winAmount}!");
                    winpl = true;
                    countWin++;
                }
            }
            if (!windl && !winpl) {
                if (totalsumpl > totalsumdl|| winpl)
                {
                    Console.WriteLine("Поздравляю Вы победили!");
                    decimal winAmount = bet * 2;
                    accountManager.Deposit(player.Username, winAmount);
                    player.TransactionHistory.Add(new Transaction(winAmount, TransactionType.Deposit, DateTime.Now));
                    player.GameHistory.Add(new GameResult("Blackjack", winAmount, DateTime.Now));
                    //player.GameHistory.Add(new GameResult("Blackjack", winAmount - bet, DateTime.Now));
                    Console.WriteLine($"Сумма Вашего выигрыша {winAmount}!");
                    countWin++;
                    }
                else if (totalsumpl == totalsumdl)
                {
                        Console.WriteLine("Сумма очков равна, ставка возвращается.");

                        accountManager.Deposit(player.Username, bet);
                }
                else
                {
                    Console.WriteLine("Победу одержал дилер.");
                    player.GameHistory.Add(new GameResult("Blackjack", 0, DateTime.Now));
                    }
            }
                if (countWin == 5) 
                {
                    player.Balance += 200;
                    Console.WriteLine($"Вы получили бонус за 5 побед, Ваш новый баланс - {player.Balance} ");
                }
                else if(countWin == 10)
                {
                    player.Balance += 200;
                    Console.WriteLine($"Вы получили бонус за 5 побед, Ваш новый баланс - {player.Balance} ");
                }
                if (countWin / countGame >= 0.5f && countGame>5)
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
            Console.WriteLine("Правила Блэкджека:");
            Console.WriteLine("1. Цель игры - набрать количество очков, максимально приближенное к 21," +
                " но не превышающее его.");
            Console.WriteLine("2. Все карты номиналом от 2 до 10 имеют значение, равное их номиналу." +
                " Валеты, дамы и короли стоят 10 очков. Туз может считаться как 1, так и 11 очков, в зависимости" +
                " от того, что лучше для игрока.");
            Console.WriteLine("3. Игра начинается с того, что каждому игроку и дилеру раздаются две карты.");
            Console.WriteLine("4. Игроки  решают, будут ли они брать дополнительные карты." +
                " Цель - улучшить свою руку, не превысив 21.");
            Console.WriteLine("5. Если у игрока после набора карт сумма очков превышает 21, игрок проигрывает." +
                " Если сумма очков у игрока 21 или ближе к 21, чем у дилера, не превышая его, игрок выигрывает.");
            Console.WriteLine("6. Выигрыш выплачивается 1 к 1");
            Console.WriteLine("7. В случае, если у дилера и игрока одинаковая сумма очков, объявляется ничья," +
                " и ставка возвращается игроку.");
        }

    }
}
