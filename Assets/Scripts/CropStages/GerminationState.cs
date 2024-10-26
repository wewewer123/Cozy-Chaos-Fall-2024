using UnityEngine;
using UnityEngine.UIElements;

public class GerminationState : CropStageAbstract
{

    private CropGrowthData _cropGrowthData;

    private float _timeToTransitionToNextStage;

    public GerminationState(Crop crop) : base(crop)
    {
        _cropGrowthData = Resources.Load<CropGrowthData>("CropGrowthData/Germination");
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
        crop.growthStage = new VineState(crop);
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