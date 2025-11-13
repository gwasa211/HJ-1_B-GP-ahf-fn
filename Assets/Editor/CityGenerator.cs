using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CityGenerator : EditorWindow                   //Unity 에디터 창을 만드는 클래스
{
    private int gridSizeX = 10;                    //도시 가로 크기
    private int gridSizeZ = 10;                     //도시 세로 크기
    private float buildingSpacing = 15;             //건물 사이 간격
    private float roadWidth = 5f;                   //도로 폭
    private bool makeStatic = true;                 //생성되는 오브젝트를 Static으로 만들지 여부

    [MenuItem("Tools/City Generator")]              //유니티 상단 메뉴에 버튼 추가
    public static void ShowWindow()
    {
        GetWindow<CityGenerator>("City Generator");                   //에디터 창 열기
    }
    private void OnGUI()
    {
        GUILayout.Label("Simple City Generator", EditorStyles.boldLabel);
        gridSizeX = EditorGUILayout.IntField("Grid Size X", gridSizeX);
        gridSizeZ = EditorGUILayout.IntField("Grid Size Z", gridSizeZ);

        buildingSpacing = EditorGUILayout.FloatField("Building Spacing", buildingSpacing);

        roadWidth = EditorGUILayout.FloatField("Road Width", roadWidth);
        makeStatic = EditorGUILayout.Toggle("Make Static", makeStatic);
        GUILayout.Space(10);
        if (GUILayout.Button("Generate City"))
        {

        }
        if (GUILayout.Button("Clear City"))
        {

        }
    }
    private void CreateBuilding(Vector3 position, Transform parent)
    {
        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        building.name = "Building";

        float height = Random.Range(5.0f, 20.0f);
        building.transform.position = position + Vector3.up * height / 2.0f;
        building.transform.localScale = new Vector3(buildingSpacing - roadWidth - 1f, height, buildingSpacing - roadWidth - 1f);
        building.transform.SetParent(parent);

        Renderer renderer = building.GetComponent<Renderer>();
        renderer.material.color = new Color(Random.Range(0.5f, 0.8f), Random.Range(0.5f, 0.8f), Random.Range(0.5f, 0.8f));

        if (makeStatic)
        {
            building.isStatic = true;
        }

    }

    private void CreateRoad(Vector3 position, Transform parent)
    {
        GameObject road = GameObject.CreatePrimitive(PrimitiveType.Cube);

        road.transform.position = position + Vector3.up * 0.1f;
        road.transform.localScale = new Vector3(buildingSpacing, 0.2f, buildingSpacing);
        road.transform.SetParent(parent);

        Renderer renderer = road.GetComponent<Renderer>();
        renderer.material.color = new Color(0.3f, 0.3f, 0.3f);

        if (makeStatic)
        {
            road.isStatic = true;
        }

    }
}
