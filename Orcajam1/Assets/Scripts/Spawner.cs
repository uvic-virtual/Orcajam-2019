using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Camera playerCam;
    private Transform playerPos;

    private float timer;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerCam = GetComponentInChildren<Camera>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime + Random.Range(0, 1);
        if(timer >= 1)
        {
            Vector3 spawnPos = playerPos.position;
            spawnPos.x += 2 +Random.Range(-15,15);
            spawnPos.z += Random.Range(0, 10);
            spawnPos.y -= 20f;
            GameObject clone = Instantiate(obj, spawnPos, Quaternion.identity);

            StartCoroutine(MoveUp(clone));
            timer = 0;
        }

    }

    private IEnumerator MoveUp(GameObject target)
    {
        float transSpeed = 13f;
        for (int i = 0; i < 3000 / transSpeed; i++)
        {
            target.transform.Translate(Vector3.up * Time.deltaTime * transSpeed, playerCam.transform);
            yield return new WaitForEndOfFrame();
        }
        Destroy(target);
        
    }




}
