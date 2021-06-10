using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SymptomCheckerRevision
{
    class Program
    {
        // Constants for readability.
        const int HEADACHE = 0;
        const int VOMITING = 1;
        const int CRAMP = 2;
        const int RASH = 3;
        const int COUGH = 4;
        const int FATIGUE = 5;
        const int NAUSEA = 6;
        const int DIZZINESS = 7;
        const int COLD = 8;
        const int CHESTPAIN = 9;
        const int INFECTION = 10;

        const int FLU = 0;
        const int HEALTHY = 1;
        const int ANTIBIOTIC = 2;
        const int PAINKILLER = 3;
        const int GASTRO = 4;
        static void Main(string[] args)
        {
            bool running = true;
            bool[] symptomsStatus = new bool[11]; // By default every entry is initialised false.
            string[] symptoms = { "headache", "vomiting", "cramp", "rash", "cough", "fatigue", "nausea", "dizziness", "cold", "chestpain", "infection" };

            // Diagnosis | flu/healthy/giveAntibiotics/givePainkiller/gastro
            bool[] results = new bool[5];

            while (running)
            {
                // Taking syptoms from user.
                Console.Write("Please enter your symptoms: ");
                symptomsStatus = getSymptoms(Console.ReadLine(), symptoms);

                // Calculate.
                results = infer(symptomsStatus);

                // Output
                Console.Write(printResults(results, symptoms, symptomsStatus));

                // Continue or quit?
                Console.Write("\nContinue? y/n: ");
                if (Console.ReadLine() == "n")
                {
                    running = false;
                }
                Console.Write("\n");
            }
        }

        public static bool[] getSymptoms(string input, string[] symptoms)
        {
            bool[] symptomsStatus = new bool[11]; // By default every entry is initialised false.

            // Check for symptom matches.
            int iterative = 0;
            foreach (string symptom in symptoms)
            {
                foreach (Match m in Regex.Matches(input, symptom))
                {
                    symptomsStatus[iterative] = true;
                }
                iterative++;
            }
            return symptomsStatus;
        }

        public static bool[] infer(bool[] symptomsStatus)
        {
            bool[] result = new bool[5]; // All results false. Note this logic follows what was lain out in the java infer function, though seems wrong.
            if (symptomsStatus[HEADACHE] && symptomsStatus[VOMITING])
            {
                result[FLU] = true;
            }
            if (symptomsStatus[INFECTION])
            {
                result[ANTIBIOTIC] = true;
            }
            else if (!symptomsStatus[INFECTION])
            {
                result[HEALTHY] = true;
            }
            if (symptomsStatus[HEADACHE])
            {
                result[PAINKILLER] = true;
            }
            if (symptomsStatus[NAUSEA] && symptomsStatus[FATIGUE])
            {
                result[GASTRO] = true;
            }
            return result;
        }

        public static string printResults(bool[] results, string[] symptomsNames, bool[] symptomsStatus)
        {
            string message = "\nYour Symptoms are:";
            int i = 0;
            foreach (string symptom in symptomsNames)
            {
                if (symptomsStatus[i])
                {
                    message += "\n" + symptomsNames[i];
                }
                i++;
            }

            message += "\n\n";

            if (results[FLU])
            {
                message += "\nYou have flu.\n";
            }
            if (results[HEALTHY])
            {
                message += "You are healthy.\n";
            }
            if (results[GASTRO])
            {
                message += "You have Gastroenteritis.\n";
            }
            if (results[ANTIBIOTIC])
            {
                message += "Antibiotics have been perscribed.\n";
            }
            if (results[PAINKILLER])
            {
                message += "Painkillers have been perscribed.\n";
            }

            return message;
        }
    }
}
