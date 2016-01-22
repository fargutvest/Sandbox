using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        //static string[] anim = new string[] { "/", "--", @"\", @"|" };
        static string[] anim = new string[] 
        { @"srsssssssssrrrsrrrrrrrrrrrr;r;;iri;iiiiiiii:i:i:i:i::::i,
s:;ririiiiiiii:i::::::::::,:,,,,,,,,,,,,,,,,,,,,,, , , , 
sirrrrr:rir;riri;i;iiiiiiii:i::::::::::::,:,:,:,,,,,,,,, 
r:rr:,;s,,:iii:iii:i:i::::::::::::,:,:,,,,,,,,,,,,,,,,,, 
i,i:s29BMs:::::::::::::,:,,,:,,,,,,,,,,,,,,,,,,,, ,,, ,, 
i,:,2Gs29G:,,,,,,,,,,,,,,,,,,,,,,,,,,, ,     , ,       , 
@9;isM99sSisi,,,, :i  ,,,,, , , ,                        
9s:Gr,:r;ss9MGGG2i9@,  ,,, ,   ,                         
:  Sr   ,sMG5s9B@SGBGr                                   
rr2Ss  i:rH22sX@BMB@@@s                                , 
i:rri,:sirss22XGMB@B@B@r5s     ,    ,,,,,,,,,,,,,,,,,,,, 
,    ,,::::rsssss@B@B@BMB@22srG@rrS:,,,:,:,::::::i:::iii:
s,, ,,,,,,rs5X5srM@M@B@B@B@B@@BB@B@G9X9X9X2s2ss299GH2sXGS
s:,,  ,:,XGG9G995GBG9G2SGGM@MMGMBBM@B@B@B@B@B@9X99HXHSHX5
s:,   GG9BGMBG9S@BBXss2s59@MMGMGG9MGMGMMMMMGM@@MBBMMB9SSX
SSS29GGMGGMBMBMBM@MMSX92SGGGBBMMMBMMMBBMMBBMMBMMMMM@B@@@B
@B@B@MXM9GMM@MMMBM@GBS5H99G9MM@MMMMMMMBMMMBBBMBMBMBMBMMMB
B@@@GMX9GMMGMBMBMMBBBMMBMMMGM@M@BBMBB@B@B@BBMBM@M@B@MBMMM
BGMMBBMMBM@GMMBM@B@BBM@BBMBMMMBB@MMM@M@@@BBM@B@BBMBMBBBM@
BMMMM@B@MMB@BBMMM@BBBBB@@@MG9MB@BMGGM@BBMMGMGMMMMMGMMMMMM
@B@BBMMB@MMMMMBMMMMMMMMMMM@GBMBMM99G@MMB@B@MMGMMMGMGMGMMM
BMMBMMMBMMMBMBMMBBMB@@G99MGMBBMGGMGM9XMBB@B@B@B@MBM@GGMMG
@M@BBMBBBBBMBB@B@B@BB9X29MMM@MMB@MMMMM@GGGMMMB@B@@@BBB@@B
MMMBMBB@B@@@B@B@MM9SS99GMMGBGGG@B@B@@@MMGMBBMMMMMBB@B@B@B
BMBB@BBMMMBMMGGG9GSGMX99GS99XMG9GMMBBBBM@@@MBMMMBMMMMMBM@", 
@";:;:;:;::::::::::::::,,,,,,,,,,,,,,,,,,,,,,,,,,,, 
;,,:,,,,,,,,,,,,,,,,,,,,,, , , , ,                
:,:::,,,:,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, , , , , 
:,,,,,;  ,,,,,,,,,,,,,,,,,,,,,,, , , , ,          
, ,,;rG5;,,,,,,,,,,,,,, , ,   ,                   
,  ,sr;sr      ,   , ,                            
M;,,ss3;r,:     ,                                 
r,,s ,,,;i33ss;,3r                                
, ,r    ;Ss;iM@;3s;                               
,:;r  ,,:r;;;MMBB@M;                              
,,,,, ;,:;;;;rHMM@MM:::                         , 
     , ,,,,;;,,MMMMM3HH;;,;X,,;      , ,,,,,,,,,,,
:       ,,;rr:,BM@MMMMM@BBGBBABii;iii:;::;sssi;;3i
;,    , rsss3r;H@i3;:ss@BHSXBBXMMMMMBXBBr;ir;i;iii
:    ;5rBsBB3;@MB:,;:;S@BXGXS3XGGGXBX55B@BBBHAH;;;
;;;;s3ss3BB@@BXMBS;s;isHH@BBB@BBB@BBB@BBBXBBB@BM@M
B@@BB;XsXB@@BB@M@B3;;;3S3@@BBBBBBBBBB@@B@@BB@B@BBB
@MMXHr;SXBH@B@B@MB@BBBBBG@@M@BB@MMM@MMBBBM@@@@@@BB
BGABMBBBB@GBB@MMM@@@M@@BBBMMMBB@@MMMM@BMM@@@@B@@@@
@BXB@@MBB@M@BBBM@@@@MM@B3H@MMBHG@M@@BXBABBBBXBBBBX
M@MBBB@@BB@BBBBBBBBBBBB@XB@BAisX@XMMMM@HBBXGAXHXBX
@BB@BBBBBBB@BB@@B@MB;sGHB@BSHHXX,S@MMMMMMMBB@@SGXB
MB@@BB@@@BB@MMMMMMX;;;BG@BB@M@BBABXsAXBBMMMMMMMMMB
XB@@BM@MMMMMMM@Bi;;ss@BGBSS@MMMMM@BHB@BBXXB@@MMMMM
BB@@M@BB@@BX3Ss3;BsrssiiisBssX@@@@@@MM@BBX@BXBBB@@", 
@":,:::,:::,:,:,,,:,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, 
:,,,,,,,,,,,,,,,,,,,,,,, , ,                      
:,:,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, ,,, , , ,   , , 
,,,, ,:  ,,,,,,,,,,,,,,,,,,,,, ,     , ,          
, ,,;iSs: ,,,,,,, ,,, ,   ,   ,                   
,  ,ri;ri          , ,                            
@;,,rrs;;,:     ,                                 
i,,i ,,,:;ssrs:,si                                
, ,;    ;3r;;@5;ss;                               
,:;i  ,,,i:;:M@s33X;                              
,,,,, :,::;;;rrMBBMM::,                         , 
     , ,,,,;:,,MM@@@;ss;;,;H,,;      , ,,, ,,,,,,,
:       ,,;ii:,i@AMMMMB35335S3Sii;;ii;;:::rrr;;;s;
:,      isssrr;rA;;;:riASsrs33s@BB@@333Sriir;i;;;;
:,   ;3rSrssr;XM3::;::sXsssrrisrrisssirsGssssss;;;
;;;;rsssrsHXXHs@Sr;r;irsiBGHHXX3ABGAXBHGsrSHHBHBA@
sHSs3i3rs3XBHSX@Xsi;;irriXBBS5GGGGGHSB@GBXXXBGXSHG
B@@rri;rrs;BXBHB@GGrSrs3iBBM@XHB@M@@M@XAX@BB@BBBGX
AiiHMHsHGB;SGBMMMBBBMBXH3G@M@HABB@M@MXX@M@BBXXBBBB
BG5ABB@GS@@BHSAMBB@B@M@X;;BMMS;;BM@BSrHiHXS55GGSSs
M@MXG3B@HXBGXHSHS35SHHHXrXBHi;isB3@MMMX;3Sriis;sSs
BXXBXGGX3HXXGG@@GBMs;irrAXS;;irr:;B@MMMMMMXB@B:;33
@X@@XBBB@A5BMMMMMBr;;isrAG3B@XSS;Xi:;sSXMMMMMM@MMX
3SBBX@@MM@@MMMAr;;;sr3sssriXMMMMMBS;G@SHssGB@MMMMM
AXBB@BHXBBHr:;;i;ssisrrirrs;;rB@B@BBMMBB35BG3S3GBB",
@":,:::::::,,,:,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, 
,,,,,,,,,,,,,,,,,,,,,, ,   , ,                    
:,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, , , , , , ,   , 
,,,, ,:  ,,,,,,,,,,,,,,,,,,, , , , ,              
, ,,;;Ss: ,,,,,,, ,,, , , ,                       
,  ,ri:ri                                         
X;,,irr:;,:     ,                                 
i,,i  ,,:;ssir;,si                                
, ,;    ;si:;H5;ss;                               
,,;i  ,,,;:;;BXsss3;                              
,,,,, :,,:;;;rrMXS@@:::                         , 
       ,,,,::,,@@BBB;ss::,;H,,:      , ,,, ,,,,,,,
:       ,,;ii:,rBsMMM@H3S333S3Srr;iii:;:::irr;;;s;
:,      ;sssrr;rs;;;:ii3ssrss3sHSHXA3335siir;i;;;;
:    ;3r3r3ss;3Bs;:;::sssssssrsrsrsssrss3s3ssss;;;
;;;;rsssr3ss5srBrr;r;irsrHssr53ssGss5Hrsrrrss5sHsB
ss333i3rss5GsrSBSrr;;;rri5XGrrss335siAB3XH3SG35rss
GBBsrr;rssiHHAsX@srsrrssrXGMBSiX@M@BMBS3H@XBBXXB3S
3;rrBrsssGiisAMMMXGHMH3sssBM@ssAX@M@MHS@MBBXHSXAXX
Bs;3BHBsrB@Hsi3MBX@X@MBsiiHMMriiX@BAr;s;r5irrssrrs
M@@Hs;BBrSG35srsi;iisrsSs5Sii;irG;@MMMH;;i;;i;irsr
XSHXH35S;sHH33BBsA@s;rrss5rririr;;GBMMMMMMSSBG;;;i
@S@BSABBBS;XM@MMMHr;;iss3ssGBSi;;3i;;;iSMMMMMMBMMH
;iBASB@MM@@MMMsi;;;rr3sssrrsMM@MMA;;sBir;;3A@M@MMM
SHBB@XsHBXr;;;;;;srirriiirsi;;XBXBGXMMAGriX3;i;3GX" };
        static void Main(string[] args)
        {
            int i = 0;
            while (true)
            {
                if (i > 3) i = 0;
                Console.Write(anim[i]);
                //System.Threading.Thread.Sleep(1);
                Console.Clear();
                i++;
            }
            
        }
    }
}
