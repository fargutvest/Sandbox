/*!======================================================================*\
|| #################################################################### ||
|| # vBulletin 3.8.7
|| # ---------------------------------------------------------------- # ||
|| # Copyright �2000-2011 vBulletin Solutions, Inc. All Rights Reserved. ||
|| # This file may not be redistributed in whole or significant part. # ||
|| # ---------------- VBULLETIN IS NOT FREE SOFTWARE ---------------- # ||
|| # http://www.vbulletin.com | http://www.vbulletin.com/license.html # ||
|| #################################################################### ||
\*======================================================================*/

/**
* Handle Firebug calls when Firebug is not available (getfirebug.com)
*/
if (!window.console || !console.firebug)
{
    window.console = {};
    var names = ["log", "debug", "info", "warn", "error", "assert", "dir", "dirxml", "group", "groupEnd", "time", "timeEnd", "count", "trace", "profile", "profileEnd"];
    for (var i = 0; i < names.length; ++i) window.console[names[i]] = function() {};
}

/**
* Setup Variables
*
* @var    string    SESSIONURL - stores the session URL
* @var    string    SECURITYTOKEN - token that should be passed with POST requests
* @var    array    vbphrase - stores text phrases
* @var    array    vB_Editor - array of vB_Text_Editor objects
* @var    boolean    ignorequotechars - ignore characters inside [quote] tags for message length check
* @var    integer    pagenavcounter - counts the number of pagenav instances encountered so far
* @var    boolean    is_regexp - does window.regExp exist? - Catch errors with less capable browsers
* @var    boolean    AJAX_Compatible - does the current browser support AJAX?
* @var    string    pointer_cursor - help out old versions of IE that don't understand style.cursor = pointer
* @var    array    Viewport info array
* @var    integer    Length of standard AJAX timeout (ms)
*/
var SESSIONURL       = (typeof(SESSIONURL) == "undefined" ? "" : SESSIONURL);
var SECURITYTOKEN    = (typeof(SECURITYTOKEN) == "undefined" ? "" : SECURITYTOKEN);
var vbphrase         = (typeof(vbphrase) == "undefined" ? new Array() : vbphrase);
var vB_Editor        = new Array();
var ignorequotechars = false;
var pagenavcounter   = 0;
var is_regexp        = (window.RegExp) ? true : false;
var AJAX_Compatible  = false;
var viewport_info    = null;
var vB_Default_Timeout = 15000;

/**
* Define the browser loading the page
*
* @var    string    userAgent Useragent string
* @var    boolean    is_opera  Opera
* @var    boolean    is_saf    Safari
* @var    boolean    is_webtv  WebTV
* @var    boolean    is_ie     Internet Explorer
* @var    boolean    is_ie4    Internet Explorer 4
* @var    boolean    is_ie7    Internet Explorer 7
* @var    boolean    is_ps3    Playstation 3
* @var    boolean    is_moz    Mozilla / Firefox / Camino
* @var    boolean    is_kon    Konqueror
* @var    boolean    is_ns     Netscape
* @var    boolean    is_ns4    Netscape 4
* @var    boolean    is_mac    Client is running MacOS
*/
var userAgent = navigator.userAgent.toLowerCase();
var is_opera  = (YAHOO.env.ua.opera > 0);
var is_saf    = (YAHOO.env.ua.webkit > 0);
var is_webtv  = (userAgent.indexOf('webtv') != -1);
var is_ie     = ((YAHOO.env.ua.ie > 0) && (!is_opera) && (!is_saf) && (!is_webtv));
var is_ie4    = (YAHOO.env.ua.ie == 4);
var is_ie7    = (YAHOO.env.ua.ie >= 7);
var is_ps3    = (userAgent.indexOf('playstation 3') != -1);
var is_moz    = (YAHOO.env.ua.gecko > 0);
var is_kon    = (userAgent.indexOf('konqueror') != -1);
var is_ns     = ((userAgent.indexOf('compatible') == -1) && (userAgent.indexOf('mozilla') != -1) && (!is_opera) && (!is_webtv) && (!is_saf));
var is_ns4    = ((is_ns) && (parseInt(navigator.appVersion) == 4));
var is_mac    = (userAgent.indexOf('mac') != -1);

/**
* @var    string    pointer_cursor - help out old versions of IE that don't understand style.cursor = pointer
*/
var pointer_cursor   = (is_ie ? 'hand' : 'pointer');

/**
* Workaround for heinous IE bug - add special vBlength property to all strings
* This method is applied to ALL string objects automatically
*
* @return    integer
*/
String.prototype.vBlength = function()
{
    return (is_ie && this.indexOf("\n") != -1) ? this.replace(/\r?\n/g, "_").length : this.length;
}

/**
* Overrides IE's original String.prototype.substr to accept negative values
*
* @param    integer    Substring start position
* @param    integer    Substring length
*
* @return    string
*/
if ("1234".substr(-2, 2) == "12") // (which would be incorrect)
{
    String.prototype.substr_orig = String.prototype.substr;

    String.prototype.substr = function(start, length)
    {
        if (typeof(length) == "undefined")
        {
            return this.substr_orig((start < 0 ? this.length + start : start));
        }
        else
        {
            return this.substr_orig((start < 0 ? this.length + start : start), length);
        }
    };
}

/**
* Define Array.shift() for browsers that don't have it
*/
if (typeof Array.prototype.shift === 'undefined')
{
    Array.prototype.shift = function()
    {
        for(var i = 0, b = this[0], l = this.length-1; i < l; i++)
        {
            this[i] = this[i + 1];
        }
        this.length--;
        return b;
    };
}

/**
* Function to emulate document.getElementById
*
* @param    string    Object ID
*
* @return    mixed    null if not found, object if found
*/
function fetch_object(idname)
{
    if (document.getElementById)
    {
        return document.getElementById(idname);
    }
    else if (document.all)
    {
        return document.all[idname];
    }
    else if (document.layers)
    {
        return document.layers[idname];
    }
    else
    {
        return null;
    }
}

/**
* Function to emulate document.getElementsByTagName
*
* @param    object    Parent object (eg: document)
* @param    string    Tag type (eg: 'td')
*
* @return    array
*/
function fetch_tags(parentobj, tag)
{
    if (parentobj == null)
    {
        return new Array();
    }
    else if (typeof parentobj.getElementsByTagName != 'undefined')
    {
        return parentobj.getElementsByTagName(tag);
    }
    else if (parentobj.all && parentobj.all.tags)
    {
        return parentobj.all.tags(tag);
    }
    else
    {
        return new Array();
    }
}

/**
* Function to count the number of tags in an object
*
* @param    object    Parent object (eg: document)
* @param    string    Tag type (eg: 'td')
*
* @return    integer
*/
function fetch_tag_count(parentobj, tag)
{
    return fetch_tags(parentobj, tag).length;
}

// #############################################################################
// Event handlers

/**
* Handles the different event models of different browsers and prevents event bubbling
*
* @param    event    Event object
*
* @return    event
*/
function do_an_e(eventobj)
{
    if (!eventobj || is_ie)
    {
        window.event.returnValue = false;
        window.event.cancelBubble = true;
        return window.event;
    }
    else
    {
        eventobj.stopPropagation();
        eventobj.preventDefault();
        return eventobj;
    }
}

/**
* Handles the different event models of different browsers and prevents event bubbling in a lesser way than do_an_e()
*
* @param    event    Event object
*
* @return    event
*/
function e_by_gum(eventobj)
{
    if (!eventobj || is_ie)
    {
        window.event.cancelBubble = true;
        return window.event;
    }
    else
    {
        if (eventobj.target.type == 'submit')
        {
            // naughty safari
            eventobj.target.form.submit();
        }
        eventobj.stopPropagation();
        return eventobj;
    }
}

