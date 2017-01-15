using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float moveSpeed = 3.0f;
    Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            coll.gameObject.GetComponent<Block>().Hit();
            Destroy(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + new Vector2(-1, 0) * moveSpeed * Time.fixedDeltaTime);
    }
}
