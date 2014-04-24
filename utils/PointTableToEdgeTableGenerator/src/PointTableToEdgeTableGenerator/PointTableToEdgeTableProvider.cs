﻿using System.Collections.Generic;
using System.Linq;

namespace PointTableToEdgeTableGenerator
{
    public static class PointTableToEdgeTableProvider
    {
        private static ushort[] _pointFlagsToIntegralEdgeFlagsTable =
            new ushort[]
            {
                0x0  , 0x109, 0x203, 0x30a, 0x406, 0x50f, 0x605, 0x70c,
                0x80c, 0x905, 0xa0f, 0xb06, 0xc0a, 0xd03, 0xe09, 0xf00,
                0x190, 0x99 , 0x393, 0x29a, 0x596, 0x49f, 0x795, 0x69c,
                0x99c, 0x895, 0xb9f, 0xa96, 0xd9a, 0xc93, 0xf99, 0xe90,
                0x230, 0x339, 0x33 , 0x13a, 0x636, 0x73f, 0x435, 0x53c,
                0xa3c, 0xb35, 0x83f, 0x936, 0xe3a, 0xf33, 0xc39, 0xd30,
                0x3a0, 0x2a9, 0x1a3, 0xaa , 0x7a6, 0x6af, 0x5a5, 0x4ac,
                0xbac, 0xaa5, 0x9af, 0x8a6, 0xfaa, 0xea3, 0xda9, 0xca0,
                0x460, 0x569, 0x663, 0x76a, 0x66 , 0x16f, 0x265, 0x36c,
                0xc6c, 0xd65, 0xe6f, 0xf66, 0x86a, 0x963, 0xa69, 0xb60,
                0x5f0, 0x4f9, 0x7f3, 0x6fa, 0x1f6, 0xff , 0x3f5, 0x2fc,
                0xdfc, 0xcf5, 0xfff, 0xef6, 0x9fa, 0x8f3, 0xbf9, 0xaf0,
                0x650, 0x759, 0x453, 0x55a, 0x256, 0x35f, 0x55 , 0x15c,
                0xe5c, 0xf55, 0xc5f, 0xd56, 0xa5a, 0xb53, 0x859, 0x950,
                0x7c0, 0x6c9, 0x5c3, 0x4ca, 0x3c6, 0x2cf, 0x1c5, 0xcc ,
                0xfcc, 0xec5, 0xdcf, 0xcc6, 0xbca, 0xac3, 0x9c9, 0x8c0,
                0x8c0, 0x9c9, 0xac3, 0xbca, 0xcc6, 0xdcf, 0xec5, 0xfcc,
                0xcc , 0x1c5, 0x2cf, 0x3c6, 0x4ca, 0x5c3, 0x6c9, 0x7c0,
                0x950, 0x859, 0xb53, 0xa5a, 0xd56, 0xc5f, 0xf55, 0xe5c,
                0x15c, 0x55 , 0x35f, 0x256, 0x55a, 0x453, 0x759, 0x650,
                0xaf0, 0xbf9, 0x8f3, 0x9fa, 0xef6, 0xfff, 0xcf5, 0xdfc,
                0x2fc, 0x3f5, 0xff , 0x1f6, 0x6fa, 0x7f3, 0x4f9, 0x5f0,
                0xb60, 0xa69, 0x963, 0x86a, 0xf66, 0xe6f, 0xd65, 0xc6c,
                0x36c, 0x265, 0x16f, 0x66 , 0x76a, 0x663, 0x569, 0x460,
                0xca0, 0xda9, 0xea3, 0xfaa, 0x8a6, 0x9af, 0xaa5, 0xbac,
                0x4ac, 0x5a5, 0x6af, 0x7a6, 0xaa , 0x1a3, 0x2a9, 0x3a0,
                0xd30, 0xc39, 0xf33, 0xe3a, 0x936, 0x83f, 0xb35, 0xa3c,
                0x53c, 0x435, 0x73f, 0x636, 0x13a, 0x33 , 0x339, 0x230,
                0xe90, 0xf99, 0xc93, 0xd9a, 0xa96, 0xb9f, 0x895, 0x99c,
                0x69c, 0x795, 0x49f, 0x596, 0x29a, 0x393, 0x99 , 0x190,
                0xf00, 0xe09, 0xd03, 0xc0a, 0xb06, 0xa0f, 0x905, 0x80c,
                0x70c, 0x605, 0x50f, 0x406, 0x30a, 0x203, 0x109, 0x0
            };

