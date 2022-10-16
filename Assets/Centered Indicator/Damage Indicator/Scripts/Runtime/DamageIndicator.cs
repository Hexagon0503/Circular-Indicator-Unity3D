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
    private Dictionary<DamageIndicatorType, IndicatorUIData> savedUIData;

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
        BuildUIDataList();
    }
    #endregion

    #region METHODS
    /// <summary>
    /// 
    /// </summary>
    private void BuildUIDataList()
    {
        savedUIData = new Dictionary<DamageIndicatorType, IndicatorUIData>();
        for (int i = 0; i < indicatorUIDatas.Length; i++)
        {
            savedUIData.Add(indicatorUIDatas[i].Type, indicatorUIDatas[i]);
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
            runtimeIndicators[info.Sender].indicatorData.targetPosition = info.Direction;
            if (runtimeIndicators[info.Sender].Type != info.Type)
            {
                runtimeIndicators[info.Sender].indicatorData.uiPrefab = GetUIPrefab(info.Type);
                runtimeIndicators[info.Sender].indicatorData.panelID = savedUIData[info.Type].panelID;
            }
            indicatorManager.ReplaceIndicator(runtimeIndicators[info.Sender].ID, runtimeIndicators[info.Sender].indicatorData);
        }
        else
        {
            info.indicatorData = new BaseIndicatorData()
            {
                targetPosition = info.Direction,
                uiPrefab = GetUIPrefab(info.Type),
                panelID = savedUIData[info.Type].panelID,
                customData = info,
            };
            info.ID = indicatorManager.RegisterIndicator(info.indicatorData);
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
        if(!savedUIData.ContainsKey(indicatorType))
        {
            Debug.LogError($"Indicator [{indicatorType}] UI Does Not Exist!");
            return null;
        }
        return savedUIData[indicatorType].uiPrefab;
    }
    #endregion

    [System.Serializable]
    public class IndicatorUIData
    {
        public DamageIndicatorType Type;
        public string panelID;
        public IndicatorUIBase uiPrefab;
    }

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
}
