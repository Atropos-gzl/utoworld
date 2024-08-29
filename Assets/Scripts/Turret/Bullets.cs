using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{
    public GameObject target;
    public Transform pos;
    public Transform pos_temp;
    public TurretData datatype;
    public GameObject Explosion_eff;
    public float[] damage = new float[3];
    public bool isMultiple = false;
    public bool isCrit = false;
    private bool flag = true;
    public bool isHit = false;
    public delegate void OnKilled(Turret turret, GameBase gameBase);
    public delegate void OnHit(Bullets bt, GameObject enemy);
    public event OnKilled OnKilledEvent;
    public event OnHit OnHitEvent;
    //public List<OnKilled> onKilleds = new List<OnKilled>();
    //public List<OnHit> onHits = new List<OnHit>();

    public void OnKilledCompleted(Turret turret, GameBase gameBase)
    {
        OnKilledEvent?.Invoke(turret, gameBase);
    }

    public void OnHitCompleted(Bullets bt, GameObject enemy)
    {
        OnHitEvent?.Invoke(bt, enemy);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (datatype.type == TurretType.CrossBow)
        {
            Destroy(gameObject, 1.5f);
        }
        Destroy(gameObject, 4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            pos = target.transform;
        if (target == null)
        {
            Destroy(gameObject);
        }
        try
        {
            if (flag)
            {
                if (datatype.type == TurretType.Culverin)
                    gameObject.GetComponent<Rigidbody>().AddForce((pos.position - transform.position) * 3.4f);
                else if (datatype.type == TurretType.CrossBow)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce((pos.position - transform.position) * 10);
                }
                else if (datatype.type == TurretType.FattyPlasma)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce((pos.position - transform.position) * 6);
                }
            }

        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (datatype.type == TurretType.CrossBow && !isHit)
        {
            Debug.Log(datatype.type + "'s bullet hit enemy");
            if (other.tag == "Enemy")
            {
                isHit = true;
                int status = other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                if (status == 0)
                {
                    OnKilledCompleted(transform.GetComponentInParent<Turret>(), GameBase.getInstance());
                }
                else
                {
                    OnHitCompleted(this, other.gameObject);
                }
                Destroy(gameObject);
            }
            if (other.tag == "map" || other.tag == "road")
            {
                flag = false;
                Destroy(GetComponent<Rigidbody>());
                gameObject.transform.Translate(transform.position * 0);
                Destroy(gameObject, 2.0f);
            }

        }
        else if (datatype.type == TurretType.Culverin && !isHit)
        {
            if (other.tag == "Enemy" || other.tag == "road" || other.tag == "map")
            {
                isHit = true;
                Debug.Log(datatype.type + "'s bullet hit enemy");
                GameObject go = Instantiate(Explosion_eff, transform.position, Quaternion.identity);
                Collider[] enemies = Physics.OverlapSphere(transform.position, 4.0f, (1 << 9));
                foreach (Collider enemy in enemies)
                {
                    try
                    {
                        if (enemy.tag == "Enemy")
                        {
                            Vector3 dis = enemy.transform.position - transform.position;
                            //damage[0] = (damage[0] * ((4.0f - dis.magnitude) / 4.0f));
                            int status = enemy.GetComponentInParent<Enemy>().TakeDamage(damage);
                            if (status == 0)
                            {
                                OnKilledCompleted(transform.GetComponentInParent<Turret>(), GameBase.getInstance());
                            }
                            else
                            {
                                OnHitEvent(this, other.gameObject);
                            }
                            Destroy(go, 1.0f);
                        }
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
                Debug.Log("销毁炮弹");
                Destroy(gameObject);
            }
            
            
        }
        else if (datatype.type == TurretType.FattyPlasma && !isHit)
        {
            
            if (other.tag == "Enemy")
            {
                isHit = true;
                Debug.Log(datatype.type + "'s bullet hit enemy");
                int status = other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                if (status == 0)
                {
                    OnKilledCompleted(transform.GetComponentInParent<Turret>(), GameBase.getInstance());
                }
                else
                {
                    OnHitCompleted(this, other.gameObject);
                }
                Destroy(gameObject);
            }


        }
    }

    
}
