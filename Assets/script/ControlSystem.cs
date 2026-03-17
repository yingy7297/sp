using UnityEngine;

public class ControlSystem : MonoBehaviour 
{
    [Header("基本數值")]
    [SerializeField, Range(0, 10)]
    private float movespeed = 5f;
    [Header("元件")]
    [SerializeField]
    private Rigidbody2D rig;
}
