
<BR>
<H1>Результаты поиска</H1>
<HR>

<TABLE BORDER=0 width=95% spacing=0 CELLPADDING=0 CELLSPACING=0>
<TR><TD background=naw.gif><BR></TD></TR>

<?Php

	error_reporting(0);
	$timex=time();
	$timey=0;
	$bgchange="FFFFFC";
	$fchek=0;
	$command[0]="0";
	$dirtest="false";


#Указываем где нужно искать, в каких папках

if($where=="index"){
$dirtest="true";
$linkdir="";
$handle=opendir('.');

	} elseif($where=="php"){
	$linkdir="phpapps/";
	chdir('./phpapps');
	$handle=opendir('.');

		}elseif($where=="other"){
		$linkdir="otherscripts/";
		chdir('./otherscripts');
		$handle=opendir('.');

			}elseif($where=="news"){
			$linkdir="news/";
			chdir('./news');
			$handle=opendir('.');
} else {
	$dirtest="true";
	$linkdir="";
	$handle=opendir('.');
}


#Обрабатываем все данные




while (($file = readdir($handle))!==false){
	if (eregi("[a-zA-Z0-p_-]*.html",$file) or eregi("[a-zA-Z0-p_-]*.htm",$file)or eregi("[a-zA-Z0-p_-]*.php",$file)or eregi("[a-zA-Z0-p_-]*.txt",$file)){
		$fchek=$fchek+1;
		$sizer=filesize($file);
		$sizer=($sizer/1000);
		$sizer=round($sizer,1);
		$file=trim($file);
		$file=chop($file);
		$filed=file($file);
		$fileold=$file;
		$count = count($filed);
		$i = $count;
		$zt = 0;
		$clt=0;
		$found=0;
		$stringer=0;
			for($j=$zt;$j<$i;$j++){
				$string=$filed[$j];
				$stringer=$filed[$j];
				$num = "regPLACE hold";
				$string=strtolower($string);
				$stringer=strtolower($stringer);
				$whatdoreplace=strtolower($whatdoreplace);
				$string = ereg_replace($whatdoreplace, $num, $string);
					if($string!=$stringer){
					$found=$found+1;
					$abby=$found;
					$show[$found]=$stringer;
					}
			}
		$dircount=count($file);
		echo "<FONT SIZE=-1>";
		if($found>0){
			$command[0]=($command[0]+10);
			if(eregi("[a-zA-Z0-p_-]*.txt",$file)){
				$fileold=$file;
				$file="index.php";
				if($dirtest=="true"){
				$file=$fileold;
			}

		}
		if($file=="index.html"){
			if($bgchange=="EEEEEE"){
			$bgchange="FFFFFF";
			echo "</TD></TR><TR><TD BGCOLOR=$bgchange><BR><A HREF=\"$linkdir$file\" target=_new><FONT SIZE=+1>$file</A><BR>$data</FONT> Размер: $sizer Kb<BR><BR>Найдено совпадений <B><FONT COLOR=RED>$found</FONT></B> :<B><FONT COLOR=RED>$whatdoreplace</FONT></B>. <BR><UL>";
			} else {
				$bgchange="EEEEEE";
				echo "</TD></TR><TR><TD BGCOLOR=$bgchange><BR><A HREF=\"$linkdir$file\" target=_new><FONT SIZE=+1>$fileold</A> </FONT> Размер: $sizer Kb<BR><BR>Найдено совпадений <B><FONT COLOR=RED>$found</FONT></B>  :<B><FONT COLOR=RED>$whatdoreplace</FONT></B>.  <BR><UL>";
				}
			} elseif($bgchange=="EEEEEE"){
			$bgchange="FFFFFF";
			echo "</TD></TR><TR><TD BGCOLOR=$bgchange><BR><A HREF=\"$linkdir$file\"><FONT SIZE=+1>$fileold</A></FONT> Размер: $sizer Kb<BR><BR>Найдено совпадений <B><FONT COLOR=RED>$found</FONT></B> :<B><FONT COLOR=RED>$whatdoreplace</FONT></B>. <BR><UL>";
			} else {
				$bgchange="EEEEEE";
				echo "</TD></TR><TR><TD BGCOLOR=$bgchange><BR><A HREF=\"$linkdir$file\"><FONT SIZE=+1>$fileold</A></FONT> Размер: $sizer Kb<BR><BR>Найдено совпадений <B><FONT COLOR=RED>$found</FONT></B> :<B><FONT COLOR=RED>$whatdoreplace</FONT></B>.  <BR><UL>";
				}
			$file=$fileold;
			for($new=1;$new<=$found;$new++){
				$show[$new]=strip_tags($show[$new]);
				$show[$new] = ereg_replace($whatdoreplace,"<FONT COLOR=black><B>$whatdoreplace</FONT></B>",$show[$new]);
				echo "<FONT SIZE=-1>";
				print "<img src=naw.gif> $show[$new]";
			}
			echo "</UL></FONT></TD></TR>";
		}

		}
}

if($command[0]==0){
	echo "<BR></TD></TR><TR><TD bgcolor=000000><CENTER><B><FONT SIZE=+2 COLOR=RED>Не найдено! ";
}

echo "<BR></TD></TR><TR><TD BGCOLOR=RED><CENTER><FONT SIZE=-1>Поиск  $fchek за ";
$timey=time();
$timea=($timey-$timex);
echo " $timea секунд";


?>
<TD/></TD></TR></TABLE><BR><CENTER>Трофимов Дмитрий<TABLE>

	<SCRIPT language='JavaScript'> var loc = ''; </SCRIPT>
<SCRIPT language='JavaScript1.4'>try{ var loc = escape(top.location.href); }catch(e){;}</SCRIPT>
<SCRIPT language='JavaScript'>
var userid = 38624149; var page = 1;
var rndnum = Math.round(Math.random() * 999111);
document.write('<IFRAME src="http://ad6.bannerbank.ru/bb.cgi?cmd=ad&hreftarget=_blank&pubid=' + userid + '&pg=' + page + '&vbn=903&w=88&h=31&num=1&r=ssi&ssi=nofillers&r=ssi&nocache=' + rndnum + '&ref=' + escape(document.referrer) + '&loc=' + loc + '" frameborder=0 vspace=0 hspace=0 width=88 height=31 marginwidth=0 marginheight=0 scrolling=no>');
document.write('<A href="http://ad6.bannerbank.ru/bb.cgi?cmd=go&pubid=' + userid + '&pg=' + page + '&vbn=903&num=1&w=88&h=31&nocache=' + rndnum + '&loc=' + loc + '&ref=' + escape(document.referrer) + '" target="_blank">');
document.write('<IMG src="http://ad6.bannerbank.ru/bb.cgi?cmd=ad&pubid=' + userid + '&pg=' + page + '&vbn=903&num=1&w=88&h=31&nocache=' + rndnum + '&ref=' + escape(document.referrer) + '&loc=' + loc + '" width=88 height=31 Alt="Trofimov chat" border=0></A></IFRAME>');
</SCRIPT>
