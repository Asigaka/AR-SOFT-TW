using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private LaserSettings settings;
    [SerializeField] private LaserUIInputs uiInputs;

    [Space]
    [SerializeField] private Transform baseTransform;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject linePrefab;

    private List<Laser> lasers = new List<Laser>();
    private List<GameObject> lines = new List<GameObject>();
    private float laserPower;
    private float laserRange;

    public static LaserController Instance;

    public void AddLaser(Laser laser) => lasers.Add(laser);

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        firePoint.gameObject.AddComponent<Laser>();
    }

    private void Update()
    {
        ThrowLaser();
        MoveLaser();
    }

    private void MoveLaser()
    {
        float baseRotate = uiInputs.BasePressValue * settings.BaseRotateSpeed;
        float gunRotate = uiInputs.GunPressValue * settings.GunRotateSpeed;

        if (gunRotate != 0)
        {
            Vector3 gunDir = new Vector3(gunRotate, 0, 0);

            gunTransform.Rotate(gunDir, Space.Self);
        }


        if (baseRotate != 0)
        {
            Vector3 baseDir = new Vector3(0, baseRotate, 0);
            baseTransform.Rotate(baseDir, Space.Self);
        }
    }

    private void ThrowLaser()
    {
        laserPower = settings.StartPower;
        laserRange = settings.Range;
        int linesCount = 0;
        foreach (Laser laser in lasers)
        {
            linesCount += CalculateLaser(firePoint.position, firePoint.up, linesCount);
        }

        CleanLasers(linesCount);
    }

    private int CalculateLaser(Vector3 start, Vector3 direction, int index)
    {
        int result = 1;
        RaycastHit hit;
        Ray ray = new Ray(start, direction);
        bool intersect = Physics.Raycast(ray, out hit, laserRange);

        Vector3 hitPos = hit.point;

        if (!intersect)
            hitPos = start + direction * laserRange;

        DrawLine(start, hitPos, index);

        if (intersect)
        {
            ReflectiveSurface refSurf = hit.collider.GetComponent<ReflectiveSurface>();

            if (refSurf)
            {
                laserRange -= hit.distance;
                float intermidiatePower = laserPower - refSurf.GetAbsorption();

                if (intermidiatePower > 0)
                {
                    laserPower = intermidiatePower;
                    result += CalculateLaser(hitPos, Vector3.Reflect(direction, hit.normal), index + result);
                }
            }
        }

        return result;
    }

    private void DrawLine(Vector3 start, Vector3 finish, int index)
    {
        LineRenderer line = null;

        if (index < lines.Count)
        {
            line = lines[index].GetComponent<LineRenderer>();
        }
        else
        {
            line = Instantiate(linePrefab).GetComponent<LineRenderer>();
            lines.Add(line.gameObject);
        }

        line.SetPosition(0, start);
        line.SetPosition(1, finish);
    }

    private void CleanLasers(int linesCount)
    {
        if (lines.Count > linesCount)
        {
            Destroy(lines[lines.Count - 1]);
            lines.RemoveAt(lines.Count - 1);
            CleanLasers(linesCount);
        }
    }
}
