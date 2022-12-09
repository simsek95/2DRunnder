using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBodyEffect : MonoBehaviour
{
   [SerializeField] GameObject particlePrefab;
    [SerializeField] int particleCount = 5;
    [SerializeField] float flyPower = 5;
    [SerializeField] float lifeTime = 4;
    private void Start()
    {
       Destroy(this.gameObject, lifeTime);   
    }

    public void Explode()
    {
        for(int i = 0; i < particleCount; i++)
        {
            Rigidbody2D rb = Instantiate(particlePrefab,transform).GetComponent<Rigidbody2D>();
            rb.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            rb.transform.parent = transform;
            rb.AddForce(GetRandomDirection()*flyPower,ForceMode2D.Impulse);
        }
    }

    Vector2 GetRandomDirection()
    {
        float x = Random.Range(-3,3);
        float y = Random.Range(-3, 3);

        return new Vector2(x,y).normalized;
    }
}
