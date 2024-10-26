using UnityEngine;

public class RipeState : CropStageAbstract
{
    private CropGrowthData _cropGrowthData;
    private float _timeToTransitionToNextStage;

    public RipeState(Crop crop) : base(crop)
    {
        _cropGrowthData = Resources.Load<CropGrowthData>("CropGrowthData/Ripe");
        _timeToTransitionToNextStage = _cropGrowthData.timeToNextStage;
        crop.Health = _cropGrowthData.health;
        crop.SetColor(_cropGrowthData.cropColor);
        crop.cropManager.AddDamageableCrop(crop);
    }

    public override void TakeDamage(int damage)
    {
        crop.previousGrowthStage = crop.growthStage;
        crop.growthStage = new UnderAttackStage(crop);
        crop.TakeDamage(damage);
    }

    public override bool CanBeAttackedByCrows()
    {
        return true;
    }

    public override bool CanCollect()
    {
        return true;
    }

    public override void TransitionToNextStage()
    {
        crop.growthStage = new DeadState(crop);
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