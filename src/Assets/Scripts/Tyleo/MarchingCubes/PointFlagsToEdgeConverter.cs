namespace Tyleo.MarchingCubes
{
    public static class PointFlagsToEdgeConverter
    {
        private static EdgeFlags[] _pointFlagsToEdgeFlagsTable =
            new EdgeFlags[]
            {

            };

        private static EdgeIndex[][] _pointFlagsToEdgeIndicesTable =
            new EdgeIndex[][]
            {

            };

        public static EdgeFlags GetEdgeFlagsFromPointFlags(PointFlags pointFlags)
        {
            throw new System.NotImplementedException();
            return _pointFlagsToEdgeFlagsTable[(byte)pointFlags];
        }

        public static EdgeIndex[] GetEdgeIndicesFromPointFlags(PointFlags pointFlags)
        {
            throw new System.NotImplementedException();
            return _pointFlagsToEdgeIndicesTable[(byte)pointFlags];
        }
    }
}
