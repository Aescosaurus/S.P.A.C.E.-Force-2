using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EmancipatorShoot
	:
	MonoBehaviour
{
	void Start()
	{
		deathUI = Resources.Load<GameObject>(
			"Prefabs/DeathUI" );
        canvas = GameObject.Find("Canvas");
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );

		cam = Camera.main;
		Assert.IsNotNull( cam );

		bulletPrefab = Resources.Load<GameObject>(
			"Prefabs/Emancipator Bullet" );
		Assert.IsNotNull( bulletPrefab );

		gun1 = transform.Find( "Gun1" );
		Assert.IsNotNull( gun1 );
		gun2 = transform.Find( "Gun2" );
		Assert.IsNotNull( gun2 );
		shotgun = transform.Find( "Shotgun" );
		Assert.IsNotNull( shotgun );

		audSrc = GetComponent<AudioSource>();
		Assert.IsNotNull( audSrc );
		for( int i = 0; i < 8; ++i )
		{
			shootSounds.Add( Resources.Load<AudioClip>(
				"Sounds/Player Shoot 0" + ( i + 1 ).ToString() ) );
			Assert.IsNotNull( shootSounds[i] );
		}

		// shotgunSpread *= Mathf.Deg2Rad;
	}

	void Update()
	{
		// if( Input.GetKeyDown( KeyCode.Alpha0 ) )
		// {
		// 	LevelHandler.SaveKitty();
		// }

		if( refire.Update( Time.deltaTime ) &&
			Input.GetAxis( "Attack" ) > 0.0f )
		{
			refire.Reset();

			Vector2 diff = RotateGetDiff();

			body.AddForce( -diff * pushForce,
				ForceMode2D.Impulse );

			if( ++curGun > 1 ) curGun = 0;

			FireBullet( curGun == 0
				? gun1.position
				: gun2.position,
				diff );
		}

		// if( burstRefire.Update( Time.deltaTime ) &&
		// 	Input.GetAxis( "Burst" ) > 0.0f )
		// {
		// 	burstRefire.Reset();
		// 
		// 	Vector2 diff = RotateGetDiff();
		// 
		// 	body.AddForce( -diff * shotgunPushForce,
		// 		ForceMode2D.Impulse );
		// 
		// 	var angle = Mathf.Atan2( diff.y,diff.x );
		// 
		// 	FireBullet( shotgun.position,diff );
		// 	FireBullet( shotgun.position,new Vector2(
		// 		Mathf.Cos( angle - shotgunSpread ),
		// 		Mathf.Sin( angle - shotgunSpread ) ) );
		// 	FireBullet( shotgun.position,new Vector2(
		// 		Mathf.Cos( angle + shotgunSpread ),
		// 		Mathf.Sin( angle + shotgunSpread ) ) );
		// }
	}

	Vector2 RotateGetDiff()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(
			Input.mousePosition );
		Vector2 diff = mousePos - ( Vector2 )transform.position;
		diff.Normalize();

		transform.rotation = Quaternion.Euler( 0.0f,0.0f,
			Mathf.Atan2( diff.y,diff.x ) * Mathf.Rad2Deg - 90.0f );

		return( diff );
	}

	void FireBullet( Vector2 pos,Vector2 moveDir )
	{
		var bull = Instantiate( bulletPrefab,
			pos,Quaternion.identity );
		var bullBody = bull.GetComponent<Rigidbody2D>();
		bullBody.AddForce( moveDir * bulletSpeed,
			ForceMode2D.Impulse );

		audSrc.PlayOneShot( shootSounds[Random.Range( 0,
			shootSounds.Count )],0.2f );
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.tag == "SpaceFox" )
		{
			var healthBar = GetComponent<HealthBar>();
			healthBar.Hurt( 1 );

			Debug.Log( deathUI.name + " - " + GameObject.Find( "Canvas" ).name );
			Instantiate( deathUI,canvas.transform );
		}
	}

    // private void OnDestroy()
    // {
    //     Debug.Log(deathUI.name + " - " + GameObject.Find("Canvas").name);
    //     Instantiate(deathUI, canvas.transform);
    // }

    Rigidbody2D body;
	Camera cam;
	GameObject bulletPrefab;
	Transform gun1;
	Transform gun2;
	Transform shotgun;
	AudioSource audSrc;
    GameObject canvas;

	List<AudioClip> shootSounds = new List<AudioClip>();

	int curGun = 0;

    /*[SerializeField]*/ GameObject deathUI = null;
	[SerializeField] float pushForce = 0.0f;
	// [SerializeField] float shotgunPushForce = 0.0f;
	[SerializeField] float bulletSpeed = 0.0f;
	// [SerializeField] float shotgunSpread = 0.0f;
	[SerializeField] Timer refire = new Timer( 0.2f );
	// [SerializeField] Timer burstRefire = new Timer( 0.5f );
}