        private static byte[][] _pointFlagsToIntegralEdgeIndicesTable =
            new byte[][]
            {
                new byte[] {},
                new byte[] { 8, 3, 0},
                new byte[] { 9, 0, 1},
                new byte[] { 8, 3, 1, 8, 1, 9},
                new byte[] {10, 1, 2},
                new byte[] { 8, 3, 0, 1, 2,10},
                new byte[] { 9, 0, 2, 9, 2,10},
                new byte[] { 3, 2, 8, 2,10, 8, 8,10, 9},
                new byte[] {11, 2, 3},
                new byte[] {11, 2, 0,11, 0, 8},
                new byte[] {11, 2, 3, 0, 1, 9},
                new byte[] { 2, 1,11, 1, 9,11,11, 9, 8},
                new byte[] {10, 1, 3,10, 3,11},
                new byte[] { 1, 0,10, 0, 8,10,10, 8,11},
                new byte[] { 0, 3, 9, 3,11, 9, 9,11,10},
                new byte[] { 8,10, 9, 8,11,10},
                new byte[] { 8, 4, 7},
                new byte[] { 3, 0, 4, 3, 4, 7},
                new byte[] { 1, 9, 0, 8, 4, 7},
                new byte[] { 9, 4, 1, 4, 7, 1, 1, 7, 3},
                new byte[] {10, 1, 2, 8, 4, 7},
                new byte[] { 2,10, 1, 0, 4, 7, 0, 7, 3},
                new byte[] { 4, 7, 8, 0, 2,10, 0,10, 9},
                new byte[] { 2, 7, 3, 2, 9, 7, 7, 9, 4, 2,10, 9},
                new byte[] { 2, 3,11, 7, 8, 4},
                new byte[] { 7,11, 4,11, 2, 4, 4, 2, 0},
                new byte[] { 3,11, 2, 4, 7, 8, 9, 0, 1},
                new byte[] { 2, 7,11, 2, 1, 7, 1, 4, 7, 1, 9, 4},
                new byte[] { 8, 4, 7,11,10, 1,11, 1, 3},
                new byte[] {11, 4, 7, 1, 4,11, 1,11,10, 1, 0, 4},
                new byte[] { 3, 8, 0, 7,11, 4,11, 9, 4,11,10, 9},
                new byte[] { 7,11, 4, 4,11, 9,11,10, 9},
                new byte[] { 9, 5, 4},
                new byte[] { 3, 0, 8, 4, 9, 5},
                new byte[] { 5, 4, 0, 5, 0, 1},
                new byte[] { 4, 8, 5, 8, 3, 5, 5, 3, 1},
                new byte[] { 2,10, 1, 9, 5, 4},
                new byte[] { 0, 8, 3, 5, 4, 9,10, 1, 2},
                new byte[] {10, 5, 2, 5, 4, 2, 2, 4, 0},
                new byte[] { 3, 4, 8, 3, 2, 4, 2, 5, 4, 2,10, 5},
                new byte[] {11, 2, 3, 9, 5, 4},
                new byte[] { 9, 5, 4, 8,11, 2, 8, 2, 0},
                new byte[] { 3,11, 2, 1, 5, 4, 1, 4, 0},
                new byte[] { 8, 5, 4, 2, 5, 8, 2, 8,11, 2, 1, 5},
                new byte[] { 5, 4, 9, 1, 3,11, 1,11,10},
                new byte[] { 0, 9, 1, 4, 8, 5, 8,10, 5, 8,11,10},
                new byte[] { 3, 4, 0, 3,10, 4, 4,10, 5, 3,11,10},
                new byte[] { 4, 8, 5, 5, 8,10, 8,11,10},
                new byte[] { 9, 5, 7, 9, 7, 8},
                new byte[] { 0, 9, 3, 9, 5, 3, 3, 5, 7},
                new byte[] { 8, 0, 7, 0, 1, 7, 7, 1, 5},
                new byte[] { 1, 7, 3, 1, 5, 7},
                new byte[] { 1, 2,10, 5, 7, 8, 5, 8, 9},
                new byte[] { 9, 1, 0,10, 5, 2, 5, 3, 2, 5, 7, 3},
                new byte[] { 5, 2,10, 8, 2, 5, 8, 5, 7, 8, 0, 2},
                new byte[] {10, 5, 2, 2, 5, 3, 5, 7, 3},
                new byte[] {11, 2, 3, 8, 9, 5, 8, 5, 7},
                new byte[] { 9, 2, 0, 9, 7, 2, 2, 7,11, 9, 5, 7},
                new byte[] { 0, 3, 8, 2, 1,11, 1, 7,11, 1, 5, 7},
                new byte[] { 2, 1,11,11, 1, 7, 1, 5, 7},
                new byte[] { 3, 9, 1, 3, 8, 9, 7,11,10, 7,10, 5},
                new byte[] { 9, 1, 0,10, 7,11,10, 5, 7},
                new byte[] { 3, 8, 0, 7,10, 5, 7,11,10},
                new byte[] {11, 5, 7,11,10, 5},
                new byte[] {10, 6, 5},
                new byte[] { 8, 3, 0,10, 6, 5},
                new byte[] { 0, 1, 9, 5,10, 6},
                new byte[] {10, 6, 5, 9, 8, 3, 9, 3, 1},
                new byte[] { 1, 2, 6, 1, 6, 5},
                new byte[] { 0, 8, 3, 2, 6, 5, 2, 5, 1},
                new byte[] { 5, 9, 6, 9, 0, 6, 6, 0, 2},
                new byte[] { 9, 6, 5, 3, 6, 9, 3, 9, 8, 3, 2, 6},
                new byte[] { 3,11, 2,10, 6, 5},
                new byte[] { 6, 5,10, 2, 0, 8, 2, 8,11},
                new byte[] { 1, 9, 0, 6, 5,10,11, 2, 3},
                new byte[] { 1,10, 2, 5, 9, 6, 9,11, 6, 9, 8,11},
                new byte[] {11, 6, 3, 6, 5, 3, 3, 5, 1},
                new byte[] { 0, 5, 1, 0,11, 5, 5,11, 6, 0, 8,11},
                new byte[] { 0, 5, 9, 0, 3, 5, 3, 6, 5, 3,11, 6},
                new byte[] { 5, 9, 6, 6, 9,11, 9, 8,11},
                new byte[] {10, 6, 5, 4, 7, 8},
                new byte[] { 5,10, 6, 7, 3, 0, 7, 0, 4},
                new byte[] { 5,10, 6, 0, 1, 9, 8, 4, 7},
                new byte[] { 4, 5, 9, 6, 7,10, 7, 1,10, 7, 3, 1},
                new byte[] { 7, 8, 4, 5, 1, 2, 5, 2, 6},
                new byte[] { 4, 1, 0, 4, 5, 1, 6, 7, 3, 6, 3, 2},
                new byte[] { 9, 4, 5, 8, 0, 7, 0, 6, 7, 0, 2, 6},
                new byte[] { 4, 5, 9, 6, 3, 2, 6, 7, 3},
                new byte[] { 7, 8, 4, 2, 3,11,10, 6, 5},
                new byte[] {11, 6, 7,10, 2, 5, 2, 4, 5, 2, 0, 4},
                new byte[] {11, 6, 7, 8, 0, 3, 1,10, 2, 9, 4, 5},
                new byte[] { 6, 7,11, 1,10, 2, 9, 4, 5},
                new byte[] { 6, 7,11, 4, 5, 8, 5, 3, 8, 5, 1, 3},
                new byte[] { 6, 7,11, 4, 1, 0, 4, 5, 1},
                new byte[] { 4, 5, 9, 3, 8, 0,11, 6, 7},
                new byte[] { 9, 4, 5, 7,11, 6},
                new byte[] {10, 6, 4,10, 4, 9},
                new byte[] { 8, 3, 0, 9,10, 6, 9, 6, 4},
                new byte[] { 1,10, 0,10, 6, 0, 0, 6, 4},
                new byte[] { 8, 6, 4, 8, 1, 6, 6, 1,10, 8, 3, 1},
                new byte[] { 9, 1, 4, 1, 2, 4, 4, 2, 6},
                new byte[] { 1, 0, 9, 3, 2, 8, 2, 4, 8, 2, 6, 4},
                new byte[] { 2, 4, 0, 2, 6, 4},
                new byte[] { 3, 2, 8, 8, 2, 4, 2, 6, 4},
                new byte[] { 2, 3,11, 6, 4, 9, 6, 9,10},
                new byte[] { 0,10, 2, 0, 9,10, 4, 8,11, 4,11, 6},
                new byte[] {10, 2, 1,11, 6, 3, 6, 0, 3, 6, 4, 0},
                new byte[] {10, 2, 1,11, 4, 8,11, 6, 4},
                new byte[] { 1, 4, 9,11, 4, 1,11, 1, 3,11, 6, 4},
                new byte[] { 0, 9, 1, 4,11, 6, 4, 8,11},
                new byte[] {11, 6, 3, 3, 6, 0, 6, 4, 0},
                new byte[] { 8, 6, 4, 8,11, 6},
                new byte[] { 6, 7,10, 7, 8,10,10, 8, 9},
                new byte[] { 9, 3, 0, 6, 3, 9, 6, 9,10, 6, 7, 3},
                new byte[] { 6, 1,10, 6, 7, 1, 7, 0, 1, 7, 8, 0},
                new byte[] { 6, 7,10,10, 7, 1, 7, 3, 1},
                new byte[] { 7, 2, 6, 7, 9, 2, 2, 9, 1, 7, 8, 9},
                new byte[] { 1, 0, 9, 3, 6, 7, 3, 2, 6},
                new byte[] { 8, 0, 7, 7, 0, 6, 0, 2, 6},
                new byte[] { 2, 7, 3, 2, 6, 7},
                new byte[] { 7,11, 6, 3, 8, 2, 8,10, 2, 8, 9,10},
                new byte[] {11, 6, 7,10, 0, 9,10, 2, 0},
                new byte[] { 2, 1,10, 7,11, 6, 8, 0, 3},
                new byte[] { 1,10, 2, 6, 7,11},
                new byte[] { 7,11, 6, 3, 9, 1, 3, 8, 9},
                new byte[] { 9, 1, 0,11, 6, 7},
                new byte[] { 0, 3, 8,11, 6, 7},
                new byte[] {11, 6, 7},
                new byte[] {11, 7, 6},
                new byte[] { 0, 8, 3,11, 7, 6},
                new byte[] { 9, 0, 1,11, 7, 6},
                new byte[] { 7, 6,11, 3, 1, 9, 3, 9, 8},
                new byte[] { 1, 2,10, 6,11, 7},
                new byte[] { 2,10, 1, 7, 6,11, 8, 3, 0},
                new byte[] {11, 7, 6,10, 9, 0,10, 0, 2},
                new byte[] { 7, 6,11, 3, 2, 8, 8, 2,10, 8,10, 9},
                new byte[] { 2, 3, 7, 2, 7, 6},
                new byte[] { 8, 7, 0, 7, 6, 0, 0, 6, 2},
                new byte[] { 1, 9, 0, 3, 7, 6, 3, 6, 2},
                new byte[] { 7, 6, 2, 7, 2, 9, 2, 1, 9, 7, 9, 8},
                new byte[] { 6,10, 7,10, 1, 7, 7, 1, 3},
                new byte[] { 6,10, 1, 6, 1, 7, 7, 1, 0, 7, 0, 8},
                new byte[] { 9, 0, 3, 6, 9, 3, 6,10, 9, 6, 3, 7},
                new byte[] { 6,10, 7, 7,10, 8,10, 9, 8},
                new byte[] { 8, 4, 6, 8, 6,11},
                new byte[] {11, 3, 6, 3, 0, 6, 6, 0, 4},
                new byte[] { 0, 1, 9, 4, 6,11, 4,11, 8},
                new byte[] { 1, 9, 4,11, 1, 4,11, 3, 1,11, 4, 6},
                new byte[] {10, 1, 2,11, 8, 4,11, 4, 6},
                new byte[] {10, 1, 2,11, 3, 6, 6, 3, 0, 6, 0, 4},
                new byte[] { 0, 2,10, 0,10, 9, 4,11, 8, 4, 6,11},
                new byte[] { 2,11, 3, 6, 9, 4, 6,10, 9},
                new byte[] { 3, 8, 2, 8, 4, 2, 2, 4, 6},
                new byte[] { 2, 0, 4, 2, 4, 6},
                new byte[] { 1, 9, 0, 3, 8, 2, 2, 8, 4, 2, 4, 6},
                new byte[] { 9, 4, 1, 1, 4, 2, 4, 6, 2},
                new byte[] { 8, 4, 6, 8, 6, 1, 6,10, 1, 8, 1, 3},
                new byte[] { 1, 0,10,10, 0, 6, 0, 4, 6},
                new byte[] { 8, 0, 3, 9, 6,10, 9, 4, 6},
                new byte[] {10, 4, 6,10, 9, 4},
                new byte[] { 9, 5, 4, 7, 6,11},
                new byte[] { 4, 9, 5, 3, 0, 8,11, 7, 6},
                new byte[] { 6,11, 7, 4, 0, 1, 4, 1, 5},
                new byte[] { 6,11, 7, 4, 8, 5, 5, 8, 3, 5, 3, 1},
                new byte[] { 6,11, 7, 1, 2,10, 9, 5, 4},
                new byte[] {11, 7, 6, 8, 3, 0, 1, 2,10, 9, 5, 4},
                new byte[] {11, 7, 6,10, 5, 2, 2, 5, 4, 2, 4, 0},
                new byte[] { 7, 4, 8, 2,11, 3,10, 5, 6},
                new byte[] { 4, 9, 5, 6, 2, 3, 6, 3, 7},
                new byte[] { 9, 5, 4, 8, 7, 0, 0, 7, 6, 0, 6, 2},
                new byte[] { 4, 0, 1, 4, 1, 5, 6, 3, 7, 6, 2, 3},
                new byte[] { 7, 4, 8, 5, 2, 1, 5, 6, 2},
                new byte[] { 4, 9, 5, 6,10, 7, 7,10, 1, 7, 1, 3},
                new byte[] { 5, 6,10, 0, 9, 1, 8, 7, 4},
                new byte[] { 5, 6,10, 7, 0, 3, 7, 4, 0},
                new byte[] {10, 5, 6, 4, 8, 7},
                new byte[] { 5, 6, 9, 6,11, 9, 9,11, 8},
                new byte[] { 0, 9, 5, 0, 5, 3, 3, 5, 6, 3, 6,11},
                new byte[] { 0, 1, 5, 0, 5,11, 5, 6,11, 0,11, 8},
                new byte[] {11, 3, 6, 6, 3, 5, 3, 1, 5},
                new byte[] { 1, 2,10, 5, 6, 9, 9, 6,11, 9,11, 8},
                new byte[] { 1, 0, 9, 6,10, 5,11, 3, 2},
                new byte[] { 6,10, 5, 2, 8, 0, 2,11, 8},
                new byte[] { 3, 2,11,10, 5, 6},
                new byte[] { 9, 5, 6, 3, 9, 6, 3, 8, 9, 3, 6, 2},
                new byte[] { 5, 6, 9, 9, 6, 0, 6, 2, 0},
                new byte[] { 0, 3, 8, 2, 5, 6, 2, 1, 5},
                new byte[] { 1, 6, 2, 1, 5, 6},
                new byte[] {10, 5, 6, 9, 3, 8, 9, 1, 3},
                new byte[] { 0, 9, 1, 5, 6,10},
                new byte[] { 8, 0, 3,10, 5, 6},
                new byte[] {10, 5, 6},
                new byte[] {11, 7, 5,11, 5,10},
                new byte[] { 3, 0, 8, 7, 5,10, 7,10,11},
                new byte[] { 9, 0, 1,10,11, 7,10, 7, 5},
                new byte[] { 3, 1, 9, 3, 9, 8, 7,10,11, 7, 5,10},
                new byte[] { 2,11, 1,11, 7, 1, 1, 7, 5},
                new byte[] { 0, 8, 3, 2,11, 1, 1,11, 7, 1, 7, 5},
                new byte[] { 9, 0, 2, 9, 2, 7, 2,11, 7, 9, 7, 5},
                new byte[] {11, 3, 2, 8, 5, 9, 8, 7, 5},
                new byte[] {10, 2, 5, 2, 3, 5, 5, 3, 7},
                new byte[] { 5,10, 2, 8, 5, 2, 8, 7, 5, 8, 2, 0},
                new byte[] { 9, 0, 1,10, 2, 5, 5, 2, 3, 5, 3, 7},
                new byte[] { 1,10, 2, 5, 8, 7, 5, 9, 8},
                new byte[] { 1, 3, 7, 1, 7, 5},
                new byte[] { 8, 7, 0, 0, 7, 1, 7, 5, 1},
                new byte[] { 0, 3, 9, 9, 3, 5, 3, 7, 5},
                new byte[] { 9, 7, 5, 9, 8, 7},
                new byte[] { 4, 5, 8, 5,10, 8, 8,10,11},
                new byte[] { 3, 0, 4, 3, 4,10, 4, 5,10, 3,10,11},
                new byte[] { 0, 1, 9, 4, 5, 8, 8, 5,10, 8,10,11},
                new byte[] { 5, 9, 4, 1,11, 3, 1,10,11},
                new byte[] { 8, 4, 5, 2, 8, 5, 2,11, 8, 2, 5, 1},
                new byte[] { 3, 2,11, 1, 4, 5, 1, 0, 4},
                new byte[] { 9, 4, 5, 8, 2,11, 8, 0, 2},
                new byte[] {11, 3, 2, 9, 4, 5},
                new byte[] { 3, 8, 4, 3, 4, 2, 2, 4, 5, 2, 5,10},
                new byte[] {10, 2, 5, 5, 2, 4, 2, 0, 4},
                new byte[] { 0, 3, 8, 5, 9, 4,10, 2, 1},
                new byte[] { 2, 1,10, 9, 4, 5},
                new byte[] { 4, 5, 8, 8, 5, 3, 5, 1, 3},
                new byte[] { 5, 0, 4, 5, 1, 0},
                new byte[] { 3, 8, 0, 4, 5, 9},
                new byte[] { 9, 4, 5},
                new byte[] { 7, 4,11, 4, 9,11,11, 9,10},
                new byte[] { 3, 0, 8, 7, 4,11,11, 4, 9,11, 9,10},
                new byte[] {11, 7, 4, 1,11, 4, 1,10,11, 1, 4, 0},
                new byte[] { 8, 7, 4,11, 1,10,11, 3, 1},
                new byte[] { 2,11, 7, 2, 7, 1, 1, 7, 4, 1, 4, 9},
                new byte[] { 3, 2,11, 4, 8, 7, 9, 1, 0},
                new byte[] { 7, 4,11,11, 4, 2, 4, 0, 2},
                new byte[] { 2,11, 3, 7, 4, 8},
                new byte[] { 2, 3, 7, 2, 7, 9, 7, 4, 9, 2, 9,10},
                new byte[] { 4, 8, 7, 0,10, 2, 0, 9,10},
                new byte[] { 2, 1,10, 0, 7, 4, 0, 3, 7},
                new byte[] {10, 2, 1, 8, 7, 4},
                new byte[] { 9, 1, 4, 4, 1, 7, 1, 3, 7},
                new byte[] { 1, 0, 9, 8, 7, 4},
                new byte[] { 3, 4, 0, 3, 7, 4},
                new byte[] { 8, 7, 4},
                new byte[] { 8, 9,10, 8,10,11},
                new byte[] { 0, 9, 3, 3, 9,11, 9,10,11},
                new byte[] { 1,10, 0, 0,10, 8,10,11, 8},
                new byte[] {10, 3, 1,10,11, 3},
                new byte[] { 2,11, 1, 1,11, 9,11, 8, 9},
                new byte[] {11, 3, 2, 0, 9, 1},
                new byte[] {11, 0, 2,11, 8, 0},
                new byte[] {11, 3, 2},
                new byte[] { 3, 8, 2, 2, 8,10, 8, 9,10},
                new byte[] { 9, 2, 0, 9,10, 2},
                new byte[] { 8, 0, 3, 1,10, 2},
                new byte[] {10, 2, 1},
                new byte[] { 8, 1, 3, 8, 9, 1},
                new byte[] { 9, 1, 0},
                new byte[] { 8, 0, 3},
                new byte[] {}
            };

        public static EdgeFlags GetEdgeFlagsFromPointFlags(PointFlags pointFlags)
        {
            return (EdgeFlags)_pointFlagsToIntegralEdgeFlagsTable[(byte)pointFlags];
        }

        public static IEnumerable<EdgeIndex> GetEdgeIndicesFromPointFlags(PointFlags pointFlags)
        {
            return
                from edgeIndex in _pointFlagsToIntegralEdgeIndicesTable[(byte)pointFlags]
                select (EdgeIndex)edgeIndex;
        }
    }
}
