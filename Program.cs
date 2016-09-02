using System;
using System.Collections.Generic;

namespace Igra
{
    public class Program
    {

        static void Main(string[] args)
        {
            string scentence = ""; //Trazeni pojam
            char[] progress = new char[100]; //Niz u koji upisujemo napredak pogadanja
            char letter; //Uneseno slovo
            int numOfTry = 0; //Broj pokusaja
            int numOfGood = 0; //Counter tocnih pokusaja
            int goodGuess = -1; //Varijabla za validaciju pokusaja (1 - tocan pokusaj, (-1) - pogresan pokusaj, 0 - pogodeno konacno rijesenje)


            List<char> history = new List<char>(); //Lista pokusanih slova

            Rules(); //Ispisi pravila
             

            scentence = InputScentence(progress, scentence); //pojam = Unesi pojam

            numOfTry = CalcNumOfTry(scentence.Length); //Izracunaj pocetni broj pokusaja

            Console.WriteLine();

            while (numOfTry > 0) //Dok ima pokusaja
            {
                Console.Clear();
                goodGuess = -1;

                PrintProgress(progress, scentence); //Ispisi napredak

                Console.Write("\n\nIskoristena slova: ");
                PrintHistory(history); //Ispisi iskoristena slova
                Console.WriteLine("\n\nPreostalo pokušaja: " + numOfTry); //Ispisi preostale pokusaje



                letter = InputLetter(history); //Unesi slovo

                goodGuess = CompareLetter(progress, scentence, letter, numOfTry); //Provjeri je li uneseno tocno slovo i validiraj pokusaj

                if (goodGuess == 1) //Ako je pokusaj dobar
                {
                    numOfGood++; //Povecaj counter dobrih pokusaja
                    if (numOfGood % 3 == 0) //Ako je to treci po redu dobar pokusaj
                        numOfTry = numOfTry + 1; //Dodaj jedan 'free' pokusaj
                }
                else if (goodGuess == 0) //Ako je pojam pogoden
                    numOfTry = -1; //Stavi pokusaje na -1 da bi izasao iz petlje
                else //Ako je pokusaj pogresan
                    numOfTry = numOfTry - 1; //Smanji broj preostalih pokusaja

            }

            Console.WriteLine("\nIgra gotova");

            if (numOfTry == 0) //Ako je igrac izgubio (istrosio pokusaje) ispisi GAME OVER
                Console.WriteLine("\nIzgubili ste!\nTrazena recenica je bila: " + scentence);


            Console.ReadLine();




        }

        // Funkcija za ispis dosad pogodjenog pomocu _ i slova
        static void PrintProgress(char[] progress, string scentence)
        {
            //vrtimo petlju za ispis (petlja ide do broja unesih karaktera)
            for (int i = 0; i < scentence.Length; i++)
            {
                if (scentence[i] == ' ')
                {
                    Console.Write(progress[i] + " ");

                }
                else
                {
                    Console.Write(progress[i] + " ");
                }
            }
        }

        // Funkcija za unos pojma koji se pogadja i spremanje u niz progress pomocu _
        static string InputScentence(char[] progress, string scentence)
        {
            int count = 0;

            do
            {
                Console.WriteLine("\nUnesite recenicu za pogađanje: ");
                scentence = Console.ReadLine();
                if (scentence != String.Empty) //Ako je nesto uneseno
                {
                    count = 0; //counter za provjeru jesmo li dosli do kraja unesenog stringa

                    for (int i = 0; i < scentence.Length; i++) //Vrtimo petlju koliko ima znakova u unesenom pojmu
                    {

                        if (scentence[i] == ' ') // slucaj znak je razmak
                        {
                            if (i == 0) //ako je razmak prvi znak - error
                            {
                                Console.WriteLine("\nRecenica pocinje razmakom.\nMolimo unesite samo velika ili mala slova");
                                break;
                            }
                            else if (i == (scentence.Length - 1)) // ako je razmak zadnji znak - error
                            {
                                Console.WriteLine("\nRecenica zavrsava razmakom.\nMolimo unesite samo velika ili mala slova");
                                break;
                            }
                            else // ako je razmak unutar pojma - upisi ga u niz progress i povecaj counter
                            {
                                progress[i] = scentence[i];
                                count++;
                            } 

                        }
                        else if ((scentence[i] >= 'A' && scentence[i] <= 'Z') || (scentence[i] >= 'a' && scentence[i] <= 'z')) // slucaj zank je slovo
                        {
                            progress[i] = ('_'); //upisi _ u niz progress
                            count++;
                        }
                        else //slucaj znak je nealfabetni znak - error
                        {
                            Console.WriteLine("Recenica sadrzi nepodrzane znakove.\nMolimo unesite samo velika ili mala slova");
                            break;
                        }
                    }

                }
                else //Ako nije nista uneseno - count = -1 (ostani u petlji, pitaj unos ponovno)
                {
                    Console.WriteLine("Niste unijeli nista.\nMolimo unesite samo velika ili mala slova");
                    count = -1;
                }

            } while (count != scentence.Length); //Vrti dok nismo dosli do kraja pojma

            return scentence; //Vrati uneseni pojam (string)
        }

