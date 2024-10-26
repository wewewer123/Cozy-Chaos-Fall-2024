using UnityEngine;

public class VineState : CropStageAbstract
{

    private CropGrowthData _cropGrowthData;

    private float _timeToTransitionToNextStage;

    public VineState(Crop crop) : base(crop)
    {
        _cropGrowthData = Resources.Load<CropGrowthData>("CropGrowthData/Vine");
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
        crop.growthStage = new BlossomState(crop);
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