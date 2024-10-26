using System.Diagnostics;

public class UnderAttackStage : CropStageAbstract
{
    private CropGrowthData _cropGrowthData;
    public UnderAttackStage(Crop crop) : base(crop)
    {

    }

    public override bool CanBeAttackedByCrows()
    {
        return true;
    }

    public override bool CanCollect()
    {
        return false;
    }

    public override void TakeDamage(int damage)
    {
        crop.Health -= damage;
        if (crop.Health <= 0)
        {
            crop.growthStage = new DeadState(crop);
        }
    }

    public override void TransitionToNextStage()
    {
        var prevState = crop.previousGrowthStage;
        crop.previousGrowthStage = crop.growthStage;
        crop.growthStage = prevState;
        crop.cropManager.AddDamageableCrop(crop);
    }

    public override void Update()
    {
        return;
    }
}