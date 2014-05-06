namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Provides a conversion from point names to integers used as a convenient first step for
    /// creating the PointIndicex enum.
    /// </summary>
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
