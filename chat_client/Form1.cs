using System.Net;
using System.Net.Sockets;
using System.Text;

namespace chat_client
{
    public partial class ChatClientForm : Form
    {
        public ChatClientForm()
        {
            InitializeComponent();
        }

        //Name_label ��ȭ�� ǥ�� ��
        //Name_textBox ��ȭ�� �Է�â
        //Enter_Btn ���� �õ� ��ư
        //ChatBox ��ȭâ
        //Chat_EnterBox ��ȭ �Է�â
        //ChatClientForm ��ü ��

        TcpClient tcpClient = null;
        NetworkStream netStream = null;
        //������ ä���� ����
        ChatHandler chatHandler = new ChatHandler();
        delegate void SetTextDelegate(string s);

        private void Enter_Btn_Click(object sender, EventArgs e)
        {
            if (Enter_Btn.Text == "����")
            {
                try
                {
                    tcpClient = new TcpClient();
                    tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 2022);
                    netStream = tcpClient.GetStream();

                    chatHandler.Setup(this, netStream, this.ChatBox);
                    Thread chatThread = new Thread(new ThreadStart(chatHandler.ChatProcess));
                    chatThread.Start();

                    MessageSend("<" + Name_textBox.Text + "> �Բ��� �����ϼ̽��ϴ�.", true);
                    Enter_Btn.Text = "������";
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("server error or not start \n\n" + ex.Message);
                }
            }
            else
            {
                //���� �ذ��ؾߴ�
                MessageSend("<" + Name_textBox.Text + "> �Բ��� �������� �ϼ̽��ϴ�.", false);
                Enter_Btn.Text = "����";

                chatHandler.ChatClose();
                tcpClient.Close();
            }
        }

        public void SetText(string text)
        {
            if (this.ChatBox.InvokeRequired)
            {
                SetTextDelegate d = new SetTextDelegate(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.ChatBox.AppendText(text);
            }
        }

        private void MessageSend(string msg, Boolean isMsg)
        {
            try
            {
                //���� �����͸� �о� Default ������ ����Ʈ ��Ʈ������ ��ȯ�ؼ� ����
                string dataToSend = msg + "\r\n";
                byte[] data = Encoding.Default.GetBytes(dataToSend);
                netStream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                if (isMsg == true)
                {
                    MessageBox.Show("server is not start or \n\n" + ex.Message, "Client");
                    Enter_Btn.Text = "����";
                    chatHandler.ChatClose();
                    tcpClient.Close();
                }
            }
        }

        private void Chat_EnterBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //key�� enter��
            if (e.KeyChar == 13)
            {
                //������ ���ӵ� ���
                if (Enter_Btn.Text == "������")
                {
                    MessageSend("<" + Name_textBox.Text + "> " + Chat_EnterBox.Text, true);
                }
                Chat_EnterBox.Text = "";
                e.Handled = true; //�̺�Ʈ ó�� ����
            }
        }
    }

    public class ChatHandler
    {
        private TextBox ChatBox;
        private NetworkStream netStream;
        private StreamReader strReader;
        private ChatClientForm form;

        public void Setup(ChatClientForm form, NetworkStream netStream, TextBox ChatBox)
        {
            this.ChatBox = ChatBox;
            this.netStream = netStream;
            this.form = form;
            this.strReader = new StreamReader(netStream);
        }

        public void ChatProcess()
        {
            while(true)
            {
                try
                {
                    string message = strReader.ReadLine();

                    if (message != null && message != "")
                    {
                        //SetText���� ��������Ʈ�� �̿��Ͽ� �������� �Ѿ���� �޽����� ����
                        form.SetText(message + "\r\n");
                    }
                }
                catch (System.Exception)
                {
                    break;
                }
            }
        }

        public void ChatClose()
        {
            netStream.Flush();
            netStream.Close();
            strReader.Close();
        }
    }
}