namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Provides a conversion from edge names to integers so that they may be indexed from an array
    /// in a marching cube. This provides an increase in speed over a switch-case.
    /// </summary>
    public enum EdgeIndex :
        byte
    {
        ZxNyNz = 0,
        ZxNyPz = 2,
        ZxPyNz = 4,
        ZxPyPz = 6,
        NxZyNz = 9,
        PxZyNz = 8,
        NxZyPz = 10,
        PxZyPz = 11,
        NxNyZz = 1,
        NxPyZz = 5,
        PxNyZz = 3,
        PxPyZz = 7
    }
}
