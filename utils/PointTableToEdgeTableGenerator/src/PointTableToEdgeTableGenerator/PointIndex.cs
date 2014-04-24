namespace PointTableToEdgeTableGenerator
{
    public enum PointIndex :
        byte
    {
        NxNyNz = 1,
        NxNyPz = 2,
        NxPyNz = 5,
        NxPyPz = 6,
        PxNyNz = 0,
        PxNyPz = 3,
        PxPyNz = 4,
        PxPyPz = 7
    }
}
