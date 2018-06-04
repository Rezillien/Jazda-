using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.FileObjects;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class FileManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    private List<List<Vector3>> polygons = new List<List<Vector3>>();


    public GameObject CrossRoadType;
    public GameObject LineType;
    public GameObject GreenPathType;

    private bool outputFlag = true;
    private bool inputFlag = true;

    private string outputName = "output";

    private string inputName = "input";

    private float asd = 0.0f;

    public string file;

    // Update is called once per frame
    void Update()
    {
        if (outputFlag && Input.GetButtonDown(outputName))
        {
            var root = new Root();
            var crossRoads = new List<CrossRoad>();
            var streets = new List<Street>();
            var linesTo = new List<LaneTo>();

            var cnt = 0;

            var scene = SceneManager.GetActiveScene();
            var objects = scene.GetRootGameObjects();

            foreach (var obj in objects)
            {
                if (obj.tag == "crossroad")
                {
                    var position = obj.transform.position;
                    crossRoads.Add(new CrossRoad{id=cnt++,x=position.x,y=position.y});
                } else if (obj.tag == "line")
                {
                    var position = obj.transform.position;
                    var scale = obj.transform.localScale;
                    var angle = obj.transform.eulerAngles;
                    linesTo.Add(new LaneTo{anglex = angle.x,angley = angle.y,anglez = angle.z,scalex = scale.x,scaley = scale.y,way = 1,x=position.x,y=position.y});
                } else if (obj.tag == "greenpath")
                {
                    var position = obj.transform.position;
                    var scale = obj.transform.localScale;
                    var angle = obj.transform.eulerAngles;
                    linesTo.Add(new LaneTo { anglex = angle.x, angley = angle.y, anglez = angle.z, scalex = scale.x, scaley = scale.y, way = 0, x = position.x, y = position.y });

                }

            }

           

            root.streets = streets;
            root.crossRoads = crossRoads;
            root.lanes = linesTo;
            var dataString = JsonConvert.SerializeObject(root, Formatting.Indented);
            outputFlag = false;
            StreamWriter sw = new StreamWriter(file);
            sw.Write(dataString);
            sw.Close();
        }

        if (Input.GetButtonUp(outputName))
        {
            outputFlag = true;
        }

        if (inputFlag && Input.GetButtonDown(inputName))
        {
            inputFlag = false;
            StreamReader sr = new StreamReader(file);

            var data = sr.ReadToEnd();

            Root root = JsonConvert.DeserializeObject<Root>(data);

            sr.Close();

            convertVariables(root);
        }

        if (Input.GetButtonUp(inputName))
        {
            inputFlag = true;
        }
    }

    private void convertVariables(Root root)
    {
        foreach (var VARIABLE in root.crossRoads)
        {
            polygons.Add(new List<Vector3>());
        }
        if(root.lanes != null)
        foreach (var lane in root.lanes)
        {
            GameObject prefab;
            if (lane.way == -1 || lane.way == 1) {
                prefab = BothCollider;
            }
            else {
                prefab = Blocked;
            }

            GameObject obj = Instantiate(prefab) as GameObject;


            Transform transform = obj.GetComponent<Transform>();

            Vector3 position = new Vector3{x=lane.x,y=lane.y};
            Vector3 angle = new Vector3 {x = lane.anglex, y = lane.angley, z = lane.anglez};
            Vector3 scale = new Vector3{x=lane.scalex,y=lane.scaley};

//            float length = Vector3.Distance(start, end) - 8f;
//            print(length);

            transform.SetPositionAndRotation(position, new Quaternion(0, 0, 0, 0));
            //                transform.Translate(middle);
            print(angle);
//            transform.RotateAround(middle, Vector3.forward, angle);
            transform.eulerAngles = angle;
            transform.localScale = scale;
        }

        Vector3 start = new Vector3();
        Vector3 end = new Vector3();
        foreach (var street in root.streets)
        {
            start.x = //Camera.main.pixelWidth * 
                root.crossRoads[street.crossRoadFrom].x * 1f - 52.5f;
            start.y = //Camera.main.pixelHeight * 
                root.crossRoads[street.crossRoadFrom].y * 1f - 52.5f;
            start.z = 0f;
            end.x = //Camera.main.pixelWidth * 
                root.crossRoads[street.crossRoadTo].x * 1f - 52.5f;
            end.y = //Camera.main.pixelHeight * 
                root.crossRoads[street.crossRoadTo].y * 1f - 52.5f;
            end.z = 0f;
//            start = Camera.main.ScreenToWorldPoint(start);
//            end = Camera.main.ScreenToWorldPoint(end);
//            print(start.x);
//            print(start.y);
//            print(start.z);
            start.z = 0;
            end.z = 0;
            polygons[street.crossRoadFrom].Add(Camera.main.ScreenToWorldPoint(start) + end - start);
            polygons[street.crossRoadTo].Add(Camera.main.ScreenToWorldPoint(end) + start - end);
            createStreet(start, end, street.lanes);


        }

        foreach (var cross in root.crossRoads)
        {
            GameObject crs = genericCrossRoad;


            
            //        crossroadTransform.RotateAround(middle, Vector3.forward, angle);
            GameObject crossroad2 = Instantiate(crs) as GameObject;
            Transform crossroadTransform2 = crossroad2.GetComponent<Transform>();
            crossroadTransform2.SetPositionAndRotation(new Vector3{x=cross.x,y=cross.y}, new Quaternion(0, 0, 0, 0));

            crossroadTransform2.localScale += new Vector3(4f, 4f, 0);
        }


        foreach (var polygon in polygons)
        {
            List<Vector2> vertices2D = new List<Vector2>();
            foreach (var verticle in polygon)
            {
                vertices2D.Add(new Vector2 {x = verticle.x, y = verticle.y});
            }

            Triangulator tr = new Triangulator(vertices2D.ToArray());
            int[] indices = tr.Triangulate();

            Mesh msh = new Mesh();
            msh.vertices = polygon.ToArray();
            msh.triangles = indices;
            msh.RecalculateNormals();
            msh.RecalculateBounds();

            MeshFilter filter = GetComponent<MeshFilter>();
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            filter.mesh = msh;
        }
    }

    public GameObject lane2;
    public GameObject lane3;

    public GameObject leftCollider;
    public GameObject RightCollider;
    public GameObject Blocked;
    public GameObject BothCollider;

    public GameObject genericCrossRoad;


    public void createStreet(Vector3 start, Vector3 end, List<Lane> lanes)
    {
        GameObject prefab;

        start.z = 1;
        end.z = 1;

        start.x += 1.5f * Camera.main.orthographicSize;
        start.y += 1.5f * Camera.main.orthographicSize;

        end.x += 1.5f * Camera.main.orthographicSize;
        end.y += 1.5f * Camera.main.orthographicSize;

        if (lanes.Count % 2 == 0)
            foreach (var lane in lanes)
            {
                if (lane.way == -1 || lane.way == 1)
                {
                    prefab = BothCollider;
                }
                else
                {
                    prefab = Blocked;
                }

                GameObject obj = Instantiate(prefab) as GameObject;


                Transform transform = obj.GetComponent<Transform>();

                Vector3 middle = (start + end) / 2;
                float angle = Vector3.SignedAngle(
                    Vector3.right,
                    end - start,
                    Vector3.forward
                );
                float offset = lanes.IndexOf(lane) - lanes.Count / 2 + 0.5f;
                Vector3 off = start - middle;
                float len = Vector3.Distance(new Vector3(0, 0, 0), off);
                off.x /= len;
                off.y /= len;
                off.z /= len;

                off *= angle;


//                middle += off;

                middle.x += Convert.ToSingle(Math.Cos(Convert.ToDouble(angle + 90) * Math.PI / 180f)) * offset; //TODO
                middle.y += Convert.ToSingle(Math.Sin(Convert.ToDouble(angle + 90) * Math.PI / 180f)) * offset;

                float length = Vector3.Distance(start, end) - 8f;
                print(length);

                transform.SetPositionAndRotation(middle, new Quaternion(0, 0, 0, 0));
//                transform.Translate(middle);
                print(angle);
                transform.RotateAround(middle, Vector3.forward, angle);
                transform.localScale += new Vector3(length, 0, 0);
            }
        else
        {
            foreach (var lane in lanes)
            {
                if (lane.way == -1 || lane.way == 1)
                {
                    prefab = BothCollider;
                }
                else
                {
                    prefab = Blocked;
                }

                GameObject obj = Instantiate(prefab) as GameObject;


                Transform transform = obj.GetComponent<Transform>();

                Vector3 middle = (start + end) / 2;
                float angle = Vector3.SignedAngle(
                    Vector3.right,
                    end - start,
                    Vector3.forward
                );
                float offset = lanes.IndexOf(lane) - lanes.Count / 2 + 0.5f;
                middle.x += Convert.ToSingle(Math.Cos(Convert.ToDouble(angle + 90) * Math.PI / 180f)) * offset;
                middle.y += (Convert.ToSingle(Math.Sin(Convert.ToDouble(angle + 90) * Math.PI / 180f))) * offset;
                float length = Vector3.Distance(start, end) - 10f;
                print(length);

                transform.SetPositionAndRotation(middle, new Quaternion(0, 0, 0, 0));
                print(angle);
                transform.RotateAround(middle, Vector3.forward, angle);
                transform.localScale += new Vector3(length, 0, 0);
            }
        }

        

//        crossroadTransform.RotateAround(start, Vector3.forward, angle);
//        crossroadTransform2.RotateAround(end, Vector3.forward, angle);
    }
}

