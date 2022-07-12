using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    public int Width = 30;
    public int Height = 30;

    public Maze GenerateMaze()
    {
        MazeGeneratorCell[,] cells = new MazeGeneratorCell[Width, Height];

        for (int x = 0; x < cells.GetLength(0); x++) {
            for (int y = 0; y < cells.GetLength(1); y++) {
                cells[x, y] = new MazeGeneratorCell {X = x, Y = y};
            }
        }

        for (int x = 0; x < cells.GetLength(0); x++) {
            cells[x, Height - 1].WallLeft = false;
        }

        for (int y = 0; y < cells.GetLength(1); y++) {
            cells[Width - 1, y].WallBottom = false;
        }

        GenerateWay(cells);

        Maze map = new Maze();
        map.cells = cells;
        map.finishPosition = GenerateExit(cells);

        return map;
    }

    private void GenerateWay(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell firstPoint = maze[0, 0];
        firstPoint.Visited = true;
        firstPoint.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do {
            List<MazeGeneratorCell> cellBeside = new List<MazeGeneratorCell>();

            int x = firstPoint.X;
            int y = firstPoint.Y;

            if (x > 0 && !maze[x - 1, y].Visited) cellBeside.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) cellBeside.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].Visited) cellBeside.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].Visited) cellBeside.Add(maze[x, y + 1]);

            if (cellBeside.Count > 0) {
                MazeGeneratorCell chosen = cellBeside[Random.Range(0, cellBeside.Count)];
                RemoveWall(firstPoint, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                chosen.DistanceFromStart = firstPoint.DistanceFromStart + 1;
                firstPoint = chosen;
            }
            else {
                firstPoint = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell firstWall, MazeGeneratorCell secondWall)
    {
        if (firstWall.X == secondWall.X) {
            if (firstWall.Y > secondWall.Y) firstWall.WallBottom = false;
            else secondWall.WallBottom = false;
        }
        else {
            if (firstWall.X > secondWall.X) firstWall.WallLeft = false;
            else secondWall.WallLeft = false;
        }
    }

    private Vector2Int GenerateExit(MazeGeneratorCell[,] map)
    {
        MazeGeneratorCell exitPoint = map[0, 0];

        for (int x = 0; x < map.GetLength(0); x++) {
            if (map[x, Height - 2].DistanceFromStart > exitPoint.DistanceFromStart) exitPoint = map[x, Height - 2];
            if (map[x, 0].DistanceFromStart > exitPoint.DistanceFromStart) exitPoint = map[x, 0];
        }

        for (int y = 0; y < map.GetLength(1); y++) {
            if (map[Width - 2, y].DistanceFromStart > exitPoint.DistanceFromStart) exitPoint = map[Width - 2, y];
            if (map[0, y].DistanceFromStart > exitPoint.DistanceFromStart) exitPoint = map[0, y];
        }

        if (exitPoint.X == 0) exitPoint.WallLeft = false;
        else if (exitPoint.Y == 0) exitPoint.WallBottom = false;
        else if (exitPoint.X == Width - 2) map[exitPoint.X + 1, exitPoint.Y].WallLeft = false;
        else if (exitPoint.Y == Height - 2) map[exitPoint.X, exitPoint.Y + 1].WallBottom = false;

        return new Vector2Int(exitPoint.X, exitPoint.Y);
    }
}