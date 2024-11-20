using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Blackjack Oyununa Hoş Geldiniz!");
            bool playAgain = true;

            while (playAgain)
            {
                PlayGame();
                Console.WriteLine("\nTekrar oynamak ister misiniz? (E/H)");
                playAgain = Console.ReadLine().ToLower() == "e";
            }

            Console.WriteLine("Oyun bitti. Teşekkürler!");
        }

        static void PlayGame()
        {
            List<int> playerCards = new List<int> { DrawCard(), DrawCard() };
            List<int> dealerCards = new List<int> { DrawCard(), DrawCard() };

            int playerTotal = CalculateTotal(playerCards);
            int dealerTotal = CalculateTotal(dealerCards);

            Console.WriteLine("\nSenin Kartların: " + string.Join(", ", playerCards) + $" (Toplam: {playerTotal})");
            Console.WriteLine("Krupiyerin görünen kartı: " + dealerCards[0]);

            // Oyuncunun turu
            while (true)
            {
                Console.WriteLine("\nKart çekmek istiyor musunuz? (E/H)");
                string choice = Console.ReadLine().ToLower();

                if (choice == "e")
                {
                    playerCards.Add(DrawCard());
                    playerTotal = CalculateTotal(playerCards);
                    Console.WriteLine("Senin Kartların: " + string.Join(", ", playerCards) + $" (Toplam: {playerTotal})");

                    if (playerTotal > 21)
                    {
                        Console.WriteLine("Bust! 21'i geçtiniz, kaybettiniz.");
                        return;
                    }
                }
                else
                {
                    break;
                }
            }

            // Krupiyerin turu
            Console.WriteLine("\nKrupiyerin Kartları: " + string.Join(", ", dealerCards) + $" (Toplam: {dealerTotal})");
            while (dealerTotal < 17)
            {
                dealerCards.Add(DrawCard());
                dealerTotal = CalculateTotal(dealerCards);
                Console.WriteLine("Krupiyer yeni kart çekti. Krupiyerin Kartları: " + string.Join(", ", dealerCards) + $" (Toplam: {dealerTotal})");
            }

            // Sonuçları değerlendirme
            if (dealerTotal > 21)
            {
                Console.WriteLine("Krupiyer bust yaptı! Kazandınız!");
            }
            else if (playerTotal > dealerTotal)
            {
                Console.WriteLine("Tebrikler! Kazandınız.");
            }
            else if (playerTotal == dealerTotal)
            {
                Console.WriteLine("Berabere!");
            }
            else
            {
                Console.WriteLine("Kaybettiniz! Krupiyer kazandı.");
            }
        }

        static int DrawCard()
        {
            int card = random.Next(1, 11); // Kartlar 1-10 arasında değer alır
            return card;
        }

        static int CalculateTotal(List<int> cards)
        {
            int total = 0;
            int aceCount = 0;

            foreach (int card in cards)
            {
                if (card == 1)
                {
                    aceCount++;
                    total += 11; // As ilk başta 11 sayılır
                }
                else
                {
                    total += card;
                }
            }

            // Eğer toplam 21'i aşıyorsa As'ları 1 olarak say
            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }
    }
}
