using UnityEngine;

[System.Serializable]
public class BaseIndicatorData
{
    #region FIELDS
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
    public object customData;

    /// <summary>
    /// 
    /// </summary>
    public string panelID { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool checkDistance { get; set; }

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