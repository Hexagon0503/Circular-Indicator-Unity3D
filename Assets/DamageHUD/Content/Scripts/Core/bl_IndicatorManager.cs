using UnityEngine;
using System.Collections.Generic;

public class bl_IndicatorManager : MonoBehaviour {
    [Header("List")]
    public List<bl_Indicator> IndicatorsEntrys = new List<bl_Indicator>();
    [Header("Settings")]
    [Range(1,15)]
    [Tooltip("Time before fade sprite indicator.")]
    public float TimeToShow = 5;
    [Range(20, 100)]
    public float PivotSize = 20;
    [Range(0,70)]
    [SerializeField]private float Inclination = 10;
    public Vector2 SpriteSize = new Vector2(80, 25);
    public Color SpriteColor = Color.white;
    [Tooltip("Use smooth movement rotation?.")]
    public bool LerpMovement = true;
    [Tooltip("Use local position of camera or localPlayer Object as reference?")]
    public bool UseCameraReference = true;
    public bool ShowDistance = true;
    [Header("References")]
    [Tooltip("This can be the root of player or the camera player.")]
    [SerializeField]private Transform LocalPlayer;
    [SerializeField]private GameObject IndicatorUI;
    [Tooltip("RectTransform where indicators will be instantiate (Default Root Canvas)")]
    [SerializeField]private Transform PanelIndicator;

    #region UNITY METHODS

    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        //Just call if have at least a indicator
        if (IndicatorsEntrys.Count > 0)
        {
            ControllIndicators();
        }
    }
    #endregion

    #region METHODS
    /// <summary>
    /// when a new event called
    /// </summary>
    /// <param name="info">info of new indicator</param>
    void OnNewIndicator(bl_IndicatorInfo info)
    {
        //Apply globat settings
        info.PivotSize = PivotSize;
        info.Size = SpriteSize;
        info.ShowDistance = ShowDistance;
        //Determine if need create on new indicator or just update one existing
        //this is determined based on whether there is an indicator of the same sender
        //so, first check if have the same sender
        if (bl_IndicatorUtils.CheckIfHaveSender(info.Sender, IndicatorsEntrys))
        {
            //if have a sender, then get it from list for update.
            int id = bl_IndicatorUtils.GetSenderInList(info.Sender, IndicatorsEntrys);
            //If is update just show the half of time.
            info.TimeToShow = TimeToShow / 2;
            UpdateIndicator(info, id);
        }
        else//if not have a sender, them create a new indicator and cache this sender.
        {
            info.TimeToShow = TimeToShow;
            //If dont have asigne a color yet.
            if (info.Color == new Color(1, 1, 1, 0))
            {
                info.Color = SpriteColor;
            }
            CreateNewIndicator(info);
        }
    }

    /// <summary>
    /// Create a new indicator UI
    /// </summary>
    /// <param name="info"></param>
    void CreateNewIndicator(bl_IndicatorInfo info)
    {
        GameObject newentry = Instantiate(IndicatorUI) as GameObject;
        bl_Indicator indicator = newentry.GetComponent<bl_Indicator>();
        indicator.GetInfo(info,this);
        newentry.transform.SetParent(PanelIndicator, false);
        //cache the new indicator
        IndicatorsEntrys.Add(indicator);
    }

    /// <summary>
    /// If have a indicator of a same sender and this is available yet.
    /// them just update information.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="id"></param>
    void UpdateIndicator(bl_IndicatorInfo info,int id)
    {
        bl_Indicator indicator = IndicatorsEntrys[id];
        if(indicator == null)
        {
            Debug.LogWarning("Can't update indicator because this doesn't exit in list");
            return;
        }
        if (info.Color == new Color(1, 1, 1, 0))
        {
            info.Color = SpriteColor;
        }

        indicator.GetInfo(info,this,true);
    }

    /// <summary>
    /// Control direction of each Indicator in List
    /// </summary>
    void ControllIndicators()
    {
        for(int i = 0; i < IndicatorsEntrys.Count; i++)
        {
            bl_Indicator indicator = IndicatorsEntrys[i];
            //Remove nulls indicators in list
            if(indicator == null || indicator.Transform == null)
            {
                IndicatorsEntrys.Remove(indicator);
                return;
            }

            //If show distance
            if (indicator.Info.ShowDistance)
            {
                //Calculate distance from sender
                float d = Vector3.Distance(indicator.Info.Sender.transform.position, LocalPlayer.position);
                indicator.UpdateDistance(d);
            }
            Vector3 forward = Vector3.zero;
            //Get camera player or current camera
            if (UseCameraReference && bl_IndicatorUtils.UseCamera != null)
            {
                forward = bl_IndicatorUtils.UseCamera.transform.forward;
            }
            else
            {
                forward = LocalPlayer.forward;
            }
            //Calculate direction 
            Vector3 rhs = indicator.Info.Direction - LocalPlayer.position;
            Vector3 offset = indicator.Transform.localEulerAngles;
            //Convert angle into screen space
            rhs.y = 0f;
            rhs.Normalize();
            //Get the angle between two positions.
            float angle = Vector3.Angle(rhs, forward);
            //Calculate the perpendicular of both vectors
            //More information about this calculation: https://unity3d.com/es/learn/tutorials/modules/beginner/scripting/vector-maths-dot-cross-products?playlist=17117
            Vector3 Perpendicular = Vector3.Cross(forward, rhs);
            //Calculate magnitude between two vectors
            float dot = -Vector3.Dot(Perpendicular, LocalPlayer.up);
            //get the horizontal angle in direction of target / sender.
            angle = bl_IndicatorUtils.AngleCircumference(dot, angle);
            //Apply the horizontal rotation to the indicator.
            offset.z = angle;
            if (LerpMovement)
            {
                indicator.Transform.localRotation = Quaternion.Slerp(indicator.Transform.localRotation, Quaternion.Euler(offset), 17 * Time.deltaTime);
            }
            else
            {
                indicator.Transform.localRotation = Quaternion.Euler(offset);
            }
        }       
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="indicator"></param>
    public void RemoveIndicator(bl_Indicator indicator)
    {
        if (IndicatorsEntrys.Contains(indicator))
        {
            IndicatorsEntrys.Remove(indicator);
        }
        else
        {
            Debug.LogWarning("This indicator " + indicator.gameObject.name + " is not in list.");
        }
    }
    #endregion
}