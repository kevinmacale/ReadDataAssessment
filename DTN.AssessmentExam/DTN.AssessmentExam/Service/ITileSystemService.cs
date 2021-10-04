namespace DTN.AssessmentExam.Service
{
    public interface ITileSystemService
    {
        double GroundResolution(double latitude, int levelOfDetail);
        void LatLongToPixelXY(double latitude, double longitude, int levelOfDetail, out int pixelX, out int pixelY);
        double MapScale(double latitude, int levelOfDetail, int screenDpi);
        uint MapSize(int levelOfDetail);
        void PixelXYToLatLong(int pixelX, int pixelY, int levelOfDetail, out double latitude, out double longitude);
        void PixelXYToTileXY(int pixelX, int pixelY, out int tileX, out int tileY);
        void QuadKeyToTileXY(string quadKey, out int tileX, out int tileY, out int levelOfDetail);
        void TileXYToPixelXY(int tileX, int tileY, out int pixelX, out int pixelY);
        string TileXYToQuadKey(int tileX, int tileY, int levelOfDetail);
    }
}