using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hareket : MonoBehaviour
{
    public int puan = 0;
    bool bitis = false;
    bool oyunBasladi = false; // Oyunun baþlayýp baþlamadýðýný kontrol etmek için

    // Start is called before the first frame update
    void Start()
    {
        puan = 0;
        GetComponent<Rigidbody>().velocity = Vector3.zero; // Baþlangýçta durmasýný saðlar
    }

    // Update is called once per frame
    void Update()
    {
        // Eðer oyun baþlamamýþsa Enter tuþunu bekle
        if (!oyunBasladi)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Enter tuþuna basýlýnca oyun baþlasýn
            {
                oyunBasladi = true;
            }
            return; // Oyunun baþlamasý beklenirken diðer kodlar çalýþmasýn
        }

        // Eðer oyun baþlamýþsa hareket kodlarý çalýþsýn
        if (!bitis)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * 3, ForceMode.Force);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        // Kullanýcý giriþlerini kontrol et
        if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody>().AddForce(0, 0, 8, ForceMode.Force);
        }

        if (Input.GetKey("a"))
        {
            GetComponent<Rigidbody>().AddForce(0, 0, -8, ForceMode.Force);
        }

        if (GetComponent<Rigidbody>().position.x <= -224)
        {
            bitis = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Invoke("restart", 3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Engel")
        {
            Invoke("restart", 3f);
            bitis = true;
        }

        if (collision.collider.tag == "altin")
        {
            puan++;
            Destroy(collision.gameObject);
        }
    }

    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        bitis = false;
        oyunBasladi = false; // Oyunu yeniden baþlattýðýnýzda tekrar Enter tuþu bekler
    }
}
