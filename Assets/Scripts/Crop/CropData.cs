using UnityEngine;

public class CropData : MonoBehaviour
{
    public int ExistTime = 0;
    public int ReadyTime = 0;
    public int MaxExistTime = 0;
    float DeltaTime = 0;

    private void FixedUpdate()
    {
        DeltaTime += Time.deltaTime;
        if (DeltaTime >= 1)
        {
            DeltaTime = DeltaTime - 1;
            ExistTime = ExistTime + 1;
        }
        if (ExistTime >= MaxExistTime || MaxExistTime == 0 || ReadyTime == 0)
        {
            ResetCrop();
        }
        else if (ExistTime < ReadyTime)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        else if (ExistTime >= ReadyTime)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
    }

    public bool CheckCollectCrop()
    {
        if (ExistTime >= ReadyTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetCrop()
    {
        ExistTime = 0;
        ReadyTime = Random.Range(5, 15);
        MaxExistTime = Random.Range(ReadyTime + 5, ReadyTime + 10);
    }
}