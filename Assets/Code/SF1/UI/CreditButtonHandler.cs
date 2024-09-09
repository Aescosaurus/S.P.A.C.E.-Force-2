using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButtonHandler
	:
	MonoBehaviour
{
	public void OpenAescTwitter()
	{
		Application.OpenURL( "https://twitter.com/Aescosaurus" );
	}
	public void OpenAescYoutube()
	{
		Application.OpenURL( "https://www.youtube.com/channel/UCubN3UbXTajcE4600l6WYew" );
	}
	public void OpenAescDiscord()
	{
		Application.OpenURL( "https://discord.gg/TPMTph" );
	}
	public void OpenAescGithub()
	{
		Application.OpenURL( "https://github.com/Aescosaurus" );
	}

	public void OpenCosInsta()
	{
		Application.OpenURL( "https://www.instagram.com/wjbono/" );
	}
	public void OpenCosGithub()
	{
		Application.OpenURL( "https://github.com/wjbono" );
	}

	public void OpenBenSite()
	{
		Application.OpenURL( "https://benedictroffmarsh.com/" );
	}
	public void OpenBenBandcamp()
	{
		Application.OpenURL( "https://benedictroff-marsh.bandcamp.com/" );
	}
	public void OpenBenFacebook()
	{
		Application.OpenURL( "https://www.facebook.com/benedict.roffmarsh" );
	}
	public void OpenBenYoutube()
	{
		Application.OpenURL( "https://www.youtube.com/user/BenedictRoffMarsh/videos" );
	}
}
