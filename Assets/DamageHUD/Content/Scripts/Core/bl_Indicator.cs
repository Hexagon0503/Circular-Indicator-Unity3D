using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bl_Indicator : MonoBehaviour {

	[HideInInspector]public GameObject Sender = null;
    public bl_IndicatorInfo Info;
    public AnimationCurve FadeCurve;
    [SerializeField]private RectTransform m_Sprite;
    [SerializeField]private Text DistanceText = null;
    [SerializeField]private CanvasGroup Alpha;
    [SerializeField]private Animator Animater;
    private bl_IndicatorManager Manager = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <param name="manager"></param>
    /// <param name="update"></param>
    public void GetInfo(bl_IndicatorInfo info,bl_IndicatorManager manager, bool update = false)
    {
        //Apply global settings
        Info = info;
        Manager = manager;
        m_Sprite.sizeDelta = info.Size;
        Sender = info.Sender;
        m_Sprite.GetComponent<Image>().color = info.Color;
        Transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, info.PivotSize, 0);
        DistanceText.gameObject.SetActive((info.ShowDistance) ? true : false);
        //If update reset current process and restart.
        if (update)
        {
            StopAllCoroutines();
            Alpha.alpha = 1;
        }
        //Play push animation if update or not.
        if (Animater != null) { Animater.Play("IndicatorPush",0,0); }
        //Play countdown for fade.
        StartCoroutine(Fade());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distance"></param>
    public void UpdateDistance(float distance)
    {
        if (!Info.ShowDistance || !DistanceText.gameObject.activeSelf)
            return;
        DistanceText.text = (int)distance + "m";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(Info.TimeToShow);
        float curveTime = 1;
        while(Alpha.alpha > 0)
        {
            Alpha.alpha = FadeCurve.Evaluate(curveTime);
            curveTime -= Time.deltaTime;
            yield return null;
        }
        //When fade is end, them remove indicator from list
        Manager.RemoveIndicator(this);
        Destroy(this.gameObject);
    }

    private Transform m_Transform = null;
    public Transform Transform
    {
        get
        {
            if(m_Transform == null)
            {
                m_Transform = GetComponent<Transform>();
            }
            return m_Transform;
        }
    }
}