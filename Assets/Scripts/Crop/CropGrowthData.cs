using UnityEngine;

[CreateAssetMenu(fileName = "CropGrowthData", menuName = "Crop Growth Data")]
public class CropGrowthData : ScriptableObject
{
    public string stageName;
    public float timeToNextStage;
    public Color cropColor;
    public int health;
}