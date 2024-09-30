using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public Level(byte[,] map, int countPlayers, byte[] iScale)
    {
        this.map = map;
        this.countPlayers = countPlayers;
        this.iScale = iScale;
    }

    public byte[,] map;
    public int countPlayers;
    public byte[] iScale;
}

public static class Levels
{
    public static Level[] levels = new Level[]{

    new Level(
    new byte[11, 9] {
    {0,0,0,1,1,1,0,0,0},
    {0,0,1,1,5,1,1,0,0},
    {0,1,1,1,0,1,4,1,0},
    {1,1,3,0,0,0,1,1,1},
    {1,1,3,1,0,1,1,1,1},
    {1,1,3,1,0,1,4,1,1},
    {0,5,1,1,2,1,1,1,0},
    {0,0,1,2,2,2,1,0,0},
    {0,0,0,1,1,2,0,0,0},
    {0,0,0,1,1,4,0,0,0},
    {0,0,0,1,1,1,0,0,0},
    },
    countPlayers: 3,
    iScale: new byte[2] { 2, 2 }),

    new Level(
    new byte[8, 8] {
    {1,1,1,0,0,1,1,1},
    {1,1,1,0,0,1,1,1},
    {0,0,1,1,1,1,0,0},
    {0,0,3,1,1,1,0,0},
    {0,0,3,1,1,1,0,0},
    {0,0,3,1,1,1,0,0},
    {1,5,1,0,0,1,1,1},
    {1,1,1,0,0,2,1,1},
    },
    countPlayers: 2,
    iScale: new byte[1] { 2 }),

    new Level(
    new byte[3, 4] {
    {2,1,2,1},
    {2,1,3,2},
    {1,2,1,1},
    },
    countPlayers: 2,
    iScale: new byte[1] { 1 })
        
    };
}