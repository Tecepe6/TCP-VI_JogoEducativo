using System.Collections.Generic;
using UnityEngine;

public class MechaDisplay : MonoBehaviour
{
    private enum MeshType
    {
        Static,
        Skinned
    }
    [Header ("Mecha GOs")]
    [SerializeField] GameObject rightArmObj;
    [SerializeField] GameObject brandObj;
    [SerializeField] GameObject leftArmObj;

    [Header ("Select the Type")]
    [SerializeField] MeshType meshType;

    [Header ("Mecha Mesh Filters")]
    [SerializeField] MeshFilter rightArmMeshFilter;    
    [SerializeField] MeshFilter brandMeshFilter;
    [SerializeField] MeshFilter leftArmMeshFilter;

    [Header ("Mecha SKMesh Renderer")]
    [SerializeField] SkinnedMeshRenderer rightArmMeshRend;    
    [SerializeField] SkinnedMeshRenderer brandMeshRend;
    [SerializeField] SkinnedMeshRenderer leftArmMeshRend;

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
        loadGameObject(ref rightArmObj, "/MechaDisplay/RightArm");
        loadGameObject(ref brandObj, "/MechaDisplay/Brand");
        loadGameObject(ref leftArmObj, "/MechaDisplay/LeftArm");

        //Getting MeshFilters
        rightArmMeshFilter = rightArmObj.GetComponentInChildren<MeshFilter>();
        brandMeshFilter = brandObj.GetComponentInChildren<MeshFilter>();
        leftArmMeshFilter = leftArmObj.GetComponentInChildren<MeshFilter>();

        //Getting SKMesh Renderers
        rightArmMeshRend = rightArmObj.GetComponentInChildren<SkinnedMeshRenderer>();
        brandMeshRend = brandObj.GetComponentInChildren<SkinnedMeshRenderer>();
        leftArmMeshRend = leftArmObj.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Start() 
    {
        //Setup when starting
        ChangeMeshes(MechaManager.Selected.RightArm);
        ChangeMeshes(MechaManager.Selected.Brand);
        ChangeMeshes(MechaManager.Selected.LeftArm);
        
        //subscribe method to the event
        MechaManager.instance.PartChanged += ChangeMeshes;
        MechaManager.instance.BodyPartChanged += OutlineBodyPart;
        RemoveBodyOutline(); //for first refresh
    }
    
    private void Update() 
    {
        //this.transform.Rotate(0f,30f * Time.deltaTime,0f);
    }

    //only works for MeshFilter:
    private void OutlineBodyPart(MechaManager.Selected bodyPart)
    {
        //first make all unselected every frame
        RemoveBodyOutline();

        if(meshType == MeshType.Static)
        {
            if (rightArmMeshFilter == null || brandMeshFilter == null || leftArmMeshFilter == null)
            {
                readressObjects();
            }
            
            switch(bodyPart) 
            {
                case MechaManager.Selected.RightArm:
                    rightArmMeshFilter.GetComponent<MeshRenderer>().materials[1].SetInt("_Visible", 1);
                    break;
                case MechaManager.Selected.Brand:
                    brandMeshFilter.GetComponent<MeshRenderer>().materials[1].SetInt("_Visible", 1);
                    break;
                case MechaManager.Selected.LeftArm:
                    leftArmMeshFilter.GetComponent<MeshRenderer>().materials[1].SetInt("_Visible", 1);
                    break;
            }
        }
        
        if(meshType == MeshType.Skinned && rightArmMeshRend != null)
        {
            switch(bodyPart) 
            {
                case MechaManager.Selected.RightArm:
                    rightArmMeshRend.materials[1].SetInt("_Visible", 1);
                    break;
                case MechaManager.Selected.Brand:
                    brandMeshRend.materials[1].SetInt("_Visible", 1);
                    break;
                case MechaManager.Selected.LeftArm:
                    leftArmMeshRend.materials[1].SetInt("_Visible", 1);
                    break;
            }
        }
    }

    private void RemoveBodyOutline()
    {
        if(meshType == MeshType.Static)
        {
            //null check for readressing
            if (rightArmMeshFilter == null || brandMeshFilter == null || leftArmMeshFilter == null)
            {
                readressObjects();
            }
            
            rightArmMeshFilter.GetComponent<MeshRenderer>().materials[1].SetInt("_Visible", 0);
            brandMeshFilter.GetComponent<MeshRenderer>().materials[1].SetInt("_Visible", 0);
            leftArmMeshFilter.GetComponent<MeshRenderer>().materials[1].SetInt("_Visible", 0);
        }

        if(meshType == MeshType.Skinned && rightArmMeshRend!=null)
        {
            rightArmMeshRend.materials[1].SetInt("_Visible", 0);
            brandMeshRend.materials[1].SetInt("_Visible", 0);
            leftArmMeshRend.materials[1].SetInt("_Visible", 0);
        }
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

        if(meshType == MeshType.Static)
        {
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
                    ReverseMesh(leftArmMeshFilter);
                    break;
            }
        }
        
        if (meshType == MeshType.Skinned)
        {
            switch(bodyPart) 
            {
                case MechaManager.Selected.RightArm:
                    LoadNewSKMesh(rightArmMeshRend, newRArmMesh,newRArmMaterial);
                    break;
                case MechaManager.Selected.Brand:
                    LoadNewSKMesh(brandMeshRend, newBrandMesh, newBrandMaterial);
                    break;
                case MechaManager.Selected.LeftArm:
                    LoadNewSKMesh(leftArmMeshRend, newLArmMesh, newLArmMaterial);
                    ReverseMesh(leftArmMeshRend);
                    break;
            }
        }
        
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

    private void LoadNewSKMesh(SkinnedMeshRenderer meshRend, Mesh newMesh, Material newMaterial)
    {
        if (newMesh != null)
        {
            meshRend.sharedMesh = newMesh;
            Material[] materials = meshRend.sharedMaterials;
            
            materials[0] = newMaterial;
            meshRend.sharedMaterials = materials;
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

    private void ReverseMesh(SkinnedMeshRenderer meshRenderer) //to reverse when it's the leftie arm
    {
        Vector3 newMeshScale = meshRenderer.transform.localScale;
        if(newMeshScale.x > 0) //reverse only if it isn't already
        {
            newMeshScale.x *= -1;
        }
        meshRenderer.transform.localScale = newMeshScale;
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

    private void readressObjects()
    {
        loadGameObject(ref rightArmObj, "/MechaDisplay/RightArm");
        loadGameObject(ref brandObj, "/MechaDisplay/Brand");
        loadGameObject(ref leftArmObj, "/MechaDisplay/LeftArm");

        //Getting MeshFilters
        rightArmMeshFilter = rightArmObj.GetComponentInChildren<MeshFilter>();
        brandMeshFilter = brandObj.GetComponentInChildren<MeshFilter>();
        leftArmMeshFilter = leftArmObj.GetComponentInChildren<MeshFilter>();

        //Getting SKMesh Renderers
        rightArmMeshRend = rightArmObj.GetComponentInChildren<SkinnedMeshRenderer>();
        brandMeshRend = brandObj.GetComponentInChildren<SkinnedMeshRenderer>();
        leftArmMeshRend = leftArmObj.GetComponentInChildren<SkinnedMeshRenderer>();
    }

}
