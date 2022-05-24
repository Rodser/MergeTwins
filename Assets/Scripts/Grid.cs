using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Rodser.MergeTwins.Items;
using Rodser.MergeTwins.Grounds;

namespace Rodser.MergeTwins
{
	public class Grid : MonoBehaviour
	{        
        private int width = 1;
        private int height = 1;
        private CellAsset cellAsset = null;
        private ItemAsset startItemAsset = null;
        private int startCountItems = 1;        
        private float timeBetweenSpawn = 1f;
        private float timeToDefeat = 1f;
        private float spaceBetweenCells = 0f;

        private float currentTimeToDefeat = 0f;
        private Ground[] grid = null;
        private bool hasCompiled = false;
        private bool isFilled = false;

        public void BuildGrid(int width, int height, CellAsset cell, ItemAsset startItem, int startCount, float timeSpawn, float timeToDefeat)
        {
            this.hasCompiled = false;
            this.width = width;
            this.height = height;
            this.cellAsset = cell;
            this.startItemAsset = startItem;
            this.startCountItems = startCount;
            this.timeBetweenSpawn = timeSpawn;
            this.timeToDefeat = timeToDefeat;

            StartCoroutine(BuilderGridRoutine());
            StartCoroutine(SpawnItemsRoutine());
        }

        private void Update()
        {
            if (this.isFilled && Game.IsPlaying)
            {
                currentTimeToDefeat += Time.deltaTime;
                if (currentTimeToDefeat >= this.timeToDefeat)
                {
                    hasCompiled = false;
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
            
            SpawnItems(this.startCountItems);
            StartCoroutine(SpawnItemRepeatRoutine());
        }

        private IEnumerator SpawnItemRepeatRoutine()
        {
            while (hasCompiled)
            {
                yield return new WaitForSeconds(this.timeBetweenSpawn);
                SpawnItems(1);
            }
        }

        internal void SetSpace(float spaceBetweenCells)
        {
            this.spaceBetweenCells = spaceBetweenCells;
        }

        private IEnumerator BuilderGridRoutine()
        {
            this.grid = new Ground[this.height * this.width];

            int n = 0;
            for (int z = 0; z < this.height; z++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    CreateCell(x, z, n++);
                    yield return new WaitForSeconds(0.3f);
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

            cellAsset.Initialization(positionCell, this.transform, number);
            this.grid[number] = cellAsset.CurrentGround;
        }

        private void SpawnItems(int countItems)
        {
            List<Ground> freeCells = new List<Ground>();

            for (int i = 0; i < countItems; i++)
            {
                freeCells.AddRange(from Ground ground in this.grid
                                   where ground != null
                                   where ground.IsFree
                                   select ground);
                
                RandomSpawn(freeCells);
            }
        }

        private void RandomSpawn(List<Ground> grounds)
        {
            if (grounds.Count == 0)
            {
                this.isFilled = true;
                return;
            }

            this.isFilled = false;
            this.currentTimeToDefeat = 0f;

            int lucky = UnityEngine.Random.Range(0, grounds.Count);
            this.grid[grounds[lucky].Number].SpawnItem(this.startItemAsset);
        }

        internal void Remove()
        {
            if(this.grid is null)
            {
                return;
            }

            for (int i = 0; i < this.grid.Length; i++)
            {
                this.grid[i].Remove();
            }

            this.grid = null;
            this.isFilled = false;
        }
    }
}
