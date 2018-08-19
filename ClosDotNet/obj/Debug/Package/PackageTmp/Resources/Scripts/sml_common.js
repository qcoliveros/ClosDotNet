/**
 * Copyright Integro Technologies Pte Ltd

 *
 * @author $Author: cctay $<br>
 * @version $Revision: 24320 $
 * @since $Date: 2015-07-09 16:18:55 +0800 (Thu, 09 Jul 2015) $
 * Tag: $Name:  $
 */
 

// ================= Form Getter & Setter =============
var globalActionFormObj;

var loadingImageCount = 0;

var g_LastURL = "";

var lastPopupCount = 0;
var lastPopupObj = null;
var lastPopupModule = "";

var useBlockUI = true;	//this requires jquery.blockUI.js and img with id="spinLoading"
var useBlockUIcolor = true;	//this means to use default color in showSilkScreen()

var silkScreenHideSecond = 360; // default time in second for hide the silk screen after being shown

//--------------------------------------------------------------------------------------
//Browser detect routine starts here
//
var BrowserDetect = {
	init: function () {
		this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
		this.version = this.searchVersion(navigator.userAgent)
			|| this.searchVersion(navigator.appVersion)
			|| "an unknown version";

		var trident = !!navigator.userAgent.match(/Trident\/7.0/);
		var net = !!navigator.userAgent.match(/.NET4.0E/);
		var IE11 = trident && net;
		if (IE11 == true) {
			this.browser = "Explorer";
		}

		this.OS = this.searchString(this.dataOS) || "an unknown OS";
	},
	searchString: function (data) {
		for (var i=0;i<data.length;i++)	{
			var dataString = data[i].string;
			var dataProp = data[i].prop;
			this.versionSearchString = data[i].versionSearch || data[i].identity;
			if (dataString) {
				if (dataString.indexOf(data[i].subString) != -1)
					return data[i].identity;
			}
			else if (dataProp)
				return data[i].identity;
		}
	},
	searchVersion: function (dataString) {
		var index = dataString.indexOf(this.versionSearchString);
		if (index == -1) return;
		return parseFloat(dataString.substring(index+this.versionSearchString.length+1));
	},
	dataBrowser: [
		{
			string: navigator.userAgent,
			subString: "Chrome",
			identity: "Chrome"
		},
		{ 	string: navigator.userAgent,
			subString: "OmniWeb",
			versionSearch: "OmniWeb/",
			identity: "OmniWeb"
		},
		{
			string: navigator.vendor,
			subString: "Apple",
			identity: "Safari",
			versionSearch: "Version"
		},
		{
			prop: window.opera,
			identity: "Opera",
			versionSearch: "Version"
		},
		{
			string: navigator.vendor,
			subString: "iCab",
			identity: "iCab"
		},
		{
			string: navigator.vendor,
			subString: "KDE",
			identity: "Konqueror"
		},
		{
			string: navigator.userAgent,
			subString: "Firefox",
			identity: "Firefox"
		},
		{
			string: navigator.vendor,
			subString: "Camino",
			identity: "Camino"
		},
		{		// for newer Netscapes (6+)
			string: navigator.userAgent,
			subString: "Netscape",
			identity: "Netscape"
		},
		{
			string: navigator.userAgent,
			subString: "MSIE",
			identity: "Explorer",
			versionSearch: "MSIE"
		},
		{
			string: navigator.userAgent,
			subString: "Gecko",
			identity: "Mozilla",
			versionSearch: "rv"
		},
		{ 		// for older Netscapes (4-)
			string: navigator.userAgent,
			subString: "Mozilla",
			identity: "Netscape",
			versionSearch: "Mozilla"
		}
	],
	dataOS : [
		{
			string: navigator.platform,
			subString: "Win",
			identity: "Windows"
		},
		{
			string: navigator.platform,
			subString: "Mac",
			identity: "Mac"
		},
		{
			   string: navigator.userAgent,
			   subString: "iPhone",
			   identity: "iPhone/iPod"
	    },
		{
			   string: navigator.userAgent,
			   subString: "iPad",
			   identity: "iPad"
	    },
		{
			string: navigator.platform,
			subString: "Linux",
			identity: "Linux"
		}
	]

};

BrowserDetect.init();

//--------------------------------------------------------------------------------------
//Browser detect routine ends here


//--------------------------------------------------------------------------------------
//Cookie routine starts here
//
function getCookie(cookieName){var cookieJar=document.cookie.split("; ");for(var x=0;x<cookieJar.length;x++){var oneCookie=cookieJar[x].split("=");if(oneCookie[0]==escape(cookieName)){return unescape(oneCookie[1]);}}return null;}
function setCookie(cookieName,cookieValue,lifeTime,path,domain,isSecure){if(!cookieName){return false;}if(lifeTime=="delete"){lifeTime=-10;}document.cookie=escape(cookieName)+"="+escape(cookieValue)+(lifeTime?";expires="+(new Date((new Date()).getTime()+(1000*lifeTime))).toGMTString():"")+(path?";path="+path:"")+(domain?";domain="+domain:"")+(isSecure?";secure":"");if(lifeTime<0){if(typeof(getCookie(cookieName))=="string"){return false;}return true;}if(typeof(getCookie(cookieName))=="string"){return true;}return false;}
function deleteCookie(cookieName) {setCookie(cookieName, "", "delete");}
//--------------------------------------------------------------------------------------
//Cookie routine ends here
//

calculateWindowScroll = function() {
var w = window;
var T, L, W, H;

	L = window.pageXOffset || document.body.scrollLeft;
	T = window.pageYOffset || document.body.scrollTop;

	if (window.ie) 
		W = Math.max(document.body.offsetWidth, document.body.scrollWidth);
	else if (window.khtml) 
		W = document.body.scrollWidth;
	else 
		W = document.body.scrollWidth;
	  
	if (window.ie) 
		H = Math.max(document.body.offsetHeight, document.body.scrollHeight);
	else if (window.khtml) 
		H = document.body.scrollHeight;
	else
		H = document.body.scrollHeight;

	return { top: T, left: L, width: W, height: H };
};


calculatePageSize = function() {
var xScroll, yScroll;

    if (window.innerHeight && window.scrollMaxY) {  
		xScroll = document.body.scrollWidth;
		yScroll = window.innerHeight + window.scrollMaxY;
    } else if (document.body.scrollHeight > document.body.offsetHeight){ // all but Explorer Mac
		xScroll = document.body.scrollWidth;
		yScroll = document.body.scrollHeight;
    } else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
		xScroll = document.body.offsetWidth;
		yScroll = document.body.offsetHeight;
    }

var windowWidth, windowHeight;

    if (self.innerHeight) {  // all except Explorer
		windowWidth = self.innerWidth;
		windowHeight = self.innerHeight;
    } else if (document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode
		windowWidth = document.documentElement.clientWidth;
		windowHeight = document.documentElement.clientHeight;
    } else if (document.body) { // other Explorers
		windowWidth = document.body.clientWidth;
		windowHeight = document.body.clientHeight;
    }

var pageHeight, pageWidth;

    // for small pages with total height less then height of the viewport
    if(yScroll < windowHeight){
		pageHeight = windowHeight;
    } else { 
		pageHeight = yScroll;
    }

    // for small pages with total width less then width of the viewport
    if(xScroll < windowWidth){  
		pageWidth = windowWidth;
    } else {
		pageWidth = xScroll;
    }

    return {pageWidth: pageWidth, pageHeight: pageHeight, windowWidth: windowWidth, windowHeight: windowHeight};
};


function hasScrollBar(el, direction) { 
    direction = (direction === 'vertical') ? 'scrollTop' : 'scrollLeft'; 
    var result = !! el[direction]; 
 
    if (!result) { 
        el[direction] = 1; 
        result = !!el[direction]; 
        el[direction] = 0; 
    } 
    return result; 
} 

//alert('vertical? ' + hasScrollBar(document.body, 'vertical')); 
//alert('horizontal? ' + hasScrollBar(document.body, 'horizontal')); 

function getDocumentHeight() {
    return Math.max(
        Math.max(document.body.scrollHeight, document.documentElement.scrollHeight),
        Math.max(document.body.offsetHeight, document.documentElement.offsetHeight),
        Math.max(document.body.clientHeight, document.documentElement.clientHeight)
    );
}

function scrollToElem(id) {
	if ($(id).offset() != null) {
		var shiftUp = 0;
		var bannerPanel = $('.cbp-af-header-shrink');
		if (bannerPanel.length > 0) {
			shiftUp = bannerPanel.height();
			if (isNaN(shiftUp) == true) shiftUp = 50;
			shiftUp = shiftUp + 20;
			//js_DisplayPrompt("Header", "Banner exists " + bannerPanel.css("height") + " shiftUp: " + shiftUp);
		}

		$('body,html').animate({
				scrollTop: ($(id).offset().top - shiftUp)
			}, 500);
	}
}

//--------------------------------------------------------------------------------------
//Ajax routine starts here
//

//this is used for onbeforeunload counter
var unloadingNow = false;
var unloadingCount = 0;

//alert('create Ajax object');
var ajaxpack = new Object()

//ajaxpack.ajaxobj = createAjaxObj()
ajaxpack.ajaxobj = null;
ajaxpack.filetype = "txt"
ajaxpack.addrandomnumber = 1 //Set to 1 or 0. See documentation.
ajaxpack.countIndex = 1

var uID = ""

function createAjaxObj() {
	var httprequest = false
	if (window.XMLHttpRequest) { // if Mozilla, Safari etc
		httprequest=new XMLHttpRequest()
		if (httprequest.overrideMimeType)
			httprequest.overrideMimeType('text/xml')
	}
	else if (window.ActiveXObject) { // if IE
		try {
			httprequest = new ActiveXObject("Msxml2.XMLHTTP");
		} catch (e) {
			try {
				httprequest = new ActiveXObject("Microsoft.XMLHTTP");
			}
			catch (e) { 
				alert("Ajax creation error")
			}
		}
	}
	return httprequest
}


ajaxpack.getAjaxRequest=function(url, parameters, callbackfunc, filetype, writeDiv, countIndex) {

	if (unloadingNow == true)
		return;

	ajaxpack.ajaxobj = createAjaxObj() //recreate ajax object to defeat cache problem in IE

	if (this.addrandomnumber == 1) //Further defeat caching problem in IE?
		var parameters = parameters + "&ajaxcachebust=" + new Date().getTime()

	if (this.ajaxobj) {
		this.writeDiv = writeDiv
		this.filetype = filetype
		this.countIndex = countIndex
		this.ajaxobj.onreadystatechange = callbackfunc
		this.ajaxobj.open('GET', url+"?"+parameters, true)

		this.ajaxobj.send(null)
	}

}

ajaxpack.postAjaxRequestNormal=function(url, parameters, callbackfunc, filetype, writeDiv, countIndex) {

	ajaxpack.ajaxobj = createAjaxObj() //recreate ajax object to defeat cache problem in IE

	if (this.ajaxobj) {
		this.writeDiv = writeDiv
		this.filetype = filetype
		this.countIndex = countIndex
		this.ajaxobj.onreadystatechange = callbackfunc;
		this.ajaxobj.open('POST', url, true);
		this.ajaxobj.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
		this.ajaxobj.setRequestHeader("Content-length", parameters.length);
		//this.ajaxobj.setRequestHeader("Connection", "close");	//bypass ie6 bug

		this.ajaxobj.send(parameters);
	}
}

