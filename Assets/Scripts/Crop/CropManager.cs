using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 0;
    [SerializeField] private GameObject cropPrefab;
    [SerializeField] private BoxCollider2D cropBoundary;
    [SerializeField] private float cropSquareUnit = 1;

    private Dictionary<Vector2Int, Crop> damageableCrops = new();


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
        var posGrid = ConvertToGridPosition(position);
        if (cropField[posGrid.x, posGrid.y] != null && cropField[posGrid.x, posGrid.y].CanCollect())
        {
            Debug.Log("Remove crop at " + posGrid);
            CollectCrop(posGrid);
        }
        else
        {
            Debug.Log("Plant crop at " + posGrid);
            PlantCrop(posGrid);
        }
    }

    public Crop GetDamageableCrop()
    {
        if (damageableCrops.Count == 0) return null;
        var randomIndex = Random.Range(0, damageableCrops.Count);
        var randomCrop = damageableCrops.ElementAt(randomIndex);
        damageableCrops.Remove(randomCrop.Key);
        return randomCrop.Value;
    }

    public void AddDamageableCrop(Crop crop)
    {
        if (damageableCrops.ContainsKey(ConvertToGridPosition(crop.transform.position))) return;
        damageableCrops.Add(ConvertToGridPosition(crop.transform.position), crop);
    }


    private void PlantCrop(Vector2Int position)
    {
        var pos = GridPositionToGridCenter(position);
        var crop = Instantiate(cropPrefab, pos, Quaternion.identity).GetComponent<Crop>();
        crop.cropManager = this;
        cropField[position.x, position.y] = crop;
    }

    private void CollectCrop(Vector2Int position)
    {
        var crop = cropField[position.x, position.y];
        if (crop == null) return;
        if (!crop.CanCollect()) return;
        crop.Collect();
        cropField[position.x, position.y] = null;
    }


    private static Vector3Int ConvertToBlockPosition(Vector3 position)
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
