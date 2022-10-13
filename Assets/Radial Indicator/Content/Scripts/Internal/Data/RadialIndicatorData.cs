using UnityEngine;

[System.Serializable]
public class RadialIndicatorData
{
    #region SERIALIZE FIELDS
    /// <summary>
    /// 
    /// </summary>
    public string Sender;

    /// <summary>
    /// 
    /// </summary>
    public IndicatorUIBase uiPrefab;

    /// <summary>
    /// 
    /// </summary>
    public Transform Target;

    /// <summary>
    /// 
    /// </summary>
    public bool checkDistance = false;
    #endregion

    #region FIELDS
    /// <summary>
    /// 
    /// </summary>
    public object customData;

    /// <summary>
    /// 
    /// </summary>
    public IndicatorUIBase runtimeUI { get; set; }

    /// <summary>
    /// 
    /// </summary>
    private Vector3 savedPosition;
    public Vector3 targetPosition
    {
        get
        {
            if (Target != null)
            {
                savedPosition = Target.position;
            }
            return savedPosition;
        }
        set => savedPosition = value;
    }
    #endregion
}