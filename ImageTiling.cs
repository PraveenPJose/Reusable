using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTiling : MonoBehaviour {
    [System.Serializable]
    public struct Int2DFormat
    {
        public int x, y;
    }
    [System.Serializable]
    public struct Float2DFormat
    {
        public float x, y;
    }
    public GameObject tilePices;
    public Float2DFormat xyEdge;
    public Float2DFormat xyEdgeDifferents;
    public Int2DFormat xyLength;
    public Material mat;
    public Texture image;
    public float xx, yy;
	void Start ()
    {
        CreateTileWall();
      //  ChangeImage(image);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void CreateTileWall()
    {
        for(int i=0;i<xyLength.x;i++)
        {
            for (int j = 0; j < xyLength.y; j++)
            {
                GameObject tile=Instantiate(tilePices, transform);
                tile.transform.localPosition = new Vector3(xyEdge.x+(xyEdgeDifferents.x*i), xyEdge.y + (xyEdgeDifferents.y * j),0);
                ImageTilingSplit(tile, i, j);
            }
        }
    }

    void ImageTilingSplit(GameObject tempObj, int x,int y)
    {
        Rect uv1 = new Rect(x * 1 / (float)xyLength.x, 1 - ((y * 2) + 1) * 1 / (float)xyLength.y, 1 / (float)xyLength.x, 1 / (float)xyLength.y);
        Rect uv2 = new Rect(x * 1 / (float)xyLength.x, 1 - ((y * 2) + 2) * 1 / (float)xyLength.y, 1 / (float)xyLength.x, 1 / (float)xyLength.y);
        tempObj.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(uv1.x, uv1.y);
        tempObj.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(uv1.width, uv1.height);
        tempObj.GetComponent<MeshRenderer>().material.mainTexture = image;
    }

    public void ChangeImage(Texture t)
    {
        mat.mainTexture = t;
    }
}
