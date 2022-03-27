using UnityEngine;

[CreateAssetMenu(menuName = "Reflective Surface Settings", fileName = "ReflectiveSurfaceSettings")]
public class ReflectiveSurfaceSettings : ScriptableObject
{
    [SerializeField] private float rayAbsorption;

    public float RayAbsorption { get => rayAbsorption; }
}
