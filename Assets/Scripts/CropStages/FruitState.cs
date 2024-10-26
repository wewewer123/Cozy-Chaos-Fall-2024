using UnityEngine;

public class FruitState : CropStageAbstract
{
    private CropGrowthData _cropGrowthData;
    private float _timeToTransitionToNextStage;

    public FruitState(Crop crop) : base(crop)
    {
        _cropGrowthData = Resources.Load<CropGrowthData>("CropGrowthData/Fruit");
        _timeToTransitionToNextStage = _cropGrowthData.timeToNextStage;
        crop.Health = _cropGrowthData.health;
        crop.SetColor(_cropGrowthData.cropColor);
        crop.cropManager.AddDamageableCrop(crop);
    }

    public override bool CanBeAttackedByCrows()
    {
        return true;
    }

    public override void TakeDamage(int damage)
    {
        crop.previousGrowthStage = crop.growthStage;
        crop.growthStage = new UnderAttackStage(crop);
        crop.TakeDamage(damage);
    }

    public override bool CanCollect()
    {
        return false;
    }

    public override void TransitionToNextStage()
    {
        crop.previousGrowthStage = crop.growthStage;
        crop.growthStage = new RipeState(crop);
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