public class Triangulator
{
    private List<Vector2> m_points = new List<Vector2>();

    public Triangulator(Vector2[] points)
    {
        m_points = new List<Vector2>(points);
    }

    public int[] Triangulate()
    {
        List<int> indices = new List<int>();

        int n = m_points.Count;
        if (n < 3)
            return indices.ToArray();

        int[] V = new int[n];
        if (Area() > 0)
        {
            for (int v = 0; v < n; v++)
                V[v] = v;
        }
        else
        {
            for (int v = 0; v < n; v++)
                V[v] = (n - 1) - v;
        }

        int nv = n;
        int count = 2 * nv;
        for (int m = 0, v = nv - 1; nv > 2;)
        {
            if ((count--) <= 0)
                return indices.ToArray();

            int u = v;
            if (nv <= u)
                u = 0;
            v = u + 1;
            if (nv <= v)
                v = 0;
            int w = v + 1;
            if (nv <= w)
                w = 0;

            if (Snip(u, v, w, nv, V))
            {
                int a, b, c, s, t;
                a = V[u];
                b = V[v];
                c = V[w];
                indices.Add(a);
                indices.Add(b);
                indices.Add(c);
                m++;
                for (s = v, t = v + 1; t < nv; s++, t++)
                    V[s] = V[t];
                nv--;
                count = 2 * nv;
            }
        }

        indices.Reverse();
        return indices.ToArray();
    }

