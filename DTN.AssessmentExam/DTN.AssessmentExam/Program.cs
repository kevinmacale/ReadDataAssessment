using DTN.AssessmentExam.Model;
using DTN.AssessmentExam.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DTN.AssessmentExam
{
    class Program
    {
        private static readonly HashSet<string> _strikedOwners = new HashSet<string>();
        static void Main(string[] args)
        {
            PrintMenu();
        }

        /// <summary>
        /// Print menu messages
        /// </summary>
        private static void PrintMenu()
        {
            Console.WriteLine("Press y to reload the data.");
            Console.WriteLine("Press x to exit. ");
            var enteredCharacter = Console.ReadLine();
            switch (enteredCharacter)
            {
                case "x":
                case "X":
                    Environment.Exit(0);
                    break;
                case "y":
                case "Y":
                    Start();
                    PrintMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    PrintMenu();
                    break;
            }
        }

        /// <summary>
        /// Initialize codes
        /// </summary>
        private static void Start()
        {
            var lightningStrikeProcessService = new LightningStrikeProcessService();
            lightningStrikeProcessService.ProcessLightningStrike(PrintAlertMessage, _strikedOwners);
        }

        /// <summary>
        /// Print message when strike recieved
        /// </summary>
        /// <param name="assetOwner"></param>
        /// <param name="assetName"></param>
        private static void PrintAlertMessage(AssetModel assetModel)
        {
            if (assetModel == null)
            {
                Console.WriteLine("Invalid Asset Model");
                return;
            }

            if (string.IsNullOrEmpty(assetModel.assetOwner))
            {
                Console.WriteLine("Invalid Asset Owner");
                return;
            }

            if (string.IsNullOrEmpty(assetModel.assetName))
            {
                Console.WriteLine("Invalid Asset Name");
                return;
            }

            _strikedOwners.Add(assetModel.assetOwner);
            Console.WriteLine($"Lighting Alert for {assetModel.assetOwner}:{assetModel.assetName}");
        }
    }
}
