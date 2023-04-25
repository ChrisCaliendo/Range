using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{

    public FishingMinigame minigame;
    [SerializeField]
    public Transform[] routes;
    private Vector2[] r;

    private int routeToGo;
    private SpriteRenderer fishSprite;
    private float tParam;

    private Vector2 objectPosition;

    [SerializeField] private float speedModifier;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        fishSprite = gameObject.GetComponent<SpriteRenderer>();
        r = new Vector2[15];
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.2f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        //for(int i = 0; i<routes.Length; i--)
        //{
        //    r[i] = routes[routeNum].GetChild().position;
        //}

         r[0] = routes[routeNum].GetChild(0).position;
         r[1] = routes[routeNum].GetChild(1).position;
         r[2] = routes[routeNum].GetChild(2).position;
         r[3] = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * r[0] + 3 * Mathf.Pow(1 - tParam, 2) * tParam * r[1] + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * r[2] + Mathf.Pow(tParam, 3) * r[3];

            if (objectPosition.x > transform.position.x) fishSprite.flipX = true;
            else fishSprite.flipX = false;
            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;

    }  

    public void AssignRoutes(GameObject path)
    {
        
    }

    public void AssignMinigame(FishingMinigame parent)
    {
        minigame = parent;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        minigame.catchingInProgress(); 
    }
    private void OnTriggerExit2D(Collider2D other) {
        minigame.catchingDelayed();
    }
}
