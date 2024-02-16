using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPH : MonoBehaviour
{
    [Header("Camara")]
    public List<Material> materials;
    public GameObject camaraRota;

    [Header("Fade")]
    public float fadeDuration = 5f; // Duración de la animación de desvanecimiento
    private float fadeTimer = 0f;
    private bool isBroken = false;

    private bate Bate;

    // Start is called before the first frame update
    void Start()
    {
        Bate = FindAnyObjectByType<bate>();
        foreach (Material mat in materials)
        {
            Color colorcamara = mat.color;
            colorcamara.a = 1;
            mat.color = colorcamara;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTimer < fadeDuration && isBroken)
        {
            fadeTimer += Time.deltaTime;
            float alpha = 1 - (fadeTimer / fadeDuration);
            foreach (Material mat in materials)
            {
                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
            }
                       
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bate") && Bate.isgrab)
        {
            isBroken = true;
            camaraRota = Instantiate(camaraRota, collision.transform.position, camaraRota.transform.rotation);
            Destroy(camaraRota, fadeDuration - 0.5f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(this.gameObject, fadeDuration - 0.5f);
        }

    }
}
