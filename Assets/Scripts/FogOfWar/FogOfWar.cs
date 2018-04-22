using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteMask))]
public class FogOfWar : MonoBehaviour
{
    public Camera camera;
    public int rWidth = 1024;
    public int rHeight = 768;
    public int iterCount = 5;
    public float rPixel = 0.01f;
    private int width=800;
    private int height=600;
    public Texture2D prefog;
    public Sprite fog;
    public static FogOfWar instance = null;
    public float timeToUpdate = 0.5f;
    private float currentTime = 0;
    public List<CircleCollider2D> colliders;
    private Collider2D[] result;
    private int _iter = 0;

    private void Open(Vector3 position, float radius)
    {
        //Debug.Log(position);
        //Debug.Log(radius);
        int rw = Mathf.RoundToInt(width * transform.localScale.x);
        int rh = Mathf.RoundToInt(height * transform.localScale.y);
        int r = Mathf.CeilToInt(radius / transform.localScale.x);
        int px = Mathf.RoundToInt((Mathf.FloorToInt(position.x  * 100) + rw / 2)/ transform.localScale.x);
        int py = Mathf.RoundToInt((Mathf.FloorToInt(position.y * 100) + rh / 2)/ transform.localScale.y);
        int lx = Mathf.Min(Mathf.Max(px - r, 0), width);
        int ly = Mathf.Min(Mathf.Max(py - r, 0), height); 
        int rx = Mathf.Min(Mathf.Max(px + r, 0), width);
        int ry = Mathf.Min(Mathf.Max(py + r, 0), height);
        int w = rx - lx;
        int h = ry - ly;
        Color visible = new Color(0, 0, 0, 0);
        //Debug.Log(new Vector3(lx, ly, h));
        Color[] data = prefog.GetPixels(
            lx,
            ly,
            w,
            h);
        for (int vx = lx; vx < rx; vx++)
        {
            for (int vy = ly; vy < ry; vy++)
            {
                if (new Vector2(vx - px, vy - py).magnitude < r)
                {
                    data[((vx - lx) * h + (vy - ly))] = visible;
                }
            }
        }
        prefog.SetPixels(
            lx,
            ly,
            w,
            h,
            data);
    }

    private void Open(Vision vision)
    {
        if (vision == null)
            return;
        Open(vision.transform.position, vision.radius);

    }
	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
            colliders = new List<CircleCollider2D>();
            GenerateEmpty();
            CompleteSprite();
        }
        else
        {
            Destroy(this);
        }
    }

    private void GenerateEmpty()
    {
        rWidth = Mathf.CeilToInt(camera.orthographicSize * 100.0f * 2.0f * camera.aspect);
        rHeight = Mathf.CeilToInt(camera.orthographicSize * 100.0f * 2.0f);

        width = Mathf.RoundToInt(rWidth * rPixel);
        height = Mathf.RoundToInt(rHeight * rPixel);
        transform.localScale = new Vector3(1.0f / rPixel, 1.0f/rPixel, 1); 
        prefog = new Texture2D(width, height);
        Color[] data = prefog.GetPixels();
        for (int index = 0; index < data.Length; index++)
        {
            data[index] = new Color(0, 0, 0, 1);
        }
        prefog.SetPixels(data);
        prefog.Apply();
    }

    private void CompleteSprite()
    {
        fog = Sprite.Create(prefog, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        GetComponent<SpriteMask>().sprite = fog;
        GetComponent<SpriteRenderer>().sprite = fog;
        if (GetComponent<PolygonCollider2D>())
            Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private void RebuildFog()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.NameToLayer("FogOfWar");
        Collider2D[] result = new Collider2D[10];
        int count = GetComponent<PolygonCollider2D>().OverlapCollider(filter, result);
        if (count>0)
        {
            for (int index=0; index < count; index++)
            {
               Open(result[index].GetComponent<Vision>());
            }
            prefog.Apply();
            //CompleteSprite();
        }
    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (currentTime > timeToUpdate)
        {
            currentTime -= timeToUpdate;
            _iter++;
            RebuildFog();
            if (_iter == iterCount)
                CompleteSprite();
        }
	}
}
