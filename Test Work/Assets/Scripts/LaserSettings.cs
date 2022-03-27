using UnityEngine;

[CreateAssetMenu(menuName = "Laser Settings", fileName = "LaserSettings")]
public class LaserSettings : ScriptableObject
{
    [SerializeField] private float range;
    [SerializeField] private float startPower;
    [SerializeField] private float baseRotateSpeed = 2;
    [SerializeField] private float gunRotateSpeed = 2;

    public float Range { get => range; }
    public float StartPower { get => startPower; }
    public float BaseRotateSpeed { get => baseRotateSpeed; }
    public float GunRotateSpeed { get => gunRotateSpeed; }
}
