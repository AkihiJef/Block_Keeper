using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
    public float minSpeed = 2;
    public float lowSpeedChangeDistance = 1;
    public float maxSpeed = 8;
    public float randMoveEachTime = 15f;
    private float randMoveTimeCount;
    public float moveSpeed=1;
    public GameObject Ball;
    private Rigidbody2D rigidBody;
    private float randMoveSpeed = 1;
    private int moveStyle;
    private float perlinSeed;
    private float perlinTime= 0;
    private float moveDirection=1;
    public GameObject Background;
	// Use this for initialization
	void Start () {
        randMoveTimeCount = randMoveEachTime;
        rigidBody = GetComponent<Rigidbody2D>();
        perlinSeed = Random.value * 8;
    }
	void RandMove(float deltaTime)
    {
        moveSpeed = randMoveSpeed;
        perlinTime += deltaTime;
        randMoveSpeed = (Mathf.PerlinNoise(perlinTime, perlinSeed) + 1)/2;
    }
    void MoveToBall()
    {
        float thatY = Ball.GetComponent<Rigidbody2D>().GetPoint(Vector2.zero).y;
        float thisY = rigidBody.GetPoint(Vector2.zero).y;
        if(Mathf.Abs(thisY - thatY)< lowSpeedChangeDistance)
        {
            moveSpeed = Mathf.Abs(thisY - thatY) / lowSpeedChangeDistance;
        }
        else
        {
            moveSpeed = 1;
        }
        moveDirection = thisY - thatY>0 ? 1 : -1;
    }
    void Move(float deltaTime)
    {
        rigidBody.MovePosition(new Vector2(0, moveDirection) * moveSpeed * deltaTime * minSpeed + rigidBody.position);
    }
	// Update is called once per frame
	void Update () {
        
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.name == "Background")
        {
            
            moveDirection = -moveDirection;

        }
    }
    void FixedUpdate()
    {
        moveStyle = (Ball.transform.position.x < (Background.transform.lossyScale.x / 6.0f)) ? 1 : 0;
        if (moveStyle==1)
        {

            RandMove(Time.fixedDeltaTime);
        }
        else
        {
            MoveToBall();
        }
        Move(Time.fixedDeltaTime);
    }
}
