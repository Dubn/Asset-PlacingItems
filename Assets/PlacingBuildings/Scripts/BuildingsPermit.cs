using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingsPermit : MonoBehaviour
{
    public static bool IsCollision { get { return isCollision; } set { isCollision = value; } }
    private static bool isCollision;
    private GameObject WarningBanner;
    private string collisionName;
    private void Start()
    {
        isCollision = false;
    }

    void OnTriggerStay(Collider collision)
    {
        collisionName = collision.name;
        ShowWarningBaner();
        isCollision = true;
    }

    void OnTriggerExit(Collider other)
    {
        isCollision = false;
        Destroy(WarningBanner, 0.2f);
    }
    private void ShowWarningBaner()
    {
        if (WarningBanner == null)
        {
            WarningBanner = Instantiate(Resources.Load("WarningBanner"), new Vector3(0.1f, 1.83f, -0.28f), Quaternion.identity) as GameObject;

        }
    }
    
}
