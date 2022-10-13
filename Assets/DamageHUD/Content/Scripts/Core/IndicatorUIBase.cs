using UnityEngine;

public abstract class IndicatorUIBase : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public abstract void Show(IndicatorData data);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distance"></param>
    public abstract void UpdateDistance(float distance);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="angle"></param>
    public abstract void SetAngle(float angle);

    /// <summary>
    /// 
    /// </summary>
    public abstract void Destroy();
}