function processGetPostNormal() {

var myajax=ajaxpack.ajaxobj
var myfiletype=ajaxpack.filetype
var mycountindex=ajaxpack.countIndex

	if (myajax.readyState == 4) { //if request of file completed
		//alert(myajax.status)
		//alert(myajax.responseText)
		if (myajax.status==200 || window.location.href.indexOf("http")==-1) { //if request was successful or running script locally

			if (ajaxpack.writeDiv != null) {
				if (ajaxpack.writeDiv == "broadcast")
				{
					if (myajax.responseText.indexOf("broadcast item found") > -1)
					{
						//jsDebug("broadcast: (" + trim(myajax.responseText, " ") + ")  location: " + myajax.responseText.indexOf("broadcast"));
						if (showBroadcastMessage) {
							//this is generated by \template\check_broadcast.jsp which is triggered from iconheader.jsp and reload.jsp
							showBroadcastMessage();
						}
					}
				}
				else
				{
					document.getElementById(ajaxpack.writeDiv).innerHTML = myajax.responseText
				}
			}
			else
			{
				countDownTime = parseInt(countDownInterval, 10) - 1;
			}
		} //end for - //if request was successful or running script locally
	} //end for - if request of file completed
}


//--------------------------------------------------------------------------------------
//Ajax routine ends here
//


//--------------------------------------------------------------------------------------
//Session timeout routine starts here
//

//configure refresh interval (in seconds)
var actualTitle = document.title + " "
var countDownInterval = -999; //this is the default value to indicate first time access
var cookieSupport = false;
var counter;

if (cookieSupport) {
	if (getCookie("timeout"))
            countDownInterval = getCookie("timeout");
	else
	  if (countDownInterval != -999)
		alert("Session cookie support not available. Please check browser settings.");
}

var useAlertBox = false;
var countDownTime = parseInt(countDownInterval, 10);

//should be triggered from JSP to put in actual web timeout
//initCountCookie("SMARTLENDER", 30*60);
//countDownSession();	//start session countdown

function initCountCookie(myID, sessionTimeoutValue) {
	uID = myID;
	countDownInterval = sessionTimeoutValue;
	countDownTime = parseInt(countDownInterval, 10);
}

function restartCountCookie() {
	countDownTime = parseInt(countDownInterval, 10) - 1;
}

function countDownSession() {
	timediff = parseInt(countDownTime, 10);
	//if (isNaN(timediff)) alert("count value is invalid");

	oneMinute = 60 //minute unit in seconds
	oneHour = 60*60 //hour unit in seconds
	oneDay = 60*60*24 //day unit in seconds
	dayfield = Math.floor(timediff/oneDay)
	hourfield = Math.floor((timediff-dayfield*oneDay)/oneHour)
	minutefield = Math.floor((timediff-dayfield*oneDay-hourfield*oneHour)/oneMinute)
	secondfield = Math.floor((timediff-dayfield*oneDay-hourfield*oneHour-minutefield*oneMinute))

	var countDownChoice = false;
	var cdDiv = "countDownDiv";

	//check for broadcast message
	if (countDownTime % 300 == 0)
	{
	  //ajaxpack.postAjaxRequestNormal(getContextPath() + '/reload.jsp', '', processGetPostNormal, 'txt', "broadcast", 1);
	  //countDownTime = parseInt(countDownInterval) - 1;
	}

	if ((countDownTime == 121) || (countDownTime == 301) || (countDownTime == 601))
	{
		if (typeof $.gritter != 'undefined')
		{
			var unique_id = $.gritter.add({
				// (string | mandatory) the heading of the notification
				title: ('Session is expiring in another ' + Math.floor(countDownTime/60) + ' minutes'),
				// (string | mandatory) the text inside the notification
				text: 'It is noticed that you have not been doing anything on the page for a period of time, kindly note that the server shall time out your session if this continues',
				// (string | optional) the image to display on the left
				image: './images/session-timeout.png',
				// (bool | optional) if you want it to fade out on its own or just sit there
				sticky: true,
				// (int | optional) the time you want it to be alive for before fading out
				time: '',
				// (string | optional) the class name you want to apply to that specific message
				class_name: 'my-sticky-class'
			});
		}

	}
	
	if ((countDownTime == 59))
	{
        var currentTime = new Date();
        var startPoint = currentTime.getTime();
		
		if (useAlertBox == true)
		{
			countDownChoice = confirm("Your session will be terminated soon in " + (minutefield+1) + " minute(s).\nPlease respond now to avoid being logout.\n\nClick OK button to continue now.");
			var futureTime = new Date();
			var endPoint = futureTime.getTime();
			countDownTime = countDownTime - ((endPoint - startPoint) / 1000);
			if (countDownTime <= 0) {
				//since user waited for so long, no need to refresh even if OKAY is clicked
				countDownChoice = false;
			}
		} 
		else 
		{
			var d = document.getElementById(cdDiv);
			if (!d) { d = document.createElement("div"); document.body.insertBefore(d, document.body.firstChild) };
			d.id = cdDiv;

			d.innerHTML = 	'<div style="line-height:20px;padding:5px;font-size:12px;font-family:Verdana,Arial,Helvetica;color:#000000;' +
							'position:absolute;width:100%;top:0;left:0;opacity:0.9;filter:alpha(opacity=90);z-index:90000;background:#ffffc0;border: 2px solid #eeee50;margin-bottom:2px">' +
							'<center>' +
							'<br/><br/>' +
							'<b>Your session will expire in another ' + (minutefield+1) + ' minute(s).' + '<br/>' +
							'Click OK button to extend it now.' + '<br/>' +
							'</b>' + '<br/>' +
							'<span style="cursor:pointer"><img src="./images/button/okMouseOut.gif" border="0"></span><br/><br/><br/>'+
							'</center>' +
							'</div>';
			window.scrollTo(0, 0);
			d.getElementsByTagName('span')[0].onclick = function() {
				ajaxpack.postAjaxRequestNormal(getContextPath() + '/reload.jsp', '', processGetPostNormal, 'txt', null, 1);

				var yDiv = document.getElementById(cdDiv);
				if (yDiv != null)
					document.body.removeChild(yDiv);
			}
		}
	}

	countDownTime--;

	if (countDownChoice) {
		ajaxpack.postAjaxRequestNormal(getContextPath() + '/reload.jsp', '', processGetPostNormal, 'txt', null, 1);
		countDownTime = parseInt(countDownInterval, 10) - 1;
	}

	if (countDownTime <= 0) {
		countDownTime = parseInt(countDownInterval, 10);
		clearTimeout(counter);

		top.document.title = actualTitle + " - [ Session has expired, you need to login again ]"

		if (useAlertBox == false)
		{
			var d = document.getElementById(cdDiv);
			if (!d) { d = document.createElement("div"); document.body.insertBefore(d, document.body.firstChild) };
			d.id = cdDiv;
			d.innerHTML = 	'<div style="line-height:20px;padding:5px;font-size:12px;font-family:Verdana,Arial,Helvetica;color:#ff0000;' +
							'position:absolute;width:100%;top:0;left:0;opacity:0.9;filter:alpha(opacity=90);z-index:90000;background:#ffc0c0;border: 2px solid #ee5050;margin-bottom:2px">' +
							'<center>' + '<br/><br/><br/>' +
							'<b>Your session has expired, click <a href="' + getContextPath() + '">here</a> to login again.</b>' + '<br/><br/><br/><br/>' +
							'</center>' +
							'</div>';
			window.scrollTo(0, 0);
		}

		//auto logout
		//window.location.href =  getContextPath() + "/Logout.do?event=logout";
		return;
	}

	if (unloadingNow == true) {
		unloadingCount++;

		if (unloadingCount == silkScreenHideSecond) {
			hideSilkScreen();
		}
		top.document.title = "Waiting for server to respond: " + unloadingCount + " second(s)";
	}
	else {
		if (minutefield > 0) 
			top.document.title = actualTitle + " - Time left: " + minutefield + " minute(s) " + secondfield + " second(s)"
		else
			top.document.title = actualTitle + " - Time left: " + secondfield + " second(s)"
			
		unloadingCount = 0;
	}

	counter = setTimeout("countDownSession()", 1000);
}

//--------------------------------------------------------------------------------------
//Session timeout routine ends here
//


function imposeMaxLength(object, maxLen) {
	if (object.value.length > maxLen) {
        object.value = object.value.substring(0, maxLen);
	}
}

function formatNumber(value, decimalPlace){
	var power = Math.pow(10, parseInt(decimalPlace));
	value = parseFloat(value) * power
	value = "" + value;
	var wholeNumber = value.substr(0, value.length-decimalPlace);
	var decimalNumber = value.substr(value.length-decimalPlace);
	return wholeNumber + "." + decimalNumber;
}


function doProgress(msg){
	var obj = document.getElementById("loadingDiv");
	if ( obj != null ){
		obj.style.display="block";
		var mod = loadingImageCount%10;
		if (mod==0) obj.innerHTML ="<span style='color:#800000'>&lt; "+msg+" &gt;</span>";
		else obj.innerHTML =  "<span style='color:#8" + mod + "" + mod + "" + mod + "99;'>&lt;</span>" + 
			obj.innerHTML + "<span style='color:#8" + mod + "" + mod + "" + mod + "99;'>&gt;</span>";	
		loadingImageCount ++;
	}
	var callFunction = "doProgress('"+msg+"')";
	setTimeout(callFunction, 500);
}

function doStartFormSubmission(msg)
{
	if(msg)
		;
	else
		msg = "Loading";
	
	var callFunction = "doProgress('"+msg+"')";
 	setTimeout(callFunction, 500);
 	document.forms[0].style.display="none";
}

function setActionForm(objForm){
	globalActionFormObj=objForm;
}

function getActionForm(){
	if (globalActionFormObj) return globalActionFormObj;
	return document.forms[1];
}


function viewUploadedAttachment(selectIdx, sessionListKey, servletUri){
	var objForm = document.forms["viewAttachmentForm"];
	if (!objForm) {
		alert("Not able to found form.name [viewAttachmentForm] to perform download... ");
		return;
	}

	//turn off silkscreen
	if (typeof removeOnStopEvent == "function") {
		removeOnStopEvent();
	}

	objForm.action = servletUri;
	objForm.sessionListKey.value = sessionListKey;
	objForm.selectIdx.value=selectIdx;
	objForm.submit();
 }
   
// ============= Common Form Actions ===================== //
function actionPerform(eventId)
{
     var objForm = getActionForm();
     if(eventId!='') objForm.event.value = eventId;
     objForm.submit();
}

function actionPerformWithUri(actionUri, eventId)
{
    var objForm 	= getActionForm();
    objForm.action  = actionUri;
	actionPerform(eventId);
}

function actionGoPageIndex(idx, eventId)
{
	var objForm = getActionForm();
    objForm.startIndex.value  = idx;
	actionPerform(eventId);
}
    
function actionEditContent(idx, actionUri, eventId)
{
    var objForm = getActionForm();
    objForm.selectIdx.value   = idx;
    actionPerformWithUri(actionUri, eventId);
}

