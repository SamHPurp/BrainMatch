using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    GameObject selector;
    GameObject[] go = new GameObject[4];

    Mesh goCube, goSphere;

    Shader specular;

    Color[] colours = new Color[4] {Color.red,Color.blue,Color.green,Color.black};

    void Start()
    {
        specular = Shader.Find("Specular");

        BuildInitialField();
    }

    void BuildInitialField()
    {
        for (int i = 0; i < go.Length; i++)
        {
            go[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go[i].name = "Object " + (i+1);
            go[i].transform.parent = gameObject.transform;
            go[i].GetComponent<Renderer>().material.shader = specular;
        }

        go[0].transform.position = new Vector2(-2 ,  0);
        go[1].transform.position = new Vector2( 2 ,  0);
        go[2].transform.position = new Vector2( 0 , -2);
        go[3].transform.position = new Vector2( 0 ,  2);

        selector = GameObject.CreatePrimitive(PrimitiveType.Cube);
        selector.transform.position = new Vector2(0, 0);
        selector.GetComponent<Renderer>().material.shader = specular;
        selector.name = "Selector";

        GameObject dummyObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        goSphere = dummyObject.GetComponent<MeshFilter>().mesh;
        goCube = go[0].GetComponent<MeshFilter>().mesh;
        Destroy(dummyObject);

        NewRound();
    }

    void NewRound()
    {
        RandomiseOptions();
        SetColour(selector, colours[Random.Range(0, 4)]);
    }

    void RandomiseOptions()
    {
        int x = Random.Range(0, 4);
        int count = 0;

        while (count < 4)
        {
            SetColour(go[count], colours[x]);
            count++; x++;

            if (x == 4)
                x = 0;
        }
    }

    void SetColour(GameObject go, Color color)
    {
        go.GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    void SetMesh(GameObject go, Mesh theMesh)
    {
        go.GetComponent<MeshFilter>().mesh = theMesh;
    }
}