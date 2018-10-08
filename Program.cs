using System;
using System.Collections.Generic;
namespace Igra {
  public class Program {
    static void Main (string[] args) {
      string scentence = ""; // searched term
      char[] progress = new char[100]; // store the progress of guesses
      char letter; // inputed letter
      int numOfTry = 0;
      int numOfGood = 0; // good tries count
      int goodGuess = -1; // validate attempts variable (1 - good guess, (-1) - wrong, 0 - soulution found)

      List<char> history = new List<char> (); // List of tries letters
      Rules ();

      scentence = InputScentence (progress, scentence);
      numOfTry = CalcNumOfTry (scentence.Length);
      Console.WriteLine ();

      while (numOfTry > 0)
      {
        Console.Clear ();
        goodGuess = -1;

        PrintProgress (progress, scentence);

        Console.Write ("\n\nIskoristena slova: ");
        PrintHistory (history); // Already used letters
        Console.WriteLine ("\n\nPreostalo pokušaja: " + numOfTry); // Number of tries left

        letter = InputLetter (history);
        goodGuess = CompareLetter (progress, scentence, letter, numOfTry); // Is the letter correct and validate attempt

        if (goodGuess == 1) {
          numOfGood++;
          if (numOfGood % 3 == 0) numOfTry = numOfTry + 1;
        }
        else if (goodGuess == 0) numOfTry = -1;
        else numOfTry = numOfTry - 1;
      }

      Console.WriteLine ("\nIgra gotova");

      if (numOfTry == 0) // if the player lost, write game over
        Console.WriteLine ("\nIzgubili ste!\nTrazena recenica je bila: " + scentence);
        Console.ReadLine ();
    }

    // Function to write out guessed tries till present using _
    static void PrintProgress (char[] progress, string scentence) {
      for (int i = 0; i < scentence.Length; i++) {
        if (scentence[i] == ' ') Console.Write (progress[i] + " ");
        else Console.Write (progress[i] + " ");
      }
    }

    // Funkcija za unos pojma koji se pogadja i spremanje u niz progress pomocu _
    static string InputScentence (char[] progress, string scentence) {
      int count = 0;

      do {
        Console.WriteLine ("\nUnesite recenicu za pogađanje: ");
        scentence = Console.ReadLine ();
        if (scentence != String.Empty) //Ako je nesto uneseno
        {
          count = 0; // to check if the end is reached of the inputed string
          for (int i = 0; i < scentence.Length; i++) {
            if (scentence[i] == ' ') { // eliminate edge case
              if (i == 0) { // if space first, error
                Console.WriteLine ("\nRecenica pocinje razmakom.\nMolimo unesite samo velika ili mala slova");
                break;
              } else if (i == (scentence.Length - 1)) { // if space last, error
                Console.WriteLine ("\nRecenica zavrsava razmakom.\nMolimo unesite samo velika ili mala slova");
                break;
              } else {
                progress[i] = scentence[i];
                count++;
              }
            } else if ((scentence[i] >= 'A' && scentence[i] <= 'Z') || (scentence[i] >= 'a' && scentence[i] <= 'z')) {
              // inputed character is letter
              progress[i] = ('_'); // write _ to progress
              count++;
            } else {
              // non-alphabetic char inputed, error
              Console.WriteLine ("Recenica sadrzi nepodrzane znakove.\nMolimo unesite samo velika ili mala slova");
              break;
            }
          }
        } else {
          // if nothing inserted - count = -1, stay in the loop ask again
          Console.WriteLine ("Niste unijeli nista.\nMolimo unesite samo velika ili mala slova");
          count = -1;
        }
      } while (count != scentence.Length);
      return scentence;
    }