// #############################################################################
// Message manipulation and validation

/**
* Checks that a message is valid for submission to PHP
*
* @param    string    Message text
* @param    mixed    Either subject text (if you want to make sure it exists) or 0 if you don't care
* @param    integer    Minimum acceptable character limit for the message
*
* @return    boolean
*/
function validatemessage(messagetext, subjecttext, minchars)
{
    if (is_kon || is_saf || is_webtv)
    {
        // ignore less-than-capable browsers
        return true;
    }
    else if (subjecttext.length < 1)
    {
        // subject not specified
        alert(vbphrase['must_enter_subject']);
        return false;
    }
    else
    {
        var stripped = PHP.trim(stripcode(messagetext, false, ignorequotechars));

        if (stripped.length < minchars)
        {
            // minimum message length not met
            alert(construct_phrase(vbphrase['message_too_short'], minchars));
            return false;
        }
        else if (typeof(document.forms.vbform) != 'undefined' && typeof(document.forms.vbform.imagestamp) != 'undefined')
        {
            // This form has image verification enabled
            document.forms.vbform.imagestamp.failed = false;

            if (document.forms.vbform.imagestamp.value.length != 6)
            {
                alert(vbphrase['complete_image_verification']);
                document.forms.vbform.imagestamp.failed = true;
                document.forms.vbform.imagestamp.focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            // everything seems ok
            return true;
        }
    }
}


/**
* Strips quotes and bbcode tags from text
*
* @param    string    Text to manipulate
* @param    boolean    If true, strip <x> otherwise strip [x]
* @param    boolean    If true, strip all [quote]...contents...[/quote]
*
* @return    string
*/
function stripcode(str, ishtml, stripquotes)
{
    if (!is_regexp)
    {
        return str;
    }

    if (stripquotes)
    {
        var start_time = new Date().getTime();

        while ((startindex = PHP.stripos(str, '[quote')) !== false)
        {
            if (new Date().getTime() - start_time > 2000)
            {
                // while loop has been running for over 2 seconds and has probably gone infinite
                break;
            }

            if ((stopindex = PHP.stripos(str, '[/quote]')) !== false)
            {
                fragment = str.substr(startindex, stopindex - startindex + 8);
                str = str.replace(fragment, '');
            }
            else
            {
                break;
            }
            str = PHP.trim(str);
        }
    }

    if (ishtml)
    {
        // exempt image tags -- they need to count as characters in the string
        // as the do as BB codes
        str = str.replace(/<img[^>]+src="([^"]+)"[^>]*>/gi, '$1');

        var html1 = new RegExp("<(\\w+)[^>]*>", 'gi');
        var html2 = new RegExp("<\\/\\w+>", 'gi');

        str = str.replace(html1, '');
        str = str.replace(html2, '');

        var html3 = new RegExp('(&nbsp;)', 'gi');
        str = str.replace(html3, ' ');
    }
    else
    {
        var bbcode1 = new RegExp("\\[(\\w+)(=[^\\]]*)?\\]", 'gi');
        var bbcode2 = new RegExp("\\[\\/(\\w+)\\]", 'gi');

        str = str.replace(bbcode1, '');
        str = str.replace(bbcode2, '');
    }

    return str;
}

// #############################################################################
// vB_PHP_Emulator class
// #############################################################################

/**
* PHP Function Emulator Class
*/
function vB_PHP_Emulator()
{
}

// =============================================================================
// vB_PHP_Emulator Methods

/**
* Find a string within a string (case insensitive)
*
* @param    string    Haystack
* @param    string    Needle
* @param    integer    Offset
*
* @return    mixed    Not found: false / Found: integer position
*/
vB_PHP_Emulator.prototype.stripos = function(haystack, needle, offset)
{
    if (typeof offset == 'undefined')
    {
        offset = 0;
    }

    index = haystack.toLowerCase().indexOf(needle.toLowerCase(), offset);

    return (index == -1 ? false : index);
}

/**
* Trims leading whitespace
*
* @param    string    String to trim
*
* @return    string
*/
vB_PHP_Emulator.prototype.ltrim = function(str)
{
    return str.replace(/^\s+/g, '');
}

/**
* Trims trailing whitespace
*
* @param    string    String to trim
*
* @return    string
*/
vB_PHP_Emulator.prototype.rtrim = function(str)
{
    return str.replace(/(\s+)$/g, '');
}

/**
* Trims leading and trailing whitespace
*
* @param    string    String to trim
*
* @return    string
*/
vB_PHP_Emulator.prototype.trim = function(str)
{
    return this.ltrim(this.rtrim(str));
}

/**
* Emulation of PHP's preg_quote()
*
* @param    string    String to process
*
* @return    string
*/
vB_PHP_Emulator.prototype.preg_quote = function(str)
{
    // replace + { } ( ) [ ] | / ? ^ $ \ . = ! < > : * with backslash+character
    return str.replace(/(\+|\{|\}|\(|\)|\[|\]|\||\/|\?|\^|\$|\\|\.|\=|\!|\<|\>|\:|\*)/g, "\\$1");
}

/**
* Emulates PHP's preg_match_all()... sort of
*
* @param    string    Haystack
* @param    string    Regular expression - to be inserted into RegExp(x)
*
* @return    mixed    Array on match, false on no match
*/
vB_PHP_Emulator.prototype.match_all = function(string, regex)
{
    var gmatch = string.match(RegExp(regex, "gim"));
    if (gmatch)
    {
        var matches = new Array();

        var iregex = new RegExp(regex, "im");
        for (var i = 0; i < gmatch.length; i++)
        {
            matches[matches.length] = gmatch[i].match(iregex);
        }

        return matches;
    }
    else
    {
        return false;
    }
}

/**
* Emulates unhtmlspecialchars in vBulletin
*
* @param    string    String to process
*
* @return    string
*/
vB_PHP_Emulator.prototype.unhtmlspecialchars = function(str)
{
    var f = new Array(/&lt;/g, /&gt;/g, /&quot;/g, /&amp;/g);
    var r = new Array('<', '>', '"', '&');

    for (var i in f)
    {
        if (YAHOO.lang.hasOwnProperty(f, i))
        {
            str = str.replace(f[i], r[i]);
        }
    }

    return str;
}

/**
* Unescape CDATA from vB_AJAX_XML_Builder PHP class
*
* @param    string    Escaped CDATA
*
* @return    string
*/
vB_PHP_Emulator.prototype.unescape_cdata = function(str)
{
    var r1 = /<\=\!\=\[\=C\=D\=A\=T\=A\=\[/g;
    var r2 = /\]\=\]\=>/g;

    return str.replace(r1, '<![CDATA[').replace(r2, ']]>');
}

/**
* Emulates PHP's htmlspecialchars()
*
* @param    string    String to process
*
* @return    string
*/
vB_PHP_Emulator.prototype.htmlspecialchars = function(str)
{
    //var f = new Array(/&(?!#[0-9]+;)/g, /</g, />/g, /"/g);
    var f = new Array(
        (is_mac && is_ie ? new RegExp('&', 'g') : new RegExp('&(?!#[0-9]+;)', 'g')),
        new RegExp('<', 'g'),
        new RegExp('>', 'g'),
        new RegExp('"', 'g')
    );
    var r = new Array(
        '&amp;',
        '&lt;',
        '&gt;',
        '&quot;'
    );

    for (var i = 0; i < f.length; i++)
    {
        str = str.replace(f[i], r[i]);
    }

    return str;
}

