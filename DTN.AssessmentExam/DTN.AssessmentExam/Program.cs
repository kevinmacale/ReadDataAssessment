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
            var jsonHelperService = new JsonHelperService();
            var strikeModels = jsonHelperService.DeserializeJsonString<List<StrikeModel>>("_strike.json");
            var assetModels = jsonHelperService.DeserializeJsonString<List<AssetModel>>("_asset.json");

            if (strikeModels == null || !strikeModels.Any())
            {
                Console.WriteLine("Invalid Strike Resources");
                return;
            }

            if (assetModels == null || !assetModels.Any())
            {
                Console.WriteLine("Invalid Asset Resources");
                return;
            }

            var tileSystemService = new TileSystemService();
            var levelOfDetails = 12;
            foreach (var strikeModel in strikeModels)
            {
                tileSystemService.LatLongToPixelXY(strikeModel.latitude, strikeModel.longitude, levelOfDetails, out int pixelX, out int pixelY);
                tileSystemService.PixelXYToTileXY(pixelX, pixelY, out int tileX, out int tileY);
                var quadKey = tileSystemService.TileXYToQuadKey(tileX, tileY, levelOfDetails);

                var strikedAsset = assetModels.FirstOrDefault(x => x.quadKey == quadKey);
                if (strikedAsset != null && !IsAssetOwnerAlreadyRegistered(strikedAsset.assetOwner) && IsValidToRaiseAnAlert(strikeModel.flashType))
                {
                    _strikedOwners.Add(strikedAsset.assetOwner);
                    PrintAlertMessage(strikedAsset.assetOwner, strikedAsset.assetName);
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Print message when strike recieved
        /// </summary>
        /// <param name="assetOwner"></param>
        /// <param name="assetName"></param>
        private static void PrintAlertMessage(string assetOwner, string assetName)
        {
            if (string.IsNullOrEmpty(assetOwner))
            {
                Console.WriteLine("Invalid Asset Owner");
                return;
            }

            if (string.IsNullOrEmpty(assetName))
            {
                Console.WriteLine("Invalid Asset Name");
                return;
            }

            Console.WriteLine($"Lighting Alert for {assetOwner}:{assetName}");
        }

        /// <summary>
        /// Check if flash type was cloud to ground or cloud to cloud
        /// </summary>
        /// <param name="flashType"></param>
        /// <returns></returns>
        private static bool IsValidToRaiseAnAlert(FlashTypeEnum flashType)
        {
            return flashType != FlashTypeEnum.Heartbeat;
        }

        /// <summary>
        /// Check if asset owner already received an strike
        /// </summary>
        /// <param name="assetOwner"></param>
        /// <returns></returns>
        private static bool IsAssetOwnerAlreadyRegistered(string assetOwner)
        {
            if (string.IsNullOrEmpty(assetOwner))
            {
                Console.WriteLine("Invalid Asset Owner");
                return false;
            }

            return _strikedOwners.Contains(assetOwner);
        }
    }
}
