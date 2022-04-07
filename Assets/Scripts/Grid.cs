using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MiniIT.Test.Items;

namespace MiniIT.Test
{
	public class Grid : MonoBehaviour
	{
        [Header("Configuration of Grid")]
		[SerializeField] private int width = 3;
		[SerializeField] private int height = 3;
        [SerializeField] private float spaceBetweenCells = 2f;
		[SerializeField] private Cell cell = null;
        
        [Header("Start configuration")]
        [SerializeField] private Item startItem;
        [SerializeField] private int startCountItems;

        private Cell[] grid = null;
        private bool hasCompiled = false;

        private void Awake()
        {
            GreateGrid();
            StartCoroutine(SpawnItemsRoutine());
        }

        private IEnumerator SpawnItemsRoutine()
        {
            while (!hasCompiled)
            {
                yield return null;
            }
            SpawnItems();
        }

        private void GreateGrid()
        {
            grid = new Cell[height * width];

            for (int z = 0, i = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
            hasCompiled = true;
        }

        private void CreateCell(int x, int z, int number)
        {
            Vector3 positionCell = new Vector3
            {
                x = x * spaceBetweenCells,
                y = 0f,
                z = z * spaceBetweenCells
            };

            grid[number] = cell.CloneCell();
            grid[number].Number = number;
            grid[number].Initialization(positionCell, transform);
        }

        private void SpawnItems()
        {
            List<Cell> freeCells = new();

            for (int i = 0; i < startCountItems; i++)
            {
                freeCells.AddRange(from Cell cell in grid
                                   where cell.IsFree
                                   select cell);
                RandomSpawn(freeCells);
            }
        }

        private void RandomSpawn(List<Cell> cells)
        {
            int lucky = UnityEngine.Random.Range(0, cells.Count);
            grid[cells[lucky].Number].SpawnItem(startItem);
        }
    }
}
