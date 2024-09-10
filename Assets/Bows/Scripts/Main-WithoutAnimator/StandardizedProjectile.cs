using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(AudioSource))]
public class StandardizedProjectile : MonoBehaviour
{
    #region Private Values
    [HideInInspector] public Rigidbody rigid;
    [HideInInspector] public StandardizedBow bowScript;
    
    private AudioSource audioSource;
    private float currentTime;    
    private bool collisionHappened;
    #endregion

    #region Public Values
    internal Collider[] authorColliders;
    [Range(0, 1f),Tooltip("This value is multiplied with the emission amount of the particle. Higher the value, more particle emission.")]
    public float effectOfVelocityOnParticleEmission=0.5f; 
    [Header("     Projectile On Hit Particles")]    
    public GameObject projectileHitParticleFlesh;
    public GameObject projectileHitParticleWood;
    public GameObject projectileHitParticleStone;
    public GameObject projectileHitParticleMetal;
    [Header("     Projectile On Hit Sounds")]
    public AudioClip hitSoundFlesh;
    public AudioClip hitSoundWood;
    public AudioClip hitSoundStone;
    public AudioClip hitSoundMetal;
    [Header("     Projectile On Hit Detection Tags")]
    [Tooltip("Tag names of the objects that needs to be registered as flesh.")]
    public string fleshDetectionTagToHash = "Flesh";
    [Tooltip("Tag names of the objects that needs to be registered as wood.")]
    public string woodDetectionTagToHash = "Wood";
    [Tooltip("Tag names of the objects that needs to be registered as stone.")]
    public string stoneDetectionTagToHash = "Stone";
    [Tooltip("Tag names of the objects that needs to be registered as metal.")]
    public string metalDetectionTagToHash = "Metal";
    public MeshRenderer projectileMeshRenderer;
    #endregion
    
