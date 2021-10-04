using CoordinateSharp;
using DTN.AssessmentExam.Service;
using System;

namespace DTN.AssessmentExam
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse("1386285909025"));
            var tileSystemService = new TileSystemService();
            tileSystemService.LatLongToPixelXY(33.5524951, -94.5822016, 12, out int pixelX, out int pixelY);
            tileSystemService.PixelXYToTileXY(pixelX, pixelY, out int tileX, out int tileY);
            var quadKey = tileSystemService.TileXYToQuadKey(tileX, tileY, 12);
        }
    }
}
