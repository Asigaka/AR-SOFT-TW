using UnityEngine;

public class LaserUIInputs : MonoBehaviour
{
    [SerializeField] private float basePressValue;
    [SerializeField] private float gunPressValue;

    private static LaserUIInputs Instance;

    public float BasePressValue { get => basePressValue; }
    public float GunPressValue { get => gunPressValue; }

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public void OnBaseBtnUp() => basePressValue = 0;

    public void OnGunBtnUp() => gunPressValue = 0;

    public void OnLeftBaseBtnDown() => basePressValue = -1;

    public void OnRightBaseBtnDown() => basePressValue = 1;

    public void OnLeftGunBtnDown() => gunPressValue = -1;

    public void OnRightGunBtnDown() => gunPressValue = 1;
}
