using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public Animator anim;
    public GameObject clickWindow;

   private float zAxis;

    private void Awake()
    {
        clickWindow.transform.localScale = new Vector3(.2f, .2f, 0f);
        zAxis = 19;
    }

    private void Update()
    {
        clickWindow.transform.position = new Vector3(-2f, -3f, -1f);
    }

    public void Hatch()
    {
        anim.SetTrigger("Crack");
    }
    public void Teen()
    {
        anim.SetFloat("Age", 1f);
        transform.position = new Vector3(-2f, -3f, 0f);
    }
    public void Adult()
    {
        anim.SetFloat("Age", 2f);
        transform.position = new Vector3(-2f, -2f, 0f);
        transform.localScale = new Vector3(7.5f, 7.5f, 0f);
    }
}
