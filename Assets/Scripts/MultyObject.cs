using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Shape
{
   shape3D, cube5D,clone5D, plane3D, shape4D, shapecube5D, hyperbola5D, hyperCurveCube5D
}
[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MultyObject : MonoBehaviour
{

    [Header("Transform W")]
    [SerializeField] public float W_Position;
    [SerializeField] public float W_Scale = 1;
    [Header("Transform H")]
    [SerializeField] public float H_Position;
    [SerializeField] public float H_Scale = 1;
    [Header("Shape")]
    [SerializeField] public Shape shape;
    [SerializeField] public Mesh[] shapes3D;
    [SerializeField] Mesh[] shapes3Dcol;
    [SerializeField] public shapeSettings shapeSettings;
    [Header("Save prametrs")]
    [SerializeField] public Vector3 scale3D;
    public bool hide;
    MultyTransform instatiate;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;
    SphereCollider SphereCollider;
    BoxCollider BoxCollider;
    CapsuleCollider CapsuleCollider;

    MeshFilter meshFilter;
    List<GameObject> childs = new();
    GameObject selfShadow;

    int w;
 
    void Start()
    {
       
        if (scale3D == Vector3.zero) scale3D = transform.localScale;
        shapes3Dcol = new Mesh[shapes3D.Length];
        for (int i = 0; i < shapes3D.Length; i++)
        {
            shapes3Dcol[i] = new Mesh();
            shapes3Dcol[i].vertices = shapes3D[i].vertices;
            shapes3Dcol[i].triangles = shapes3D[i].triangles;
            shapes3Dcol[i].normals = shapes3D[i].normals;
            shapes3Dcol[i].uv = shapes3D[i].uv;
            shapes3Dcol[i].name = i.ToString();

        }
        float c = transform.childCount;
        for (int i = 0; i < c; i++)
        {
            childs.Add(transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    public void Update()
    {
        instatiate ??= FindFirstObjectByType<MultyTransform>();
        meshCollider ??= GetComponent<MeshCollider>();
        meshRenderer ??= GetComponent<MeshRenderer>();
        meshFilter ??= GetComponent<MeshFilter>();
        SphereCollider ??= GetComponent<SphereCollider>();
        BoxCollider ??= GetComponent<BoxCollider>();
        CapsuleCollider ??= GetComponent<CapsuleCollider>();

        if (shape == Shape.plane3D)
        {
            transform.localScale = new Vector3(100000, scale3D.y, 100000);
        }
        if (shape == Shape.shape3D)
        {
            
            transform.localScale = new Vector3(scale3D.x, scale3D.y, scale3D.z);
        }
        if (shape == Shape.cube5D)
        {
            if (shapeSettings != null)
            {
                if (shapeSettings.W_materials.Length != 0)
                {



                    int w2 = (int)((((instatiate.W_Position - W_Position) * shapeSettings.W_materials.Length) / (W_Scale / 2)) - (W_Scale / 2));

                    if (w2 > -1 && w2 < shapeSettings.W_materials.Length)
                    {

                        if (meshRenderer)
                        {
                            meshRenderer.material = shapeSettings.W_materials[w2];
                        }


                    }
                }
            }
            transform.localScale = new Vector3(scale3D.x, scale3D.y, scale3D.z);
            if (instatiate.W_Position + W_Scale > W_Position && instatiate.W_Position - W_Scale < W_Position &&
                instatiate.H_Position + H_Scale > H_Position && instatiate.H_Position - H_Scale < H_Position)
            {
                hide = false;
                if (meshRenderer)
                {
                    meshRenderer.enabled = true;
                }
                if (meshCollider)
                {
                    meshCollider.enabled = true;
                }
                if (SphereCollider)
                {
                    SphereCollider.enabled = true;
                }
                if (BoxCollider)
                {
                    BoxCollider.enabled = true;
                }
                if (CapsuleCollider)
                {
                    CapsuleCollider.enabled = true;
                }
                if (childs.Count > 0)
                {
                    foreach (GameObject child in childs)
                    {
                        if (child != null) child.SetActive(true);
                    }
                }
            }
            else
            {
                hide = true;

                if (meshRenderer)
                {
                    meshRenderer.enabled = false;
                }
                if (meshCollider)
                {
                    meshCollider.enabled = false;
                }
                if (SphereCollider)
                {
                    SphereCollider.enabled = false;
                }
                if (BoxCollider)
                {
                    BoxCollider.enabled = false;
                }
                if (CapsuleCollider)
                {
                    CapsuleCollider.enabled = false;
                }
                if (childs.Count > 0)
                {
                    foreach (GameObject child in childs)
                    {
                        if (child != null) child.SetActive(false);
                    }


                }

            }
        }
            if (shape == Shape.shapecube5D)
            {
                if (shapeSettings != null)
                {
                    if (shapeSettings.W_materials.Length != 0)
                    {



                        int w2 = (int)((((instatiate.W_Position - W_Position) * shapeSettings.W_materials.Length) / (W_Scale / 2)) - (W_Scale / 2));

                        if (w2 > -1 && w2 < shapeSettings.W_materials.Length)
                        {

                            if (meshRenderer)
                            {
                                meshRenderer.material = shapeSettings.W_materials[w2];
                            }


                        }
                    }
                }
                transform.localScale = new Vector3(scale3D.x, scale3D.y, scale3D.z);
                w = (int)(((instatiate.W_Position - W_Position) * shapes3D.Length) / (W_Scale / 2));




                if (w > -1 && w < shapes3Dcol.Length)
                {

                    if (meshFilter)
                    {
                        meshFilter.sharedMesh = shapes3Dcol[w];
                    }


                }



                if (instatiate.W_Position > W_Position && instatiate.W_Position - W_Scale < W_Position &&
                    instatiate.H_Position + H_Scale > H_Position && instatiate.H_Position - H_Scale < H_Position)
                {
                    hide = false;
                    if (meshRenderer)
                    {
                        meshRenderer.enabled = true;
                    }
                    if (meshCollider)
                    {
                        meshCollider.enabled = true;
                    }
                    if (childs.Count > 0)
                    {
                        foreach (GameObject child in childs)
                        {
                            if (child != null) child.SetActive(true);
                        }

                    }
                }
                else
                {
                    hide = true;
                    if (meshRenderer)
                    {
                        meshRenderer.enabled = false;
                    }
                    if (meshCollider)
                    {
                        meshCollider.enabled = false;
                    }
                    if (childs.Count > 0)
                    {
                        foreach (GameObject child in childs)
                        {
                            if (child != null) child.SetActive(false);
                        }

                    }
                }

            }
            if (shape == Shape.shape4D)
            {
                if (shapeSettings != null)
                {
                    if (shapeSettings.W_materials.Length != 0)
                    {



                        int w2 = (int)((((instatiate.W_Position - W_Position) * shapeSettings.W_materials.Length) / (W_Scale / 2)) - (W_Scale / 2));

                        if (w2 > -1 && w2 < shapeSettings.W_materials.Length)
                        {

                            if (meshRenderer)
                            {
                                meshRenderer.material = shapeSettings.W_materials[w2];
                            }


                        }
                    }
                }
                transform.localScale = new Vector3(scale3D.x, scale3D.y, scale3D.z);
                w = (int)(((instatiate.W_Position - W_Position) * shapes3D.Length) / (W_Scale / 2));




                if (w > -1 && w < shapes3D.Length)
                {

                    if (meshFilter)
                    {
                        meshFilter.sharedMesh = shapes3D[w];
                    }


                }



                if (instatiate.W_Position > W_Position && instatiate.W_Position - W_Scale < W_Position)
                {
                    hide = false;
                    if (meshRenderer)
                    {
                        meshRenderer.enabled = true;
                    }
                    if (meshCollider)
                    {
                        meshCollider.enabled = true;
                    }
                    if (SphereCollider)
                    {
                        SphereCollider.enabled = true;
                    }
                    if (BoxCollider)
                    {
                        BoxCollider.enabled = true;
                    }
                    if (CapsuleCollider)
                    {
                        CapsuleCollider.enabled = true;
                    }
                    if (childs.Count > 0)
                    {
                        foreach (GameObject child in childs)
                        {
                            if (child != null) child.SetActive(true);
                        }
                    }
                }
                else
                {
                    hide = true;

                    if (meshRenderer)
                    {
                        meshRenderer.enabled = false;
                    }
                    if (meshCollider)
                    {
                        meshCollider.enabled = false;
                    }
                    if (SphereCollider)
                    {
                        SphereCollider.enabled = false;
                    }
                    if (BoxCollider)
                    {
                        BoxCollider.enabled = false;
                    }
                    if (CapsuleCollider)
                    {
                        CapsuleCollider.enabled = false;
                    }
                    if (childs.Count > 0)
                    {
                        foreach (GameObject child in childs)
                        {
                            if (child != null) child.SetActive(false);
                        }

                    }

                }

            }
            if (shape == Shape.clone5D)
            {
                if (shapeSettings != null)
                {
                    if (shapeSettings.W_materials.Length != 0)
                    {



                        int w2 = (int)((((instatiate.W_Position - W_Position) * shapeSettings.W_materials.Length) / (W_Scale / 2)) - (W_Scale / 2));

                        if (w2 > -1 && w2 < shapeSettings.W_materials.Length)
                        {

                            if (meshRenderer)
                            {
                                meshRenderer.material = shapeSettings.W_materials[w2];
                            }


                        }
                    }
                }
                float h = H_Scale - Mathf.Abs(H_Position - instatiate.H_Position);
                Vector3 v3 = scale3D;

                float w = W_Scale - Mathf.Abs(W_Position - instatiate.W_Position);
                float s = ((w / W_Scale) + (h / H_Scale)) / 2;
                transform.localScale = new Vector3(s * scale3D.x, s * scale3D.y, s * scale3D.z);
                if (instatiate.W_Position + W_Scale > W_Position && instatiate.W_Position - W_Scale < W_Position &&
                    instatiate.H_Position + H_Scale > H_Position && instatiate.H_Position - H_Scale < H_Position)
                {
                    hide = false;
                    if (meshRenderer)
                    {
                        meshRenderer.enabled = true;
                    }
                    if (meshCollider)
                    {
                        meshCollider.enabled = true;
                    }
                    if (SphereCollider)
                    {
                        SphereCollider.enabled = true;
                    }
                    if (BoxCollider)
                    {
                        BoxCollider.enabled = true;
                    }
                    if (CapsuleCollider)
                    {
                        CapsuleCollider.enabled = true;
                    }
                    if (childs.Count > 0)
                    {
                        foreach (GameObject child in childs)
                        {
                            if (child != null) child.SetActive(true);
                        }
                    }
                }
                else
                {
                    hide = true;

                    if (meshRenderer)
                    {
                        meshRenderer.enabled = false;
                    }
                    if (meshCollider)
                    {
                        meshCollider.enabled = false;
                    }
                    if (SphereCollider)
                    {
                        SphereCollider.enabled = false;
                    }
                    if (BoxCollider)
                    {
                        BoxCollider.enabled = false;
                    }
                    if (CapsuleCollider)
                    {
                        CapsuleCollider.enabled = false;
                    }
                    if (childs.Count > 0)
                    {
                        foreach (GameObject child in childs)
                        {
                            if (child != null) child.SetActive(false);
                        }

                    }

                }
            }

        if (shape == Shape.hyperbola5D)
        {
            if (shapeSettings != null)
            {
                if (shapeSettings.W_materials.Length != 0)
                {



                    int w2 = (int)((((instatiate.W_Position - W_Position) * shapeSettings.W_materials.Length) / (W_Scale / 2)) - (W_Scale / 2));

                    if (w2 > -1 && w2 < shapeSettings.W_materials.Length)
                    {

                        if (meshRenderer)
                        {
                            meshRenderer.material = shapeSettings.W_materials[w2];
                        }


                    }
                }
            }
            float h = H_Scale + Mathf.Abs(H_Position - instatiate.H_Position) * Mathf.Abs(H_Position - instatiate.H_Position);
            Vector3 v3 = scale3D;

            float w = W_Scale + Mathf.Abs(W_Position - instatiate.W_Position) * Mathf.Abs(W_Position - instatiate.W_Position);
            float s = ((w / W_Scale) + (h / H_Scale)) / 2;
            transform.localScale = new Vector3(s * scale3D.x, s * scale3D.y, s * scale3D.z);
            if (instatiate.W_Position + W_Scale > W_Position && instatiate.W_Position - W_Scale < W_Position &&
                instatiate.H_Position + H_Scale > H_Position && instatiate.H_Position - H_Scale < H_Position)
            {
                hide = false;
                if (meshRenderer)
                {
                    meshRenderer.enabled = true;
                }
                if (meshCollider)
                {
                    meshCollider.enabled = true;
                }
                if (SphereCollider)
                {
                    SphereCollider.enabled = true;
                }
                if (BoxCollider)
                {
                    BoxCollider.enabled = true;
                }
                if (CapsuleCollider)
                {
                    CapsuleCollider.enabled = true;
                }
                if (childs.Count > 0)
                {
                    foreach (GameObject child in childs)
                    {
                        if (child != null) child.SetActive(true);
                    }
                }
            }
            else
            {
                hide = true;

                if (meshRenderer)
                {
                    meshRenderer.enabled = false;
                }
                if (meshCollider)
                {
                    meshCollider.enabled = false;
                }
                if (SphereCollider)
                {
                    SphereCollider.enabled = false;
                }
                if (BoxCollider)
                {
                    BoxCollider.enabled = false;
                }
                if (CapsuleCollider)
                {
                    CapsuleCollider.enabled = false;
                }
                if (childs.Count > 0)
                {
                    foreach (GameObject child in childs)
                    {
                        if (child != null) child.SetActive(false);
                    }

                }
            }
        }
        if (shape == Shape.hyperCurveCube5D && shapeSettings != null)
        {
            float h = (H_Scale- Mathf.Abs(H_Position - instatiate.H_Position))/ H_Scale;
            Vector3 v3 = scale3D;
           
            float w = (W_Scale - Mathf.Abs(W_Position - instatiate.W_Position))/ W_Scale;
            transform.localScale = new Vector3(
                 scale3D.x * shapeSettings.WX_scale.Evaluate(w) * shapeSettings.HX_scale.Evaluate(h),
                 scale3D.y * shapeSettings.WY_scale.Evaluate(w) * shapeSettings.HY_scale.Evaluate(h),
                 scale3D.z * shapeSettings.WZ_scale.Evaluate(w) * shapeSettings.HZ_scale.Evaluate(h));
            if (instatiate.W_Position + W_Scale > W_Position && instatiate.W_Position - W_Scale < W_Position &&
                instatiate.H_Position + H_Scale > H_Position && instatiate.H_Position - H_Scale < H_Position)
            {
                hide = false;
                if (meshRenderer)
                {
                    meshRenderer.enabled = true;
                }
                if (meshCollider)
                {
                    meshCollider.enabled = true;
                }
                if (SphereCollider)
                {
                    SphereCollider.enabled = true;
                }
                if (BoxCollider)
                {
                    BoxCollider.enabled = true;
                }
                if (CapsuleCollider)
                {
                    CapsuleCollider.enabled = true;
                }
                if (childs.Count > 0)
                {
                    foreach (GameObject child in childs)
                    {
                        if (child != null) child.SetActive(true);
                    }
                }
            }
            else
            {
                hide = true;

                if (meshRenderer)
                {
                    meshRenderer.enabled = false;
                }
                if (meshCollider)
                {
                    meshCollider.enabled = false;
                }
                if (SphereCollider)
                {
                    SphereCollider.enabled = false;
                }
                if (BoxCollider)
                {
                    BoxCollider.enabled = false;
                }
                if (CapsuleCollider)
                {
                    CapsuleCollider.enabled = false;
                }
                if (childs.Count > 0)
                {
                    foreach (GameObject child in childs)
                    {
                        if (child != null) child.SetActive(false);
                    }

                }
            }
        }
        if (meshCollider)
        {

            meshCollider.sharedMesh = meshFilter.sharedMesh;
           
        }
    }
}