/**
* Searches an array for a value
*
* @param    string    Needle
* @param    array    Haystack
* @param    boolean    Case insensitive
*
* @return    integer    Not found: -1 / Found: integer index
*/
vB_PHP_Emulator.prototype.in_array = function(ineedle, haystack, caseinsensitive)
{
    var needle = new String(ineedle);
    var i;

    if (caseinsensitive)
    {
        needle = needle.toLowerCase();
        for (i in haystack)
        {
            if (YAHOO.lang.hasOwnProperty(haystack, i))
            {
                if (haystack[i].toLowerCase() == needle)
                {
                    return i;
                }
            }
        }
    }
    else
    {
        for (i in haystack)
        {
            if (YAHOO.lang.hasOwnProperty(haystack, i))
            {
                if (haystack[i] == needle)
                {
                    return i;
                }
            }
        }
    }
    return -1;
}

/**
* Emulates PHP's strpad()
*
* @param    string    Text to pad
* @param    integer    Length to pad
* @param    string    String with which to pad
*
* @return    string
*/
vB_PHP_Emulator.prototype.str_pad = function(text, length, padstring)
{
    text = new String(text);
    padstring = new String(padstring);

    if (text.length < length)
    {
        padtext = new String(padstring);

        while (padtext.length < (length - text.length))
        {
            padtext += padstring;
        }

        text = padtext.substr(0, (length - text.length)) + text;
    }

    return text;
}

/**
* A sort of emulation of PHP's urlencode - not 100% the same, but accomplishes the same thing
*
* @param    string    String to encode
*
* @return    string
*/
vB_PHP_Emulator.prototype.urlencode = function(text)
{
    text = escape(text.toString()).replace(/\+/g, "%2B");

    // this escapes 128 - 255, as JS uses the unicode code points for them.
    // This causes problems with submitting text via AJAX with the UTF-8 charset.
    var matches = text.match(/(%([0-9A-F]{2}))/gi);
    if (matches)
    {
        for (var matchid = 0; matchid < matches.length; matchid++)
        {
            var code = matches[matchid].substring(1,3);
            if (parseInt(code, 16) >= 128)
            {
                text = text.replace(matches[matchid], '%u00' + code);
            }
        }
    }

    // %25 gets translated to % by PHP, so if you have %25u1234,
    // we see it as %u1234 and it gets translated. So make it %u0025u1234,
    // which will print as %u1234!
    text = text.replace('%25', '%u0025');

    return text;
}

/**
* Works a bit like ucfirst, but with some extra options
*
* @param    string    String with which to work
* @param    string    Cut off string before first occurence of this string
*
* @return    string
*/
vB_PHP_Emulator.prototype.ucfirst = function(str, cutoff)
{
    if (typeof cutoff != 'undefined')
    {
        var cutpos = str.indexOf(cutoff);
        if (cutpos > 0)
        {
            str = str.substr(0, cutpos);
        }
    }

    str = str.split(' ');
    for (var i = 0; i < str.length; i++)
    {
        str[i] = str[i].substr(0, 1).toUpperCase() + str[i].substr(1);
    }
    return str.join(' ');
}

// #############################################################################
// vB_AJAX_Handler
// #############################################################################

/**
* XML Sender Class - deprecated. Here for BC only. Use YAHOO.util.Connect.asyncRequest instead.
*
* @param    boolean    Should connections be asyncronous? (Now redundant)
*/
function vB_AJAX_Handler(async) { this.async = async ? true : false; this.conn = null; };
vB_AJAX_Handler.prototype.init = function() { return AJAX_Compatible; };
vB_AJAX_Handler.is_compatible = function() { return AJAX_Compatible; };
vB_AJAX_Handler.prototype.onreadystatechange = function(callback) { this.callback = callback; };
vB_AJAX_Handler.prototype.fetch_data = function(xml_node) {
    console.warn("vB_AJAX_Handler.prototype.fetch_data() is deprecated.\nUse responseXML.getElementsByTagName(\"x\")[i].firstChild.nodeValue instead.");
    if (xml_node && xml_node.firstChild && xml_node.firstChild.nodeValue) { return PHP.unescape_cdata(xml_node.firstChild.nodeValue); } else { return ''; }
};
vB_AJAX_Handler.prototype.send = function(desturl, datastream) {
    this.conn = YAHOO.util.Connect.asyncRequest("POST", desturl, { success: this.callback }, datastream + "&securitytoken=" + SECURITYTOKEN + "&s=" + fetch_sessionhash());
    this.handler = this.conn.conn;
};

/**
* Checks to see if this Javascript-aware browser is also capable of handling AJAX requests
*
* @return    boolean
*/
function is_ajax_compatible()
{
    if (typeof vb_disable_ajax != "undefined" && vb_disable_ajax == 2)
    {
        return false;
    }
    else if (is_ie && !is_ie4)
    {
        return true;
    }
    else if (window.XMLHttpRequest)
    {
        try
        {
            var ajax_test = new XMLHttpRequest();
            return ajax_test.setRequestHeader ? true : false;
        }
        catch(e)
        {
            return false;
        }
    }
    else
    {
        return false;
    }
}

// we can check this variable to see if browser is AJAX compatible
AJAX_Compatible = is_ajax_compatible();
console.info("This browser is%s AJAX compatible", AJAX_Compatible ? "" : " NOT");

// #############################################################################
// YUI AJAX Stuff
// #############################################################################

/**
* Handles AJAX request timeouts returned from YUI connection manager
*
* @param    object    YUI AJAX
*/
function vBulletin_AJAX_Error_Handler(ajax)
{
    /**
    * Available properties of ajax object
    *
    * .tId
    * .status
    * .statusText
    * .getResponseHeader[ ]
    * .getAllResponseHeaders
    * .responseText
    * .responseXML
    * .argument
    */
    console.warn("AJAX Error: Status = %s: %s", ajax.status, ajax.statusText);
}

// #############################################################################
// vB_Hidden_Form
// #############################################################################

/**
* Form Generator Class
*
* Builds a form filled with hidden fields for invisible submit via POST
*
* @param    string    Script (my_target_script.php)
*/
function vB_Hidden_Form(script)
{
    this.action = script;
    this.variables = new Array();
}

// =============================================================================
// vB_Hidden_Form methods

/**
* Adds a hidden input field to the form object
*
* @param    string    Name attribute
* @param    string    Value attribute
*/
vB_Hidden_Form.prototype.add_variable = function(name, value)
{
    this.variables[this.variables.length] = new Array(name, value);
    console.log("vB_Hidden_Form :: add_variable(%s)", name);
};

/**
* Fetches all form elements inside an HTML element and performs 'add_input()' on them
*
* @param    object    HTML element to search
*/
vB_Hidden_Form.prototype.add_variables_from_object = function(obj)
{
    if (!obj)
    {
        return;
    }
    console.info("vB_Hidden_Form :: add_variables_from_object(%s)", obj.id);
    var inputs = fetch_tags(obj, 'input');
    var i;

    for (i = 0; i < inputs.length; i++)
    {
        switch (inputs[i].type)
        {
            case 'checkbox':
            case 'radio':
                if (inputs[i].checked)
                {
                    this.add_variable(inputs[i].name, inputs[i].value);
                }
                break;
            case 'text':
            case 'hidden':
            case 'password':
                this.add_variable(inputs[i].name, inputs[i].value);
                break;
            default:
                continue;
        }
    }

    var textareas = fetch_tags(obj, 'textarea');
    for (i = 0; i < textareas.length; i++)
    {
        this.add_variable(textareas[i].name, textareas[i].value);
    }

    var selects = fetch_tags(obj, 'select');
    for (i = 0; i < selects.length; i++)
    {
        if (selects[i].multiple)
        {
            for (var j = 0; j < selects[i].options.length; j++)
            {
                if (selects[i].options[j].selected)
                {
                    this.add_variable(selects[i].name, selects[i].options[j].value);
                }
            }
        }
        else
        {
            this.add_variable(selects[i].name, selects[i].options[selects[i].selectedIndex].value);
        }
    }
};

