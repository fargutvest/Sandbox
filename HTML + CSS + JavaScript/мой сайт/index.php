<?php
###############################################################################
#                          :: All about v1.0 ::                               #
#                                                                             #
# ��������� ������������ ������� ������� � ������� ������������ ��������� ��  #
# ������                                                                      #
# �����: ������� ��������                                                     #
# e-mail: timas@hot.ee                                                        #
###############################################################################
?>
<html>

<head>
  <title>������� �������� �������</title>
</head>

<body>
<b>���������� � ������������:</b>
<?php
echo  $HTTP_USER_AGENT;          //������� ����
?>
<br>
<b>���� �����:</b>
<?php
echo $HTTP_HOST;                //���� �����
?>
<br>
<b>�������� ����������:</b>
<?php
echo $HTTP_CONNECTION;          //�������� ����������
?>
<br>
<b>���� ���������� ������������:</b>
<?php
echo $HTTP_ACCEPT_LANGUAGE;     //���� ����������
?>
<br>
<b>���� ������������:</b>
<?php
echo $REMOTE_PORT;              //port
?>
<br>
<b>IP ������������:</b>
<?php
echo $REMOTE_ADDR;              //ip
?>
<br>
<b>������ ���������� ���������� ����� �� �������:</b>
<?php
echo $PATH_TRANSLATED;          //���� � ����� �������
?>
<br>
<b>������� ���������� ���� �� �����:</b>
<?php
echo $SCRIPT_NAME;              //���������� ���������� �����
?>
<br>
<b>������������� �������:</b>
<?php
echo $SERVER_ADMIN;             //������������� �������
?>
<br>
<b>���� �������:</b>
<?php
echo $SERVER_PORT;              //port �������
?>
<br>
<?php
echo $SERVER_PROTOCOL;          //�������� �������
?>
<br>
<b>�������� �������:</b>
<?php
echo $SERVER_SIGNATURE;         //������� �������� �������
?>
<b>�������� ����� �� �������:</b>
<?php
echo $SERVER_SOFTWARE;          //�������������� ������� (�������� �������)
?>
<br>
<b>Gateway interface:</b>
<?php
echo $GATEWAY_INTERFACE;        //gateway interface
?>
<br>
<b>������ PHP �� �������:</b>
<?php
echo PHP_VERSION;               //����� ������ PHP ����� �� �������
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