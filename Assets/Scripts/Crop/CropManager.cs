using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 0;
    [SerializeField] private GameObject cropPrefab;
    [SerializeField] private BoxCollider2D cropBoundary;
    [SerializeField] private float cropSquareUnit = 1;

    private Dictionary<Vector3Int, Crop> crops = new();


    private Crop[,] cropField;

    private void Start()
    {
        var bounds = cropBoundary.bounds;
        var left = bounds.min.x;
        var right = bounds.max.x;
        var bottom = bounds.min.y;
        var top = bounds.max.y;

        var width = right - left;
        var height = top - bottom;

        cropField = new Crop[(int)(width / cropSquareUnit), (int)(height / cropSquareUnit)];
    }


    public void Interact(Vector3 position)
    {
        var posInt = ConvertToBlockPosition(position);
        var posGrid = ConvertToGridPosition(position);
        if (cropField[posGrid.x, posGrid.y] != null)
        {
            Debug.Log("Remove crop at " + posInt);
            RemoveCrop(posGrid);
        }
        else
        {
            Debug.Log("Plant crop at " + posInt);
            PlantCrop(posGrid);
        }
    }


    private void PlantCrop(Vector2Int position)
    {
        var pos = GridPositionToGridCenter(position);
        var crop = Instantiate(cropPrefab, pos, Quaternion.identity).GetComponent<Crop>();
        cropField[position.x, position.y] = crop;
    }

    public void RemoveCrop(Vector2Int position)
    {
        var crop = cropField[position.x, position.y];
        if (crop == null) return;
        crop.Collect();
        cropField[position.x, position.y] = null;
    }


    public static Vector3Int ConvertToBlockPosition(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), Mathf.FloorToInt(position.z));
    }

    private Vector2Int ConvertToGridPosition(Vector3 position)
    {
        var x = Mathf.FloorToInt((position.x - cropBoundary.bounds.min.x) / cropSquareUnit);
        var y = Mathf.FloorToInt((position.y - cropBoundary.bounds.min.y) / cropSquareUnit);
        return new Vector2Int(x, y);
    }

    private Vector3 GridPositionToGridCenter(Vector2Int position)
    {
        return new Vector3(cropBoundary.bounds.min.x + position.x * cropSquareUnit + cropSquareUnit / 2, cropBoundary.bounds.min.y + position.y * cropSquareUnit + cropSquareUnit / 2, 0);
    }
}
