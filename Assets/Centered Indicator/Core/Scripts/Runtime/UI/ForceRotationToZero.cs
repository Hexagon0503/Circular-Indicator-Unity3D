using UnityEngine;

/// <summary>
/// Keeps rotation steady
/// </summary>
public class ForceRotationToZero : MonoBehaviour
{
    private Transform m_Transform;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        m_Transform = transform;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        //Rotate
        m_Transform.rotation = Quaternion.identity;
    }
}