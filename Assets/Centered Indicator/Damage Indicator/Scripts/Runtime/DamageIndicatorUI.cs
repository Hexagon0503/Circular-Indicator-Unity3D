using UnityEngine;
using DG.Tweening;
using Coffee.UIEffects;

public class DamageIndicatorUI : IndicatorUIBase
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
    private string indicatorSender;

    /// <summary>
    /// 
    /// </summary>
    private UIGradient gradientGroup;

    /// <summary>
    /// 
    /// </summary>
    private TweenAnimator tweenAnimator;

    /// <summary>
    /// 
    /// </summary>
    private DamageIndicator indicatorManager;
    #endregion

    #region UNITY METHODS
    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        gradientGroup = GetComponentInChildren<UIGradient>();
        tweenAnimator = GetComponent<TweenAnimator>();
        indicatorManager = DamageIndicator.Instance;
        gradientGroup.offset = -1;
        BuildSequence();
    }
    #endregion

    #region METHODS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public override void Show(BaseIndicatorData data)
    {
        if (data.customData is DamageIndicatorData)
        {
            indicatorSender = ((DamageIndicatorData)data.customData).Sender;
        }
        tweenAnimator.Restart();
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


    /// <summary>
    /// 
    /// </summary>
    private void RemoveIndicator()
    {
        indicatorManager.RemoveIndicator(indicatorSender);
    }
    #endregion

    #region Tweening
    /// <summary>
    /// 
    /// </summary>
    private void BuildSequence()
    {
        Sequence tweenSequence = DOTween.Sequence()
            .SetRecyclable(true)
            .SetAutoKill(false)
            .Pause()
            .OnComplete(Destroy);

        tweenSequence.Append(DOTween.To(()=> gradientGroup.offset, x=> gradientGroup.offset = x, 1f, appearTime))
            .AppendInterval(duration - appearTime - hideTime)
            .Append(DOTween.To(() => gradientGroup.offset, x => gradientGroup.offset = x, -1f, hideTime))
            .OnComplete(RemoveIndicator);

        tweenAnimator.RegisterSequence(tweenSequence);
    }
    #endregion
}
