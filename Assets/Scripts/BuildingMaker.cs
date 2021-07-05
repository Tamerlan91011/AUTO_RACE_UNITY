using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SocialPlatforms;

class BuildingMaker : InfrastructureBehaviour
{


    public Material building;

    IEnumerator Start()
    {
        while (!map.IsReady)
        {
            yield return null;
        }

        // Для каждого пути, который является зданием и количество узлов в котором больше, чем 1
        foreach (var way in map.ways.FindAll( (w) => { return w.IsBuilding && w.NodeIDs.Count > 1;  }))
        {
            // Создаём новый игровой объект
            GameObject go = new GameObject();
            GameObject roof = new GameObject();

            go.tag = "Building";
            roof.tag = "Roof";
            // Получаем локальный центр здания
            Vector3 localOrigin = GetCentre(way);
            // Перемещаем созданный объект в позицию, координаты которой равны разности координат локального центра и центра границ всей карты
            go.transform.position = localOrigin - map.bounds.Centre;
            roof.transform.position = localOrigin - map.bounds.Centre;

            // Создаём фильтр и отрисовщик мешей
            MeshFilter mf = go.AddComponent<MeshFilter>();
            MeshFilter mf1 = roof.AddComponent<MeshFilter>();
            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            MeshRenderer mr1 = roof.AddComponent<MeshRenderer>();

            // Выбираем building в качестве материала, который будет использовать рендерер mr
            mr.material = building;
            mr1.material = building;

            // Создаём контейнеры векторов, нормалей, UV и индексов для дальнейшей отрисовки
            List<Vector3> vectors = new List<Vector3>();
            List<Vector3> rvectors = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<int> indicies = new List<int>();

            Vector3[] roofVectors;
            int[] roofTriangles;
            Vector2[] roofUV;

            int verticesCount = way.NodeIDs.Count;

            for (int i = 0; i < verticesCount; i++)
            {
                OsmNode vertex = map.nodes[way.NodeIDs[i]];

                Vector3 low_vertex3 = new Vector3(vertex.X, way.Height, vertex.Y) - localOrigin;

                rvectors.Add(low_vertex3);
            }

            Triangulation.GetResult(rvectors, false, Vector3.up, out roofVectors, out roofTriangles, out roofUV);

            for (int i = 1; i < way.NodeIDs.Count; i++)
            {

                // Создаём две точки, которые изначально содержат координаты X и Y и изначально являются узлами на карте
                OsmNode p1 = map.nodes[way.NodeIDs[i - 1]];
                OsmNode p2 = map.nodes[way.NodeIDs[i]];

                // Создаём трёхмерные векторы, которые являются копиями точек, вытягивая из них координаты X, Y, а также обнуляя координату Z
                Vector3 pp1 = new Vector3(p1.X, 0, p1.Y);
                Vector3 pp2 = new Vector3(p2.X, 0, p2.Y);

                // Создаём новые векторы, координаты которых равны разности коориднат первоначальных векторов и локального центра контура
                Vector3 v1 = pp1 - localOrigin;
                Vector3 v2 = pp2 - localOrigin;

                // Придаём высоту за счёт добавления новых векторов с ненулевой Z-координатой
                Vector3 v3 = v1 + new Vector3(0, way.Height, 0);
                Vector3 v4 = v2 + new Vector3(0, way.Height, 0);

                vectors.Add(v1);
                vectors.Add(v2);
                vectors.Add(v3);
                vectors.Add(v4);

                uvs.Add(new Vector2(0, 0));
                uvs.Add(new Vector2(1, 0));
                uvs.Add(new Vector3(0, 1));
                uvs.Add(new Vector3(1, 1));

                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);

                int idx1, idx2, idx3, idx4;
                idx4 = vectors.Count - 1;
                idx3 = vectors.Count - 2;
                idx2 = vectors.Count - 3;
                idx1 = vectors.Count - 4;

                // Первый треугольник v1, v3, v2
                indicies.Add(idx1);
                indicies.Add(idx3);
                indicies.Add(idx2);

                // Первый треугольник v3, v4, v2
                indicies.Add(idx3);
                indicies.Add(idx4);
                indicies.Add(idx2);

                // Первый треугольник v2, v3, v1
                indicies.Add(idx2);
                indicies.Add(idx3);
                indicies.Add(idx1);

                // Первый треугольник v2, v4, v3
                indicies.Add(idx2);
                indicies.Add(idx4);
                indicies.Add(idx3);

            }

            mf.mesh.vertices = vectors.ToArray();
            mf.mesh.normals = normals.ToArray();
            mf.mesh.triangles = indicies.ToArray();
            mf.mesh.uv = uvs.ToArray();

            mf1.mesh.vertices = roofVectors;
            mf1.mesh.RecalculateNormals();
            mf1.mesh.RecalculateBounds();
            mf1.mesh.triangles = roofTriangles;
            mf1.mesh.uv = roofUV;

            go.AddComponent<MeshCollider>();

        }
    }


}// Для каждого узла в пути
/*for (int i = 1; i < way.NodeIDs.Count; i++)
{

    // Создаём две точки, которые изначально содержат координаты X и Y и изначально являются узлами на карте
    OsmNode p1 = map.nodes[way.NodeIDs[i - 1]];
    OsmNode p2 = map.nodes[way.NodeIDs[i]];

    // Создаём трёхмерные векторы, которые являются копиями точек, вытягивая из них координаты X, Y, а также обнуляя координату Z
    Vector3 pp1 = new Vector3(p1.X, 0, p1.Y);
    Vector3 pp2 = new Vector3(p2.X, 0, p2.Y);

    // Создаём новые векторы, координаты которых равны разности коориднат первоначальных векторов и локального центра контура
    Vector3 v1 = pp1 - localOrigin;
    Vector3 v2 = pp2 - localOrigin;

    // Придаём высоту за счёт добавления новых векторов с ненулевой Z-координатой
    Vector3 v3 = v1 + new Vector3(0, way.Height, 0);
    Vector3 v4 = v2 + new Vector3(0, way.Height, 0);

    vectors.Add(v1);
    vectors.Add(v2);
    vectors.Add(v3);
    vectors.Add(v4);

    uvs.Add(new Vector2(0, 0));
    uvs.Add(new Vector2(1, 0));
    uvs.Add(new Vector3(0, 1));
    uvs.Add(new Vector3(1, 1));

    normals.Add(-Vector3.forward);
    normals.Add(-Vector3.forward);
    normals.Add(-Vector3.forward);
    normals.Add(-Vector3.forward);

    int idx1, idx2, idx3, idx4;
    idx4 = vectors.Count - 1;
    idx3 = vectors.Count - 2;
    idx2 = vectors.Count - 3;
    idx1 = vectors.Count - 4;

    // Первый треугольник v1, v3, v2
    indicies.Add(idx1);
    indicies.Add(idx3);
    indicies.Add(idx2);

    // Первый треугольник v3, v4, v2
    indicies.Add(idx3);
    indicies.Add(idx4);
    indicies.Add(idx2);

    // Первый треугольник v2, v3, v1
    indicies.Add(idx2);
    indicies.Add(idx3);
    indicies.Add(idx1);

    // Первый треугольник v2, v4, v3
    indicies.Add(idx2);
    indicies.Add(idx4);
    indicies.Add(idx3);

}

mf.mesh.vertices = vectors.ToArray();
mf.mesh.normals = normals.ToArray();
mf.mesh.triangles = indicies.ToArray();
mf.mesh.uv = uvs.ToArray();*/