// ================ Common Form Function =================//
function transferAllOption(source, target){
	if (!source || !source.options) source = document.getElementById(source);
	if (!target || !target.options) target = document.getElementById(target);
	for(var i=0;i<source.options.length;i++){
		target.options.add(new Option(source.options[i].text,source.options[i].value));
		target.options[target.options.length-1].selected="selected"
		source.options[i]=null;
		i--;
	}
}

// Transferring selected <option>s between two multi-<select>s.
// Param expect is source <select> object and target <select>
function transferSelectedOption(source, target){
	if (!source || !source.options) source = document.getElementById(source);
	if (!target || !target.options) target = document.getElementById(target);
	for(var i=0;i<source.options.length;i++){
		if (source.options[i].selected && !source.options[i].disabled) {
			target.options.add(new Option(source.options[i].text,source.options[i].value));
			target.options[target.options.length-1].selected="selected"
			source.options[i]=null;
			i--;
		}
	}
}

//Transferring selected <option>s between two multi-<select>s.
//Param expect is source <select> object and target <select>
function transferSelectedCurrecyOption(source, target,selected){
	if (!source || !source.options) source = document.getElementById(source);
	if (!target || !target.options) target = document.getElementById(target);
	for(var i=0;i<source.options.length;i++){
		if (source.options[i].selected && !source.options[i].disabled) {
			target.options.add(new Option(source.options[i].text,source.options[i].value));
			target.options[target.options.length-1].selected="selected"
			source.options[i]=null;
			i--;
		}
	}
	
	if (!selected || !selected.options) selected = document.getElementById(selected);
	for (var i=0;i<selected.options.length;i++) {
		selected.options[i].selected = true;
	}
}

//	Remove the unselected option(s) from target
//  Remove the source option(s) where target option(s) is selected
function syncSelectedOption(source, target){
	if (!source || !source.options) source = document.getElementById(source);
	if (!target || !target.options) target = document.getElementById(target);
	
	if (source && target){
		for(var idx = target.options.length-1; idx>=0; idx--)
		{
			if(!target.options[idx].selected)
			{
				target.options[idx] = null;
			}
			else
			{
				source.options[idx] = null;
			}
		}
	}
}

function getSelectionText(objSelect){
	if (!objSelect) return;
	var option = objSelect.options[objSelect.selectedIndex];
	if(option) return option.text
	return "";
}


// Sets the selected Option to the label matching the "textValue" string
function defaultSelectByText(objSelect, textValue){
	for(var i=0;objSelect && i<objSelect.options.length;i++){
		var option = objSelect.options[i];
		if(option.text == textValue){
			option.selected = true;
			return;
		}
	}
}

// ================= Common Util Functions ==================//
function toggleDisplay(objId){
	var obj = document.getElementById(objId);
	var objImg = document.getElementById(objId + "Img"); // Extension to toggle icon
	if(!obj) return;
	if(obj.style.display=="none") {
		obj.style.display="block";
		if(objImg) objImg.src=objImg.src.replace("expand.","collapse.");
	}
	else {
		obj.style.display="none";
		if(objImg) objImg.src=objImg.src.replace("collapse.","expand.");
	}
}

function focusObject(id){
	var obj = document.getElementById(id);
	if (obj) obj.focus();
}

function hideObject(id){
	var obj = document.getElementById(id);
	if (obj) obj.style.display = "none";
}
function showObject(id){
	var obj = document.getElementById(id);
	if (obj) {
		if (obj.tagName === 'tr' || obj.tagName === 'TR') {
			obj.style.display = '';
		}
		else {
			obj.style.display = "block";
		}
	}
}


function selectAllOptions(obj){
	if(!obj) return;
	if(!obj.length) {obj.selected = true; return;}
	for(var i=0;i<obj.length;i++) obj[i].selected = true;
}

// unselect all the radio buttons given the tag name
function unselectAllRadioBtnOptions(obj){
	if (!obj) {
		return;
	}
	
	if (!obj.length) {
		obj.checked = false; return;
	}

	for (var i=0;i<obj.length;i++) {
		if (obj[i].type && obj[i].type == 'hidden') {
			obj[i].value = '';
		} else {
			obj[i].checked = false;
		}
	}
}

// unselect all the radio buttons given the tag name excluding 1 value
function unselectRadioBtnOptions(groupName1, groupName2, groupName3, isPricing)
{
    var radioGroup = document.getElementsByName(groupName1);
	
	if(radioGroup)
    {
        for(var j=0;j<radioGroup.length;j++) 
        {
        	if ((isPricing == 'N' && 
        		radioGroup[j].value != 'N') ||
        		(isPricing == 'Y' && 
        		radioGroup[j].value != 'S'))
        	{
        		radioGroup[j].checked = false;
        	}
        }
    }
    if ((isPricing == 'N' &&
    	document.getElementById(groupName3) && 
    	document.getElementById(groupName3).value &&
    	document.getElementById(groupName3).value == 'L') ||
    	isPricing == 'Y')
    {
    	unselectAllRadioBtnOptions(document.getElementsByName(groupName2));
    	
    	hideObject("tierByAmount");
        hideObject("tierByTenor");
        hideObject("tierByQuantum");
        hideObject("tierByCurrency");
        hideObject("tierByUtilization");
       	hideObject('addMultiTierBtn');
       	hideObject('caTierButtons');
    }
}

// select the parent radio button when one of the child radio buttons is selected 
function selectParentRadioBtn(groupName, isPricing)
{
    var radioGroup = document.getElementsByName(groupName);
	
    if(radioGroup)
    {
        for(var j=0;j<radioGroup.length;j++) 
        {
        	if ((isPricing == 'N' && 
        		radioGroup[j].value == 'T') ||
        		(isPricing == 'Y' && 
        		radioGroup[j].value == 'M'))
        	{
        		radioGroup[j].checked = true;
        	}
        	else 
        	{
        		radioGroup[j].checked = false;
        	}
        }
    }
}

// checks if atleast 1 checkbox is selected in the group of checkboxes(mostly used during remove operation)

function isAnyChkBoxSelected(objName)
{
	if ($('#' + objName).length > 0) {
		return $('#' + objName + ':checked').length > 0;
	}
	else if ($('input[name="'+objName+'"]').length > 0) {
		return $('input[name="'+objName+'"]:checked').length > 0;
	}
		
}  

// Enable entire radio button group by name
function enableRadioButtonGroup(groupName)
{
    var radioGroup = document.getElementsByName(groupName);

    if(radioGroup)
    {
        for(var j=0;j<radioGroup.length;j++) {
        	radioGroup[j].disabled = false;
        }
    }
}

//Get selected item value in a radio button group
function getSelectedRadioButtonValue(groupName)
{
    var radioGroup = document.getElementsByName(groupName);

    if(radioGroup)
    {
        for(var j=0;j<radioGroup.length;j++) 
        {
        	if (radioGroup[j].checked == true)
        	{
        		return radioGroup[j].value;
        	}	
        }
    }
    return null;
}

/* converts an array to a String of  "-" separated values of the array.
	Takes the objName(usually struts multibox) and NOT the id 
 */

function createStrFromArray(objName)
{
	var values = [];
	if ($('#' + objName + ':checked').length > 0) {
		$.each($('#' + objName + ':checked'), function (i,v) {
			values.push(v.value);
		});
	}
	else if ($('input[name="'+objName+'"]:checked').length > 0) {
		$.each($('input[name="'+objName+'"]:checked'), function (i,v) {
			values.push(v.value);
		});
	}
	// must do reverse for java.util.List#remove() which will shift items to left after removed
	return values.reverse().join('-');	
}

var _prepareViewModeRunCount = 0;
// =================== Checker View =======================//
//<array to store view section processed 
var objIdArray = new Array();

function prepareViewModeByObjId(objId)
{
	var objForm = document.getElementById(objId);

	if(!objForm)return;
	
	if(_prepareViewModeRunCount!= 0) return;
	for (var i=0;i<objIdArray.length;i++)
	{
		if(objId== objIdArray[i])
			return; //<processed, skip
	}

	objIdArray.push(objId);
	
	prepareViewMode(objForm);
	_prepareViewModeRunCount--;
	hideImages(objForm);
}

var enableDTUViewMode = 1;

var _blankPlaceHolder = "__";
var viewModeOnly = false;

var byPassTextAreaViewMode = false;
var delayCheckerMode = 0;

function prepareCheckerMode(){

	if (byPassTextAreaViewMode == false) {
		if (typeof $.fn.cleditor == 'function') {
			delayCheckerMode++;
			return;
		}
	}

	var objForm = getActionForm();
	enableDTUViewMode = 0;
	if (_prepareViewModeRunCount==0) {
		prepareViewMode(objForm);
		viewModeOnly = true;
		$('.fap-edit').hide();
	} 
	// NOTE:
	// --------------------------------------------
	// we only run this function once per page load.
	// if there is a need to run multiple execution, 
	// consider using prepareViewModeByObjId() for finer grain control;
}

var ua = window.navigator.userAgent;
var msie = ua.indexOf("MSIE")>0 || (!!window.ActiveXObject || "ActiveXObject" in window);

var msieOld = 8;

// IE 7 has the following funny behaviors 
// a) UPPER the tag.	
// b) remove "" for attribute value if there is not space
// c) arranging attribute position
function prepareViewMode(objForm){	
	_prepareViewModeRunCount++;
	
	if(!objForm) return;
	
	var ignoreList = "hidden|button"
	var elementList = objForm.getElementsByTagName("TD");
	var offset = 100;
	var checkBoxType 	= 'type="checkbox"' ;
	var radioType 		= 'type="radio"' ;
	var textType 		= 'type="text"' ;
	var textareaType 	= '<textarea' ;
	var remarkType 		= 'name="submissionRemarks"' ;
	var selectType 		= '<select' ;
	var cellType 		= '<td' ;
	
	if (msie) {
		if (BrowserDetect.version <= msieOld) {
			textType = "<INPUT";
			textareaType = textareaType.toUpperCase();
			selectType = selectType.toUpperCase();
			cellType = cellType.toUpperCase();
			checkBoxType = 'type=checkbox' ;
			radioType = 'type=radio' ;
			remarkType 		= 'name=submissionRemarks' ;
		}
	}

	if (byPassTextAreaViewMode == true) {
		textareaType = "<**NoNeedTextArea**>";
	}

	for (var i=0;i<elementList.length;i++){
		var content = "" + elementList[i].innerHTML;
		

		if (elementList[i].className && elementList[i].className.indexOf("makerOnly")>=0){
			elementList[i].style.display="none";
		}
		
		else if (elementList[i].className
            && ((elementList[i].className.indexOf("alwaysEditable") >= 0)
                || (elementList[i].className.indexOf("alwaysEditableInline") >= 0)
                || (elementList[i].className.indexOf("cancel") >= 0)))
		{
			continue;
		}
		else {
			prepareCellContent(0, textType, textareaType, selectType, 
							checkBoxType, radioType,cellType, remarkType, elementList[i]);
		}
	}

	hideFormElements(objForm);
	
	elementList = objForm.getElementsByTagName("TD");
	for (var i=0;i<elementList.length;i++){
		var tdContent;
		if (elementList[i].className && elementList[i].className.indexOf("alwaysEditable")>=0){
			tdContent = elementList[i];
			var tdElementList = tdContent.getElementsByTagName("INPUT");
			for (var j=0;tdElementList && j<tdElementList.length;j++){
				if (tdElementList[j].type && tdElementList[j].type=="checkbox")
					tdElementList[j].style.display="inline";
				else if (tdElementList[j].type && tdElementList[j].type!="hidden")
					tdElementList[j].style.display="block";
			}
			tdElementList = tdContent.getElementsByTagName("SELECT");
			for (var j=0;tdElementList && j<tdElementList.length;j++){
				tdElementList[j].style.display="block";
			}
			tdElementList = tdContent.getElementsByTagName("TEXTAREA");
			for (var j=0;tdElementList && j<tdElementList.length;j++){
				if (tdElementList[j].name && tdElementList[j].name!="submissionRemarks")
					tdElementList[j].style.display="block";
			}
		}

		if (elementList[i].className && elementList[i].className.indexOf("alwaysEditableInLine")>=0){
			tdContent = elementList[i];
			var tdElementList = tdContent.getElementsByTagName("INPUT");
			for (var j=0;tdElementList && j<tdElementList.length;j++){
				if (tdElementList[j].type && tdElementList[j].type=="checkbox")
					tdElementList[j].style.display="inline";
				else if (tdElementList[j].type && tdElementList[j].type!="hidden")
					tdElementList[j].style.display="inline";
			}
			tdElementList = tdContent.getElementsByTagName("SELECT");
			for (var j=0;tdElementList && j<tdElementList.length;j++){
				tdElementList[j].style.display="inline";
			}		
		}		
	}
}

