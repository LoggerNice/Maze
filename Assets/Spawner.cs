using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Cell WallPrefab;
    public GameObject ExitPrefab;
    public Vector3 SizeCell = new Vector3(1,1,0);

    public Maze map;

    private void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        map = generator.GenerateMaze();

        for (int x = 0; x < map.cells.GetLength(0); x++)
        {
            for (int y = 0; y < map.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(WallPrefab, new Vector3(x * SizeCell.x, y * SizeCell.y, y * SizeCell.z), Quaternion.identity);

                c.WallLeft.SetActive(map.cells[x, y].WallLeft);
                c.WallBottom.SetActive(map.cells[x, y].WallBottom);
                
            }
        }
        
        Instantiate(ExitPrefab, new Vector3(map.finishPosition.x, map.finishPosition.y, SizeCell.z), Quaternion.identity);
    }
}