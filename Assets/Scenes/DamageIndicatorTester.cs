using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorTester : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public void ShowDamage()
    {
        DamageIndicator.Instance.AddIndicator(new DamageIndicatorData(transform.name, DamageIndicatorType.Damage, transform.position));
    }

    /// <summary>
    /// 
    /// </summary>
    public void ShowSuppression()
    {
        DamageIndicator.Instance.AddIndicator(new DamageIndicatorData(transform.name, DamageIndicatorType.Suppression, transform.position));
    }
}
