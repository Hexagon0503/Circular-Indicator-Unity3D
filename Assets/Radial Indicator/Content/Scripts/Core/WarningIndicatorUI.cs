using UnityEngine;
using DG.Tweening;

public class WarningIndicatorUI : IndicatorUIBase
{
    #region SERIALIZE FIELDS
    [SerializeField, Range(1, 5)] private float duration = 3;
    [SerializeField, Range(0, 1)] private float appearTime;
    [SerializeField, Range(0, 1)] private float hideTime = 1;
    #endregion

    #region FIELDS
    /// <summary>
    /// 
    /// </summary>
    private CanvasGroup alphaGroup;

    /// <summary>
    /// 
    /// </summary>
    private Sequence tweenSequence = null;
    #endregion

    #region UNITY METHODS
    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        alphaGroup = GetComponent<CanvasGroup>();
        alphaGroup.alpha = 0;
        BuildSequence();
    }
    #endregion

    #region METHODS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public override void Show(RadialIndicatorData data)
    {
        PlayTween();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Destroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distance"></param>
    public override void UpdateDistance(float distance) { }
    #endregion

    #region Tweening
    /// <summary>
    /// 
    /// </summary>
    private void BuildSequence()
    {
        tweenSequence = DOTween.Sequence()
            .SetRecyclable(true)
            .SetAutoKill(false)
            .Pause();

        tweenSequence.Append(alphaGroup.DOFade(1, appearTime))
            .AppendInterval(duration - appearTime - hideTime)
            .Append(alphaGroup.DOFade(0, hideTime));
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayTween()
    {
        if (tweenSequence != null)
            tweenSequence.Restart();
    }
    #endregion
}
