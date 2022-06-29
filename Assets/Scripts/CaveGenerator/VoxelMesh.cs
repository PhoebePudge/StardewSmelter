using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Code sourced from online forum
/// </summary>
public class VoxelMesh : MonoBehaviour {
    public bool Modified;

    private VoxelBuilder _voxelBuilder;

    private VoxelData[,,] _voxels;

    private int _height;
    private int _size;

    public VoxelMesh() {
        _voxelBuilder = new VoxelBuilder();
    }

    public void GenerateTerrainData(int height, int size, VoxelData[,,] _voxels) {
        _height = height;
        _size = size;

        this._voxels = _voxels;
        Modified = true;
    }

    public bool UpdateMesh() {
        if (Modified) {
            UpdateMeshData();
            Modified = false;

            return true;
        }

        return false;
    }

    public VoxelData GetVoxel(Vector3Int coordinates) {
        return _voxels[coordinates.x, coordinates.y, coordinates.z];
    }

    public int GetHighestPoint(Vector2Int coordinates) {
        for (var z = 0; z < _height; z++) {
            if (_voxels[coordinates.x, coordinates.y, z] == null) {
                return z;
            }
        }

        return -1;
    }

    public bool AddVoxel(Vector3Int coordinates) {
        if (_voxels[coordinates.x, coordinates.y, coordinates.z] == null) {
            _voxels[coordinates.x, coordinates.y, coordinates.z] = new VoxelData {
                Visibility = GetVisibilityData(coordinates.x, coordinates.y, coordinates.z),
                Type = VoxelType.Dirt
            };

            Modified = true;
            UpdateNeighbourVoxels(coordinates, false);

            return true;
        }

        return false;
    }

    public bool RemoveVoxel(Vector3Int coordinates) {
        if (_voxels[coordinates.x, coordinates.y, coordinates.z] != null) {
            _voxels[coordinates.x, coordinates.y, coordinates.z] = null;
            Modified = true;

            UpdateNeighbourVoxels(coordinates, true);

            return true;
        }

        return false;
    }

    private void UpdateNeighbourVoxels(Vector3Int coordinates, bool voxelRemoved) {
        if (coordinates.x > 0 && _voxels[coordinates.x - 1, coordinates.y, coordinates.z] != null) {
            _voxels[coordinates.x - 1, coordinates.y, coordinates.z].Visibility.Right = voxelRemoved;
        }

        if (coordinates.x < _size - 1 && _voxels[coordinates.x + 1, coordinates.y, coordinates.z] != null) {
            _voxels[coordinates.x + 1, coordinates.y, coordinates.z].Visibility.Left = voxelRemoved;
        }

        if (coordinates.y > 0 && _voxels[coordinates.x, coordinates.y - 1, coordinates.z] != null) {
            _voxels[coordinates.x, coordinates.y - 1, coordinates.z].Visibility.Front = voxelRemoved;
        }

        if (coordinates.y < _size - 1 && _voxels[coordinates.x, coordinates.y + 1, coordinates.z] != null) {
            _voxels[coordinates.x, coordinates.y + 1, coordinates.z].Visibility.Back = voxelRemoved;
        }

        if (coordinates.z > 0 && _voxels[coordinates.x, coordinates.y, coordinates.z - 1] != null) {
            _voxels[coordinates.x, coordinates.y, coordinates.z - 1].Visibility.Top = voxelRemoved;
        }

        if (coordinates.z < _size - 1 && _voxels[coordinates.x, coordinates.y, coordinates.z + 1] != null) {
            _voxels[coordinates.x, coordinates.y, coordinates.z + 1].Visibility.Bottom = voxelRemoved;
        }
    }


    private void UpdateMeshData() {
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uv = new List<Vector2>();

        for (var x = 0; x < _size; x++) {
            for (var y = 0; y < _size; y++) {
                for (var z = 0; z < _height; z++) {
                    var voxelData = _voxels[x, y, z];

                    if (voxelData != null) {

                        if (voxelData.Visibility == null) {
                            voxelData.Visibility = GetVisibilityData(x, y, z);
                        }

                        _voxelBuilder.Position = new Vector3(x, z, y);
                        _voxelBuilder.TopFace = voxelData.Visibility.Top;
                        _voxelBuilder.BottomFace = voxelData.Visibility.Bottom;
                        _voxelBuilder.FrontFace = voxelData.Visibility.Front;
                        _voxelBuilder.BackFace = voxelData.Visibility.Back;
                        _voxelBuilder.RightFace = voxelData.Visibility.Right;
                        _voxelBuilder.LeftFace = voxelData.Visibility.Left;
                        _voxelBuilder.TextureType = voxelData.Type;

                        _voxelBuilder.GenerateAndAddToLists(vertices, triangles, uv);
                    }
                }
            }
        }

        var meshFilter = GetComponent<MeshFilter>();
        var meshCollider = GetComponent<MeshCollider>();

        meshFilter.mesh.Clear();
        meshFilter.mesh.vertices = vertices.ToArray();
        meshFilter.mesh.triangles = triangles.ToArray();
        meshFilter.mesh.uv = uv.ToArray();
        meshFilter.mesh.RecalculateNormals();

        meshCollider.sharedMesh = meshFilter.mesh;
    }

    private VoxelVisibilityData GetVisibilityData(int x, int y, int z) {
        return new VoxelVisibilityData {
            Top = z == _height - 1 || _voxels[x, y, z + 1] == null,
            Bottom = z > 0 && _voxels[x, y, z - 1] == null,

            Front = y < _size - 1 && _voxels[x, y + 1, z] == null,
            Back = y > 0 && _voxels[x, y - 1, z] == null,

            Right = x < _size - 1 && _voxels[x + 1, y, z] == null,
            Left = x > 0 && _voxels[x - 1, y, z] == null
        };
    }
}