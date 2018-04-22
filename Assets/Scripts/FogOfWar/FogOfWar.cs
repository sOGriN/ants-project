using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteMask))]
public class FogOfWar : MonoBehaviour {
    public int width=800;
    public int height=600;
    public Texture2D prefog;
    public Sprite fog;
    public static FogOfWar instance = null;

    public void Open(Vector3 position, float radius)
    {
        Debug.Log(position);
        Debug.Log(radius);
        int r = Mathf.CeilToInt(radius);
        int px = Mathf.FloorToInt(position.x / transform.localScale.x * 100);
        int py = Mathf.FloorToInt(position.y * 100);
        int x = px - r;
        int y = py - r;
        int lx = Mathf.Min(x + 2 * r, width);
        Color visible = new Color(0, 0, 0, 1);
        Color[] data = prefog.GetPixels(
            x + width / 2,
            y + height / 2,
            2 * r,
            2 * r);
        for (int vx = x; vx < x + 2 * r; vx++)
        {
            for (int vy = y; vy < y + 2 * r; vy++)
            {
                if (new Vector2(vx - px, vy - py).magnitude < radius)
                {
                    data[((vx - x) * 2 * r + (vy - y))] = visible;
                }
            }
        }
        prefog.SetPixels(
            x + width / 2,
            y + height / 2,
            2 * r,
            2 * r,
            data);
        prefog.Apply();
        fog = Sprite.Create(prefog, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        GetComponent<SpriteMask>().sprite = fog;
    }
	// Use this for initialization
	void Start () {
        //width = Screen.width;
        //height = Screen.height;
        prefog = new Texture2D(width, height);
        Color[] data = prefog.GetPixels();
        for (int index = 0; index < data.Length; index++)
        {
            data[index] = new Color(0, 0, 0, 0);
        }
        prefog.SetPixels(data);
        prefog.Apply();
        fog = Sprite.Create(prefog, new Rect(0,0,width, height), new Vector2(0.5f, 0.5f));
        GetComponent<SpriteMask>().sprite = fog;
        //GetComponentInChildren<Transform>().localScale = new Vector3(width, height, 1);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
