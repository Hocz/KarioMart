using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private PlayerController playerController;

    [Header("Knockback Settings:")]

    [SerializeField]
    public float knockbackForce = 400f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        playerController.currentMoveDirection = 0;
        playerController.canMovePlayer = false;
        
        rigidbody2d.velocity = Vector3.zero;
        rigidbody2d.velocity = (currentPosition - collision.GetContact(0).point).normalized * knockbackForce * Time.deltaTime;
        
        StartCoroutine(KnockbackCooldown());
    }
    
    public IEnumerator KnockbackCooldown()
    {
        yield return new WaitForSeconds(.2f);

        rigidbody2d.velocity = Vector3.zero;
        playerController.canMovePlayer = true;
    }
}
