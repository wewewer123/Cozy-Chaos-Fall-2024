

using UnityEngine;

public abstract class CropStageAbstract
{

    protected Crop crop;

    public CropStageAbstract(Crop crop)
    {
        this.crop = crop;
    }
    protected float timeToTransitionToNextState;

    public abstract void Update();

    public abstract void TransitionToNextStage();
    public abstract bool CanCollect();

    public abstract bool CanBeAttackedByCrows();
    public abstract void TakeDamage(int damage);

}