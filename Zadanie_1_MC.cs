using System;
using System.Linq;

namespace Zadanie_1_MichalCengel
{
    public class NespravnyVstupException : Exception
    {
        public NespravnyVstupException() : base() { }
        public NespravnyVstupException(string message) : base(message) { }
        public NespravnyVstupException(string message, Exception innerException) : base(message, innerException) { }
    }
    static class Keno10
    {
        private const int TOTAL_LOSOVANE = 20;
        private const int MIN_LOS_CISLO = 1;
        private const int MAX_LOS_CISLO = 80;
        static bool JeUnikatne(int[] pole, int index)
        {
            for (int i = 0; i < index; i++)
            {
                if (pole[i] == pole[index])
                    return false;
            }
            return true;
        }
        public static void VypisPola(int[] pole)
        {
            foreach (int cislo in pole)
                if (cislo != 0)
                    Console.Write(cislo + " ");
            Console.WriteLine();
        }
        public static int[] Losovanie()
        {
            int[] losovaneCisla = new int[TOTAL_LOSOVANE];
            Random rand = new Random();

            for (int i = 0; i < TOTAL_LOSOVANE; i++)
            {
                do
                {
                    losovaneCisla[i] = rand.Next(MIN_LOS_CISLO, MAX_LOS_CISLO + 1);
                } while (!JeUnikatne(losovaneCisla, i));
            }
            Console.Write("Boli vylosované čísla: ");
            VypisPola(losovaneCisla);
            return losovaneCisla;
        }
        public static int[] NacitajTipy()
        {
            int pocetTipov = 0;
            while (true)
            {
                Console.Write("Zadaj počet tipovaných čísel (od 1 do 10) pre túto hru KENO10: ");
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out pocetTipov))
                        throw new NespravnyVstupException("Nesprávny formát zadávaného čísla!");
                    if (!(1 <= pocetTipov && pocetTipov <= 10))
                        throw new NespravnyVstupException("Zadané číslo nie je z povoleného rozsahu!");
                }
                catch (NespravnyVstupException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                break;
            }
            int[] tipyCisla = new int[pocetTipov];
            Console.WriteLine("Povolené hodnoty sú od {0} do {1}.", Keno10.MIN_LOS_CISLO, Keno10.MAX_LOS_CISLO);
            for (int i = 0; i < pocetTipov; i++)
            {
                while (true) //do
                {
                    try
                    {
                        Console.Write("Zadaj {0}. tip: ", i + 1);
                        if (!int.TryParse(Console.ReadLine(), out tipyCisla[i]))
                            throw new NespravnyVstupException("Nesprávny formát zadávaného tipu!");
                        if (!(MIN_LOS_CISLO <= tipyCisla[i] && tipyCisla[i] <= MAX_LOS_CISLO))
                            throw new NespravnyVstupException("Zadaný tip nie je z povoleného rozsahu!");
                        if (!(JeUnikatne(tipyCisla, i)))
                            throw new NespravnyVstupException("Zadaný tip nie je unikátny!");
                        break;
                    }
                    catch (NespravnyVstupException ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }

                }// while (!(JeUnikatne(tipyCisla, i) && MIN_LOS_CISLO <= tipyCisla[i] && tipyCisla[i] <= MAX_LOS_CISLO));
            }
            Console.WriteLine();
            Console.Write("Zadal si tieto tipy: ");
            VypisPola(tipyCisla);
            return tipyCisla;
        }
        public static void Vyhodnotenie(int[] LosovaneCisla, int[] TipovaneCisla)
        {
            int[] Zhody = new int[0];

            foreach (int tip in TipovaneCisla)
            {
                for (int i = 0; i < LosovaneCisla.Length; i++)
                {
                    if (tip == LosovaneCisla[i])
                    {
                        Zhody.Append(tip);
                        continue;
                    }
                }
            }
            if (Zhody.Length == 0)
            {
                Console.WriteLine("Žiadne z tipovaných čísel nie je výherné.");
                return;
            }
            Console.Write("Počet výherných čísel je {0} a sú to tieto: ", Zhody.Length);
            VypisPola(Zhody);
            return;
        }
    }
    internal class Zadanie_1_MC
    {
        static void Main(string[] args)
        {
            int[] tipy = Keno10.NacitajTipy();
            int[] tah = Keno10.Losovanie();
            Keno10.Vyhodnotenie(tah, tipy);
            _ = Console.ReadKey();
        }
    }
}
