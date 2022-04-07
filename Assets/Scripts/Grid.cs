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
        
        [Header("Time")]
        [SerializeField] private float timeBetweenSpawn = 1f;
        
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
            
            SpawnItems(startCountItems);
            StartCoroutine(SpawnItemReapitRoutine());
        }

        private IEnumerator SpawnItemReapitRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenSpawn);
                SpawnItems(1);
            }
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

        private void SpawnItems(int countItems)
        {
            List<Cell> freeCells = new();

            for (int i = 0; i < countItems; i++)
            {
                freeCells.AddRange(from Cell cell in grid
                                   where cell.IsFree
                                   select cell);
                
                RandomSpawn(freeCells);
            }
        }

        private void RandomSpawn(List<Cell> cells)
        {
            if (cells.Count == 0)
            {
                return;
            }
            
            int lucky = UnityEngine.Random.Range(0, cells.Count);
            grid[cells[lucky].Number].SpawnItem(startItem);
        }
    }
}
