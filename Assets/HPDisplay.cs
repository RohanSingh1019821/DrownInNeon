using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDisplay : MonoBehaviour
{

    public GameObject playerObj;
    private animCharController player;

    public GameObject pegExplosion;



    public GameObject pegPrefab;
    public int pegAmount = 10;
    private List<GameObject> pegs;

    private int health = 3;

    void OnEnable()
    {
        player = playerObj.GetComponent<animCharController>();
        health = player.hitPoints;


        pegs = new List<GameObject>(pegAmount);
        for (int i = 0; i < pegAmount; i++)
        {
            GameObject prefabInstance = Instantiate(pegPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            pegs.Add(prefabInstance);
        }

        PositionPegs();
    }

    void PositionPegs()
    {
        int rotInterval = 0;
        int pegsTospawn = health;
        foreach (GameObject peg in pegs)
        {
            peg.SetActive(false);
            if (!peg.activeInHierarchy && pegsTospawn > 0)
            {
                peg.transform.eulerAngles = new Vector3(0, 0, rotInterval);
                peg.SetActive(true);
                rotInterval += 360 / health;
                pegsTospawn -= 1;
                //return peg;
            }
        }
    }

    void Update()
    {
        this.transform.position = player.transform.position;
        this.transform.Rotate(0,0, 60 * Time.deltaTime ,Space.Self);
    }

    public void Hit(Vector3 damagePos)
    {
        pegExplosion.transform.position = (damagePos - transform.position).normalized + transform.position;

        var dir = damagePos - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
        pegExplosion.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        pegExplosion.SetActive(false);
        pegExplosion.SetActive(true);
        health -= 1;
        PositionPegs();
    }


    public void Regen()
    {
        GetComponent<AudioSource>().Play();
        health += 1;
        PositionPegs();
    }
}
