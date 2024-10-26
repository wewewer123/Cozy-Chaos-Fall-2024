using UnityEngine;

public enum GrowthStage
{
    Germination,
    Vine,
    Blossom,
    Fruit,
    Ripe,
}
public class Crop : MonoBehaviour, ICollectable
{
    [HideInInspector] public GrowthStage growthStage = GrowthStage.Germination;
    public CropManager cropManager;

    private float growthTimeSincePlanted = 0f;

    private void Update()
    {
        growthTimeSincePlanted += Time.deltaTime;
    }


    [SerializeField] private float[] stageTimers;
    private void UpdateStage()
    {
        if (growthTimeSincePlanted >= stageTimers[(int)growthStage])
        {
        }
    }

    public void Collect()
    {
        Destroy(gameObject);
    }
}
