using UnityEngine;

public abstract class IndicatorUIBase : MonoBehaviour
{
    #region METHODS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public abstract void Show(BaseIndicatorData data);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distance"></param>
    public abstract void UpdateDistance(float distance);

    /// <summary>
    /// 
    /// </summary>
    public abstract void Destroy();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="useLerp"></param>
    /// <param name="lerpSpeed"></param>
    private Vector3 localAngle = Vector3.zero;
    public virtual void SetAngle(float angle, bool useLerp, float lerpSpeed)
    {
        localAngle.z = angle;
        if (useLerp)
        {
            rectTransform.localRotation = Quaternion.Lerp(rectTransform.localRotation, Quaternion.Euler(localAngle), Time.deltaTime * lerpSpeed);
        }
        else
        {
            rectTransform.localRotation = Quaternion.Euler(localAngle);
        }
    }
    #endregion

    #region FUNCTIONS
    /// <summary>
    /// 
    /// </summary>
    public virtual bool IsVisible()
    {
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    private RectTransform _rectTransform;
    protected RectTransform rectTransform
    {
        get
        {
            if(_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }
            return _rectTransform;
        }
    }
    #endregion
}
