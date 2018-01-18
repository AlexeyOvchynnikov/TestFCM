package md5b030589d025f30f589d5a8ac845a9865;


public class MyFirebaseIidService
	extends com.google.firebase.iid.FirebaseInstanceIdService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenRefresh:()V:GetOnTokenRefreshHandler\n" +
			"";
		mono.android.Runtime.register ("TestFCM.Droid.MyFirebaseIidService, TestFCM.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyFirebaseIidService.class, __md_methods);
	}


	public MyFirebaseIidService ()
	{
		super ();
		if (getClass () == MyFirebaseIidService.class)
			mono.android.TypeManager.Activate ("TestFCM.Droid.MyFirebaseIidService, TestFCM.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onTokenRefresh ()
	{
		n_onTokenRefresh ();
	}

	private native void n_onTokenRefresh ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
