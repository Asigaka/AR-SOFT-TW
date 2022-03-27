using UnityEngine;

public class Laser : MonoBehaviour
{
    private void Start()
    {
        LaserController.Instance.AddLaser(this);
    }
}
