/* 0 -> headeroff, 1 -> footeroff, 2 -> adsofftitle, 3 -> createsitetitle, 4 -> createsiteurl, 5 -> margintop */
GDATA={'RU':['0','0','Отключить рекламу','Создать сайт бесплатно','http://www.ucoz.ru/?pguid=3089118685','31px'],
       'DE':['1','0','','Homepage erstellen','http://www.ucoz.de/?pguid=882644864','0px'],
    'US':['1','0','','Create free website','http://www.ucoz.com/?pguid=2901615790','0px'],
    'RO':['1','0','','Creeaza site gratuit','http://www.ucoz.ro/?pguid=3485241110','0px'],
    'NO':['1','1','','','','0px']
};
var wy=50;
if(typeof GDATA[u_data[0]]==='undefined') u_data[0]='US';
if (GDATA[u_data[0]][0]=="1") wy-=25;
if (GDATA[u_data[0]][1]=="1") wy-=25;
document.write('<input type="hidden" id="wrapperX'+u_data[1]+'" value="12"><input type="hidden" id="wrapperY'+u_data[1]+'" value="'+wy+'">');
var b_v0='right';
var b_v1='-(document.getElementById("'+u_data[1]+'")?document.getElementById("'+u_data[1]+'").offsetWidth:0)+(document.documentElement.clientWidth?document.documentElement.clientWidth:document.body.clientWidth)+';
var b_v2='     <td valign="top" width="12"><a href="javascript://" onclick="f'+u_data[1]+'('+"'none'"+');if(o=window.parent.document.getElementById('+"'iF"+u_data[1]+"')){o.style.display='none';}"+'if(o=window.parent.document.getElementById('+"'dV"+u_data[1]+"')){o.style.display='none';}"+'return false;" title="Close"><img alt="" src="http://'+u_data[3]+'.ucoz.net/img/ma/cv.gif" style="border:0;width:12px;height:54px;margin-top:'+GDATA[u_data[0]][5]+';"></a></td>';
if (u_data[2]=="1") {
    b_v0='left';
    b_v1='';
}
document.write('<style type="text/css">.c'+u_data[1]+' {position:absolute;'+b_v0+':0px;top:0px;z-index:10000;} div > div.c'+u_data[1]+' {position:fixed;}</style>'+
'<!--[if gte IE 5.5]><![if lt IE 8]><style type="text/css">table#'+u_data[1]+' {position:absolute;'+b_v0+':expression(('+b_v1+'(ignoreMe2=document.documentElement.scrollLeft?document.documentElement.scrollLeft:document.body.scrollLeft))+"px");top:expression((ignoreMe=document.documentElement.scrollTop?document.documentElement.scrollTop:document.body.scrollTop)+"px");}</style><![endif]><![endif]-->'+
'<script type="text/javascript">'+
'    function f'+u_data[1]+'(a){try{document.getElementById("p'+u_data[1]+'").style.display=a;}catch(e){}try{document.getElementById("'+u_data[1]+'").style.display=a;}catch(e){}}'+
'    function f2'+u_data[1]+'(){f'+u_data[1]+'("");}'+
'</script>'+
'<div id="p'+u_data[1]+'" class="c'+u_data[1]+'">'+
'    <table border="0" cellspacing="0" cellpadding="0" id="'+u_data[1]+'" style="z-index:10000;">'+
'    <tr>'+
(u_data[2]=="1" ? '' : b_v2)+
'	<td id="advtd'+u_data[1]+'">'+
'	    <style type="text/css">'+
'a.a-close-link {font: 10px/14px "Tahoma", "Arial" !important;color: #3e3e3e !important;text-decoration: underline !important;margin: 0 !important;background:#ffffff;border-radius:3px;-moz-border-radius:3px;padding:0 4px;}'+
'a.a-close-link:hover {text-decoration: none !important;}'+
'a.a-buttons {color: #3e3e3e !important;text-transform: uppercase !important;font: 14px/25px "Tahoma", "Trebuchet MS", "Arial" !important;text-shadow: 0 1px 1px #fff;text-decoration: none !important;display: block;height: 25px !important;}'+
'a.a-buttons span {display: block !important;text-align: center !important;height:25px;line-height: 23px !important;padding: 0 0 0 12px !important;}'+
'a.a-buttons span span {}'+
'a.green-but {background: url(http://'+u_data[3]+'.ucoz.net/ucoz/img/uads/a-buttons.png) no-repeat right -25px;padding-right: 7px;}'+
'a.green-but span {background: url(http://'+u_data[3]+'.ucoz.net/ucoz/img/uads/a-buttons.png) no-repeat;}'+
'a.blue-but {background: url(http://'+u_data[3]+'.ucoz.net/ucoz/img/uads/a-buttons.png) no-repeat right -75px;padding-right: 7px;}'+
'a.blue-but span {background: url(http://'+u_data[3]+'.ucoz.net/ucoz/img/uads/a-buttons.png) no-repeat 0 -50px;}'+
'a.a-buttons.a-check span span {background: url(http://'+u_data[3]+'.ucoz.net/ucoz/img/uads/b-ico-check.png) no-repeat 0 5px;}'+
'a.a-buttons.a-clock span span {background: url(http://'+u_data[3]+'.ucoz.net/ucoz/img/uads/b-ico-clock.png) no-repeat 0 5px;}'+
'	    </style>');
    if (GDATA[u_data[0]][0]=="0") document.write('<a href="http://ads.ucoz.ru/narod-noads/" target="_blank" title="Disable uCoz advertisement" class="a-buttons green-but a-clock"><span><span>'+GDATA[u_data[0]][2]+'</span></span></a>');
    document.write('<div id="mainadsdv'+u_data[1]+'" style="background:#fff">');
