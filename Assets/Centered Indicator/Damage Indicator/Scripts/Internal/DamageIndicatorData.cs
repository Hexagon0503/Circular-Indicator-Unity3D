using UnityEngine;

[System.Serializable]
public class DamageIndicatorData
{
    /// <summary>
    /// 
    /// </summary>
    public string Sender;

    /// <summary>
    /// 
    /// </summary>
    public int ID;

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
    public BaseIndicatorData indicatorData { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_sender"></param>
    /// <param name="_indicatorType"></param>
    /// <param name="_direction"></param>
    public DamageIndicatorData(string _sender, DamageIndicatorType _indicatorType, Vector3 _direction)
    {
        Sender = _sender;
        Type = _indicatorType;
        Direction = _direction;
    }

    /// <summary>
    /// 
    /// </summary>
    public DamageIndicatorData()
    {
    }
}