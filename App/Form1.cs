using App.Datas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public static class Own
    {
        private static string author = "";
        private static string room = "Default";

        public static string Author { get => author; set => author = value; }
        public static string Room { get => room; set => room = value; }
    }

    public partial class Form1 : Form
    {
        UDPSender udp_s;
        UDPListener udp_l;
        Thread listen;
        Thread listenNick;
        Thread empty;
        Thread updateRU;
        string tmpAuthor = "";
        Stopwatch stopwatch = new Stopwatch();
        bool isHello = false;

        List<string> roomsList = new List<string>();
        List<string> userList = new List<string>();


        public string TmpAuthor { get => tmpAuthor; set => tmpAuthor = value; }
        public Thread Listen { get => listen; set => listen = value; }
        public Thread ListenNick { get => listenNick; set => listenNick = value; }
        public List<string> RoomsList { get => roomsList; set => roomsList = value; }
        public List<string> UserList { get => userList; set => userList = value; }

        public Form1()
        {
            InitializeComponent();

            input.KeyDown += new KeyEventHandler(input_KeyPressed);
            udp_s = new UDPSender();
            udp_l = new UDPListener(this);

            listen = new Thread(udp_l.Listen);
            empty = new Thread(SendEmptyData);
            updateRU = new Thread(UpdateRoomsAndUsers);

            empty.Start();
            updateRU.Start();

            output.SelectionColor = Color.Gray;
            output.AppendText("Enter NICK\r\n");
            output.SelectionColor = Color.Black;
        }
        
        private void input_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InputParser();
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            InputParser();
        }


        public void InputParser()
        {
            if (Own.Author == "" && input.Text != "")
            {
                tmpAuthor = input.Text;
                input.Enabled = false;

                listenNick = new Thread(udp_l.ListenNick);
                listenNick.Start();

                byte[] data = DataSaver.Save(new DataNick(input.Text));
                udp_s.Send(data);
            }
            else if (input.Text != "" && input.Text.Length > 4 && input.Text.Substring(0, 4).ToUpper() == "JOIN")
            {
                byte[] data = DataSaver.Save(new DataLeftRoom());
                udp_s.Send(data);
                Own.Room = input.Text.Substring(5);
                data = DataSaver.Save(new DataJoinRoom());
                udp_s.Send(data);
            }
            else if (input.Text != "" && input.Text.Length > 4 && input.Text.Substring(0, 4).ToUpper() == "LEFT")
            {
                byte[] data = DataSaver.Save(new DataLeftRoom());
                udp_s.Send(data);
                Own.Room = "Default";
                data = DataSaver.Save(new DataJoinRoom());
                udp_s.Send(data);
            }
            else if (input.Text != "" && input.Text.Length >= 5 && input.Text.Substring(0, 5).ToUpper() == "CLEAR")
            {
                output.Text = "";
            }
            else if (input.Text != "")
            {
                tmpAuthor = input.Text;
                byte[] data = DataSaver.Save(new DataMSG(input.Text));
                udp_s.Send(data);
            }

            input.Text = "";

            sendButton.Focus();
        }

        public void OutputParser(byte[] data)
        {
            Data tmp = DataLoader.Load(data);

            if(tmp is DataNick)
            {
                if (output.InvokeRequired && tmp.Value.Contains(tmpAuthor) && tmp.Value.Contains("BUSY") && tmpAuthor!="")
                {
                    output.Invoke((MethodInvoker)delegate
                    {
                        output.SelectionBackColor = Color.Gray;
                        output.AppendText("NICK " + tmp.Value + "\r\n");
                        output.SelectionBackColor = Color.White;
                        tmpAuthor = "";
                    });
                    EnableInput(true);
                    output.SelectionColor = Color.Gray;
                    output.AppendText("Enter NICK\r\n");
                    output.SelectionColor = Color.Black;

                    listenNick.Abort();
                }
                else if (Own.Author != "")
                {
                    if (tmp.Value == Own.Author)
                    {
                        byte[] recv = DataSaver.Save(new DataNick(tmp.Value + " BUSY"));
                        udp_s.Send(recv);
                    }
                }
            }

            else if (tmp is DataMSG)
            {
                if (output.InvokeRequired && tmp.Room == Own.Room)
                {
                    output.Invoke((MethodInvoker)delegate
                    {
                        if (tmp.Author.Equals(Own.Author)) output.SelectionBackColor = Color.LightGreen;
                        else output.SelectionBackColor = Color.White;

                        output.AppendText(tmp.Author + " ");
                        output.AppendText("[" + tmp.Room + "] ");
                        output.SelectionBackColor = Color.White;
                        output.AppendText(tmp.Value + "\r\n");
                    });
                }
                else if (tmp.Room == Own.Room)
                {
                    if (tmp.Author.Equals(Own.Author)) output.SelectionBackColor = Color.LightGreen;
                    else output.SelectionBackColor = Color.White;

                    output.AppendText(tmp.Author + " ");
                    output.AppendText("[" + tmp.Room + "] ");
                    output.SelectionBackColor = Color.White;
                    output.AppendText(tmp.Value + "\r\n");
                }
            }

            else if (tmp is DataInfo)
            {
                if(!roomsList.Contains(tmp.Room) && tmp.Room != "") roomsList.Add(tmp.Room);

                if (tmp.Room == Own.Room && !userList.Contains(tmp.Author + tmp.Value))
                {
                    userList.Remove(tmp.Author);
                    userList.Remove(tmp.Author + " (pisze...)");
                    userList.Add(tmp.Author + tmp.Value);
                }
            }


            else if (tmp is DataJoinRoom)
            {
                if (output.InvokeRequired && tmp.Room == Own.Room)
                {
                    output.Invoke((MethodInvoker)delegate
                    {
                        output.SelectionColor = Color.Gray;
                        output.AppendText(tmp.Author + " JOIN to " + tmp.Room + "\r\n");
                        output.SelectionColor = Color.Black;
                    });
                }
                else if (tmp.Room == Own.Room)
                {
                    output.SelectionColor = Color.Gray;
                    output.AppendText(tmp.Author + " JOIN to " + tmp.Room + "\r\n");
                    output.SelectionColor = Color.Black;
                }
            }

            else if (tmp is DataLeftRoom)
            {
                if (output.InvokeRequired && tmp.Room == Own.Room)
                {
                    output.Invoke((MethodInvoker)delegate
                    {
                        output.SelectionColor = Color.Gray;
                        output.AppendText(tmp.Author + " LEFT " + tmp.Room + "\r\n");
                        output.SelectionColor = Color.Black;
                    });
                }
                else if (tmp.Room == Own.Room)
                {
                    output.SelectionColor = Color.Gray;
                    output.AppendText(tmp.Author + " LEFT " + tmp.Room + "\r\n");
                    output.SelectionColor = Color.Black;
                }
            }

            if (isHello==false && Own.Author != "")
            {

                if (output.InvokeRequired)
                {
                    output.Invoke((MethodInvoker)delegate
                    {
                        output.SelectionColor = Color.Gray;
                        output.AppendText("Hello " + Own.Author +"\r\n");
                        output.SelectionColor = Color.Black;
                    });
                }
                else
                {
                    output.SelectionColor = Color.Gray;
                    output.AppendText("Hello " + Own.Author + "\r\n");
                    output.SelectionColor = Color.Black;
                }

                isHello = true;
            }
        }

        public void EnableInput(bool value)
        {
            if (input.InvokeRequired)
            {
                input.Invoke((MethodInvoker)delegate
                {
                    input.Enabled = value;
                });
            }
            else
            {
                input.Enabled = value;
            }
        }




        private void SendEmptyData()
        {
            while (true)
            {
                string value = "";
                if (input.InvokeRequired)
                {
                    input.Invoke((MethodInvoker)delegate
                    {
                        if (input.Focused && Own.Author != "") value = " (pisze...)";
                    });
                }
                else
                {
                    if (input.Focused && Own.Author!="") value = " (pisze...)";
                }
                byte[] data = DataSaver.Save(new DataInfo(value));
                udp_s.Send(data);

                Thread.Sleep(500);
            }
        }

        private void UpdateRoomsAndUsers()
        {
            while (true)
            {
                if (rooms.InvokeRequired)
                {
                    rooms.Invoke((MethodInvoker)delegate
                    {
                        rooms.Text = "";
                        foreach (string s in roomsList)
                        {
                            if (s.Equals(Own.Room)) rooms.SelectionBackColor = Color.LightGreen;
                            else rooms.SelectionBackColor = Color.White;

                            rooms.AppendText(s);
                            rooms.SelectionBackColor = Color.White;
                            rooms.AppendText("\r\n");
                        }
                    });
                }
                else
                {
                    rooms.Text = "";
                    foreach (string s in roomsList)
                    {
                        if (s.Equals(Own.Room)) rooms.SelectionBackColor = Color.LightGreen;
                        else rooms.SelectionBackColor = Color.White;

                        rooms.AppendText(s);
                        rooms.SelectionBackColor = Color.White;
                        rooms.AppendText("\r\n");
                    }
                }

                if (users.InvokeRequired)
                {
                    users.Invoke((MethodInvoker)delegate
                    {
                        users.Text = "";
                        foreach (string s in userList)
                        {
                            if (s.Contains(Own.Author)) users.SelectionBackColor = Color.LightGreen;
                            else users.SelectionBackColor = Color.White;

                            users.AppendText(s);
                            users.SelectionBackColor = Color.White;
                            users.AppendText("\r\n");
                        }
                    });
                }
                else
                {
                    users.Text = "";
                    foreach (string s in userList)
                    {
                        if (s.Contains(Own.Author)) users.SelectionBackColor = Color.LightGreen;
                        else users.SelectionBackColor = Color.White;

                        users.AppendText(s);
                        users.SelectionBackColor = Color.White;
                        users.AppendText("\r\n");
                    }
                }

                roomsList.Clear();
                userList.Clear();

                Thread.Sleep(2000);
            }
        }


        
    }
}
