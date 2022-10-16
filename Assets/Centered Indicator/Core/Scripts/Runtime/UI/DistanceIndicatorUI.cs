using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceIndicatorUI : IndicatorUIBase
{
    #region SERIALIZE FIELDS
    [SerializeField] private GameObject rootUI;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Image indicatorIcon;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Graphic[] graphics;
    #endregion

    #region FIELDS
    /// <summary>
    /// 
    /// </summary>
    private float distanceAlpha;

    /// <summary>
    /// 
    /// </summary>
    private DistanceIndicatorData indicatorData;
    #endregion

    #region METHODS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public override void Show(BaseIndicatorData data)
    {
        if(data is DistanceIndicatorData)
        {
            indicatorData = (DistanceIndicatorData)data;
            indicatorIcon.sprite = indicatorData.Icon;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distance"></param>
    public override void UpdateDistance(float distance)
    {
        if (indicatorData == null) return;
        //
        if(distance >= indicatorData.minMaxRange.y)
        {
            rootUI.SetActive(false);
            return;
        }
        rootUI.SetActive(true);
        distanceAlpha = 1 - (distance / indicatorData.minMaxRange.y);
        for (int i = 0; i < graphics.Length; i++)
        {
            graphics[i].color = indicatorData.rangeColorGradient.Evaluate(distanceAlpha);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Destroy()
    {
        Destroy(gameObject);
    }
    #endregion

    #region FUNCTIONS
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override bool IsVisible()
    {
        return rootUI.activeSelf;
    }
    #endregion
}
