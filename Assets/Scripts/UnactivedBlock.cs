using UnityEngine;
using System.Collections;

public class UnactivedBlock : MonoBehaviour {
    public GameObject ball;
    public bool init=false;
    public GameObject blockPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (init && ball.transform.position.x > (15.19692f / 6.0f))
        {
            GameObject block = Instantiate(blockPrefab, transform.position,transform.rotation) as GameObject;
            block.GetComponent<SpriteRenderer>().sortingLayerName = "Object";
            block.GetComponent<SpriteRenderer>().sortingOrder = -1;
            Destroy(gameObject);
        }
	}
}
