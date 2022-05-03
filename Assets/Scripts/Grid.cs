using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Rodser.MergeTwins.Items;

namespace Rodser.MergeTwins
{
	public class Grid : MonoBehaviour
	{
        [SerializeField] private float spaceBetweenCells = 2f;
        
        private int width = 1;
        private int height = 1;
        private Cell cell = null;
        private ItemAsset startItemAsset = null;
        private int startCountItems = 1;        
        private float timeBetweenSpawn = 1f;
        private float timeToDefeat = 1f;

        private float currentTimeToDefeat = 0f;
        private Cell[] grid = null;
        private bool hasCompiled = false;
        private bool isFilled = false;
        
        public Cell[] CurrentGrid => grid;

        public void CreateGrid(int width, int height, Cell cell, ItemAsset startItem, int startCount, float timeSpawn, float timeToDefeat)
        {
            this.width = width;
            this.height = height;
            this.cell = cell;
            this.startItemAsset = startItem;
            this.startCountItems = startCount;
            this.timeBetweenSpawn = timeSpawn;
            this.timeToDefeat = timeToDefeat;

            this.BuilderGrid();
            StartCoroutine(this.SpawnItemsRoutine());
        }

        private void Update()
        {
            if (this.isFilled && Game.IsPlaying)
            {
                currentTimeToDefeat += Time.deltaTime;
                if (currentTimeToDefeat >= this.timeToDefeat)
                {
                    Game.GameOver();
                }
            }
        }

        private IEnumerator SpawnItemsRoutine()
        {
            while (!hasCompiled)
            {
                yield return null;
            }
            
            this.SpawnItems(this.startCountItems);
            StartCoroutine(this.SpawnItemRepeatRoutine());
        }

        private IEnumerator SpawnItemRepeatRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(this.timeBetweenSpawn);
                this.SpawnItems(1);
            }
        }

        private void BuilderGrid()
        {
            this.grid = new Cell[this.height * this.width];

            for (int z = 0, i = 0; z < this.height; z++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    this.CreateCell(x, z, i++);
                }
            }
            
            this.hasCompiled = true;
        }

        private void CreateCell(int x, int z, int number)
        {
            Vector3 positionCell = new Vector3
            {
                x = x * this.spaceBetweenCells,
                y = 0f,
                z = z * this.spaceBetweenCells
            };

            this.CurrentGrid[number] = this.cell.CloneCell();
            this.CurrentGrid[number].Number = number;
            this.CurrentGrid[number].Initialization(positionCell, this.transform);
        }

        private void SpawnItems(int countItems)
        {
            List<Cell> freeCells = new();

            for (int i = 0; i < countItems; i++)
            {
                freeCells.AddRange(from Cell cell in this.CurrentGrid
                                   where cell.IsFree
                                   select cell);
                
                this.RandomSpawn(freeCells);
            }
        }

        private void RandomSpawn(List<Cell> cells)
        {
            if (cells.Count == 0)
            {
                this.isFilled = true;
                return;
            }

            this.isFilled = false;
            this.currentTimeToDefeat = 0f;

            int lucky = UnityEngine.Random.Range(0, cells.Count);
            this.CurrentGrid[cells[lucky].Number].SpawnItem(this.startItemAsset);
        }
    }
}