/**
* Fetches a variable value
*
* @param    string    Variable name
*
* @return    mixed    Variable value
*/
vB_Hidden_Form.prototype.fetch_variable = function(varname)
{
    for (var i = 0; i < this.variables.length; i++)
    {
        if (this.variables[i][0] == varname)
        {
            return this.variables[i][1];
        }
    }

    return null;
};

/**
* Submits the hidden form object
*/
vB_Hidden_Form.prototype.submit_form = function()
{
    this.form = document.createElement('form');
    this.form.method = 'post';
    this.form.action = this.action;

    for (var i = 0; i < this.variables.length; i++)
    {
        var inputobj = document.createElement('input');

        inputobj.type  = 'hidden';
        inputobj.name  = this.variables[i][0];
        inputobj.value = this.variables[i][1];

        this.form.appendChild(inputobj);
    }

    console.info("vB_Hidden_Form :: submit_form() -> %s", this.action);
    document.body.appendChild(this.form).submit();
};

/**
* Builds a URI query string from the given variables
*/
vB_Hidden_Form.prototype.build_query_string = function()
{
    var query_string = '';

    for (var i = 0; i < this.variables.length; i++)
    {
        query_string += this.variables[i][0] + '=' + PHP.urlencode(this.variables[i][1]) + '&';
    }

    console.info("vB_Hidden_Form :: Query String = %s", query_string);
    return query_string;
}

/**
* Legacy functions for backward compatability
*/
vB_Hidden_Form.prototype.add_input = vB_Hidden_Form.prototype.add_variable;
vB_Hidden_Form.prototype.add_inputs_from_object = vB_Hidden_Form.prototype.add_variables_from_object;


// #############################################################################
// vB_Select_Overlay_Handler
// #############################################################################

/**
* Handler for <select> tags that are overlayed with another element
* Fixes a problem in IE versions older than IE7.
*
* @param    mixed    Object or ID string that is the overlayed object
*/
function vB_Select_Overlay_Handler(overlay)
{
    this.browser_affected = (is_ie && YAHOO.env.ua.ie < 7);

    if (this.browser_affected)
    {
        this.overlay = YAHOO.util.Dom.get(overlay);
        this.hidden_selects = new Array();
        console.log("Initializing <select> overlay handler for '%s'.", this.overlay.id);
    }
}

// =============================================================================
// vB_Hidden_Form methods

/**
* Hides any selects that intersect the overlayed object
*/
vB_Select_Overlay_Handler.prototype.hide = function()
{
    if (this.browser_affected)
    {
        var overlay_region = YAHOO.util.Dom.getRegion(this.overlay);

        var selects = document.getElementsByTagName("select");
        for (var i = 0; i < selects.length; i++)
        {
            if (region_intersects(selects[i], overlay_region))
            {
                if (YAHOO.util.Dom.isAncestor(this.overlay, selects[i]))
                {
                    continue;
                }
                else
                {
                    YAHOO.util.Dom.setStyle(selects[i], "visibility", "hidden");
                    this.hidden_selects.push(YAHOO.util.Dom.generateId(selects[i]));
                }
            }
        }
    }
};

/**
* Un-hides any hidden selects
*/
vB_Select_Overlay_Handler.prototype.show = function()
{
    if (this.browser_affected)
    {
        var selectid;
        while (selectid = this.hidden_selects.pop())
        {
            YAHOO.util.Dom.setStyle(selectid, "visibility", "visible");
        }
    }
};

// #############################################################################
// Window openers and instant messenger wrappers

/**
* Opens a generic browser window
*
* @param    string    URL
* @param    integer    Width
* @param    integer    Height
* @param    string    Optional Window ID
*/
function openWindow(url, width, height, windowid)
{
    return window.open(
        url,
        (typeof windowid == 'undefined' ? 'vBPopup' : windowid),
        'statusbar=no,menubar=no,toolbar=no,scrollbars=yes,resizable=yes'
        + (typeof width != 'undefined' ? (',width=' + width) : '') + (typeof height != 'undefined' ? (',height=' + height) : '')
    );
}

/**
* Opens control panel help window
*
* @param    string    Script name
* @param    string    Action type
* @param    string    Option value
*
* @return    window
*/
function js_open_help(scriptname, actiontype, optionval)
{
    return openWindow(
        'help.php?s=' + SESSIONHASH + '&do=answer&page=' + scriptname + '&pageaction=' + actiontype + '&option=' + optionval,
        600, 450, 'helpwindow'
    );
}

/**
* Opens a window to show a list of attachments in a thread (misc.php?do=showattachments)
*
* @param    integer    Thread ID
*
* @return    window
*/
function attachments(threadid)
{
    return openWindow(
        'misc.php?' + SESSIONURL + 'do=showattachments&t=' + threadid,
        480, 300
    );
}

/**
* Opens a window to show a list of posters in a thread (misc.php?do=whoposted)
*
* @param    integer    Thread ID
*
* @return    window
*/
function who(threadid)
{
    return openWindow(
        'misc.php?' + SESSIONURL + 'do=whoposted&t=' + threadid,
        230, 300
    );
}

/**
* Opens an IM Window
*
* @param    string    IM type
* @param    integer    User ID
* @param    integer    Width of window
* @param    integer    Height of window
*
* @return    window
*/
function imwindow(imtype, userid, width, height)
{
    return openWindow(
        'sendmessage.php?' + SESSIONURL + 'do=im&type=' + imtype + '&u=' + userid,
        width, height
    );
}

/**
* Sends an MSN message
*
* @param    string    Target MSN handle
*
* @return    boolean    false
*/
function SendMSNMessage(name)
{
    if (!is_ie)
    {
        alert(vbphrase['msn_functions_only_work_in_ie']);
    }
    else
    {
        try
        {
            MsgrObj.InstantMessage(name);
        }
        catch(e)
        {
            alert(vbphrase['msn_functions_only_work_in_ie']);
        }
    }

    return false;
}

/**
* Adds an MSN Contact (requires MSN)
*
* @param    string    MSN handle
*
* @return    boolean    false
*/
function AddMSNContact(name)
{
    if (!is_ie)
    {
        alert(vbphrase['msn_functions_only_work_in_ie']);
    }
    else
    {
        try
        {
            MsgrObj.AddContact(0, name);
        }
        catch(e)
        {
            alert(vbphrase['msn_functions_only_work_in_ie']);
        }
    }

    return false;
}

/**
* Detects Caps-Lock when a key is pressed
*
* @param    event
*
* @return    boolean    True if Caps-Lock is on
*/
function detect_caps_lock(e)
{
    e = (e ? e : window.event);

    var keycode = (e.which ? e.which : (e.keyCode ? e.keyCode : (e.charCode ? e.charCode : 0)));
    var shifted = (e.shiftKey || (e.modifiers && (e.modifiers & 4)));
    var ctrled = (e.ctrlKey || (e.modifiers && (e.modifiers & 2)));

    // if characters are uppercase without shift, or lowercase with shift, caps-lock is on.
    return (keycode >= 65 && keycode <= 90 && !shifted && !ctrled) || (keycode >= 97 && keycode <= 122 && shifted);
}

