<?php
###############################################################################
#                          :: All about v1.0 ::                               #
#                                                                             #
# Программа сканирования системы сервера и системы пользователя входящего на  #
# сервер                                                                      #
# Автор: Тимофей Пуговкин                                                     #
# e-mail: timas@hot.ee                                                        #
###############################################################################
?>
<html>

<head>
  <title>Скрипты описания системы</title>
</head>

<body>
<b>Информация о пользователе:</b>
<?php
echo  $HTTP_USER_AGENT;          //краткая инфа
?>
<br>
<b>Хост сайта:</b>
<?php
echo $HTTP_HOST;                //хост сайта
?>
<br>
<b>Скорость соединения:</b>
<?php
echo $HTTP_CONNECTION;          //скорость соединения
?>
<br>
<b>Язык интерфейса пользователя:</b>
<?php
echo $HTTP_ACCEPT_LANGUAGE;     //язык интерфейса
?>
<br>
<b>Порт пользователя:</b>
<?php
echo $REMOTE_PORT;              //port
?>
<br>
<b>IP пользователя:</b>
<?php
echo $REMOTE_ADDR;              //ip
?>
<br>
<b>Точная директория нахождения файла на сервере:</b>
<?php
echo $PATH_TRANSLATED;          //путь к файлу сервера
?>
<br>
<b>Краткая директория того же файла:</b>
<?php
echo $SCRIPT_NAME;              //директория нахождения файла
?>
<br>
<b>Администратор сервера:</b>
<?php
echo $SERVER_ADMIN;             //администратор сервера
?>
<br>
<b>Порт сервера:</b>
<?php
echo $SERVER_PORT;              //port сервера
?>
<br>
<?php
echo $SERVER_PROTOCOL;          //протокол сервера
?>
<br>
<b>Протокол сервера:</b>
<?php
echo $SERVER_SIGNATURE;         //краткое описание сервера
?>
<b>Описание софта на сервере:</b>
<?php
echo $SERVER_SOFTWARE;          //характеристики сервера (описание системы)
?>
<br>
<b>Gateway interface:</b>
<?php
echo $GATEWAY_INTERFACE;        //gateway interface
?>
<br>
<b>Версия PHP на сервере:</b>
<?php
echo PHP_VERSION;               //какая версия PHP стоит на сервере
?>
<br>
<b>PHP OS</b>
<?php
echo PHP_OS
?>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>


</body>

</html>