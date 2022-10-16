using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IndicatorManager : MonoBehaviour 
{
    #region SERIALIZE FIELDS
    [Header("Settings")]
    [SerializeField] private bool useFrameDelay;
    [SerializeField, Range(1, 15)] private int frameCheckRate = 5;

    [Header("References")]
    [SerializeField] private IndicatorUIBase indicatorUIPrefab;
    [SerializeField] private Transform indicatorPanel;
    [SerializeField] private IndicatorPanelDictionary customPanels;

    [System.Serializable]
    public class IndicatorPanelDictionary : SerializableDictionary<string, RectTransform> { }
    #endregion

    #region FIELDS
    /// <summary>
    /// 
    /// </summary>
    public Transform LocalPlayer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    private Dictionary<int, BaseIndicatorData> runtimeIndicators = new Dictionary<int, BaseIndicatorData>();

    /// <summary>
    /// 
    /// </summary>
    private int currentFrameRate = 0;

    /// <summary>
    /// 
    /// </summary>
    private int registerCount;
    #endregion

    #region UNITY METHODS
    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (runtimeIndicators.Count < 1)
        {
            currentFrameRate = 0;
            return;
        }
        if (useFrameDelay)
        {
            if(currentFrameRate == 0)
            {
                UpdateIndicators();
            }
            currentFrameRate = (currentFrameRate + 1) % frameCheckRate;
            return;
        }
        UpdateIndicators();
    }
    #endregion

    #region METHODS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public int RegisterIndicator(BaseIndicatorData info)
    {
        int ID = registerCount;
        info.runtimeUI = SpawnIndicatorUI(info);
        runtimeIndicators.Add(ID, info);
        UpdateIndicator(runtimeIndicators[ID]);
        registerCount++;
        return ID;
    }

    public void ReplaceIndicator(int ID, BaseIndicatorData info)
    {
        if (runtimeIndicators.ContainsKey(ID))
        {
            if (runtimeIndicators[ID].uiPrefab != info.uiPrefab)
            {
                runtimeIndicators[ID].runtimeUI.Destroy();
                runtimeIndicators[ID].runtimeUI = SpawnIndicatorUI(info);
            }
            else
            {
                runtimeIndicators[ID].runtimeUI?.Show(info);
            }
        }
        else
        {
            RegisterIndicator(info);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    public void RemoveIndicator(int ID)
    {
        if (runtimeIndicators.ContainsKey(ID))
        {
            runtimeIndicators[ID].runtimeUI.Destroy();
            runtimeIndicators.Remove(ID);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void UpdateIndicators()
    {
        if (runtimeIndicators.Count < 1) return;
        //
        foreach (BaseIndicatorData indicator in runtimeIndicators.Values)
        {
            UpdateIndicator(indicator);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="indicator"></param>
    private float distance, angle, dot;
    private Vector3 forward, rhs, Perpendicular;

    private void UpdateIndicator(BaseIndicatorData indicator)
    {
        if (indicator.runtimeUI == null) return;
        //
        if (indicator.checkDistance)
        {
            distance = Vector3.Distance(LocalPlayer.position, indicator.targetPosition);
            indicator.runtimeUI.UpdateDistance(distance);
            if (!indicator.runtimeUI.IsVisible())
            {
                return;
            }
        }
        //
        forward = LocalPlayer.forward;
        rhs = indicator.targetPosition - LocalPlayer.position;

        rhs.y = 0f;
        rhs.Normalize();

        angle = Vector3.Angle(rhs, forward);
        Perpendicular = Vector3.Cross(forward, rhs);
        dot = -Vector3.Dot(Perpendicular, LocalPlayer.up);
        angle = AngleCircumference(dot, angle);

        indicator.runtimeUI.SetAngle(angle, false, 15);
    }
    #endregion

    #region FUNCTIONS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public IndicatorUIBase SpawnIndicatorUI(BaseIndicatorData info)
    {
        IndicatorUIBase ui = Instantiate(info.uiPrefab != null ? info.uiPrefab : indicatorUIPrefab, (!string.IsNullOrEmpty(info.panelID) && customPanels.ContainsKey(info.panelID)) ? customPanels[info.panelID] : indicatorPanel);
        ui.Show(info);
        return ui;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dot"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public const float circumference = 360f;
    public static float AngleCircumference(float dot, float angle)
    {
        if (dot < 0)
        {
            angle = circumference - angle;
        }
        return angle;
    }
    #endregion

    private static IndicatorManager _instance = null;
    public static IndicatorManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<IndicatorManager>();
            }
            return _instance;
        }
    }
}