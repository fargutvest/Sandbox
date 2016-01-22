function post_thanks_collapse(postid)
{	
	fetch_object('post_thanks_collapse_box_' + postid).innerHTML = '<img src="images/misc/progress.gif" />';

	YAHOO.util.Connect.asyncRequest('POST', 'post_thanks.php', {
		success: function(o)
		{
			if(o.responseText !== undefined)
			{
				fetch_object('post_thanks_collapse_box_' + postid).innerHTML = o.responseText;
			}
		},
	failure: function(o)
	{
		if(o.responseText !== undefined)
		{
			alert(o.responseText);
		}
	},
	timeout: vB_Default_Timeout,
	cache: false
	}, 'do=post_thanks_collapse&p=' + postid + '&securitytoken=' + SECURITYTOKEN);

	return false;
}