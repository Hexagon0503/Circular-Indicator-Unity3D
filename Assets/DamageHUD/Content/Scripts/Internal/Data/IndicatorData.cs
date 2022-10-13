using UnityEngine;

[System.Serializable]
public class IndicatorData
{
    /// <summary>
    /// 
    /// </summary>
    public string Sender;

    /// <summary>
    /// 
    /// </summary>
    public DamageIndicatorType Type;

    /// <summary>
    /// 
    /// </summary>
    public Vector3 Direction;

    /// <summary>
    /// 
    /// </summary>
    public bool ShowDistance = false;

    /// <summary>
    /// 
    /// </summary>
    public IndicatorUIBase runtimeUI { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="indicatorType"></param>
    /// <param name="vector"></param>
    public IndicatorData(DamageIndicatorType indicatorType, Vector3 vector)
    {
        Type = indicatorType;
        Direction = vector;
    }

    /// <summary>
    /// 
    /// </summary>
    public IndicatorData()
    {
    }
}