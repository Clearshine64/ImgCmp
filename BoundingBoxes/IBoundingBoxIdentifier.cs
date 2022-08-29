using System.Collections.Generic;
using System.Drawing;

namespace ImgCmp
{
    internal interface IBoundingBoxIdentifier
    {
        IEnumerable<Rectangle> CreateBoundingBoxes(int[,] labelMap);
    }
}