/**
* Confirms log-out request
*
* @param    string    Log-out confirmation message
*
* @return    boolean
*/
function log_out(confirmation_message)
{
    var ht = document.getElementsByTagName("html")[0];
    ht.style.filter = "progid:DXImageTransform.Microsoft.BasicImage(grayscale=1)";
    if (confirm(confirmation_message))
    {
        return true;
    }
    else
    {
        ht.style.filter = "";
        return false;
    }
}

// #############################################################################
// Cookie handlers

/**
* Sets a cookie
*
* @param    string    Cookie name
* @param    string    Cookie value
* @param    date    Cookie expiry date
*/
function set_cookie(name, value, expires)
{
    console.log("Set Cookie :: %s = '%s'", name, value);
    document.cookie = name + '=' + escape(value) + '; path=/' + (typeof expires != 'undefined' ? '; expires=' + expires.toGMTString() : '');
}

/**
* Deletes a cookie
*
* @param    string    Cookie name
*/
function delete_cookie(name)
{
    console.log("Delete Cookie :: %s", name);
    document.cookie = name + '=' + '; expires=Thu, 01-Jan-70 00:00:01 GMT' +  '; path=/';
}

/**
* Fetches the value of a cookie
*
* @param    string    Cookie name
*
* @return    string
*/
function fetch_cookie(name)
{
    cookie_name = name + '=';
    cookie_length = document.cookie.length;
    cookie_begin = 0;
    while (cookie_begin < cookie_length)
    {
        value_begin = cookie_begin + cookie_name.length;
        if (document.cookie.substring(cookie_begin, value_begin) == cookie_name)
        {
            var value_end = document.cookie.indexOf (';', value_begin);
            if (value_end == -1)
            {
                value_end = cookie_length;
            }
            var cookie_value = unescape(document.cookie.substring(value_begin, value_end));
            console.log("Fetch Cookie :: %s = '%s'", name, cookie_value);
            return cookie_value;
        }
        cookie_begin = document.cookie.indexOf(' ', cookie_begin) + 1;
        if (cookie_begin == 0)
        {
            break;
        }
    }
    console.log("Fetch Cookie :: %s (null)", name);
    return null;
}

// #############################################################################
// Form element managers (used for 'check all' type systems)

/**
* Sets all checkboxes, radio buttons or selects in a given form to a given state, with exceptions
*
* @param    object    Form object
* @param    string    Target element type (one of 'radio', 'select-one', 'checkbox')
* @param    string    Selected option in case of 'radio'
* @param    array    Array of element names to be excluded
* @param    mixed    Value to give to found elements
*/
function js_toggle_all(formobj, formtype, option, exclude, setto)
{
    for (var i =0; i < formobj.elements.length; i++)
    {
        var elm = formobj.elements[i];
        if (elm.type == formtype && PHP.in_array(elm.name, exclude, false) == -1)
        {
            switch (formtype)
            {
                case 'radio':
                    if (elm.value == option) // option == '' evaluates true when option = 0
                    {
                        elm.checked = setto;
                    }
                break;
                case 'select-one':
                    elm.selectedIndex = setto;
                break;
                default:
                    elm.checked = setto;
                break;
            }
        }
    }
}

/**
* Sets all <select> elements to the selectedIndex specified by the 'selectall' element
*
* @param    object    Form object
*/
function js_select_all(formobj)
{
    exclude = new Array();
    exclude[0] = 'selectall';
    js_toggle_all(formobj, 'select-one', '', exclude, formobj.selectall.selectedIndex);
}

/**
* Sets all <input type="checkbox" /> elements to have the same checked status as 'allbox'
*
* @param    object    Form object
*/
function js_check_all(formobj)
{
    exclude = new Array();
    exclude[0] = 'keepattachments';
    exclude[1] = 'allbox';
    exclude[2] = 'removeall';
    js_toggle_all(formobj, 'checkbox', '', exclude, formobj.allbox.checked);
}

/**
* Sets all <input type="radio" /> groups to have a particular option checked
*
* @param    object    Form object
* @param    mixed    Selected option
*/
function js_check_all_option(formobj, option)
{
    exclude = new Array();
    exclude[0] = 'useusergroup';
    js_toggle_all(formobj, 'radio', option, exclude, true);
}

/**
* Alias to js_check_all
*/
function checkall(formobj)
{
    js_check_all(formobj);
}

/**
* Alias to js_check_all_option
*/
function checkall_option(formobj, option)
{
    js_check_all_option(formobj, option);
}

/**
* Resize function for CP textareas
*
* @param    integer    If positive, size up, otherwise size down
* @param    string    ID of the textarea
*
* @return    boolean    false
*/
function resize_textarea(to, id)
{
    var textarea = fetch_object(id);

    textarea.style.width = parseInt(textarea.offsetWidth) + (to < 0 ? -100 : 100) + "px";
    textarea.style.height = parseInt(textarea.offsetHeight) + (to < 0 ? -100 : 100) + "px";

    return false;
}

/**
* Determines if two HTML objects intersect
*
* @param    object    1st HTML object
* @param    object    2nd HTML object
*
* @return    boolean    True if intersection is found
*/
function region_intersects(obj1, obj2)
{
    obj1 = typeof(obj1.left) == "undefined" ? YAHOO.util.Dom.getRegion(obj1) : obj1;
    obj2 = typeof(obj2.left) == "undefined" ? YAHOO.util.Dom.getRegion(obj2) : obj2;

    return (obj1.left > obj2.right || obj1.right < obj2.left || obj1.top > obj2.bottom || obj1.bottom < obj2.top) ? false : true;
}

/**
* Fetch information about the current browser viewport
*
* @return    array    Contains x, y, w, h elements
*/
function fetch_viewport_info()
{
    if (viewport_info == null)
    {
        viewport_info = {
            x : YAHOO.util.Dom.getDocumentScrollLeft(),
            y : YAHOO.util.Dom.getDocumentScrollTop(),
            w : YAHOO.util.Dom.getViewportWidth(),
            h : YAHOO.util.Dom.getViewportHeight()
        };

        console.info("Viewport Info: Size = %dx%d, Position = %d,%d", viewport_info['w'], viewport_info['h'], viewport_info['x'], viewport_info['y']);
    }

    return viewport_info;
}

/**
* Clear cached viewport info
*/
function clear_viewport_info()
{
    viewport_info = null;
}

/**
* Position an element in the middle of the browser window
* Note that this will only work if it is called AFTER an element is part of the DOM tree and has its full size
*
* @param    object    HTML element to be positioned
*/
function center_element(el)
{
    viewport_info = fetch_viewport_info();

    YAHOO.util.Dom.setXY(el, [
        viewport_info['w'] / 2 + viewport_info['x'] - el.clientWidth / 2,
        viewport_info['h'] / 2 + viewport_info['y'] - el.clientHeight / 2
    ]);
}

/* Parses a stylesheet to find all rules within, including those fetched with @import
*
* @param    object    Usually document.styleSheets
*
* @return    array
*/
function fetch_all_stylesheets(sheets)
{
    var all_sheets = new Array(),
        i = 0,
        current_sheet = null,
        current_rule = 0,
        current_import = 0;

    for (i = 0; i < document.styleSheets.length; i++)
    {
        current_sheet = document.styleSheets[i];

        all_sheets.push(current_sheet);

        try
        {
            if (current_sheet.cssRules) // not IE
            {
                for (current_rule = 0; current_rule < current_sheet.cssRules.length; current_rule++)
                {
                    if (current_sheet.cssRules[current_rule].styleSheet)
                    {
                        all_sheets.push(current_sheet.cssRules[current_rule].styleSheet);
                    }
                }
            }
            else // IE
            {
                if (current_sheet.imports)
                {
                    for (current_import = 0; current_import < current_sheet.imports.length; current_import++)
                    {
                        all_sheets.push(current_sheet.imports[current_import]);
                    }
                }
            }
        }
        catch(e)
        {
            // trying to access a stylesheet outside the allowed domain
            all_sheets.pop();
            continue;
        }
    }

    return all_sheets;
}

