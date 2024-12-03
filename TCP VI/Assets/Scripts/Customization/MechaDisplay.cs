using UnityEngine;

public class MechaDisplay : MonoBehaviour
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
        loadGameObject(rightArmObj, "/MechaDisplay/RightArm");
        loadGameObject(brandObj, "/MechaDisplay/Brand");
        loadGameObject(leftArmObj, "/MechaDisplay/LeftArm");

        
        rightArmMeshFilter = rightArmObj.GetComponentInChildren<MeshFilter>();
        brandMeshFilter = brandObj.GetComponentInChildren<MeshFilter>();
        leftArmMeshFilter = leftArmObj.GetComponentInChildren<MeshFilter>();

        // TODO: Add a SkinnedMeshRenderer Implementation path. 
        //(this will probably make MeshFilter obsolete if everything is animated with bones)

        ReverseMesh(leftArmMeshFilter); //reverse leftie
    }

    private void Start() 
    {
        //subscribe method to the event
        MechaManager.instance.PartChanged += ChangeMeshes;
    }

    private void ChangeMeshes(MechaManager.Selected bodyPart)
    {
        //Get Each Mesh or Material from the SOs
        newRArmMesh = MechaManager.instance.GetRightArm?.Mesh;
        newBrandMesh = MechaManager.instance.GetBrand?.Mesh;
        newLArmMesh = MechaManager.instance.GetLeftArm?.Mesh;

        newRArmMaterial = MechaManager.instance.GetRightArm?.Material;
        newBrandMaterial = MechaManager.instance.GetBrand?.Material;
        newLArmMaterial = MechaManager.instance.GetLeftArm?.Material;

        switch(bodyPart) 
        {
        case MechaManager.Selected.RightArm:
            LoadNewMesh(rightArmMeshFilter, newRArmMesh,newRArmMaterial);
            break;
        case MechaManager.Selected.Brand:
            LoadNewMesh(brandMeshFilter, newBrandMesh, newBrandMaterial);
            break;
        case MechaManager.Selected.LeftArm:
            LoadNewMesh(leftArmMeshFilter, newLArmMesh, newLArmMaterial);
            break;
        }
    }

    private void LoadNewMesh(MeshFilter meshFilter, Mesh newMesh, Material newMaterial)
    {
        if (newMesh != null)
        {
            meshFilter.mesh = newMesh;
            
            MeshRenderer renderer = meshFilter.GetComponent<MeshRenderer>();
            renderer.material = newMaterial;
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

    private void loadGameObject(GameObject go, string goPath)
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
