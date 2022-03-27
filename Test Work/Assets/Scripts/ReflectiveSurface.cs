using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ReflectiveSurface : MonoBehaviour
{
    [SerializeField] private ReflectiveSurfaceSettings settings;

    public float GetAbsorption() => settings.RayAbsorption;
}
