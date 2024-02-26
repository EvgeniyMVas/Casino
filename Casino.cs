using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    internal class Casino
    {
        private AccountManager accountManager;
        private List<string> games = new List<string>() { "Blackjack", "Roulette" };

        public Casino(AccountManager accountManager) 
        {
            this.accountManager = accountManager;
        }
        public void ChooseGame()
        {

            bool playAgain = true;
            while (playAgain)
            {
                Console.WriteLine("Выберите игру:");
                for (int i = 0; i < games.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {games[i]}");
                }
                if (int.TryParse(Console.ReadLine(), out int x) && x >= 1 && x <= games.Count)
                {
                    switch (x)
                    {
                        case 1:
                            Console.WriteLine("Вы выбрали Blackjack");
                            Blackjack blackjackGame = new Blackjack(accountManager);
                            blackjackGame.Play();
                            break;
                        case 2:
                            Console.WriteLine("Вы выбрали Roulette");
                            Roulette rouletteGame = new Roulette(accountManager);
                            rouletteGame.Play();
                            break;
                       
                        default:
                            Console.WriteLine("Unknown number");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный выбор! Попробуйте снова.");
                    continue;
                }

                Console.WriteLine("Хотите выбрать новую игру? (1 - да/2 - нет)");
                if (!int.TryParse(Console.ReadLine(), out int response) || response != 1)
                {
                    playAgain = false;
                }
            }
        }
    }
}
