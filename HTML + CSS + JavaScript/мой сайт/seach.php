<?php
define('ROOT_DIR', dirname (__FILE__));
define('CONTENT', ROOT_DIR."/content/");
if(isset($_GET['search']) && isset($_GET['post_search'])) {
$word = htmlspecialchars($_GET['search']), ENT_QUOTES);
if(isset($word) && $word != "") {
     if ($dir = @opendir(CONTENT)) {
       while (($file = readdir($dir)) !== false) {
          if ($file != "." && $file != "..") {
               $file = CONTENT.$file;
               require_once($file);
                         if(isset($content)) {
                              if(eregi($word, $content)) {
                              $string = strip_tags($content, '<a><b><i><u><br><font>');
                              echo $string;
                              $count = $count + 1;
                              }
                         } else {
                         echo "�� ���� �������� �� �����������������!";
                         }
               }
               echo "����� ������� �������: ".$count;
       }  
       closedir($dir);
     } else {
     echo "���������� ������ �� ������� ��� �� �������";
     }

}
}
?>
