using UnityEngine;

public class PROTODrawProjection : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private CastSpriteShape _castSpriteShape;

    public void Button()
    {
        print("stp unity");
        Texture2D texture = new(500, 280, TextureFormat.ARGB32, false);

        print("enter step 1");
        for(int i = 0; i < texture.GetPixels().Length; i++)
        {
            texture.GetPixels()[i] = Color.white;
        }

        print("enter step 2");
        foreach (Vector2 point in _castSpriteShape.GetDrawData().points)
        {
            print(point);
            texture.SetPixel((int)point.x, (int)point.y, Color.black);
        }
        texture.Apply();

        print("enter step 3");
        _renderer.material.mainTexture = texture;
    }
}
