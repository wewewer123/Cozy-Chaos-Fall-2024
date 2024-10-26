using UnityEditor;
using UnityEngine;

public class DeadState : CropStageAbstract
{
    public DeadState(Crop crop) : base(crop)
    {
        crop.Destroy();
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
        return;
    }

    public override void Update()
    {
        return;
    }

    public override void TakeDamage(int damage)
    {

    }
}