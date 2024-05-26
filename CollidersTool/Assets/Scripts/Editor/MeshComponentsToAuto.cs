#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using System.IO;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Object;



    [ShowOdinSerializedPropertiesInInspector]
    public class MeshComponentsToAuto
    {
        [MenuItem("TAOptimization/MeshFilterMeshRendererAdd")]

        private static void FilterRendererAdd()
        {
            if (Selection.count == 0)
            {
                EditorUtility.DisplayDialog("MeshFilterMeshRendererAdd", "Тебе нужно выделить коллайдеры для добавления", "Ok");
                return;
            }
            Iterate();
        }
        private static void Iterate()
        {

            //var material = Resources.Load<Material>("Assets/Resources/PolygonPrototype_Global_Grid_01_1.mat");
            //var filter = Resources.Load<Mesh>("Assets/Resources/Cube_1.asset");
            var boxcolliderGameObjects = Selection.gameObjects
                .SelectMany(m => m.GetComponentsInChildren(typeof(BoxCollider), false))
                .Select(m => m.gameObject)
                .ToArray();

            foreach (GameObject go in boxcolliderGameObjects)
            {
                go.AddComponent<MeshFilter>();
                var trgMeshFilter = go.GetComponent<MeshFilter>();
                trgMeshFilter.sharedMesh = Resources.Load("Cube") as Mesh;

                go.AddComponent<MeshRenderer>();
                var trgRend = go.GetComponent<MeshRenderer>();
                trgRend.sharedMaterial = Resources.Load("TestMat") as Material;


            }
        }

        [MenuItem("TAOptimization/MeshFilterMeshRendererDelete")]
        private static void FilterRendererDelete()
        {
            if (Selection.count == 0)
            {
                EditorUtility.DisplayDialog("MeshFilterMeshRendererDelete", "Тебе нужно выделить коллайдеры для добавления", "Ok");
                return;
            }
            Iterate2();
        }
        private static void Iterate2()
        {
            var boxcolliderGameObjects = Selection.gameObjects
                .SelectMany(m => m.GetComponentsInChildren(typeof(BoxCollider), false))
                .Select(m => m.gameObject)
                .ToArray();

            foreach (GameObject go in boxcolliderGameObjects)
            {
                if (go.GetComponent<MeshFilter>() != null)
                {

                    GameObject.DestroyImmediate(go.GetComponent<MeshFilter>());
                }

                if (go.GetComponent<MeshRenderer>() != null)
                {

                    GameObject.DestroyImmediate(go.GetComponent<MeshRenderer>());
                }

            }
        }
    }

#endif