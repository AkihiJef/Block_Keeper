using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public float moveSpeed = 3;
    public float randReflectAngle = 30;
    private Rigidbody2D rigidBody;
    public Vector2 velocity;
    public ulong ballStyle = 0;
    public float strongHitRadius;
    public GameObject platform;
    public GameObject bullet;
    public float bulletEachTime = 1.3f;
    public float bulletTimeCount = 0;
    GameObject Se;
    int[] dropSomething = { 400, 30, 100, 50, 70, 40 };
    int[] dropSomethingLucky = { 200, 30, 100, 50, 70, 40 };
    float[] dropTime = { 0, 8, 8, 8, 8, 8, 8 };
    float[] dropTimeCount = { 0, 0, 0, 0, 0, 0, 0 };
    int[] dropSomethingTemp;
    int[] dropSomethingTempLucky;
    GameObject[] visibalSprites;
    GameObject e, s1, s2, g, h, l;
    // Use this for initialization
    void Start () {
        velocity = new Vector2(-1, 1).normalized;
        visibalSprites = new GameObject[7];
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(moveSpeed * velocity*50);
        Se = GameObject.Find("Se");
        dropSomethingTemp = new int[dropSomething.Length];
        int sum = 0;
        for (int i = 0; i < dropSomethingTemp.Length; i++)
        {
            sum += dropSomething[i];
            dropSomethingTemp[i] = sum;
        }
        dropSomethingTempLucky = new int[dropSomething.Length];
        int sumLucky = 0;
        for (int i = 0; i < dropSomethingTempLucky.Length; i++)
        {
            sumLucky += dropSomethingLucky[i];
            dropSomethingTempLucky[i] = sumLucky;
        }
        e = GameObject.Find("E");
        visibalSprites[0] = null;
        s1 = GameObject.Find("S+");
        visibalSprites[1] = e;
        s2 = GameObject.Find("S-");
        visibalSprites[2] = s1;
        g = GameObject.Find("G");
        visibalSprites[3] = g;
        h = GameObject.Find("H");
        visibalSprites[4] = h;
        l = GameObject.Find("L");
        visibalSprites[5] = l;
        visibalSprites[6] = s2;
        for(int i = 0; i < 7; i++)
        {
            if (visibalSprites[i] != null)
                visibalSprites[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
	    for(int i = 1; i < dropTimeCount.Length; i++)
        {
            if ((ballStyle & (ulong)1 << i) != 0)
            {
                dropTimeCount[i] -= Time.deltaTime;
                if (dropTimeCount[i] < 0)
                {
                    ballStyle &= ~(((ulong)1) << i);
                    if(visibalSprites[i]!=null)
                    visibalSprites[i].SetActive(false);

                }
            }
        }
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        bool hitBlock = false;
        if (coll.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            hitBlock = true;
            if ((ballStyle & (ulong)1 <<1)!=0)
            {
                if (coll.gameObject.GetComponent<Block>().special)
                {
                    if ((ballStyle & (ulong)1 << 4) != 0) coll.gameObject.GetComponent<Block>().Hit();
                    coll.gameObject.GetComponent<Block>().Hit();
                }
                Collider2D[] blocks = Physics2D.OverlapCircleAll(rigidBody.position, strongHitRadius, 1 << LayerMask.NameToLayer("Block"));
                foreach(Collider2D block in blocks)
                {
                    if (!block.gameObject.GetComponent<Block>().special)
                    {
                        if((ballStyle & (ulong)1 << 4) != 0) block.GetComponent<Block>().Hit();
                        block.GetComponent<Block>().Hit();
                    }
                }
            }else
            {
                if ((ballStyle & (ulong)1 << 4) != 0) coll.gameObject.GetComponent<Block>().Hit();
                coll.gameObject.GetComponent<Block>().Hit();
            }

        }
        if (coll.gameObject.name == "Platform")
        {
            Se.GetComponent<AudioSource>().Play();
            float resultAngle = Random.value * (180 - 2 * randReflectAngle) + 90 + randReflectAngle;
            Vector2 newVelocity = new Vector2(Mathf.Cos(resultAngle * Mathf.Deg2Rad), Mathf.Sin(resultAngle * Mathf.Deg2Rad));
            velocity = newVelocity;
            return;
        }
        
        ContactPoint2D minDistanceHitPoint = new ContactPoint2D();
        float hitDistance = 555;
        foreach (ContactPoint2D contact in coll.contacts)
        {
            if((contact.point - rigidBody.position).magnitude < hitDistance)
            {
                hitDistance = (contact.point - rigidBody.position).magnitude;
                minDistanceHitPoint = contact;
            }
        }
        if(!hitBlock)
        Se.GetComponent<AudioSource>().Play();
        if(Vector2.Reflect(velocity, minDistanceHitPoint.normal)!=Vector2.zero)
        velocity = Vector2.Reflect(velocity, minDistanceHitPoint.normal).normalized;
    }
    void FixedUpdate()
    {
        int specialSpeed = 0;
        if ((ballStyle & (ulong)1 <<2)!=0)
        {
            specialSpeed++;
        }
        if ((ballStyle & (ulong)1 << 6) != 0)
        {
            specialSpeed--;

        }
        rigidBody.MovePosition(rigidBody.position + (1.0f + specialSpeed*0.3f)*velocity * moveSpeed * Time.fixedDeltaTime);
        if ((ballStyle & (ulong)1 << 3) != 0)
        {
            if(bulletTimeCount <= 0)
            {
                GameObject.Find("Se3").GetComponent<AudioSource>().Play();
                Instantiate(bullet, platform.transform.position + new Vector3(-0.5f, 0.0f,0.0f), platform.transform.rotation);
                bulletTimeCount += bulletEachTime;
            }else
            {
                bulletTimeCount -= Time.fixedDeltaTime;
            }
        }
    }
    public void changeStatus(int status)
    {
        if ((ballStyle & (ulong)1 << status) == 0)
        {
            ballStyle |= (ulong)1 << status;
            dropTimeCount[status] = dropTime[status];
            if (visibalSprites[status] != null)
            {
                visibalSprites[status].SetActive(true);
            }
        }
        return;
    }
    public void randStatus()
    {
        if ((ballStyle | (ulong)1 << 5) != 0)
        {
            int length = dropSomethingTempLucky.Length;
            float result = Random.value * dropSomethingTempLucky[length - 1];
            int dropResult = 0;
            for (int i = 0; i < length; i++)
            {
                if (result < dropSomethingTempLucky[i])
                {
                    dropResult = i;
                    break;
                }
            }
            changeStatus(dropResult);
        }
        else
        {
            int length = dropSomethingTemp.Length;
            float result = Random.value * dropSomethingTemp[length - 1];
            int dropResult = 0;
            for (int i = 0; i < length; i++)
            {
                if (result < dropSomethingTemp[i])
                {
                    dropResult = i;
                    break;
                }
            }
            changeStatus(dropResult);
        }
        
        return;
    }
}
