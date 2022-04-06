using UnityEngine;

namespace MiniIT.Test
{
	public class Grid : MonoBehaviour
	{
        [Header("Setting up the grid")]
		[SerializeField] private int	 width = 3;
		[SerializeField] private int	 height = 3;
        [SerializeField] private float	 spaceBetweenCells = 2f;
		[SerializeField] private Cell	 prefabCell = null;

		private Cell[] grid = null;

		private void Awake()
        {
            GreateGrid();
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
        }

        private void CreateCell(int x, int z, int v)
        {
			Vector3 positionCell = new Vector3();
			positionCell.x = x * spaceBetweenCells;
			positionCell.y = 0f;
			positionCell.z = z * spaceBetweenCells;

			grid[v] = Instantiate<Cell>(prefabCell);			
			grid[v].transform.SetParent(transform, false);
            grid[v].transform.localPosition = positionCell;
		}
    }
}
