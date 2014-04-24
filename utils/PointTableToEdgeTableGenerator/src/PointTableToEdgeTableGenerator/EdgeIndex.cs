namespace PointTableToEdgeTableGenerator
{
    public enum EdgeIndex :
        ushort
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

    public static class EdgeIndexExtensions
    {

        public static string ToShortString(this EdgeIndex @this)
        {
            switch (@this)
            {
                case EdgeIndex.ZxNyNz:
                    return "ZxNyNz";
                case EdgeIndex.ZxNyPz:
                    return "ZxNyPz";
                case EdgeIndex.ZxPyNz:
                    return "ZxPyNz";
                case EdgeIndex.ZxPyPz:
                    return "ZxPyPz";
                case EdgeIndex.NxZyNz:
                    return "NxZyNz";
                case EdgeIndex.PxZyNz:
                    return "PxZyNz";
                case EdgeIndex.NxZyPz:
                    return "NxZyPz";
                case EdgeIndex.PxZyPz:
                    return "PxZyPz";
                case EdgeIndex.NxNyZz:
                    return "NxNyZz";
                case EdgeIndex.NxPyZz:
                    return "NxPyZz";
                case EdgeIndex.PxNyZz:
                    return "PxNyZz";
                case EdgeIndex.PxPyZz:
                    return "PxPyZz";
                default:
                    throw new System.Exception();
            }
        }
    }
}
