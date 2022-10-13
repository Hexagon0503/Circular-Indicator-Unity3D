using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IndicatorManager : MonoBehaviour 
{
    #region SERIALIZE FIELDS
    [Header("Settings")]
    [Range(1, 15)] public int frameCheckRate = 5;

    [Header("References")]
    [SerializeField] private IndicatorUIBase indicatorUIPrefab;
    [SerializeField] private Transform indicatorPanel;
    #endregion

    #region FIELDS
    /// <summary>
    /// 
    /// </summary>
    private Dictionary<string, RadialIndicatorData> runtimeIndicators = new Dictionary<string, RadialIndicatorData>();

    /// <summary>
    /// 
    /// </summary>
    private int currentFrameRate = 0;
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
        if (currentFrameRate == 0)
        {
            UpdateIndicators();
        }
        currentFrameRate = (currentFrameRate + 1) % frameCheckRate;
    }
    #endregion

    #region METHODS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public void AddIndicator(RadialIndicatorData info)
    {
        if (runtimeIndicators.ContainsKey(info.Sender))
        {
            if (runtimeIndicators[info.Sender].uiPrefab != info.uiPrefab)
            {
                runtimeIndicators[info.Sender].runtimeUI.Destroy();
                runtimeIndicators[info.Sender].runtimeUI = SpawnIndicatorUI(info);
            }
            else
            {
                runtimeIndicators[info.Sender].runtimeUI?.Show(info);
            }
        }
        else
        {
            info.runtimeUI = SpawnIndicatorUI(info);
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
            runtimeIndicators[sender].runtimeUI.Destroy();
            runtimeIndicators.Remove(sender);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void UpdateIndicators()
    {
        if (runtimeIndicators.Count < 1) return;
        //
        foreach (RadialIndicatorData indicator in runtimeIndicators.Values)
        {
            if (indicator.runtimeUI == null) continue;
            //
            Transform playerPos = Camera.main.transform;
            if (indicator.checkDistance)
            {
                indicator.runtimeUI.UpdateDistance(Vector3.Distance(playerPos.position, indicator.targetPosition));
            }

            //
            Vector3 forward = playerPos.forward;
            Vector3 rhs = indicator.targetPosition - playerPos.position;

            rhs.y = 0f;
            rhs.Normalize();

            float angle = Vector3.Angle(rhs, forward);
            Vector3 Perpendicular = Vector3.Cross(forward, rhs);
            float dot = -Vector3.Dot(Perpendicular, playerPos.up);
            angle = AngleCircumference(dot, angle);

            indicator.runtimeUI.SetAngle(angle, false, 15);
        }
    }
    #endregion

    #region FUNCTIONS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public IndicatorUIBase SpawnIndicatorUI(RadialIndicatorData info)
    {
        IndicatorUIBase ui = Instantiate(indicatorUIPrefab, indicatorPanel);
        ui.Show(info);
        return ui;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="dot"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float AngleCircumference(float dot, float angle)
    {
        float ac = angle;
        float circumference = 360f;
        ac = angle - 10;
        if (dot < 0)
        {
            ac = circumference - angle;
        }
        return ac;
    }
    #endregion
}