    // Function to input letter attempt
    static char InputLetter (List<char> history) {
      string letter1 = "";
      char letter;
      do {
        letter = '\0';
        Console.WriteLine ("\nUnesite slovo");
        letter1 = Console.ReadLine ();

        if (letter1.Length != 1) Console.WriteLine ("\nKRIVI UNOS!");
        else if (letter1.Length == 1) {
          letter = letter1[0];
          if (IsItLetter (letter) == 0) {
            if (history.Contains (Char.ToUpper (letter))) {
              // check if already attempted
              letter1 = ""; // if attempted put to '' to stay in the loo[]
              Console.WriteLine ("\nSlovo je vec uneseno! Unesite ponovno!");
            } else {
              //if not attempted
              history.Add (Char.ToUpper (letter)); // set to history of attempted
              history.Sort (); // sort for the better UX
            }
          } else letter1 = ""; // if not letter, return empty, stay in the loop
        }
      } while (letter1.Length != 1); // while string length is not 1
      return letter; // if all good, return letter
    }

    // Check if the char is letter
    static int IsItLetter (char letter) {
      if (char.IsLetter (letter)) return 0;
      else {
        Console.WriteLine ("\nUneseni znak nije slovo. Ponovite unos!\n");
        return -1;
      }
    }

    // Check if attempt is successful
    static int CompareLetter (char[] progress, string scentence, char letter, int numOfTry) {
      int goodGuess; // try validator (1 - good, -1 - bad, 0 - solution)
      string tmpProgress = "";
      goodGuess = -1; // initial assumption - wrong

      for (int i = 0; i < scentence.Length; i++) {
        if (Char.ToUpper (scentence[i]) == Char.ToUpper (letter)) {
          // if the inputed letter is equel to the some letter in the sentence
          progress[i] = scentence[i];
          goodGuess = 1; // attempt is good
        }
      }

      for (int i = 0; i < scentence.Length; i++) {
        tmpProgress += progress[i]; // needed for the check below
      }

      if (tmpProgress == scentence) {
        // if the temp progress is equel to the guessing sentence
        Console.Clear ();
        PrintProgress (progress, scentence); // congratulate
        Console.WriteLine ("\nCestitamo! Pobijedili ste!!");
        Console.WriteLine ("\nTrazena recenica je bila: " + scentence);
        goodGuess = 0; // Sentence is guessed
      }

      tmpProgress = "";
      return goodGuess;
    }

    // write out the game rules
    static void Rules () {
      Console.WriteLine ("\t\t***************************************");
      Console.WriteLine ("\t\t************ V J E S A L A ************");
      Console.WriteLine ("\t\t***************************************");
      Console.WriteLine ("\t\t*     Jedan igrac unosi recenicu,     *");
      Console.WriteLine ("\t\t*        a drugi igrac pogada.        *");
      Console.WriteLine ("\t\t*                                     *");
      Console.WriteLine ("\t\t*    Pogadate jedno po jedno slovo    *");
      Console.WriteLine ("\t\t*                                     *");
      Console.WriteLine ("\t\t* Svako koje pogodite vam se otkriva. *");
      Console.WriteLine ("\t\t*                                     *");
      Console.WriteLine ("\t\t*   Za svako koje pogriješite broj    *");
      Console.WriteLine ("\t\t*      pokusaja se smanjuje za 1      *");
      Console.WriteLine ("\t\t*                                     *");
      Console.WriteLine ("\t\t*    Za svako trece koje pogodite     *");
      Console.WriteLine ("\t\t*    broj pokusaja se poveca za 1     *");
      Console.WriteLine ("\t\t*                                     *");
      Console.WriteLine ("\t\t*   Ukoliko istrošite sve pokušaje    *");
      Console.WriteLine ("\t\t*           igra je gotova!           *");
      Console.WriteLine ("\t\t***************************************");
      Console.WriteLine ("\t\t***************************************");
    }

    // write out the tried letters
    static void PrintHistory (List<char> history) {
      for (int i = 0; i < history.Count; i++) {
        Console.Write (history[i] + " ");
      }

    }

    // Calculate the initial num of tries
    static int CalcNumOfTry (int length) {
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