/**
* Highlights the login box to make it obvious where to login.
*/
function highlight_login_box()
{
    var obj = fetch_object('navbar_username');
    var addclass = 'inlinemod';
    var i, timeout = 1600, flash_speed = 200;

    if (obj)
    {
        obj.focus();
        obj.select();

        for (i = 0; i < timeout; i += 2 * flash_speed)
        {
            window.setTimeout(function() { YAHOO.util.Dom.addClass(obj, addclass) }, i);
            window.setTimeout(function() { YAHOO.util.Dom.removeClass(obj, addclass) }, i + flash_speed);
        }
    }

    return false;
}

// #############################################################################
// Collapsible element handlers

/**
* Toggles the collapse state of an object, and saves state to 'vbulletin_collapse' cookie
*
* @param    string    Unique ID for the collapse group
*
* @return    boolean    false
*/
function toggle_collapse(objid, forcestate)
{
    if (!is_regexp)
    {
        return false;
    }

    var obj = fetch_object('collapseobj_' + objid);
    var img = fetch_object('collapseimg_' + objid);
    var cel = fetch_object('collapsecel_' + objid);

    if (!obj)
    {
        // nothing to collapse!
        if (img)
        {
            // hide the clicky image if there is one
            img.style.display = 'none';
        }
        return false;
    }

    if (obj.style.display == 'none' || 'open' == forcestate)
    {
        obj.style.display = '';

        if (!forcestate)
        {
            save_collapsed(objid, false);
        }

        if (img)
        {
            img_re = new RegExp("_collapsed\\.gif$");
            img.src = img.src.replace(img_re, '.gif');
        }
        if (cel)
        {
            cel_re = new RegExp("^(thead|tcat)(_collapsed)$");
            cel.className = cel.className.replace(cel_re, '$1');
        }
    }
    else if (obj.style.display != 'none' || 'closed' == forcestate)
    {
        obj.style.display = 'none';

        if (!forcestate)
        {
            save_collapsed(objid, true);
        }

        if (img)
        {
            img_re = new RegExp("\\.gif$");
            img.src = img.src.replace(img_re, '_collapsed.gif');
        }
        if (cel)
        {
            cel_re = new RegExp("^(thead|tcat)$");
            cel.className = cel.className.replace(cel_re, '$1_collapsed');
        }
    }
    return false;
}

/**
* Updates vbulletin_collapse cookie with collapse preferences
*
* @param    string    Unique ID for the collapse group
* @param    boolean    Add a cookie
*/
function save_collapsed(objid, addcollapsed)
{
    var collapsed = fetch_cookie('vbulletin_collapse');
    var tmp = new Array();

    if (collapsed != null)
    {
        collapsed = collapsed.split('\n');

        for (var i in collapsed)
        {
            if (YAHOO.lang.hasOwnProperty(collapsed, i) && collapsed[i] != objid && collapsed[i] != '')
            {
                tmp[tmp.length] = collapsed[i];
            }
        }
    }

    if (addcollapsed)
    {
        tmp[tmp.length] = objid;
    }

    expires = new Date();
    expires.setTime(expires.getTime() + (1000 * 86400 * 365));
    set_cookie('vbulletin_collapse', tmp.join('\n'), expires);
}

// #############################################################################
// Event Handlers for PageNav menus

/**
* Class to handle pagenav events
*/
function vBpagenav()
{
}

/**
* Handles clicks on pagenav menu control objects
*/
vBpagenav.prototype.controlobj_onclick = function(e)
{
    this._onclick(e);
    var inputs = fetch_tags(this.menu.menuobj, 'input');
    for (var i = 0; i < inputs.length; i++)
    {
        if (inputs[i].type == 'text')
        {
            inputs[i].focus();
            break;
        }
    }
};

/**
* Submits the pagenav form... sort of
*/
vBpagenav.prototype.form_gotopage = function(e)
{
    if ((pagenum = parseInt(fetch_object('pagenav_itxt').value, 10)) > 0)
    {
        window.location = vBmenu.menus[vBmenu.activemenu].addr + '&page=' + pagenum;
    }
    return false;
};

/**
* Handles clicks on the 'Go' button in pagenav popups
*/
vBpagenav.prototype.ibtn_onclick = function(e)
{
    return this.form.gotopage();
};

/**
* Handles keypresses in the text input of pagenav popups
*/
vBpagenav.prototype.itxt_onkeypress = function(e)
{
    return ((e ? e : window.event).keyCode == 13 ? this.form.gotopage() : true);
};

// #############################################################################
// DHTML Popup Menu Handling (complements vbulletin_menu.js)

/**
* Wrapper for vBmenu.register
*
* @param    string    Control ID
* @param    boolean    No image (true)
* @param    boolean    Does nothing any more
*/
function vbmenu_register(controlid, noimage, datefield)
{
    if (typeof(vBmenu) == "object")
    {
        return vBmenu.register(controlid, noimage);
    }
    else
    {
        return false;
    }
}

// #############################################################################
// Stuff that really doesn't fit anywhere else

/**
* Converts an HTML string into a DOM node for use elsewhere
*
* Note: Only works reliably if the string is enclosed in an HTML element.
* eg: "<span>Hello, <strong>username</strong></span>" not "Hello, <strong>username</strong>"
*
* @param    string    HTML String to convert
*/
function string_to_node(html_string)
{
    var node_container = document.createElement("div");
    node_container.innerHTML = html_string;

    var node = node_container.firstChild;
    while (node && node.nodeType != 1)
    {
        node = node.nextSibling;
    }

    if (!node)
    {
        return node_container.firstChild.cloneNode(true);
    }
    else
    {
        return node.cloneNode(true);
    }
}

/**
* Sets an element and all its children to be 'unselectable'
*
* @param    mixed    Object/Object ID to be made unselectable
*/
function set_unselectable(obj)
{
    obj = YAHOO.util.Dom.get(obj);

    if (!is_ie4 && typeof obj.tagName != 'undefined')
    {
        if (obj.hasChildNodes())
        {
            for (var i = 0; i < obj.childNodes.length; i++)
            {
                set_unselectable(obj.childNodes[i]);
            }
        }
        obj.unselectable = 'on';
    }
}

/**
* Fetches the sessionhash from the SESSIONURL variable
*
* @return    string
*/
function fetch_sessionhash()
{
    return (SESSIONURL == '' ? '' : SESSIONURL.substr(2, 32));
}

/**
* Emulates the PHP version of vBulletin's construct_phrase() sprintf wrapper
*
* @param    string    String containing %1$s type replacement markers
* @param    string    First replacement
* @param    string    Nth replacement
*
* @return    string
*/
function construct_phrase()
{
    if (!arguments || arguments.length < 1 || !is_regexp)
    {
        return false;
    }

    var args = arguments;
    var str = args[0];
    var re;

    for (var i = 1; i < args.length; i++)
    {
        re = new RegExp("%" + i + "\\$s", 'gi');
        str = str.replace(re, args[i]);
    }
    return str;
}

