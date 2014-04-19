using System;

[Flags]
public enum PointFlags :
    byte
{
    None = 0,

    PosXPosYPosZ = 1 << 0,
    NegXPosYPosZ = PosXPosYPosZ << 1,
    NegXNegYPosZ = NegXPosYPosZ << 1,
    PosXNegYPosZ = NegXNegYPosZ << 1,
    PosXPosYNegZ = PosXNegYPosZ << 1,
    NegXPosYNegZ = PosXPosYNegZ << 1,
    NegXNegYNegZ = NegXPosYNegZ << 1,
    PosXNegYNegZ = NegXNegYNegZ << 1,

    All =
        PosXPosYPosZ |
        NegXPosYPosZ |
        NegXNegYPosZ |
        PosXNegYPosZ |
        PosXPosYNegZ |
        NegXPosYNegZ |
        NegXNegYNegZ |
        PosXNegYNegZ
}
