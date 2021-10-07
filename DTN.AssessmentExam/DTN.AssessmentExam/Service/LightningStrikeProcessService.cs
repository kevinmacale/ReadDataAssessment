using DTN.AssessmentExam.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DTN.AssessmentExam.Service
{
    /// <summary>
    /// Lighting strike process service
    /// </summary>
    public class LightningStrikeProcessService : ILightningStrikeProcessService
    {
        private const int LEVEL_OF_DETAILS = 12;
        /// <summary>
        /// This will process lighting strike
        /// </summary>
        public void ProcessLightningStrike(Action<AssetModel> printMessage, HashSet<string> currentRegisteredOwners)
        {
            var jsonHelperService = new JsonHelperService();
            var strikeModels = jsonHelperService.DeserializeJsonString<IEnumerable<StrikeModel>>("_strike.json");
            var assetModels = jsonHelperService.DeserializeAssetsToDictionary("_asset.json");

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
            foreach (var strikeModel in strikeModels)
            {
                var result = ProcessStrikeLocation(LEVEL_OF_DETAILS, currentRegisteredOwners, assetModels, tileSystemService, strikeModel);
                if (result != null)
                    printMessage?.Invoke(result);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// This will process the strike location
        /// </summary>
        /// <param name="assetModels"></param>
        /// <param name="tileSystemService"></param>
        /// <param name="levelOfDetails"></param>
        /// <param name="strikeModel"></param>
        public AssetModel ProcessStrikeLocation(int levelOfDetails, HashSet<string> currentRegisteredOwners, Dictionary<string, AssetModel> assetModels, TileSystemService tileSystemService, StrikeModel strikeModel)
        {
            tileSystemService.LatLongToPixelXY(strikeModel.latitude, strikeModel.longitude, levelOfDetails, out int pixelX, out int pixelY);
            tileSystemService.PixelXYToTileXY(pixelX, pixelY, out int tileX, out int tileY);
            var quadKey = tileSystemService.TileXYToQuadKey(tileX, tileY, levelOfDetails);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            if (assetModels.TryGetValue(quadKey, out AssetModel strikedAsset))
                if (!IsAssetOwnerAlreadyRegistered(strikedAsset.assetOwner, currentRegisteredOwners) && IsValidToRaiseAnAlert(strikeModel.flashType))
                {
                    stopwatch.Stop();
                    return strikedAsset;
                }

            return null;
        }

        /// <summary>
        /// Check if flash type was cloud to ground or cloud to cloud
        /// </summary>
        /// <param name="flashType"></param>
        /// <returns></returns>
        private bool IsValidToRaiseAnAlert(FlashTypeEnum flashType)
        {
            return flashType != FlashTypeEnum.Heartbeat;
        }

        /// <summary>
        /// Check if asset owner already received an strike
        /// </summary>
        /// <param name="assetOwner"></param>
        /// <returns></returns>
        private bool IsAssetOwnerAlreadyRegistered(string assetOwner, HashSet<string> currentRegisteredOwners)
        {
            if (string.IsNullOrEmpty(assetOwner))
            {
                Console.WriteLine("Invalid Asset Owner");
                return false;
            }

            return currentRegisteredOwners.Contains(assetOwner);
        }
    }
}
