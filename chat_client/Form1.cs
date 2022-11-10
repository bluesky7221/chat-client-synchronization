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

        //Name_label 대화명 표시 라벨
        //Name_textBox 대화명 입력창
        //Enter_Btn 접속 시도 버튼
        //ChatBox 대화창
        //Chat_EnterBox 대화 입력창
        //ChatClientForm 전체 폼

        TcpClient tcpClient = null;
        NetworkStream netStream = null;
        //서버와 채팅을 실행
        ChatHandler chatHandler = new ChatHandler();
        delegate void SetTextDelegate(string s);

        private void Enter_Btn_Click(object sender, EventArgs e)
        {
            if (Enter_Btn.Text == "입장")
            {
                try
                {
                    tcpClient = new TcpClient();
                    tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 2022);
                    netStream = tcpClient.GetStream();

                    chatHandler.Setup(this, netStream, this.ChatBox);
                    Thread chatThread = new Thread(new ThreadStart(chatHandler.ChatProcess));
                    chatThread.Start();

                    MessageSend("<" + Name_textBox.Text + "> 님께서 접속하셨습니다.", true);
                    Enter_Btn.Text = "나가기";
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("server error or not start \n\n" + ex.Message);
                }
            }
            else
            {
                //에러 해결해야댐
                MessageSend("<" + Name_textBox.Text + "> 님께서 접속해제 하셨습니다.", false);
                Enter_Btn.Text = "입장";

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
                //보낼 데이터를 읽어 Default 형식의 바이트 스트림으로 변환해서 전송
                string dataToSend = msg + "\r\n";
                byte[] data = Encoding.Default.GetBytes(dataToSend);
                netStream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                if (isMsg == true)
                {
                    MessageBox.Show("server is not start or \n\n" + ex.Message, "Client");
                    Enter_Btn.Text = "입장";
                    chatHandler.ChatClose();
                    tcpClient.Close();
                }
            }
        }

        private void Chat_EnterBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //key가 enter면
            if (e.KeyChar == 13)
            {
                //서버에 접속된 경우
                if (Enter_Btn.Text == "나가기")
                {
                    MessageSend("<" + Name_textBox.Text + "> " + Chat_EnterBox.Text, true);
                }
                Chat_EnterBox.Text = "";
                e.Handled = true; //이벤트 처리 중지
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
                        //SetText에서 델리게이트를 이용하여 서버에서 넘어오는 메시지를 쓴다
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