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
        
        rightArmMeshFilter = rightArmObj.GetComponentInChildren<MeshFilter>();
        brandMeshFilter = brandObj.GetComponentInChildren<MeshFilter>();
        leftArmMeshFilter = leftArmObj.GetComponentInChildren<MeshFilter>();

        // TODO: Add a SkinnedMeshRenderer Implementation path. 
        //(this will probably make MeshFilter obsolete if everything is animated with bones)

        ReverseMesh(leftArmMeshFilter); //reverse leftie
    }

    private void Start() 
    {
        MechaManager.instance.PartChanged += ChangeMeshes; //subscribe method to the event
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
             Debug.LogWarning($"<color=yellow>[DEV_WARNING] New Mesh is null | Not loaded.");
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


}
