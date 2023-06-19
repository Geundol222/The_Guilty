using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] Transform player;
    [SerializeField] float bulletSpeed;
    [SerializeField] int damage;

    public void Fire()
    {
        muzzleEffect.Play();

        RaycastHit hit;

        Vector3 dir = (player.position - muzzleEffect.transform.position).normalized;

        if (Physics.Raycast(muzzleEffect.transform.position, dir, out hit, Vector3.Distance(muzzleEffect.transform.position, player.position), playerMask))
        {
            IHittable hittable = hit.transform.gameObject.GetComponent<IHittable>();
            ParticleSystem hitEffect = GameManager.Resource.Instantiate<ParticleSystem>("Particles/HitEffect", hit.point, Quaternion.LookRotation(hit.normal), true);
            hitEffect.transform.parent = hit.transform;
            GameManager.Resource.Destroy(hitEffect.gameObject, 1.5f);

            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, hit.point));

            hittable?.TakeDamage(damage);
        }
    }

    IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    {
        TrailRenderer trail = GameManager.Resource.Instantiate<TrailRenderer>("Particles/BulletTrail", startPoint, Quaternion.identity, true);
        trail.Clear();

        float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

        float rate = 0;
        while (rate < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
            rate += Time.deltaTime / totalTime;

            yield return null;
        }

        GameManager.Resource.Destroy(trail.gameObject);
    }
}