/**
* Handles the quick style/language options in the footer
*
* @param    object    Select object
* @param    string    Type (style or language)
*/
function switch_id(selectobj, type)
{
    var id = selectobj.options[selectobj.selectedIndex].value;

    if (id == '')
    {
        return;
    }

    var url = new String(window.location);
    var fragment = new String('');

    // get rid of fragment
    url = url.split('#');

    // deal with the fragment first
    if (url[1])
    {
        fragment = '#' + url[1];
    }

    // deal with the main url
    url = url[0];

    // remove id=x& from main bit
    if (url.indexOf(type + 'id=') != -1 && is_regexp)
    {
        var re = new RegExp(type + "id=\\d+&?");
        url = url.replace(re, '');
    }

    // add the ? to the url if needed
    if (url.indexOf('?') == -1)
    {
        url += '?';
    }
    else
    {
        // make sure that we have a valid character to join our id bit
        lastchar = url.substr(url.length - 1);
        if (lastchar != '&' && lastchar != '?')
        {
            url += '&';
        }
    }

    window.location = url + type + 'id=' + id + fragment;
}

/**
* Finds all images within the specified element and sets their alt attribute as title
*
* @param    object    Containing element
*/
function child_img_alt_2_title(object)
{
    var imgs = object.getElementsByTagName("img");
    for (var i = 0; i < imgs.length; i++)
    {
        img_alt_2_title(imgs[i]);
    }
}

/**
* Takes the 'alt' attribute for an image and attaches it to the 'title' attribute
*
* @param    object    Image object
*/
function img_alt_2_title(img)
{
    if (!img.title && img.alt != '')
    {
        img.title = img.alt;
    }
}


function do_securitytoken_replacement(newtoken)
{
    if (newtoken == '')
    {
        return;
    }

    for (var formid = 0; formid < document.forms.length; formid++)
    {
        if (document.forms[formid].elements['securitytoken'] && document.forms[formid].elements['securitytoken'].value == SECURITYTOKEN)
        {
            document.forms[formid].elements['securitytoken'].value = newtoken;
        }
    }
    SECURITYTOKEN = newtoken;
    console.log("Securitytoken updated");
}

function handle_securitytoken_response(ajax)
{
    console.log("Processing securitytoken update");
    if (ajax.responseXML && ajax.responseXML.getElementsByTagName("securitytoken").length)
    {
        var newtoken = ajax.responseXML.getElementsByTagName("securitytoken")[0].firstChild.nodeValue;
        do_securitytoken_replacement(newtoken);
        securitytoken_errors = 0;
    }
    else
    {
        handle_securitytoken_error(ajax);
    }
}

function handle_securitytoken_error(ajax)
{
    console.log("Securitytoken Error");
    if (++securitytoken_errors == 3)
    {
        do_securitytoken_replacement('guest');
    }
}

// 1 hour = 3,600,000ms
var securitytoken_timeout = window.setTimeout('replace_securitytoken()', 3600000);
var securitytoken_errors = 0;

function replace_securitytoken()
{
    window.clearTimeout(securitytoken_timeout);
    if (AJAX_Compatible && SECURITYTOKEN != '' && SECURITYTOKEN != 'guest')
    {
        securitytoken_timeout = window.setTimeout('replace_securitytoken()', 3600000);
        YAHOO.util.Connect.asyncRequest("POST", "ajax.php", {
            success: handle_securitytoken_response,
            failure: handle_securitytoken_error,
            timeout: vB_Default_Timeout
        }, SESSIONURL + "securitytoken=" + SECURITYTOKEN + "&do=securitytoken");
    }
}

// #############################################################################
// Initialize a Comment

/**
* This function runs all the necessary Javascript code on a Comment
* after it has been loaded via AJAX. Don't use this method before a
* complete page load or you'll have problems.
*
* @param    object    Object containing postbits
*/
function Comment_Init(obj)
{
    if (typeof obj.id == "undefined")
    {
        return;
    }

    var objectid = obj.id;
    if (isNaN(objectid))
    {
        var match = null;
        if (match = objectid.match(/(\d+)/))
        {
            objectid = match[0];
        }
    }

    if (typeof inlineMod_comment != "undefined")
    {
        im_init(obj, inlineMod_comment);
    }

    if (typeof vB_QuickEditor_Factory != "undefined")
    {
        if (typeof vB_QuickEditor_Factory.controls[objectid] == "undefined")
        {
            vB_QuickEditor_Factory.controls[objectid] = new vB_QuickEditor(objectid, vB_QuickEditor_Factory);
        }
        else
        {
            vB_QuickEditor_Factory.controls[objectid].init();
        }
    }

    if (typeof vB_QuickLoader_Factory != "undefined")
    {
        vB_QuickLoader_Factory.controls[objectid] = new vB_QuickLoader(objectid, vB_QuickLoader_Factory);
    }

    child_img_alt_2_title(obj);
}

// #############################################################################
// Initialize a PostBit

/**
* This function runs all the necessary Javascript code on a PostBit
* after it has been loaded via AJAX. Don't use this method before a
* complete page load or you'll have problems.
*
* @param    object    Object containing postbits
*/
function PostBit_Init(obj, postid)
{
    console.log("PostBit Init: %d", postid);

    if (typeof vBmenu != 'undefined')
    {
        // init profile menu(s)
        var divs = fetch_tags(obj, 'div');
        for (var i = 0; i < divs.length; i++)
        {
            if (divs[i].id && divs[i].id.substr(0, 9) == 'postmenu_')
            {
                vBmenu.register(divs[i].id, true);
            }
        }
    }

    if (typeof vB_QuickEditor != 'undefined')
    {
        // init quick edit controls
        vB_AJAX_QuickEdit_Init(obj);
    }

    if (typeof vB_QuickReply != 'undefined')
    {
        // init quick reply button
        qr_init_buttons(obj);
    }

    if (typeof mq_init != 'undefined')
    {
        // init quick reply button
        mq_init(obj);
    }

    if (typeof vBrep != 'undefined')
    {
        if (typeof postid != 'undefined' && typeof postid != 'null')
        {
            vbrep_register(postid);
        }
    }

    if (typeof inlineMod != 'undefined')
    {
        im_init(obj);
    }

    if (typeof vB_Lightbox != 'undefined')
    {
        init_postbit_lightbox(obj, false, true);
    }

    child_img_alt_2_title(obj);
}

// #############################################################################
// Main vBulletin Javascript Initialization

