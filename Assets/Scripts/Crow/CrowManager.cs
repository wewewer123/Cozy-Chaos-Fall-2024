using System.Collections;
using System.Collections.Generic;
using CozyChaos2024Fall;
using UnityEngine;

public class CrowManager : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 4f;
    public float peckingSpeed = 3f;
    [SerializeField] private BoxCollider2D spawnBoundary;
    [SerializeField] private GameObject crowPrefab;


    private List<Crow> _crows = new();

    private CropManager _cropManager;

    private void Start()
    {
        _cropManager = FindObjectOfType<CropManager>();
    }

    private float lastSpawnedTime = 0f;

    private void Update()
    {
        if (Time.time - lastSpawnedTime >= spawnDelay)
        {
            lastSpawnedTime = Time.time;
            var crop = _cropManager.GetDamageableCrop();
            if (crop != null)
            {
                SpawnCrow(crop);
            }
        }
    }

    private void SpawnCrow(Crop target)
    {
        var spawnLocation = GetRandomEdgeLocation();

        var crow = Instantiate(crowPrefab, spawnLocation, Quaternion.identity).GetComponent<Crow>();
        crow.target = target;
        crow.crowManager = this;
        _crows.Add(crow);
    }


    // Returns a random spawn location that is always on the edge of the collider
    public Vector2 GetRandomEdgeLocation()
    {
        var bounds = spawnBoundary.bounds;

        // Choose a random edge (top, bottom, left, or right)
        int edge = Random.Range(0, 4);
        var location = edge switch
        {
            // Top edge
            0 => new Vector2(Random.Range(bounds.min.x, bounds.max.x), bounds.max.y),
            // Bottom edge
            1 => new Vector2(Random.Range(bounds.min.x, bounds.max.x), bounds.min.y),
            // Left edge
            2 => new Vector2(bounds.min.x, Random.Range(bounds.min.y, bounds.max.y)),
            // Right edge
            3 => new Vector2(bounds.max.x, Random.Range(bounds.min.y, bounds.max.y)),
            _ => throw new System.ArgumentOutOfRangeException(),
        };
        return location;
    }
}