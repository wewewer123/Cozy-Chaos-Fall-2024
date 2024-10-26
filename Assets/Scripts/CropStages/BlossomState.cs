using UnityEngine;

public class BlossomState : CropStageAbstract
{
    private CropGrowthData _cropGrowthData;
    private float _timeToTransitionToNextStage;

    public BlossomState(Crop crop) : base(crop)
    {
        _cropGrowthData = Resources.Load<CropGrowthData>("CropGrowthData/Blossom");
        _timeToTransitionToNextStage = _cropGrowthData.timeToNextStage;
        crop.Health = _cropGrowthData.health;
        crop.SetColor(_cropGrowthData.cropColor);
    }

    public override void TakeDamage(int damage)
    {
        return;
    }

    public override bool CanBeAttackedByCrows()
    {
        return false;
    }

    public override bool CanCollect()
    {
        return false;
    }

    public override void TransitionToNextStage()
    {
        crop.previousGrowthStage = crop.growthStage;
        crop.growthStage = new FruitState(crop);
    }

    public override void Update()
    {
        _timeToTransitionToNextStage -= Time.deltaTime;
        if (_timeToTransitionToNextStage <= 0)
        {
            TransitionToNextStage();
        }
    }
}