/**
* This function runs (almost) at the end of script loading on most vBulletin pages
*
* It sets up things like image alt->title tags, turns on the popup menu system etc.
*
* @return    boolean
*/
function vBulletin_init()
{
    // don't bother doing any exciting stuff for WebTV
    if (is_webtv)
    {
        return false;
    }

    // set 'title' tags for image elements
    child_img_alt_2_title(document);

    // finalize popup menus
    if (typeof vBmenu == 'object')
    {
        // close all menus on document click or resize
        if (typeof(YAHOO) != "undefined")
        {
            YAHOO.util.Event.on(document, "click", vbmenu_hide);
            YAHOO.util.Event.on(window, "resize", vbmenu_hide);
        }
        else if (window.attachEvent && !is_saf)
        {
            document.attachEvent('onclick', vbmenu_hide);
            window.attachEvent('onresize', vbmenu_hide);
        }
        else if (document.addEventListener && !is_saf)
        {
            document.addEventListener('click', vbmenu_hide, false);
            window.addEventListener('resize', vbmenu_hide, false);
        }
        else
        {
            window.onclick = vbmenu_hide;
            window.onresize = vbmenu_hide;
        }

        // add popups to pagenav elements
        var pagenavs = fetch_tags(document, 'td');
        for (var n = 0; n < pagenavs.length; n++)
        {
            if (pagenavs[n].hasChildNodes() && pagenavs[n].firstChild.name && pagenavs[n].firstChild.name.indexOf('PageNav') != -1)
            {
                var addr = pagenavs[n].title;
                pagenavs[n].title = '';
                pagenavs[n].innerHTML = '';
                pagenavs[n].id = 'pagenav.' + n;
                var pn = vBmenu.register(pagenavs[n].id);
                pn.addr = addr;

                if (is_saf)
                {
                    pn.controlobj._onclick = pn.controlobj.onclick;
                    pn.controlobj.onclick = vBpagenav.prototype.controlobj_onclick;
                }
            }
        }

        // process the pagenavs popup form
        if (typeof addr != 'undefined')
        {
            fetch_object('pagenav_form').gotopage = vBpagenav.prototype.form_gotopage;
            fetch_object('pagenav_ibtn').onclick = vBpagenav.prototype.ibtn_onclick;
            fetch_object('pagenav_itxt').onkeypress = vBpagenav.prototype.itxt_onkeypress;
        }

        // activate the menu system
        vBmenu.activate(true);
    }

    // the new init system
    vBulletin.init();

    return true;
}

// #############################################################################
// vBulletin Javascript Framework

/**
* General class for handling custom events and custom controls
*/
function vBulletin_Framework()
{
    /**
    * @var    array    Array of elements to be passed to control init functions
    * @var    array    Array of AJAX load/save URLs
    * @var    array    Array of YUI custom events
    * @var    date    Current time
    */
    this.elements = new Array();
    this.ajaxurls = new Array();
    this.events = new Array();
    this.time = new Date();

    // custom event to fire during class init
    this.add_event("systemInit");
}

/**
* Initializes the object - usually called at end of footer template
*/
vBulletin_Framework.prototype.init = function()
{
    console.info("Firing System Init");
    this.events.systemInit.fire();
}

/**
* Emulates OOP class extension
*
* @param    object    Extended class
* @param    object    Base class
*/
vBulletin_Framework.prototype.extend = function(subClass, baseClass)
{
   function inheritance() {}
   inheritance.prototype = baseClass.prototype;

   subClass.prototype = new inheritance();
   subClass.prototype.constructor = subClass;
   subClass.baseConstructor = baseClass;
   subClass.superClass = baseClass.prototype;
}

/**
* Registers a custom control for later initialization
* Arguments 1-n are stored for later use
*
* @param    string    Control type (vB_DatePicker etc.)
* @param    string    HTML element ID
*/
vBulletin_Framework.prototype.register_control = function(controltype, element)
{
    var args = new Array();
    for (var i = 1; i < arguments.length; i++)
    {
        args.push(arguments[i]);
    }
    if (!this.elements[controltype])
    {
        console.info("Creating array vBulletin.elements[\"%s\"]", controltype);
        this.elements[controltype] = new Array();
    }
    var x = this.elements[controltype].push(args);
    console.log("vBulletin.elements[\"%s\"][%d] = %s", controltype, x-1, args.join(", "));
}

/**
* Registers AJAX load/save urls for a control
*
* @param    Fetch URL
* @param    Save URL
* @param    List of elements to which these URLs apply
*/
vBulletin_Framework.prototype.register_ajax_urls = function(fetch, save, elements)
{
    fetch = fetch.split("?"); fetch[1] = SESSIONURL + "securitytoken=" + SECURITYTOKEN + "&ajax=1&" + fetch[1].replace(/\{(\d+)(:\w+)?\}/gi, '%$1$s');
    save  =  save.split("?");  save[1] = SESSIONURL + "securitytoken=" + SECURITYTOKEN + "&ajax=1&" + save[1].replace(/\{(\d+)(:\w+)?\}/gi, '%$1$s');

    console.log("Register AJAX URLs for %s", elements);

    for (var i = 0; i < elements.length; i++)
    {
        this.ajaxurls[elements[i]] = new Array(fetch, save);
    }
}

/**
* Register a custom event
*
* @param    string    Event name
*/
vBulletin_Framework.prototype.add_event = function(eventname)
{
    this.events[eventname] = new YAHOO.util.CustomEvent(eventname);
}

/**
* BC: Pass console calls to Firebug if it's available
*/
//vBulletin_Framework.prototype.console = console.log;
vBulletin_Framework.prototype.console = function()
{
    if (window.console || console.firebug)
    {
        var args = new Array();
        for (var i = 0; i < arguments.length; i++)
        {
            args[args.length] = arguments[i];
        }

        try
        {
            eval("console.log('" + args.join("','") + "');");
        }
        catch(e) {}
    }
}

// #############################################################################

// initialize the PHP function emulator
var PHP = new vB_PHP_Emulator();

// Create an instance of the vBulletin Framework object
var vBulletin = new vBulletin_Framework();

vBulletin.events.systemInit.subscribe(function()
{
    YAHOO.util.Event.on(window, "resize", clear_viewport_info);
    YAHOO.util.Event.on(window, "scroll", clear_viewport_info)
});

// #############################################################################
// Functions for dismissing dismissible notices
function handle_dismiss_notice_error(ajax)
{
    if (ajax.argument)
    {
        var nodeDismissID = YAHOO.util.Dom.get("dismiss_notice_hidden");
        nodeDismissID.value = ajax.argument;
        var nodeForm = YAHOO.util.Dom.get("notices");
        nodeForm.submit();
    }
}

function handle_dismiss_notice_ajax(ajax)
{
    if (ajax.responseXML && ajax.responseXML.getElementsByTagName("dismissed").length)
    {
        var dismissed = ajax.responseXML.getElementsByTagName("dismissed")[0].firstChild.nodeValue;
        var nodeNotice = YAHOO.util.Dom.get("navbar_notice_" + dismissed);
        if (nodeNotice != null)
        {
            nodeNotice.style.display = "none";
            var nodeNotices = YAHOO.util.Dom.getElementsByClassName("navbar_notice", "", YAHOO.util.Dom.get("notices"));
            var showing = 0;
            for (i = 0; i < nodeNotices.length; i++)
            {
                if (nodeNotices[i].style.display != "none")
                {
                    showing++;
                }
            }
            if (showing == 0)
            {
                var nodeNoticeTable = YAHOO.util.Dom.get("notices");
                if (nodeNoticeTable != null)
                {
                    // hide the table if no more notices left
                    nodeNoticeTable.style.display = "none";
                }
            }
        }
    }
    else
    {
        handle_dismiss_notice_error(ajax);
    }
}

/**
* @param    int    integer value of noticeid
*
* returns false, to disable link for js compatible browsers, when AJAX enabled or browser support it
* returns true, to actually submit the form, when AJAX disabled or browser does not support it
*/
function dismiss_notice(noticeid)
{
    if (AJAX_Compatible)
    {
        var ajax_req = YAHOO.util.Connect.asyncRequest("POST", "ajax.php?do=dismissnotice", {
            success: handle_dismiss_notice_ajax,
            failure: handle_dismiss_notice_error,
            timeout: vB_Default_Timeout,
            argument: noticeid
        }, SESSIONURL + "securitytoken=" + SECURITYTOKEN + "&do=dismissnotice&noticeid=" + PHP.urlencode(noticeid));
        return false;
    }
    return true;
}

/*======================================================================*\
|| ####################################################################
|| # NulleD By - FintMax
|| # CVS: $RCSfile$ - $Revision: 39862 $
|| ####################################################################
\*======================================================================*/
