using UnityEngine;
using System.IO;

public class TextureApplicatior : MonoBehaviour {
    public string imagePath; // PNG görüntüsünün yolu

    void Start() {
        CreateMeshFromPNG(imagePath);
    }

    void CreateMeshFromPNG(string filePath) {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(fileData);

        GameObject meshObject = new GameObject("CustomMesh");
        MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();

        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.mainTexture = texture;

        Mesh mesh = new Mesh();
        float width = texture.width;
        float height = texture.height;
        float scaleFactor = 0.01f; // Piksel boyutunu Unity birimlerine dönüştürmek için ölçeklendirme faktörü

        // Mesh köşelerini oluştur
        Vector3[] vertices = new Vector3[4] {
            new Vector3(0, 0, 0),
            new Vector3(width * scaleFactor, 0, 0),
            new Vector3(0, height * scaleFactor, 0),
            new Vector3(width * scaleFactor, height * scaleFactor, 0)
        };

        // Üçgenleri oluştur
        int[] tris = new int[6] {
            0, 2, 1,
            2, 3, 1
        };

        // UV koordinatlarını ayarla
        Vector2[] uv = new Vector2[4] {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.uv = uv;

        meshFilter.mesh = mesh;
    }
}