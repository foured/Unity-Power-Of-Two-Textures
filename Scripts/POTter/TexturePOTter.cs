using System.IO;
using UnityEngine;
using UnityEditor;

public class TexturePOTter
{
    private Texture2D _texture;

    private Vector2Int _min, _max;
    private Color[,] _pixels;

    private bool _isLoaded = false;

    public TexturePOTter(Texture2D texture)
    {
        _texture = texture;
    }

    public string Process(string path, string newTextureName = null)
    {
        if (!_isLoaded)
        {
            Debug.LogWarning("TexturePOTter wasnt inited.");
            return "";
        }

        int nw = _max.x - _min.x;
        int nh = _max.y - _min.y;

        int wcpot = FindClosestPowerOfTwo(nw);
        int hcpot = FindClosestPowerOfTwo(nh);

        int woffset = (wcpot - nw) / 2;
        int hoffset = (hcpot - nh) / 2;

        Color[,] npixels = new Color[wcpot, hcpot];

        for (int x = 0; x < wcpot; x++)
        {
            for (int y = 0; y < hcpot; y++)
            {
                npixels[x, y] = Color.clear;
            }
        }

        for (int x = _min.x; x <= _max.x; x++)
        {
            for (int y = _min.y; y <= _max.y; y++)
            {
                int newX = x - _min.x + woffset;
                int newY = y - _min.y + hoffset;

                npixels[newX, newY] = _pixels[x, y];
            }
        }

        Color[] npixels1D = new Color[wcpot * hcpot];

        for (int y = 0; y < wcpot; y++)
        {
            for (int x = 0; x < hcpot; x++)
            {
                int index = y * wcpot + x;
                npixels1D[index] = npixels[x, y];
            }
        }

        Texture2D newTexture = new Texture2D(wcpot, hcpot);
        newTexture.SetPixels(npixels1D);
        newTexture.Apply();

        byte[] pngData = newTexture.EncodeToPNG();
        string nname = $"{_texture.name}_POT";
        if (newTextureName != null)
            nname = newTextureName;

        string filePath = $"{path}/{nname}.png";
        File.WriteAllBytes(filePath, pngData);

        string msg = $"New texture saved at: {filePath}";
        Debug.Log(msg);
        return msg;
    }

    public bool SetTextureData()
    {
        if (!_texture.isReadable)
        {
            Debug.LogWarning($"{_texture.name} is marked as unreadable.");
            return false;
        }
        else
        {
            int width = _texture.width;
            int height = _texture.height;

            _min = new Vector2Int(width, height);
            _max = new Vector2Int(0, 0);

            _pixels = new Color[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _pixels[x, y] = _texture.GetPixel(x, y);
                    if (IsNotEmpy(_pixels[x, y]))
                    {
                        if (_min.x > x)
                            _min.x = x;
                        if (_min.y > y)
                            _min.y = y;

                        if (_max.x < x)
                            _max.x = x;
                        if (_max.y < y)
                            _max.y = y;
                    }
                }
            }
            _isLoaded = true;
            return true;
        }
    }

    private int FindClosestPowerOfTwo(int n)
    {
        int power = 1;
        while (power < n)
        {
            power *= 2;
        }
        return power;
    }

    private bool IsNotEmpy(Color color)
    {
        return color != Color.clear;
    }

#if UNITY_EDITOR
    public static void SetReadWriteEnable(Texture2D texture, bool enable)
    {
        string texturePath = AssetDatabase.GetAssetPath(texture);

        TextureImporter textureImporter = AssetImporter.GetAtPath(texturePath) as TextureImporter;

        bool oldIsReadable = textureImporter.isReadable;

        textureImporter.isReadable = enable;
        AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);
        Debug.Log($"Read/Write Enabled set to {enable} for {texture.name}");
    }
#endif
}
