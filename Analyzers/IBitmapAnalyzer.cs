using System.Drawing;

namespace ImgCmp
{
    internal interface IBitmapAnalyzer
    {
        bool[,] Analyze(Bitmap first, Bitmap second);
    }
}