    private float Area()
    {
        int n = m_points.Count;
        float A = 0.0f;
        for (int p = n - 1, q = 0; q < n; p = q++)
        {
            Vector2 pval = m_points[p];
            Vector2 qval = m_points[q];
            A += pval.x * qval.y - qval.x * pval.y;
        }

        return (A * 0.5f);
    }

    private bool Snip(int u, int v, int w, int n, int[] V)
    {
        int p;
        Vector2 A = m_points[V[u]];
        Vector2 B = m_points[V[v]];
        Vector2 C = m_points[V[w]];
        if (Mathf.Epsilon > (((B.x - A.x) * (C.y - A.y)) - ((B.y - A.y) * (C.x - A.x))))
            return false;
        for (p = 0; p < n; p++)
        {
            if ((p == u) || (p == v) || (p == w))
                continue;
            Vector2 P = m_points[V[p]];
            if (InsideTriangle(A, B, C, P))
                return false;
        }

        return true;
    }

    private bool InsideTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
    {
        float ax, ay, bx, by, cx, cy, apx, apy, bpx, bpy, cpx, cpy;
        float cCROSSap, bCROSScp, aCROSSbp;

        ax = C.x - B.x;
        ay = C.y - B.y;
        bx = A.x - C.x;
        by = A.y - C.y;
        cx = B.x - A.x;
        cy = B.y - A.y;
        apx = P.x - A.x;
        apy = P.y - A.y;
        bpx = P.x - B.x;
        bpy = P.y - B.y;
        cpx = P.x - C.x;
        cpy = P.y - C.y;

        aCROSSbp = ax * bpy - ay * bpx;
        cCROSSap = cx * apy - cy * apx;
        bCROSScp = bx * cpy - by * cpx;

        return ((aCROSSbp >= 0.0f) && (bCROSScp >= 0.0f) && (cCROSSap >= 0.0f));
    }


    List<GameObject> GetAllObjectsInScene()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                continue;

            if (!EditorUtility.IsPersistent(go.transform.root.gameObject))
                continue;

            objectsInScene.Add(go);
        }

        return objectsInScene;
    }
}