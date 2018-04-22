using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Mask : MonoBehaviour {
    public Sprite maskSprite = null;
    private bool _showed = false;
    private SpriteRenderer instance = null;
    public bool IsShowed()
    {
        return _showed;
    }
	// Use this for initialization
	void Start () {
		if (maskSprite != null)
        { 
            instance = new GameObject().AddComponent<SpriteRenderer>();
            instance.transform.parent = transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            instance.sprite = maskSprite;
            instance.gameObject.SetActive(false);
        }
	}
    private void OnMouseEnter()
    {
        _showed = true;
        instance.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        _showed = false;
        instance.gameObject.SetActive(false);
    }
}