        //Funkcija za pokusaj (unos slova)
        static char InputLetter(List<char> history)
        {
            string letter1 = "";
            char letter;
            do
            {
                letter = '\0';
                Console.WriteLine("\nUnesite slovo");
                letter1 = Console.ReadLine();

                if (letter1.Length != 1) //Ako je uneseno vise ili nijedan znak - error
                    Console.WriteLine("\nKRIVI UNOS!");
                else if (letter1.Length == 1) //AKo je unesen jedan znak - nastavi
                {
                    letter = letter1[0]; //Spremi uneseni znak u char letter

                    
                    if (IsItLetter(letter)==0) //ako je uneseni znak slovo 
                    {
                        if (history.Contains(Char.ToUpper(letter))) //provjeri da li je slovo vec prije pokusano
                        {
                            letter1 = ""; //ako je slovo pokusano stavi string na Empty da bi ostao u petlji
                            Console.WriteLine("\nSlovo je vec uneseno! Unesite ponovno!");
                        }
                        else //ako slovo nije vec prije pokusano
                        {
                            history.Add(Char.ToUpper(letter)); //spremi slovo u listu history (lista vec pokusanih slova)
                            history.Sort(); //Sortiraj listu history radi preglednijeg ispisa
                        }
                    }
                    else //ako uneseni znak nije slovo stavi string na Empty da bi ostao u petlji
                        letter1 = ""; 
                }

            } while (letter1.Length != 1); //Vrti petlju dok duljina stringa nije 1


            return letter; //vrati uneseno slovo

        }

        //Funkcija provjere je li znak slovo
        static int IsItLetter(char letter)
        {

            if (char.IsLetter(letter)) //Ako je znak slovo vrati 0
            {
                return 0;

            }
            else //Ako nije ispisi gresku i vrati -1
            {
                Console.WriteLine("\nUneseni znak nije slovo. Ponovite unos!\n");
                return -1;
            }

        }

        //Funkcija provjere da li je pokusaj uspjesan
        static int CompareLetter(char[] progress, string scentence, char letter, int numOfTry)
        {

            int goodGuess; //Varijabla za validaciju pokusaja (1 - tocan pokusaj, (-1) - pogresan pokusaj, 0 - pogodeno konacno rijesenje)
            string tmpProgress = "";

            goodGuess = -1; //Pocetna pretpostavka - pogresan pokusaj

            for (int i = 0; i < scentence.Length; i++) //Vrti cijelokupan pojam
            {
                if (Char.ToUpper(scentence[i]) == Char.ToUpper(letter)) //Ako je uneseni znak jednak i-tom znaku pojma
                {
                    progress[i] = scentence[i]; //Zamijeni _ u nizu progress sa slovom

                    goodGuess = 1; //pokusaj je tocan

                }
            }


            for (int i = 0; i < scentence.Length; i++) //Vrti cijelokupan pojam
            {
                tmpProgress += progress[i]; //Puni string iz niza progress radi provjere ispod
            }

            if (tmpProgress == scentence) //Ako je trenutan progress jednak trazenom pojmu
            {
                Console.Clear();

                PrintProgress(progress, scentence); //Isprintaj progress, ispisi cestitku i ispis trazeni pojam

                Console.WriteLine("\nCestitamo! Pobijedili ste!!");
                Console.WriteLine("\nTrazena recenica je bila: " + scentence);
                goodGuess = 0; //Pogoden je cijelokupni pojam
            }

            tmpProgress = ""; //Vrati temp string na Empty

            return goodGuess; //Vrati validaciju pokusaja
        }

        //Funcija ispisa pravila igre
        static void Rules()
        {
            Console.WriteLine("\t\t***************************************");
            Console.WriteLine("\t\t************ V J E S A L A ************");
            Console.WriteLine("\t\t***************************************");
            Console.WriteLine("\t\t*     Jedan igrac unosi recenicu,     *");
            Console.WriteLine("\t\t*        a drugi igrac pogada.        *");
            Console.WriteLine("\t\t*                                     *");
            Console.WriteLine("\t\t*    Pogadate jedno po jedno slovo    *");
            Console.WriteLine("\t\t*                                     *");
            Console.WriteLine("\t\t* Svako koje pogodite vam se otkriva. *");
            Console.WriteLine("\t\t*                                     *");
            Console.WriteLine("\t\t*   Za svako koje pogriješite broj    *"); 
            Console.WriteLine("\t\t*      pokusaja se smanjuje za 1      *");
            Console.WriteLine("\t\t*                                     *");
            Console.WriteLine("\t\t*    Za svako trece koje pogodite     *");
            Console.WriteLine("\t\t*    broj pokusaja se poveca za 1     *");
            Console.WriteLine("\t\t*                                     *");
            Console.WriteLine("\t\t*   Ukoliko istrošite sve pokušaje    *");
            Console.WriteLine("\t\t*           igra je gotova!           *");
            Console.WriteLine("\t\t***************************************");
            Console.WriteLine("\t\t***************************************");
        }

        //Funkcija ispisa liste vec pokusanih slova
        static void PrintHistory(List<char> history)
        {
            for (int i = 0; i < history.Count; i++)
            {
                Console.Write(history[i] + " ");
            }


        }

        //Funkcija izracuna pocetnog broja pokusaj
        static int CalcNumOfTry(int length)
        {
            int numOfTry;

            if (length >= 1 && length <= 4)
                numOfTry = 2;
            else if (length >= 5 && length <= 8)
                numOfTry = 3;
            else if (length >= 9 && length <= 12)
                numOfTry = 4;
            else if (length >= 13 && length <= 15)
                numOfTry = 5;
            else if (length > 15)
                numOfTry = 6;
            else
                numOfTry = 0;

            return numOfTry;
        }

    }

}
