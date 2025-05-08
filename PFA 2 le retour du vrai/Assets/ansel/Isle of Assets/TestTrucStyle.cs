using UnityEngine;

public class TestTrucStyle : MonoBehaviour
{
    public Material mat;
    float count;

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        mat.mainTextureOffset = new Vector2 (count, 0);
        if (count >= 10)
        {
            count = 0;
        }
    }
}
