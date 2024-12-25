using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    #region SERIALIZE FIELDS
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private IndicatorUIData[] indicatorUIDatas;
    #endregion

    #region FIELDS
    /// <summary>
    /// 
    /// </summary>
    private Dictionary<DamageIndicatorType, IndicatorUIData> uiData_Dic;

    /// <summary>
    /// 
    /// </summary>
    private Dictionary<string, DamageIndicatorData> runtimeIndicators = new Dictionary<string, DamageIndicatorData>();

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
        Initialize();
    }
    #endregion

    #region METHODS
    /// <summary>
    /// 
    /// </summary>
    private void Initialize()
    {
        uiData_Dic = new Dictionary<DamageIndicatorType, IndicatorUIData>();
        for (int i = 0; i < indicatorUIDatas.Length; i++)
        {
            uiData_Dic.Add(indicatorUIDatas[i].Type, indicatorUIDatas[i]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public void AddIndicator(DamageIndicatorData info)
    {
        if (runtimeIndicators.ContainsKey(info.Sender))
        {
            runtimeIndicators[info.Sender].IndicatorData.targetPosition = info.Direction;
            if (runtimeIndicators[info.Sender].Type != info.Type)
            {
                runtimeIndicators[info.Sender].IndicatorData.uiPrefab = GetUIPrefab(info.Type);
                runtimeIndicators[info.Sender].IndicatorData.panelID = uiData_Dic[info.Type].panelID;
            }
            indicatorManager.ReplaceIndicator(runtimeIndicators[info.Sender].ID, runtimeIndicators[info.Sender].IndicatorData);
        }
        else
        {
            info.IndicatorData = new BaseIndicatorData()
            {
                targetPosition = info.Direction,
                uiPrefab = GetUIPrefab(info.Type),
                panelID = uiData_Dic[info.Type].panelID,
                customData = info,
            };
            info.ID = indicatorManager.RegisterIndicator(info.IndicatorData);
            runtimeIndicators.Add(info.Sender, info);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    public void RemoveIndicator(string sender)
    {
        if (runtimeIndicators.ContainsKey(sender))
        {
            indicatorManager.RemoveIndicator(runtimeIndicators[sender].ID);
            runtimeIndicators.Remove(sender);
        }
    }
    #endregion

    #region FUNCTIONS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="indicatorType"></param>
    /// <returns></returns>
    public IndicatorUIBase GetUIPrefab(DamageIndicatorType indicatorType)
    {
        if(!uiData_Dic.ContainsKey(indicatorType))
        {
            Debug.LogError($"Indicator [{indicatorType}] UI Does Not Exist!");
            return null;
        }
        return uiData_Dic[indicatorType].uiPrefab;
    }
    #endregion

    #region NESTED CLASS
    [System.Serializable]
    public class IndicatorUIData
    {
        public DamageIndicatorType Type;
        public string panelID;
        public IndicatorUIBase uiPrefab;
    }
    #endregion

    #region GETTER
    /// <summary>
    /// 
    /// </summary>
    private static DamageIndicator _instance = null;
    public static DamageIndicator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DamageIndicator>();
            }
            return _instance;
        }
    }
    #endregion
}
