using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hareket : MonoBehaviour
{
    public int puan = 0;
    bool bitis = false;
    bool oyunBasladi = false; // Oyunun ba�lay�p ba�lamad���n� kontrol etmek i�in

    // Start is called before the first frame update
    void Start()
    {
        puan = 0;
        GetComponent<Rigidbody>().velocity = Vector3.zero; // Ba�lang��ta durmas�n� sa�lar
    }

    // Update is called once per frame
    void Update()
    {
        // E�er oyun ba�lamam��sa Enter tu�unu bekle
        if (!oyunBasladi)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Enter tu�una bas�l�nca oyun ba�las�n
            {
                oyunBasladi = true;
            }
            return; // Oyunun ba�lamas� beklenirken di�er kodlar �al��mas�n
        }

        // E�er oyun ba�lam��sa hareket kodlar� �al��s�n
        if (!bitis)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * 3, ForceMode.Force);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        // Kullan�c� giri�lerini kontrol et
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
        oyunBasladi = false; // Oyunu yeniden ba�latt���n�zda tekrar Enter tu�u bekler
    }
}
