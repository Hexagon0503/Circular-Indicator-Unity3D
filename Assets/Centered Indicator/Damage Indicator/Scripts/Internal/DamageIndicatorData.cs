using UnityEngine;

[System.Serializable]
public class DamageIndicatorData
{
    #region PROPERTIES
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
    public BaseIndicatorData IndicatorData { get; set; }
    #endregion

    #region CONSTRUCT
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
    #endregion
}