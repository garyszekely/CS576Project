using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Tile { Wall, Slime };

public class AdditionHallway : MonoBehaviour
{
    public GameObject wall;

    int x = 21;
    int y = 21;
    int start_x = 1;
    int start_y = 1;
    List<Tile>[,] grid;

    void Start()
    {
        grid = new List<Tile>[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (i == start_x && j == start_y)
                {
                    grid[i, j] = new List<Tile> { };

                }
                else
                {
                    grid[i, j] = new List<Tile> { Tile.Wall };
                }

            }
        }

        List<int[]> walls = new List<int[]> { new int[] { start_x + 1, start_y + 1 }, new int[] { start_x, start_y + 1 } };
        List<int[]> visited = new List<int[]> { new int[] { start_x, start_y } };
        while (walls.Count > 0)
        {
            int[] wall = walls[Random.Range(0, walls.Count - 1)];
            int count = 0;
            if (wall[0] > 1 && visited.Any<int[]>(e => e[0] == wall[0] - 1 && e[1] == wall[1]))
            {
                count += 1;
            }
            if (wall[0] < 19 && visited.Any<int[]>(e => e[0] == wall[0] + 1 && e[1] == wall[1]))
            {
                count += 1;
            }
            if (wall[1] > 1 && visited.Any<int[]>(e => e[0] == wall[0] && e[1] == wall[1] - 1))
            {
                count += 1;
            }
            if (wall[1] < 19 && visited.Any<int[]>(e => e[0] == wall[0] && e[1] == wall[1] + 1))
            {
                count += 1;
            }

            if (count == 1)
            {
                grid[wall[0], wall[1]] = new List<Tile> { };
                visited.Add(wall);
                if (wall[0] > 1)
                {
                    walls.Add(new int[] { wall[0] - 1, wall[1] });
                }
                if (wall[0] < 19)
                {
                    walls.Add(new int[] { wall[0] + 1, wall[1] });
                }
                if (wall[1] > 1)
                {
                    walls.Add(new int[] { wall[0], wall[1] - 1 });
                }
                if (wall[1] < 19)
                {
                    walls.Add(new int[] { wall[0], wall[1] + 1 });
                }
            }
            walls.RemoveAll(e => e[0] == wall[0] && e[1] == wall[1]);
        }

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (grid[i, j].Contains(Tile.Wall))
                {
                    GameObject.Instantiate(wall, new Vector3((i - 10) * 2, 2, (j - 10) * 2), Quaternion.identity);
                }
            }
        }
    }

    void Update()
    {

    }
}
