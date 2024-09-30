using UnityEngine;


public class MovementLogic : MonoBehaviour
{
    private const byte _JUMP_LENGHT = 5;


    private static Vector2Int[] _moveVector =
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
    };

    public static Vector2Int[] GetMovementCoord(Vector2Int position, byte[,] _map)
    {
        Vector2Int[] a = new Vector2Int[4];
        for (int i = 0; i < 4; i ++)
            a[i] = GetMovementCoordFromOr(position, (Move)i, _map);
        return a;
    }
    private static byte CheckOur(byte cell, byte our) //проверяет наш это чел или не наш
    {
        if (cell == our || cell == our + GameManager.MAX_COUNT_PL)
            return 1;
        else
            return 0;
    }

    public static Vector2Int GetMovementCoordFromOr(Vector2Int position, Move move, byte[,] map)
    {
        int ofs_y = 0, ofs_x = 0;
        int y = position[0];
        int x = position[1];
        print("Нажал: " + y + " " + x);
        //byte countComrade = 0;

        for(int i = 0; i < _JUMP_LENGHT; i++)
        {
            switch (move)
            {
                case Move.right:
                    ofs_x = 1;
                    break;
                case Move.left:
                    ofs_x = -1;
                    break;
                case Move.up:
                    ofs_y = -1;
                    break;
                case Move.down:
                    ofs_y = 1;
                    break;
            }
            y += ofs_y;
            x += ofs_x;
            print(y + " " + x);
            if (!CheckCoord(y, x))
            {
                break;
            }
            if (map[y, x] == (byte)TypeCell.empty)
            {
                return new Vector2Int(y, x);
            }
            if (CheckOur(map[y, x], map[position[0], position[1]]) == 1)
            {
                    break;
            }
        }
        return new Vector2Int(-1, -1);
    }

    public static bool CheckCell(int y, int x,  byte[,] map) //можно ли встать на этой клетке (проверил)
    {

        if (y < 0 || x < 0 || y >= GameManager.mapSize[0] || x >= GameManager.mapSize[1])
            return false;
        if (map[y, x] != (byte)TypeCell.empty)
            return false;

        return true;
    }




    public static byte[,] UpdateMap(byte[,] map, 
        Vector2Int oldPosition, Vector2Int newPosition)
    {
        if (Vector2Int.Distance(newPosition, oldPosition) > 1)
            map = UpdateOne(map, oldPosition);
        map = UpdateCross(map, newPosition);
        return map;
    }
    

    private static byte[,] UpdateCross(byte[,] map, Vector2Int Position)
    {
        foreach (Vector2Int vector in _moveVector)
            map = UpdateOne(map, Position + vector);
        return map;
    }

    private static bool CheckCoord(Vector2Int coord)
    {
        if (coord[0] < 0 ||
            coord[1] < 0 ||
            coord[0] >= GameManager.mapSize[0] ||
            coord[1] >= GameManager.mapSize[1])
            return false;
        
        return true;
    }

    private static bool CheckCoord(int y, int x)
    {
        return CheckCoord(new Vector2Int(y, x));
    }



    private static byte[,] UpdateOne(byte[,] map, Vector2Int p)
    {
        if (CheckCoord(p))
        {
            if (map[p[0], p[1]] == (byte)TypeCell.none ||
                map[p[0], p[1]] > 1 + GameManager.MAX_COUNT_PL
                )
                return map;
        }
        else return map;
        
        byte lider = 0;
        byte max = 0;
        byte maxPlayer = (byte)TypeCell.player + GameManager.MAX_COUNT_PL;
        for (byte type = 2; type < maxPlayer; type++)
        {
            byte count = 0;
            foreach (Vector2Int vector in _moveVector)
            {
                Vector2Int g = p + vector;
                if (CheckCoord(g))
                    count += CheckOur(map[g[0], g[1]], type);     
            }

            if (max < count)
            {
                max = count;
                lider = type;
            }
        }
        if (max > 2 && lider != 0)
        {
            if (map[p[0], p[1]] != lider)
            {
                GameManager.CellUpdate(p, (TypeCell)lider);
                map[p[0], p[1]] = lider;
                //Debug.Log("UpdateCross: " + p.ToString() + " max: " + max + " lider: " + lider);
                map = UpdateCross(map, p);
            }
        }
        return map;
    }


    public static byte[,] UpdateAll(byte[,] map)
    {
        for (int i = 0; i < GameManager.mapSize[0]; i++)
        {
            for (int j = 0; j < GameManager.mapSize[1]; j++)
            {
                map = UpdateOne(map, new Vector2Int(i,j));
            }  
        }
        return map;
    } 




    /*
     *  первая Y вторая X
     * 
     * 
     * */

}
