using UnityEngine;

public class PlayerMechaModel : MonoBehaviour
{
    [Header ("Mecha GOs")]
    [SerializeField] GameObject rightArmObj;
    [SerializeField] GameObject brandObj;
    [SerializeField] GameObject leftArmObj;

    [Header ("Mecha Mesh Filters")]
    [SerializeField] MeshFilter rightArmMeshFilter;    
    [SerializeField] MeshFilter brandMeshFilter;
    [SerializeField] MeshFilter leftArmMeshFilter;

    [Header ("Mecha Meshes")]
    [SerializeField] Mesh newRArmMesh;
    [SerializeField] Mesh newBrandMesh;
    [SerializeField] Mesh newLArmMesh;

    [Header ("Mecha Materials")]
    [SerializeField] Material newRArmMaterial;
    [SerializeField] Material newBrandMaterial;
    [SerializeField] Material newLArmMaterial;
    


    private void Awake() 
    {
        loadGameObject(ref rightArmObj, "/Player/Right Arm");
        loadGameObject(ref brandObj, "/Player/Body");
        loadGameObject(ref leftArmObj, "/Player/Left Arm");

        
        rightArmMeshFilter = rightArmObj.GetComponentInChildren<MeshFilter>();
        brandMeshFilter = brandObj.GetComponentInChildren<MeshFilter>();
        leftArmMeshFilter = leftArmObj.GetComponentInChildren<MeshFilter>();

        ReverseMesh(leftArmMeshFilter);  //reverse leftie
    }

    private void Start() 
    {
        ChangeMeshes();
    }

    private void ChangeMeshes()
    {
        //Get Each Mesh or Material from the SOs
        newRArmMesh = MechaManager.instance.GetRightArm?.Mesh;
        newBrandMesh = MechaManager.instance.GetBrand?.Mesh;
        newLArmMesh = MechaManager.instance.GetLeftArm?.Mesh;

        newRArmMaterial = MechaManager.instance.GetRightArm?.Material;
        newBrandMaterial = MechaManager.instance.GetBrand?.Material;
        newLArmMaterial = MechaManager.instance.GetLeftArm?.Material;

        LoadNewMesh(rightArmMeshFilter, newRArmMesh,newRArmMaterial);
        LoadNewMesh(brandMeshFilter, newBrandMesh, newBrandMaterial);
        LoadNewMesh(leftArmMeshFilter, newLArmMesh, newLArmMaterial);
        ReverseMesh(leftArmMeshFilter);
    }

    private void LoadNewMesh(MeshFilter meshFilter, Mesh newMesh, Material newMaterial)
    {
        if (newMesh != null)
        {
            meshFilter.mesh = newMesh;
            
            MeshRenderer renderer = meshFilter.GetComponent<MeshRenderer>();
            
            Material[] materials = renderer.materials;
            materials[0] = newMaterial;
            renderer.materials = materials;
        }
        else
        {
             Debug.LogWarning($"[DEV_WARNING] New Mesh is null | Not loaded.");
        }
    }

    private void ReverseMesh(MeshFilter meshFilter) //to reverse when it's the leftie arm
    {
        Vector3 newMeshScale = meshFilter.transform.localScale;
        if(newMeshScale.x > 0) //reverse only if it isn't already
        {
            newMeshScale.x *= -1;
        }
        meshFilter.transform.localScale = newMeshScale;
    }

    private void loadGameObject(ref GameObject go, string goPath)
    {
        if(go != null)
        {
            return;
        }
        
        GameObject goToLoad = GameObject.Find(goPath);
        if(goToLoad == null)
        {
            Debug.LogError($"[DEV_ERROR] Cannot Find {goPath} object in scene.");
            return;
        }
        else
        {
            go = GameObject.Find(goPath);
        }
    }
    
}
