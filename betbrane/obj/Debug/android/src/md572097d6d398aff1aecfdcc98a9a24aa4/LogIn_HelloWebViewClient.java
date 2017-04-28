package md572097d6d398aff1aecfdcc98a9a24aa4;


public class LogIn_HelloWebViewClient
	extends android.webkit.WebViewClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("betbrane.Resources.Activities.LogIn+HelloWebViewClient, betbrane, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LogIn_HelloWebViewClient.class, __md_methods);
	}


	public LogIn_HelloWebViewClient () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LogIn_HelloWebViewClient.class)
			mono.android.TypeManager.Activate ("betbrane.Resources.Activities.LogIn+HelloWebViewClient, betbrane, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
