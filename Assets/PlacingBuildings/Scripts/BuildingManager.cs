using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> hologramBuildings = new List<GameObject>();
    [SerializeField]
    private List<GameObject> realBuildings = new List<GameObject>();
    private GameObject[] buldingsButton;
    private GameObject hologramBuilding;
    private Vector3 mousePosition;
    private RaycastHit hit;
    private int pickedBuildingNumber;
    private bool readyToBuild;
    private bool drawHologram;
    private int numberOfChilds;
    private static BuildingManager m_instance;
    public static BuildingManager Instance
    {
        get
        {
            if (Equals(m_instance, null))
            {
                return m_instance = FindObjectOfType(typeof(BuildingManager)) as BuildingManager;
            }

            return m_instance;
        }
    }

    void Start()
    {
        buldingsButton = GameObject.FindGameObjectsWithTag("BuldingButtons");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && drawHologram)
        {
            StartShowingHologramBuilding();
            readyToBuild = true;
        }
        if (ClickOnPlane(ray) && readyToBuild)
        {
            if(!BuildingsPermit.IsCollision)
            {
                PlaceBuilding();
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Resetart();
        }
    }

    public void PlaceBuilding()
    {
        Instantiate(realBuildings[pickedBuildingNumber], hologramBuilding.GetComponent<Transform>().position, Quaternion.identity);
        Resetart();
    }

    public void ClickedBuildingButton()
    {
        pickedBuildingNumber = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        ButtonInteractable(false);
        hologramBuilding = Instantiate(hologramBuildings[pickedBuildingNumber], new Vector3(0, 100, 0), Quaternion.identity);
        drawHologram = true;
        AddScriptsToBuildings();
    }

    private void Resetart()
    {
        drawHologram = false;
        readyToBuild = false;
        ButtonInteractable(true);
        Destroy(hologramBuilding);
    }

    private void AddScriptsToBuildings()
    {
        HologramBuildingsChilds().gameObject.AddComponent<BuildingsPermit>();
        HologramBuildingsChilds().gameObject.AddComponent<ChangeToHologram>();
    }

    private void ButtonInteractable(bool enableButtonInteractions)
    {
        foreach (GameObject button in buldingsButton)
        {
            button.GetComponent<Button>().interactable = enableButtonInteractions;
        }
    }

    private void StartShowingHologramBuilding()
    {
        mousePosition = hit.point;
        hologramBuilding.transform.position = new Vector3(mousePosition.x, mousePosition.y + GetTransfotmY() + 0.001f, mousePosition.z);       
    }

    private Vector3 GetWorldMeshSize(Transform t)
    {
        Vector3 size = Vector3.zero;
        MeshFilter[] filters = t.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter filter in filters)
        {
            if (filter.sharedMesh == null) continue;
            Vector3 meshSize = Vector3.Scale(filter.sharedMesh.bounds.size, filter.transform.lossyScale);
            size = Vector3.Max(size, meshSize);
        }
        return size;
    }

    private Transform HologramBuildingsChilds()
    {
        if (hologramBuilding.transform.childCount > 0)
        {
            for (int childIndex = 0; childIndex < hologramBuilding.transform.childCount; childIndex++)
            {
                numberOfChilds = childIndex;
            }
            return hologramBuilding.transform.GetChild(numberOfChilds);
        }
        else
        {
            return hologramBuilding.transform;
        }

    }

    private bool ClickOnPlane(Ray ray)
    {
        return Physics.Raycast(ray, out hit) && Input.GetButton("Fire1");
    }

    private float GetTransfotmY()
    {
        return GetWorldMeshSize(hologramBuilding.GetComponent<Transform>()).y / 2;
    }

    private bool NoBoxCollider()
    {
        return HologramBuildingsChilds().gameObject.GetComponent<BoxCollider>() == null;
    }

}
