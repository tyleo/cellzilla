using System;

[Flags]
public enum EdgeFlags :
    ushort
{
    None = 0,

    PosYPosZ = 1 << 0,
    NegXPosZ = PosYPosZ << 1,
    NegYPosZ = NegXPosZ << 1,
    PosXPosZ = NegYPosZ << 1,
    PosYNegZ = PosXPosZ << 1,
    NegXNegZ = PosYNegZ << 1,
    NegYNegZ = NegXNegZ << 1,
    PosXNegZ = NegYNegZ << 1,
    PosXPosY = PosXNegZ << 1,
    NegXPosY = PosXPosY << 1,
    NegXNegY = NegXPosY << 1,
    PosXNegY = NegXNegY << 1,

    All =
        PosYPosZ |
        NegXPosZ |
        NegYPosZ |
        PosXPosZ |
        PosYNegZ |
        NegXNegZ |
        NegYNegZ |
        PosXNegZ |
        NegXPosY |
        NegXPosY |
        NegXNegY |
        PosXNegY
}
