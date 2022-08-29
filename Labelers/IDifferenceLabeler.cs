namespace ImgCmp
{
    internal interface IDifferenceLabeler
    {
        int[,] Label(bool[,] differenceMap);
    }
}