function findFrontHiddenTagString(content)
{
	var i = 0;
	var result = 0;
	var lastresult = 0;
	
	//content = '<input name="proposedLimitCcyStr" type="hidden" value="SGD"><input name="proposedLimitCcyId" type="hidden" value="10662"><input name="proposedLimit" class="amount-field" id="proposedLimit" type="text" size="15" maxLength="25" value="1,000">';
	var startPos, closePos;

	var startStr = "<input";
	if (msie) {
		if (BrowserDetect.version <= msieOld) {
			startStr = "<INPUT";
		}
	}

	var chkStr = 'type="hidden"';
	if (msie) {
		if (BrowserDetect.version <= msieOld) {
			chkStr = "type=hidden";
		}
	}

	while (result != -1)
	{
		startPos = content.indexOf(startStr, lastresult);
		closePos = content.indexOf(">", lastresult);

		result = content.indexOf(chkStr, lastresult);

		if ((result > startPos) && (result < closePos)) {
			lastresult = closePos + 1;	//skip this hidden tag
		}
		else {
			result = -1;
		}
	}

	return (lastresult);
}

function prepareCellContent(startPos, textType, textareaType, selectType, 
							checkBoxType, radioType, cellType, remarkType, obj)
{
    var content         = obj.innerHTML.substring(startPos);
	var textIndex 		= content.indexOf(textType);
	var frontShift		= 0;
	var closePos 		= content.indexOf(">");

	var plainHidden     = findFrontHiddenTagString (content);
	if (plainHidden > 0) {
		content 		= content.substring(plainHidden);
		textIndex 		= content.indexOf(textType);
		frontShift		= plainHidden;
	}

	var textAreaIndex 	= content.indexOf(textareaType);
	var selectIndex 	= content.indexOf(selectType);
	var checkboxIndex 	= content.indexOf(checkBoxType);
	var radioIndex 		= content.indexOf(radioType);
	var cellIndex 		= content.indexOf(cellType);
	
	var orderIndex = new Array(parseInt(selectIndex), parseInt(checkboxIndex), parseInt(radioIndex), parseInt(textIndex), parseInt(textAreaIndex));			
	var actionIndex = new Array("select", "checkbox", "radio", "text", "textarea");
	var nextAction = "";
	var lowestIndex = 1000000;
	var debugStr = "";
	var endTag = ">";

	for(var j=0;j<orderIndex.length;j++){
		if (orderIndex[j]>=0 && orderIndex[j]<lowestIndex){
			lowestIndex = orderIndex[j];
			nextAction =  actionIndex[j];
		}
		debugStr += "\n" + actionIndex[j] + ":" + orderIndex[j];
	}

	if (nextAction!=""){
		var addedContent = "";
		if (nextAction=="checkbox")
			addedContent = extractCheckboxSelectedToCell(checkboxIndex, content, cellType, nextAction, obj);
		else if (nextAction=="radio"){
			addedContent = extractCheckboxSelectedToCell(radioIndex, content, cellType, nextAction, obj);
		}
		else if (nextAction=="textarea"){
			addedContent = extractTextAreaContentToCell(textAreaIndex, content, cellType, remarkType, obj);
			
			if (addedContent) {
				var tempContent = '';
				var maxLength = 100;
				var count = 0;
				while (true)
				{
					if (addedContent.length <= maxLength) {
						tempContent =  tempContent + addedContent ;
						break;
					} else{
					    
						chunk = addedContent.substring(0,maxLength);
						
						addedContent = addedContent.slice(maxLength);
						
						tempContent = tempContent + chunk + '<br>';
						
					}
					count = count + 1;
				} 
			    addedContent = tempContent; 
			    endTag = "</textarea>";
			}
		}
		else if (nextAction=="select"){
			addedContent = extractSelectedOptionToCell(selectIndex, content, cellType, obj);
			endTag = "</select>";	
		}
		else if (nextAction=="text"){
			//content = replaceSpecialValue(textIndex, content, cellType, textType, obj);
			addedContent  = extractTextContentToCell(textIndex, content, cellType, textType, obj);
		}
		
		if (addedContent){
			if (msie) {
				if (BrowserDetect.version <= msieOld) {
					endTag = endTag.toUpperCase();
				}
			}
			lowestIndex = lowestIndex + frontShift;

			var insertIndex;
			insertIndex = content.substring(lowestIndex).indexOf(endTag) + lowestIndex + startPos + endTag.length;
			if (frontShift > 0)
				insertIndex = obj.innerHTML.substring(startPos).substring(lowestIndex).indexOf(endTag) + lowestIndex + startPos + endTag.length;

			obj.innerHTML = obj.innerHTML.substring(0, insertIndex) + addedContent + obj.innerHTML.substring(insertIndex);
			
			prepareCellContent(insertIndex+addedContent.length, textType, textareaType, selectType, 
							checkBoxType, radioType, cellType, remarkType, obj)
		}
		
	}
			
}

function replaceSpecialValue(textIndex, content, cellType, textType, obj) {
	
	if (textIndex >= 0 && (content.indexOf(cellType) < 0 || content.indexOf(cellType) > textIndex)) {
		if (msie) {
			var tempStr = content.substr(content.indexOf(textType));
			var closePos = tempStr.indexOf(">");
			tempStr = tempStr.substr(0, tempStr.indexOf(">"));

			if (tempStr.indexOf("type=radio") >= 0 || 
				tempStr.indexOf("type=checkbox") >= 0 || 
				tempStr.indexOf("type=button") >= 0) 
			{
				return content;
			} 
			else if (tempStr.indexOf("hidden") < 0 && tempStr.indexOf("radio") < 0 && tempStr.indexOf("checkbox") < 0)
			{
			 	var startStr = "value="
				var endStr = " name=";

					if (BrowserDetect.version > 9) {
						endStr = "\"";
					}
			 	
			 		//<get the next element of value
			 		var valuePosition = content.indexOf("value=");
			 		var namePosition = content.indexOf("name=");
			 		var readOnlyPosition = content.indexOf("readOnly");
			 		var maxLengthPosition = content.indexOf("maxLength=");
					if (maxLengthPosition == -1) maxLengthPosition = content.indexOf("maxlength=");
			 		var sizePosition = content.indexOf("size=");
					
					if (closePos < valuePosition)
						valuePosition = -1;
			 	    
			 		if (valuePosition > 0) {
			 			if (readOnlyPosition != -1 && readOnlyPosition > valuePosition && 
			 				(readOnlyPosition < namePosition || namePosition == -1) &&
			 				(readOnlyPosition < maxLengthPosition || maxLengthPosition == -1) &&
			 				(readOnlyPosition < sizePosition || sizePosition == -1))
				 		{
			 				endStr = " readOnly";
				 		} 
						else if (maxLengthPosition != -1 && maxLengthPosition > valuePosition && 
			 				(maxLengthPosition < namePosition || namePosition == -1) &&
			 				(maxLengthPosition < readOnlyPosition || readOnlyPosition == -1) &&
			 				(maxLengthPosition < sizePosition || sizePosition == -1))
				 		{
			 				endStr = " maxLength=";
				 		} 
						else if (sizePosition != -1 && sizePosition > valuePosition && 
			 				(sizePosition < namePosition || namePosition == -1) &&
			 				(sizePosition < readOnlyPosition || readOnlyPosition == -1) &&
			 				(sizePosition < maxLengthPosition || maxLengthPosition == -1))
				 		{
			 				endStr = " size=";
				 		} 
						else if (namePosition < valuePosition)
						{
							endStr = " value=";
							startStr = "name=";

							if (BrowserDetect.version <= msieOld) {
								var selectPosition = content.indexOf("<SELECT");
								if (selectPosition > valuePosition) {
									endStr = " value=";
									startStr = "name=";
								}
							}
						}
			 		} 
					else {
						//no value found, so return
						return content;
					}

			 		/*
			 		// for debugging purpose
			 		alert("content: " + content + "\n" +
			 				"value: " + valuePosition + "\n" +
			 				"name: " + namePosition + "\n" +
			 				"readOnly: " + readOnlyPosition + "\n" +
			 				"maxLength: " + maxLengthPosition + "\n" +
			 				"size: " + sizePosition + "\n" +
			 				"startStr: " + startStr + "\n" +
			 				"endStr: " + endStr);
			 		*/
			 	
			 	
				var startPos = content.indexOf(startStr);
				if (startPos < 0) { 
					return content;
				}

				var oldValue = content.substr(startPos + startStr.length);
				var oldValue = "" + oldValue.substring(0, oldValue.indexOf(endStr) - 1);

			 	var newValue = replaceCharacters(oldValue);
			 	
				return content.replace(oldValue, newValue);
			}
		}
	}
	
	return content;
}

