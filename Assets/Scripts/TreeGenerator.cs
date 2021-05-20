using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public int xNeg;
    public int xPos;
    public int yNeg;
    public int yPos;
    public GameObject[] trees;

    private void Start()
    {
        for (int i = 0; i < 1000; i++)
        {
            GameObject tree = Instantiate(trees[Random.Range(0, trees.Length)], new Vector3(Random.Range(xNeg, xPos), 0f, Random.Range(yNeg, yPos)), Quaternion.identity);
            tree.transform.Rotate(new Vector3(-90, Random.Range(0, 360), Random.Range(0, 360)));
            float scale = Random.Range(50f, 200f);
            tree.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
