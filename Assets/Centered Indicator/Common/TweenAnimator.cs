using UnityEngine;
using DG.Tweening;

public class TweenAnimator : MonoBehaviour
{
    #region SERIALIZE FIELDS
    #endregion

    #region FIELDS
    /// <summary>
    /// 
    /// </summary>
    private Sequence tweenSequence = null;
    #endregion

    #region METHODS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newSequence"></param>
    public void RegisterSequence(Sequence newSequence)
    {
        tweenSequence = newSequence;
        tweenSequence?.SetLink(gameObject, LinkBehaviour.PauseOnDisable);
        tweenSequence?.SetLink(gameObject, LinkBehaviour.KillOnDestroy);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Play()
    {
        tweenSequence?.Restart();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Restart()
    {
        tweenSequence?.Restart();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Kill()
    {
        tweenSequence?.Kill();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Pause()
    {
        tweenSequence?.Pause();
    }
    #endregion
}