    #region On Hit Privates Variables
    // Hash is more performant
    private static int fleshHash, woodHash, stoneHash, metalHash, contactHash, projectileHash;
    // On hit particles won't be instantiated in runtime - cache Particle System
    private ParticleSystem fleshHitPS, woodHitPS, stoneHitPS, metalHitPS; 
    // Cache variables for projectile game object
    private GameObject fleshParticleInstance, woodParticleInstance, stoneParticleInstance, metalParticleInstance, currentParticleInstance;
    // Cache variables for projectile transform
    private Transform fleshParticleTransform, woodParticleTransform, stoneParticleTransform, metalParticleTransform;
    // Burst control variable for emission control
    private ParticleSystem.Burst burstControl;
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {        
        // Reset timer
        currentTime = 0;
        // Cache rigidbody
        if (rigid==null)
        {
            // Just added as a precaution
            Debug.Log("Rigidbody is found through an expensive method. This is not good. There might be an error in pooling. Consider checking what you changed in script.");
            rigid = GetComponent<Rigidbody>();
        }        
        rigid.useGravity = false;  // Disable gravity so projectile doesn't fall while being drawn
        if (bowScript==null)
        {
            // Just added as a precaution
            Debug.Log("StandardizeBow script is found through an expensive method. This is not good. Consider checking what you changed in script.");
            bowScript = GetComponentInParent<StandardizedBow>();
        }
        
        // Hashing is here to prevent using strings for comparison - The projectile pooling is called from the bow.
        fleshHash = fleshDetectionTagToHash.GetHashCode();
        woodHash = woodDetectionTagToHash.GetHashCode();
        stoneHash = stoneDetectionTagToHash.GetHashCode();
        metalHash = metalDetectionTagToHash.GetHashCode();
        projectileHash = transform.tag.GetHashCode();
        // 
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (!collisionHappened)
        {
            // Start from 90 vert and rotate depending on the launch angle and velocity
            transform.rotation = Quaternion.LookRotation(rigid.velocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var authorCollider in authorColliders)
        {
            if(other == authorCollider) return;
        }
        if(other.TryGetComponent(out IgnoreArrows _)) return;
        contactHash = other.tag.GetHashCode();
        if (contactHash == fleshHash)
        {
            // Contact with flesh            
            if (projectileHitParticleFlesh!=null)
            {
                fleshParticleTransform.position = transform.position;
                fleshParticleInstance.SetActive(true);
                currentParticleInstance = fleshParticleInstance;
                // Velocity - Emission Calculation
                burstControl.count = Mathf.FloorToInt(rigid.velocity.magnitude * effectOfVelocityOnParticleEmission);
                fleshHitPS.emission.SetBurst(0, burstControl);
                fleshHitPS.Play();
            }
            if (hitSoundFlesh!=null)
            {
                audioSource.PlayOneShot(hitSoundFlesh);
            }            
            ArrowStop(other);
        }
        else if (contactHash == woodHash)
        {
            // Contact with wood            
            if (projectileHitParticleWood!=null)
            {
                woodParticleTransform.position = transform.position;
                woodParticleInstance.SetActive(true);
                currentParticleInstance = woodParticleInstance;
                // Velocity - Emission Calculation
                burstControl.count = Mathf.FloorToInt(rigid.velocity.magnitude * effectOfVelocityOnParticleEmission);
                woodHitPS.emission.SetBurst(0, burstControl);
                woodHitPS.Play();
            }
            if (hitSoundWood!=null)
            {
                audioSource.PlayOneShot(hitSoundWood);
            }            
            ArrowStop(other);
        }
        else if (contactHash == stoneHash)
        {
            // Contact with stone
            if (projectileHitParticleStone!=null)
            {
                stoneParticleTransform.position = transform.position;
                stoneParticleInstance.SetActive(true);
                currentParticleInstance = stoneParticleInstance;
                // Velocity - Emission Calculation
                burstControl.count = Mathf.FloorToInt(rigid.velocity.magnitude * effectOfVelocityOnParticleEmission);
                stoneHitPS.emission.SetBurst(0, burstControl);
                stoneHitPS.Play();
            }            
            if (hitSoundStone!=null)
            {
                audioSource.PlayOneShot(hitSoundStone);
            }            
            ArrowStop(other);
        }
        else if (contactHash == metalHash)
        {
            // Contact with metal
            if (projectileHitParticleMetal!=null)
            {
                metalParticleTransform.position = transform.position;
                metalParticleInstance.SetActive(true);
                currentParticleInstance = metalParticleInstance;
                // Velocity - Emission Calculation
                burstControl.count = Mathf.FloorToInt(rigid.velocity.magnitude * effectOfVelocityOnParticleEmission);
                metalHitPS.emission.SetBurst(0, burstControl);
                metalHitPS.Play();
            }            
            if (hitSoundMetal!=null)
            {
                audioSource.PlayOneShot(hitSoundMetal);
            }         
            ArrowStop(other);
        }
        else
        {
            // If it is not another object that has the same tag with projectile
            if (contactHash != projectileHash)
            {
                currentParticleInstance = null;
                // Unknown contact --- Tags of the object not assigned -- No SFX & particle
                ArrowStop(other);
            }            
        }
    }

    private void ArrowStop(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody otherRigid))
        {
            CoroutineBackgroundControl.Instance.StartCoroutine(AddForceAfterStop(otherRigid, rigid.velocity));
        }
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        rigid.velocity = Vector3.zero;
        rigid.useGravity = false;
        transform.parent = other.transform;
        collisionHappened = true;
    }

    public void ArrowReset()
    {
        rigid.velocity = Vector3.zero;
        rigid.useGravity = false;
        collisionHappened = false;
        rigid.constraints = RigidbodyConstraints.None;
        currentTime = 0;
        gameObject.SetActive(false);
        // Deactivate the last used particle
        if (currentParticleInstance!=null)
        {
            currentParticleInstance.SetActive(false);
        }
    }

    private IEnumerator AddForceAfterStop(Rigidbody target, Vector3 velocity)
    {
        yield return new WaitForSeconds(0.1f);
        target.AddForce(velocity, ForceMode.Impulse);
    }

    // Particles are pooled too - The starting projectile pool size in Standardize Bows is important. Make it high enough that, 
    // the script never have to Instantiate anything in runtime( Depends on your fire rate - [10,30] ).
    public void PoolTheParticles()
    {        
        if (projectileHitParticleFlesh != null)
        {            
            fleshParticleInstance = Instantiate<GameObject>(projectileHitParticleFlesh, transform);
            fleshHitPS = fleshParticleInstance.GetComponent<ParticleSystem>();
            fleshParticleTransform = fleshParticleInstance.transform;
            fleshParticleInstance.SetActive(false);
        }
        if (projectileHitParticleStone != null)
        {
            woodParticleInstance = Instantiate<GameObject>(projectileHitParticleWood, transform);
            woodHitPS = woodParticleInstance.GetComponent<ParticleSystem>();
            woodParticleTransform = woodParticleInstance.transform;
            woodParticleInstance.SetActive(false);
        }
        if (projectileHitParticleStone != null)
        {
            stoneParticleInstance = Instantiate<GameObject>(projectileHitParticleStone, transform);
            stoneHitPS = stoneParticleInstance.GetComponent<ParticleSystem>();
            stoneParticleTransform = stoneParticleInstance.transform;
            stoneParticleInstance.SetActive(false);
        }
        if (projectileHitParticleMetal != null)
        {
            metalParticleInstance = Instantiate<GameObject>(projectileHitParticleMetal, transform);
            metalHitPS = metalParticleInstance.GetComponent<ParticleSystem>();
            metalParticleTransform = metalParticleInstance.transform;
            metalParticleInstance.SetActive(false);
        }
    }
}
