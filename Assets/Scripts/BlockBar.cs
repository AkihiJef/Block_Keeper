using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BlockBar : MonoBehaviour {
    public int spendCount = 10;
    public GameObject ball;
    public GameObject canvas;
    public GameObject unactivedBlock;
    public GameObject makeBlock;
    public GameObject background;
    public Vector3 startPosition;
    public Quaternion startRotation;
    public GameObject rotationSpriteRenderer;
    public GameObject text;
    public LevelSystem levelSystem;
    public bool hitSomething = false;
    // Use this for initialization
    bool InsideWorld()
    {
        float rightInsideX = (background.transform.lossyScale.x / 6.0f) - transform.lossyScale.x * 0.5f;
        float leftInsideX =  transform.lossyScale.x * 0.5f- background.transform.lossyScale.x * 0.5f;
        float insideY = background.transform.lossyScale.y * 0.5f - transform.lossyScale.y * 0.5f;
        return transform.position.y < insideY && transform.position.y > -insideY && transform.position.x < rightInsideX && transform.position.x> leftInsideX;

    }
    bool boxCast()
    {
        float weight = transform.lossyScale.x;
        float height = transform.lossyScale.y;
        float angle = transform.eulerAngles.z;
        int rayLayer = 1 << LayerMask.NameToLayer("Block") | 1 << LayerMask.NameToLayer("Ball") | 1 << LayerMask.NameToLayer("Obstacle")|1<<12;
        if (Physics2D.Raycast(new Vector2(transform.position.x-weight / 2, transform.position.y- height / 2), new Vector2(Mathf.Cos((angle+90)*Mathf.Deg2Rad), Mathf.Sin((angle + 90) * Mathf.Deg2Rad)), height, rayLayer))
        {
            return false;
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x - weight / 2, transform.position.y - height / 2), new Vector2(Mathf.Cos((angle) * Mathf.Deg2Rad), Mathf.Sin((angle) * Mathf.Deg2Rad)), weight, rayLayer))
        {
            return false;
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x + weight / 2, transform.position.y + height / 2), new Vector2(Mathf.Cos((angle - 90) * Mathf.Deg2Rad), Mathf.Sin((angle - 90) * Mathf.Deg2Rad)), height, rayLayer))
        {
            return false;
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x + weight / 2, transform.position.y+ height / 2), new Vector2(Mathf.Cos((angle + 180) * Mathf.Deg2Rad), Mathf.Sin((angle + 180) * Mathf.Deg2Rad)), weight, rayLayer))
        {
            return false;
        }
        return true;
    }
        IEnumerator OnMouseDown()
    {
        if (levelSystem.playerCount < spendCount) yield break;
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));
        canvas.SetActive(false);
        while (Input.GetMouseButton(0))
        {
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
            Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            transform.position = CurPosition;
            yield return new WaitForFixedUpdate();
        }
        if (InsideWorld() && boxCast())
        {
            rotationSpriteRenderer.SetActive(true);
            float mouseStartX = Input.mousePosition.x;
            while (!Input.GetMouseButton(0))
            {
                transform.Rotate(new Vector3(0, 0, Input.mousePosition.x - mouseStartX));
                mouseStartX = Input.mousePosition.x;
                yield return new WaitForFixedUpdate();
            }
            if (InsideWorld() && boxCast())
            {
                GameObject newBlock = Instantiate(unactivedBlock, transform.position, transform.rotation) as GameObject;
                newBlock.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                newBlock.GetComponent<UnactivedBlock>().blockPrefab = makeBlock;
                newBlock.GetComponent<UnactivedBlock>().ball = ball;
                newBlock.GetComponent<UnactivedBlock>().init = true;
                levelSystem.playerCount -= spendCount;
            }
        }
        transform.position = startPosition;
        transform.rotation = startRotation;
        canvas.SetActive(true);
        rotationSpriteRenderer.SetActive(false);

    }
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rotationSpriteRenderer.SetActive(false);
        text.GetComponent<Text>().text = spendCount.ToString();
    }
    void Update()
    {
        GetComponent<SpriteRenderer>().enabled = levelSystem.playerCount >= spendCount;
    }
}