function replaceCharacters(oldValue){
	var newValue;
	
	if (oldValue != null) {
		newValue = 
			oldValue
				.replace(/!/g, "&#33;")
				.replace(/"/g, "&#34;")				
				.replace(/%/g, "&#37;")
				.replace(/'/g, "&#39;")
				.replace(/:/g, "&#58;")
				.replace(/</g, "&#60;")
				.replace(/=/g, "&#61;")
				.replace(/>/g, "&#62;")
				.replace(/`/g, "&#64;")
				.replace(/{/g, "&#123;")
				.replace(/~/g, "&#126;")
				;
	}
	
	return newValue
}
 
function hideFormElements(objForm){
	var elementList = objForm.elements;
	if(!elementList) { hideFormElementsByTag(objForm);return;};
	for (var i=0;i<elementList.length;i++){
		if(elementList[i].type=="radio" || elementList[i].type=="checkbox"){
			elementList[i].style.display="none";
		}
		else if (elementList[i].className
            && ((elementList[i].className.indexOf("alwaysEditable") >= 0)
                || (elementList[i].className.indexOf("alwaysEditableInLine") >= 0)
                || (elementList[i].className.indexOf("cancel")>=0)))
		{
			continue;
		}
		else if (elementList[i].name && elementList[i].name!="submissionRemarks")
			elementList[i].style.display="none";
		else if(!elementList[i].name && elementList[i].tagName!="FIELDSET") {
			elementList[i].style.display="none";
		}
	}
	hideImages(objForm);
}

function hideFormElementsByTag(objForm){
	var elementList = objForm.getElementsByTagName("INPUT");
	for (var i=0;elementList && i<elementList.length;i++){
		if (elementList[i].type && elementList[i].type!="hidden")
			elementList[i].style.display="none";
	}
	elementList = objForm.getElementsByTagName("SELECT");
	for (var i=0;elementList && i<elementList.length;i++){
		elementList[i].style.display="none";
	}
	elementList = objForm.getElementsByTagName("TEXTAREA");
	for (var i=0;elementList && i<elementList.length;i++){
		if (elementList[i].name && elementList[i].name!="submissionRemarks")
			elementList[i].style.display="none";
	}
}

function hideImages(objForm){
	var elementList = objForm.getElementsByTagName("IMG");
	for (var i=0;i<elementList.length;i++){
		if (elementList[i].src.indexOf("images/calendara.gif")>=0){
			if(elementList[i].className.indexOf("alwaysEditableInLine")>=0){
				continue;
			}else{
				elementList[i].style.display="none";
			}
			
		}
	}
}

function extractCheckboxSelectedToCell(checkboxIndex, content, cellType, optionType , obj){
	if  (checkboxIndex>=0 && (content.indexOf(cellType) < 0 || content.indexOf(cellType)>checkboxIndex)){
		var checked = "checked=\"checked\"";
		var startStr = "<input"
		if (msie) {
			if (BrowserDetect.version <= msieOld) {
				checked = "CHECKED";
				startStr = "<INPUT"
			}
			if (BrowserDetect.version == 9
					|| BrowserDetect.version == 10
					|| BrowserDetect.version == 11) {
				checked = "CHECKED";
			}
		}
		content = content.substr(content.indexOf(startStr));
		var returnStr = ""
		var checkbox = content.substring(content.indexOf(startStr), content.indexOf(">")+1);
		if (checkbox.indexOf(checked)>0 || (checked == "CHECKED" && checkbox.indexOf("checked")>0))
		    returnStr = "<img src='../Resources/Images/icon_" + optionType + "_selected.gif'/>";
		else
		    returnStr = "<img src='../Resources/Images/icon_" + optionType + "_unselected.gif'/>";

		return returnStr;
	}
}




function extractTextAreaContentToCell(textAreaIndex, content, cellType,  remarkType, obj){
	if  (textAreaIndex>=0 && content.indexOf(cellType) < 0 && content.indexOf(remarkType)<0){
		return extractTextAreaContentValue(content) + " ";
	}
}

function extractTextContentToCell(textIndex, content, cellType, textType, obj){
	if (textIndex>=0 && (content.indexOf(cellType)<0 || content.indexOf(cellType)>textIndex)){
		if (msie) {
		     var tempStr = content.substr(content.indexOf(textType));
		     tempStr = tempStr.substr(0, tempStr.indexOf(">"));
			 var returnStr = "";
			 
			 if(tempStr.indexOf(".variance")>=0){
				var a = 1;
			 }
			 
			 if(tempStr.indexOf("type=radio")>=0){
			 	returnStr = extractCheckboxSelectedToCell(textIndex, content, cellType, "radio" ,obj);
			 }
			 else if(tempStr.indexOf("type=checkbox")>=0){
			 	returnStr = extractCheckboxSelectedToCell(textIndex, content, cellType, "checkbox" ,obj);
			 }
			 else if(tempStr.indexOf("type=button")>=0){
			 	returnStr = ""; 	
			 }
			 else if(tempStr.indexOf("hidden")<0 && tempStr.indexOf("radio")<0 && tempStr.indexOf("checkbox")<0){
			 	returnStr = extractTextContentValue(content) + "";
			 	if 	(returnStr && returnStr=="") return _blankPlaceHolder;
			 }
			 return returnStr;
		} 
		else {
			 var returnStr = extractTextContentValue(content) + "";
			 if (returnStr && returnStr=="") return _blankPlaceHolder;
			 else return returnStr;
		}
	}
}

function extractSelectedOptionToCell(selectIndex, content, cellType, obj){
	var returnStr = "";
	var flag=false;
	if  (selectIndex>=0 && content.indexOf(cellType) < 0){
		returnStr = extractSelectContentValue(content);
		if (returnStr.toLowerCase().indexOf("please select")>=0 || returnStr=="") returnStr = _blankPlaceHolder; 
	}
	return (returnStr);
}


function extractTextContentValue(content){
	var startStr = "value=\"";
	var endStr = "\"";

	content = content.substr(content.toUpperCase().indexOf("<INPUT"));
	content = content.substr(0, content.indexOf(">"));
	if (content.indexOf('value=""')>0 || content.indexOf('value=')<0) return _blankPlaceHolder;
	if (msie) {
		if (content.indexOf("type=checkbox")>0) return "";
		if (content.indexOf("type=button")>0) return "";
		startStr = "value="
		endStr = " name=";

		 		//<get the next element of value
		 		var valuePosition = content.indexOf("value=");
		 		var namePosition = content.indexOf("name=");
		 		var readOnlyPosition = content.indexOf("readOnly");
		 		var maxLengthPosition = content.indexOf("maxLength=");
				if (maxLengthPosition == -1) maxLengthPosition = content.indexOf("maxlength=");
				var sizePosition = content.indexOf("size=");
				var dataValPosition = content.indexOf("data-val=");
				var dataValReqPosition = content.indexOf("data-val-required=");
		 	    
		 		if (valuePosition > 0) {
		 			if (readOnlyPosition != -1 && readOnlyPosition > valuePosition && 
		 				(readOnlyPosition < namePosition || namePosition == -1) &&
		 				(readOnlyPosition < maxLengthPosition || maxLengthPosition == -1) &&
		 				(readOnlyPosition < sizePosition || sizePosition == -1) &&
                        (readOnlyPosition < dataValPosition || dataValPosition == -1) &&
                        (readOnlyPosition < dataValReqPosition || dataValReqPosition == -1))
			 		{
		 				endStr = " readOnly";
			 		} else if (maxLengthPosition != -1 && maxLengthPosition > valuePosition && 
		 				(maxLengthPosition < namePosition || namePosition == -1) &&
		 				(maxLengthPosition < readOnlyPosition || readOnlyPosition == -1) &&
		 				(maxLengthPosition < sizePosition || sizePosition == -1) &&
                        (maxLengthPosition < dataValPosition || dataValPosition == -1) &&
                        (maxLengthPosition < dataValReqPosition || dataValReqPosition == -1))
			 		{
		 				endStr = " maxLength=";
			 		} else if (sizePosition != -1 && sizePosition > valuePosition && 
		 				(sizePosition < namePosition || namePosition == -1) &&
		 				(sizePosition < readOnlyPosition || readOnlyPosition == -1) &&
		 				(sizePosition < maxLengthPosition || maxLengthPosition == -1) &&
                        (sizePosition < dataValPosition || dataValPosition == -1) &&
                        (sizePosition < dataValReqPosition || dataValReqPosition == -1))
			 		{
		 				endStr = " size=";
			 		} else if (dataValPosition != -1 && dataValPosition > valuePosition &&
		 				((valuePosition > namePosition || namePosition == -1) ||
                            (dataValPosition < namePosition || namePosition == -1)) &&
		 				(dataValPosition < readOnlyPosition || readOnlyPosition == -1) &&
		 				(dataValPosition < maxLengthPosition || maxLengthPosition == -1) &&
                        (dataValPosition < sizePosition || sizePosition == -1) &&
                        (dataValPosition < dataValReqPosition || dataValReqPosition == -1))
			 		{
			 		    endStr = " data-val=";
			 		} else if (dataValReqPosition != -1 && dataValReqPosition > valuePosition &&
		 				((valuePosition > namePosition || namePosition == -1) ||
                            (dataValReqPosition < namePosition || namePosition == -1)) &&
		 				(dataValReqPosition < readOnlyPosition || readOnlyPosition == -1) &&
		 				(dataValReqPosition < maxLengthPosition || maxLengthPosition == -1) &&
                        (dataValReqPosition < sizePosition || sizePosition == -1) &&
                        (dataValReqPosition < dataValPosition || dataValPosition == -1))
                    {
			 		    endStr = " data-val-required=";
			 		} else if (namePosition < valuePosition) {
						startStr = "value=";
						endStr = " jQuery";
					}
		 		}
		 		/*
		 		// for debugging purpose
		 		alert("content: " + content + "\n" +
		 				"value: " + valuePosition + "\n" +
		 				"name: " + namePosition + "\n" +
		 				"readOnly: " + readOnlyPosition + "\n" +
		 				"maxLength: " + maxLengthPosition + "\n" +
		 				"size: " + sizePosition + "\n" +
		 				"startStr: " + startStr + "\n" +
		 				"endStr: " + endStr);
		 		*/
		 		
	}
	var startPos = content.indexOf(startStr);
	if (startPos<0) return "";
	
	var temp = content.substr(startPos + startStr.length);
	var returnStr = "" + temp.substring(0, temp.indexOf(endStr));

	//for IE11, detect browser as Mozilla
	if (BrowserDetect.browser == "Explorer" || (msie && BrowserDetect.browser == "Mozilla")) {
		//if (BrowserDetect.version == 9) 
		{	//fix for IE9, also with compatibility mode
			if (returnStr == "") {
				if (temp.indexOf(endStr) == -1) {
					endStr = " size=";	//fix for 
					if (temp.indexOf(endStr) != -1)
					{
						returnStr = "" + temp.substring(0, temp.indexOf(endStr));
						temp = returnStr;
					}
					else if (temp.indexOf(' type="text') != -1)
					{
						returnStr = "" + temp.substring(0, temp.indexOf(' type="text'));
						temp = returnStr;
					}
					else
						returnStr = temp;
				}
			}
			else {
				if (BrowserDetect.version > 9) {
					if (returnStr.indexOf("&#34;&#62;") >= 0) {
						temp = returnStr;
						returnStr = "" + temp.substring(0, temp.indexOf("&#34;&#62;"));
						//check if there is a size=
						if (returnStr.indexOf("size") >= 0) {
							temp = returnStr;
							returnStr = "" + temp.substring(0, temp.indexOf("size"));
						}
					}
				}
				else {
					if (returnStr.indexOf("&#62;") >= 0) {
						temp = returnStr;
						returnStr = "" + temp.substring(0, temp.indexOf("&#62;"));
						//check if there is a size=
						if (returnStr.indexOf("size") >= 0) {
							temp = returnStr;
							returnStr = "" + temp.substring(0, temp.indexOf("size"));
						}
					}
					else if (returnStr.indexOf("&#34;") >= 0) {
						temp = returnStr;
						returnStr = " " + temp.substring(temp.indexOf("&#34;")+5);
						temp = returnStr;
					}
					else if (returnStr.indexOf(" size=") != -1)
					{
						returnStr = "" + temp.substring(0, temp.indexOf(" size="));
						temp = returnStr;
					}
				}
			}
		}
	}

	if (msie && returnStr.lastIndexOf("\"")>0) { // when there is a space inbetween value, there will be double quotes
	    returnStr = returnStr.substring(1, returnStr.lastIndexOf("\""));
	}
	
	if (msie && returnStr != null) {
		returnStr = returnStr.replace(/"/g, "").replace(/'/g, "");
	}
	
	return returnStr;
}

function extractTextAreaContentValue(content){
	var startStr = "<textarea";
	var endStr = "</textarea>";
	if (msie) {
		if (BrowserDetect.version <= msieOld) {
			startStr = startStr.toUpperCase();
			endStr = endStr.toUpperCase();
		}
	}
	var startPos = content.indexOf(startStr);
	content = content.substr(startPos + startStr.length);
	content = content.substr(content.indexOf(">")+1);
	content = content.substring(0, content.indexOf(endStr));
	return content.replace(/\n/g,"<br/>");
}

function extractSelectContentValue(content){
	var returnStr = "";
	var startStr = "selected=\"selected\">";
	var endStr = "</option>";
	var endContentStr = "</select>";
	var temp ="";
	var startPos ="";
	
	var selectedPosition = content.indexOf("selected>");
	if (selectedPosition !=-1)
		startStr = "selected>";
	else {
		startStr = "selected value=";
		if (BrowserDetect.version > 9) {
			startStr = 'selected\" value=';
		} else if (BrowserDetect.version == 9) {
			startStr = 'selected="selected"';
		}
		// temp is sub str till > eg.<option selected value="1234">1234</option> temp will be 'selected value="1234">'
		temp = content.substr(content.indexOf(startStr),content.indexOf(">",content.indexOf(startStr)) - content.indexOf(startStr) +1);
	}

	if (BrowserDetect.version > msieOld) {
		//
	} else {
		endStr = endStr.toUpperCase();
		endContentStr = endContentStr.toUpperCase();
	}
	
	startPos = content.indexOf(startStr);
	content = content.substr(0 , content.indexOf(endContentStr));
			
	while (startPos>0){
		if(selectedPosition != -1)
			content = content.substr(startPos + startStr.length);			
		else
			content = content.substr(startPos + temp.length);
		startPos = content.indexOf(endStr)		
		if (returnStr.length>0) returnStr += "<br/>";
		returnStr += content.substring(0, startPos) 
		startPos = content.indexOf(startStr);
		temp = content.substr(content.indexOf(startStr),content.indexOf(">",content.indexOf(startStr)) - content.indexOf(startStr) +1);
		
			/*alert("content: " + content + "\n" +
			"temp:" +temp +"\n" +
			"content12: " + content + "\n" +
			"returnSr:" +returnStr+"\n"+
			"startPos:"+startPos+"\n"); */
			
	}
	

	return returnStr ;
}

function performSetSelectedList()
{
	if (document.forms[0].borColQuesSize)
	{
		var borColQuesSize = document.forms[0].borColQuesSize.value;
	    
		if(borColQuesSize && borColQuesSize > 0)
		{
			for(var x=0;x<borColQuesSize;x++)
		    {
		    	var borColQuesId = "borColQuesRow["+x+"].selectedAnswerOptionIds";
		    	var currSelect = document.getElementById(borColQuesId);
						        
		        if(currSelect !=null)
		        {
		        	for (var i=0;i<currSelect.options.length;i++)
		        	{
		        		currSelect.options[i].selected = true;
					}
				}
			}
		}
	}
}

function limitTextInput(field, maximumNum, fieldName) {
	if (field.value.length > maximumNum) {
        field.value = field.value.substring(0, maximumNum);
        alert('The '+fieldName+' field is limited to '+maximumNum+' characters.');
        
        if (field.value.length > maximumNum) {
	        var str = field.value.substring(maximumNum-1, maximumNum);
	        var strkey = String.fromCharCode(13);
	        if (strkey == str) {
				field.value = field.value.substring(0, maximumNum-1);
	        }
        }                        
	}
}

function trim (str) {
    return str.replace(/^\s\s*/, '').replace(/\s\s*$/, ''); 
}


 
function removeUnsafeXMLcharacter (XMLstring)
{
	//& is a required char for XML to denote an entity - with the enclosing & and ; characters, it is called an entity reference	
	result = XMLstring.replace(/&/g, "&amp;");		//global search for & - &amp; 
	result = result.replace(/&amp;amp/g, "&amp");	//in case &amp exists
	//result = result.replace(/</g, "&lt;");			//global search for < - &lt; 
	//result = result.replace(/&amp;lt/g, "&lt");		//in case &lt exists
	//result = result.replace(/>/g, "&gt;");			//global search for > - &gt; 
	//result = result.replace(/&amp;gt/g, "&gt");		//in case &gt exists
	//result = result.replace(/"/g, "&quot;");		//global search for " - &quot; 
	//result = result.replace(/&amp;quot/g, "&quot");	//in case &quot exists
	return (result);
}


//--------------------------------------------------------------------------------------
//URL routine starts here
//
function getParameter(name, urlData) {
  var q = document.location.search;
  if (urlData != "undefined" && urlData != null) q = urlData;

  var i = q.indexOf(name + '=');
  if (i == -1) {
    return false;
  }

  var r = q.substr(i + name.length + 1, q.length - i - name.length - 1);
  i = r.indexOf('&');
  if (i != -1) {
    r = r.substr(0, i);
  }

  return r.replace(/\+/g, ' ');
}

function getContextPath() {
  var q = document.location.pathname;
  var name = '/';

  var i = q.indexOf(name);
  if (i == -1) {
    return "";
  }
  
  //alert("i: " + i);
  var j = q.indexOf(name, i+1);
  if (j == -1) {
    return q;
  }

  //alert("j: " + j);
  var r = q.substr(0, j);
  //alert("r: " + r);

  return r.replace(/\+/g, ' ');
}

function showURLDetails() {
   var x = window.location;
   var t = ['Property - Typeof - Value',
            'window.location - ' + (typeof x) + ' - ' + x ];
   for (var prop in x){
     t.push(prop + ' - ' + (typeof x[prop]) + ' - ' +  (x[prop] || 'n/a'));
   }
   alert(t.join('\n'));
}

function getModulePath()
{
var q = document.location.pathname;
var name = '/';

var result = q.split("/");
var finalresult = "";

	if (result.length > 0)
	{
		var draftmodule = result[result.length-1];
		finalresult = draftmodule.split("?");
	}
	return (finalresult[0]);
}


//--------------------------------------------------------------------------------------
//URL routine ends here
//


//--------------------------------------------------------------------------------------
//Silk screen routine starts here
//

var bodyStyleOverflowX = null;
var bodyStyleOverflowY = null;

function disableBodyScrollbar () {
	return;

	if (document.body != null) {
		if (hasScrollBar(document.body, 'horizontal'))
		{
			bodyStyleOverflowX = document.body.style.overflowX;
			document.body.style.overflowX = "hidden";
		}

		if (hasScrollBar(document.body, 'vertical'))
		{
			bodyStyleOverflowY = document.body.style.overflowY;
			document.body.style.overflowY = "hidden";
		}
	}
}

function restoreBodyScrollbar () {
	return;

	if (document.body != null) {
		if (bodyStyleOverflowX != null) {
			document.body.style.overflowX = bodyStyleOverflowX;
		}

		if (bodyStyleOverflowY != null) {
			document.body.style.overflowY = bodyStyleOverflowY;
		}
	}
}

function disablePaginationLinks() {

}

var unloadedCount = 0;

function closeLastPopup() {
	lastPopupCount = 0;
	try {
		//force close the popup
		lastPopupObj.close();
		lastPopupObj = null;
	} catch (e) {
		alert("Popup window not valid");
	}
}

function setUnloadFlag(e) {
	//lastPopupModule will be updated via Proxy class of window.open
	if (lastPopupObj != null) {
		if (typeof lastPopupObj.name != 'undefined') {
			lastPopupCount++;
			////alert("window.onbeforeunload - setUnloadFlag " + lastPopupCount);	////
			if (typeof BrowserDetect != 'undefined') {
				checkCount = 1; //chrome
				if (BrowserDetect.browser == "Explorer") {
					checkCount = 1;
					if (lastPopupModule == "Terms.do")
						checkCount++;
				}
			}
			if (lastPopupCount > checkCount) {
				closeLastPopup();
			}
		}
	}

	var lastScrollXY;
	if (typeof ScrollSneak != 'undefined') {
		lastScrollXY = new ScrollSneak (location.hostname);
		lastScrollXY.sneak();
	}

	if (unloadedCount >= 9999) {
		//this is required for handling download object that just returned from servlet
		//note that incUnloadCount(9999) need to be called before servlet is invoked to download object
		unloadedCount = 1;
		return;
	}

	if (unloadingNow == false) {
		showSilkScreen();
		//alert("setunloadflag");
	}

	unloadingNow = true;
	//disablePaginationLinks();
}

function incUnloadCount(incValue) {
	unloadedCount = unloadedCount + incValue;
}

function getUnloadCount() {
	return (unloadedCount);
}

function showSilkScreen() {
	unloadingCount = 0;
	//this makes focus back to main window so last control (i.e. dropdown) cannot be used, but not window.open
	//self.focus();

	if (useBlockUI == true) {
		if (typeof $ == 'undefined')
			return;

		if (typeof $.blockUI == 'undefined')
			return;

		if (typeof BrowserDetect != 'undefined') {
			if (BrowserDetect.browser == "Explorer") {
				$.blockUI.defaults.fadeIn = 0;
				$.blockUI.defaults.fadeOut = 0;
			}
		}

		var silkScreen = document.getElementById("spinLoading");
		if (silkScreen != null) {
			if (useBlockUIcolor == true) {
				$.blockUI({
					message: $('#spinLoading'),
					overlayCSS: { backgroundColor: '#F5F5F5' },
					css: {
						border: 'none', 
						padding: '0px', 
						baseZ: 30000000,
						backgroundColor: '#FFFFFF', /* use #FF to be transparent */
						'-webkit-border-radius': '12px', 
						'-moz-border-radius': '12px', 
						'border-radius': '12px',
						opacity: 0.7, 
						color: '#080808',
						/* top:  ($(window).height() - 400) /2 + 'px', */
						/* left: ($(window).width() - 400) /2 + 'px', */
						width: '400px'
					}
				});
			}
			else {
				$.blockUI({
					message: $('#spinLoading')
				});
			}

			//hack for IE to animate gif
			setTimeout("document.getElementById('spinLoading').src = document.getElementById('spinLoading').src", 100);
		}

		return;
	}
	
	var silkScreen = document.getElementById("fullscreensilk");
	if (silkScreen != null) {
		disableBodyScrollbar();

		if (typeof BrowserDetect != 'undefined') {
			if (BrowserDetect.browser == "Explorer") {
				if (BrowserDetect.version == 6) {
					//hide dropdown for IE6
					var jsElement;
					if (document.all) {
						jsElement = document.all.tags("SELECT");
					} else if (document.getElementsByTagName) {
						jsElement = document.getElementsByTagName("SELECT");
					}
					for (var i = 0; i < jsElement.length; i++) {
						jsElement[i].style.display = "none";
					}
				}
			}
		}

		var screenInfo = calculatePageSize();
		var scrollData = calculateWindowScroll();

		silkScreen.style.width = (screenInfo.pageWidth) + "px";	//screenInfo.pageWidth, screen.width, 3000
		silkScreen.style.height = (screenInfo.pageHeight) + "px"; //screenInfo.pageHeight, screen.height, 3000
		silkScreen.style.display = "block";
		//silkScreen.innerHTML ="<center><span style='color:#800000;text-align:center;'> &lt; Please wait - System is processing &gt;</span></center>";

		var silkScreenImage = document.getElementById("fullscreensilkimage");
		if (silkScreenImage != null) {
			silkScreenImage.style.left = (scrollData.left) + "px";
			silkScreenImage.style.top = ((screenInfo.windowHeight/2) + scrollData.top - (141/2)) + "px";
			silkScreenImage.style.display = "block";

			//hack for IE to animate gif
			setTimeout("document.getElementById('spinLoading').src = document.getElementById('spinLoading').src", 100);
			//setTimeout("document.getElementById('spinLoading').src = 'images/loading.gif'", 200);
		}
	}

}

function hideSilkScreen() {
	unloadingCount = 0;
	document.body.style.cursor = 'default';

	if (useBlockUI == true) {
		if (typeof $ != 'undefined') {
			if (typeof $.blockUI != 'undefined')
				$.unblockUI();
		}

		unloadingNow = false;
		return;
	}

	var silkScreen = document.getElementById("fullscreensilk");
	if (silkScreen != null && silkScreen.style.display == "block") {
		restoreBodyScrollbar();

		if (typeof BrowserDetect != 'undefined') {
			if (BrowserDetect.browser == "Explorer") {
				if (BrowserDetect.version == 6) {
					//hide dropdown for IE6
					var jsElement;
	
					if (document.all) {
						jsElement = document.all.tags("SELECT");
					} else if (document.getElementsByTagName) {
						jsElement = document.getElementsByTagName("SELECT");
					}
					for (var i = 0; i < jsElement.length; i++) {
						jsElement[i].style.display = "";
					}
				}
			}
		}
		silkScreen.style.display = "none";
		unloadingNow = false;
	}
}

function handleResize() {
	if (useBlockUI == true) {
		if (unloadingNow == true) {
			showSilkScreen();
		}
		return;
	}

	if (unloadingNow == true) {
		var silkScreen = document.getElementById("fullscreensilk");
		if (silkScreen != null && silkScreen.style.display == "block") {
			silkScreen.style.display = "none";
		}
		showSilkScreen();
	}
}

window.onresize = handleResize;

//only IE supported, other browsers need to use <body onbeforeunload=""> to attach
var onStopOriginalEventObj = window.onbeforeunload;

//comment this to disable submit-silkscreen
window.onbeforeunload = setUnloadFlag;

var onStopEventObj = window.onbeforeunload;

function disableOnStopEvent() {
	onStopEventObj = window.onbeforeunload;
	window.onbeforeunload = "";
}

function removeOnStopEvent() {
	setTimeout("hideSilkScreen()", 500);
}

function restoreOnStopEvent() {
	window.onbeforeunload = onStopEventObj;
}

/* ================================================================================= */
/* ============================ Override Alert function ============================ */
/* ================================================================================= */


(function() {
    var proxied = window.alert;

    window.alert = function() {
    	hideSilkScreen();
        if (typeof console == 'undefined') {

			if (proxied != null) {
				if (typeof proxied.apply == 'function') {
					return proxied.apply(this, arguments);
				}else {
					return proxied(arguments[0]);
				}
			} else {
				alert(arguments[0]);
			}
        }else {
			if (proxied != null) {
				if (typeof proxied.apply == 'function') {
					return proxied.apply(this, arguments);
				} else {
					return proxied(arguments[0]);
				}
			}
			else {
				alert(arguments[0]);
			}
            console.log(arguments[0]);
        }
    };

    var proxy = window.open;

    window.open = function() {
		removeOnStopEvent();
		////alert("proxy - window.open");	////
		if (lastPopupObj != null) {
			if (typeof lastPopupObj.name != 'undefined') {
				closeLastPopup();
			}
		}

        if (typeof console == 'undefined') {
			if (proxy != null) {
				lastPopupModule = getModulePath();

				if (typeof proxy.apply == 'function') {
					lastPopupObj = proxy.apply(this, arguments);  
				} else {
					if (arguments.length == 3)
						lastPopupObj = proxy(arguments[0],arguments[1],arguments[2]);
					else if (arguments.length == 2)
						lastPopupObj = proxy(arguments[0],arguments[1]);
					else if (arguments.length == 1)
						lastPopupObj = proxy(arguments[0]);
					else
						lastPopupObj = proxy(arguments[0],arguments[1],arguments[2],arguments[3]);
				}
			}
        } else {
			if (proxy != null) {
				lastPopupModule = getModulePath();

				if (typeof proxy.apply == 'function') {
					lastPopupObj = proxy.apply(this, arguments);
				} else {
					if (arguments.length == 3)
						lastPopupObj = proxy(arguments[0],arguments[1],arguments[2]);
					else if (arguments.length == 2)
						lastPopupObj = proxy(arguments[0],arguments[1]);
					else if (arguments.length == 1)
						lastPopupObj = proxy(arguments[0]);
					else
						lastPopupObj = proxy(arguments[0],arguments[1],arguments[2],arguments[3]);
				}
			}
            console.log(arguments[0]);
        }

		lastPopupCount++;
    };

    var proxi = window.showModalDialog;

    window.showModalDialog = function() {
    	removeOnStopEvent();
        if (typeof console == 'undefined') {
			if (proxi != null) {
				if (typeof proxi.apply == 'function') {
					return proxi.apply(this, arguments);  
				} else {
					return proxi(arguments[0]);  
				}
			}
        }else {
			if (proxi != null) {
				if (typeof proxi.apply == 'function') {
					return proxi.apply(this, arguments);  
				} else {
					return proxi(arguments[0]);  
				}
			}
            console.log(arguments[0]);  
        }  
    }

    var proxyPrint = window.print;

    window.print = function() {
    	removeOnStopEvent();
        if (typeof console == 'undefined') {
			if (proxyPrint != null) {
				if (typeof proxyPrint.apply == 'function') {
					return proxyPrint.apply(this, arguments);  
				} else {
					return proxyPrint(arguments[0]);  
				}
			}
        } else {
			if (proxyPrint != null) {
				if (typeof proxyPrint.apply == 'function') {
					return proxyPrint.apply(this, arguments);  
				} else {
					return proxyPrint(arguments[0]);  
				}
			}
            console.log(arguments[0]);  
        }  
    }

    var proxyConfirm = window.confirm;

    window.confirm = function() {
    	hideSilkScreen();
        if (typeof console == 'undefined') {

			if (proxyConfirm != null) {
				if (typeof proxyConfirm.apply == 'function') {
					return proxyConfirm.apply(this, arguments);
				}else {
					return proxyConfirm(arguments[0]);
				}
			} else {
				alert(arguments[0]);
			}
        }else {
			if (proxyConfirm != null) {
				if (typeof proxyConfirm.apply == 'function') {
					return proxyConfirm.apply(this, arguments);
				} else {
					return proxyConfirm(arguments[0]);
				}
			}
			else {
				alert(arguments[0]);
			}
            console.log(arguments[0]);
        }
    };

}) ();


//--------------------------------------------------------------------------------------
//Silk screen routine ends here


function getNameOrElements (objID)
{
	var myObj = document.getElementById(objID);
	if (myObj == null) {	//fix for other browsers since IE can retrieve those under Name as Id
		var elname = document.getElementsByName(objID);
		if (elname.length == 0) {
			myObj = null;
		} else {
			myObj = elname[0];
		}
	}
	return (myObj);
}

function replaceImageLinkWithImageForDisabledPurpose(selector) {
	 var ar = $(selector);
	 ar.children('img:first').addClass('opacity-40');
	 ar.replaceWith(ar.contents());
}

function maxOutSelectionBoxForIE(selector, defaultSize) {
	if($.browser.msie && parseInt($.browser.version, 10) <= 8) {
		var maxSize = 0;
		$.each($(selector), function(i,v) {
			var currentSize = $(v).width();
			if (currentSize > maxSize) {
				maxSize = currentSize;
			}
		});
		$(selector).css('width', maxSize);
	}
	else {
		$(selector).css('width', defaultSize);
	}
}

/**
 * Populate another drop down list based on a parent drop down list selection via AJAX mechansim
 * 
 * @param parentSelector jquery selector syntax of parent drop down
 * @param childSelector jquery selector syntax of the drop down getting populated
 * @param url url to fire the request to server, or a function which must return a url string
 * @param callback function to be invoked on the list of result return from server request, 
 * 		  this will do the actual population of the drop down list
 */
function populateDropdownViaAjax(parentSelector, childSelector, url, callback) {
	if (!callback || typeof(callback) !== 'function') {
		throw 'common.js[populateDropdownViaAjax]: Please provide a valid callback for response manipulation.';
	}
	if (!$(parentSelector + ' option:selected').val()) {
		$(childSelector).empty().append($('<option>', { value : "" }).text("Please Select"));
		return;
	}
	
	var theUrl;
	if (typeof(url) === 'function') {
		theUrl = url.call();
		if (typeof(theUrl) !== 'string') {
			throw 'common.js[populateDropdownViaAjax]: Please provide a url callback which return request url';
		}
	}
	else {
		theUrl = url + $(parentSelector + ' option:selected').val();
	}
	
	$.ajax({
		url: theUrl,
        success: function(response,st,xhr)  {
            if (response) {
                $.each(response, function(key, value) {
                	callback(key,value);
                });
            }
        },
        beforeSend: function() {
        	 $(childSelector).hide('slow').empty().append($('<option>', { value : "" }).text("Please Select"));
        },
        complete: function() {
        	$(childSelector).show('fast');
        },
        cache: false
	});
}

function roundDecimal(num,allowedDecimals){
	if(num.indexOf(".")!=-1){
		var numOfDecimals=(num.substring(num.indexOf(".")+1,num.length)).length;
		if(numOfDecimals>allowedDecimals){
			var floatNum=parseFloat(num);
			return floatNum.toFixed(allowedDecimals);
		}
		else{
			return num;
		}
	}
	else{
		return num;
	}		
}

function truncateDecimal(num,allowedDecimals){
	if(num.indexOf(".")!=-1){
		var numOfDecimals=(num.substring(num.indexOf(".")+1,num.length)).length;
		if(numOfDecimals>allowedDecimals){
			return num.substring(0,((num.length-numOfDecimals)+allowedDecimals));
		}
		else{
			return num;
		}
	}
	else{
		return num;
	}		
}

function jsDebug(dbgStr) {
	if (parent.debug) {
		parent.debug(dbgStr);
	}
}

function getStyleBySelector( selector ) {
	var sheetList = document.styleSheets;
	var ruleList;
	var i, j;

	if (sheetList != null) {
		for (i=sheetList.length-1; i >= 0; i--) {
			
			ruleList = sheetList[i].cssRules;	//for Webkit
			if (typeof ruleList == 'undefined') {
				ruleList = sheetList[i].rules;	//for IE
			}

			for (j=0; j<ruleList.length; j++) {
				if (ruleList[j].selectorText == selector) {
					return ruleList[j].style;
				}   
			}
		}
	}
	return null;
}


function cancelBack()   
{   
    if ((event.keyCode == 8 ||    
       (event.keyCode == 37 && event.altKey) ||    
       (event.keyCode == 39 && event.altKey))   
        && (event.srcElement.readOnly == true))   
    	{
	        event.cancelBubble = true;
	        event.returnValue = false;
    	}
}

function addCommaOnBlur(obj) {
	var nStr = obj.value;
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById(obj.id).value = x1 + x2;
}

function removeCommaOnFocus(obj) {
	var nStr = obj.value;
	if (typeof nStr != 'string') {
		return nStr;
	}
	document.getElementById(obj.id).value = nStr.replace(/,/g,'');
}

//removeBankSpecific("content-body");
function removeBankSpecific(idName) {

	var titleStr = top.document.title;
	if (titleStr.indexOf("Financial Statement") != -1) {
		return;
	}

	var hx = document.getElementById(idName);
	var myHTML = hx.innerHTML;
	
	if (myHTML.indexOf("Omnibus Details") != -1) {
		return;
	}
	
	if (BrowserDetect.browser == "Explorer") {
		if (BrowserDetect.version < 9) {	
			if (typeof barDrawComplete == "boolean") {
				return;
			}
		}
	}
	var myNoUOB = myHTML.replace(/UOB/g, "INTEGRO-BANK");
	var myNoFEB = myNoUOB.replace(/FEB/g, "SUBSIDIARY");
	
	var myNoUOBUrl = myNoFEB.replace(/bank=INTEGRO-BANK/g, "bank=UOB");
	var myNoFEBUrl = myNoUOBUrl.replace(/bank=SUBSIDIARY/g, "bank=FEB");

	hx.innerHTML = myNoFEBUrl;

}

function paginate(items, placeholder, itemsPerPage) {
	var total = items.length;
	placeholder.empty();
	if (total > itemsPerPage) {
		var html = []; 
		html.push('<ul class="pagination">');
		var totalPage = Math.ceil(total / itemsPerPage);
		
		for (var i=0; i<totalPage; i++) {
			html.push('<li><span class="pagee has-tips" title="Go to page ');
			html.push(i+1);
			html.push('" data-page="');
			html.push(i+1);
			html.push('">');
			html.push(i+1);
			html.push('</span></li>');
		}
		html.push('</ul>');
		placeholder.append(html.join(''));
	
		placeholder.find('span.pagee:nth(0)').addClass('selected');
		items.hide();
		items.slice(0,itemsPerPage).show();
		
		placeholder.find('span.pagee').click(function() {
			placeholder.find('span.pagee').removeClass('selected');
			$(this).addClass('selected');
			var page= $(this).data('page');
			items.hide();
			items.slice(((page-1)*itemsPerPage),(page*itemsPerPage)).show();
		});
	}
}

function triggerPopUp(subSectionId){
	var subSectionElement = document.createElement('div');
	subSectionElement.innerHTML = document.getElementById(subSectionId).innerHTML;
	$.fancybox(subSectionElement, {
		fitToView: true,
		width: 1000,
		height: 250,
		autoHeight:true,
		autoSize: false,
		closeClick: false,
		openEffect: 'none',
		closeEffect: 'none',
		scrolling:"no",
		closeBtn:false,
	    afterShow: function() { 
	        $('<br><center><a href="javascript:$.fancybox.close();removeOnStopEvent()" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage(\'Close\',\'\',\'images/button/close1a.gif\',1)"><img src="images/button/close1.gif" name="Close" border="0" id="Close" /></a></center>').appendTo(this.inner);
	    },
		helpers:{ 
			overlay : {closeClick: false}
        }
	});
	removeOnStopEvent();
}

function getElementValue(v) {
	if($(v).prop("tagName") == 'SELECT'){
		return $(v).find('option:selected').text();
	}
	else if ($(v).prev().prop('type') == 'radio') {
		if (v.length) {
			for (var j = 0; j < v.length; j++) {
				if (v[j].checked) {
					return $(v[j]).data("code");
				}
			}
		}
	} else {
		return $(v).val();
	}
}

function addIndexesAsMandatory(elementId, indexes) {
	var existingIndexes = $(elementId).val();
	for (var i = 0; i < indexes.length; i++) {
		var index = existingIndexes.indexOf(indexes[i]);
		if (index < 0) {
			existingIndexes = existingIndexes + ', ' + indexes[i];
		}
	}
	$(elementId).val(existingIndexes);
}

function processConditionalMandatoryOptionalElements(elementId, conditionalMandatoryOptionalConfig){
	if(conditionalMandatoryOptionalConfig){
		for(var i= 0; i < conditionalMandatoryOptionalConfig.length; i++){
			var elementIndex = conditionalMandatoryOptionalConfig[i][0];
			var elementCriteria = conditionalMandatoryOptionalConfig[i][1];
			var mandatoryIndexes = conditionalMandatoryOptionalConfig[i][2];
			var element=document.getElementsByName("elementIndexValue["+elementIndex+"]");
			if(getElementValue(element)==elementCriteria){
				addIndexesAsMandatory(elementId, mandatoryIndexes);
			}
		}
	}
}

function populateChildDropdown(theUrl, childElement, origValue, isAnimateReq)
{
	$.ajax({
		url: theUrl,
		async:false,
		beforeSend: function() {
			if(isAnimateReq){
				$(childElement).hide('slow');
			}

			$(childElement).empty().append($('<option>', { value : "" }).text("Please Select"));
		},		
		success: function(data,st,xhr)  {
			if (data) {
				$(childElement).find('option').remove();
				$(childElement).append('<option value="">Please Select</option>');
				$.each(data, function(i, v){
					$(childElement).append('<option value="'+v['id']+'">'+v['codeValue']+'</option>');
				});
				$(childElement).val(origValue);
			}
		},
		complete: function() {
			if(isAnimateReq){
				$(childElement).show('fast');
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {

		},
		cache: false
	});
	
}

function clearElementValue(v) {
	if(v){
		if ($(v).prev().prop('type') == 'radio') {
			if (v.length) {
				for (var j = 0; j < v.length; j++) {
					if (v[j].checked) {
						$(v[j]).removeAttr('checked');
					}
				}
			}
		} else {
			$(v).val('');
		}
	}
}

function enableOrDisableFields(conditionalDisableConfig, modifiedElementIndex){
	if(conditionalDisableConfig){
		for(var i= 0; i < conditionalDisableConfig.length; i++){
			var elementIndex = conditionalDisableConfig[i][0];
			var elementCriteria = conditionalDisableConfig[i][1];
			var enableOrDisableFields = conditionalDisableConfig[i][2];
			var isDisable = conditionalDisableConfig[i][3];
			var element=document.getElementsByName("elementIndexValue["+elementIndex+"]");
			for (var j = 0; j < enableOrDisableFields.length; j++) {
				var disableElement=document.getElementsByName("elementIndexValue["+enableOrDisableFields[j]+"]");
				if(modifiedElementIndex==elementIndex || modifiedElementIndex==undefined){
					if($.inArray(getElementValue(element), elementCriteria)>-1 || ($.inArray('undefined', elementCriteria)>-1 && !(getElementValue(element)))){
						if(isDisable){
							clearElementValue(disableElement);
						}
						$(disableElement).prop('disabled', isDisable);
					}else{
						if(!isDisable){
							clearElementValue(disableElement);
						}
						$(disableElement).prop('disabled',!isDisable);
					}
				}
			}
		}
	}
}

function configDropdownList(conditionalDropdownConfig, isAnimateReq, modifiedElementIndex){
	if(conditionalDropdownConfig){
		for(var i= 0; i < conditionalDropdownConfig.length; i++){
			var elementIndex = conditionalDropdownConfig[i][0];
			var elementCriteria = conditionalDropdownConfig[i][1];
			var dropConfigIndexes = conditionalDropdownConfig[i][2];
			var dropConfigCodeSet = conditionalDropdownConfig[i][3];
			if(modifiedElementIndex==elementIndex || modifiedElementIndex==undefined){
				for (var j = 0; j < dropConfigIndexes.length; j++) {
					var element=document.getElementsByName("elementIndexValue["+elementIndex+"]");
					if(getElementValue(element)==elementCriteria){
						populateConfigDropdown(dropConfigCodeSet[j], isAnimateReq, dropConfigIndexes[j]);
					}
				}
			}
		}
	}
}

/**
 * Convert select to array with values
 */    
function serializeSelects(select) {
    var array = [];
    $(select + ' option').each(function(){ array.push($(this).val()) });
    return array;
}

/**
 * Populate another multiple select based on a parent multiple list selection via AJAX mechansim
 * 
 * @param parentSelector jquery selector syntax of parent multiple select
 * @param childSelector jquery selector syntax of the multiple select getting populated
 * @param url url to fire the request to server, or a function which must return a url string
 * @param callback function to be invoked on the list of result return from server request, 
 * 		  this will do the actual population of the multiple select
 */
function populateMultipleSelectViaAjax(parentSelector, childSelector, url, callback, completeCallBack) {
	if (!callback || typeof(callback) !== 'function') {
		throw 'common.js[populateMultipleSelectViaAjax]: Please provide a valid callback for response manipulation.';
	}
	if (!$(parentSelector + ' option').val()) {
		$(childSelector).empty();
		if( completeCallBack && typeof(completeCallBack) === 'function') completeCallBack();
		return;
	}
	
	var theUrl;
	if (typeof(url) === 'function') {
		theUrl = url.call();
		if (typeof(theUrl) !== 'string') {
			throw 'common.js[populateMultipleSelectViaAjax]: Please provide a url callback which return request url';
		}
	}
	else {
		theUrl = url + serializeSelects(parentSelector);
	}
	
	$.ajax({
		url: theUrl,
        success: function(response,st,xhr)  {
            if (response) {
                $.each(response, function(key, value) {
                	callback(key,value);
                });
            }
        },
        beforeSend: function() {
        	$(childSelector).empty();
        },
        complete: function() {
        	if( completeCallBack && typeof(completeCallBack) === 'function') completeCallBack();
        },
        cache: false
	});
}

//--------------------------------------------------------------------------------------
//Animate css routine starts here
//
function startCSSAnim (myElem, x) {
	$(myElem).removeClass().addClass(x + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function(){
	  $(this).removeClass();
	});
};

//--------------------------------------------------------------------------------------
//Animate css routine ends here

function initConfigRatingFormElementValues(){
	$('input[type=radio]').each(function(i,v){
		if($(v).data("code")==$(v).data("existing-value")){
			$(v).prop("checked", true)
		}
	});
}

function getCommaSeparatedValueFromArry(disableConfigItemArry){
	var tempArray = new Array(disableConfigItemArry.length);
	for(i=0;i<disableConfigItemArry.length;i++){
		tempArray.push(disableConfigItemArry[i]);
	}
	return tempArray;
}

