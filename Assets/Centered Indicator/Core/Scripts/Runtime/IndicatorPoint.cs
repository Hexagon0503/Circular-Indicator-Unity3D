using UnityEngine;

public class IndicatorPoint : MonoBehaviour
{
    #region SERIALIZE FIELDS
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private DistanceIndicatorData indicatorData;
    #endregion

    #region FIELDS
    /// <summary>
    /// 
    /// </summary>
    private int ID = -1;

    /// <summary>
    /// 
    /// </summary>
    private IndicatorManager indicatorManager;
    #endregion

    #region UNITY METHODS
    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        indicatorManager = IndicatorManager.Instance;
        if (indicatorData.Target == null)
        {
            indicatorData.Target = transform;
        }
        indicatorData.checkDistance = true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        Register();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        Unregister();
    }
    #endregion

    #region METHODS
    //
    private void Register()
    {
        if (indicatorManager == null) return;
        //
        ID = indicatorManager.RegisterIndicator(indicatorData);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Unregister()
    {
        if (ID != -1)
        {
            if (indicatorManager)
            {
                indicatorManager.RemoveIndicator(ID);
            }
            ID = -1;
        }
    }
    #endregion
}
