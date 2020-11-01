using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace server
{
    public partial class Form1 : Form
    {
        static TcpListener tcpListener; //монитор подключений TCP клиентов
        static Thread listenThread; //создание потока

        static List<TcpClient> clients = new List<TcpClient>(); //список клиентских подключений
        static List<NetworkStream> netStream = new List<NetworkStream>(); //список потока данных
 

        public Form1()
        {
            InitializeComponent();
            try
            {
                richTextBox1.AppendText("Server Started!\r\n");
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                listenThread = new Thread(new ThreadStart(ListenThread));
                listenThread.Start(); //старт потока
            }
            catch
            {
                Disconnect();
            }

        }

         void ListenThread()
        {
            tcpListener.Start();
 
            while (true)
            {
                clients.Add(tcpListener.AcceptTcpClient()); //подключение пользователя
                netStream.Add(clients[clients.Count - 1].GetStream()); //обьект для получения данных
                Thread clientThread = new Thread(new ParameterizedThreadStart(ClientRecieve));
                clientThread.Start(clients.Count - 1);
            }
        }
 
         void ClientRecieve(object ID)
        {
            int thisID = (int)ID;
 
            byte[] recieve = new byte[64];
 
            while (true)
            {
                try
                {
                    netStream[thisID].Read(recieve, 0, 64);
                }
                catch
                {
                    richTextBox1.AppendText("Client with ID: " + thisID + " has Disconnected!\r\n");
                    break;
                }
                richTextBox1.AppendText(thisID + " message: " + Encoding.ASCII.GetString(recieve)+"\r\n");
                SendMessageToClients(recieve);
                for (int i = 0; i < 64; i++)
                {
                    recieve[i] = 0;
                }
            }
        }
 
        static void SendMessageToClients(byte[] toSend)
        {
            for (int i = 0; i < netStream.Count; i++)
            {
                netStream[i].Write(toSend, 0, 64); //передача данных
                netStream[i].Flush(); //удаление данных с потока
            }
        }
 
        static void Disconnect()
        {
            tcpListener.Stop(); //остановка чтения
 
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
                netStream[i].Close(); //отключение потока
            }
            Environment.Exit(0); //завершение процесса
        }

       
    }



    }

