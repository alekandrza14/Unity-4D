using MoonSharp.Interpreter;
using ObjParser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum NDemention
{
    _3D, _4D, _5D, _ND
}
public enum Functional
{
    none, spawner, user, mgickStick, steyk, trash
}
public enum StandartKey
{
    leftmouse, E, Q, leftshift, notrequired
}
public enum CustomObjectType
{
    Object,PlayerScin
}

public class CustomObjectData
{
    public string Модель;
    public string[] Модели = new string[] { };
    public Color Цвет;
    public Color[] Цвета = new Color[] { };
    public Vector3 Рамер;
  
    public string Метериал;
    public string[] Метериалы;
    public string Описание = "Привет, это элемент использовал формат файла json";

}

public class CustomObject : MonoBehaviour
{
    
    public MeshFilter mf;
    public Vector3[] verti;
    public int[] tria;
    public Vector2 WHPos;
    public Vector2[] uvs;
    public string s;
    public bool saved;
    public bool Imsaveble;
    public LayerMask Mashime;
    public CustomObjectData Model = new CustomObjectData();
    float[] SpawnTimer;
    bool interact;
   
    List<RawImage> test;
  
    // Start is called before the first frame update
    void Start()
    {
        
        if (VarSave.GetString("Scin") != "" && Imsaveble) s = VarSave.GetString("Scin");
        
      
       
            
                rcs();
    }
    public void resetCurrentSettings()
    {
        
        mf.mesh = generate();
        for(int i = 0; i < Model.Модели.Length; i++)
        {
            Mesh newMesh = generateAdd(i);
            GameObject obj = new GameObject("dopmodels");
            obj.transform.SetParent(transform.GetChild(0));
            obj.transform.position = transform.position;
            obj.transform.localScale = transform.localScale;
            obj.transform.rotation = transform.rotation;
            obj.AddComponent<MeshFilter>().sharedMesh = newMesh;
            obj.AddComponent<MeshCollider>().sharedMesh = newMesh;
            obj.GetComponent<MeshCollider>().cookingOptions = MeshColliderCookingOptions.None;
            if (obj.GetComponent<MeshRenderer>()) if(Model.Метериалы!=null)  if (Model.Метериалы.Length > 0) 
            {
                Material newMaterial2 = Resources.Load<Material>("CO_MainMaterials/" + Model.Метериалы[i]);
                obj.GetComponent<MeshRenderer>().material = newMaterial2;
            }
            obj.AddComponent<MeshRenderer>().material.color = Model.Цвета[i];
           
        }
       
        GetComponent<MeshCollider>().sharedMesh = mf.mesh;
        GetComponent<MeshCollider>().cookingOptions = MeshColliderCookingOptions.None;
        transform.localScale = Model.Рамер;
        Material newMaterial = Resources.Load<Material>("CO_MainMaterials/" + Model.Метериал);
        if (!newMaterial) 
        {
            GetComponent<MeshRenderer>().material = Resources.Load<Material>("Default");
        }
        else
        {
            GetComponent<MeshRenderer>().material = newMaterial;
        }
        GetComponent<MeshRenderer>().material.color = Model.Цвет;
        name = s + "(Clone)";
    }
    public void rcs()
    {
        resetCurrentSettings();
    }
    private Mesh generateAdd(int model)
    {
        ObjParser.Obj newobj = new ObjParser.Obj();


       
        newobj.LoadObj("Ресурсы/" + Model.Модели[model] + ".модель");
        var mesh = new Mesh();
        mesh.name = Model.Модели[model];
        Vector3[] vertices = new Vector3[newobj.VertexList.Count];
        Vector2[] uv = new Vector2[newobj.VertexList.Count]; // Создаем массив UV-координат такого же размера, как и вершины
        List<int> triangles = new List<int>();

        for (int i = 0; i < newobj.VertexList.Count; i++)
        {
            vertices[i] = new Vector3((float)newobj.VertexList[i].X, (float)newobj.VertexList[i].Y, (float)newobj.VertexList[i].Z);
        }

        for (int f = 0; f < newobj.FaceList.Count; f++)
        {
            for (int i = 0; i < newobj.FaceList[f].VertexIndexList.Length; i++)
            {
                int vertexIndex = newobj.FaceList[f].VertexIndexList[i] - 1;
                triangles.Add(vertexIndex);
            }
        }


        for (int i = 0; i < newobj.VertexList.Count; i++)
        {
            uv[i] = (new Vector2((float)newobj.VertexList[i].X - (float)newobj.VertexList[i].Y, (float)newobj.VertexList[i].Z - (float)newobj.VertexList[i].Y));
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv;
        mesh.RecalculateNormals(UnityEngine.Rendering.MeshUpdateFlags.Default);
        return mesh;
    }
    private Mesh generate()
    {
        ObjParser.Obj newobj = new ObjParser.Obj();
        Directory.CreateDirectory("Ресурсы");


        if (!File.Exists("Ресурсы/" + s + ".объект"))
        {
            Model.Рамер = Vector3.one;
            Model.Цвет = Color.red;
            Model.Модель = "Куб";
            File.WriteAllText("Ресурсы/" + s + ".объект", JsonUtility.ToJson(Model));
        }
        if (File.Exists("Ресурсы/" + s + ".объект"))
        {
            Model = JsonUtility.FromJson<CustomObjectData>(File.ReadAllText("Ресурсы/" + s + ".объект"));
        }
       
       

      



        newobj.LoadObj("Ресурсы/" + Model.Модель + ".модель");
        var mesh = new Mesh();
        mesh.name = Model.Модель;
        Vector3[] vertices = new Vector3[newobj.VertexList.Count];
        Vector2[] uv = new Vector2[newobj.VertexList.Count]; // Создаем массив UV-координат такого же размера, как и вершины
        List<int> triangles = new List<int>();

        for (int i = 0; i < newobj.VertexList.Count; i++)
        {
            vertices[i] = new Vector3((float)newobj.VertexList[i].X, (float)newobj.VertexList[i].Y, (float)newobj.VertexList[i].Z);
        }

        for (int f = 0; f < newobj.FaceList.Count; f++)
        {
            for (int i = 0; i < newobj.FaceList[f].VertexIndexList.Length; i++)
            {
                int vertexIndex = newobj.FaceList[f].VertexIndexList[i] - 1;
                triangles.Add(vertexIndex);
            }
        }


        for (int i = 0; i < newobj.VertexList.Count; i++)
        {
            uv[i] = (new Vector2((float)newobj.VertexList[i].X - (float)newobj.VertexList[i].Y, (float)newobj.VertexList[i].Z - (float)newobj.VertexList[i].Y));
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv;
        mesh.RecalculateNormals(UnityEngine.Rendering.MeshUpdateFlags.Default);
        return mesh;
    }
    List<float> v3 = new List<float>();
    public List<Vector3> BuildPosition(List<float> vec)
    {
        List<Vector3> vec3 = new List<Vector3>();
        for (int i = 0; i < vec.Count; i += 3)
        {

            vec3.Add(new(vec[i], vec[i + 1], vec[i + 2]));


        }
        return vec3;
    }
    public List<Vector3> Pos = new List<Vector3>();
    public List<string> data = new List<string>();
    int patrn = 0;
    public void LuaLogic(string loadedCode)
    {
        v3 = new();
        data = new();
        Pos = new();
        //   string scriptCode = @"    
        //	-- defines a Jump function
        //	function Jump (time)
        //		if (time>= 1) then
        //			return 1
        //       else
        //           return 0
        //		end
        //	end";
        UserData.RegisterType<Vector3>();
        UserData.RegisterType<List<float>>();
        UserData.RegisterType<List<string>>();
        Script script = new Script();
        script.Globals["vec3"] = v3;
        script.Globals["ditem"] = data;
        script.DoString(loadedCode);


        DynValue luaFactFunction = script.Globals.Get("Build");

        DynValue res = script.Call(luaFactFunction, new object[]
        {
            ((double)patrn)
        }
        );
        DynValue luaFactFunction2 = script.Globals.Get("Item");

        DynValue res2 = script.Call(luaFactFunction2, new object[]
        {
            ((double)patrn)
        }
        );



        if (res.UserData.Object != null)
        {
            v3 = (List<float>)res.UserData.Object;
        }
        Pos = BuildPosition(v3);
        if (res2.UserData.Object != null)
        {
            data = (List<string>)res2.UserData.Object;
        }
        patrn++;

        for (int i = 0; i < Pos.Count; i++)
        {
            Instantiate(Resources.Load<GameObject>("items/" + data[i]), new Vector3(Pos[i].x, Pos[i].y, Pos[i].z) + transform.position, Quaternion.identity);
        }


    }
    public void sec10()
    {
    }
}
