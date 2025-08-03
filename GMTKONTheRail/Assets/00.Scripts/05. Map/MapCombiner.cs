using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class MapCombiner
{
    private Dictionary<Material, List<CombineInstance>> _combineDictionary
         = new Dictionary<Material, List<CombineInstance>>();

    private List<Transform> _exceptedList = new List<Transform>();

    public MapObject Execute(MapObject root)
    {
        FindMeshData(root.transform);
        MapObject bundle = CombineMesh(root.transform);
        ClearMeshData();

        return bundle;
    }


    public void FindMeshData(Transform root)
    {
        Transform current = root;

        foreach(Transform point in current)
        {
            var n0 = point.TryGetComponent<ExceptMeshCombine>(out var meshCombine);
            var n1 = point.TryGetComponent<RailManagement>(out var rail);
            if (n0 || n1)
            {
                _exceptedList.Add(point);
                continue;
            }

            FindMeshData(point);

            var m1 = point.TryGetComponent<MeshRenderer>(out var meshRender);
            var m2 = point.TryGetComponent<MeshFilter>(out var meshFilter);

            if (m1 && m2)
            {
                Matrix4x4 trm = Matrix4x4.TRS
                    (point.transform.position,
                    point.transform.rotation,
                    point.transform.lossyScale);

                Mesh meshSample = Mesh.Instantiate(meshFilter.sharedMesh);

                var combineInstance = new CombineInstance();
                combineInstance.mesh = meshSample;
                combineInstance.transform = trm;

                for(int i = 0; i<meshRender.sharedMaterials.Length; ++i)
                {
                    combineInstance.subMeshIndex = i;
                    if (meshRender.sharedMaterials[i] == null) return;

                    if (!_combineDictionary.TryGetValue(meshRender.sharedMaterials[i], out var list))
                    {
                        list = _combineDictionary[meshRender.sharedMaterials[i]]
                            = new List<CombineInstance>();
                    }

                    list.Add(combineInstance);
                }
            }
        }
    }

    private MapObject CombineMesh(Transform root)
    {
        GameObject bundle = new GameObject($"{root.name}_combine_result", typeof(MapObject));
        var mapObj = bundle.GetComponent<MapObject>();

        foreach (var pair in _combineDictionary)
        {
            var result = new Mesh();
            result.indexFormat = IndexFormat.UInt32;
            result.CombineMeshes(pair.Value.ToArray(), true);

            GameObject obj = new GameObject($"{root.name}_{pair.Key.name}",
                typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));

            var filter = obj.GetComponent<MeshFilter>();
            filter.mesh = result;
            filter.sharedMesh = result;

            var renderer = obj.GetComponent<MeshRenderer>();
            renderer.material = pair.Key;

            var collider = obj.GetComponent<MeshCollider>();
            collider.sharedMesh = result;

            obj.transform.SetParent(bundle.transform);
        }

        GameObject exceptListGameObj = new GameObject($"except_list");

        foreach (var exceptobj in _exceptedList)
        {
            var newObj = GameObject.Instantiate(exceptobj);
            newObj.transform.SetParent(exceptListGameObj.transform);

            if (newObj.TryGetComponent<RailManagement>(out var rail))
            {
                rail.Init();
                mapObj.SetRail(rail);
            }
        }

        exceptListGameObj.transform.SetParent(bundle.transform);

        bundle.gameObject.SetActive(false);

        return mapObj;
    }

    private void ClearMeshData()
    {
        _combineDictionary.Clear();
        _exceptedList.Clear();
    }
}
