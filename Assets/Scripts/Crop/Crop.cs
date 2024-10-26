using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum GrowthStage
{
    Germination,
    Vine,
    Blossom,
    Fruit,
    Ripe,
}
public class Crop : MonoBehaviour, ICollectable, IDamageable
{
    [HideInInspector] public CropStageAbstract growthStage { get; set; }
    [HideInInspector] public CropStageAbstract previousGrowthStage { get; set; }

    public CropManager cropManager;
    public event EventHandler OnCropDestroyed;

    public int Health { get; set; }

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        growthStage = new GerminationState(this);
    }


    private void Update()
    {
        growthStage.Update();
    }


    public void Collect()
    {
        Destroy(gameObject);
    }


    public void Destroy()
    {
        OnCropDestroyed?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject, 5);
    }

    public bool CanCollect()
    {
        return growthStage.CanCollect();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Crop under attack");
        growthStage.TakeDamage(damage);
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
