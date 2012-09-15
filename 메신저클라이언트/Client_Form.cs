using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CRMmanager;
using Microsoft.Win32;

namespace Client
{
    /// <summary>
    /// Form1�� ���� ��� �����Դϴ�.
    /// </summary>
    public class Client_Form : System.Windows.Forms.Form
    {

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private string[] news = new string[10];
        private DirectoryInfo di = new DirectoryInfo(@"C:\MiniCTI");
        private DirectoryInfo memodi = null;
        private DirectoryInfo dialogdi = null;
        private DirectoryInfo FilesDir = null;
        private DirectoryInfo privatefolder = null;
        private DirectoryInfo MonthFolder = null;
        private DirectoryInfo DayFolder = null;
        public string date = DateTime.Now.ToShortDateString();
        private FileInfo fileInfo = null;
        private string myname = null;
        private string myid = null;
        private string mypass = null;
        private string serverIP = null;
        private string socket_port_crm = null;
        private string MadeNoticeDetail = null;
        private string extension = null;
        private string com_cd = null;
        private string FtpHost = null;
        private string FtpUsername = null;
        private string passwd = null;
        private string version = null;
        private string updaterDir = null;
        private string tempFolder = null;
        private int FtpPort = 0;
        private string custom_font = null;
        private string custom_color = null;
        private string top = null;

        private bool isMemo = false;
        private bool isFile = false;
        private bool isNotice = false; //������ ���� ����Ʈ ���� �̹� ���� �ִ���
        private bool isMadeNoticeResult = false;
        private bool isNoticeListAll = false;
        private bool noActive = false;
        private bool nopop = false;
        private bool isHide = false;
        private bool firstCall = false;
        public XmlDocument xmldoc = new XmlDocument();
        private Point mousePoint = Point.Empty;
        private Color labelColor;

        private IPAddress local = null;
        private static PopForm popform = null;
        private MissedCallForm missedcallform = null;
        private MissedCallList missedlistform = null;
        private SetAutoStartForm configform = null;
        private SetServer_Form setserverform = null;
        private NotifyForm notifyform = null;
        private AboutForm aboutform = null;
        private ToolTip tooltip = new ToolTip();
        private System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerForNotify = new System.Windows.Forms.Timer();

        private NotReadMemoForm notreadmemoform = null;
        private NotReadNoticeForm notreadnoticeform = null;
        private NotReceiveFileForm notreceivefileform = null;
        private NoticeResultForm noticeresultform = null;
        private NoticeListForm noticelistform = null;
        private NoReceiveBoardForm noreceiveboardform = null;
        private MemoListForm memolistform = null;
        private DownloadResultForm downloadform = null;
        private DialogListForm dialoglistform = null;
        private SetExtensionForm extform = null;
        private SelectTransferForm selecttransferform = null;
        private RequestUpdate request = null;
        private int mainform_width = 0;
        private int mainform_height = 0;
        private int missedCallCount = 0;
        private int screenWidth = 0;
        private int screenHeight = 0;
        private int mainform_x = 0;
        private int mainform_y = 0;


        private int listenport = 8883;
        private int sendport = 8884;
        private int checkport = 8886;
        private int filereceiveport = 9003;
        private int filesendport = 9004;
        private Socket ServerSocket = null;
        private UdpClient listenSock = null;
        private UdpClient sendSock = null;
        private UdpClient checkSock = null;
        private UdpClient filesock = null;
        private UdpClient filesendSock = null;
        private IPEndPoint server = null;
        private IPEndPoint filesender = null;
        private IPEndPoint filesend = null;

        private Thread receive = null;
        private Thread checkThread = null;
        private Hashtable ChatFormList = new Hashtable();  //ä��â  key=id, value=chatform
        private Hashtable MemoFormList = new Hashtable(); //key=time, value=SendMemoForm
        private Hashtable TeamInfoList = new Hashtable(); //key=id, value=team
        private Hashtable InList = new Hashtable();       //key=id, value=IPEndPoint
        private Hashtable MemberInfoList = new Hashtable();
        private Hashtable FileSendDetailList = new Hashtable();
        private Hashtable FileSendFormList = new Hashtable();
        private Hashtable FileSendThreadList = new Hashtable();
        private Hashtable FileReceiverThreadList = new Hashtable();
        private Hashtable NoticeDetailForm = new Hashtable();
        private Hashtable TransferNotiArea = new Hashtable();
        private Hashtable CustomerList = new Hashtable();
        private ArrayList NotiFormList = new ArrayList();
        private ArrayList TransferNotiFormTable = new ArrayList();
        private Hashtable treesource = new Hashtable();
        private ArrayList MemoTable = new ArrayList();
        private ArrayList omitteamlist = new ArrayList();
        private bool connected = false;
        private bool serverAlive = true;
        private bool receiverStart = false;
        private Color txtcolor = Color.Blue;
        private Font txtfont = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
        private Label name;
        private Label team;
        private ContextMenuStrip mouseMenuG;
        private ToolStripMenuItem StripMn_gmemo;
        private ToolStripMenuItem StripMn_gfile;
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip mouseMenuN;
        private ToolStripMenuItem StripMn_memo;
        private ToolStripMenuItem StripMn_file;
        private log Log = new log();
        private ToolStripMenuItem StripMn_gchat;
        private ToolStripMenuItem StripMn_chat;
        private ImageList imageList;
        private TextBox id;
        private NotifyIcon notifyIcon;
        private Label NRmemo;
        private Label NRfile;
        private Label NRnotice;
        private PictureBox pic_NRmemo;
        private PictureBox pic_NRnotice;
        private PictureBox pic_NRfile;
        private PictureBox pic_title;
        private Panel default_panal;
        private Label label_id;
        private Label label_pass;
        private TextBox tbx_pass;
        private Panel panel_logon;
        private StatusStrip statusStrip1;
        private Label label_stat;
        private ContextMenuStrip mouseMenuStat;
        private ToolStripMenuItem StripMn_online;
        private ToolStripMenuItem StringMn_away;
        private ToolStripMenuItem StripMn_DND;
        private CheckBox cbx_pass_save;
        private Panel panel_progress;
        private Label label_progress_status;
        private PictureBox pictureBox1;
        private PictureBox pbx_loginCancel;
        private ContextMenuStrip menu_notifyicon;
        private ToolStripMenuItem Mn_notify_show;
        private ToolStripMenuItem Mn_notify_dispose;
        private ContextMenuStrip TM_file_sub;
        private ToolStripMenuItem TM_file_logout;
        private ToolStripMenuItem TM_file_exit;
        private ContextMenuStrip TM_motion_sub;
        private ToolStripMenuItem TM_motion_chat;
        private ToolStripMenuItem TM_motion_memo;
        private ToolStripMenuItem TM_motion_sendfile;
        private ToolStripMenuItem TM_motion_sendnotice;
        private ContextMenuStrip TM_option_sub;
        private ToolStripMenuItem TM_option_default;
        private ToolStripMenuItem TM_option_extension;
        private ToolStripMenuItem TM_option_server;
        private ContextMenuStrip TM_help_sub;
        private ToolStripMenuItem TM_help_show;
        private Button btn_login;
        private Button btn_dialoguebox;
        private Button btn_memobox;
        private Button btn_board;
        private Button btn_crm;
        private Button btn_sendnotice;
        private Label label2;
        private Label label1;
        private Button btn_resultnotice;
        private Label label3;
        private TreeView memTree;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem MnFile;
        private ToolStripMenuItem MnLogout;
        private ToolStripMenuItem MnExit;
        private ToolStripMenuItem MnMotion;
        private ToolStripMenuItem MnMemo;
        private ToolStripMenuItem MnDialogue;
        private ToolStripMenuItem MnNotice;
        private ToolStripMenuItem MnSendFile;
        private ToolStripMenuItem MnSetting;
        private ToolStripMenuItem Mn_default;
        private ToolStripMenuItem Mn_extension;
        private ToolStripMenuItem Mn_server;
        private ToolStripMenuItem MnHelp;
        private ToolStripMenuItem MnShowHelp;
        private PictureBox pictureBox2;
        private WebBrowser webBrowser1;
        private ToolStripMenuItem StripMn_busy;
        private OpenFileDialog openFileDialog;

        //�����忡�� �� ȣ��� ��������Ʈ
        private delegate void WriteLog(string m);
        private delegate void PanelCtrlDelegate(bool l);
        private delegate void FormTextCtrlDelegate(string c);
        private delegate void ArrangeCtrlDelegate(string[] ar);
        private delegate void ChildNodeDelegate(string st, ArrayList list);
        private delegate void AddChatMsg(string msg, ChatForm form);
        private delegate void ChangeStat(string name, string team);
        private delegate void Loginfail();
        private delegate void LogOutDelegate();
        private delegate int onFlashWindow(ChatForm form);
        private delegate string[] GetTeam();
        private delegate Hashtable GetMember(string teamname);
        private delegate Hashtable GetAllMemberDelegate();
        private delegate void DelChatterDelegate(string id, ChatForm form);
        private delegate string[] GetChatters(ChatForm form);
        private delegate void AddChatter(string id, string name, ChatForm form);
        private delegate void ShowFileSendStatDelegate(string stat, SendFileForm form);
        private delegate void ShowFileSendDetailDelegate(string key, string detail, FileSendDetailListView view);
        private delegate bool ShowCloseButtonDelegate(FileSendDetailListView view);
        private delegate string SaveFileDialogDelegate(string filename);
        private delegate void NoticeListSorting(int index);
        private delegate void ShowTransInfoDele(string ani, string senderid, string date, string time);

        public delegate void stringDele(string ani);
        public delegate void doublestringDele(string ani, string calltype);
        private delegate void objectDele(object obj);
        private delegate void RingingDele(string ani, string name, string server_type);
        private delegate void AbandonDele();
        public delegate void NoParamDele();
        public delegate void intParamDele(int i);

        public event stringDele onAnswerEvent;
        public event NoParamDele onLoginEvent;
        public ArrayList cmstorage = new ArrayList();
        string ANI = "";
        string crmstat = "";
        CRMmanager.CRMmanager cm;
        private SkinSoft.AquaSkin.AquaSkin aquaSkin1;
        private PictureBox pbx_stat;
        private Label label4;
        private PictureBox pic_NRtrans;
        private Label NRtrans;
        private ToolStripMenuItem weDo����ToolStripMenuItem;
        private Panel InfoBar;
        CRMmanager.FRM_MAIN crm_main;

        

        public Client_Form()
        {
            try
            {
                //
                // Windows Form �����̳� ������ �ʿ��մϴ�.
                //

                InitializeComponent();
                cm = new CRMmanager.CRMmanager();
                crm_main = new FRM_MAIN();
                //
                // TODO: InitializeComponent�� ȣ���� ���� ������ �ڵ带 �߰��մϴ�.
                //
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }

        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        private void Client_Form_Load(object sender, EventArgs e)
        {
            startForm();
        }


        private void startForm()
        {
            screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.SetBounds(screenWidth - this.Width, 0, this.Width, this.Height);
            readconfig();
            LogFileCheck();
            Thread thread = new Thread(new ThreadStart(VersionCheck));
            thread.Start();
            logWrite("LogFileCheck() �Ϸ�" + DateTime.Now.ToString());
            setLoginInfo();
            Microsoft.Win32.SystemEvents.SessionEnding += new Microsoft.Win32.SessionEndingEventHandler(SystemEvents_SessionEnding);
            t1.Interval = 10000;
            t1.Tick += new EventHandler(t1_Tick);
            timerForNotify.Interval = 10000;
            timerForNotify.Tick += new EventHandler(timerForNotify_Tick);
            makeTransferNotiArea();
            StartService();

        }




        private void makeTransferNotiArea()
        {
            try
            {
                int remnantArea = screenHeight;
                int end = screenHeight - 48;
                TransferNotiArea[end.ToString()] = "0";
                for (int i = 1; i < 5; i++)
                {
                    end -= 48;
                    TransferNotiArea[end.ToString()] = "0";
                    logWrite("TransferNotiArea[" + end.ToString() + "]");
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        void Client_Form_Validating(object sender, CancelEventArgs e)
        {

        }

        private void killInitWD()
        {
            try
            {
                Process[] pros = Process.GetProcessesByName("InitWD");
                if (pros.Length > 0)
                {
                    foreach (Process pro in pros)
                    {
                        pro.Kill();
                    }
                }
                else
                {
                    logWrite("InitWD ���μ��� ��ã��");
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// app.config xml �������� ���� �ε�
        /// </summary>
        private void readconfig()
        {
            try
            {
                FtpHost = System.Configuration.ConfigurationSettings.AppSettings["FtpHost"].ToString();
                tempFolder = System.Configuration.ConfigurationSettings.AppSettings["FtpLocalFolder"].ToString();
                passwd = System.Configuration.ConfigurationSettings.AppSettings["FtpPass"].ToString();
                FtpPort = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["FtpPort"].ToString());
                FtpUsername = System.Configuration.ConfigurationSettings.AppSettings["FtpUserName"].ToString();
                updaterDir = System.Configuration.ConfigurationSettings.AppSettings["UpdaterDir"].ToString();
                version = System.Configuration.ConfigurationSettings.AppSettings["FtpVersion"].ToString();
                top = System.Configuration.ConfigurationSettings.AppSettings["topmost"].ToString();
                string temp = System.Configuration.ConfigurationSettings.AppSettings["nopop"].ToString();
                if (temp.Equals("1"))
                {
                    this.nopop = true;
                }

                if (top.Equals("1"))
                {
                    this.TopMost = true;
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void VersionCheck()
        {
            bool isUpdate = false;
            try
            {

                Uri ftpuri = new Uri(FtpHost);
                FtpWebRequest wr = (FtpWebRequest)WebRequest.Create(ftpuri);
                wr.Method = WebRequestMethods.Ftp.ListDirectory;
                wr.Credentials = new NetworkCredential(FtpUsername, passwd);
                FtpWebResponse wres = (FtpWebResponse)wr.GetResponse();
                Stream st = wres.GetResponseStream();
                string SVRver = null;

                if (st.CanRead)
                {
                    StreamReader sr = new StreamReader(st);
                    SVRver = sr.ReadLine();
                }

                logWrite("Server Version = " + SVRver);
                logWrite("Client Version = " + version);

                if (SVRver.Equals(version.Trim()))
                {
                    version = SVRver;

                    logWrite("Last Version is already Installed!");
                }
                else
                {
                    string[] ver = SVRver.Split('.');
                    string[] now = version.Split('.');
                    for (int v = 0; v < ver.Length; v++)
                    {
                        if (!ver[v].Equals(now[v]))
                        {
                            if (Convert.ToInt32(ver[v]) > Convert.ToInt32(now[v]))
                            {
                                NoParamDele dele = new NoParamDele(requestUpdate);
                                Invoke(dele);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
            noActive = true;
            //return isUpdate;
        }

        private void requestUpdate()
        {
            request = new RequestUpdate();
            request.btn_no.MouseClick += new MouseEventHandler(btn_no_MouseClick);
            request.btn_yes.MouseClick += new MouseEventHandler(btn_yes_MouseClick);
            request.Show();
        }

        private void btn_yes_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (connected == true)
                {
                    LogOut();
                }
                this.notifyIcon.Visible = false;
                logWrite("Update Start!!" + "  " + DateTime.Now.ToShortTimeString());
                System.Diagnostics.Process.Start(updaterDir);
                logWrite("FtpHost : " + FtpHost);
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void btn_no_MouseClick(object sender, MouseEventArgs e)
        {
            if (request != null)
            {
                request.Close();
            }
        }

        private void setLoginInfo()
        {
            try
            {
                id.Text = System.Configuration.ConfigurationSettings.AppSettings["id"];
                extension = System.Configuration.ConfigurationSettings.AppSettings["extension"];
                custom_font = System.Configuration.ConfigurationSettings.AppSettings["custom_font"];
                custom_color = System.Configuration.ConfigurationSettings.AppSettings["custom_color"];

                if (System.Configuration.ConfigurationSettings.AppSettings["save_pass"].Equals("1"))
                {
                    tbx_pass.Text = System.Configuration.ConfigurationSettings.AppSettings["pass"];
                    cbx_pass_save.Checked = true;
                }

                //if (custom_color.Length > 1)
                //{
                //    txtcolor = getCustomColor();
                //}
                //if (custom_font.Length > 1)
                //{
                //    txtfont = getCustomFont();
                //}
            }
            catch (Exception ex)
            {
                logWrite("setLoginInfo Error : " + ex.ToString());
            }
        }

        private void t1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (popform != null)
                {
                    popform.Close();
                    t1.Stop();
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }




        private void SystemEvents_SessionEnding(object sender, Microsoft.Win32.SessionEndingEventArgs e)
        {
            try
            {
                //MessageBox.Show("�ý��� ����");
                if (connected == true)
                {
                    LogOut();
                    Process.GetCurrentProcess().Kill();
                }
                else
                {

                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        /// <summary>
        /// ��� ���� ��� ���ҽ��� �����մϴ�.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    if (connected == true)
                        closing();
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// �����̳� ������ �ʿ��� �޼����Դϴ�.
        /// �� �޼����� ������ �ڵ� ������� �������� ���ʽÿ�.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client_Form));
            this.mouseMenuG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StripMn_gchat = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMn_gmemo = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMn_gfile = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.mouseMenuN = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StripMn_chat = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMn_memo = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMn_file = new System.Windows.Forms.ToolStripMenuItem();
            this.id = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.default_panal = new System.Windows.Forms.Panel();
            this.panel_progress = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_progress_status = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.pbx_loginCancel = new System.Windows.Forms.PictureBox();
            this.cbx_pass_save = new System.Windows.Forms.CheckBox();
            this.pic_title = new System.Windows.Forms.PictureBox();
            this.label_pass = new System.Windows.Forms.Label();
            this.tbx_pass = new System.Windows.Forms.TextBox();
            this.label_id = new System.Windows.Forms.Label();
            this.panel_logon = new System.Windows.Forms.Panel();
            this.InfoBar = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pic_NRtrans = new System.Windows.Forms.PictureBox();
            this.NRtrans = new System.Windows.Forms.Label();
            this.pbx_stat = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_stat = new System.Windows.Forms.Label();
            this.pic_NRfile = new System.Windows.Forms.PictureBox();
            this.pic_NRnotice = new System.Windows.Forms.PictureBox();
            this.pic_NRmemo = new System.Windows.Forms.PictureBox();
            this.NRfile = new System.Windows.Forms.Label();
            this.NRnotice = new System.Windows.Forms.Label();
            this.NRmemo = new System.Windows.Forms.Label();
            this.team = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_resultnotice = new System.Windows.Forms.Button();
            this.memTree = new System.Windows.Forms.TreeView();
            this.btn_sendnotice = new System.Windows.Forms.Button();
            this.btn_dialoguebox = new System.Windows.Forms.Button();
            this.btn_memobox = new System.Windows.Forms.Button();
            this.btn_board = new System.Windows.Forms.Button();
            this.btn_crm = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mouseMenuStat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StripMn_online = new System.Windows.Forms.ToolStripMenuItem();
            this.StringMn_away = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMn_DND = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMn_busy = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_notifyicon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Mn_notify_show = new System.Windows.Forms.ToolStripMenuItem();
            this.Mn_notify_dispose = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_file_sub = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TM_file_logout = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_file_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_motion_sub = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TM_motion_chat = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_motion_memo = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_motion_sendfile = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_motion_sendnotice = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_option_sub = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TM_option_default = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_option_extension = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_option_server = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_help_sub = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TM_help_show = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MnFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MnLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.MnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MnMotion = new System.Windows.Forms.ToolStripMenuItem();
            this.MnMemo = new System.Windows.Forms.ToolStripMenuItem();
            this.MnDialogue = new System.Windows.Forms.ToolStripMenuItem();
            this.MnNotice = new System.Windows.Forms.ToolStripMenuItem();
            this.MnSendFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.Mn_default = new System.Windows.Forms.ToolStripMenuItem();
            this.Mn_extension = new System.Windows.Forms.ToolStripMenuItem();
            this.Mn_server = new System.Windows.Forms.ToolStripMenuItem();
            this.MnHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MnShowHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.weDo����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aquaSkin1 = new SkinSoft.AquaSkin.AquaSkin(this.components);
            this.mouseMenuG.SuspendLayout();
            this.mouseMenuN.SuspendLayout();
            this.default_panal.SuspendLayout();
            this.panel_progress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_loginCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_title)).BeginInit();
            this.panel_logon.SuspendLayout();
            this.InfoBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NRtrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_stat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NRfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NRnotice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NRmemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.mouseMenuStat.SuspendLayout();
            this.menu_notifyicon.SuspendLayout();
            this.TM_file_sub.SuspendLayout();
            this.TM_motion_sub.SuspendLayout();
            this.TM_option_sub.SuspendLayout();
            this.TM_help_sub.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aquaSkin1)).BeginInit();
            this.SuspendLayout();
            // 
            // mouseMenuG
            // 
            this.mouseMenuG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMn_gchat,
            this.StripMn_gmemo,
            this.StripMn_gfile});
            this.mouseMenuG.Name = "mouseMenuG";
            this.mouseMenuG.Size = new System.Drawing.Size(163, 70);
            // 
            // StripMn_gchat
            // 
            this.StripMn_gchat.Name = "StripMn_gchat";
            this.StripMn_gchat.Size = new System.Drawing.Size(162, 22);
            this.StripMn_gchat.Text = "�׷� ��ȭ�ϱ�";
            this.StripMn_gchat.Click += new System.EventHandler(this.chat_Click);
            // 
            // StripMn_gmemo
            // 
            this.StripMn_gmemo.Name = "StripMn_gmemo";
            this.StripMn_gmemo.Size = new System.Drawing.Size(162, 22);
            this.StripMn_gmemo.Text = "�׷� ����������";
            this.StripMn_gmemo.Click += new System.EventHandler(this.StripMn_gmemo_Click);
            // 
            // StripMn_gfile
            // 
            this.StripMn_gfile.Name = "StripMn_gfile";
            this.StripMn_gfile.Size = new System.Drawing.Size(162, 22);
            this.StripMn_gfile.Text = "�׷� ���Ϻ�����";
            this.StripMn_gfile.Click += new System.EventHandler(this.StripMn_gfile_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "�α׾ƿ�.png");
            this.imageList.Images.SetKeyName(1, "�¶���.png");
            this.imageList.Images.SetKeyName(2, "ȭ��ǥ�Ʒ�_6.png");
            this.imageList.Images.SetKeyName(3, "ȭ��ǥ��_6.png");
            this.imageList.Images.SetKeyName(4, "������.png");
            this.imageList.Images.SetKeyName(5, "�ٸ��빫��.png");
            this.imageList.Images.SetKeyName(6, "��ȭ��.png");
            // 
            // mouseMenuN
            // 
            this.mouseMenuN.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMn_chat,
            this.StripMn_memo,
            this.StripMn_file});
            this.mouseMenuN.Name = "mouseMenuG";
            this.mouseMenuN.Size = new System.Drawing.Size(135, 70);
            // 
            // StripMn_chat
            // 
            this.StripMn_chat.Name = "StripMn_chat";
            this.StripMn_chat.Size = new System.Drawing.Size(134, 22);
            this.StripMn_chat.Text = "��ȭ�ϱ�";
            this.StripMn_chat.Click += new System.EventHandler(this.chat_Click);
            // 
            // StripMn_memo
            // 
            this.StripMn_memo.Name = "StripMn_memo";
            this.StripMn_memo.Size = new System.Drawing.Size(134, 22);
            this.StripMn_memo.Text = "����������";
            this.StripMn_memo.Click += new System.EventHandler(this.StripMn_memo_Click);
            // 
            // StripMn_file
            // 
            this.StripMn_file.Name = "StripMn_file";
            this.StripMn_file.Size = new System.Drawing.Size(134, 22);
            this.StripMn_file.Text = "���Ϻ�����";
            this.StripMn_file.Click += new System.EventHandler(this.StripMn_file_Click);
            // 
            // id
            // 
            this.id.BackColor = System.Drawing.SystemColors.Window;
            this.id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.id.Location = new System.Drawing.Point(118, 201);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(100, 21);
            this.id.TabIndex = 9;
            this.id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.id_KeyDown);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "����";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "WeDo �޽���";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_Click);
            // 
            // default_panal
            // 
            this.default_panal.BackColor = System.Drawing.SystemColors.Control;
            this.default_panal.Controls.Add(this.panel_progress);
            this.default_panal.Controls.Add(this.btn_login);
            this.default_panal.Controls.Add(this.pbx_loginCancel);
            this.default_panal.Controls.Add(this.cbx_pass_save);
            this.default_panal.Controls.Add(this.pic_title);
            this.default_panal.Controls.Add(this.label_pass);
            this.default_panal.Controls.Add(this.tbx_pass);
            this.default_panal.Controls.Add(this.label_id);
            this.default_panal.Controls.Add(this.id);
            this.default_panal.Location = new System.Drawing.Point(0, 0);
            this.default_panal.Name = "default_panal";
            this.default_panal.Size = new System.Drawing.Size(284, 508);
            this.default_panal.TabIndex = 24;
            // 
            // panel_progress
            // 
            this.panel_progress.BackColor = System.Drawing.Color.Transparent;
            this.panel_progress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_progress.Controls.Add(this.pictureBox1);
            this.panel_progress.Controls.Add(this.label_progress_status);
            this.panel_progress.Location = new System.Drawing.Point(99, 206);
            this.panel_progress.Name = "panel_progress";
            this.panel_progress.Size = new System.Drawing.Size(88, 86);
            this.panel_progress.TabIndex = 27;
            this.panel_progress.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label_progress_status
            // 
            this.label_progress_status.AutoSize = true;
            this.label_progress_status.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_progress_status.Location = new System.Drawing.Point(24, 68);
            this.label_progress_status.Name = "label_progress_status";
            this.label_progress_status.Size = new System.Drawing.Size(41, 12);
            this.label_progress_status.TabIndex = 1;
            this.label_progress_status.Text = "������";
            // 
            // btn_login
            // 
            this.btn_login.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_login.Location = new System.Drawing.Point(94, 319);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(93, 30);
            this.btn_login.TabIndex = 102;
            this.btn_login.Text = "�α���";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_login_MouseClick);
            // 
            // pbx_loginCancel
            // 
            this.pbx_loginCancel.BackColor = System.Drawing.Color.Transparent;
            this.pbx_loginCancel.Location = new System.Drawing.Point(115, 307);
            this.pbx_loginCancel.Name = "pbx_loginCancel";
            this.pbx_loginCancel.Size = new System.Drawing.Size(45, 25);
            this.pbx_loginCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbx_loginCancel.TabIndex = 101;
            this.pbx_loginCancel.TabStop = false;
            this.pbx_loginCancel.Visible = false;
            // 
            // cbx_pass_save
            // 
            this.cbx_pass_save.Font = new System.Drawing.Font("����", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbx_pass_save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbx_pass_save.Location = new System.Drawing.Point(118, 261);
            this.cbx_pass_save.Name = "cbx_pass_save";
            this.cbx_pass_save.Size = new System.Drawing.Size(94, 15);
            this.cbx_pass_save.TabIndex = 100;
            this.cbx_pass_save.Text = "��й�ȣ ����";
            this.cbx_pass_save.UseVisualStyleBackColor = true;
            this.cbx_pass_save.CheckedChanged += new System.EventHandler(this.cbx_pass_save_CheckedChanged);
            // 
            // pic_title
            // 
            this.pic_title.BackColor = System.Drawing.Color.Transparent;
            this.pic_title.ErrorImage = null;
            this.pic_title.Image = ((System.Drawing.Image)(resources.GetObject("pic_title.Image")));
            this.pic_title.InitialImage = null;
            this.pic_title.Location = new System.Drawing.Point(64, 76);
            this.pic_title.Name = "pic_title";
            this.pic_title.Size = new System.Drawing.Size(153, 67);
            this.pic_title.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pic_title.TabIndex = 21;
            this.pic_title.TabStop = false;
            // 
            // label_pass
            // 
            this.label_pass.AutoSize = true;
            this.label_pass.BackColor = System.Drawing.Color.Transparent;
            this.label_pass.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_pass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_pass.Location = new System.Drawing.Point(59, 232);
            this.label_pass.Name = "label_pass";
            this.label_pass.Size = new System.Drawing.Size(53, 12);
            this.label_pass.TabIndex = 99;
            this.label_pass.Text = "��й�ȣ";
            // 
            // tbx_pass
            // 
            this.tbx_pass.BackColor = System.Drawing.SystemColors.Window;
            this.tbx_pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbx_pass.Location = new System.Drawing.Point(118, 229);
            this.tbx_pass.Name = "tbx_pass";
            this.tbx_pass.PasswordChar = '��';
            this.tbx_pass.Size = new System.Drawing.Size(100, 21);
            this.tbx_pass.TabIndex = 10;
            this.tbx_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbx_pass_KeyDown);
            // 
            // label_id
            // 
            this.label_id.AutoSize = true;
            this.label_id.BackColor = System.Drawing.Color.Transparent;
            this.label_id.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_id.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_id.Location = new System.Drawing.Point(66, 206);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(41, 12);
            this.label_id.TabIndex = 27;
            this.label_id.Text = "���̵�";
            // 
            // panel_logon
            // 
            this.panel_logon.BackColor = System.Drawing.SystemColors.Control;
            this.panel_logon.Controls.Add(this.InfoBar);
            this.panel_logon.Controls.Add(this.pictureBox2);
            this.panel_logon.Controls.Add(this.btn_resultnotice);
            this.panel_logon.Controls.Add(this.memTree);
            this.panel_logon.Controls.Add(this.btn_sendnotice);
            this.panel_logon.Controls.Add(this.btn_dialoguebox);
            this.panel_logon.Controls.Add(this.btn_memobox);
            this.panel_logon.Controls.Add(this.btn_board);
            this.panel_logon.Controls.Add(this.btn_crm);
            this.panel_logon.Controls.Add(this.webBrowser1);
            this.panel_logon.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel_logon.Location = new System.Drawing.Point(-8, 24);
            this.panel_logon.Margin = new System.Windows.Forms.Padding(0);
            this.panel_logon.Name = "panel_logon";
            this.panel_logon.Size = new System.Drawing.Size(298, 507);
            this.panel_logon.TabIndex = 25;
            this.panel_logon.Visible = false;
            // 
            // InfoBar
            // 
            this.InfoBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(220)))), ((int)(((byte)(237)))));
            this.InfoBar.Controls.Add(this.label4);
            this.InfoBar.Controls.Add(this.pic_NRtrans);
            this.InfoBar.Controls.Add(this.NRtrans);
            this.InfoBar.Controls.Add(this.pbx_stat);
            this.InfoBar.Controls.Add(this.label3);
            this.InfoBar.Controls.Add(this.label2);
            this.InfoBar.Controls.Add(this.label1);
            this.InfoBar.Controls.Add(this.label_stat);
            this.InfoBar.Controls.Add(this.pic_NRfile);
            this.InfoBar.Controls.Add(this.pic_NRnotice);
            this.InfoBar.Controls.Add(this.pic_NRmemo);
            this.InfoBar.Controls.Add(this.NRfile);
            this.InfoBar.Controls.Add(this.NRnotice);
            this.InfoBar.Controls.Add(this.NRmemo);
            this.InfoBar.Controls.Add(this.team);
            this.InfoBar.Controls.Add(this.name);
            this.InfoBar.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.InfoBar.Location = new System.Drawing.Point(0, 0);
            this.InfoBar.Margin = new System.Windows.Forms.Padding(0);
            this.InfoBar.Name = "InfoBar";
            this.InfoBar.Size = new System.Drawing.Size(298, 83);
            this.InfoBar.TabIndex = 105;
            this.InfoBar.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("����", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(247, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 11);
            this.label4.TabIndex = 28;
            this.label4.Text = "����";
            this.label4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_NRtrans_MouseClick);
            // 
            // pic_NRtrans
            // 
            this.pic_NRtrans.BackColor = System.Drawing.Color.Transparent;
            this.pic_NRtrans.ErrorImage = null;
            this.pic_NRtrans.Image = ((System.Drawing.Image)(resources.GetObject("pic_NRtrans.Image")));
            this.pic_NRtrans.InitialImage = null;
            this.pic_NRtrans.Location = new System.Drawing.Point(228, 61);
            this.pic_NRtrans.Name = "pic_NRtrans";
            this.pic_NRtrans.Size = new System.Drawing.Size(17, 17);
            this.pic_NRtrans.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_NRtrans.TabIndex = 27;
            this.pic_NRtrans.TabStop = false;
            this.pic_NRtrans.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_NRtrans_MouseClick);
            this.pic_NRtrans.MouseEnter += new System.EventHandler(this.pic_NRtrans_MouseEnter);
            // 
            // NRtrans
            // 
            this.NRtrans.AutoSize = true;
            this.NRtrans.BackColor = System.Drawing.Color.Transparent;
            this.NRtrans.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NRtrans.ForeColor = System.Drawing.Color.Black;
            this.NRtrans.Location = new System.Drawing.Point(275, 64);
            this.NRtrans.Name = "NRtrans";
            this.NRtrans.Size = new System.Drawing.Size(11, 12);
            this.NRtrans.TabIndex = 26;
            this.NRtrans.Text = "0";
            // 
            // pbx_stat
            // 
            this.pbx_stat.BackColor = System.Drawing.Color.Transparent;
            this.pbx_stat.Image = global::Client.Properties.Resources.�¶���;
            this.pbx_stat.Location = new System.Drawing.Point(21, 7);
            this.pbx_stat.Name = "pbx_stat";
            this.pbx_stat.Size = new System.Drawing.Size(30, 25);
            this.pbx_stat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_stat.TabIndex = 25;
            this.pbx_stat.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("����", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(35, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "����";
            this.label3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NRmemo_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("����", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(106, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 11);
            this.label2.TabIndex = 23;
            this.label2.Text = "����";
            this.label2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_NRnotice_MouseClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("����", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(178, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 11);
            this.label1.TabIndex = 22;
            this.label1.Text = "����";
            this.label1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_NRfile_MouseClick);
            // 
            // label_stat
            // 
            this.label_stat.AutoSize = true;
            this.label_stat.BackColor = System.Drawing.Color.Transparent;
            this.label_stat.Font = new System.Drawing.Font("����", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_stat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_stat.Location = new System.Drawing.Point(55, 34);
            this.label_stat.Name = "label_stat";
            this.label_stat.Size = new System.Drawing.Size(52, 11);
            this.label_stat.TabIndex = 21;
            this.label_stat.Text = "�¶��� ��";
            this.label_stat.MouseLeave += new System.EventHandler(this.label_stat_MouseLeave);
            this.label_stat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label_stat_MouseClick);
            this.label_stat.MouseEnter += new System.EventHandler(this.label_stat_MouseEnter);
            // 
            // pic_NRfile
            // 
            this.pic_NRfile.BackColor = System.Drawing.Color.Transparent;
            this.pic_NRfile.ErrorImage = null;
            this.pic_NRfile.Image = ((System.Drawing.Image)(resources.GetObject("pic_NRfile.Image")));
            this.pic_NRfile.InitialImage = null;
            this.pic_NRfile.Location = new System.Drawing.Point(159, 61);
            this.pic_NRfile.Name = "pic_NRfile";
            this.pic_NRfile.Size = new System.Drawing.Size(17, 17);
            this.pic_NRfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_NRfile.TabIndex = 20;
            this.pic_NRfile.TabStop = false;
            this.pic_NRfile.MouseLeave += new System.EventHandler(this.pic_NRfile_MouseLeave);
            this.pic_NRfile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_NRfile_MouseClick);
            this.pic_NRfile.MouseEnter += new System.EventHandler(this.pic_NRfile_MouseEnter);
            // 
            // pic_NRnotice
            // 
            this.pic_NRnotice.BackColor = System.Drawing.Color.Transparent;
            this.pic_NRnotice.ErrorImage = null;
            this.pic_NRnotice.Image = ((System.Drawing.Image)(resources.GetObject("pic_NRnotice.Image")));
            this.pic_NRnotice.InitialImage = null;
            this.pic_NRnotice.Location = new System.Drawing.Point(89, 61);
            this.pic_NRnotice.Name = "pic_NRnotice";
            this.pic_NRnotice.Size = new System.Drawing.Size(17, 17);
            this.pic_NRnotice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_NRnotice.TabIndex = 19;
            this.pic_NRnotice.TabStop = false;
            this.pic_NRnotice.MouseLeave += new System.EventHandler(this.pic_NRnotice_MouseLeave);
            this.pic_NRnotice.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_NRnotice_MouseClick);
            this.pic_NRnotice.MouseEnter += new System.EventHandler(this.pic_NRnotice_MouseEnter);
            // 
            // pic_NRmemo
            // 
            this.pic_NRmemo.BackColor = System.Drawing.Color.Transparent;
            this.pic_NRmemo.ErrorImage = null;
            this.pic_NRmemo.Image = ((System.Drawing.Image)(resources.GetObject("pic_NRmemo.Image")));
            this.pic_NRmemo.InitialImage = null;
            this.pic_NRmemo.Location = new System.Drawing.Point(16, 61);
            this.pic_NRmemo.Name = "pic_NRmemo";
            this.pic_NRmemo.Size = new System.Drawing.Size(17, 17);
            this.pic_NRmemo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_NRmemo.TabIndex = 18;
            this.pic_NRmemo.TabStop = false;
            this.pic_NRmemo.MouseLeave += new System.EventHandler(this.pic_NRmemo_MouseLeave);
            this.pic_NRmemo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NRmemo_Click);
            this.pic_NRmemo.MouseEnter += new System.EventHandler(this.pic_NRmemo_MouseEnter);
            // 
            // NRfile
            // 
            this.NRfile.AutoSize = true;
            this.NRfile.BackColor = System.Drawing.Color.Transparent;
            this.NRfile.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NRfile.ForeColor = System.Drawing.Color.Black;
            this.NRfile.Location = new System.Drawing.Point(206, 64);
            this.NRfile.Name = "NRfile";
            this.NRfile.Size = new System.Drawing.Size(11, 12);
            this.NRfile.TabIndex = 12;
            this.NRfile.Text = "0";
            this.NRfile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_NRfile_MouseClick);
            // 
            // NRnotice
            // 
            this.NRnotice.AutoSize = true;
            this.NRnotice.BackColor = System.Drawing.Color.Transparent;
            this.NRnotice.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NRnotice.ForeColor = System.Drawing.Color.Black;
            this.NRnotice.Location = new System.Drawing.Point(134, 64);
            this.NRnotice.Name = "NRnotice";
            this.NRnotice.Size = new System.Drawing.Size(11, 12);
            this.NRnotice.TabIndex = 11;
            this.NRnotice.Text = "0";
            this.NRnotice.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_NRnotice_MouseClick);
            // 
            // NRmemo
            // 
            this.NRmemo.AutoSize = true;
            this.NRmemo.BackColor = System.Drawing.Color.Transparent;
            this.NRmemo.Font = new System.Drawing.Font("����ü", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NRmemo.ForeColor = System.Drawing.Color.Black;
            this.NRmemo.Location = new System.Drawing.Point(63, 64);
            this.NRmemo.Name = "NRmemo";
            this.NRmemo.Size = new System.Drawing.Size(11, 12);
            this.NRmemo.TabIndex = 10;
            this.NRmemo.Text = "0";
            this.NRmemo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NRmemo_Click);
            // 
            // team
            // 
            this.team.AutoSize = true;
            this.team.BackColor = System.Drawing.Color.Transparent;
            this.team.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.team.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.team.Location = new System.Drawing.Point(190, 14);
            this.team.Name = "team";
            this.team.Size = new System.Drawing.Size(35, 13);
            this.team.TabIndex = 4;
            this.team.Text = "�Ҽ�";
            this.team.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.BackColor = System.Drawing.Color.Transparent;
            this.name.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.name.ForeColor = System.Drawing.Color.Black;
            this.name.Location = new System.Drawing.Point(56, 14);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(59, 13);
            this.name.TabIndex = 2;
            this.name.Text = "�̸�(id)";
            this.name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 419);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(282, 82);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            this.pictureBox2.MouseHover += new System.EventHandler(this.pictureBox2_MouseHover);
            this.pictureBox2.MouseEnter += new System.EventHandler(this.pictureBox2_MouseEnter);
            // 
            // btn_resultnotice
            // 
            this.btn_resultnotice.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_resultnotice.BackgroundImage")));
            this.btn_resultnotice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_resultnotice.Location = new System.Drawing.Point(11, 329);
            this.btn_resultnotice.Name = "btn_resultnotice";
            this.btn_resultnotice.Size = new System.Drawing.Size(50, 43);
            this.btn_resultnotice.TabIndex = 12;
            this.btn_resultnotice.UseVisualStyleBackColor = true;
            this.btn_resultnotice.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_resultnotice_MouseClick);
            this.btn_resultnotice.MouseEnter += new System.EventHandler(this.btn_resultnotice_MouseEnter);
            // 
            // memTree
            // 
            this.memTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.memTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.memTree.Cursor = System.Windows.Forms.Cursors.Hand;
            this.memTree.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.memTree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.memTree.ImageIndex = 2;
            this.memTree.ImageList = this.imageList;
            this.memTree.Indent = 25;
            this.memTree.ItemHeight = 20;
            this.memTree.Location = new System.Drawing.Point(68, 88);
            this.memTree.Margin = new System.Windows.Forms.Padding(0);
            this.memTree.MinimumSize = new System.Drawing.Size(220, 325);
            this.memTree.Name = "memTree";
            this.memTree.SelectedImageIndex = 2;
            this.memTree.ShowLines = false;
            this.memTree.ShowNodeToolTips = true;
            this.memTree.ShowPlusMinus = false;
            this.memTree.ShowRootLines = false;
            this.memTree.Size = new System.Drawing.Size(220, 325);
            this.memTree.TabIndex = 5;
            this.memTree.Visible = false;
            this.memTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.memTree_NodeMouseDoubleClick);
            this.memTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.memTree_AfterCollapse);
            this.memTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.memTree_NodeMouseClick);
            this.memTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.memTree_AfterExpand);
            // 
            // btn_sendnotice
            // 
            this.btn_sendnotice.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_sendnotice.BackgroundImage")));
            this.btn_sendnotice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_sendnotice.Location = new System.Drawing.Point(11, 280);
            this.btn_sendnotice.Name = "btn_sendnotice";
            this.btn_sendnotice.Size = new System.Drawing.Size(50, 43);
            this.btn_sendnotice.TabIndex = 11;
            this.btn_sendnotice.UseVisualStyleBackColor = true;
            this.btn_sendnotice.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_sendnotice_MouseClick);
            this.btn_sendnotice.MouseEnter += new System.EventHandler(this.btn_sendnotice_MouseEnter);
            // 
            // btn_dialoguebox
            // 
            this.btn_dialoguebox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_dialoguebox.BackgroundImage")));
            this.btn_dialoguebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_dialoguebox.Location = new System.Drawing.Point(11, 231);
            this.btn_dialoguebox.Name = "btn_dialoguebox";
            this.btn_dialoguebox.Size = new System.Drawing.Size(50, 43);
            this.btn_dialoguebox.TabIndex = 10;
            this.btn_dialoguebox.UseVisualStyleBackColor = true;
            this.btn_dialoguebox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_dialoguebox_MouseClick);
            this.btn_dialoguebox.MouseEnter += new System.EventHandler(this.btn_dialoguebox_MouseEnter);
            // 
            // btn_memobox
            // 
            this.btn_memobox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_memobox.BackgroundImage")));
            this.btn_memobox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_memobox.Location = new System.Drawing.Point(11, 182);
            this.btn_memobox.Name = "btn_memobox";
            this.btn_memobox.Size = new System.Drawing.Size(50, 43);
            this.btn_memobox.TabIndex = 9;
            this.btn_memobox.UseVisualStyleBackColor = true;
            this.btn_memobox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_memobox_MouseClick);
            this.btn_memobox.MouseEnter += new System.EventHandler(this.btn_memobox_MouseEnter);
            // 
            // btn_board
            // 
            this.btn_board.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_board.BackgroundImage")));
            this.btn_board.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_board.Location = new System.Drawing.Point(11, 134);
            this.btn_board.Name = "btn_board";
            this.btn_board.Size = new System.Drawing.Size(50, 43);
            this.btn_board.TabIndex = 8;
            this.btn_board.UseVisualStyleBackColor = true;
            this.btn_board.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_board_MouseClick);
            this.btn_board.MouseEnter += new System.EventHandler(this.btn_board_MouseEnter);
            // 
            // btn_crm
            // 
            this.btn_crm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_crm.BackgroundImage")));
            this.btn_crm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_crm.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_crm.Location = new System.Drawing.Point(11, 88);
            this.btn_crm.Name = "btn_crm";
            this.btn_crm.Size = new System.Drawing.Size(50, 43);
            this.btn_crm.TabIndex = 7;
            this.btn_crm.UseVisualStyleBackColor = true;
            this.btn_crm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btn_crm_MouseClick);
            this.btn_crm.MouseEnter += new System.EventHandler(this.btn_crm_MouseEnter);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(13, 421);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(271, 82);
            this.webBrowser1.TabIndex = 14;
            this.webBrowser1.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(282, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mouseMenuStat
            // 
            this.mouseMenuStat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMn_online,
            this.StringMn_away,
            this.StripMn_DND,
            this.StripMn_busy});
            this.mouseMenuStat.Name = "mouseMenuStat";
            this.mouseMenuStat.Size = new System.Drawing.Size(142, 108);
            // 
            // StripMn_online
            // 
            this.StripMn_online.Image = ((System.Drawing.Image)(resources.GetObject("StripMn_online.Image")));
            this.StripMn_online.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.StripMn_online.Name = "StripMn_online";
            this.StripMn_online.Size = new System.Drawing.Size(141, 26);
            this.StripMn_online.Text = "�¶���";
            this.StripMn_online.Click += new System.EventHandler(this.StripMn_online_Click);
            // 
            // StringMn_away
            // 
            this.StringMn_away.Image = ((System.Drawing.Image)(resources.GetObject("StringMn_away.Image")));
            this.StringMn_away.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.StringMn_away.Name = "StringMn_away";
            this.StringMn_away.Size = new System.Drawing.Size(141, 26);
            this.StringMn_away.Text = "�ڸ����";
            this.StringMn_away.Click += new System.EventHandler(this.StringMn_away_Click);
            // 
            // StripMn_DND
            // 
            this.StripMn_DND.Image = ((System.Drawing.Image)(resources.GetObject("StripMn_DND.Image")));
            this.StripMn_DND.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.StripMn_DND.Name = "StripMn_DND";
            this.StripMn_DND.Size = new System.Drawing.Size(141, 26);
            this.StripMn_DND.Text = "�ٸ��빫��";
            this.StripMn_DND.Click += new System.EventHandler(this.StripMn_DND_Click);
            // 
            // StripMn_busy
            // 
            this.StripMn_busy.Image = ((System.Drawing.Image)(resources.GetObject("StripMn_busy.Image")));
            this.StripMn_busy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.StripMn_busy.Name = "StripMn_busy";
            this.StripMn_busy.Size = new System.Drawing.Size(141, 26);
            this.StripMn_busy.Text = "��ȭ��";
            this.StripMn_busy.Click += new System.EventHandler(this.StripMn_busy_Click);
            // 
            // menu_notifyicon
            // 
            this.menu_notifyicon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Mn_notify_show,
            this.Mn_notify_dispose});
            this.menu_notifyicon.Name = "menu_notifyicon";
            this.menu_notifyicon.Size = new System.Drawing.Size(111, 48);
            // 
            // Mn_notify_show
            // 
            this.Mn_notify_show.Name = "Mn_notify_show";
            this.Mn_notify_show.Size = new System.Drawing.Size(110, 22);
            this.Mn_notify_show.Text = "���̱�";
            this.Mn_notify_show.Click += new System.EventHandler(this.Mn_notify_show_Click);
            // 
            // Mn_notify_dispose
            // 
            this.Mn_notify_dispose.Name = "Mn_notify_dispose";
            this.Mn_notify_dispose.Size = new System.Drawing.Size(110, 22);
            this.Mn_notify_dispose.Text = "����";
            this.Mn_notify_dispose.Click += new System.EventHandler(this.Mn_notify_dispose_Click);
            // 
            // TM_file_sub
            // 
            this.TM_file_sub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TM_file_logout,
            this.TM_file_exit});
            this.TM_file_sub.Name = "TM_file_sub";
            this.TM_file_sub.Size = new System.Drawing.Size(123, 48);
            // 
            // TM_file_logout
            // 
            this.TM_file_logout.Enabled = false;
            this.TM_file_logout.Name = "TM_file_logout";
            this.TM_file_logout.Size = new System.Drawing.Size(122, 22);
            this.TM_file_logout.Text = "�α׾ƿ�";
            this.TM_file_logout.Click += new System.EventHandler(this.Btnlogout_Click);
            // 
            // TM_file_exit
            // 
            this.TM_file_exit.Name = "TM_file_exit";
            this.TM_file_exit.Size = new System.Drawing.Size(122, 22);
            this.TM_file_exit.Text = "����";
            this.TM_file_exit.Click += new System.EventHandler(this.MnExit_Click);
            // 
            // TM_motion_sub
            // 
            this.TM_motion_sub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TM_motion_chat,
            this.TM_motion_memo,
            this.TM_motion_sendfile,
            this.TM_motion_sendnotice});
            this.TM_motion_sub.Name = "TM_file_sub";
            this.TM_motion_sub.Size = new System.Drawing.Size(135, 92);
            this.TM_motion_sub.Opening += new System.ComponentModel.CancelEventHandler(this.TM_motion_sub_Opening);
            // 
            // TM_motion_chat
            // 
            this.TM_motion_chat.Enabled = false;
            this.TM_motion_chat.Name = "TM_motion_chat";
            this.TM_motion_chat.Size = new System.Drawing.Size(134, 22);
            this.TM_motion_chat.Text = "��ȭ�ϱ�";
            this.TM_motion_chat.Click += new System.EventHandler(this.MnDialog_Click);
            // 
            // TM_motion_memo
            // 
            this.TM_motion_memo.Enabled = false;
            this.TM_motion_memo.Name = "TM_motion_memo";
            this.TM_motion_memo.Size = new System.Drawing.Size(134, 22);
            this.TM_motion_memo.Text = "����������";
            this.TM_motion_memo.Click += new System.EventHandler(this.MnMemo_Click);
            // 
            // TM_motion_sendfile
            // 
            this.TM_motion_sendfile.Enabled = false;
            this.TM_motion_sendfile.Name = "TM_motion_sendfile";
            this.TM_motion_sendfile.Size = new System.Drawing.Size(134, 22);
            this.TM_motion_sendfile.Text = "���Ϻ�����";
            this.TM_motion_sendfile.Click += new System.EventHandler(this.MnSendFile_Click);
            // 
            // TM_motion_sendnotice
            // 
            this.TM_motion_sendnotice.Enabled = false;
            this.TM_motion_sendnotice.Name = "TM_motion_sendnotice";
            this.TM_motion_sendnotice.Size = new System.Drawing.Size(134, 22);
            this.TM_motion_sendnotice.Text = "�����ϱ�";
            this.TM_motion_sendnotice.Click += new System.EventHandler(this.MnNotice_Click);
            // 
            // TM_option_sub
            // 
            this.TM_option_sub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TM_option_default,
            this.TM_option_extension,
            this.TM_option_server});
            this.TM_option_sub.Name = "TM_option_sub";
            this.TM_option_sub.Size = new System.Drawing.Size(123, 70);
            // 
            // TM_option_default
            // 
            this.TM_option_default.Name = "TM_option_default";
            this.TM_option_default.Size = new System.Drawing.Size(122, 22);
            this.TM_option_default.Text = "�⺻����";
            this.TM_option_default.Click += new System.EventHandler(this.Mn_default_Click);
            // 
            // TM_option_extension
            // 
            this.TM_option_extension.Name = "TM_option_extension";
            this.TM_option_extension.Size = new System.Drawing.Size(122, 22);
            this.TM_option_extension.Text = "��������";
            this.TM_option_extension.Click += new System.EventHandler(this.Mn_extension_Click);
            // 
            // TM_option_server
            // 
            this.TM_option_server.Name = "TM_option_server";
            this.TM_option_server.Size = new System.Drawing.Size(122, 22);
            this.TM_option_server.Text = "��������";
            this.TM_option_server.Click += new System.EventHandler(this.Mn_server_Click);
            // 
            // TM_help_sub
            // 
            this.TM_help_sub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TM_help_show});
            this.TM_help_sub.Name = "TM_help_sub";
            this.TM_help_sub.Size = new System.Drawing.Size(135, 26);
            // 
            // TM_help_show
            // 
            this.TM_help_show.Name = "TM_help_show";
            this.TM_help_show.Size = new System.Drawing.Size(134, 22);
            this.TM_help_show.Text = "���򸻺���";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnFile,
            this.MnMotion,
            this.MnSetting,
            this.MnHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(282, 24);
            this.menuStrip1.TabIndex = 104;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MnFile
            // 
            this.MnFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnLogout,
            this.MnExit});
            this.MnFile.Name = "MnFile";
            this.MnFile.Size = new System.Drawing.Size(57, 20);
            this.MnFile.Text = "����(&F)";
            // 
            // MnLogout
            // 
            this.MnLogout.Enabled = false;
            this.MnLogout.Name = "MnLogout";
            this.MnLogout.Size = new System.Drawing.Size(122, 22);
            this.MnLogout.Text = "�α׾ƿ�";
            this.MnLogout.Click += new System.EventHandler(this.Btnlogout_Click);
            // 
            // MnExit
            // 
            this.MnExit.Name = "MnExit";
            this.MnExit.Size = new System.Drawing.Size(122, 22);
            this.MnExit.Text = "����";
            this.MnExit.Click += new System.EventHandler(this.MnExit_Click);
            // 
            // MnMotion
            // 
            this.MnMotion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnMemo,
            this.MnDialogue,
            this.MnNotice,
            this.MnSendFile});
            this.MnMotion.Name = "MnMotion";
            this.MnMotion.Size = new System.Drawing.Size(62, 20);
            this.MnMotion.Text = "����(&M)";
            // 
            // MnMemo
            // 
            this.MnMemo.Enabled = false;
            this.MnMemo.Name = "MnMemo";
            this.MnMemo.Size = new System.Drawing.Size(134, 22);
            this.MnMemo.Text = "����������";
            this.MnMemo.Click += new System.EventHandler(this.MnMemo_Click);
            // 
            // MnDialogue
            // 
            this.MnDialogue.Enabled = false;
            this.MnDialogue.Name = "MnDialogue";
            this.MnDialogue.Size = new System.Drawing.Size(134, 22);
            this.MnDialogue.Text = "��ȭ�ϱ�";
            this.MnDialogue.Click += new System.EventHandler(this.MnDialog_Click);
            // 
            // MnNotice
            // 
            this.MnNotice.Enabled = false;
            this.MnNotice.Name = "MnNotice";
            this.MnNotice.Size = new System.Drawing.Size(134, 22);
            this.MnNotice.Text = "�����ϱ�";
            this.MnNotice.Click += new System.EventHandler(this.MnNotice_Click);
            // 
            // MnSendFile
            // 
            this.MnSendFile.Enabled = false;
            this.MnSendFile.Name = "MnSendFile";
            this.MnSendFile.Size = new System.Drawing.Size(134, 22);
            this.MnSendFile.Text = "���Ϻ�����";
            this.MnSendFile.Click += new System.EventHandler(this.MnSendFile_Click);
            // 
            // MnSetting
            // 
            this.MnSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Mn_default,
            this.Mn_extension,
            this.Mn_server});
            this.MnSetting.Name = "MnSetting";
            this.MnSetting.Size = new System.Drawing.Size(58, 20);
            this.MnSetting.Text = "����(&S)";
            // 
            // Mn_default
            // 
            this.Mn_default.Name = "Mn_default";
            this.Mn_default.Size = new System.Drawing.Size(122, 22);
            this.Mn_default.Text = "�⺻����";
            this.Mn_default.Click += new System.EventHandler(this.Mn_default_Click);
            // 
            // Mn_extension
            // 
            this.Mn_extension.Name = "Mn_extension";
            this.Mn_extension.Size = new System.Drawing.Size(122, 22);
            this.Mn_extension.Text = "��������";
            this.Mn_extension.Click += new System.EventHandler(this.Mn_extension_Click);
            // 
            // Mn_server
            // 
            this.Mn_server.Name = "Mn_server";
            this.Mn_server.Size = new System.Drawing.Size(122, 22);
            this.Mn_server.Text = "��������";
            this.Mn_server.Click += new System.EventHandler(this.Mn_server_Click);
            // 
            // MnHelp
            // 
            this.MnHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnShowHelp,
            this.weDo����ToolStripMenuItem});
            this.MnHelp.Name = "MnHelp";
            this.MnHelp.Size = new System.Drawing.Size(72, 20);
            this.MnHelp.Text = "����(&H)";
            // 
            // MnShowHelp
            // 
            this.MnShowHelp.Name = "MnShowHelp";
            this.MnShowHelp.Size = new System.Drawing.Size(135, 22);
            this.MnShowHelp.Text = "���򸻺���";
            // 
            // weDo����ToolStripMenuItem
            // 
            this.weDo����ToolStripMenuItem.Name = "weDo����ToolStripMenuItem";
            this.weDo����ToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.weDo����ToolStripMenuItem.Text = "WeDo ����";
            this.weDo����ToolStripMenuItem.Click += new System.EventHandler(this.weDo����ToolStripMenuItem_Click);
            // 
            // aquaSkin1
            // 
            this.aquaSkin1.AquaStyle = SkinSoft.AquaSkin.AquaStyle.Panther;
            this.aquaSkin1.License = ((SkinSoft.AquaSkin.Licensing.AquaSkinLicense)(resources.GetObject("aquaSkin1.License")));
            this.aquaSkin1.ShadowStyle = SkinSoft.AquaSkin.ShadowStyle.Small;
            this.aquaSkin1.ToolStripStyle = SkinSoft.AquaSkin.ToolStripRenderStyle.Mixed;
            // 
            // Client_Form
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(282, 547);
            this.Controls.Add(this.panel_logon);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.default_panal);
            this.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(950, 10);
            this.MinimumSize = new System.Drawing.Size(298, 585);
            this.Name = "Client_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WeDo �޽���";
            this.Load += new System.EventHandler(this.Client_Form_Load);
            this.SizeChanged += new System.EventHandler(this.Client_Form_SizeChanged);
            this.Shown += new System.EventHandler(this.Client_Form_Shown);
            this.Activated += new System.EventHandler(this.Client_Form_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Client_Form_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_Form_FormClosing);
            this.mouseMenuG.ResumeLayout(false);
            this.mouseMenuN.ResumeLayout(false);
            this.default_panal.ResumeLayout(false);
            this.default_panal.PerformLayout();
            this.panel_progress.ResumeLayout(false);
            this.panel_progress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_loginCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_title)).EndInit();
            this.panel_logon.ResumeLayout(false);
            this.InfoBar.ResumeLayout(false);
            this.InfoBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NRtrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_stat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NRfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NRnotice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_NRmemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.mouseMenuStat.ResumeLayout(false);
            this.menu_notifyicon.ResumeLayout(false);
            this.TM_file_sub.ResumeLayout(false);
            this.TM_motion_sub.ResumeLayout(false);
            this.TM_option_sub.ResumeLayout(false);
            this.TM_help_sub.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aquaSkin1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void menu_notifyicon_MouseLeave(object sender, EventArgs e)
        {
            menu_notifyicon.Close();
        }


        private void notifyIcon_Click(object sender, MouseEventArgs e)
        {

            int pointx = System.Windows.Forms.StatusBar.MousePosition.X;
            int pointy = System.Windows.Forms.StatusBar.MousePosition.Y;
            if (e.Button == MouseButtons.Left)
            {
                string topmost = System.Configuration.ConfigurationSettings.AppSettings["topmost"];
                if (topmost.Equals("0"))
                {
                    this.TopMost = false;
                }
                else
                {
                    this.TopMost = true;
                }
                //this.WindowState = FormWindowState.Normal;
                this.SetBounds(mainform_x, mainform_y, mainform_width, mainform_height);
                this.ShowInTaskbar = true;
                this.Show();
                this.Activate();
                isHide = false;
                firstCall = false;

            }
            else
            {
                menu_notifyicon.Show(pointx, pointy);
            }
        }




        #endregion


        private void login_Click(object sender, EventArgs e)
        {
            checkInfoForLogin();
        }

        /// <summary>
        /// �� ��ſ� ����� UDP ���� �ν��Ͻ� ���� �� �޽��� ���� Thread ����
        /// </summary>
        /// 

        private void StartService()
        {
            try
            {
                if (this.myid != null && this.mypass != null && extension != null)
                {
                    panel_progress.Visible = true;
                    defaultCtrl(false);
                    logWrite("StartService() ����");
                    serverIP = GetServerIP();
                    IPHostEntry ihe = Dns.GetHostByName(Dns.GetHostName());
                    local = ihe.AddressList[0];
                    logWrite(local.ToString());
                    if (serverIP.ToLower().Equals("localhost"))
                    {
                        serverIP = "127.0.0.1";
                    }
                    server = new IPEndPoint(IPAddress.Parse(serverIP), 8881);
                    try
                    {
                        IPEndPoint listen = new IPEndPoint(IPAddress.Any, listenport);
                        IPEndPoint send = new IPEndPoint(IPAddress.Any, sendport);
                        IPEndPoint check = new IPEndPoint(IPAddress.Any, checkport);

                        listenSock = new UdpClient(listen);
                        listenSock.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 2000);
                        //listenSock.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 2000);
                        sendSock = new UdpClient(send);
                        checkSock = new UdpClient(check);

                        //filesender = new IPEndPoint(IPAddress.Any, filereceiveport);
                        //filesock = new UdpClient(filesender);

                        filesend = new IPEndPoint(IPAddress.Any, filesendport);
                        filesendSock = new UdpClient(filesend);
                    }
                    catch (Exception ex)
                    {
                        logWrite("StartService() socket �������� : " + ex.ToString());
                    }

                    try
                    {
                        receive = new Thread(new ThreadStart(Receive));
                        receive.Start();
                        logWrite("Recieve ������ ����");
                    }
                    catch (Exception e)
                    {
                        logWrite("recieve ������ ���� ���� :" + e.ToString());
                    }

                    logWrite("������ ���ӽõ�");

                    SendInfo();
                }
                else if (this.myid == null)
                {
                    id.Focus();
                }
                else
                {
                    tbx_pass.Focus();
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void SendCheck()
        {
            try
            {
                IPEndPoint checkiep = new IPEndPoint(server.Address, 8885);
                byte[] re = null;
                checkSock.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 2000);
                byte[] buffer = Encoding.ASCII.GetBytes("1");
                while (true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        while (connected == true)
                        {
                            Thread.Sleep(3000);

                            try
                            {
                                int count = checkSock.Send(buffer, buffer.Length, checkiep);
                                re = checkSock.Receive(ref checkiep);
                                if (re == null)
                                {
                                    logWrite("�޽����� ���޵��� ����");
                                    break;
                                }
                                else
                                {
                                    //logWrite("üũ!");
                                    //�α��� �õ����� ��Ȳ���� �������� ������
                                    if (serverAlive == false)
                                    {
                                        serverAlive = true;
                                        NoParamDele relogin = new NoParamDele(StartService);
                                        Invoke(relogin);
                                        PanelCtrlDelegate dele = new PanelCtrlDelegate(ServerFailCtrl);
                                        Invoke(dele, true);
                                    }
                                }
                            }
                            catch (Exception ex1)
                            {
                                logWrite("���� üũ ���� " + i + "��°");
                                break;
                            }
                        }
                    }

                    if (connected == true)
                    {
                        if (serverAlive == true)
                        {
                            serverAlive = false;
                            LogOutDelegate logoutdele = new LogOutDelegate(LogOut);
                            Invoke(logoutdele);
                            LogOutDelegate dele = new LogOutDelegate(disposeServerFail);
                            Invoke(dele);
                        }
                    }
                }
            }
            catch (Exception ex2)
            {
                logWrite(ex2.ToString());
            }
        }

        private void disposeServerFail()
        {
            try
            {
                panel_progress.Visible = true;
                label_progress_status.Text = "������";
                ServerFailCtrl(false);
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void defaultCtrl(bool value)
        {
            tbx_pass.Enabled = value;
            id.Enabled = value;
            btn_login.Enabled = value;
            cbx_pass_save.Enabled = value;
            panel_progress.Visible = !value;
        }

        private void ServerFailCtrl(bool value)
        {
            label_id.Visible = value;
            label_pass.Visible = value;
            tbx_pass.Visible = value;
            id.Visible = value;
            btn_login.Visible = value;
            cbx_pass_save.Visible = value;
            pbx_loginCancel.Visible = !value;
        }

        //private void StartService()
        //{
        //    try
        //    {
        //        serveraddress = GetServerIP();
        //        if (serveraddress != null)
        //        {
        //            server = new IPEndPoint(IPAddress.Parse(serveraddress), 8881);  //���� EndPoint
        //            try
        //            {
        //                Socket tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //                tempSocket.Connect(server);

        //                if (tempSocket.Connected == true)
        //                {
        //                    logWrite("ServerSocket Connected");
        //                    ServerSocket = tempSocket;
        //                    ServerSocket.Blocking = true; ;
        //                }

        //                filesender = new IPEndPoint(IPAddress.Any, filereceiveport);  //���ϼ��� EndPoint
        //                filesock = new UdpClient(filesender);

        //                filesend = new IPEndPoint(IPAddress.Any, filesendport);       //�������� EndPoint
        //                filesendSock = new UdpClient(filesend);
        //            }
        //            catch (Exception ex)
        //            {
        //                logWrite("StartService() socket �������� : " + ex.ToString());
        //            }

        //            try
        //            {
        //                receive = new Thread(new ThreadStart(Receive));
        //                receive.Start();
        //                logWrite("Recieve ������ ����");
        //            }
        //            catch (Exception e)
        //            {
        //                logWrite("recieve ������ ���� ���� :" + e.ToString());
        //            }

        //            logWrite("������ ���ӽõ�");
        //            SendInfo();
        //        }
        //        else
        //        {
        //            MessageBox.Show("������ ������ �����ǰ� �����Ǿ� ���� �ʽ��ϴ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        logWrite(exp.ToString());
        //    }
        //}

        /// <summary>
        /// �޽��� �������� �޼ҵ�(�������� : UDP)
        /// </summary>
        /// 
        private void Receive()
        {
            try
            {
                server = new IPEndPoint(IPAddress.Parse(serverIP), 8881);

                receiverStart = true;
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 8883);

                byte[] buffer = null;


                while (true)
                {
                    try
                    {
                        logWrite("Receive() ���Ŵ�� ");
                        if (listenSock != null)
                        {
                            buffer = listenSock.Receive(ref sender);
                        }
                        logWrite("����! ");
                        if (buffer != null && buffer.Length != 0)
                        {
                            logWrite("sender IP : " + sender.Address.ToString());
                            logWrite("sender port : " + sender.Port.ToString());

                            listenSock.Send(buffer, buffer.Length, sender);  //���������� �޽��� �����ϸ� ����(udp����� ���й���)

                            string msg = Encoding.UTF8.GetString(buffer);

                            logWrite("���� �޽��� : " + msg);
                            MsgFilter(msg, sender);
                        }
                    }
                    catch (ThreadAbortException e)
                    { }
                    catch (SocketException e)
                    {
                        if (connected == true)
                            logWrite("Receive() ���� : " + e.ToString());
                    }
                }

                if (connected == true)
                    logWrite("##���## : Receiver �� �ߴܵǾ����ϴ�. ");
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }



        //private void Receive()
        //{
        //    receiverStart = true;
        //    byte[] msgbuffer = null;
        //    byte[] header = null;
        //    byte[] buffer = null;
        //    try
        //    {
        //        while (true)
        //        {

        //            if (ServerSocket != null && ServerSocket.Connected)
        //            {

        //                logWrite("�޽��� ����!");
        //                header = new byte[4];
        //                ServerSocket.Receive(header, 4, SocketFlags.Peek);
        //                if (Encoding.ASCII.GetString(header).Trim().Equals("SIZE"))
        //                {
        //                    ServerSocket.Receive(header, 4, SocketFlags.None);
        //                    header = new byte[5];
        //                    ServerSocket.Receive(header, 5, SocketFlags.None);

        //                    string size = Encoding.ASCII.GetString(header);
        //                    logWrite("SIZE : " + size);

        //                    msgbuffer = new byte[Convert.ToInt32(size.Trim())];
        //                    ServerSocket.Receive(msgbuffer, Convert.ToInt32(size.Trim()), SocketFlags.None);
        //                }


        //                if (msgbuffer != null && msgbuffer.Length != 0)
        //                {
        //                    logWrite("sender IP : " + ((IPEndPoint)ServerSocket.RemoteEndPoint).Address.ToString());
        //                    logWrite("sender port : " + ((IPEndPoint)ServerSocket.RemoteEndPoint).Port.ToString());

        //                    string msg = Encoding.UTF8.GetString(msgbuffer);

        //                    logWrite("���� �޽��� : " + msg);
        //                    ArrayList list = new ArrayList();  //MsgFilter �޼ҵ忡 ���� �Ű����� ����
        //                    Thread msgThread = new Thread(new ParameterizedThreadStart(MsgFilter)); //���ŵ� �޽����� ó���� ���� ������ ����
        //                    msgThread.Start(msg);
        //                }
        //            }
        //            else
        //            {
        //                logWrite("ServerSocket is Disconnected");
        //                break;
        //            }
        //        }
        //    }
        //    catch (ThreadAbortException e)
        //    {}
        //    catch(SocketException e)
        //    {
        //        if (connected == true)
        //            logWrite("Receive() ���� : " + e.ToString());
        //    }
        //    if (connected == true)
        //        logWrite("##���## : Receiver �� �ߴܵǾ����ϴ�. ");
        //}

        /// <summary>
        /// ���� ���� ���� �޼ҵ�
        /// </summary>
        /// <param name="obj">���� ���ſ� �ʿ��� ���� ����(�����̸�, ũ��, �������, ������� id</param>
        private void FileReceiver(object obj)
        {
            try
            {
                Hashtable fileinfo = (Hashtable)obj;
                string filename = null;
                int filesize = 0;
                string sendername = null;
                string senderid = null;
                string fileseq = null;
                int rowIndex = 0;
                string NRseq = null;

                foreach (DictionaryEntry de in fileinfo)
                {
                    if (de.Key.ToString().Equals("name"))
                    {
                        sendername = de.Value.ToString();
                        logWrite("File sender name : " + sendername);
                    }
                    else if (de.Key.ToString().Equals("senderid"))
                    {
                        senderid = de.Value.ToString();
                        logWrite("File sender id : " + senderid);
                    }
                    else if (de.Key.Equals("filenum"))
                    {
                        fileseq = de.Value.ToString();
                    }
                    else if (de.Key.Equals("rowindex"))
                    {
                        rowIndex = Convert.ToInt32(de.Value);
                    }
                    else if (de.Key.Equals("NRseq"))
                    {
                        NRseq = de.Value.ToString();
                    }
                    else
                    {
                        filename = (string)de.Key;
                        logWrite("FileReceiver() : filename=" + filename);

                        filesize = (int)de.Value;
                        logWrite("FileReceiver() : filesize=" + filesize);
                    }
                }

                byte[] buffer = null;

                FileInfo fi = new FileInfo(filename);
                int filenum = 0;
                int strlength = filename.Length;
                string ext = fi.Extension;

                //������ ���ϰ� ���� �̸��� ������ ������ ��� ó������
                if (fi.Exists == true)
                {
                    string str = filename.Substring(0, (strlength - 4));
                    string tempname = null;
                    do
                    {
                        filenum++;
                        str += "(" + filenum.ToString() + ")"; //���� �̸��� ������ ���� ��� "���ϸ�(filenum).Ȯ����" �� ���·� ����
                        tempname = str + ext;
                        fi = new FileInfo(tempname);
                    } while (fi.Exists == true);
                    filename = tempname;
                }

                int receivefailcount = 0;
                int sendfailcount = 0;
                FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.Read, 40960); //�ѹ��� 40960 ����Ʈ�� ����

                if (filesock == null)
                {
                    filesender = new IPEndPoint(IPAddress.Any, filereceiveport);
                    filesock = new UdpClient(filesender);
                }

                filesock.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);

                try
                {
                    lock (filesock)
                    {
                        while (true)
                        {
                            Thread.Sleep(10);
                            //logWrite("FileReceiver() ���Ŵ�� ");
                            try
                            {
                                buffer = filesock.Receive(ref filesender);
                                //logWrite("����!");
                            }
                            catch (Exception se)
                            {
                                logWrite("FileReceiver()  filesock.Receive ���� : " + se.ToString());
                                receivefailcount++;
                                if (receivefailcount == 5)
                                {
                                    logWrite("���� ���� 5ȸ�� FileReceiver �ߴ�!");
                                    break;
                                }
                            }
                            if (buffer != null && buffer.Length != 0)
                            {
                                //logWrite("sender IP : " + filesender.Address.ToString());
                                //logWrite("sender port : " + filesender.Port.ToString());

                                byte[] receivebyte = Encoding.UTF8.GetBytes(buffer.Length.ToString());

                                try
                                {
                                    filesock.Send(receivebyte, receivebyte.Length, filesender);  //���������� �޽��� �����ϸ� ����(udp����� ���й���)

                                    //logWrite(buffer.Length.ToString() + " byte ����!");
                                }
                                catch (Exception se1)
                                {
                                    logWrite("FileReceiver() filesock.Send ���� : " + se1.ToString());
                                    sendfailcount++;
                                    if (sendfailcount == 5)
                                    {
                                        logWrite("�������� ���� 5ȸ�� FileReceiver �ߴ�!");
                                        break;
                                    }
                                }
                                if (fs.CanWrite == true)
                                {
                                    try
                                    {
                                        fs.Write(buffer, 0, buffer.Length);
                                        fs.Flush();
                                    }
                                    catch (Exception e)
                                    {
                                        logWrite("FileStream.Write() ���� : " + e.ToString());
                                    }
                                }
                                FileInfo finfo = new FileInfo(filename);

                                int size = Convert.ToInt32(finfo.Length);

                                if (size > 0)
                                {
                                    SendMsg("14|" + NRseq, server);
                                    intParamDele intdele = new intParamDele(refreshNRfile);
                                    Invoke(intdele, rowIndex);

                                }

                                if (size >= filesize)  //���� �����͸� ��� �����ߴ��� üũ
                                {
                                    if (filesender.Address.Equals(server.Address))
                                    {
                                        ChangeStat showdownloadform = new ChangeStat(ShowDownloadForm); //�ٿ�ε� ���¹� ǥ�� delegate
                                        Invoke(showdownloadform, new object[] { "������ ����", filename });
                                    }
                                    else
                                    {
                                        foreach (DictionaryEntry de in InList)
                                        {
                                            if (((IPEndPoint)de.Value).Address.Equals(filesender.Address))
                                            {
                                                senderid = de.Key.ToString();
                                                logWrite(senderid);
                                                sendername = getName(senderid);
                                                break;
                                            }
                                        }
                                        logWrite("���� ���� �Ϸ�");

                                        ChangeStat showdownloadform = new ChangeStat(ShowDownloadForm);
                                        if (senderid.Length > 1)
                                        {
                                            Invoke(showdownloadform, new object[] { sendername + "(" + senderid + ") ������ ����", filename });
                                        }
                                        else
                                        {
                                            Invoke(showdownloadform, new object[] { "������ ����", filename });
                                        }
                                    }
                                    fs.Close();
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (SocketException e)
                {
                    logWrite("FileReceive() ���� : " + e.ToString());
                }
                logWrite("FileReceiver �� �ߴܵǾ����ϴ�. ");
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// �ٿ�ε� �ϷḦ �˸��� �ٿ����� ���� �Ǵ� ���Ͽ��� ���� ��
        /// </summary>
        /// <param name="sendername">���� �������</param>
        /// <param name="filename">���ϸ�</param>
        private void ShowDownloadForm(string sendername, string filename)
        {
            try
            {
                string[] farray = filename.Split('\\');
                downloadform = new DownloadResultForm();
                downloadform.btn_opendir.MouseClick += new MouseEventHandler(btn_opendir_Click);
                downloadform.btn_openfile.MouseClick += new MouseEventHandler(btn_openfile_Click);
                downloadform.label_sender.Text = sendername;
                downloadform.label_filename.Text = farray[(farray.Length - 1)];
                downloadform.label_fullname.Text = filename;
                downloadform.WindowState = FormWindowState.Minimized;
                downloadform.Show();
                downloadform.timer.Start();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }


        /// <summary>
        /// ���ۿϷ�� ���¿��� ���Ͽ��� ���ý�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_openfile_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string filename = null;
                int num = button.Parent.Controls.Count;
                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("label_fullname"))
                    {
                        filename = button.Parent.Controls[i].Text;
                        break;
                    }
                }
                System.Diagnostics.Process.Start(filename);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }


        /// <summary>
        /// ���ۿϷ�� ���¿��� �ٿ����� ���� ���ý�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_opendir_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string filename = null;
                int num = button.Parent.Controls.Count;
                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("label_fullname"))
                    {
                        filename = button.Parent.Controls[i].Text;
                        break;
                    }
                }
                FileInfo fileinfo = new FileInfo(filename);
                string dirname = fileinfo.DirectoryName;
                System.Diagnostics.Process.Start(dirname);

            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// �޽��� ���� �޼ҵ�(ä�� �� ���� ���� �޽���)
        /// </summary>
        /// <param name="msg">�޽���</param>
        /// <param name="iep">�޽��� �߽��� IPEndPoint</param>
        /// 
        public void SendMsg(string msg, IPEndPoint iep)
        {
            try
            {
                //iep.Port=listenport; //�������� ��Ʈ�� �������� ��Ʈ�� ����
                if (iep.Port == 8882)
                {
                    iep.Port = 8881;
                }
                else if (iep.Port != 8881)
                {
                    iep.Port = listenport;
                }

                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                byte[] re = null;
                sendSock.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 500);//���� ���� �޽��� ���ð� ����

                logWrite("Receiver IP : " + iep.Address.ToString());
                logWrite("Receiver port : " + iep.Port.ToString());
                logWrite("�߼� �޽��� :" + msg);

                for (int i = 0; i < 2; i++)
                {
                    logWrite("SendMsg() : " + i.ToString() + " ��° �õ�!");
                    sendSock.Send(buffer, buffer.Length, iep);

                    try
                    {
                        re = sendSock.Receive(ref iep);
                        if (re != null && re.Length != 0)
                        {
                            logWrite("����޽��� : " + Encoding.UTF8.GetString(re));
                            break;
                        }

                    }
                    catch (SocketException e) { }
                }

                if (re == null || re.Length == 0)
                {
                    logWrite("SendMsg() ���� : ���� �޼����� �������� �ʽ��ϴ�.");
                    string[] str1 = msg.Split('|');

                    if (str1[0].Equals("8")) //���� �α��� ������ ���
                    {
                        logWrite("�α��� ���ӿ� ������ �������� �ʽ��ϴ�. [��������] :" + iep.Address.ToString() + ":" + iep.Port.ToString());
                        MessageBox.Show(this, "������ �������� �ʽ��ϴ�.\r\n[��������] :" + iep.Address.ToString() + ":" + iep.Port.ToString(), "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        panel_progress.Visible = false;
                        defaultCtrl(true);
                        closing();
                    }
                    else if (iep.Port == 8881)
                    {
                        logWrite("������ �������� �ʽ��ϴ�. �������� :" + iep.Address.ToString() + ":" + iep.Port.ToString());
                    }
                    else if (str1[0].Equals("m"))
                    {
                        SendMsg("4|" + msg, server);
                    }
                    else
                    {
                        logWrite("EndPoint : " + iep.Address.ToString() + ":" + iep.Port.ToString() + " �� �������� �ʾҽ��ϴ�.");
                        foreach (DictionaryEntry de in InList)
                        {
                            IPEndPoint ipoint = (IPEndPoint)de.Value;
                            int port = getListenPort(ipoint.Port);
                            if (iep.Port == port)
                            {
                                SendMsg("3|" + de.Key.ToString(), server);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }
        //public void SendMsg(string msg)
        //{
        //    byte[] buffer = Encoding.UTF8.GetBytes(msg);
        //    byte[] re = null;
        //    logWrite("�߼� �޽��� :" + msg);
        //    if (ServerSocket != null)
        //    {
        //        try
        //        {
        //            ServerSocket.Send(buffer);
        //        }
        //        catch (SocketException e)
        //        {
        //            logWrite("SendMsg SocketException : " + e.ToString());
        //        }
        //        catch (Exception ex)
        //        {
        //            logWrite(ex.ToString());
        //        }
        //    }
        //    else
        //    {
        //        logWrite("ServerSocket is null!");

        //    }
        //}


        /// <summary>
        /// �ٸ� �����(��)���� ���� ���� �غ�
        /// </summary>
        /// <param name="list">���� ���� ����� ���</param>
        /// <param name="filename">���ϸ�</param>
        /// <param name="timekey">�������۽ð�(���� ���������۾� �� ����Ű�� ��)</param>
        private void FileSendRequest(ArrayList list, string filename, string timekey)
        {
            try
            {
                FileInfo fi = new FileInfo(filename);
                long size = fi.Length;
                ShowFileSendDetailDelegate detail = new ShowFileSendDetailDelegate(ShowFileSendDetail);


                //���� ���� ������(F|���ϸ�|����ũ��|����key|������id)
                string msg = "F|" + filename + "|" + size.ToString() + "|" + timekey + "|" + this.myid;
                string smsg = "5|" + filename + "|" + size.ToString() + "|" + timekey + "|" + this.myid + "|";
                logWrite("FileSendRequest() �������� �޽��� ���� : " + msg);
                logWrite("FileSendRequest() �������� �޽��� ���� : " + smsg);
                FileSendDetailListView view = (FileSendDetailListView)FileSendDetailList[timekey];
                bool outter = false;
                foreach (string id in list)
                {
                    if (InList.ContainsKey(id) && InList[id] != null) //���۴���ڰ� �α��� ������ ���
                    {
                        IPEndPoint fiep = (IPEndPoint)InList[id];
                        fiep.Port = listenport;
                        SendMsg(msg, fiep);
                        Invoke(detail, new object[] { id, "���� �����", view });
                    }
                    else  //���۴���ڰ� �α׾ƿ� ������ ���
                    {
                        smsg += id + ";";
                        Invoke(detail, new object[] { id, "��������", view });
                        outter = true;
                    }
                }
                if (outter == true)
                    SendMsg(smsg, server);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }


        /// <summary>
        /// ���� ���� �޼ҵ�
        /// </summary>
        /// <param name="obj">���� ���ۿ� �ʿ��� ����(Y|���ϸ�|����������key|������id) </param>
        private void SendFile(object obj) //tempMsg(Y|���ϸ�|������Ű|������id)
        {
            try
            {
                Hashtable table = (Hashtable)obj;
                IPEndPoint iep = null;
                string[] info = null;

                foreach (DictionaryEntry de in table)
                {
                    info = (string[])de.Key;
                    iep = (IPEndPoint)de.Value;
                }
                if (info[0].Equals("Y"))
                {
                    iep.Port = filereceiveport;   //������������� ��Ʈ�� ����
                }
                else
                {
                    iep.Port = 9001;   //������������ ��Ʈ�� ����
                }
                logWrite("SendFile() �������� ��Ʈ ���� :" + iep.Port.ToString());

                FileInfo fi = new FileInfo(info[1]);
                logWrite("SendFile() FileInfo �ν��Ͻ� ���� : " + info[1]);

                int read = 0;
                byte[] buffer = null;
                byte[] re = null;

                ShowFileSendDetailDelegate detail = new ShowFileSendDetailDelegate(ShowFileSendDetail);
                ShowFileSendStatDelegate show = new ShowFileSendStatDelegate(ShowFileSendStat);
                ShowCloseButtonDelegate close = new ShowCloseButtonDelegate(ShowCloseButton);
                SendFileForm form = (SendFileForm)FileSendFormList[info[2]];
                FileSendDetailListView view = (FileSendDetailListView)FileSendDetailList[info[2]];



                if (filesendSock == null)
                {
                    filesend = new IPEndPoint(IPAddress.Any, filesendport);
                    filesendSock = new UdpClient(filesend);
                }
                else
                {
                    filesendSock.Close();
                    //filesend = new IPEndPoint(IPAddress.Any, filesendport);
                    filesendSock = new UdpClient(filesend);
                }

                filesendSock.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);

                if (fi.Exists == true)
                {

                    BufferedStream bs = new BufferedStream(new FileStream(info[1], FileMode.Open, FileAccess.Read, FileShare.Read, 40960), 40960);

                    double sendfilesize = Convert.ToDouble(fi.Length);
                    double percent = (40960 / sendfilesize) * 100;
                    double total = 0.0;

                    lock (filesendSock)
                    {
                        while (true)
                        {
                            for (int i = 0; i < 3; i++) //udp ����� ���۽��� ����
                            {
                                try
                                {
                                    logWrite("FileReceiver IP : " + iep.Address.ToString());
                                    logWrite("FileReceiver port : " + iep.Port.ToString());
                                    if (sendfilesize >= 40960.0)
                                        buffer = new byte[40960];
                                    else buffer = new byte[Convert.ToInt32(sendfilesize)];
                                    read = bs.Read(buffer, 0, buffer.Length);
                                    filesendSock.Send(buffer, buffer.Length, iep);
                                    logWrite("filesendSock.Send() : " + i.ToString() + " ��° �õ�!");
                                }
                                catch (Exception e)
                                {
                                    logWrite("SendFile() BufferedStream.Read() ���� :" + e.ToString());
                                }
                                try
                                {
                                    re = filesendSock.Receive(ref iep);
                                    int reSize = int.Parse(Encoding.UTF8.GetString(re));
                                    if (reSize == buffer.Length) break;
                                }
                                catch (SocketException e1)
                                {
                                    logWrite(e1.ToString());
                                }
                            }

                            if (re == null || re.Length == 0)
                            {
                                logWrite("filesendSock.Send() ������ �������� �ʽ��ϴ�. ������ ���� : " + iep.Address.ToString() + ":" + iep.Port.ToString());
                                MessageBox.Show("�������� ���� : ������ �������� ����", "���۽���", MessageBoxButtons.OK);
                                if (FileSendFormList.Count != 0 && FileSendFormList[info[2]] != null)
                                {
                                    Invoke(show, new object[] { "���۽���", form });
                                }
                                break;
                            }
                            else
                            {
                                sendfilesize = (sendfilesize - 40960.0);
                                total += percent;
                                if (total >= 100.0) total = 100.0;
                                string[] totalArray = (total.ToString()).Split('.');

                                if (!iep.Address.ToString().Equals(serverIP))
                                {
                                    if (FileSendFormList.Count != 0 && FileSendFormList[info[2]] != null)
                                    {
                                        Invoke(show, new object[] { "������(" + totalArray[0] + " %)", form });
                                    }
                                    if (FileSendDetailList.Count != 0 && FileSendDetailList[info[2]] != null)
                                    {
                                        string detailmsg = "������(" + totalArray[0] + " %)";
                                        Invoke(detail, new object[] { info[3], detailmsg, view });
                                    }
                                }
                            }
                            if (total == 100.0)
                            {
                                string detailmsg = "���ۿϷ�";
                                if (iep.Address.ToString().Equals(serverIP))
                                {
                                    Invoke(detail, new object[] { "server", detailmsg, view });
                                }
                                else
                                {
                                    Invoke(detail, new object[] { info[3], detailmsg, view });
                                }
                                bool isFinished = (bool)Invoke(close, view);  //�ڼ���
                                if (isFinished == false)
                                    Invoke(show, new object[] { detailmsg, form });  //FileSendForm
                                else
                                    Invoke(show, new object[] { "Finish", form });  //FileSendForm
                            }
                            if (total == 100.0)
                            {
                                bs.Close();
                                break;
                            }
                        }
                    }
                    try
                    {
                        if (FileSendThreadList.Count != 0 && FileSendThreadList[info[2] + "|" + info[3]] != null)
                        {
                            ((Thread)FileSendThreadList[info[2] + "|" + info[3]]).Abort();
                            logWrite("FileSendThread Abort()!");
                        }
                    }
                    catch (ThreadAbortException te)
                    {
                        logWrite(te.ToString());
                    }
                }
                else
                {
                    logWrite("SendFile() ������ ���� : " + info[1]);
                }
            }
            catch (IOException ioexception)
            {
                MessageBox.Show("������ ���� �������� �Դϴ�.\r\n ������ �ݰų� �б��������� ������ �ּ���", "���", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }


        /// <summary>
        /// ���� ���� ���°� ����
        /// </summary>
        /// <param name="stat">����</param>
        /// <param name="form">��� ���� ��</param>
        private void ShowFileSendStat(string stat, SendFileForm form)
        {
            try
            {
                if (stat.Equals("Finish"))
                {
                    form.label_result.Text = "��� ���� ������ �Ϸ�Ǿ����ϴ�.";
                    form.btn_cancel.Text = "��  ��";
                    form.btn_start.Visible = false;
                }
                else form.label_result.Text = stat;
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// FileSendForm �� �ڼ��� ���� Ŭ���� ��Ÿ���� ����Ʈ �� ���� ���°� ����
        /// </summary>
        /// <param name="keyid">����Ʈ ���� ������ ���� id(���� �޴� ����� id)</param>
        /// <param name="detail">ǥ���ϰ��� �ϴ� ���°�</param>
        /// <param name="view">����Ʈ �� ��ü</param>
        private void ShowFileSendDetail(string keyid, string detail, FileSendDetailListView view)
        {
            try
            {
                ListViewItem[] itemArray = null;
                if (keyid.Equals("server"))
                {
                    foreach (ListViewItem item in view.listView.Items)
                    {
                        if (item.SubItems[1].Text.Equals("��������"))
                        {
                            item.SubItems[1].Text = detail;
                        }
                    }
                }
                else
                {
                    if (view.listView.Items.ContainsKey(keyid))
                    {
                        itemArray = view.listView.Items.Find(keyid, false);
                    }
                    if (itemArray != null)
                    {
                        itemArray[0].SubItems[1].Text = detail;
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// FileSendDetailListView ���� ��� ����ڰ� ���ۿϷ� �Ǹ� FileSendForm ��ư�� ����
        /// </summary>
        /// <param name="view">FileSendDetailListView ��ü</param>
        /// <returns></returns>
        private bool ShowCloseButton(FileSendDetailListView view)
        {
            bool isFinished = false;
            int num = 0;
            try
            {
                for (int i = 0; i < view.listView.Items.Count; i++)
                {
                    if (!view.listView.Items[i].SubItems[1].Text.Equals("���ۿϷ�")) num++;
                }
                if (num == 0) isFinished = true;
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
            return isFinished;
        }

        /// <summary>
        /// ���� ���� �� ���� ���� ���� ��Ʈ�� IPEndPoint �� port �� ����
        /// </summary>
        /// <param name="port">���� ���� ���Ŵ���� IPEndpoint�� port</param>
        /// <returns></returns>
        public int getFileReceivePort(int port)
        {
            if (port == 8881 || port == 8882) port = 9001;
            if (port == 8883 || port == 8884) port = 9003;
            return port;
        }

        /// <summary>
        /// �޽��� ���� �� �������� �������� ��Ʈ������ ����
        /// </summary>
        /// <param name="port">���� �޽��� ������ IPEndPoint�� ��Ʈ��</param>
        /// <returns></returns>
        public int getListenPort(int port)
        {
            if (port == 8884) listenport = 8883;
            if (port == 8886) listenport = 8885;
            if (port == 8888) listenport = 8887;
            return port;
        }

        /// <summary>
        /// ���ŵ� �޽����� �м��Ͽ� �� ��û�� �°� ó��
        /// </summary>
        /// <param name="obj">ArrayList�� ����ȯ�� Object</param>
        protected void MsgFilter(object obj, IPEndPoint iep)
        {
            try
            {
                string msg = (string)obj; //���� �޽���
                msg = msg.Trim();

                string[] tempMsg = msg.Split('|');
                string code = tempMsg[0];
                switch (code)
                {
                    case "f"://�α��� ���н�(f|n or p)
                        try
                        {
                            FormTextCtrlDelegate informationMsg = new FormTextCtrlDelegate(ShowMessageBox);
                            if (tempMsg[1].Equals("n"))
                            {
                                Invoke(informationMsg, "��ϵ��� ���� ����� �Դϴ�.");
                            }
                            else
                                Invoke(informationMsg, "��й�ȣ�� Ʋ�Ƚ��ϴ�.");

                            Loginfail fail = new Loginfail(logInFail);
                            Invoke(fail, null);
                        }
                        catch (Exception e) { };
                        break;

                    case "g": //�α��� ������ (g|name|team|company|com_cd)

                        connected = true;

                        stringDele changeProgressStyle = new stringDele(chageProgressbar);
                        Invoke(changeProgressStyle, "�ε���");
                        setCRM_DB_HOST("c:\\MiniCTI\\config\\MiniCTI_config.xml", serverIP);
                        setCRM_DB_HOST(Application.StartupPath + "\\MiniCTI_config.xml", serverIP);

                        FileInfo temp = new FileInfo(Application.StartupPath + "\\MiniCTI_config.xml");

                        FileInfo tempfileinfo = new FileInfo("C:\\MiniCTI\\config\\MiniCTI_config.xml");
                        if (!tempfileinfo.Exists)
                        {
                            logWrite("MiniCTI config ���� ����");
                            FileInfo file_copied = temp.CopyTo(tempfileinfo.FullName);
                        }
                        else
                        {
                            FileInfo file_copied = temp.CopyTo(tempfileinfo.FullName, true);
                        }

                        string sampleExcelFile = "����������-����.xlsx";
                        FileInfo tempExcel = new FileInfo(Application.StartupPath + "\\" + sampleExcelFile);

                        FileInfo tempExcelfileinfo = new FileInfo("C:\\MiniCTI\\sample\\" + sampleExcelFile);
                        if (!tempExcelfileinfo.Exists)
                        {
                            logWrite("����������-���� ���� ����");
                            FileInfo file_copied = tempExcel.CopyTo(tempExcelfileinfo.FullName);
                        }
                        else
                        {
                            FileInfo file_copied = tempExcel.CopyTo(tempExcelfileinfo.FullName, true);
                        }

                        myname = tempMsg[1];//���������� ���޵� �̸� ����
                        myid = this.id.Text;
                        com_cd = tempMsg[4];
                        logWrite("�α��� ����! (" + DateTime.Now.ToString() + ")");


                        //���� ������ Client_Form �� ǥ��
                        FormTextCtrlDelegate FlushName = new FormTextCtrlDelegate(FlushInfo);
                        FormTextCtrlDelegate FlushTeam = new FormTextCtrlDelegate(Flushteam);

                        Invoke(FlushName, tempMsg[1]);

                        if (tempMsg[2].Length > 0)
                        {
                            Invoke(FlushTeam, tempMsg[2]);
                        }
                        else
                        {
                            Invoke(FlushTeam, tempMsg[3]);
                        }



                        this.KeyDown += new KeyEventHandler(Client_Form_KeyDown);
                        int count = this.Controls.Count;

                        for (int i = 0; i < count; i++)
                        {
                            this.Controls[i].KeyDown += new KeyEventHandler(Client_Form_KeyDown);
                        }

                        //���� �� ��ȭ ���� ����, ���� ����
                        MemoFileCheck();
                        DialogFileCheck();
                        FileDirCheck();

                        if (checkThread == null)
                        {
                            checkThread = new Thread(new ThreadStart(SendCheck));
                            checkThread.Start();
                            logWrite("SendCheck ������ ����");
                        }

                        break;


                    case "M": //�ٸ� Ŭ���̾�Ʈ ��� �� ���ӻ��� ����(M|���̸�|id!����̸�|id!����̸�)


                        if (tempMsg[1].Equals("e")) //��� ��Ʈ�� ���� ���ۿϷ� �޽����� ��� -> Client_Form�� �α��� ���·� ���� ������Ҹ� Ȱ��ȭ �Ѵ�.
                        {
                            PanelCtrlDelegate loginPanel = new PanelCtrlDelegate(LogInPanelVisible); //�α��� �г� ��Ʈ�� ȣ���
                            PanelCtrlDelegate TreeViewPanel = new PanelCtrlDelegate(TreeViewVisible);  //memTreeView ��Ʈ��ȣ���
                            PanelCtrlDelegate btnCtrl = new PanelCtrlDelegate(ButtonCtrl);//���� ��ư ��Ʈ��ȣ���

                            Invoke(TreeViewPanel, true);

                            // ��ư Ȱ��ȭ
                            Invoke(btnCtrl, true);

                            //tooltip ����
                            tooltip.AutoPopDelay = 0;
                            tooltip.AutomaticDelay = 2000;
                            tooltip.InitialDelay = 100;

                            Invoke(loginPanel, false);

                            //crm ���α׷� ����
                            Thread thread = new Thread(new ThreadStart(SetUserInfo));
                            thread.Start();
                            //NoParamDele dele = new NoParamDele(SetUserInfo);
                            //Invoke(dele);
                        }
                        else // ��Ʈ�� ������ ������ ���
                        {
                            ArrayList list = new ArrayList();
                            if (tempMsg.Length > 2)
                            {
                                int m = 0;
                                for (int i = 2; i < tempMsg.Length; i++) //�迭 ���� 2��° ������ id!name�� ����
                                {
                                    if (tempMsg[i].Length != 0)
                                    {
                                        list.Add(tempMsg[i]);
                                        string[] memInfo = tempMsg[i].Split('!');  //<id>�� <name>�� �и��Ͽ� memInfo�� ����
                                        MemberInfoList[memInfo[0]] = memInfo[1];
                                        logWrite("MemberInfoList[" + memInfo[0] + "] = " + memInfo[1]);
                                        TeamInfoList[memInfo[0]] = tempMsg[1];      //�ٸ� ������ <�Ҽ�>�� <id>�� hashtable�� ����(key=id, value=�Ҽ�)
                                        logWrite("TeamInfoList[" + memInfo[0] + "] = " + tempMsg[1]);
                                        //logWrite("����� ���� ��� : �̸�(" + memInfo[1] + ") ���̵�(" + memInfo[0] + ")");
                                    }

                                }
                                ChildNodeDelegate AddMemNode = new ChildNodeDelegate(MakeMemTree);
                                object[] ar = { tempMsg[1], list };
                                Invoke(AddMemNode, ar);

                                treesource[tempMsg[1]] = list;
                                memTree.Tag = treesource;
                                logWrite(tempMsg[1] + " �� ����Ʈ ����!");

                                //��������Ʈ ����

                            }
                        }
                        break;

                    case "y":    //�α��� Client ����Ʈ y|id|���°� 
                        string team = (string)TeamInfoList[tempMsg[1]];

                        int Port = 8883;
                        logWrite("�� : " + team + " ����� id : " + tempMsg[1] + "  port : " + Port.ToString());

                        //InList[tempMsg[1]] = server;   //�α��� ����Ʈ ���̺� ����(key=id, value=IPEndPoint)

                        ChangeStat changelogin = new ChangeStat(ChangeMemStat);
                        Invoke(changelogin, new object[] { tempMsg[1], tempMsg[2] });

                        break;

                    case "IP":    //�α��� Client ����Ʈ IP|id|ip�ּ� 

                        Port = 8883;
                        logWrite(" ����� id : " + tempMsg[1] + "IP �ּ� : " + tempMsg[2] + "  port : " + Port.ToString());

                        InList[tempMsg[1]] = new IPEndPoint(IPAddress.Parse(tempMsg[2]), Port);   //�α��� ����Ʈ ���̺� ����(key=id, value=IPEndPoint)

                        break;

                    case "a":  //�ߺ��α��� �õ��� �˷���
                        Loginfail relogin = new Loginfail(ReLogin);
                        Invoke(relogin, null);

                        break;

                    case "u": //���������� ���� �α׾ƿ� �޽��� ������ ���
                        LogOutDelegate logoutDel = new LogOutDelegate(LogOut);
                        Invoke(logoutDel, null);
                        break;

                    case "d":  //���� ��ȭ�޽����� ��� (d|Formkey|id/id/...|name|�޽���)

                        string[] ids = tempMsg[2].Split('/');
                        if (!ids[0].Equals(myid))
                        {
                            if (ChatFormList.Contains(tempMsg[1]) && ChatFormList[tempMsg[1]] != null)  //�̹� �߽��ڿ� ä������ ���
                            {
                                ChatForm form = (ChatForm)ChatFormList[tempMsg[1]];
                                AddChatMsg addMsg = new AddChatMsg(Addmsg);
                                string smsg = tempMsg[3] + "|" + tempMsg[4];
                                Invoke(addMsg, new object[] { smsg, form });

                                //if() ����� ���¿� ����
                                //form.Activate();
                            }
                            else                                   //���ο� ��ȭ��û�� ���
                            {
                                ArrangeCtrlDelegate OpenChatForm = new ArrangeCtrlDelegate(NewChatForm);
                                Invoke(OpenChatForm, new object[] { tempMsg });
                            }
                        }
                        break;

                    case "c":  //c|formkey|id/id/..|name  //��ȭ�� �ʴ밡 �Ͼ ���

                        if (ChatFormList.ContainsKey(tempMsg[1]) && ChatFormList[tempMsg[1]] != null)
                        {
                            string[] nameArray = tempMsg[2].Split('/');
                            ChatForm form = (ChatForm)ChatFormList[tempMsg[1]];
                            AddChatter addChatter = new AddChatter(Addchatter);
                            AddChatMsg addMsg = new AddChatMsg(Addmsg);

                            for (int i = 0; i < (nameArray.Length - 1); i++)
                            {
                                string tempname = getName(nameArray[i]);
                                Invoke(addChatter, new object[] { nameArray[i], tempname, form });
                                //string tempmsg = tempMsg[3] + "���� " + tempname + "���� ��ȭ�� �ʴ��Ͽ����ϴ�.";
                                //Invoke(addMsg, new object[] { tempmsg, form });
                            }
                        }

                        break;

                    case "C":  //�������� ����Ȯ�� ����(C|Ȯ����id|noticeid)
                        ArrangeCtrlDelegate NoticeDetailResultFormAdd = new ArrangeCtrlDelegate(NoticeReaderAdd);
                        Invoke(NoticeDetailResultFormAdd, new object[] { tempMsg });
                        break;

                    case "q": //���� ��ȭ�� ������ ��ȭâ ���� (q|Formkey|id)

                        if (ChatFormList.ContainsKey(tempMsg[1]) && ChatFormList[tempMsg[1]] != null)
                        {
                            ChatForm form = (ChatForm)ChatFormList[tempMsg[1]];
                            DelChatterDelegate delchatter = new DelChatterDelegate(DelChatter);
                            Invoke(delchatter, new object[] { tempMsg[2], form });
                        }

                        break;

                    case "dc": //���� ��ȭ�� ������ ���� ���� (dc|Formkey|id)

                        if (ChatFormList.ContainsKey(tempMsg[1]) && ChatFormList[tempMsg[1]] != null)
                        {
                            ChatForm form = (ChatForm)ChatFormList[tempMsg[1]];

                        }

                        break;

                    case "m"://�޸� ������ ��� m|name|id|message|������id
                        ArrangeCtrlDelegate memo = new ArrangeCtrlDelegate(MakeMemo);
                        Invoke(memo, new object[] { tempMsg });
                        MemoFileWrite(msg + "|" + DateTime.Now.ToString());
                        break;


                    case "F":  //�������� �� ���� ���� ����     F|���ϸ�|����ũ��|����key|������id
                        string name = getName(tempMsg[4]);
                        string[] filenameArray = tempMsg[1].Split('\\'); //���ϸ� ����
                        int filesize = int.Parse(tempMsg[2]);            //����ũ�� ĳ����
                        IPEndPoint siep = (IPEndPoint)InList[tempMsg[4]];
                        siep.Port = 8883;
                        string savefilename = "C:\\MiniCTI\\" + this.myid + "\\Files\\" + filenameArray[filenameArray.Length - 1];
                        if (InList.ContainsKey(tempMsg[4]) && InList[tempMsg[4]] != null)
                        {
                            Hashtable info = new Hashtable();
                            info[savefilename] = filesize;
                            info["name"] = name;
                            info["senderid"] = tempMsg[4];
                            SendMsg("Y|" + tempMsg[1] + "|" + tempMsg[3] + "|" + this.myid, siep); //����(Y|���ϸ�|����Key|������id)
                            FileReceiver((object)info);
                        }
                        downloadform.Show();
                        break;

                    case "Y"://���� �ޱ� ���� �޽���(Y|���ϸ�|����key|������id)
                        ShowFileSendDetailDelegate show = new ShowFileSendDetailDelegate(ShowFileSendDetail);
                        Hashtable table = new Hashtable();
                        table[tempMsg] = (IPEndPoint)InList[tempMsg[3]];

                        //���� ���� ������ ����
                        Thread sendfile = new Thread(new ParameterizedThreadStart(SendFile));
                        sendfile.Start((object)table);

                        FileSendThreadList[tempMsg[2] + "|" + tempMsg[3]] = sendfile;

                        FileSendDetailListView view = (FileSendDetailListView)FileSendDetailList[tempMsg[2]];
                        Invoke(show, new object[] { tempMsg[3], "���۴����", view });

                        break;

                    case "FS"://���� �ޱ� ���� �޽���(FS|���ϸ�|����key|������id)
                        ShowFileSendDetailDelegate show_ = new ShowFileSendDetailDelegate(ShowFileSendDetail);
                        Hashtable table_ = new Hashtable();
                        table_[tempMsg] = server;

                        //���� ���� ������ ����
                        Thread sendfile_ = new Thread(new ParameterizedThreadStart(SendFile));
                        sendfile_.Start((object)table_);

                        FileSendThreadList[tempMsg[2] + "|" + tempMsg[3]] = sendfile_;

                        FileSendDetailListView view_ = (FileSendDetailListView)FileSendDetailList[tempMsg[2]];
                        Invoke(show_, new object[] { tempMsg[3], "���۴����", view_ });

                        break;

                    case "N": //���� �ޱ� �ź�("N|���ϸ�|����Ű|id)

                        ShowFileSendDetailDelegate detail = new ShowFileSendDetailDelegate(ShowFileSendDetail);

                        FileSendDetailListView View = (FileSendDetailListView)FileSendDetailList[tempMsg[2]];

                        Invoke(detail, new object[] { tempMsg[3], "�ޱ� �ź�", View });

                        string Name = getName(tempMsg[3]);
                        string[] FilenameArray = tempMsg[1].Split('\\');
                        MessageBox.Show(Name + " ���� ���� �ޱ⸦ �ź��ϼ̽��ϴ�.\r\n\r\n���ϸ� : " + FilenameArray[(FilenameArray.Length - 1)], "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;

                    case "i":  //�߰� �α��� ������ ���  ���� : i|id|�Ҽ�����|ip|�̸�

                        try
                        {
                            //�α��� ����Ʈ ���̺� �߰�
                            IPEndPoint addiep = new IPEndPoint(IPAddress.Parse(tempMsg[3]), 8883);
                            InList[tempMsg[1]] = addiep;
                            //memTree �信 �α��� ����� ���� ����
                            ChangeStat change = new ChangeStat(ChangeMemStat);
                            Invoke(change, new object[] { tempMsg[1], "online" });
                            TeamInfoList[tempMsg[1]] = tempMsg[2];

                        }
                        catch (Exception e)
                        {
                            logWrite(e.ToString());
                        }
                        break;

                    case "o":  //�α׾ƿ� ������ �߻��� ���  o|id|�Ҽ�
                        FormTextCtrlDelegate logoutchatter = new FormTextCtrlDelegate(LogoutChatter);
                        Invoke(logoutchatter, tempMsg[1]);
                        lock (this)
                        {
                            InList[tempMsg[1]] = null;
                        }
                        ChangeStat logout = new ChangeStat(ChangeLogout);
                        Invoke(logout, new object[] { tempMsg[1], tempMsg[2] });

                        break;

                    case "n":  //�������� �޽��� (n|�޽���|�߽���id|mode|noticetime|����)

                        logWrite("�������� ����!");
                        if (!tempMsg[2].Equals(this.myid)) //�ڱⰡ ���� ������ ��� ������ ����
                        {
                            ArrangeCtrlDelegate notice = new ArrangeCtrlDelegate(ShowNoticeDirect);
                            Invoke(notice, new object[] { tempMsg });
                        }

                        break;

                    case "A": //������ ���� ����(A|mnum|fnum|nnum|tnum)
                        ArrangeCtrlDelegate ShowAbsentInfo = new ArrangeCtrlDelegate(ShowAbsentInfoNumber);
                        Invoke(ShowAbsentInfo, new object[] { tempMsg });

                        break;

                    case "Q"://������ �޸� ����Ʈ (Q|sender;content;time;seqnum|sender;content;time;seqnum|...
                        ArrangeCtrlDelegate ShowNRmemoList = new ArrangeCtrlDelegate(ShowMemoList);
                        Invoke(ShowNRmemoList, new object[] { tempMsg });

                        break;

                    case "T"://������ ���� ����Ʈ (T|sender;content;time;mode;seqnum|sender;content;time;mode;seqnum|...
                        ArrangeCtrlDelegate ShowNRnoticeList = new ArrangeCtrlDelegate(ShowNotReadNoticeList);
                        Invoke(ShowNRnoticeList, new object[] { tempMsg });

                        break;

                    case "R"://������ ���� ����Ʈ (R|�������;filenum;filename;time;size;seqnum|�������;filenum;filename;time;size;seqnum|...
                        ArrangeCtrlDelegate ShowNRFileList = new ArrangeCtrlDelegate(ShowFileList);
                        Invoke(ShowNRFileList, new object[] { tempMsg });

                        break;

                    case "t": //"t|ntime��content��nmode��title�Ӿ��������1:���������2:...|...

                        ArrangeCtrlDelegate ShowNoticeResultFromDB = new ArrangeCtrlDelegate(showNoticeResultFromDB);
                        Invoke(ShowNoticeResultFromDB, new object[] { tempMsg });

                        break;

                    case "L"://�������� ����Ʈ ������ ���  L|time!content!mode!sender!seqnum
                        if (!(tempMsg.Length < 2))
                        {
                            ArrangeCtrlDelegate makeNoticeList = new ArrangeCtrlDelegate(MakeNoticeListForm);
                            Invoke(makeNoticeList, new object[] { tempMsg });
                        }
                        else
                        {
                            MessageBox.Show("��ϵ� ������ �����ϴ�.", "��������", MessageBoxButtons.OK);
                        }
                        break;

                    case "s"://�� Ŭ���̾�Ʈ ���°� ���� �޽��� s|id|����|IPAddress
                        if (!tempMsg[1].Equals(this.myid))
                        {
                            ChangeStat presenceupdate = new ChangeStat(PresenceUpdate);
                            Invoke(presenceupdate, new object[] { tempMsg[1], tempMsg[2] });
                            IPEndPoint tempiep = new IPEndPoint(IPAddress.Parse(tempMsg[3]), listenport);
                            lock (InList)
                            {
                                InList[tempMsg[1]] = tempiep;
                            }
                        }
                        break;

                    case "Ring": //�߽��� ǥ��(Ring|ani|name|server_type)

                        RingingDele ringdele = new RingingDele(Ringing);
                        Invoke(ringdele, new object[] { tempMsg[1], tempMsg[2], tempMsg[3] });

                        break;

                    case "Dial": //���̾�� ������ �˾�(Dial|ani)
                        doublestringDele dialdele = new doublestringDele(Answer);
                        Invoke(dialdele, new object[] { tempMsg[1], "2" });
                        break;

                    case "Answer": //offhook�� ������ �˾�(Answer|ani|type)
                        doublestringDele answerdele = new doublestringDele(Answer);
                        Invoke(answerdele, new object[] { tempMsg[1], tempMsg[2] });

                        break;

                    case "Abandon": //Abandon �߻���
                        AbandonDele abandon = new AbandonDele(Abandoned);
                        Invoke(abandon);
                        break;

                    case "Other": //�ٸ������ �����
                        AbandonDele other = new AbandonDele(OtherAnswer);
                        Invoke(other);
                        break;

                    case "pass"://������ ���� ����(pass|ani|senderID|receiverID
                        if (tempMsg[2].Equals(this.myid))
                        {
                            //MessageBox.Show("�̰��Ǿ����ϴ�.");
                        }
                        else
                        {
                            //notifyTransfer(tempMsg);
                            ArrangeCtrlDelegate passdele = new ArrangeCtrlDelegate(notifyTransfer);
                            Invoke(passdele, new object[] { tempMsg });
                        }
                        break;

                    case "trans"://������ �̰� �޽ý� ����(trans|sender��content��time��seqnum|...)
                        ArrangeCtrlDelegate ShowNRTransList = new ArrangeCtrlDelegate(showNoreadTransfer);
                        Invoke(ShowNRTransList, new object[] { tempMsg });
                        break;

                }
            }
            catch (Exception exception)
            {
                logWrite(exception.StackTrace);
            }
        }

        private void Client_Form_onLoginEvent()
        {
            SetUserInfo();
        }

        private void notifyTransfer(string[] tempMsg)//pass|ani|senderID|receiverID|TONG_DATE|TONG_TIME|CustomerName
        {
            try
            {
                notifyform = new NotifyForm();
                notifyform.button1.MouseClick += new MouseEventHandler(NotifyForm_Confirm_MouseClick);
                notifyform.Tag = tempMsg[1];
                notifyform.label_TONGDATE.Text = tempMsg[4];
                notifyform.label_TONGTIME.Text = tempMsg[5];
                notifyform.label_ani.Text = tempMsg[1];
                notifyform.label_senderid.Text = tempMsg[2];
                if (tempMsg.Length > 6)
                {
                    if (tempMsg[6].Length > 0)
                    {
                        notifyform.label_Customer.Text = tempMsg[6];
                    }
                    else
                    {
                        notifyform.label_Customer.Text = tempMsg[1];
                    }

                    string senderName = getName(tempMsg[2]);
                    notifyform.label_sender.Text = "from " + senderName + "(" + tempMsg[2] + ")";
                }

                notifyform.Focus();
                notifyform.Show();
                timerForNotify.Start();

            }
            catch (Exception ex)
            {

            }
        }

        private void timerForNotify_Tick(object sender, EventArgs e)
        {
            try
            {
                if (notifyform != null)
                {
                    timerForNotify.Stop();
                    int height_point = 0;

                    if (TransferNotiArea.Count > 0)
                    {
                        foreach (DictionaryEntry de in TransferNotiArea)
                        {
                            if (de.Value.ToString().Equals("0"))
                            {
                                int temp = Convert.ToInt32(de.Key.ToString());
                                if (temp > height_point)
                                {
                                    height_point = temp;
                                }
                            }
                            else
                            {
                                logWrite("TransferNotiArea[" + de.Key.ToString() + "] = " + de.Value.ToString());
                                logWrite(de.Key.ToString() + " is not 0");
                            }
                        }

                        if (height_point == 0)
                        {
                            //���� ������ �±��� ����
                            NoParamDele dele = new NoParamDele(closeNoticeForm);
                            Invoke(dele);

                            foreach (DictionaryEntry de in TransferNotiArea)
                            {
                                if (de.Value.ToString().Equals("0"))
                                {
                                    int temp = Convert.ToInt32(de.Key.ToString());
                                    if (temp > height_point)
                                    {
                                        height_point = temp;
                                    }
                                }
                                else
                                {
                                    logWrite("TransferNotiArea[" + de.Key.ToString() + "] = " + de.Value.ToString());
                                    logWrite(de.Key.ToString() + " is not 0");
                                }
                            }
                        }
                    }
                    TransferNotiForm miniform = new TransferNotiForm();
                    miniform.pbx_icon.Image = global::Client.Properties.Resources.img_customer;
                    miniform.MouseClick += new MouseEventHandler(miniform_MouseClick);
                    miniform.pbx_icon.MouseClick += new MouseEventHandler(pbx_icon_MouseClick_for_Transfer);
                    miniform.label_Customer.MouseClick += new MouseEventHandler(label_Customer_MouseClick);
                    miniform.label_from.MouseClick += new MouseEventHandler(label_Customer_MouseClick);
                    miniform.label_Customer.Text = notifyform.label_Customer.Text;
                    miniform.label_from.Text = notifyform.label_sender.Text;
                    miniform.label_ani.Text = notifyform.label_ani.Text;
                    miniform.label_date.Text = notifyform.label_TONGDATE.Text;
                    miniform.label_time.Text = notifyform.label_TONGTIME.Text;
                    miniform.label_senderid.Text = notifyform.label_senderid.Text;
                    screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
                    screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
                    miniform.SetBounds(screenWidth - miniform.Width, height_point, miniform.Width, miniform.Height);
                    notifyform.Close();
                    notifyform = null;
                    miniform.TopLevel = true;
                    miniform.Show();
                    TransferNotiArea[height_point.ToString()] = "1";
                    NotiFormList.Add(miniform);
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void closeNoticeForm()
        {
            try
            {
                logWrite("closeNoticeForm ����");
                TransferNotiForm miniform = (TransferNotiForm)NotiFormList[0];
                if (TransferNotiArea.ContainsKey(miniform.Top.ToString()))
                {
                    TransferNotiArea[miniform.Top.ToString()] = "0";
                }
                miniform.Close();
                NotiFormList.RemoveAt(0);
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }


        private void pbx_icon_MouseClick_for_Transfer(object sender, MouseEventArgs e)
        {
            try
            {

                PictureBox label = (PictureBox)sender;

                TransferNotiForm miniform = (TransferNotiForm)label.Parent;

                ShowTransInfoDele dele = new ShowTransInfoDele(showTransferInfo);

                Invoke(dele, new object[] { miniform.label_ani.Text, miniform.label_senderid.Text, miniform.label_date.Text, miniform.label_time.Text });
                if (TransferNotiArea.ContainsKey(miniform.Top.ToString()))
                {
                    TransferNotiArea[miniform.Top.ToString()] = "0";
                }

                foreach (TransferNotiForm form in NotiFormList)
                {
                    if (miniform.Equals(form))
                    {
                        logWrite("���� TransferNotiForm ã��!");
                        NotiFormList.Remove(form);
                        break;
                    }
                }
                miniform.Close();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void label_Customer_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                Label label = (Label)sender;

                TransferNotiForm miniform = (TransferNotiForm)label.Parent;

                ShowTransInfoDele dele = new ShowTransInfoDele(showTransferInfo);

                Invoke(dele, new object[] { miniform.label_ani.Text, miniform.label_senderid.Text, miniform.label_date.Text, miniform.label_time.Text });
                if (TransferNotiArea.ContainsKey(miniform.Top.ToString()))
                {
                    TransferNotiArea[miniform.Top.ToString()] = "0";
                }
                foreach (TransferNotiForm form in NotiFormList)
                {
                    if (miniform.Equals(form))
                    {
                        logWrite("���� TransferNotiForm ã��!");
                        NotiFormList.Remove(form);
                        break;
                    }
                }
                miniform.Close();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }

        }

        /// <summary>
        /// ������ȭ �±��� Ŭ���� ������â �˾� ó��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Customer_MouseClick_for_Call(object sender, MouseEventArgs e)
        {
            try
            {

                Label label = (Label)sender;

                TransferNotiForm miniform = (TransferNotiForm)label.Parent;

                string ani = "";
                string temp = miniform.label_Customer.Text;
                string[] tempArr = temp.Split('(');
                if (tempArr.Length > 1)
                {
                    ani = tempArr[1].Split(')')[0];
                }
                else
                {
                    ani = temp;
                }

                doublestringDele dele = new doublestringDele(showCustomerPopup);
                Invoke(dele, new object[] { ani, "1" });

                logWrite("miniform.Top = " + miniform.Top.ToString());
                if (TransferNotiArea.ContainsKey(miniform.Top.ToString()))
                {
                    TransferNotiArea[miniform.Top.ToString()] = "0";
                }

                foreach (TransferNotiForm form in NotiFormList)
                {
                    if (miniform.Equals(form))
                    {
                        logWrite("���� TransferNotiForm ã��!");
                        NotiFormList.Remove(form);
                        break;
                    }
                }
                miniform.Close();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }

        }


        private void miniform_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                TransferNotiForm miniform = (TransferNotiForm)sender;

                ShowTransInfoDele dele = new ShowTransInfoDele(showTransferInfo);

                Invoke(dele, new object[] { miniform.label_ani.Text, miniform.label_senderid.Text, miniform.label_date.Text, miniform.label_time.Text });
                if (TransferNotiArea.ContainsKey(miniform.Top.ToString()))
                {
                    TransferNotiArea[miniform.Top.ToString()] = "0";
                }

                foreach (TransferNotiForm form in NotiFormList)
                {
                    if (miniform.Equals(form))
                    {
                        logWrite("���� TransferNotiForm ã��!");
                        NotiFormList.Remove(form);
                        break;
                    }
                }
                miniform.Close();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }

        }

        /// <summary>
        /// ������ȭ �±��� Ŭ���� ������ ȭ�� �˾�ó��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miniform_MouseClick_for_Call(object sender, MouseEventArgs e)
        {
            try
            {
                TransferNotiForm miniform = (TransferNotiForm)sender;
                string ani = "";
                string temp = miniform.label_Customer.Text;
                string[] tempArr = temp.Split('(');
                if (tempArr.Length > 1)
                {
                    ani = tempArr[1].Split(')')[0];
                }
                else
                {
                    ani = temp;
                }
                doublestringDele dele = new doublestringDele(showCustomerPopup);
                Invoke(dele, new object[] { ani, "1" });

                logWrite("miniform.Top = " + miniform.Top.ToString());
                if (TransferNotiArea.ContainsKey(miniform.Top.ToString()))
                {
                    TransferNotiArea[miniform.Top.ToString()] = "0";
                }

                foreach (TransferNotiForm form in NotiFormList)
                {
                    if (miniform.Equals(form))
                    {
                        logWrite("���� TransferNotiForm ã��!");
                        NotiFormList.Remove(form);
                        break;
                    }
                }
                miniform.Close();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }

        }

        private void showTransferInfo(string ani, string senderid, string tong_date, string tong_time)
        {
            logWrite("showTransferInfo(" + ani + ", " + senderid + ", " + tong_date + ", " + tong_time);
            try
            {
                crm_main.OpenCustomerPopupTransfer(ani, senderid, tong_date, tong_time, "3");
                crm_main.WindowState = FormWindowState.Normal;
                crm_main.StartPosition = FormStartPosition.Manual;
                crm_main.SetBounds(0, 0, crm_main.Width, crm_main.Height);
                crm_main.Show();
                crm_main.Activate();
            }
            catch (System.ObjectDisposedException dis)
            {
                try
                {
                    cm.SetUserInfo(this.com_cd, this.myid, tbx_pass.Text, serverIP, socket_port_crm);
                    crm_main = new FRM_MAIN();
                    crm_main.StartPosition = FormStartPosition.Manual;
                    crm_main.SetBounds(0, 0, crm_main.Width, crm_main.Height);
                    crm_main.OpenCustomerPopupTransfer(ani, senderid, tong_date, tong_time, "3");
                    crm_main.Show();
                }
                catch (Exception ex1)
                {
                    logWrite(ex1.ToString());
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void NotifyForm_Confirm_MouseClick(object sender, MouseEventArgs e)
        {
            if (notifyform != null)
            {
                timerForNotify.Stop();
                ShowTransInfoDele dele = new ShowTransInfoDele(showTransferInfo);

                Invoke(dele, new object[] { notifyform.label_ani.Text, notifyform.label_senderid.Text, notifyform.label_TONGDATE.Text, notifyform.label_TONGTIME.Text });
                notifyform.Close();

            }
        }

        private void makeSelectTransferForm(string ani)
        {
            try
            {
                selecttransferform = new SelectTransferForm();
                selecttransferform.Tag = ani;
                selecttransferform.pbx_trans_confirm.MouseClick += new MouseEventHandler(pbx_trans_confirm_MouseClick);
                foreach (DictionaryEntry de in InList)
                {
                    if (!de.Key.ToString().Equals(this.myid))
                    {
                        if (de.Value != null)
                        {
                            string tempName = getName(de.Key.ToString());
                            selecttransferform.cbx_receiver.Items.Add(tempName + "(" + de.Key.ToString() + ")");
                        }
                    }
                }
                selecttransferform.Show();
                selecttransferform.cbx_receiver.DroppedDown = true;
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void pbx_trans_confirm_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string itemstring = selecttransferform.cbx_receiver.SelectedItem.ToString();
                string[] tempArr = itemstring.Split('(');
                string[] tempArr1 = tempArr[1].Split(')');
                string receiverID = tempArr1[0];
                string ani = selecttransferform.Tag.ToString();
                selecttransferform.Close();
                string tempmsg = "23|" + ani + "|" + receiverID;
                SendMsg(tempmsg, server);
                MessageBox.Show("������ ���� �Ϸ�!", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void chageProgressbar(string status)
        {
            label_progress_status.Text = status;
        }

        private void SetUserInfo()
        {
            logWrite("SetUserInfo ����");
            logWrite("com_cd = " + this.com_cd);
            logWrite("myid = " + this.myid);
            logWrite("pass = " + tbx_pass.Text);

            cm.SetUserInfo(this.com_cd, this.myid, tbx_pass.Text, serverIP, socket_port_crm);

            NoParamDele dele = new NoParamDele(startCRMmanager);
            Invoke(dele);
        }

        private void startCRMmanager()
        {
            try
            {
                crm_main = new FRM_MAIN();

                crm_main.StartPosition = FormStartPosition.Manual;
                crm_main.SetBounds(0, 0, crm_main.Width, crm_main.Height);
                crm_main.Activate();
                crm_main.Show();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }


        /// <summary>
        /// �߽��� ǥ�� ó��
        /// </summary>
        /// <param name="ani"></param>
        /// <param name="name"></param>
        /// <param name="server_type"></param>

        private void Ringing(string ani, string name, string server_type)
        {
            try
            {
                CustomerList[ani] = name;
                if (popform != null)
                {
                    t1.Stop();
                    popform.Close();
                }
                //getForegroundWindow();
                popform = new PopForm();
                popform.Tag = name;
                if (name.Length > 0)
                {
                    popform.label1.Text = name + "\r\n" + ani;
                }
                else
                {
                    popform.label1.Text = ani;
                }

                if (isHide == false && firstCall == false)
                {
                    popform.TopMost = true;
                    firstCall = true;
                }
                else
                {
                    popform.TopMost = false;
                }
                this.TopLevel = true;
                popform.TopLevel = true;
                popform.Show();
                //getForegroundWindow();



                t1.Start();

            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }


        private void Answer(string ani, string calltype)
        {

            if (popform != null)
            {
                t1.Stop();
                if (nopop == true)
                {
                    string name = popform.Tag.ToString();
                    showAnswerCallInfo(ani, name);
                }
                popform.Close();

            }
            //cm.POPUP(ani, DateTime.Now.ToString("yyyyMMddhhmmss"), "1");
            //cm.SetUserInfo(this.com_cd, this.myid, tbx_pass.Text, serverIP, socket_port_crm);

            if (nopop == false)
            {
                try
                {
                    //getForegroundWindow();
                    crm_main.OpenCustomerPopup(ani, DateTime.Now.ToString("yyyyMMddHHmmss"), calltype);
                    crm_main.WindowState = FormWindowState.Normal;
                    crm_main.StartPosition = FormStartPosition.Manual;
                    crm_main.SetBounds(0, 0, crm_main.Width, crm_main.Height);
                    crm_main.Show();
                    crm_main.Activate();
                    crm_main.TopLevel = true;

                    //crm_main.Activated += new EventHandler(crm_main_Activated);

                }
                catch (System.ObjectDisposedException dis)
                {
                    //getForegroundWindow();
                    cm.SetUserInfo(com_cd, this.myid, tbx_pass.Text, serverIP, socket_port_crm);
                    crm_main = new FRM_MAIN();
                    crm_main.FormClosing += new FormClosingEventHandler(crm_main_FormClosing);
                    crm_main.StartPosition = FormStartPosition.Manual;
                    crm_main.SetBounds(0, 0, crm_main.Width, crm_main.Height);
                    crm_main.OpenCustomerPopup(ani, DateTime.Now.ToString("yyyyMMddHHmmss"), calltype);
                    crm_main.WindowState = FormWindowState.Normal;
                    crm_main.Show();
                    crm_main.Activate();
                    crm_main.TopLevel = true;
                }
            }
        }


        private void showCustomerPopup(string ani, string calltype)
        {
            try
            {
                //getForegroundWindow();
                crm_main.OpenCustomerPopup(ani, DateTime.Now.ToString("yyyyMMddHHmmss"), calltype);
                crm_main.WindowState = FormWindowState.Normal;
                crm_main.StartPosition = FormStartPosition.Manual;
                crm_main.SetBounds(0, 0, crm_main.Width, crm_main.Height);
                crm_main.Show();
                crm_main.Activate();
                crm_main.TopLevel = true;

                //crm_main.Activated += new EventHandler(crm_main_Activated);

            }
            catch (System.ObjectDisposedException dis)
            {
                //getForegroundWindow();
                cm.SetUserInfo(com_cd, this.myid, tbx_pass.Text, serverIP, socket_port_crm);
                crm_main = new FRM_MAIN();
                crm_main.FormClosing += new FormClosingEventHandler(crm_main_FormClosing);
                crm_main.StartPosition = FormStartPosition.Manual;
                crm_main.SetBounds(0, 0, crm_main.Width, crm_main.Height);
                crm_main.OpenCustomerPopup(ani, DateTime.Now.ToString("yyyyMMddHHmmss"), calltype);
                crm_main.WindowState = FormWindowState.Normal;
                crm_main.Show();
                crm_main.Activate();
                crm_main.TopLevel = true;
            }
        }

        private void showAnswerCallInfo(string ANI, string name)
        {
            try
            {

                int height_point = 0;

                if (TransferNotiArea.Count > 0)
                {
                    foreach (DictionaryEntry de in TransferNotiArea)
                    {
                        if (de.Value.ToString().Equals("0"))
                        {
                            int temp = Convert.ToInt32(de.Key.ToString());
                            if (temp > height_point)
                            {
                                height_point = temp;
                            }
                        }
                        else
                        {
                            logWrite("TransferNotiArea[" + de.Key.ToString() + "] = " + de.Value.ToString());
                            logWrite(de.Key.ToString() + " is not 0");
                        }
                    }

                    if (height_point == 0)
                    {
                        //���� ������ �±��� ����
                        NoParamDele dele = new NoParamDele(closeNoticeForm);
                        Invoke(dele);

                        foreach (DictionaryEntry de in TransferNotiArea)
                        {
                            if (de.Value.ToString().Equals("0"))
                            {
                                int temp = Convert.ToInt32(de.Key.ToString());
                                if (temp > height_point)
                                {
                                    height_point = temp;
                                }
                            }
                            else
                            {
                                logWrite("TransferNotiArea[" + de.Key.ToString() + "] = " + de.Value.ToString());
                                logWrite(de.Key.ToString() + " is not 0");
                            }
                        }
                    }
                }

                TransferNotiForm miniform = new TransferNotiForm();
                miniform.TopMost = false;
                miniform.pbx_icon.Image = global::Client.Properties.Resources.phone_black;
                miniform.pbx_close.Visible = true;
                miniform.pbx_close.MouseClick += new MouseEventHandler(pbx_close_MouseClick);
                miniform.pbx_icon.MouseClick += new MouseEventHandler(pbx_icon_MouseClick);
                miniform.MouseClick += new MouseEventHandler(miniform_MouseClick_for_Call);
                miniform.label_Customer.MouseClick += new MouseEventHandler(label_Customer_MouseClick_for_Call);
                miniform.label_from.MouseClick += new MouseEventHandler(label_Customer_MouseClick_for_Call);
                if (name.Length > 1)
                {
                    miniform.label_Customer.Text = name + "(" + ANI + ")";
                }
                else
                {
                    miniform.label_Customer.Text = ANI;
                }
                //miniform.label_from.Text = notifyform.label_sender.Text;
                //miniform.label_ani.Text = notifyform.label_ani.Text;
                //miniform.label_date.Text = notifyform.label_TONGDATE.Text;
                //miniform.label_time.Text = notifyform.label_TONGTIME.Text;
                miniform.label_from.Text = "�ð� : " + DateTime.Now.ToShortTimeString();
                screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
                screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
                miniform.SetBounds(screenWidth - miniform.Width, height_point, miniform.Width, miniform.Height);
                miniform.Show();
                TransferNotiArea[height_point.ToString()] = "1";
                NotiFormList.Add(miniform);
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void pbx_icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                PictureBox label = (PictureBox)sender;

                TransferNotiForm miniform = (TransferNotiForm)label.Parent;

                string ani = "";
                string temp = miniform.label_Customer.Text;
                string[] tempArr = temp.Split('(');
                if (tempArr.Length > 1)
                {
                    ani = tempArr[1].Split(')')[0];
                }
                else
                {
                    ani = temp;
                }

                doublestringDele dele = new doublestringDele(showCustomerPopup);
                Invoke(dele, new object[] { ani, "1" });

                logWrite("miniform.Top = " + miniform.Top.ToString());
                if (TransferNotiArea.ContainsKey(miniform.Top.ToString()))
                {
                    TransferNotiArea[miniform.Top.ToString()] = "0";
                }

                foreach (TransferNotiForm form in NotiFormList)
                {
                    if (miniform.Equals(form))
                    {
                        logWrite("���� TransferNotiForm ã��!");
                        NotiFormList.Remove(form);
                        break;
                    }
                }
                miniform.Close();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        /// <summary>
        /// �ν���Ʈ ���Ÿ�� ��ũ �ݱ� ��ư ó�� : �±����� �ݰ� �ش� TransferNotiArea�� ����.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbx_close_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                PictureBox pbx = (PictureBox)sender;
                TransferNotiForm miniform = (TransferNotiForm)pbx.Parent;
                logWrite("miniform.Top = " + miniform.Top.ToString());
                TransferNotiArea[miniform.Top.ToString()] = "0";
                foreach (TransferNotiForm form in NotiFormList)
                {
                    if (miniform.Equals(form))
                    {
                        logWrite("���� TransferNotiForm ã��!");
                        NotiFormList.Remove(form);
                        break;
                    }
                }
                miniform.Close();

            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void crm_main_Activated(object sender, EventArgs e)
        {
            getForegroundWindow();
        }

        private void getForegroundWindow()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            if (hwnd != null)
            {
                GetWindowThreadProcessId(hwnd, out pid);
                System.Diagnostics.Process CurProc = System.Diagnostics.Process.GetProcessById((int)pid);
                logWrite("GetForegroundWindow() : " + CurProc.ProcessName);

            }

        }

        private void crm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                crm_main.Hide();
            }

        }



        private void Client_Form_onAnswerEvent(string ani)
        {
            //Answer(ani);
        }

        /// <summary>
        /// ����ȣ�� �ٸ� Ŭ���̾�Ʈ�� ���Ž� ���︲â ó��
        /// </summary>
        private void OtherAnswer()
        {
            if (popform != null)
            {
                t1.Stop();
                popform.Close();

            }

        }

        private void Abandoned()
        {
            if (popform != null)
            {
                t1.Stop();
                popform.Close();

            }


            //if (missedcallform == null)
            //{
            //    missedcallform = new MissedCallForm();
            //    missedcallform.SetBounds(screenWidth - missedcallform.Width, screenHeight - missedcallform.Height, missedcallform.Width, missedcallform.Height);
            //    missedcallform.MouseClick += new MouseEventHandler(missedcallform_MouseClick);
            //    missedcallform.label_missed.MouseClick += new MouseEventHandler(missedcallform_MouseClick);
            //    missedcallform.label_callcount.MouseClick += new MouseEventHandler(missedcallform_MouseClick);
            //    missedCallCount++;
            //    missedcallform.label_callcount.Text = missedCallCount.ToString() + " Call";
            //    missedcallform.Show();
            //    missedcallform.Activate();
            //}
            //else
            //{
            //    missedCallCount++;
            //    missedcallform.label_callcount.Text = missedCallCount.ToString() + " Call";
            //    missedcallform.Activate();
            //}
        }

        private void missedcallform_MouseClick(object sender, MouseEventArgs e)
        {
            if (missedcallform != null)
            {
                missedcallform.Close();
                missedcallform = null;
                missedCallCount = 0;
            }

            if (missedlistform != null)
            {

            }
        }

        /// <summary>
        /// ���� ����Ű ó��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M && e.Modifiers == Keys.Control) //���� ������
            {
                MakeSendMemo(new Hashtable());
            }
            else if (e.KeyCode == Keys.N && e.Modifiers == Keys.Control) //�����ϱ�
            {
                MakeSendNotice();
            }
            else if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control) //���� ������
            {
                MakeSendFileForm(new Hashtable());
            }
        }

        /// <summary>
        /// �� Ŭ���̾�Ʈ ���� ���� ó��
        /// </summary>
        /// <param name="statid"></param>
        /// <param name="presence"></param>
        private void PresenceUpdate(string statid, string presence)
        {
            try
            {

                if (TeamInfoList != null && TeamInfoList.ContainsKey(statid))
                {
                    string tname = (string)TeamInfoList[statid];
                    TreeNode[] teamNode = memTree.Nodes.Find(tname, false);
                    TreeNode[] memNode = teamNode[0].Nodes.Find(statid, false);
                    switch (presence)
                    {
                        case "busy":
                            memNode[0].ImageIndex = 6;
                            memNode[0].SelectedImageIndex = 6;
                            memNode[0].Text = getName(statid) + "(��ȭ��)";
                            break;

                        case "away":
                            memNode[0].ImageIndex = 4;
                            memNode[0].SelectedImageIndex = 4;
                            memNode[0].Text = getName(statid) + "(�ڸ����)";
                            break;

                        case "logout":
                            memNode[0].ForeColor = Color.Gray;
                            memNode[0].ImageIndex = 0;
                            memNode[0].SelectedImageIndex = 0;
                            InList[statid] = null;
                            memNode[0].Text = getName(statid);
                            break;

                        case "online":
                            memNode[0].ForeColor = Color.Black;
                            memNode[0].ImageIndex = 1;
                            memNode[0].SelectedImageIndex = 1;
                            memNode[0].Text = getName(statid);
                            break;

                        case "DND":
                            memNode[0].ImageIndex = 5;
                            memNode[0].SelectedImageIndex = 5;
                            memNode[0].Text = getName(statid) + "(�ٸ��빫��)";
                            break;
                    }


                    logWrite(statid + "�� ���°�" + presence + " �� ����");
                }

            }
            catch (Exception e)
            {
                logWrite(id + " ���°� ���� ���� : " + e.ToString());
            }
        }


        /// <summary>
        /// �������� ����Ʈ �� ����
        /// </summary>
        /// <param name="tempMsg"></param>
        private void ShowNotice(string[] tempMsg)  //n|�޽��� | �߽���id | mode | noticetime |seqnum| ����
        {
            try
            {
                if (tempMsg.Length > 6)
                {
                    string nname = getName(tempMsg[2]);
                    Notice nform = new Notice();
                    if (tempMsg[0].Equals("r"))  //������ �������� ����Ʈ���� ����
                    {
                        nform.btn_confirm.MouseClick += new MouseEventHandler(btn_confirm_Click);
                    }
                    else //�ǽð� �������� ���Ž� Ȯ�ΰ�� ���� ó��
                    {
                        nform.btn_confirm.MouseClick += new MouseEventHandler(sendReadNotice);
                    }
                    nform.FormClosing += new FormClosingEventHandler(nform_FormClosing);
                    string content = tempMsg[1];
                    nform.textBox.Text = content;
                    nform.label_noticetitle.Tag = tempMsg[2];
                    nform.label_notice_sender.Text = nname;
                    nform.label_noticetitle.Text = tempMsg[6];

                    if (tempMsg[3].Equals("e") || tempMsg[3].Equals("���"))
                    {
                        nform.pbx_notice_e.Visible = true;
                    }
                    else
                    {
                        nform.pbx_notice_n.Visible = true;
                    }
                    nform.Tag = tempMsg[4];
                    nform.Show();
                    nform.Activate();
                    if (!tempMsg[0].Equals("r"))
                    {
                        nform.flash();
                    }
                }
                else
                {
                    logWrite("�޽��� �迭 ũ�� ���� : " + tempMsg.Length.ToString());
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void ShowNoticeDirect(string[] tempMsg)  //n|�޽��� | �߽���id | mode | noticetime |����
        {
            try
            {
                if (tempMsg.Length > 5)
                {
                    string nname = getName(tempMsg[2]);
                    Notice nform = new Notice();
                    if (tempMsg[0].Equals("n"))//�ǽð� �������� ���Ž� Ȯ�ΰ�� ���� ó��
                    {
                        nform.btn_confirm.MouseClick += new MouseEventHandler(sendReadNotice);
                    }
                    nform.FormClosing += new FormClosingEventHandler(nform_FormClosing);
                    string content = tempMsg[1];
                    nform.textBox.Text = content;
                    nform.label_noticetitle.Tag = tempMsg[2];
                    nform.label_notice_sender.Text = nname;
                    nform.label_noticetitle.Text = tempMsg[5];

                    if (tempMsg[3].Equals("e") || tempMsg[3].Equals("���"))
                    {
                        nform.pbx_notice_e.Visible = true;
                    }
                    else
                    {
                        nform.pbx_notice_n.Visible = true;
                    }
                    nform.Tag = tempMsg[4];
                    nform.Show();
                    nform.Activate();
                    if (!tempMsg[0].Equals("r"))
                    {
                        nform.flash();
                    }
                }
                else
                {
                    logWrite("�޽��� �迭 ũ�� ���� : " + tempMsg.Length.ToString());
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btn_confirm_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void btn_confirm_Click(object sender, MouseEventArgs e)
        {
            try
            {
                objectDele dele = new objectDele(childFormClose);
                Button button = (Button)sender;
                Invoke(dele, ((Notice)button.Parent));
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void childFormClose(object obj)
        {
            try
            {
                ((Form)obj).Close();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void TimerStart()
        {

        }



        private void nform_FormClosing(object sender, FormClosingEventArgs e)
        {

            /*
            Form form = (Form)sender;
            int count = form.Controls.Count;
            for (int i = 0; i < count; i++)
            {
                logWrite(form.Controls[i].Name);
                if (form.Controls[i].Name.Equals("cbx_confirm"))
                {
                    CheckBox box = (CheckBox)form.Controls[i];
                    if (box.Checked == false)
                    {
                        e.Cancel = true;
                        MessageBox.Show(form, "Ȯ�ζ��� üũ�� �ּ���", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                }
            }
             */
        }

        /// <summary>
        /// ���������� "Ȯ��" ��ư�� ���� ����Ȯ�� ����� ������.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendReadNotice(object sender, MouseEventArgs e)
        {
            Form form = null;
            try
            {
                Button box = (Button)sender;

                int count = box.Parent.Controls.Count;
                string senderid = null;
                string noticeid = null;

                form = (Form)box.Parent;

                if (form.Tag.ToString().Length != 0)
                {
                    noticeid = form.Tag.ToString();
                    logWrite("noticeid : " + noticeid);
                }
                for (int i = 0; i < count; i++)
                {
                    if (box.Parent.Controls[i].Name.Equals("label_noticetitle"))
                    {
                        Label label = (Label)box.Parent.Controls[i];
                        senderid = label.Tag.ToString();
                        break;
                    }
                }
                SendMsg("21|" + this.myid + "|" + noticeid + "|" + senderid, server);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
            if (form != null)
            {
                form.Close();
            }
        }

        /// <summary>
        /// MemoListForm ����
        /// </summary>
        /// <param name="tempMsg"></param>
        private void ShowMemoList(string[] tempMsg)  //(Q|sender��content��time��seqnum|...|
        {
            try
            {
                //notreadmemoform = new NotReadMemoForm();
                //notreadmemoform.listView.Click += new EventHandler(listView_ItemSelectionChanged);
                //notreadmemoform.FormClosing += new FormClosingEventHandler(notreadmemoform_FormClosing);
                //for (int i = 1; i < tempMsg.Length; i++)
                //{
                //    string[] array = tempMsg[i].Split('��');
                //    string sender = array[0];
                //    string content = array[1];
                //    string time = array[2];
                //    string seqnum = array[3];
                //    ListViewItem item = notreadmemoform.listView.Items.Add(time, "����", null);
                //    string name = getName(sender);
                //    item.SubItems.Add(name + "(" + sender + ")");
                //    item.SubItems.Add(time);
                //    item.SubItems.Add(content);
                //    item.Tag = seqnum;
                //}
                //notreadmemoform.Show();
                //notreadmemoform.Activate();
                //isMemo = true;

                if (noreceiveboardform == null)
                {
                    noreceiveboardform = new NoReceiveBoardForm();
                    noreceiveboardform.panel_memo.Enabled = true;
                    noreceiveboardform.dgv_memo.Visible = true;
                    noreceiveboardform.label_memo.Text = "������ ���� (" + this.NRmemo.Text + ")";
                    noreceiveboardform.dgv_memo.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv_memo_CellMouseClick);
                    noreceiveboardform.FormClosing += new FormClosingEventHandler(noreceiveboardform_FormClosing);

                    for (int i = 1; i < tempMsg.Length; i++)
                    {
                        string[] array = tempMsg[i].Split('��');
                        string sender = array[0];
                        string content = array[1];
                        string time = array[2];
                        string seqnum = array[3];

                        string name = getName(sender);
                        int rownum = noreceiveboardform.dgv_memo.Rows.Add(new object[] { name + "(" + sender + ")", time, content });
                        DataGridViewRow row = noreceiveboardform.dgv_memo.Rows[rownum];
                        row.Tag = seqnum;
                    }
                }
                else
                {
                    noreceiveboardform.panel_memo.Enabled = true;
                    noreceiveboardform.dgv_memo.Visible = true;
                    noreceiveboardform.label_memo.Text = "������ ���� (" + this.NRmemo.Text + ")";
                    noreceiveboardform.dgv_memo.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv_memo_CellMouseClick);

                    for (int i = 1; i < tempMsg.Length; i++)
                    {
                        string[] array = tempMsg[i].Split('��');
                        string sender = array[0];
                        string content = array[1];
                        string time = array[2];
                        string seqnum = array[3];

                        string name = getName(sender);
                        bool isExist = false;
                        DataGridViewRowCollection collection = noreceiveboardform.dgv_memo.Rows;

                        foreach (DataGridViewRow item in collection)
                        {
                            if (item.Tag.ToString().Equals(seqnum))
                            {
                                isExist = true;
                                break;
                            }
                        }
                        if (isExist == false)
                        {
                            int rownum = noreceiveboardform.dgv_memo.Rows.Add(new object[] { name + "(" + sender + ")", time, content });
                            DataGridViewRow row = noreceiveboardform.dgv_memo.Rows[rownum];
                            row.Tag = seqnum;
                        }
                    }
                }
                noreceiveboardform.WindowState = FormWindowState.Normal;
                noreceiveboardform.Show();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void noreceiveboardform_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            noreceiveboardform.Hide();
        }

        private void dgv_memo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            intParamDele dele = new intParamDele(delNRmemo);
            Invoke(dele, e.RowIndex);
        }

        private void delNRmemo(int rowIndex)
        {
            try
            {
                DataGridViewRow row = noreceiveboardform.dgv_memo.Rows[rowIndex];
                string[] temparr = row.Cells[0].Value.ToString().Split('(');
                string name = temparr[0];
                string id = temparr[1].Split(')')[0].ToString();
                string content = row.Cells[2].Value.ToString();
                string msgtime = row.Cells[1].Value.ToString();

                MakeMemo(new string[] { "", name, id, content });

                MemoFileWrite("m|" + name + "|" + id + "|" + content + "|" + this.myid + "|" + msgtime);
                noreceiveboardform.dgv_memo.Rows.RemoveAt(rowIndex);
                SendMsg("14|" + row.Tag.ToString(), server);

                int mnum = Convert.ToInt32(NRmemo.Text);
                if (mnum != 0)
                {
                    NRmemo.Text = (mnum - 1).ToString();
                }

                if (NRmemo.Text.Equals("0"))
                {
                    noreceiveboardform.panel_memo.Enabled = false;
                }
                noreceiveboardform.label_memo.Text = "������ �޸�(" + NRmemo.Text + ")";
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }


        private void notreadmemoform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form form = (Form)sender;
            e.Cancel = true;
            form.Hide();
        }

        /// <summary>
        /// ���� ���� ���������� ������
        /// </summary>
        /// <param name="tempMsg"></param>
        private void ShowNotReadNoticeList(string[] tempMsg)  //(T|sender��content��time��mode��seqnum��title|sender��content��time��mode��seqnum|...
        {
            try
            {
                if (noreceiveboardform == null)
                {
                    noreceiveboardform = new NoReceiveBoardForm();
                    noreceiveboardform.dgv_notice.Visible = true;
                    noreceiveboardform.panel_notice.Enabled = true;
                    noreceiveboardform.label_notice.Text = "������ ���� (" + this.NRnotice.Text + ")";
                    noreceiveboardform.dgv_notice.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv_notice_CellMouseClick);
                    noreceiveboardform.FormClosing += new FormClosingEventHandler(noreceiveboardform_FormClosing);

                    for (int i = 1; i < tempMsg.Length; i++)
                    {

                        string[] array = null;
                        if (tempMsg[i].Split('��').Length > 5)
                        {
                            array = tempMsg[i].Split('��');
                        }

                        if (array != null && array.Length > 5)
                        {
                            logWrite("notreadnotice_tag = " + array[2]);
                            string sender = "����";
                            if (array[0] != null && array[0].Length != 0)
                            {
                                sender = array[0];
                            }

                            string content = "�������";

                            if (array.Length > 5)
                            {
                                content = array[1];
                            }

                            string time = "����";
                            if (array[(array.Length - 3)] != null && array[(array.Length - 3)].Length != 0)
                            {
                                time = array[2];
                            }

                            string mode = "";
                            if (array[(array.Length - 2)] != null && array[(array.Length - 2)].Length != 0)
                            {
                                mode = array[3];
                            }

                            string seqnum = null;
                            if (array[(array.Length - 1)] != null && array[(array.Length - 1)].Length != 0)
                            {
                                seqnum = array[4];
                            }

                            string name = getName(sender);

                            string title = "��������";
                            if (array[5].Length > 1)
                            {
                                title = array[5];
                            }

                            if (seqnum != null && seqnum.Length != 0)
                            {
                                if (mode.Equals("e"))
                                {
                                    int rownum = noreceiveboardform.dgv_notice.Rows.Add(new object[] { "���", title, content, name + "(" + sender + ")", time });
                                    noreceiveboardform.dgv_notice.Rows[rownum].DefaultCellStyle.ForeColor = Color.Red;
                                    noreceiveboardform.dgv_notice.Rows[rownum].Tag = seqnum;
                                }
                                else
                                {
                                    int rownum = noreceiveboardform.dgv_notice.Rows.Add(new object[] { "�Ϲ�", title, content, name + "(" + sender + ")", time });
                                    noreceiveboardform.dgv_notice.Rows[rownum].Tag = seqnum;
                                }
                            }
                        }
                    }
                }
                else
                {
                    noreceiveboardform.dgv_notice.Visible = true;
                    noreceiveboardform.panel_notice.Enabled = true;
                    noreceiveboardform.label_notice.Text = "������ ���� (" + this.NRnotice.Text + ")";

                    for (int i = 1; i < tempMsg.Length; i++)
                    {

                        string[] array = null;
                        if (tempMsg[i].Split('��').Length > 5)
                        {
                            array = tempMsg[i].Split('��');
                        }

                        if (array != null && array.Length > 5)
                        {
                            logWrite("notreadnotice_tag = " + array[2]);
                            string sender = "����";
                            if (array[0] != null && array[0].Length != 0)
                            {
                                sender = array[0];
                            }

                            string content = "�������";

                            if (array.Length > 5)
                            {
                                content = array[1];
                            }

                            string time = "����";
                            if (array[(array.Length - 3)] != null && array[(array.Length - 3)].Length != 0)
                            {
                                time = array[2];
                            }

                            string mode = "";
                            if (array[(array.Length - 2)] != null && array[(array.Length - 2)].Length != 0)
                            {
                                mode = array[3];
                            }

                            string seqnum = null;
                            if (array[(array.Length - 1)] != null && array[(array.Length - 1)].Length != 0)
                            {
                                seqnum = array[4];
                            }

                            string name = getName(sender);

                            string title = "��������";
                            if (array[5].Length > 1)
                            {
                                title = array[5];
                            }

                            if (seqnum != null && seqnum.Length != 0)
                            {
                                bool isExist = false;
                                DataGridViewRowCollection collection = noreceiveboardform.dgv_notice.Rows;

                                foreach (DataGridViewRow item in collection)
                                {
                                    if (item.Tag.ToString().Equals(seqnum))
                                    {
                                        isExist = true;
                                        break;
                                    }
                                }

                                if (isExist == false)
                                {
                                    if (mode.Equals("e"))
                                    {
                                        int rownum = noreceiveboardform.dgv_notice.Rows.Add(new object[] { "���", title, content, name + "(" + sender + ")", time });
                                        noreceiveboardform.dgv_notice.Rows[rownum].DefaultCellStyle.ForeColor = Color.Red;
                                        noreceiveboardform.dgv_notice.Rows[rownum].Tag = seqnum;
                                    }
                                    else
                                    {
                                        int rownum = noreceiveboardform.dgv_notice.Rows.Add(new object[] { "�Ϲ�", title, content, name + "(" + sender + ")", time });
                                        noreceiveboardform.dgv_notice.Rows[rownum].Tag = seqnum;
                                    }
                                }
                            }
                        }


                        //notreadnoticeform = new NotReadNoticeForm();
                        //notreadnoticeform.listView.Click += new EventHandler(Notice_ItemSelectionChanged);
                        //notreadnoticeform.FormClosing += new FormClosingEventHandler(notreadnoticeform_FormClosing);
                        //for (int i = 1; i < tempMsg.Length; i++)
                        //{

                        //    string[] array = null;
                        //    if (tempMsg[i].Split('��').Length > 5)
                        //    {
                        //        array = tempMsg[i].Split('��');
                        //    }

                        //    if (array != null && array.Length > 5)
                        //    {
                        //        logWrite("notreadnotice_tag = " + array[2]);
                        //        string sender = "����";
                        //        if (array[0] != null && array[0].Length != 0)
                        //        {
                        //            sender = array[0];
                        //        }

                        //        string content = "�������";

                        //        if (array.Length > 5)
                        //        {
                        //            content = array[1];
                        //        }

                        //        string time = "����";
                        //        if (array[(array.Length - 3)] != null && array[(array.Length - 3)].Length != 0)
                        //        {
                        //            time = array[2];
                        //        }

                        //        string mode = "";
                        //        if (array[(array.Length - 2)] != null && array[(array.Length - 2)].Length != 0)
                        //        {
                        //            mode = array[3];
                        //        }

                        //        string seqnum = null;
                        //        if (array[(array.Length - 1)] != null && array[(array.Length - 1)].Length != 0)
                        //        {
                        //            seqnum = array[4];
                        //        }

                        //        ListViewItem item = null;
                        //        if (mode.Equals("n"))
                        //        {
                        //            item = notreadnoticeform.listView.Items.Add(time, "�Ϲ�", null);
                        //        }
                        //        else if (mode.Equals("e"))
                        //        {
                        //            item = notreadnoticeform.listView.Items.Add(time, "���", null);
                        //            item.ForeColor = Color.Red;
                        //        }

                        //        string name = getName(sender);
                        //        if (content.Contains("\r\n"))
                        //        {
                        //            content.Replace("\r\n", "  ");
                        //        }

                        //        string title = "��������";
                        //        if (array[5].Length > 1)
                        //        {
                        //            title = array[5];
                        //        }

                        //        if (seqnum != null && seqnum.Length != 0)
                        //        {
                        //            item.SubItems.Add(title);
                        //            item.SubItems.Add(content);
                        //            item.SubItems.Add(name + "(" + sender + ")");
                        //            item.SubItems.Add(time);

                        //            item.Tag = seqnum;
                        //            logWrite("seqnum : " + item.Tag.ToString());
                        //        }
                        //    }
                        //}
                        //notreadnoticeform.Show();
                        //notreadnoticeform.Activate();
                        //isNotice = true;
                    }
                }
                noreceiveboardform.WindowState = FormWindowState.Normal;
                noreceiveboardform.Show();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void dgv_notice_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            intParamDele dele = new intParamDele(delNRnotice);
            Invoke(dele, e.RowIndex);
        }

        private void delNRnotice(int rowIndex)
        {
            try
            {
                DataGridViewRow row = noreceiveboardform.dgv_notice.Rows[rowIndex];

                string temp = row.Cells[3].Value.ToString();
                string[] ar1 = temp.Split('(');
                string[] ar2 = ar1[1].Split(')');
                string name = ar1[0];
                string id = ar2[0];
                string msg = row.Cells[2].Value.ToString();
                string mode = row.Cells[0].Value.ToString();
                string ntime = row.Cells[4].Value.ToString();
                string seqnum = row.Tag.ToString();
                string title = row.Cells[1].Value.ToString();
                string[] array = new string[] { "r", msg, id, mode, ntime, seqnum, title };  //n|�޽���|�߽���id|mode|seqnum|title
                ShowNotice(array);

                SendMsg("14|" + seqnum, server);
                noreceiveboardform.dgv_notice.Rows.RemoveAt(rowIndex);


                int mnum = Convert.ToInt32(NRnotice.Text);
                if (mnum != 0)
                {
                    NRnotice.Text = (mnum - 1).ToString();
                }

                if (NRnotice.Text.Equals("0"))
                {
                    noreceiveboardform.panel_notice.Enabled = false;
                }
                noreceiveboardform.label_notice.Text = "������ ����(" + NRnotice.Text + ")";
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void notreadnoticeform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form form = (Form)sender;
            e.Cancel = true;
            form.Hide();
        }

        /// <summary>
        /// ������ �������� ����Ʈ �� ����
        /// </summary>
        /// <param name="tempMsg"></param>
        private void ShowFileList(string[] tempMsg) //R|sender��filenum��filename��time��size��seqnum|sender��filenum��filename��time��size��seqnum|...
        {
            //try
            //{
            //    notreceivefileform = new NotReceiveFileForm();
            //    notreceivefileform.listView1.Click += new EventHandler(File_ItemSelectionChanged);
            //    notreceivefileform.FormClosing += new FormClosingEventHandler(notreceivefileform_FormClosing);
            //    for (int i = 1; i < tempMsg.Length; i++)
            //    {
            //        string[] array = tempMsg[i].Split('��');
            //        string sender = array[0];
            //        string filename = array[2];
            //        string time = array[3];
            //        string filesize = array[4];
            //        ListViewItem item = notreceivefileform.listView1.Items.Add(time, "����", null);
            //        string name = getName(sender);
            //        item.SubItems.Add(filename);
            //        item.SubItems.Add(name + "(" + sender + ")");
            //        item.SubItems.Add(time);
            //        item.SubItems.Add(filesize);
            //        item.Tag = array[1];
            //        item.ToolTipText = array[5];
            //    }
            //    notreceivefileform.Show();
            //    notreceivefileform.Activate();
            //    isFile = true;
            //}
            //catch (Exception exception)
            //{
            //    logWrite(exception.ToString());
            //}

            try
            {
                if (noreceiveboardform != null)
                {
                    noreceiveboardform.dgv_file.Visible = true;
                    noreceiveboardform.panel_file.Enabled = true;
                    for (int i = 1; i < tempMsg.Length; i++)
                    {
                        string[] array = tempMsg[i].Split('��');
                        string sender = array[0];
                        string filename = array[2];
                        string time = array[3];
                        string filesize = array[4];

                        string name = getName(sender);
                        int rownum = noreceiveboardform.dgv_file.Rows.Add(new object[] { filename, filesize, name + "(" + sender + ")", time });
                        DataGridViewRow row = noreceiveboardform.dgv_file.Rows[rownum];
                        row.Tag = array[1];
                        row.ErrorText = array[5];
                    }

                    noreceiveboardform.label_file.Text = "������ ����(" + NRfile.Text + ")";
                }
                else
                {
                    noreceiveboardform = new NoReceiveBoardForm();
                    noreceiveboardform.panel_file.Enabled = true;
                    noreceiveboardform.dgv_file.Visible = true;
                    noreceiveboardform.dgv_file.CellClick += new DataGridViewCellEventHandler(dgv_file_CellClick);
                    noreceiveboardform.FormClosing += new FormClosingEventHandler(noreceiveboardform_FormClosing);

                    for (int i = 1; i < tempMsg.Length; i++)
                    {
                        string[] array = tempMsg[i].Split('��');
                        string sender = array[0];
                        string filename = array[2];
                        string time = array[3];
                        string filesize = array[4];

                        string name = getName(sender);
                        bool isExist = false;
                        DataGridViewRowCollection collection = noreceiveboardform.dgv_file.Rows;

                        foreach (DataGridViewRow item in collection)
                        {
                            if (item.ErrorText.Equals(array[5]))
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (isExist == false)
                        {
                            int rownum = noreceiveboardform.dgv_file.Rows.Add(new object[] { filename, filesize, name + "(" + sender + ")", time });
                            DataGridViewRow row = noreceiveboardform.dgv_file.Rows[rownum];
                            row.Tag = array[1];
                            row.ErrorText = array[5];
                        }
                    }

                    noreceiveboardform.label_file.Text = "������ ����(" + NRfile.Text + ")";
                }
                noreceiveboardform.WindowState = FormWindowState.Normal;
                noreceiveboardform.Show();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void dgv_file_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            intParamDele dele = new intParamDele(delNRfile);
            Invoke(dele, e.RowIndex);
        }

        private void delNRfile(int rowIndex)
        {
            try
            {
                DataGridViewRow row = noreceiveboardform.dgv_file.Rows[rowIndex];
                string filenum = row.Tag.ToString();
                string filename = row.Cells[0].Value.ToString();
                int filesize = Convert.ToInt32(row.Cells[1].Value.ToString());

                DialogResult result = MessageBox.Show("������ ���� ������ ���� ������ �����ðڽ��ϱ�?\r\n\r\n ���ϸ� : " + filename, "��������", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveFileDialogDelegate save = new SaveFileDialogDelegate(ShowSaveFileDialog);
                    string savefilename = (string)Invoke(save, filename);

                    Hashtable info = new Hashtable();
                    info[savefilename] = filesize;
                    info["filenum"] = filenum;
                    info["rowindex"] = rowIndex;
                    info["NRseq"] = row.ErrorText;
                    logWrite("���ϻ����� : " + filesize.ToString());
                    Thread file = new Thread(new ParameterizedThreadStart(FileReceiver));
                    file.Start((object)info);

                    SendMsg("12|" + this.myid + "|" + filenum, server);
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void refreshNRfile(int rowIndex)
        {
            try
            {

                if (noreceiveboardform != null)
                {
                    noreceiveboardform.dgv_file.Rows.RemoveAt(rowIndex);
                }

                int mnum = Convert.ToInt32(NRfile.Text);

                if (mnum != 0)
                {
                    NRfile.Text = (mnum - 1).ToString();
                }

                if (NRfile.Text.Equals("0"))
                {
                    noreceiveboardform.panel_file.Enabled = false;
                }
                noreceiveboardform.label_file.Text = "������ ����(" + NRfile.Text + ")";
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void notreceivefileform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form form = (Form)sender;
            e.Cancel = true;
            form.Hide();
        }

        private void showNoreadTransfer(string[] tempMsg)
        {
            try
            {
                if (noreceiveboardform == null)
                {
                    noreceiveboardform = new NoReceiveBoardForm();
                    noreceiveboardform.panel_trans.Enabled = true;
                    noreceiveboardform.dgv_transfer.Visible = true;
                    noreceiveboardform.label_trans.Text = "������ �̰� (" + this.NRtrans.Text + ")";
                    noreceiveboardform.dgv_transfer.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv_transfer_CellMouseClick);
                    noreceiveboardform.FormClosing += new FormClosingEventHandler(noreceiveboardform_FormClosing);

                    for (int i = 1; i < tempMsg.Length; i++)
                    {
                        string[] array = tempMsg[i].Split('��');
                        string sender = array[0];
                        string content = array[1]; // array[1] => 22&ani&senderID&receiverID&����&�ð�&CustomerName
                        string time = array[2];
                        string seqnum = array[3];
                        string name = getName(sender);

                        string[] temp = content.Split('&');
                        int rownum = 0;
                        if (temp.Length > 2)
                        {
                            rownum = noreceiveboardform.dgv_transfer.Rows.Add(new object[] { time, temp[1], name + "(" + sender + ")" });
                            DataGridViewRow row = noreceiveboardform.dgv_transfer.Rows[rownum];
                            row.Tag = seqnum + "|" + content;
                        }
                        else
                        {
                            rownum = noreceiveboardform.dgv_transfer.Rows.Add(new object[] { time, content, name + "(" + sender + ")" });
                            DataGridViewRow row = noreceiveboardform.dgv_transfer.Rows[rownum];
                            row.Tag = seqnum;
                        }

                    }
                }
                else
                {
                    noreceiveboardform.panel_trans.Enabled = true;
                    noreceiveboardform.dgv_transfer.Visible = true;
                    noreceiveboardform.label_trans.Text = "������ �̰� (" + this.NRtrans.Text + ")";
                    noreceiveboardform.dgv_transfer.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv_transfer_CellMouseClick);

                    for (int i = 1; i < tempMsg.Length; i++)
                    {
                        string[] array = tempMsg[i].Split('��');
                        string sender = array[0];
                        string content = array[1]; // array[1] => 22&ani&senderID&receiverID&����&�ð�&CustomerName
                        string time = array[2];
                        string seqnum = array[3];

                        string name = getName(sender);
                        bool isExist = false;
                        string[] temp = content.Split('&');

                        DataGridViewRowCollection collection = noreceiveboardform.dgv_transfer.Rows;


                        foreach (DataGridViewRow item in collection)
                        {
                            if (item != null)
                            {
                                if (item.Tag != null)
                                {
                                    if (item.Tag.ToString().Equals(seqnum))
                                    {
                                        isExist = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (isExist == false)
                        {
                            int rownum = 0;
                            if (temp.Length > 2)
                            {
                                rownum = noreceiveboardform.dgv_transfer.Rows.Add(new object[] { time, temp[1], name + "(" + sender + ")" });
                                DataGridViewRow row = noreceiveboardform.dgv_transfer.Rows[rownum];
                                row.Tag = seqnum + "|" + content;
                            }
                            else
                            {
                                rownum = noreceiveboardform.dgv_transfer.Rows.Add(new object[] { time, content, name + "(" + sender + ")" });
                                DataGridViewRow row = noreceiveboardform.dgv_transfer.Rows[rownum];
                                row.Tag = seqnum;
                            }
                        }
                    }
                }
                noreceiveboardform.WindowState = FormWindowState.Normal;
                noreceiveboardform.Show();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void dgv_transfer_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            intParamDele dele = new intParamDele(delNRTrans);
            Invoke(dele, e.RowIndex);
        }

        private void delNRTrans(int rowIndex)
        {
            try
            {
                DataGridViewRow row = noreceiveboardform.dgv_transfer.Rows[rowIndex];
                string[] contentArray = null;
                string[] temp = row.Tag.ToString().Split('|'); // seqnum|22&ani&senderID&receiverID&����&�ð�&CustomerName
                string ani = row.Cells[1].Value.ToString();
                if (temp.Length > 1)
                {
                    if (temp[1].Length > 0)
                    {
                        contentArray = temp[1].Split('&');

                        ShowTransInfoDele dele = new ShowTransInfoDele(showTransferInfo);
                        Invoke(dele, new object[] { contentArray[1], contentArray[2], contentArray[4], contentArray[5] });
                    }
                    else
                    {
                        doublestringDele answerdele = new doublestringDele(Answer);
                        Invoke(answerdele, new object[] { ani, "3" });
                    }
                }
                else
                {
                    doublestringDele answerdele = new doublestringDele(Answer);
                    Invoke(answerdele, new object[] { ani, "3" });
                }


                noreceiveboardform.dgv_transfer.Rows.RemoveAt(rowIndex);

                //������ ������ �̰� row ���� DB ���� ��û
                SendMsg("14|" + temp[0], server);

                int mnum = Convert.ToInt32(NRtrans.Text);
                if (mnum != 0)
                {
                    NRtrans.Text = (mnum - 1).ToString();
                }

                if (NRtrans.Text.Equals("0"))
                {
                    noreceiveboardform.panel_trans.Enabled = false;
                }
                noreceiveboardform.label_trans.Text = "������ �̰�(" + this.NRtrans.Text + ")";
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        /// <summary>
        /// ������ ���� ����Ʈ���� �ش� �������� Ŭ���� �޸�â ���� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ItemSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ListView view = (ListView)sender;
                ListViewItem mitem = view.SelectedItems[0];
                string temp = mitem.SubItems[1].Text;
                string[] ar1 = temp.Split('(');
                string[] ar2 = ar1[1].Split(')');
                string name = ar1[0];
                string id = ar2[0];
                string msg = mitem.SubItems[3].Text;
                string msgtime = mitem.SubItems[2].Text;
                string[] array = new string[] { "m", name, id, msg };
                MakeMemo(array);

                MemoFileWrite("m|" + name + "|" + id + "|" + msg + "|" + this.myid + "|" + msgtime);

                SendMsg("14|" + mitem.Tag.ToString(), server);
                view.SelectedItems[0].Remove();
                int mnum = Convert.ToInt32(NRmemo.Text);
                if (mnum != 0)
                {
                    NRmemo.Text = (mnum - 1).ToString();
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ������ �������� ����Ʈ ������ ������ Ŭ���� �ش� �������� ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Notice_ItemSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ListView view = (ListView)sender;
                ListViewItem mitem = view.SelectedItems[0];
                string temp = mitem.SubItems[3].Text;
                string[] ar1 = temp.Split('(');
                string[] ar2 = ar1[1].Split(')');
                string name = ar1[0];
                string id = ar2[0];
                string msg = mitem.SubItems[2].Text;
                string mode = mitem.SubItems[0].Text;
                string ntime = mitem.SubItems[4].Text;
                string seqnum = mitem.Tag.ToString();
                string title = mitem.SubItems[1].Text;
                string[] array = new string[] { "r", msg, id, mode, ntime, seqnum, title };  //n|�޽���|�߽���id|mode|seqnum|title
                ShowNotice(array);

                SendMsg("14|" + seqnum, server);
                view.SelectedItems[0].Remove();
                int mnum = Convert.ToInt32(NRnotice.Text);
                if (mnum != 0)
                {
                    NRnotice.Text = (mnum - 1).ToString();
                }

            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ������ �������� ����Ʈ ������ ������ Ŭ���� �ش� ���� ���� �غ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_ItemSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ListView view = (ListView)sender;
                ListViewItem mitem = view.SelectedItems[0];
                string filenum = mitem.Tag.ToString();
                string filename = mitem.SubItems[1].Text;
                int filesize = Convert.ToInt32(mitem.SubItems[4].Text);

                DialogResult result = MessageBox.Show("������ ���� ������ ���� ������ �����ðڽ��ϱ�?\r\n\r\n ���ϸ� : " + filename, "��������", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveFileDialogDelegate save = new SaveFileDialogDelegate(ShowSaveFileDialog);
                    string savefilename = (string)Invoke(save, filename);

                    Hashtable info = new Hashtable();
                    info[savefilename] = filesize;
                    logWrite(filesize.ToString());
                    Thread file = new Thread(new ParameterizedThreadStart(FileReceiver));
                    file.IsBackground = true;
                    file.Start((object)info);
                    SendMsg("12|" + this.myid + "|" + filenum, server);
                    SendMsg("14|" + mitem.ToolTipText, server);
                    view.SelectedItems[0].Remove();
                    int mnum = Convert.ToInt32(NRfile.Text);
                    if (mnum != 0)
                    {
                        NRfile.Text = (mnum - 1).ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// �α��� �� ������ �Ǽ� ���� ���̱�
        /// </summary>
        /// <param name="arg"></param>
        private void ShowAbsentInfoNumber(string[] arg)
        {
            NRmemo.Text = arg[1];
            NRfile.Text = arg[2];
            NRnotice.Text = arg[3];
            NRtrans.Text = arg[4];

            if (Convert.ToInt32(arg[3].Trim()) > 0)
            {
                SendMsg("11|" + this.myid, server);
            }
        }

        /// <summary>
        /// �ߺ��α��� �õ� ��� 
        /// </summary>
        public void ReLogin()
        {
            panel_progress.Visible = false;
            defaultCtrl(true);
            DialogResult result = MessageBox.Show(this, "���̵� " + this.id.Text + "�� �̹� �α��� �����Դϴ�.\r\n ���� ������ �α׾ƿ� �Ͻðڽ��ϱ�?", "�α��� �ߺ�", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                SendMsg("2|" + this.id.Text, server);
                closing();
                MessageBox.Show(this, "���������� �α׾ƿ� �߽��ϴ�. �ٽ� �α����� �ּ���", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else closing();
        }

        public void logInFail()
        {
            id.Focus();
        }

        private void ShowMessageBox(string msg)
        {
            panel_progress.Visible = false;
            defaultCtrl(true);
            MessageBox.Show(this, msg, "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// �α��� ������ �ڱ��̸� ���
        /// </summary>
        /// <param name="Name"></param>
        public void FlushInfo(string Name)
        {
            name.Text = Name + "(" + this.myid + ")"; ;
        }

        /// <summary>
        /// �α��� ������ �Ҽ� �� ���
        /// </summary>
        /// <param name="Name"></param>
        public void Flushteam(string Name)
        {
            string clientName = Name;
            team.Text = clientName;
        }


        /// <summary>
        /// �α��� �� �α׾ƿ� �϶� �� �г� �� ��ư ��Ʈ��
        /// </summary>
        /// <param name="value"></param>
        public void LogInPanelVisible(bool value)
        {
            label_pass.Visible = value;
            label_id.Visible = value;
            id.Visible = value;
            tbx_pass.Visible = value;
            default_panal.Visible = value;
            btn_login.Visible = value;
            btn_login.Visible = value;
            pic_title.Visible = value;

            panel_logon.Visible = !value;

        }


        public void TreeViewVisible(bool value)
        {
            try
            {
                memTree.Visible = value;
                memTree.Nodes[0].Nodes[0].Text = memTree.Nodes[0].Nodes[0].Text;
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        public void StatBarText(string value)
        {
            //statusBar.Text = value;
        }

        public void ButtonCtrl(bool value)
        {
            InfoBar.Visible = value;
            memTree.Visible = value;
            MnDialogue.Enabled = value;
            MnSendFile.Enabled = value;
            MnLogout.Enabled = value;
            MnMemo.Enabled = value;
            //MnNoticeShow.Enabled = value;
            MnNotice.Enabled = value;
            Mn_server.Enabled = !value;
            Mn_extension.Enabled = !value;

        }

        /// <summary>
        /// ���� ����� FileDailog â ����
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string ShowSaveFileDialog(string filename)
        {
            string savefilename = null;
            SaveFileDialog savefiledialog = new SaveFileDialog();
            savefiledialog.FileName = filename;
            DialogResult savefileresult = savefiledialog.ShowDialog();
            if (savefileresult == DialogResult.OK)
            {
                savefilename = savefiledialog.FileName;
            }
            return savefilename;
        }

        private void ChangeKey(string key, SendFileForm form)
        {
            form.formkey.Text = key;
        }


        /// <summary>
        /// ���ڰ� ä���� ��ȭâ�� ���� �������� �˸�
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        public void DelChatter(string id, ChatForm form)
        {
            TreeNode[] node = form.ChattersTree.Nodes.Find(id, false);
            form.chatBox.AppendText("\r\n###" + node[0].Text + "���� â�� �ݰ� ��ȭ�� �����Ͽ����ϴ�.." + "\r\n\r\n");
            node[0].Remove();
        }

        /// <summary>
        /// ä���� ������ �α׾ƿ��� ��� �˸�
        /// </summary>
        /// <param name="id"></param>
        public void LogoutChatter(string id)
        {
            try
            {
                foreach (DictionaryEntry de in ChatFormList)
                {
                    if (de.Value != null)
                    {
                        ChatForm form = (ChatForm)de.Value;
                        TreeNode[] node = form.ChattersTree.Nodes.Find(id, false);
                        if (node.Length != 0)
                        {
                            logWrite("LogoutChatter() �˻����� : " + node[0].Text);
                            form.chatBox.AppendText("\r\n\r\n###" + node[0].Text + "���� �α׾ƿ� �ϼ̽��ϴ�.\r\n\r\n");
                            form.chatBox.ScrollToCaret();
                            node[0].Remove();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// �� Ʈ���� �⺻ ��� ����
        /// </summary>
        /// <param name="ar"></param>
        public void DefaultTree(string[] ar)
        {
            try
            {
                if (ar.Length != 0)
                {
                    foreach (string node in ar)
                    {
                        TreeNode Dnode = new TreeNode(node);
                        memTree.Nodes.Add(Dnode);
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// �������� ������ ���� ���� ��� TreeView�� Ʈ�� ����
        /// </summary>
        /// <param name="team"></param>
        /// <param name="list"></param>
        private void MakeMemTree(string team, ArrayList list) //list[] {id!name}
        {
            try
            {
                int nodeNum = memTree.Nodes.Count;
                TreeNode node = null;
                if (team.Length != 0)
                {
                    if (!memTree.Nodes.ContainsKey(team))
                    {
                        node = memTree.Nodes.Add(team, "");//����� �߰�
                        node.Text = team;
                        node.NodeFont = new Font("����", 9.75f, FontStyle.Bold);
                        node.ForeColor = Color.IndianRed;
                        node.EnsureVisible();
                        //memTree.e

                        if (list != null && list.Count != 0)
                        {
                            foreach (object obj in list)  //list[] {id!name}
                            {
                                string m = (string)obj;
                                if (m.Length != 0)
                                {
                                    string[] arg = m.Split('!');
                                    if (!arg[1].Equals(myname))
                                    {
                                        TreeNode tempNode = memTree.Nodes[nodeNum].Nodes.Add(arg[0], arg[1]);   //����� ��� �߰�(��� key=id, value=name)
                                        tempNode.ToolTipText = arg[0]; //MouseOver�� ��� ��Ÿ�� 
                                        tempNode.ForeColor = Color.Gray;
                                        tempNode.Tag = arg[0];
                                        tempNode.ImageIndex = 0;
                                        tempNode.SelectedImageIndex = 0;
                                    }
                                }
                            }
                        }
                    }
                }
                memTree.ExpandAll();
                //if (!memTree.Nodes[0].IsExpanded) memTree.Nodes[0].Expand();
                //if (team.Equals(this.team.Text)) node.Expand(); 

                //this.statusBar.Text = "";
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkInfoForLogin();
            }
        }


        private void passwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkInfoForLogin();
            }
        }

        /// <summary>
        /// �α��� ��û�� �������� �α��� ���� ����
        /// </summary>
        private void SendInfo()
        {
            string info = "8|" + this.myid + "|" + this.mypass + "|" + this.extension + "|" + local.ToString();
            SendMsg(info, server);

            setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "id", this.myid);
            setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "extension", this.extension);
            if (cbx_pass_save.Checked == true)
            {
                setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "pass", this.mypass);
                setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "save_pass", "1");
            }
        }

        private void setConfigXml(string filename, string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode pnode = doc.SelectSingleNode("//appSettings");
            if (pnode.HasChildNodes)
            {
                XmlNodeList nodelist = pnode.ChildNodes;
                foreach (XmlNode node in nodelist)
                {
                    if (node.Attributes["key"].Value.Equals(key))
                    {
                        node.Attributes["value"].Value = value;
                        break;
                    }
                }
                doc.Save(filename);
            }
        }

        private void setCRM_DB_HOST(string filename, string val)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode pnode = doc.SelectSingleNode("//db");

            if (pnode.HasChildNodes)
            {
                XmlNodeList nodelist = pnode.ChildNodes;
                foreach (XmlNode node in nodelist)
                {
                    if (node.Name.Equals("dbserverip"))
                    {
                        node.InnerText = val;
                    }
                }
                doc.Save(filename);
            }
        }

        private void MnDialog_Click(object sender, EventArgs e)
        {
            string chatter = null;
            string ids = null;
            MakeChatForm(chatter, ids);
        }


        private void memTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeView view = (TreeView)sender;
                view.SelectedNode = e.Node;
                if (e.Button == MouseButtons.Right)
                {
                    if (e.Node.GetNodeCount(false) != 0)
                    {
                        mouseMenuG.Show(view, e.Location, ToolStripDropDownDirection.BelowRight);
                    }
                    else mouseMenuN.Show(view, e.Location, ToolStripDropDownDirection.BelowRight);
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }


        private void memTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (memTree.SelectedNode.GetNodeCount(true) == 0) //���� ��� ����
                {
                    logWrite(e.Node.Tag.ToString());
                    if (InList.ContainsKey(e.Node.Tag.ToString()) && InList[e.Node.Tag.ToString()] != null)
                    {
                        MakeChatForm(e.Node.Text, e.Node.Tag.ToString());
                    }
                    else  //��ȭ������ ������ �������
                    {
                        DialogResult result = MessageBox.Show(this, "��ȭ�� ������ ��ȭ�� �Ұ����� �����Դϴ�.\r\n ��� ������ �����ðڽ��ϱ�?", "�˸�", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            Hashtable MemoReceiver = new Hashtable();
                            string chatterid = (String)e.Node.Tag;
                            MemoReceiver[e.Node.Text] = chatterid;
                            MakeSendMemo(MemoReceiver);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ��ȭ�ϱ� �޴��� Ŭ���� ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chat_Click(object sender, EventArgs e)
        {
            try
            {
                if (memTree.SelectedNode.Level == 0) //������ü�� ��Ÿ���� ��带 ������ �߿� 
                {
                    MessageBox.Show(this, "����� ��ü�ʹ� ��ȭ�� �Ұ��� �մϴ�.\r\n ��� ��ü ������ �Ͻðڽ��ϱ�?", "�˸�", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else if (memTree.SelectedNode.GetNodeCount(false) != 0)//������ ��尡 ���� ��带 ������ ���� ���
                {
                    logWrite("�׷��ȭ ��û!");
                    string chattersName = null;                    //��ȭ�� �̸� ���ڿ�
                    string chattersid = this.myid + "/";           //��ȭ������ ���̵� ���ڿ�
                    ArrayList unchatters = new ArrayList();                      //��ȭ�Ұ����� ���̵� ���ڿ�
                    int chatable = 0;
                    for (int i = 0; i < memTree.SelectedNode.GetNodeCount(false); i++)
                    {
                        string chatterid = (String)memTree.SelectedNode.Nodes[i].Tag;

                        if (InList.ContainsKey(chatterid) && InList[chatterid] != null)
                        {
                            chatable++;

                            chattersName += memTree.SelectedNode.Nodes[i].Text + "/";

                            chattersid += chatterid + "/";
                        }
                        else
                        {
                            unchatters.Add(memTree.SelectedNode.Nodes[i].Text);
                        }
                    }
                    if (chatable == 0) //��ȭ������ ������ �������
                    {
                        DialogResult result = MessageBox.Show(this, "��û�� ���� ��ΰ� ��ȭ�� �Ұ����� �����Դϴ�.\r\n ��� ������ �����ðڽ��ϱ�?", "�˸�", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            Hashtable MemoReceiver = new Hashtable();
                            for (int i = 0; i < memTree.SelectedNode.GetNodeCount(false); i++)
                            {
                                string chatterid = (String)memTree.SelectedNode.Nodes[i].Tag;
                                MemoReceiver[chatterid] = memTree.SelectedNode.Nodes[i].Text;
                            }
                            MakeSendMemo(MemoReceiver);//����������
                        }
                    }
                    else MakeChatForm(chattersName, unchatters, chattersid);
                }
                else //������ ��尡 ������ ����� ���
                {
                    logWrite("�ϴ��ϴ�ȭ ��û");
                    if (InList.ContainsKey(memTree.SelectedNode.Tag) && InList[memTree.SelectedNode.Tag] != null)
                    {
                        MakeChatForm(memTree.SelectedNode.Text, memTree.SelectedNode.Tag.ToString());
                    }
                    else  //��ȭ������ ������ �������
                    {
                        DialogResult result = MessageBox.Show(this, "��ȭ�� ������ ��ȭ�� �Ұ����� �����Դϴ�.\r\n ��� ������ �����ðڽ��ϱ�?", "�˸�", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            Hashtable MemoReceiver = new Hashtable();
                            string chatterid = (String)memTree.SelectedNode.Tag;
                            MemoReceiver[chatterid] = memTree.SelectedNode.Text;
                            MakeSendMemo(MemoReceiver);
                        }

                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ��ȭâ�� �޽��� �Է�â���� ���ڸ� �Է��� ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void getMsg(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox msgBox = (TextBox)sender;
                if (e.Modifiers == Keys.Alt && e.KeyData == Keys.Enter)
                {
                    msgBox.AppendText("\r\n");
                }

                else if (e.KeyCode == Keys.Enter)
                {
                    TextBox exam = null;
                    string str = null;
                    msgBox.Text = msgBox.Text.Trim();
                    if (msgBox.Text.Length != 0)
                    {
                        int num = msgBox.Parent.Controls.Count;

                        bool SendAvailable = true;

                        for (int i = 0; i < num; i++)
                        {
                            if ("txtbox_exam".Equals(msgBox.Parent.Controls[i].Name))
                            {
                                exam = (TextBox)msgBox.Parent.Controls[i];//��ȭ�� ǥ�ü��� ���� ��������
                                break;
                            }
                        }

                        for (int i = 0; i < num; i++)
                        {
                            if ("ChattersTree".Equals(msgBox.Parent.Controls[i].Name))
                            {
                                if (((TreeView)msgBox.Parent.Controls[i]).Nodes.Count == 0) //ä�������� ����Ʈ�信 �����ڰ� ���ٸ�
                                {
                                    MessageBox.Show("��ȭ ������ �����ϴ�.\r\n��ȭ�� ������ �߰��� �ּ���.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    SendAvailable = false;
                                    logWrite("ChattersTree �� ��ȭ���� ����");
                                }
                                break;
                            }
                        }

                        if (SendAvailable == true) //ä�������ڰ� ���� ��츸
                        {
                            string key = null;
                            string ids = this.myid + "/";

                            //ä��â ���� ������Ű ������
                            for (int i = 0; i < num; i++)
                            {
                                if ("Formkey".Equals(msgBox.Parent.Controls[i].Name))
                                {
                                    key = msgBox.Parent.Controls[i].Text;
                                    logWrite("Formkey=<" + key + ">");
                                    break;
                                }
                            }

                            //ä�ø޽��� ����
                            for (int i = 0; i < num; i++)
                            {
                                if ("ChattersTree".Equals(msgBox.Parent.Controls[i].Name))
                                {
                                    ArrayList array = new ArrayList();
                                    TreeView view = (TreeView)msgBox.Parent.Controls[i];
                                    if (view.Nodes.Count != 0)
                                    {
                                        for (int n = 0; n < view.Nodes.Count; n++)
                                        {
                                            ids += view.Nodes[n].Tag + "/";
                                            array.Add(view.Nodes[n].Tag);
                                        }
                                    }
                                    str = "16|" + key + "|" + ids + "|" + myname + "|" + msgBox.Text.Trim();    //d|Formkey|id/id/..|�߽���name|�޽��� 
                                    logWrite("��ȭ�޽��� ���� : " + str);


                                    SendMsg(str, server);
                                    //foreach (string receiverid in array)
                                    //{
                                    //    if (InList.Contains(receiverid) && InList[receiverid] != null)
                                    //    {

                                    //        IPEndPoint iep = (IPEndPoint)InList[receiverid];

                                    //        logWrite("��ȭ���� id : " + receiverid + " port : " + iep.Port.ToString());

                                    //        SendMsg(str, iep);
                                    //    }
                                    //}
                                    break;
                                }
                            }
                            for (int i = 0; i < num; i++)
                            {
                                if ("chatBox".Equals(msgBox.Parent.Controls[i].Name))
                                {
                                    RichTextBox box = (RichTextBox)msgBox.Parent.Controls[i];
                                    TextBox tempbox = new TextBox();
                                    string mymsg = myname + " ���� �� :";
                                    tempbox.Text = mymsg;
                                    int lines = box.Lines.Length;
                                    box.AppendText(mymsg + "\r\n");
                                    int findnum = box.Find(mymsg, RichTextBoxFinds.Reverse); //��ȭâ���� �ڱ⸻ ��ũã��
                                    box.SelectionBackColor = Color.Black;
                                    box.Select(findnum, mymsg.Length);
                                    box.SelectionColor = Color.White;
                                    box.SelectionFont = new Font("����", 9.0f, FontStyle.Bold);

                                    mymsg = msgBox.Text.Trim();
                                    tempbox.Text = mymsg;
                                    lines = box.Lines.Length;
                                    box.AppendText("��  " + mymsg + "\r\n\r\n");
                                    findnum = box.Find(mymsg, RichTextBoxFinds.Reverse);
                                    box.SelectionBackColor = Color.White;
                                    box.Select(findnum, mymsg.Length);
                                    box.SelectionColor = exam.ForeColor;
                                    box.SelectionFont = exam.Font;
                                    box.ScrollToCaret();

                                    break;
                                }
                            }
                            msgBox.Clear();
                            msgBox.Focus();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// �������� �α��� Ŭ���̾�Ʈ ����Ʈ�� ���۹޾� �ش� Ŭ���̾�Ʈ ���¸� �α������� ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tname"></param>
        private void ChangeMemStat(string id, string status)
        {
            try
            {
                PresenceUpdate(id, status);
            }
            catch (Exception e)
            {
                logWrite(id + " ���°� ���� ���� : " + e.ToString());
            }

        }

        /// <summary>
        /// memTree�� �α��� Ŭ���̾�Ʈ ��� �߰�
        /// </summary>
        /// <param name="tempMsg">i|id|�Ҽ�|Ŭ���̾�Ʈ IP�ּ�|�̸�</param>
        private void AddMemTreeNode(string[] tempMsg)//i|id|�Ҽ�|address|�̸�
        {
            try
            {
                TreeNode[] nodeArray = memTree.Nodes.Find(tempMsg[2], true);
                if (!nodeArray[0].Nodes.ContainsKey(tempMsg[1]))
                {
                    TreeNode tempNode = nodeArray[0].Nodes.Add(tempMsg[1], tempMsg[4]);
                    tempNode.ToolTipText = tempMsg[1]; //MouseOver�� ��� ��Ÿ�� 
                    tempNode.Tag = tempMsg[1];
                    tempNode.ImageIndex = 1;
                    tempNode.SelectedImageIndex = 1;
                    nodeArray[0].Expand();
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// �α׾ƿ� ����� memTree ���� ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tname"></param>
        private void ChangeLogout(string id, string tname)
        {
            try
            {
                TreeNode[] memNode = memTree.Nodes.Find(id, true);
                if (memNode != null && memNode.Length > 0)
                {
                    memNode[0].ImageIndex = 0;
                    memNode[0].SelectedImageIndex = 0;
                    memNode[0].Text = getName(id);
                    memNode[0].ForeColor = Color.Gray;
                    logWrite(id + "�� ���°� ����");
                }
                else
                {
                    logWrite("ChangeLogout Error : " + id + " ��带 ã�� �� ����");
                }
            }
            catch (Exception e)
            {
                logWrite(id + " ���°� ���� ���� : " + e.ToString());
            }
        }

        /// <summary>
        /// ���ο� ��ȭ�޽��� ���Ž� ��ȭâ ����
        /// </summary>
        /// <param name="ar">d|formkey|id/id/...|name|�޽�������</param>
        private void NewChatForm(string[] ar)    //ar = d|formkey|id/id/...|name|�޽�������
        {
            try
            {
                ChatForm chatForm = new ChatForm();
                chatForm.txtbox_exam.Font = txtfont;
                chatForm.txtbox_exam.ForeColor = txtcolor;
                chatForm.Formkey.Text = ar[1];
                string[] tempidar = ar[2].Split('/');
                for (int i = 0; i < (tempidar.Length - 1); i++)
                {
                    if (!this.myid.Equals(tempidar[i]))
                    {
                        string tempid = tempidar[i];
                        string name = getName(tempid);
                        if (i == 0)
                        {
                            chatForm.Text += name;
                        }
                        else
                        {
                            chatForm.Text += "/" + name;
                        }
                        TreeNode parti = chatForm.ChattersTree.Nodes.Add(tempid, name + "(" + tempid + ")");
                        parti.Tag = tempid;
                        parti.ImageIndex = 0;
                        parti.SelectedImageIndex = 0;
                    }
                }

                chatForm.ReBox.KeyUp += new KeyEventHandler(getMsg);   //�亯 �޽��� �ڽ��� Ű�̺�Ʈ ����
                chatForm.BtnAddChatter.MouseClick += new MouseEventHandler(BtnAddChatter_Click);
                chatForm.chatSendFile.MouseClick += new MouseEventHandler(chatSendFile_Click);
                chatForm.btnSend.Click += new EventHandler(btnSend_Click);
                chatForm.FormClosing += new FormClosingEventHandler(chatForm_FormClosing);
                chatForm.txtbox_exam.ForeColorChanged += new EventHandler(txtbox_exam_Changed);
                chatForm.txtbox_exam.FontChanged += new EventHandler(txtbox_exam_Changed);
                //chatForm.WindowState = FormWindowState.Minimized;
                ChatFormList[chatForm.Formkey.Text] = chatForm;
                chatForm.Show();

                string mymsg = ar[3] + " ���� �� :";
                int lines = chatForm.chatBox.Lines.Length;
                chatForm.chatBox.AppendText(mymsg + "\r\n");
                int findnum = chatForm.chatBox.Find(mymsg, RichTextBoxFinds.Reverse);
                chatForm.chatBox.SelectionBackColor = Color.White;
                chatForm.chatBox.Select(findnum, mymsg.Length);
                chatForm.chatBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                chatForm.chatBox.SelectionFont = new Font("����", 9.0f, FontStyle.Bold);
                chatForm.chatBox.ScrollToCaret();

                mymsg = ar[4];
                lines = chatForm.chatBox.Lines.Length;
                chatForm.chatBox.AppendText("��  " + mymsg + "\r\n\r\n");
                findnum = chatForm.chatBox.Find(mymsg, RichTextBoxFinds.Reverse);
                chatForm.chatBox.SelectionBackColor = Color.White;
                chatForm.chatBox.SelectionFont = new Font("����", 9.0f, FontStyle.Regular);
                chatForm.chatBox.ScrollToCaret();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ��ȭâ �� ������ ��ư Ŭ���� �Է��� ��ȭ�޽��� ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {

                PictureBox button = (PictureBox)sender;
                TextBox msgBox = null;
                TextBox exam = null;  //��Ʈ���� �̸����� txtbox
                string str = null;

                int num = button.Parent.Controls.Count;

                bool SendAvailable = true;

                for (int i = 0; i < num; i++)
                {
                    if ("txtbox_exam".Equals(button.Parent.Controls[i].Name))
                    {
                        exam = (TextBox)button.Parent.Controls[i];
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if ("ChattersTree".Equals(button.Parent.Controls[i].Name))
                    {
                        if (((TreeView)button.Parent.Controls[i]).Nodes.Count == 0) //ä�������� ����Ʈ�信 �����ڰ� ���ٸ�
                        {
                            MessageBox.Show("��ȭ ������ �����ϴ�.\r\n��ȭ�� ������ �߰��� �ּ���.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SendAvailable = false;
                            logWrite("ChattersTree �� ��ȭ���� ����");
                        }
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if ("ReBox".Equals(button.Parent.Controls[i].Name))
                    {
                        msgBox = (TextBox)button.Parent.Controls[i];
                        break;
                    }
                }

                if (SendAvailable == true) //ä�������ڰ� ���� ��츸
                {
                    if (msgBox.Text.Trim().Length != 0)
                    {
                        string key = null;
                        string ids = this.myid + "/";
                        for (int i = 0; i < num; i++)
                        {
                            if ("Formkey".Equals(button.Parent.Controls[i].Name))
                            {
                                key = button.Parent.Controls[i].Text;
                                logWrite("Formkey=<" + key + ">");
                                break;
                            }
                        }

                        for (int i = 0; i < num; i++)
                        {
                            if ("ChattersTree".Equals(button.Parent.Controls[i].Name))
                            {
                                ArrayList array = new ArrayList();
                                TreeView view = (TreeView)button.Parent.Controls[i];
                                if (view.Nodes.Count != 0)
                                {
                                    for (int n = 0; n < view.Nodes.Count; n++)
                                    {
                                        ids += view.Nodes[n].Tag + "/";
                                        array.Add(view.Nodes[n].Tag);
                                    }
                                }
                                str = "16|" + key + "|" + ids + "|" + myname + "|" + msgBox.Text.Trim();    //d|Formkey|id/id/..|�߽���name|�޽��� 
                                logWrite("��ȭ�޽��� ���� : " + str);

                                SendMsg(str, server);
                                //foreach (string receiverid in array)
                                //{
                                //    if (InList.Contains(receiverid) && InList[receiverid] != null)
                                //    {

                                //        IPEndPoint iep = (IPEndPoint)InList[receiverid];

                                //        logWrite("��ȭ���� id : " + receiverid + " port : " + iep.Port.ToString());

                                //        SendMsg(str, iep);
                                //    }
                                //}
                                break;
                            }
                        }
                        for (int i = 0; i < num; i++)
                        {
                            if ("chatBox".Equals(button.Parent.Controls[i].Name))
                            {
                                RichTextBox box = (RichTextBox)msgBox.Parent.Controls[i];
                                TextBox tempbox = new TextBox();
                                string mymsg = "[" + myname + "] : " + msgBox.Text.Trim();
                                tempbox.Text = mymsg;
                                int lines = box.Lines.Length;
                                box.AppendText(mymsg + " (" + DateTime.Now.ToString() + ")" + "\r\n\r\n");
                                int findnum = box.Find(mymsg, RichTextBoxFinds.Reverse);
                                box.SelectionBackColor = Color.White;
                                box.Select(findnum, mymsg.Length);
                                box.SelectionColor = exam.ForeColor;
                                box.SelectionFont = exam.Font;
                                box.ScrollToCaret();
                                break;
                            }
                        }
                        msgBox.Clear();
                        msgBox.Focus();
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ��ȭâ���� ���Ϻ����� ��ư Ŭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chatSendFile_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Hashtable list = new Hashtable();
                Button button = (Button)sender;
                int num = button.Parent.Parent.Controls.Count;
                for (int i = 0; i < num; i++)
                {
                    if ("ChattersTree".Equals(button.Parent.Parent.Controls[i].Name))
                    {
                        TreeView view = (TreeView)button.Parent.Parent.Controls[i];
                        if (view.Nodes.Count != 0)
                        {
                            TreeNodeCollection col = view.Nodes;
                            foreach (TreeNode node in col)
                            {
                                string[] ar1 = node.Text.Split('(');
                                string[] ar2 = ar1[1].Split(')');
                                list[ar2[0]] = ar1[0];
                            }
                        }
                        break;
                    }
                }
                MakeSendFileForm(list);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ����ڰ� ��ȭ�ϱ⸦ ���ý� ��ȭâ ����
        /// </summary>
        /// <param name="chatter"></param>
        /// <param name="ids"></param>
        private void MakeChatForm(string chatter, string ids)
        {
            try
            {
                ChatForm chatForm = new ChatForm();
                chatForm.Text = chatter;
                chatForm.txtbox_exam.Font = txtfont;
                chatForm.txtbox_exam.ForeColor = txtcolor;
                chatForm.Formkey.Text = DateTime.Now.ToString() + "!" + this.myid;
                logWrite("Formkey ���� : <" + chatForm.Formkey.Text + ">");
                ToolTip tip = new ToolTip();
                tip.SetToolTip(chatForm.BtnAddChatter, "��ȭ ���� �߰�");
                chatForm.KeyDown += new KeyEventHandler(chatForm_KeyDown);
                chatForm.btnSend.KeyDown += new KeyEventHandler(btnSend_KeyDown);
                chatForm.ReBox.KeyDown += new KeyEventHandler(getMsg);    //�亯 �޽��� �ڽ��� Ű�̺�Ʈ ����
                chatForm.BtnAddChatter.MouseClick += new MouseEventHandler(BtnAddChatter_Click);
                chatForm.chatSendFile.MouseClick += new MouseEventHandler(chatSendFile_Click);
                chatForm.FormClosing += new FormClosingEventHandler(chatForm_FormClosing);
                chatForm.txtbox_exam.ForeColorChanged += new EventHandler(txtbox_exam_Changed);
                chatForm.txtbox_exam.FontChanged += new EventHandler(txtbox_exam_Changed);
                chatForm.btnSend.Click += new EventHandler(btnSend_Click);
                ChatFormList[chatForm.Formkey.Text] = chatForm;
                chatForm.Show();
                chatForm.ReBox.Focus();

                if (ids != null)
                {
                    string name = getName(ids);
                    TreeNode node = chatForm.ChattersTree.Nodes.Add(ids, name + "(" + ids + ")");//��ȭâ�� ������ ��� �߰�(key=id, text=name)
                    node.Tag = ids;
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btnSend_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Button button = (Button)sender;
                    TextBox msgBox = null;
                    int count = button.Parent.Controls.Count;

                    for (int i = 0; i < count; i++)
                    {
                        if (button.Parent.Controls[i].Name.Equals("ReBox"))
                        {
                            msgBox = (TextBox)button.Parent.Controls[i];
                            break;
                        }
                    }

                    TextBox exam = null;
                    string str = null;
                    msgBox.Text = msgBox.Text.Trim();
                    if (msgBox.Text.Length != 0)
                    {
                        int num = msgBox.Parent.Controls.Count;

                        bool SendAvailable = true;

                        for (int i = 0; i < num; i++)
                        {
                            if ("txtbox_exam".Equals(msgBox.Parent.Controls[i].Name))
                            {
                                exam = (TextBox)msgBox.Parent.Controls[i];
                                break;
                            }
                        }

                        for (int i = 0; i < num; i++)
                        {
                            if ("ChattersTree".Equals(msgBox.Parent.Controls[i].Name))
                            {
                                if (((TreeView)msgBox.Parent.Controls[i]).Nodes.Count == 0) //ä�������� ����Ʈ�信 �����ڰ� ���ٸ�
                                {
                                    MessageBox.Show("��ȭ ������ �����ϴ�.\r\n��ȭ�� ������ �߰��� �ּ���.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    SendAvailable = false;
                                    logWrite("ChattersTree �� ��ȭ���� ����");
                                }
                                break;
                            }
                        }

                        if (SendAvailable == true) //ä�������ڰ� ���� ��츸
                        {
                            string key = null;
                            string ids = this.myid + "/";
                            for (int i = 0; i < num; i++)
                            {
                                if ("Formkey".Equals(msgBox.Parent.Controls[i].Name))
                                {
                                    key = msgBox.Parent.Controls[i].Text;
                                    logWrite("Formkey=<" + key + ">");
                                    break;
                                }
                            }

                            for (int i = 0; i < num; i++)
                            {
                                if ("ChattersTree".Equals(msgBox.Parent.Controls[i].Name))
                                {
                                    ArrayList array = new ArrayList();
                                    TreeView view = (TreeView)msgBox.Parent.Controls[i];
                                    if (view.Nodes.Count != 0)
                                    {
                                        for (int n = 0; n < view.Nodes.Count; n++)
                                        {
                                            ids += view.Nodes[n].Tag + "/";
                                            array.Add(view.Nodes[n].Tag);
                                        }
                                    }
                                    str = "16|" + key + "|" + ids + "|" + myname + "|" + msgBox.Text.Trim();    //d|Formkey|id/id/..|�߽���name|�޽��� 
                                    logWrite("��ȭ�޽��� ���� : " + str);

                                    SendMsg(str, server);
                                    //foreach (string receiverid in array)
                                    //{
                                    //    if (InList.Contains(receiverid) && InList[receiverid] != null)
                                    //    {

                                    //        IPEndPoint iep = (IPEndPoint)InList[receiverid];

                                    //        logWrite("��ȭ���� id : " + receiverid + " port : " + iep.Port.ToString());

                                    //        SendMsg(str, iep);
                                    //    }
                                    //}
                                    break;
                                }
                            }


                            for (int i = 0; i < num; i++)
                            {
                                if ("chatBox".Equals(msgBox.Parent.Controls[i].Name))
                                {
                                    RichTextBox box = (RichTextBox)msgBox.Parent.Controls[i];
                                    TextBox tempbox = new TextBox();
                                    string mymsg = "[" + myname + "] : " + msgBox.Text.Trim();
                                    tempbox.Text = mymsg;
                                    int lines = box.Lines.Length;
                                    box.AppendText(mymsg + " (" + DateTime.Now.ToString() + ")" + "\r\n\r\n");
                                    int findnum = box.Find(mymsg, RichTextBoxFinds.Reverse);
                                    box.SelectionBackColor = Color.White;
                                    box.Select(findnum, mymsg.Length);
                                    box.SelectionColor = exam.ForeColor;
                                    box.SelectionFont = exam.Font;
                                    box.ScrollToCaret();

                                    break;
                                }
                            }
                            msgBox.Clear();
                            msgBox.Focus();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void chatForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Form form = (Form)sender;
                    TextBox msgBox = null;
                    int count = form.Controls.Count;

                    for (int i = 0; i < count; i++)
                    {
                        if (form.Controls[i].Name.Equals("ReBox"))
                        {
                            msgBox = (TextBox)form.Controls[i];
                            break;
                        }
                    }

                    TextBox exam = null;
                    string str = null;
                    msgBox.Text = msgBox.Text.Trim();
                    if (msgBox.Text.Length != 0)
                    {
                        int num = msgBox.Parent.Controls.Count;

                        bool SendAvailable = true;

                        for (int i = 0; i < num; i++)
                        {
                            if ("txtbox_exam".Equals(msgBox.Parent.Controls[i].Name))
                            {
                                exam = (TextBox)msgBox.Parent.Controls[i];
                                break;
                            }
                        }

                        for (int i = 0; i < num; i++)
                        {
                            if ("ChattersTree".Equals(msgBox.Parent.Controls[i].Name))
                            {
                                if (((TreeView)msgBox.Parent.Controls[i]).Nodes.Count == 0) //ä�������� ����Ʈ�信 �����ڰ� ���ٸ�
                                {
                                    MessageBox.Show("��ȭ ������ �����ϴ�.\r\n��ȭ�� ������ �߰��� �ּ���.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    SendAvailable = false;
                                    logWrite("ChattersTree �� ��ȭ���� ����");
                                }
                                break;
                            }
                        }

                        if (SendAvailable == true) //ä�������ڰ� ���� ��츸
                        {
                            string key = null;
                            string ids = this.myid + "/";

                            for (int i = 0; i < num; i++)
                            {
                                if ("Formkey".Equals(msgBox.Parent.Controls[i].Name))
                                {
                                    key = msgBox.Parent.Controls[i].Text;
                                    logWrite("Formkey=<" + key + ">");
                                    break;
                                }
                            }

                            for (int i = 0; i < num; i++)
                            {
                                if ("ChattersTree".Equals(msgBox.Parent.Controls[i].Name))
                                {
                                    ArrayList array = new ArrayList();
                                    TreeView view = (TreeView)msgBox.Parent.Controls[i];
                                    if (view.Nodes.Count != 0)
                                    {
                                        for (int n = 0; n < view.Nodes.Count; n++)
                                        {
                                            ids += view.Nodes[n].Tag + "/";
                                            array.Add(view.Nodes[n].Tag);
                                        }
                                    }
                                    str = "16|" + key + "|" + ids + "|" + myname + "|" + msgBox.Text.Trim();    //d|Formkey|id/id/..|�߽���name|�޽��� 
                                    logWrite("��ȭ�޽��� ���� : " + str);

                                    SendMsg(str, server);
                                    //foreach (string receiverid in array)
                                    //{
                                    //    if (InList.Contains(receiverid) && InList[receiverid] != null)
                                    //    {

                                    //        IPEndPoint iep = (IPEndPoint)InList[receiverid];

                                    //        logWrite("��ȭ���� id : " + receiverid + " port : " + iep.Port.ToString());

                                    //        SendMsg(str, iep);
                                    //    }
                                    //}
                                    break;
                                }
                            }

                            for (int i = 0; i < num; i++)
                            {
                                if ("chatBox".Equals(msgBox.Parent.Controls[i].Name))
                                {
                                    RichTextBox box = (RichTextBox)msgBox.Parent.Controls[i];
                                    TextBox tempbox = new TextBox();
                                    string mymsg = "[" + myname + "] : " + msgBox.Text.Trim();
                                    tempbox.Text = mymsg;
                                    int lines = box.Lines.Length;
                                    box.AppendText(mymsg + " (" + DateTime.Now.ToString() + ")" + "\r\n\r\n");
                                    int findnum = box.Find(mymsg, RichTextBoxFinds.Reverse);
                                    box.SelectionBackColor = Color.White;
                                    box.Select(findnum, mymsg.Length);
                                    box.SelectionColor = exam.ForeColor;
                                    box.SelectionFont = exam.Font;
                                    box.ScrollToCaret();

                                    break;
                                }
                            }
                            msgBox.Clear();
                            msgBox.Focus();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void txtbox_exam_Changed(object sender, EventArgs e)
        {
            try
            {
                TextBox box = (TextBox)sender;
                txtcolor = box.ForeColor;
                txtfont = box.Font;
                logWrite("����� ��Ʈ/���� ���� : " + txtcolor.Name + "/" + txtfont.Name);
                saveFontColor(txtcolor, txtfont);
            }
            catch (Exception ex)
            {
                logWrite("txtbox_exam_Changed Error : " + ex.ToString());
            }
        }

        private void saveFontColor(Color color, Font font)
        {
            try
            {
                string c_color = color.Name;
                string c_font = font.ToHfont().ToInt32().ToString();
                setConfigXml("WDMsg_Client.exe.config", "custom_color", c_color);
                setConfigXml("WDMsg_Client.exe.config", "custom_font", c_font);
            }
            catch (Exception ex)
            {
                logWrite("saveFontColor Error : " + ex.ToString());
            }
        }


        private Color getCustomColor()
        {
            Color c = txtcolor;
            try
            {
                if (custom_color != null && custom_color.Length > 0)
                {
                    logWrite("custom_color = " + custom_color);
                    c = Color.FromName(custom_color);
                }
            }
            catch (Exception ex)
            {
                logWrite("getCustomColor Error : " + ex.ToString());
            }

            return c;
        }

        private Font getCustomFont()
        {
            System.Drawing.Font f = null;
            try
            {
                if (custom_font != null && custom_font.Length > 0)
                {
                    logWrite("custom_font = " + custom_font);
                    IntPtr ptr = new IntPtr(Convert.ToInt32(custom_font));
                    f = System.Drawing.Font.FromHfont(ptr);
                }
            }
            catch (Exception ex)
            {
                logWrite("getCustomFont Error : " + ex.ToString());
            }
            return f;
        }

        private void btn_fontcolor_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Form form = (Form)button.Parent;
            int num = button.Parent.Controls.Count;
            TextBox box = null;

            for (int i = 0; i < num; i++)
            {
                if (button.Parent.Controls[i].Name.Equals("txtbox_exam"))
                {
                    box = (TextBox)button.Parent.Controls[i];
                }
            }

            ColorDialog colorDialog = new ColorDialog();
            colorDialog.SolidColorOnly = true;
            DialogResult result = colorDialog.ShowDialog(form);
            if (result == DialogResult.OK)
            {
                box.ForeColor = colorDialog.Color;
            }
        }

        private void btn_font_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Form form = (Form)button.Parent;
            int num = button.Parent.Controls.Count;
            TextBox box = null;

            for (int i = 0; i < num; i++)
            {
                if (button.Parent.Controls[i].Name.Equals("txtbox_exam"))
                {
                    box = (TextBox)button.Parent.Controls[i];
                }
            }

            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowApply = false;
            DialogResult result = fontDialog.ShowDialog(form);
            if (result == DialogResult.OK)
            {
                box.ForeColor = fontDialog.Color;
                box.Font = fontDialog.Font;
                box.Text = "��abAB";
            }
        }


        /// <summary>
        /// ����ڰ� �ټ��� ����� ��ȭ�ϱ⸦ ��û���� ��� ��ȭâ ����
        /// </summary>
        /// <param name="chattersName">��ȭ������ ������ �̸���</param>
        /// <param name="nonchatters">��ȭ�Ұ����� ������ �̸���</param>
        /// <param name="ids">��ȭ�������� id</param>
        private void MakeChatForm(string chattersName, ArrayList nonchatters, string ids)
        {
            try
            {
                ChatForm chatForm = new ChatForm();
                chatForm.Text = chattersName;
                chatForm.txtbox_exam.ForeColor = txtcolor;
                chatForm.txtbox_exam.Font = txtfont;
                chatForm.Formkey.Text = DateTime.Now.ToString() + this.myid;
                logWrite("Formkey ���� : " + chatForm.Formkey.Text);

                string[] tempidar = ids.Split('/');
                for (int i = 0; i < (tempidar.Length - 1); i++)
                {
                    if (!tempidar[i].Equals(this.myid))
                    {
                        string tempid = tempidar[i];
                        string name = getName(tempid);
                        TreeNode node = chatForm.ChattersTree.Nodes.Add(tempid, name + "(" + tempid + ")");
                        node.Tag = tempid;
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                    }
                }
                if (nonchatters.Count != 0)
                {
                    foreach (string non in nonchatters)
                    {
                        chatForm.chatBox.AppendText(non + " ���� ��ȭ�� �Ұ����� �����̹Ƿ� �������� ���߽��ϴ�.\r\n\r\n");
                    }
                }
                chatForm.Show();
                chatForm.ReBox.KeyDown += new KeyEventHandler(getMsg);    //�亯 �޽��� �ڽ��� Ű�̺�Ʈ ����
                chatForm.BtnAddChatter.MouseClick += new MouseEventHandler(BtnAddChatter_Click);
                chatForm.chatSendFile.MouseClick += new MouseEventHandler(chatSendFile_Click);
                chatForm.FormClosing += new FormClosingEventHandler(chatForm_FormClosing);
                chatForm.txtbox_exam.ForeColorChanged += new EventHandler(txtbox_exam_Changed);
                chatForm.txtbox_exam.FontChanged += new EventHandler(txtbox_exam_Changed);
                chatForm.ReBox.Focus();
                ChatFormList[chatForm.Formkey.Text] = chatForm;       //ChatterList ����(key=id/id/.. value=chatform)
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ��ȭâ���� ��ȭ�� �߰� Ŭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddChatter_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int controlsNum = button.Parent.Parent.Controls.Count;
                string key = null;
                for (int i = 0; i < controlsNum; i++)
                {
                    logWrite(button.Parent.Parent.Controls[i].Name);
                    if ("Formkey".Equals(button.Parent.Parent.Controls[i].Name))
                    {
                        key = button.Parent.Parent.Controls[i].Text;
                        logWrite("�����߰� �� Ű�� ���� :" + key);
                    }
                }
                MakeAddChatterForm(key);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ��ȭ�� �߰� ���� �� ����
        /// </summary>
        /// <param name="formkey"></param>
        private void MakeAddChatterForm(string formkey)
        {
            try
            {
                AddMemberForm addform = new AddMemberForm();
                addform.BtnConfirm.MouseClick += new MouseEventHandler(BtnConfirm_Click);
                addform.BtnCancel.Click += new EventHandler(BtnCancel_Click_forChat);
                addform.radiobt_g.Click += new EventHandler(radiobt_g_Click);
                addform.radiobt_con.Click += new EventHandler(radiobt_con_Click);
                addform.radiobt_all.Visible = false;
                addform.combobox_team.SelectedValueChanged += new EventHandler(combobox_team_SelectedValueChanged);
                addform.CurrInListBox.MouseDoubleClick += new MouseEventHandler(CurrInListBox_MouseDoubleClick);

                ChatForm form = (ChatForm)ChatFormList[formkey];

                if (form.ChattersTree.Nodes.Count != 0)
                {
                    TreeNodeCollection col = form.ChattersTree.Nodes;
                    foreach (TreeNode node in col)
                    {
                        if (node.Text.Length != 0)
                        {
                            addform.AddListBox.Items.Add(node.Text);
                        }
                    }
                }

                if (InList.Count != 0)
                {
                    foreach (DictionaryEntry de in InList)
                    {
                        if (de.Value != null)
                        {
                            string name = getName(de.Key.ToString());
                            string item = name + "(" + de.Key.ToString() + ")";
                            if (!addform.AddListBox.Items.Contains(item))
                            {
                                addform.CurrInListBox.Items.Add(item);
                            }
                        }
                    }
                }

                addform.formkey.Text = formkey;
                addform.radiobt_con.Checked = true;
                addform.Show(form);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void BtnCancel_Click_forChat(object sender, EventArgs e)
        {
            try
            {
                PictureBox button = (PictureBox)sender;
                int num = button.Parent.Controls.Count;
                ListBox addbox = null;
                ListBox currbox = null;
                ArrayList list = new ArrayList();

                string formkey = null;
                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("formkey"))
                    {
                        Label box = (Label)button.Parent.Controls[i];
                        formkey = box.Text;
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("CurrInListBox"))
                    {
                        currbox = (ListBox)button.Parent.Controls[i];
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("AddListBox"))
                    {
                        addbox = (ListBox)button.Parent.Controls[i];
                        int count = addbox.Items.Count;
                        for (int a = 0; a < count; a++)
                        {
                            string additem = (string)addbox.Items[a];
                            list.Add(additem);
                        }
                        break;
                    }
                }

                ChatForm form = (ChatForm)ChatFormList[formkey];

                if (form.ChattersTree != null && form.ChattersTree.Nodes != null && form.ChattersTree.Nodes.Count != 0)
                {
                    TreeNodeCollection col = form.ChattersTree.Nodes;
                    foreach (TreeNode node in col)
                    {
                        if (node.Text.Length != 0)
                        {
                            foreach (object obj in list)
                            {
                                string additem = (string)obj;
                                if (!node.Text.Equals(additem))
                                {
                                    currbox.Items.Add(additem);
                                    addbox.Items.Remove(additem);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }

        }

        /// <summary>
        /// �������۽� ����� �߰� ��ư Ŭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click_forFile(object sender, EventArgs e)
        {
            try
            {
                PictureBox button = (PictureBox)sender;
                int num = button.Parent.Controls.Count;
                ListBox addbox = null;
                ListBox currbox = null;
                ArrayList list = new ArrayList();

                string formkey = null;
                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("formkey"))
                    {
                        Label box = (Label)button.Parent.Controls[i];
                        formkey = box.Text;
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("CurrInListBox"))
                    {
                        currbox = (ListBox)button.Parent.Controls[i];
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("AddListBox"))
                    {
                        addbox = (ListBox)button.Parent.Controls[i];
                        int count = addbox.Items.Count;
                        for (int a = 0; a < count; a++)
                        {
                            string additem = (string)addbox.Items[a];
                            list.Add(additem);
                        }
                        break;
                    }
                }

                SendFileForm form = (SendFileForm)FileSendFormList[formkey];
                string[] receiverArray = null;
                if (form.txtbox_FileReceiver.Text.Length != 0)
                {
                    receiverArray = form.txtbox_FileReceiver.Text.Split(';');
                }
                if (receiverArray != null)
                {
                    foreach (string receiver in receiverArray)
                    {
                        if (receiver.Length > 2)  //������ ���� ���ڿ� ����
                        {
                            foreach (object obj in list)
                            {
                                string additem = (string)obj;
                                if (!receiver.Equals(additem))
                                {
                                    currbox.Items.Add(additem);
                                    addbox.Items.Remove(additem);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }


        /// <summary>
        /// �������۽� ������ �߰� ��ư Ŭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click_forMemo(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int num = button.Parent.Controls.Count;
                ListBox addbox = null;
                ListBox currbox = null;
                ArrayList list = new ArrayList();
                string formkey = null;

                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("formkey"))
                    {
                        Label box = (Label)button.Parent.Controls[i];
                        formkey = box.Text;
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("CurrInListBox"))
                    {
                        currbox = (ListBox)button.Parent.Controls[i];
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("AddListBox"))
                    {
                        addbox = (ListBox)button.Parent.Controls[i];
                        foreach (object obj in addbox.Items)
                        {
                            string additem = obj.ToString();
                            list.Add(additem);
                        }
                        break;
                    }
                }

                SendMemoForm form = (SendMemoForm)MemoFormList[formkey];

                string[] receiverArray = null;
                if (form.txtbox_receiver.Text.Length != 0)
                {
                    receiverArray = form.txtbox_receiver.Text.Split(';');
                }
                if (receiverArray != null && receiverArray.Length != 0)
                {
                    foreach (object obj in list)
                    {
                        string additem = (string)obj;
                        bool isExist = true;
                        foreach (string receiver in receiverArray)
                        {
                            if (receiver.Length != 0)  //������ ���� ���ڿ� ����
                            {
                                if (!receiver.Equals(additem))
                                {
                                    logWrite("receiver : " + receiver);
                                    logWrite("additem : " + additem);
                                    isExist = false;
                                }
                                else
                                {
                                    isExist = true;
                                    break;
                                }
                            }
                        }

                        if (isExist == false)
                        {
                            if (!currbox.Items.Contains(additem))
                            {
                                currbox.Items.Add(additem);
                            }
                            addbox.Items.Remove(additem);
                        }
                    }
                }
                else
                {
                    foreach (object obj in list)
                    {
                        string additem = (string)obj;
                        if (!currbox.Items.Contains(additem))
                        {
                            currbox.Items.Add(additem);
                        }
                        addbox.Items.Remove(additem);
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }

        }

        /// <summary>
        /// ���� �� ���� ���� ������ �߰� ������ "������" ���� ��ư Ŭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radiobt_con_Click(object sender, EventArgs e)
        {
            try
            {
                RadioButton rbt = (RadioButton)sender;
                ListBox addbox = null;
                int controls = rbt.Parent.Parent.Controls.Count;

                for (int i = 0; i < controls; i++)
                {
                    if ("combobox_team".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        ComboBox box = (ComboBox)rbt.Parent.Parent.Controls[i];
                        box.Visible = false;
                        break;
                    }
                }

                for (int i = 0; i < controls; i++)
                {
                    if ("label_choice".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        Label box = (Label)rbt.Parent.Parent.Controls[i];
                        box.Visible = false;
                        break;
                    }
                }

                for (int i = 0; i < controls; i++)
                {
                    if ("AddListBox".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        addbox = (ListBox)rbt.Parent.Parent.Controls[i];
                        break;
                    }
                }

                for (int i = 0; i < controls; i++)
                {
                    if ("CurrInListBox".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        ListBox box = (ListBox)rbt.Parent.Parent.Controls[i];
                        box.Items.Clear();
                        if (InList.Count != 0)
                        {
                            foreach (DictionaryEntry de in InList)
                            {
                                if (de.Value != null)
                                {
                                    string Name = getName(de.Key.ToString());
                                    string item = Name + "(" + de.Key.ToString() + ")";
                                    if (!addbox.Items.Contains(item))
                                    {
                                        box.Items.Add(item);
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// ���� �� ���� ���� ������ �߰� ������ "��ü" ���� ��ư Ŭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radiobt_all_Click(object sender, EventArgs e)
        {
            try
            {
                RadioButton rbt = (RadioButton)sender;
                ListBox addbox = null;
                int controls = rbt.Parent.Parent.Controls.Count;

                for (int i = 0; i < controls; i++)
                {
                    if ("combobox_team".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        ComboBox box = (ComboBox)rbt.Parent.Parent.Controls[i];
                        box.Visible = false;
                    }
                }
                for (int i = 0; i < controls; i++)
                {
                    if ("label_choice".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        Label box = (Label)rbt.Parent.Parent.Controls[i];
                        box.Visible = false;
                    }
                }

                for (int i = 0; i < controls; i++)
                {
                    if ("AddListBox".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        addbox = (ListBox)rbt.Parent.Parent.Controls[i];
                        break;
                    }
                }

                for (int i = 0; i < controls; i++)
                {
                    if ("CurrInListBox".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        ListBox box = (ListBox)rbt.Parent.Parent.Controls[i];
                        box.Items.Clear();
                        GetAllMemberDelegate getAll = new GetAllMemberDelegate(GetAllMember);
                        Hashtable all = (Hashtable)Invoke(getAll, null);
                        if (all != null)
                        {
                            if (all.Count != 0)
                            {
                                foreach (DictionaryEntry de in all)
                                {
                                    if (de.Value != null)
                                    {
                                        string item = (string)de.Value + "(" + (string)de.Key + ")";
                                        if (!addbox.Items.Contains(item))
                                        {
                                            box.Items.Add(item);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// Ʈ���� ��� ����� ����� �ҷ��� Hashtable�� ����
        /// </summary>
        /// <returns></returns>
        private Hashtable GetAllMember()
        {

            Hashtable all = new Hashtable();//key=id, value=name
            try
            {
                if (treesource != null && treesource.Count != 0)  //treesource(key=���̸�, value=list(id!name)
                {
                    foreach (DictionaryEntry de in treesource)
                    {
                        ArrayList list = (ArrayList)de.Value;
                        foreach (object obj in list)
                        {
                            string item = (string)obj;
                            string[] array = item.Split('!');
                            string tempid = array[0];
                            string tempname = array[1];
                            all[tempid] = tempname;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
            return all;
        }


        private void radiobt_g_Click(object sender, EventArgs e)
        {
            objectDele dele = new objectDele(showGroupComboBoxForAddMember);
            Invoke(dele, sender);
        }

        private void showGroupComboBoxForAddMember(object sender)
        {
            try
            {
                logWrite("�׷캰 ���� ����!");

                RadioButton rbt = (RadioButton)sender;

                int controls = rbt.Parent.Parent.Controls.Count;

                for (int i = 0; i < controls; i++)
                {
                    if ("label_choice".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        Label box = (Label)rbt.Parent.Parent.Controls[i];
                        box.Visible = true;
                        break;
                    }
                }

                for (int i = 0; i < controls; i++)
                {
                    if ("combobox_team".Equals(rbt.Parent.Parent.Controls[i].Name))
                    {
                        ComboBox box = (ComboBox)rbt.Parent.Parent.Controls[i];
                        box.Items.Clear();
                        foreach (DictionaryEntry de in treesource)
                        {
                            box.Items.Add(de.Key.ToString());
                        }
                        box.Visible = true;
                        break;
                    }
                }

                for (int i = 0; i < controls; i++)
                {
                    if ("CurrInListBox".Equals(rbt.Parent.Controls[i].Name))
                    {
                        ListBox box = (ListBox)rbt.Parent.Controls[i];

                        box.Items.Clear();

                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void combobox_team_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox box = (ComboBox)sender;
                string teamname = (String)box.SelectedItem;
                GetMember getmember = new GetMember(getMember);
                Hashtable memTable = (Hashtable)Invoke(getmember, teamname);
                int num = box.Parent.Controls.Count;
                ListBox addbox = null;

                for (int i = 0; i < num; i++)
                {
                    if ("AddListBox".Equals(box.Parent.Controls[i].Name))
                    {
                        addbox = (ListBox)box.Parent.Controls[i];
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if ("CurrInListBox".Equals(box.Parent.Controls[i].Name))
                    {
                        ListBox listbox = (ListBox)box.Parent.Controls[i];

                        listbox.Items.Clear();
                        foreach (DictionaryEntry de in memTable)
                        {
                            string tempname = (String)de.Value;
                            string tempid = (String)de.Key;
                            if (InList.ContainsKey(tempid) && InList[tempid] != null)
                            {
                                string item = tempname + "(" + tempid + ")";
                                if (!addbox.Items.Contains(item))
                                {
                                    listbox.Items.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void combobox_team_SelectedValueChangedAll(object sender, EventArgs e)
        {
            try
            {
                ComboBox box = (ComboBox)sender;
                ListBox addbox = null;
                string teamname = (String)box.SelectedItem;
                GetMember getmember = new GetMember(getMember);
                Hashtable memTable = (Hashtable)Invoke(getmember, teamname);
                int num = box.Parent.Controls.Count;

                for (int i = 0; i < num; i++)
                {
                    if ("AddListBox".Equals(box.Parent.Controls[i].Name))
                    {
                        addbox = (ListBox)box.Parent.Controls[i];
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if ("CurrInListBox".Equals(box.Parent.Controls[i].Name))
                    {
                        ListBox listbox = (ListBox)box.Parent.Controls[i];

                        listbox.Items.Clear();
                        foreach (DictionaryEntry de in memTable)
                        {
                            string tempname = (String)de.Value;
                            string tempid = (String)de.Key;
                            string item = tempname + "(" + tempid + ")";
                            if (!addbox.Items.Contains(item))
                            {
                                listbox.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }


        private string[] getTeam()
        {
            int teamnum = memTree.Nodes.Count;
            string[] teamArray = new string[teamnum];
            for (int i = 0; i < teamnum; i++)
            {
                teamArray[i] = memTree.Nodes[i].Text;
            }
            return teamArray;
        }

        private Hashtable getMember(string teamname)
        {
            Hashtable memTable = new Hashtable();
            if (treesource.ContainsKey(teamname))
            {
                ArrayList list = (ArrayList)treesource[teamname];
                foreach (object obj in list)
                {
                    string item = (string)obj;
                    string[] array = item.Split('!');
                    string tempid = array[0];
                    string tempname = array[1];
                    memTable[tempid] = tempname;
                }
            }
            return memTable;
        }



        private void BtnConfirm_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int controlsNum = button.Parent.Controls.Count;
                string key = null;
                for (int i = 0; i < controlsNum; i++)
                {
                    if ("formkey".Equals(button.Parent.Controls[i].Name))
                    {
                        Label keyvalue = (Label)button.Parent.Controls[i];
                        key = keyvalue.Text;
                    }
                }

                for (int i = 0; i < controlsNum; i++)
                {
                    if ("AddListBox".Equals(button.Parent.Controls[i].Name))
                    {
                        ListBox box = (ListBox)button.Parent.Controls[i];
                        ChatForm form = (ChatForm)ChatFormList[key];

                        if (box.Items.Count != 0)
                        {
                            string addlist = null;
                            foreach (object obj in box.Items)
                            {
                                string str = (String)obj;

                                string[] strArr = str.Split('(');  //strArr[0]=�̸�
                                string[] strArr1 = strArr[1].Split(')');//strArr1[0]=���̵�
                                addlist += strArr1[0] + "/";
                            }

                            //�߰��� ����� ����Ʈ ���� ��ȭ�ڿ��� ����
                            if (form.ChattersTree.Nodes.Count != 0)
                            {
                                GetChatters getChatters = new GetChatters(GetChattersID);
                                string[] chatters = (string[])Invoke(getChatters, form);
                                string msg = "17|" + key + "|" + addlist + "|" + myname;      //c|formkey|id/id/...|name|

                                if (chatters != null && chatters.Length != 0)
                                {
                                    foreach (string tempid in chatters)
                                    {
                                        SendMsg(msg + "|" + tempid, server);
                                    }
                                }
                            }

                            //�߰��� ����� ä��â�� ��ȭ�� ����Ʈ�� �߰�
                            foreach (object obj in box.Items)
                            {
                                string str = (String)obj;

                                string[] strArr = str.Split('(');  //strArr[0]=�̸�
                                string[] strArr1 = strArr[1].Split(')');//strArr1[0]=���̵�

                                //��ȭ�� ��� �߰� ��������Ʈ ȣ��
                                AddChatter addChatter = new AddChatter(Addchatter);
                                Invoke(addChatter, new object[] { strArr1[0], strArr[0], form });
                            }
                        }
                        break;
                    }
                }
                button.Parent.Dispose();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private string[] GetChattersID(ChatForm form)
        {
            string[] chatters = null;
            try
            {
                if (form.ChattersTree.Nodes.Count != 0)
                {
                    chatters = new string[form.ChattersTree.Nodes.Count];
                    for (int i = 0; i < form.ChattersTree.Nodes.Count; i++)
                    {
                        chatters[i] = (string)form.ChattersTree.Nodes[i].Tag;
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
            return chatters;
        }

        private void Addchatter(string id, string name, ChatForm form)
        {
            TreeNode[] nodearray = form.ChattersTree.Nodes.Find(id, false);

            if (nodearray != null && nodearray.Length != 0)
            {
                foreach (TreeNode anode in nodearray)
                {
                    if (!id.Equals(anode.Tag.ToString()))
                    {
                        TreeNode node = form.ChattersTree.Nodes.Add(id, name + "(" + id + ")");
                        node.Tag = id;
                        node.ImageIndex = 0;
                        node.SelectedImageIndex = 0;
                        form.Text += "/" + name;
                        form.chatBox.AppendText("\r\n\r\n" + node.Text + "���� ��ȭ�ڷ� �߰��߽��ϴ�.\r\n\r\n");
                    }
                }
            }
            else
            {
                TreeNode node = form.ChattersTree.Nodes.Add(id, name + "(" + id + ")");
                node.Tag = id;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                form.Text += "/" + name;
                form.chatBox.AppendText("\r\n\r\n" + node.Text + "���� ��ȭ�ڷ� �߰��߽��ϴ�.\r\n\r\n");
            }
        }

        private string getName(string id)
        {
            string name = (string)MemberInfoList[id];
            return name;
        }

        private void chatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Form chat = (Form)sender;
                string key = null;
                RichTextBox box = null;



                for (int i = 0; i < chat.Controls.Count; i++)
                {
                    if (chat.Controls[i].Name.Equals("Formkey"))
                    {
                        key = chat.Controls[i].Text.Trim();
                        break;
                    }
                }

                for (int i = 0; i < chat.Controls.Count; i++)
                {
                    if (chat.Controls[i].Name.Equals("chatBox"))
                    {
                        box = (RichTextBox)chat.Controls[i];
                        break;
                    }
                }

                try
                {
                    if (box.Text.Length > 1)
                    {
                        DialogFileWrite(key, box.Text, chat.Text);
                    }
                }
                catch (Exception e1)
                {
                    logWrite("chatForm_FormClosing() : ChatterList.Remove() ���� " + e1.ToString());
                }


                for (int num = 0; num < chat.Controls.Count; num++)
                {
                    if (chat.Controls[num].Name.Equals("ChattersTree"))
                    {
                        TreeView tree = (TreeView)chat.Controls[num];

                        if (tree.Nodes.Count > 1)                      //2���̻�� ��ȭ�� ���� ���� ���
                        {
                            string str = "18|" + key + "|" + this.myid;    //q|Formkey|id 

                            for (int n = 0; n < tree.Nodes.Count; n++)  // -1 : ������ ���� �迭 ����
                            {
                                if (!this.myid.Equals(tree.Nodes[n].Tag)) //�ڽ� ���� ����
                                {

                                    str += "|" + tree.Nodes[n].Tag;

                                    logWrite("��ȭ���� id : " + tree.Nodes[n].Tag);
                                    logWrite("��ȭ���� �޽��� ���� : " + str);
                                    SendMsg(str, server);
                                }
                            }
                            break;
                        }
                        break;
                    }
                }

                ChatFormList.Remove(key);
                logWrite("ä��â �������� key=" + key + " ChatterList ���̺��� ����");

            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void Addmsg(string msg, ChatForm form)  //msg(d|Formkey|id/id/...|�߽���name|�޽���
        {
            string[] temp = msg.Split('|');
            string sendername = temp[0];
            RichTextBox box = form.chatBox;
            string sendermsg = sendername + " ���� �� :";
            int lines = box.Lines.Length;
            box.AppendText(sendermsg + "\r\n");
            int findnum = box.Find(sendermsg, RichTextBoxFinds.Reverse);
            box.SelectionBackColor = Color.White;
            box.Select(findnum, sendermsg.Length);
            box.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            box.SelectionFont = new Font("����", 9.0f, FontStyle.Bold);

            sendermsg = temp[1];
            lines = box.Lines.Length;
            box.AppendText("��  " + sendermsg + " (" + DateTime.Now.ToString() + ")" + "\r\n\r\n");
            findnum = box.Find(sendermsg, RichTextBoxFinds.Reverse);
            box.SelectionBackColor = Color.White;
            box.Select(findnum, sendermsg.Length);
            box.SelectionFont = new Font("����", 9.0f, FontStyle.Regular);
            box.ScrollToCaret();
            form.chatBox.ScrollToCaret();
        }

        private void MakeMemo(string[] tempMemo) //tempMemo(m|name|id|message)
        {
            try
            {
                MemoForm memoForm = new MemoForm();
                memoForm.Text = tempMemo[1] + "���� ����";
                memoForm.MemoCont.Text = tempMemo[3];
                memoForm.senderid.Text = tempMemo[2];
                memoForm.MemoRe.KeyUp += new KeyEventHandler(SendMemo);
                memoForm.Memobtn.MouseClick += new MouseEventHandler(Memobtn_Click);
                memoForm.Show(memolistform);
                memoForm.Activate();
                memoForm.TopMost = true;
                memoForm.MemoRe.Focus();
                MemoTable.Add(memoForm);
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void Memobtn_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                TextBox box = null;
                int count = button.Parent.Controls.Count;

                for (int i = 0; i < count; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("MemoRe"))
                    {
                        box = (TextBox)button.Parent.Controls[i];
                        break;
                    }
                }

                if (box.Text.Trim().Length != 0)
                {
                    string msg = "19|" + this.myname + "|" + this.myid + "|" + box.Text.Trim();
                    string smsg = "4|" + this.myname + "|" + this.myid + "|" + box.Text.Trim();
                    for (int i = 0; i < box.Parent.Controls.Count; i++)
                    {
                        if (box.Parent.Controls[i].Name.Equals("senderid"))
                        {
                            Label idLabel = (Label)box.Parent.Controls[i];
                            if (InList.ContainsKey(idLabel.Text) && InList[idLabel.Text] != null)
                            {
                                IPEndPoint senderIP = (IPEndPoint)InList[idLabel.Text];
                                SendMsg(msg + "|" + idLabel.Text, server);
                            }
                            else SendMsg(smsg + "|" + idLabel.Text, server);
                            box.Parent.Dispose();
                            break;
                        }
                    }
                }
                else
                {
                    box.Clear();
                    box.Focus();
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void MakeSendMemo(Hashtable MemoReceiver)  //MemoReceiver(key=name, value=id)
        {
            try
            {
                SendMemoForm form = new SendMemoForm();
                string Receiver = null;
                form.formkey.Text = DateTime.Now.ToString();
                if (MemoReceiver.Count != 0)
                {
                    foreach (DictionaryEntry de in MemoReceiver)
                    {
                        if (de.Value != null)
                        {
                            Receiver = (String)de.Key + "(" + (String)de.Value + ");";
                            form.txtbox_receiver.Text += Receiver;
                            form.receiverIDs.AppendText((String)de.Value + ";");
                        }
                    }
                    logWrite("������ receiverIDs ����Ʈ ���� : " + form.receiverIDs.Text);
                }
                form.BtnReceiver.MouseClick += new MouseEventHandler(BtnReceiver_Click);
                form.BtnSend.MouseClick += new MouseEventHandler(BtnSend_Click);
                form.textBox1.KeyUp += new KeyEventHandler(textBox1_KeyDown);
                form.Show();
                form.textBox1.Focus();
                MemoFormList[form.formkey.Text] = form;
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
                {
                    TextBox send = (TextBox)sender;
                    bool MemoSendAvailable = false;
                    TextBox receiverbox = null;

                    for (int i = 0; i < send.Parent.Controls.Count; i++)
                    {
                        string control = send.Parent.Controls[i].Name;
                        if (control.Equals("txtbox_receiver"))
                        {
                            receiverbox = (TextBox)send.Parent.Controls[i];
                            if (receiverbox.Text.Length != 0) MemoSendAvailable = true;
                        }
                    }

                    if (MemoSendAvailable == true)
                    {
                        for (int i = 0; i < send.Parent.Controls.Count; i++)
                        {
                            string control = send.Parent.Controls[i].Name;
                            if (control.Equals("textBox1"))
                            {
                                TextBox memoContent = (TextBox)send.Parent.Controls[i];

                                if (memoContent.Text.Length != 0)
                                {
                                    string msg = "19|" + this.myname + "|" + this.myid + "|" + memoContent.Text.Trim(); //m|name|�߽���id|message
                                    string smsg = "4|" + this.myname + "|" + this.myid + "|" + memoContent.Text.Trim(); //m|name|�߽���id|message
                                    logWrite("���� �޽��� ���� : " + msg);

                                    string[] tempID = receiverbox.Text.Split(';');

                                    for (int n = 0; n < tempID.Length; n++)
                                    {
                                        if (tempID[n].Length != 0)
                                        {
                                            string[] array = tempID[n].Split('(');
                                            string[] array1 = array[1].Split(')');
                                            string reID = array1[0];

                                            if (InList.ContainsKey(reID) && InList[reID] != null)
                                            {
                                                SendMsg(msg + "|" + reID, server);
                                            }
                                            else
                                            {
                                                SendMsg(smsg + "|" + reID, server);
                                            }

                                            send.Parent.Dispose();
                                            break;

                                        }
                                    }
                                }
                                break;
                            }

                        }

                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(send.Parent, "������ ���� ������ ������ �ּ���", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            string key = null;
                            for (int num = 0; num < send.Parent.Controls.Count; num++)
                            {
                                if (send.Parent.Controls[num].Name.Equals("formkey"))
                                {
                                    Label keylabel = (Label)send.Parent.Controls[num];
                                    key = keylabel.Text;
                                    break;
                                }
                            }
                            AddMemoReceiver(key);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void MakeSendNotice()
        {
            SendNoticeForm form = new SendNoticeForm();
            form.BtnSend.MouseClick += new MouseEventHandler(BtnSendNotice_Click);
            form.textBox1.KeyUp += new KeyEventHandler(textBox1_KeyDown_SendNotice);
            form.Show();
        }

        private void textBox1_KeyDown_SendNotice(object sender, KeyEventArgs e)
        {
            try
            {
                bool isNomal = true;
                string title = "";
                if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
                {
                    TextBox box = (TextBox)sender;

                    if (box.Text.Length != 0)
                    {

                        int count = box.Parent.Controls.Count;

                        for (int i = 0; i < count; i++)
                        {
                            if (box.Parent.Controls[i].Name.Equals("rbt_nomal"))
                            {
                                RadioButton rbt = (RadioButton)box.Parent.Controls[i];
                                if (rbt.Checked == false)
                                {
                                    isNomal = false;
                                }
                                break;
                            }
                        }

                        for (int i = 0; i < count; i++)
                        {
                            if (box.Parent.Controls[i].Name.Equals("tbx_notice_title"))
                            {
                                TextBox tbx_title = (TextBox)box.Parent.Controls[i];
                                if (tbx_title.Text.Length > 0)
                                {
                                    title = tbx_title.Text;
                                }
                                else
                                {
                                    title = "��������";
                                }
                                break;
                            }
                        }

                        string noticeid = DateTime.Now.ToString();
                        if (isNomal == true)
                        {
                            MakeNoticeResult(noticeid, title, box.Text.Trim(), "�Ϲ�");
                            SendMsg("6|" + box.Text + "|" + this.myid + "|n" + "|" + noticeid + "|" + title, server);
                        }
                        else
                        {
                            MakeNoticeResult(noticeid, title, box.Text.Trim(), "���");
                            SendMsg("6|" + box.Text + "|" + this.myid + "|e" + "|" + noticeid + "|" + title, server);
                        }

                        ((SendNoticeForm)box.Parent).Dispose();
                    }
                    else
                    {
                        MessageBox.Show("������ ������ ���� �ּ���", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        box.Focus();
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void BtnSendNotice_Click(object sender, MouseEventArgs e)
        {
            string title = null;
            try
            {
                bool isNomal = true;
                Button button = (Button)sender;
                int num = button.Parent.Controls.Count;

                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("rbt_nomal"))
                    {
                        RadioButton rbt = (RadioButton)button.Parent.Controls[i];
                        if (rbt.Checked == false)
                        {
                            isNomal = false;
                        }
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("tbx_notice_title"))
                    {
                        TextBox tbx_title = (TextBox)button.Parent.Controls[i];
                        if (tbx_title.Text.Length > 0)
                        {
                            title = tbx_title.Text;
                        }
                        else
                        {
                            title = "��������";
                        }
                        break;
                    }
                }

                for (int i = 0; i < num; i++)
                {
                    if ("textBox1".Equals(button.Parent.Controls[i].Name))
                    {
                        TextBox box = (TextBox)button.Parent.Controls[i];
                        if (box.Text.Length != 0)
                        {
                            String NoticeTime = DateTime.Now.ToString();
                            if (isNomal == true)
                            {
                                MakeNoticeResult(NoticeTime, title, box.Text.Trim(), "�Ϲ�");
                                SendMsg("6|" + box.Text + "|" + this.myid + "|n" + "|" + NoticeTime + "|" + title, server);
                            }
                            else
                            {
                                MakeNoticeResult(NoticeTime, title, box.Text.Trim(), "���");
                                SendMsg("6|" + box.Text + "|" + this.myid + "|e" + "|" + NoticeTime + "|" + title, server);
                            }
                            ((SendNoticeForm)button.Parent).Dispose();
                        }
                        else
                        {
                            MessageBox.Show("������ ������ ���� �ּ���", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            box.Focus();
                        }
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void MakeNoticeResult(string noticetime, string title, string content, string mode)
        {
            try
            {
                if (isMadeNoticeResult == false)
                {
                    noticeresultform = new NoticeResultForm();
                    noticeresultform.listView1.Click += new EventHandler(listView1_Click);
                    noticeresultform.FormClosing += new FormClosingEventHandler(noticeresultform_FormClosing);
                    noticeresultform.FormClosed += new FormClosedEventHandler(noticeresultform_FormClosed);

                    ListViewItem item = noticeresultform.listView1.Items.Add(noticetime, "�ڼ���", null);
                    item.Tag = noticetime;
                    item.SubItems.Add(noticetime);
                    item.SubItems.Add(mode);
                    item.SubItems.Add(title);
                    item.SubItems.Add(content);

                    NoticeDetailResultForm noticedetailresultform = new NoticeDetailResultForm();
                    noticedetailresultform.FormClosing += new FormClosingEventHandler(noticedetailresultform_FormClosing);
                    foreach (DictionaryEntry de in MemberInfoList)
                    {
                        string receiver = de.Value.ToString() + "(" + de.Key.ToString() + ")";
                        ListViewItem ditem = noticedetailresultform.listView1.Items.Add(de.Key.ToString(), receiver, null);
                        ditem.ForeColor = Color.Red;
                        ListViewItem.ListViewSubItem subitem = ditem.SubItems.Add("Ȯ�� ����");
                    }
                    isMadeNoticeResult = true;
                    NoticeDetailForm[noticetime] = noticedetailresultform;
                }
                else
                {
                    ListViewItem item = noticeresultform.listView1.Items.Add(noticetime, "�ڼ���", null);
                    item.Tag = noticetime;
                    item.SubItems.Add(noticetime);
                    item.SubItems.Add(mode);
                    item.SubItems.Add(title);
                    item.SubItems.Add(content);

                    NoticeDetailResultForm noticedetailresultform = new NoticeDetailResultForm();
                    noticedetailresultform.FormClosing += new FormClosingEventHandler(noticedetailresultform_FormClosing);
                    foreach (DictionaryEntry de in MemberInfoList)
                    {
                        string receiver = de.Value.ToString() + "(" + de.Key.ToString() + ")";
                        ListViewItem ditem = noticedetailresultform.listView1.Items.Add(de.Key.ToString(), receiver, null);
                        ditem.ForeColor = Color.Red;
                        ditem.SubItems.Add("Ȯ�� ����");
                    }
                    NoticeDetailForm[noticetime] = noticedetailresultform;
                }
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void noticeresultform_FormClosed(object sender, FormClosedEventArgs e)
        {
            isMadeNoticeResult = false;
        }

        private void showNoticeResultFromDB(string[] tempMsg)// ntime��content��nmode��title�Ӿ��������1:���������2:...
        {

            logWrite("showNoticeResultFromDB ����");
            string noticetime = "";
            string title = "";
            string content = "";
            string mode = "";
            ArrayList notreader = new ArrayList();


            try
            {
                if (isMadeNoticeResult == false)
                {
                    noticeresultform = new NoticeResultForm();
                    noticeresultform.listView1.Click += new EventHandler(listView1_Click);
                    noticeresultform.FormClosing += new FormClosingEventHandler(noticeresultform_FormClosing);
                    noticeresultform.FormClosed += new FormClosedEventHandler(noticeresultform_FormClosed);

                    foreach (string strarr in tempMsg)
                    {
                        logWrite(strarr);
                        string[] itemarr = strarr.Split('��');

                        if (itemarr.Length > 3)
                        {
                            noticetime = itemarr[0];
                            content = itemarr[1];
                            mode = itemarr[2];
                            title = itemarr[3];
                            string[] readers = itemarr[4].Split(':');
                            foreach (string readerid in readers)
                            {
                                if (readerid.Trim().Length > 0)
                                {
                                    notreader.Add(readerid.Trim());
                                }
                            }

                            //�߼� ���� �׸��� ����Ʈ�� �߰�
                            ListViewItem item = noticeresultform.listView1.Items.Add(noticetime, "�ڼ���", null);
                            item.Tag = noticetime;
                            item.SubItems.Add(noticetime);
                            if (mode.Equals("e"))
                            {
                                item.SubItems.Add("���");
                            }
                            else
                            {
                                item.SubItems.Add("�Ϲ�");
                            }
                            item.SubItems.Add(title);
                            item.SubItems.Add(content);

                            //�߼� ���� �׸� ������ �� Ȯ�� ����Ʈ�� ����
                            NoticeDetailResultForm noticedetailresultform = new NoticeDetailResultForm();
                            noticedetailresultform.FormClosing += new FormClosingEventHandler(noticedetailresultform_FormClosing);
                            foreach (DictionaryEntry de in MemberInfoList)
                            {
                                string receiver = de.Value.ToString() + "(" + de.Key.ToString() + ")";
                                ListViewItem ditem = noticedetailresultform.listView1.Items.Add(de.Key.ToString(), receiver, null);

                                if (notreader.Contains(de.Key.ToString()))
                                {
                                    ditem.ForeColor = Color.Red;
                                    ListViewItem.ListViewSubItem subitem = ditem.SubItems.Add("Ȯ�ξ���");
                                }
                                else
                                {
                                    ditem.ForeColor = Color.Blue;
                                    ListViewItem.ListViewSubItem subitem = ditem.SubItems.Add("����");
                                }
                            }

                            NoticeDetailForm[noticetime] = noticedetailresultform;
                        }
                    }
                }
                else
                {
                    foreach (string strarr in tempMsg)
                    {
                        string[] itemarr = strarr.Split('��');

                        if (itemarr.Length > 3)
                        {
                            noticetime = itemarr[0];
                            content = itemarr[1];
                            mode = itemarr[2];
                            title = itemarr[3];
                            string[] readers = itemarr[4].Split(':');

                            foreach (string readerid in readers)
                            {
                                if (readerid.Trim().Length > 0)
                                {
                                    notreader.Add(readerid.Trim());
                                }
                            }

                            ListView.ListViewItemCollection collection = noticeresultform.listView1.Items;
                            bool isexist = false;

                            foreach (ListViewItem row in collection)
                            {
                                if (noticetime.Equals(row.Tag.ToString().Trim()))
                                {
                                    isexist = true;
                                    break;
                                }
                            }

                            if (isexist == false)
                            {
                                ListViewItem item = noticeresultform.listView1.Items.Add(noticetime, "�ڼ���", null);
                                item.Tag = noticetime;
                                item.SubItems.Add(noticetime);
                                item.SubItems.Add(mode);
                                item.SubItems.Add(title);
                                item.SubItems.Add(content);

                                NoticeDetailResultForm noticedetailresultform = new NoticeDetailResultForm();
                                noticedetailresultform.FormClosing += new FormClosingEventHandler(noticedetailresultform_FormClosing);
                                foreach (DictionaryEntry de in MemberInfoList)
                                {
                                    string receiver = de.Value.ToString() + "(" + de.Key.ToString() + ")";
                                    ListViewItem ditem = noticedetailresultform.listView1.Items.Add(de.Key.ToString(), receiver, null);

                                    if (notreader.Contains(de.Key.ToString()))
                                    {
                                        ditem.ForeColor = Color.Red;
                                        ListViewItem.ListViewSubItem subitem = ditem.SubItems.Add("Ȯ�ξ���");
                                    }
                                    else
                                    {
                                        ditem.ForeColor = Color.Blue;
                                        ListViewItem.ListViewSubItem subitem = ditem.SubItems.Add("����");
                                    }
                                }
                                NoticeDetailForm[noticetime] = noticedetailresultform;
                            }

                        }
                    }
                }

                noticeresultform.Show();
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void noticedetailresultform_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Form form = (Form)sender;
            form.Hide();
        }

        private void noticeresultform_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Form form = (Form)sender;
            form.Hide();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            try
            {
                ListView view = (ListView)sender;
                ListViewItem item = view.SelectedItems[0];
                string noticeid = item.Tag.ToString();

                if (MadeNoticeDetail != null)
                {
                    NoticeDetailResultForm form = (NoticeDetailResultForm)NoticeDetailForm[MadeNoticeDetail];
                    form.Hide();
                }
                if (NoticeDetailForm.Count != 0 && NoticeDetailForm.ContainsKey(noticeid) && NoticeDetailForm[noticeid] != null)
                {
                    NoticeDetailResultForm form = (NoticeDetailResultForm)NoticeDetailForm[noticeid];
                    form.Show();
                    MadeNoticeDetail = noticeid;
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void NoticeReaderAdd(string[] msg)//C|id|noticeid
        {
            try
            {
                if (NoticeDetailForm.ContainsKey(msg[2]) && NoticeDetailForm[msg[2]] != null)
                {
                    NoticeDetailResultForm form = (NoticeDetailResultForm)NoticeDetailForm[msg[2]];
                    ListViewItem[] itemArray = form.listView1.Items.Find(msg[1], false);
                    if (itemArray != null && itemArray.Length != 0)
                    {
                        itemArray[0].ForeColor = Color.Blue;
                        itemArray[0].SubItems[1].Text = "�� ��";
                    }
                }
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void MnMemo_Click(object sender, EventArgs e)
        {
            MakeSendMemo(new Hashtable());
        }

        private void BtnReceiver_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string key = null;

                int num = button.Parent.Controls.Count;

                for (int i = 0; i < num; i++)
                {
                    if ("formkey".Equals(button.Parent.Controls[i].Name))
                    {
                        key = button.Parent.Controls[i].Text;
                        break;
                    }
                }
                AddMemoReceiver(key);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void AddMemoReceiver(string formkey)
        {
            try
            {
                AddMemberForm addform = new AddMemberForm();
                addform.BtnConfirm.MouseClick += new MouseEventHandler(BtnConfirmForMemo_Click);
                addform.BtnCancel.MouseClick += new MouseEventHandler(BtnCancel_Click_forMemo);
                addform.radiobt_g.Click += new EventHandler(radiobt_g_Click);
                addform.radiobt_con.Click += new EventHandler(radiobt_con_Click);
                addform.radiobt_all.Click += new EventHandler(radiobt_all_Click);
                addform.combobox_team.SelectedValueChanged += new EventHandler(combobox_team_SelectedValueChangedAll);
                addform.CurrInListBox.MouseDoubleClick += new MouseEventHandler(CurrInListBox_MouseDoubleClick);

                SendMemoForm form = (SendMemoForm)MemoFormList[formkey];
                string[] receiverArray = null;
                if (form.txtbox_receiver.Text.Length != 0)
                {
                    receiverArray = form.txtbox_receiver.Text.Split(';');
                }
                if (receiverArray != null)
                {
                    foreach (string receiver in receiverArray)
                    {
                        if (receiver.Length > 2)  //������ ���� ���ڿ� ����
                        {
                            addform.AddListBox.Items.Add(receiver);
                        }
                    }
                }

                Hashtable all = GetAllMember();
                if (all != null)
                {
                    if (all.Count != 0)
                    {
                        foreach (DictionaryEntry de in all)
                        {
                            if (de.Value != null)
                            {
                                string item = (string)de.Value + "(" + (string)de.Key + ")";
                                if (!addform.AddListBox.Items.Contains(item))
                                {
                                    addform.CurrInListBox.Items.Add(item);
                                }
                            }
                        }
                    }
                }

                addform.formkey.Text = formkey;
                addform.Show(form);
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void CurrInListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListBox box = (ListBox)sender;
                string additem = null;

                if (box.SelectedItems.Count != 0)
                {
                    additem = box.SelectedItem.ToString();

                    int count = box.Parent.Controls.Count;

                    for (int i = 0; i < count; i++)
                    {
                        if (box.Parent.Controls[i].Name.Equals("AddListBox"))
                        {
                            ListBox addbox = (ListBox)box.Parent.Controls[i];

                            if (addbox.Items.Contains(additem))
                            {
                                MessageBox.Show(this, "�̹� ���õ� ����� �Դϴ�.", "�ߺ�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                addbox.Items.Add(additem);
                                box.Items.Remove(additem);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void BtnConfirmForMemo_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int controlsNum = button.Parent.Controls.Count;
                string key = null;
                for (int i = 0; i < controlsNum; i++)
                {
                    if ("formkey".Equals(button.Parent.Controls[i].Name))
                    {
                        Label keyvalue = (Label)button.Parent.Controls[i];
                        key = keyvalue.Text;
                        break;
                    }
                }
                SendMemoForm form = (SendMemoForm)MemoFormList[key];

                for (int i = 0; i < controlsNum; i++)
                {
                    if ("AddListBox".Equals(button.Parent.Controls[i].Name))
                    {
                        ListBox box = (ListBox)button.Parent.Controls[i];

                        if (box.Items.Count != 0)
                        {
                            if (form.txtbox_receiver.Text.Length != 0)
                            {
                                form.txtbox_receiver.Clear();
                            }
                            foreach (object obj in box.Items)
                            {
                                string str = (String)obj;
                                form.txtbox_receiver.AppendText(str + ";");
                            }
                        }
                        break;
                    }
                }
                button.Parent.Dispose();
                form.Activate();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }


        private void BtnSend_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button send = (Button)sender;
                bool MemoSendAvailable = false;
                TextBox receiverbox = null;

                for (int i = 0; i < send.Parent.Controls.Count; i++)
                {
                    string control = send.Parent.Controls[i].Name;
                    if (control.Equals("txtbox_receiver"))
                    {
                        receiverbox = (TextBox)send.Parent.Controls[i];
                        if (receiverbox.Text.Length != 0) MemoSendAvailable = true;
                    }
                }

                if (MemoSendAvailable == true)
                {
                    for (int i = 0; i < send.Parent.Controls.Count; i++)
                    {
                        string control = send.Parent.Controls[i].Name;
                        if (control.Equals("textBox1"))
                        {
                            TextBox memoContent = (TextBox)send.Parent.Controls[i];

                            if (memoContent.Text.Length != 0)
                            {
                                string msg = "19|" + this.myname + "|" + this.myid + "|" + memoContent.Text.Trim(); //m|name|�߽���id|message
                                string smsg = "4|" + this.myname + "|" + this.myid + "|" + memoContent.Text.Trim(); //m|name|�߽���id|message
                                logWrite("���� �޽��� ���� : " + msg);

                                string[] tempID = receiverbox.Text.Split(';');

                                for (int n = 0; n < tempID.Length; n++)
                                {
                                    if (tempID[n].Length != 0)
                                    {
                                        string[] array = tempID[n].Split('(');
                                        string[] array1 = array[1].Split(')');
                                        string reID = array1[0];
                                        logWrite("���� ������ ���̵� : " + reID);
                                        if (InList.ContainsKey(reID) && InList[reID] != null)
                                        {
                                            SendMsg(msg + "|" + reID, server);
                                        }
                                        else
                                        {
                                            SendMsg(smsg + "|" + reID, server);
                                        }
                                    }
                                }
                                send.Parent.Dispose();
                                break;


                            }
                            break;
                        }
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(send.Parent, "������ ���� ������ ������ �ּ���", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        string key = null;
                        for (int num = 0; num < send.Parent.Controls.Count; num++)
                        {
                            if (send.Parent.Controls[num].Name.Equals("formkey"))
                            {
                                Label keylabel = (Label)send.Parent.Controls[num];
                                key = keylabel.Text;
                                break;
                            }
                        }
                        AddMemoReceiver(key);
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void SendMemo(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox box = (TextBox)sender;
                if (e.KeyData == Keys.Enter || (e.Modifiers == Keys.Control && e.KeyCode == Keys.S))
                {

                    string msg = "19|" + this.myname + "|" + this.myid + "|" + box.Text.Trim();
                    string smsg = "4|" + this.myname + "|" + this.myid + "|" + box.Text.Trim();
                    for (int i = 0; i < box.Parent.Controls.Count; i++)
                    {
                        if (box.Parent.Controls[i].Name.Equals("senderid"))
                        {
                            Label idLabel = (Label)box.Parent.Controls[i];
                            if (InList.ContainsKey(idLabel.Text) && InList[idLabel.Text] != null)
                            {
                                IPEndPoint senderIP = (IPEndPoint)InList[idLabel.Text];
                                SendMsg(msg + "|" + idLabel.Text, server);
                            }
                            else SendMsg(smsg + "|" + idLabel.Text, server);
                            box.Parent.Dispose();
                            break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void MakeSendFileForm(Hashtable list)//key=id, value=name
        {
            try
            {
                SendFileForm sendform = new SendFileForm();
                sendform.formkey.Text = DateTime.Now.ToLongTimeString();
                sendform.btn_start.MouseClick += new MouseEventHandler(btn_start_Click);
                sendform.btn_cancel.MouseClick += new MouseEventHandler(btn_cancel_Click);
                sendform.btn_receivers.MouseClick += new MouseEventHandler(btn_receivers_Click);
                sendform.label_detail.MouseClick += new MouseEventHandler(label_detail_Click);
                sendform.btn_selectfile.MouseClick += new MouseEventHandler(btn_selectfile_Click);
                ToolTip tip = new ToolTip();
                tip.IsBalloon = true;
                tip.ToolTipIcon = ToolTipIcon.Info;
                tip.ToolTipTitle = "�޴»��";
                tip.SetToolTip(sendform.txtbox_FileReceiver, sendform.txtbox_FileReceiver.Text);
                FileSendFormList[sendform.formkey.Text] = sendform;

                bool isAll = false;
                if (list != null && list.Count != 0)
                {
                    foreach (DictionaryEntry de in list)
                    {
                        if (de.Value != null)
                        {
                            if (((string)de.Value).Equals("all"))
                            {
                                sendform.txtbox_FileReceiver.Text = "������ü;";
                                isAll = true;
                            }
                            else
                                sendform.txtbox_FileReceiver.Text += (string)de.Value + "(" + (string)de.Key + ");";
                        }
                        if (isAll == true) break;
                    }
                    sendform.Show();
                    sendform.Activate();
                }
                else
                {
                    sendform.Show();
                    sendform.Activate();
                }
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void btn_selectfile_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;

                int count = button.Parent.Controls.Count;
                string formkey = null;

                for (int i = 0; i < count; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("formkey"))
                    {
                        formkey = button.Parent.Controls[i].Text;
                        break;
                    }
                }
                SendFileForm form = (SendFileForm)FileSendFormList[formkey];

                ShowFileSelectDialog(form);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void ShowFileSelectDialog(SendFileForm sendform)
        {
            try
            {

                bool FileSelected = false;

                DialogResult result = openFileDialog.ShowDialog(sendform);
                string filename = null;
                if (result == DialogResult.OK)
                {

                    if (openFileDialog.FileName != null || openFileDialog.FileName.Length != 0)
                    {
                        filename = openFileDialog.FileName;
                        FileSelected = true;
                        sendform.Enabled = true;
                    }
                }
                else sendform.Enabled = true;

                if (FileSelected == true)
                {
                    string[] filenameArray = filename.Split('\\');
                    if (filenameArray.Length > 2)
                    {
                        sendform.label_filename.Text = filenameArray[0] + "\\..\\" + filenameArray[(filenameArray.Length - 1)];
                        sendform.label_filename.Tag = filename;
                    }
                    else sendform.label_filename.Text = filename;

                    ToolTip tip = new ToolTip();
                    tip.SetToolTip(sendform.label_filename, filename);

                    FileInfo fi = new FileInfo(filename);
                    int fsize = Convert.ToInt32(fi.Length / 1000);
                    if (fsize.ToString().Length > 3)
                    {
                        fsize = fsize / 1000;
                        sendform.label_filesize.Text = fsize + " MB (" + fi.Length.ToString() + " byte)";
                    }
                    else sendform.label_filesize.Text = fsize + " Kb (" + fi.Length.ToString() + " byte)";
                }
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void MnSendFile_Click(object sender, EventArgs e)
        {

            Hashtable list = new Hashtable();
            MakeSendFileForm(list);
        }

        private void StripMn_file_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = memTree.SelectedNode;
                Hashtable list = new Hashtable();

                if (node.GetNodeCount(false) != 0)
                {
                    TreeNodeCollection collection = node.Nodes;
                    foreach (TreeNode cnode in collection)
                    {
                        list[cnode.Tag] = cnode.Text;
                    }
                }
                else
                {
                    list[node.Tag] = node.Text;
                }

                MakeSendFileForm(list);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void label_detail_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Label detail = (Label)sender;
                int num = detail.Parent.Controls.Count;
                string key = null;
                for (int i = 0; i < num; i++)
                {
                    if ("formkey".Equals(detail.Parent.Controls[i].Name))
                    {
                        Label keylabel = (Label)detail.Parent.Controls[i];
                        key = keylabel.Text;
                        logWrite("label_detail_Click : formkey(" + key + ")");
                        break;
                    }
                }
                FileSendDetailListView view = (FileSendDetailListView)FileSendDetailList[key];
                view.Show();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btn_receivers_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int num = button.Parent.Controls.Count;
                string key = null;
                for (int i = 0; i < num; i++)
                {
                    if ("formkey".Equals(button.Parent.Controls[i].Name))
                    {
                        Label keylabel = (Label)button.Parent.Controls[i];
                        key = keylabel.Text;
                        logWrite("btn_receivers_Click : formkey(" + key + ")");
                        break;
                    }
                }
                AddFileReceiver(key);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btn_cancel_Click(object sender, MouseEventArgs e)
        {
            try
            {
                logWrite("btn_cancel_Click");
                Button button = (Button)sender;
                int num = button.Parent.Controls.Count;
                string key = null;
                for (int i = 0; i < num; i++)
                {
                    if ("formkey".Equals(button.Parent.Controls[i].Name))
                    {
                        Label keylabel = (Label)button.Parent.Controls[i];
                        key = keylabel.Text;

                        if (FileSendThreadList.ContainsKey(key) && FileSendThreadList[key] != null)
                        {
                            try
                            {
                                ((Thread)FileSendThreadList[key]).Abort();
                            }
                            catch (ThreadAbortException te)
                            {
                                logWrite(te.ToString());
                            }
                        }
                        if (FileSendFormList.ContainsKey(key) && FileSendFormList[key] != null)
                        {
                            FileSendFormList.Remove(key);
                            logWrite("FileSendFormList.Remove(key) :" + key);
                        }
                        if (FileSendDetailList.ContainsKey(key) && FileSendDetailList[key] != null)
                        {
                            FileSendDetailListView view = (FileSendDetailListView)FileSendDetailList[key];
                            view.Dispose();
                            FileSendDetailList.Remove(key);
                            logWrite("FileSendDetailList.Remove(key) :" + key);
                        }
                        break;
                    }
                }
                button.Parent.Dispose();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btn_start_Click(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int num = button.Parent.Controls.Count;
                SendFileForm form = null;
                string key = null;
                string filename = null;

                for (int i = 0; i < num; i++)
                {
                    if ("formkey".Equals(button.Parent.Controls[i].Name))
                    {
                        Label keylabel = (Label)button.Parent.Controls[i];
                        key = keylabel.Text;
                        logWrite("btn_start_Click : formkey �� ����(" + key + ")");
                        if (FileSendFormList.ContainsKey(key) && FileSendFormList[key] != null)
                        {
                            form = (SendFileForm)FileSendFormList[key];
                            logWrite("key���� ���� Form ã��! ");
                        }
                        else logWrite("Ű���� ���� SendFileForm ����! key=" + key);
                        break;
                    }
                }

                if (form.label_filename.Text.Length == 0)
                {
                    logWrite("btn_start_Click : ������ ������ ����");
                    DialogResult result = MessageBox.Show(form, "������ ������ ������ �ּ���", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    bool FileSelected = false;

                    if (result == DialogResult.OK)
                    {
                        DialogResult fresult = openFileDialog.ShowDialog(form);
                        if (fresult == DialogResult.OK)
                        {
                            if (openFileDialog.FileName.Length != 0)
                            {
                                filename = openFileDialog.FileName;
                                FileSelected = true;
                            }
                        }
                        if (FileSelected == true)
                        {
                            string[] filenameArray = filename.Split('\\');
                            if (filenameArray.Length > 2)
                            {
                                form.label_filename.Text = filenameArray[0] + "\\...\\" + filenameArray[(filenameArray.Length - 1)];
                                form.label_filename.Tag = filename;
                                logWrite(" btn_start_Click()  form.label_filename.Tag = " + form.label_filename.Tag.ToString());
                            }
                            else form.label_filename.Text = filename;

                            ToolTip tip = new ToolTip();
                            tip.SetToolTip(form.label_filename, filename);

                            FileInfo fi = new FileInfo(filename);
                            int fsize = Convert.ToInt32(fi.Length / 1000);
                            if (fsize.ToString().Length > 3)
                            {
                                fsize = fsize / 1000;
                                form.label_filesize.Text = fsize + " MB (" + fi.Length.ToString() + " byte)";
                            }
                            else form.label_filesize.Text = fsize + " Kb (" + fi.Length.ToString() + " byte)";
                        }
                    }
                }
                else if (form.txtbox_FileReceiver.Text.Length == 0)
                {
                    logWrite("btn_start_Click : �޴»���� ����");
                    DialogResult result = MessageBox.Show(form, "������ �޴� ����� �����ϴ�. �߰��� �ּ���.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        AddFileReceiver(form.formkey.Text);
                    }
                }
                else
                {
                    logWrite("btn_start_Click : ������ ���ϰ� ������� üũ �Ϸ�");
                    button.Visible = false;
                    form.label_result.Text = "���� �����";
                    string tempname = null;
                    string tempid = null;
                    ArrayList list = new ArrayList();
                    string[] receiverArray = form.txtbox_FileReceiver.Text.Split(';');

                    //���� ���� �ڼ��� ���� ����
                    FileSendDetailListView view = new FileSendDetailListView();
                    view.FormClosing += new FormClosingEventHandler(FileSendDetailListView_FormClosing);

                    foreach (string receiver in receiverArray)
                    {
                        if (receiver.Length != 0)
                        {
                            if (receiver.Equals("������ü"))
                            {
                                list.Add("all");
                            }
                            else
                            {
                                string[] receiverArg = receiver.Split('(');
                                tempname = receiverArg[0];
                                string[] receiverArg1 = receiverArg[1].Split(')');
                                tempid = receiverArg1[0];
                                list.Add(tempid);
                            }

                            ListViewItem item = view.listView.Items.Add(tempid, receiver, null);
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                        }
                    }
                    FileSendDetailList[form.formkey.Text] = view;
                    form.label_detail.Visible = true;
                    if (list.Count > 1)
                    {
                        view.Show();
                    }
                    FileSendRequest(list, form.label_filename.Tag.ToString(), form.formkey.Text);
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void FileSendDetailListView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                Form form = (Form)sender;
                form.Hide();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void AddFileReceiver(string formkey)
        {
            try
            {
                AddMemberForm addform = new AddMemberForm();
                addform.BtnConfirm.Click += new EventHandler(BtnConfirmForFile_Click);
                addform.BtnCancel.Click += new EventHandler(BtnCancel_Click_forFile);
                addform.radiobt_g.Click += new EventHandler(radiobt_g_Click);
                addform.radiobt_con.Click += new EventHandler(radiobt_con_Click);
                addform.radiobt_all.Click += new EventHandler(radiobt_all_Click);
                addform.combobox_team.SelectedValueChanged += new EventHandler(combobox_team_SelectedValueChangedAll);
                addform.CurrInListBox.MouseDoubleClick += new MouseEventHandler(CurrInListBox_MouseDoubleClick);

                GetAllMemberDelegate getAll = new GetAllMemberDelegate(GetAllMember);
                Hashtable all = (Hashtable)Invoke(getAll, null);
                if (all != null)
                {
                    if (all.Count != 0)
                    {
                        foreach (DictionaryEntry de in all)
                        {
                            if (de.Value != null)
                            {
                                string item = (string)de.Value + "(" + (string)de.Key + ")";
                                addform.CurrInListBox.Items.Add(item);
                            }
                        }
                    }
                }

                SendFileForm form = (SendFileForm)FileSendFormList[formkey];
                string[] receiverArray = null;
                if (form.txtbox_FileReceiver.Text.Length != 0)
                {
                    receiverArray = form.txtbox_FileReceiver.Text.Split(';');
                }
                if (receiverArray != null)
                {
                    foreach (string receiver in receiverArray)
                    {
                        if (receiver.Length > 2)  //������ ���� ���ڿ� ����
                        {
                            addform.AddListBox.Items.Add(receiver);
                        }
                    }
                }
                addform.formkey.Text = formkey;
                addform.Show(form);

            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void BtnConfirmForFile_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox button = (PictureBox)sender;
                int controlsNum = button.Parent.Controls.Count;
                string key = null;
                for (int i = 0; i < controlsNum; i++)
                {
                    if ("formkey".Equals(button.Parent.Controls[i].Name))
                    {
                        Label keyvalue = (Label)button.Parent.Controls[i];
                        key = keyvalue.Text;
                        break;
                    }
                }
                SendFileForm form = (SendFileForm)FileSendFormList[key];

                for (int i = 0; i < controlsNum; i++)
                {
                    if ("AddListBox".Equals(button.Parent.Controls[i].Name))
                    {
                        ListBox box = (ListBox)button.Parent.Controls[i];

                        if (box.Items.Count != 0)
                        {
                            if (form.txtbox_FileReceiver.Text.Length != 0)
                            {
                                form.txtbox_FileReceiver.Clear();
                            }
                            foreach (object obj in box.Items)
                            {
                                string str = (String)obj;
                                if (str.Length != 0)
                                {
                                    form.txtbox_FileReceiver.AppendText(str + ";");
                                }
                            }
                        }
                        break;
                    }
                }
                button.Parent.Dispose();
                form.Activate();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void StripMn_gfile_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable list = new Hashtable();
                if (memTree.SelectedNode.GetNodeCount(false) != 0)
                {
                    if (memTree.SelectedNode.Level == 0)
                    {
                        DialogResult result = MessageBox.Show(this, "����� ��ο��� ������ �����ðڽ��ϱ�?", "�˸�", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            list["all"] = "all";
                        }
                    }
                    else
                    {
                        TreeNodeCollection collection = memTree.SelectedNode.Nodes;
                        foreach (TreeNode node in collection)
                        {
                            list[node.Text] = node.Tag;
                        }
                    }
                }
                MakeSendFileForm(list);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }


        private void mnuSetServer_Click(object sender, System.EventArgs e)
        {
            try
            {



            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
            //MessageBox.Show("���� �غ��� �Դϴ�.", "�˸�", MessageBoxButtons.OK);
        }

        private void pbx_confirm_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (configform.cbx_autostart.CheckState == CheckState.Checked)
                {
                    setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "autostart", "1");
                    System.Configuration.ConfigurationSettings.AppSettings.Set("autostart", "1");
                    RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    rkApp.SetValue("WeDo", Application.ExecutablePath.ToString());
                    rkApp.Close();
                }
                else
                {
                    setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "autostart", "0");
                    System.Configuration.ConfigurationSettings.AppSettings.Set("autostart", "0");
                    RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (rkApp.GetValue("WeDo") != null)
                    {
                        rkApp.DeleteValue("WeDo");
                    }
                    rkApp.Close();
                }

                if (configform.cbx_topmost.CheckState == CheckState.Checked)
                {
                    this.TopMost = true;
                    setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "topmost", "1");
                    System.Configuration.ConfigurationSettings.AppSettings.Set("topmost", "1");
                }
                else
                {
                    this.TopMost = false;
                    setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "topmost", "0");
                    System.Configuration.ConfigurationSettings.AppSettings.Set("topmost", "0");
                }

                if (configform.cbx_nopop.CheckState == CheckState.Checked)
                {
                    this.nopop = true;
                    setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "nopop", "1");
                    System.Configuration.ConfigurationSettings.AppSettings.Set("nopop", "1");
                }
                else
                {
                    this.nopop = false;
                    setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "nopop", "0");
                    System.Configuration.ConfigurationSettings.AppSettings.Set("nopop", "0");
                }

                configform.Close();
                configform = null;
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void cbx_topmost_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void cbx_autostart_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void btnSetting_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                logWrite("Set server ip ����!");
                string tempip = "";
                if (setserverform.rbt_ip.Checked == true)
                {
                    tempip = setserverform.tbx_ip1.Text + "." + setserverform.tbx_ip2.Text + "." + setserverform.tbx_ip3.Text + "." + setserverform.tbx_ip4.Text;
                }
                else
                {
                    tempip = "localhost";
                }
                setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "serverip", tempip);
                setCRM_DB_HOST("c:\\MiniCTI\\config\\MiniCTI_config.xml", tempip);
                setCRM_DB_HOST(Application.StartupPath + "\\MiniCTI_config.xml", tempip);
                serverIP = tempip;
                System.Configuration.ConfigurationSettings.AppSettings.Set("serverip", serverIP);

                setserverform.Close();
                setserverform = null;

            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }



        private void btnSetting_Click(object sender, System.EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                SetServer_Form form = (SetServer_Form)button.Parent;
                int count = button.Parent.Controls.Count;
                for (int i = 0; i < count; i++)
                {
                    if ("tb_Address".Equals(button.Parent.Controls[i].Name))
                    {
                        serverIP = button.Parent.Controls[i].Text;

                        break;
                    }
                }
                form.Dispose();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        public void LogFileCheck()
        {
            try
            {
                if (!di.Exists)
                {
                    di.Create();
                }

                DirectoryInfo logDir = new DirectoryInfo(di.FullName + "\\log");
                if (!logDir.Exists)
                {
                    logDir.Create();
                }

                DirectoryInfo sampleDir = new DirectoryInfo(di.FullName + "\\sample");
                if (!sampleDir.Exists)
                {
                    sampleDir.Create();
                }

                DirectoryInfo configDir = new DirectoryInfo(di.FullName + "\\config");
                if (!configDir.Exists)
                {
                    configDir.Create();
                }

                DirectoryInfo updaterDir = new DirectoryInfo(di.FullName + "\\WeDoUpdater");
                if (!updaterDir.Exists)
                {
                    updaterDir.Create();
                }

                FileInfo[] files = null;
                di = new DirectoryInfo(Application.StartupPath + "\\WeDoUpdater");
                if (di.Exists)
                {
                    files = di.GetFiles();
                    foreach (FileInfo fi in files)
                    {
                        FileInfo finfo = new FileInfo("C:\\MiniCTI\\WeDoUpdater\\" + fi.Name);

                        fi.CopyTo(finfo.FullName, true);

                    }
                }
                else
                {
                    logWrite("WeDoUpdater.Exists = false");
                }

                date = date.Trim();
                FileInfo tempfileinfo = new FileInfo(@"C:\MiniCTI\log\" + date + ".txt");

                if (!tempfileinfo.Exists)
                {
                    tempfileinfo.Create();
                    logWrite(date + ".txt ���� ����");
                }

                FileInfo temp = new FileInfo(Application.StartupPath + "\\MiniCTI_config.xml");

                FileInfo CRMCFGfileinfo = new FileInfo("C:\\MiniCTI\\config\\MiniCTI_config.xml");
                if (!CRMCFGfileinfo.Exists)
                {
                    logWrite("MiniCTI config ���� ����");
                    FileInfo file_copied = temp.CopyTo(CRMCFGfileinfo.FullName);
                }


            }
            catch (Exception e)
            {
                logWrite(e.ToString());

            }
        }

        private void changeDBHost()
        {

        }

        private void MemoFileCheck()
        {
            privatefolder = new DirectoryInfo(@"C:\MiniCTI\" + this.myid);
            memodi = new DirectoryInfo(@"C:\MiniCTI\" + this.myid + "\\Memo");
            try
            {
                if (!privatefolder.Exists)
                {
                    privatefolder.Create();
                    logWrite("���� ���� ���� : " + this.myid);
                }
                if (!memodi.Exists)
                {
                    memodi.Create();
                    logWrite("Memo ���� ���� ����");
                }
                string today = DateTime.Now.ToShortDateString();
                FileInfo memofile = new FileInfo(@"C:\MiniCTI\" + this.myid + "\\Memo\\" + today + ".mem");
                if (!memofile.Exists)
                {
                    memofile.Create();
                    logWrite(today + ".mem ���� ����");
                }
            }
            catch (Exception e) { };
        }

        private void FileDirCheck()
        {
            try
            {
                privatefolder = new DirectoryInfo(@"C:\MiniCTI\" + id.Text);
                FilesDir = new DirectoryInfo(@"C:\MiniCTI\" + id.Text + "\\Files");
                if (!privatefolder.Exists)
                {
                    privatefolder.Create();
                    logWrite("���� ���� ���� : " + id.Text);
                }

                if (!FilesDir.Exists)
                {
                    FilesDir.Create();
                    logWrite("��ȭ ���� ���� ����");
                }
            }
            catch (Exception e) { };
        }

        private void DialogFileCheck()
        {
            try
            {
                privatefolder = new DirectoryInfo(@"C:\MiniCTI\" + id.Text);
                dialogdi = new DirectoryInfo(@"C:\MiniCTI\" + id.Text + "\\Dialog");
                if (!privatefolder.Exists)
                {
                    privatefolder.Create();
                    logWrite("���� ���� ���� : " + id.Text);
                }

                if (!dialogdi.Exists)
                {
                    dialogdi.Create();
                    logWrite("��ȭ ���� ���� ����");
                }

                string today = DateTime.Now.ToShortDateString();
                string month = today.Substring(0, 7);

                MonthFolder = new DirectoryInfo(@"C:\MiniCTI\" + id.Text + "\\Dialog\\" + month);
                if (!MonthFolder.Exists)
                {
                    MonthFolder.Create();
                    logWrite(month + " ���� ����");
                }

                DayFolder = new DirectoryInfo(@"C:\MiniCTI\" + id.Text + "\\Dialog\\" + month + "\\" + today);
                if (!DayFolder.Exists)
                {
                    DayFolder.Create();
                    logWrite(today + " ���� ����");
                }
            }
            catch (Exception e) { };
        }

        public void logWrite(string clog)
        {
            try
            {
                clog += "( " + DateTime.Now.ToString() + ")" + "\r\n";

                if (Log.logBox.InvokeRequired)
                {
                    WriteLog writelog = new WriteLog(Log.logBox.AppendText);

                    Invoke(writelog, clog);
                }
                else Log.logBox.AppendText(clog);

                logFileWrite(clog);
            }
            catch (Exception e)
            {

            }
        }

        public void logFileWrite(string _log)
        {
            StreamWriter sw = null;
            fileInfo = new FileInfo(@"C:\MiniCTI\log\" + date + ".txt");
            if (!fileInfo.Exists)
            {
                fileInfo.Create();
            }

            try
            {
                sw = new StreamWriter(@"C:\MiniCTI\log\" + date + ".txt", true);
                sw.WriteLine(_log);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("logFileWriter() ���� : " + e.ToString());
            }
        }

        private void MemoFileWrite(string memo)
        {
            StreamWriter sw = null;
            try
            {
                string today = DateTime.Now.ToShortDateString();
                FileInfo memofile = new FileInfo(@"C:\MiniCTI\" + this.myid + "\\Memo\\" + today + ".mem");
                if (!memofile.Exists)
                {
                    MemoFileCheck();
                }
                sw = memofile.AppendText();
                sw.WriteLine(memo);
                sw.Close();
                logWrite("�޸�����");
            }
            catch (Exception e)
            {
                Console.WriteLine("logFileWriter() ���� : " + e.ToString());
            }
        }

        private ArrayList MemoFileRead()
        {
            StreamReader sr = null;
            ArrayList list = new ArrayList(); ;
            try
            {
                string today = DateTime.Now.ToShortDateString();
                DirectoryInfo info = new DirectoryInfo(@"C:\MiniCTI\" + this.myid + "\\Memo");
                if (!info.Exists)
                {
                    MemoFileCheck();
                }
                else
                {
                    FileInfo[] files = info.GetFiles("*.mem");
                    foreach (FileInfo file in files)
                    {
                        sr = new StreamReader(file.FullName);
                        while (!sr.EndOfStream)
                        {
                            string memoitem = sr.ReadLine();
                            list.Add(memoitem);
                        }
                        sr.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
            return list;
        }

        private void DialogFileWrite(string dialogkey, string dialog, string person)
        {
            try
            {
                string[] array = dialogkey.Split('!');
                StreamWriter sw = null;
                if (!dialogdi.Exists || MonthFolder == null || DayFolder == null)
                {
                    DialogFileCheck();
                }
                else if (!MonthFolder.Exists || !DayFolder.Exists)
                {
                    DialogFileCheck();
                }
                string today = DateTime.Now.ToShortDateString();
                string now = DateTime.Now.Hour.ToString() + "��_" + DateTime.Now.Minute.ToString() + "��_" + DateTime.Now.Second.ToString() + "��";
                string dkey = now + "!" + person;
                string month = today.Substring(0, 7);
                logWrite("DialogFileWrite dkey = " + dkey);
                FileInfo path = new FileInfo("C:\\MiniCTI\\" + this.myid + "\\Dialog\\" + month + "\\" + today + "\\" + dkey + ".dlg");
                sw = new StreamWriter(path.FullName, false);
                try
                {
                    sw.Write(dialog);
                    sw.Flush();
                    sw.Close();
                    logWrite("��ȭ����");
                }
                catch (Exception e)
                {
                    Console.WriteLine("logFileWriter() ���� : " + e.ToString());
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        /// <summary>
        /// �޽��� Ŭ���̾�Ʈ ���ҽ� ����
        /// </summary>
        private void closing()
        {
            if (receiverStart == true)
            {
                if (serverAlive == true)
                    connected = false;

                try
                {
                    if (listenSock != null)
                    {
                        listenSock.Close();
                        logWrite("listenSock ����");
                    }

                    if (sendSock != null)
                    {
                        sendSock.Close();
                        logWrite("sendSock ����");
                    }

                    if (filesock != null)
                    {
                        filesock.Close();
                        logWrite("filesock ����");
                    }

                    if (filesendSock != null)
                    {
                        filesendSock.Close();
                        logWrite("filesendSock ����");
                    }

                    if (FileReceiverThreadList.Count != 0)
                    {
                        foreach (DictionaryEntry de in FileReceiverThreadList)
                        {
                            if (de.Value != null)
                            {
                                ((Thread)de.Value).Abort();
                            }
                        }
                        logWrite("FileReceiverThreadList clear!");
                    }

                    if (FileSendThreadList.Count != 0)
                    {
                        foreach (DictionaryEntry de in FileSendThreadList)
                        {
                            if (de.Value != null)
                            {
                                ((Thread)de.Value).Abort();
                            }
                        }
                    }
                    if (receive != null)
                    {
                        if (receive.IsAlive == true)
                        {
                            receive.Abort();
                            receiverStart = false;
                            logFileWrite("receive Thread ����!");
                        }
                    }
                    else
                    {
                        logWrite("receive Thread is null");
                    }

                    if (serverAlive == true)
                    {
                        if (checkThread != null)
                        {
                            if (checkThread.IsAlive == true)
                            {
                                checkThread.Abort();
                                logFileWrite("checkThread Thread ����!");
                                checkThread = null;
                            }
                        }
                        else
                        {
                            logFileWrite("checkThread is null");
                        }
                    }
                    else
                    {
                        logFileWrite("serverAlive is false");
                    }
                }
                catch (ThreadAbortException ex)
                {
                    logFileWrite("closing() ���� : " + ex.ToString());
                }
                catch (SocketException ex)
                {
                    logFileWrite("closing() ���� : " + ex.ToString());
                }
            }
        }

        private void LogOut()
        {
            try
            {
                if (connected == true)
                {
                    string logout = "9|" + this.myid;
                    SendMsg(logout, server);

                    //�����ִ� ��ȭâ �� ������ Ȯ�� �� ����
                    LogOutDelegate logoutFormClose = new LogOutDelegate(LogoutFormClose);
                    Invoke(logoutFormClose, null);

                    if (MemoTable.Count != 0)
                    {
                        foreach (MemoForm form in MemoTable)
                        {
                            form.Dispose();
                        }
                        MemoTable.Clear();
                    }
                    memTree.Nodes.Clear();
                    closing();
                    ButtonCtrl(false);
                    LogInPanelVisible(true);
                    defaultCtrl(true);
                    if (cbx_pass_save.Checked == false)
                    {
                        tbx_pass.Text = "";
                    }
                    label_stat.Text = "�¶��� ��";
                    id.Focus();
                    id.SelectAll();
                    if (serverAlive == true)
                    {
                        connected = false;
                    }
                }
                else
                {
                    logWrite("connected ==false");
                }
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        public void LogoutFormClose()  //�α׾ƿ� ���� ���� �� �ݱ� �� �������̺� ����
        {
            //private Hashtable ChatFormList = new Hashtable();  //ä��â  key=id, value=chatform
            //private Hashtable MemoFormList = new Hashtable(); //key=time, value=SendMemoForm
            //private Hashtable TeamInfoList = new Hashtable(); //key=id, value=team
            //private Hashtable InList = new Hashtable();       //key=id, value=IPEndPoint
            //private Hashtable MemberInfoList = new Hashtable();
            //private Hashtable FileSendDetailList = new Hashtable();
            //private Hashtable FileSendFormList = new Hashtable();
            //private Hashtable FileSendThreadList = new Hashtable();
            //private Hashtable FileReceiverThreadList = new Hashtable();
            //private Hashtable NoticeDetailForm = new Hashtable();
            //private Hashtable treesource = new Hashtable();
            //private ArrayList MemoTable = new ArrayList();
            //private ArrayList omitteamlist = new ArrayList();
            try
            {
                if (noticelistform != null)
                {
                    noticelistform.Close();
                    noticelistform = null;
                }

                if (noticeresultform != null)
                {
                    noticeresultform.Close();
                    noticeresultform = null;
                }

                if (noreceiveboardform != null)
                {
                    noreceiveboardform.Close();
                    noreceiveboardform = null;
                }

                if (ChatFormList.Count != 0)
                {
                    foreach (DictionaryEntry de in ChatFormList)
                    {
                        if (de.Value != null)
                        {
                            try
                            {
                                ChatForm form = (ChatForm)de.Value;
                                form.Dispose();
                            }
                            catch (Exception e)
                            {
                                logWrite("form.Dispose() ���� : " + e.ToString());
                            }
                        }
                    }
                    ChatFormList.Clear();
                    logWrite("ChatFormList Clear!");
                }

                if (MemoFormList.Count != 0)
                {
                    foreach (DictionaryEntry de in MemoFormList)
                    {
                        if (de.Value != null)
                        {
                            try
                            {
                                SendMemoForm form = (SendMemoForm)de.Value;
                                form.Dispose();
                            }
                            catch (Exception e)
                            {
                                logWrite("form.Dispose() ���� : " + e.ToString());
                            }
                        }
                    }
                    MemoFormList.Clear();
                    logWrite("MemoFormList Clear!");
                }

                TeamInfoList.Clear();
                InList.Clear();
                MemberInfoList.Clear();
                FileSendDetailList.Clear();

                if (FileSendFormList.Count != 0)
                {
                    foreach (DictionaryEntry de in FileSendFormList)
                    {
                        if (de.Value != null)
                        {
                            try
                            {
                                SendFileForm form = (SendFileForm)de.Value;
                                form.Dispose();
                            }
                            catch (Exception e)
                            {
                                logWrite("form.Dispose() ���� : " + e.ToString());
                            }
                        }
                    }
                    FileSendFormList.Clear();
                    logWrite("FileSendFormList Clear!");
                }

                FileSendThreadList.Clear();
                FileReceiverThreadList.Clear();

                if (NoticeDetailForm.Count != 0)
                {
                    foreach (DictionaryEntry de in NoticeDetailForm)
                    {
                        if (de.Value != null)
                        {
                            try
                            {
                                NoticeDetailResultForm form = (NoticeDetailResultForm)de.Value;
                                form.Dispose();
                            }
                            catch (Exception e)
                            {
                                logWrite("form.Dispose() ���� : " + e.ToString());
                            }
                        }
                    }
                    NoticeDetailForm.Clear();
                    logWrite("NoticeDetailForm Clear!");
                }

                treesource.Clear();

                if (MemoTable.Count != 0)
                {
                    foreach (MemoForm memoform in MemoTable)
                    {
                        memoform.Dispose();
                    }
                    MemoTable.Clear();
                    logWrite("MemoTable Clear!");
                }

                omitteamlist.Clear();
            }
            catch (Exception e)
            {
                logWrite(e.ToString());
            }
        }

        private void Btnlogout_Click(object sender, EventArgs e)
        {
            logWrite("Btnlogout_Click !");
            try
            {
                if (ChatFormList.Count != 0)
                {
                    foreach (DictionaryEntry de in ChatFormList)
                    {
                        if (de.Value != null)
                        {
                            DialogResult result = MessageBox.Show(this, "�α׾ƿ��ϸ� ���� �����ִ� ���� ��� �����ϴ�.", "�˸�", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            if (result == DialogResult.OK)
                            {
                                LogOut();
                            }
                            break;
                        }
                    }
                }
                else LogOut();
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }



        private void StripMn_memo_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable MemoReceiver = new Hashtable();

                if (memTree.SelectedNode.Text.Contains("("))
                {
                    string[] nameArg = memTree.SelectedNode.Text.Split('(');
                    MemoReceiver[nameArg[0]] = memTree.SelectedNode.Tag;
                }
                else
                {
                    MemoReceiver[memTree.SelectedNode.Text] = memTree.SelectedNode.Tag;
                }
                MakeSendMemo(MemoReceiver);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void MnExit_Click(object sender, EventArgs e)
        {
            QuitMsg();
        }

        private void QuitMsg()
        {
            bool isOk = true;
            if (connected == true)
            {
                this.notifyIcon.Visible = false;
                LogOut();
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                this.notifyIcon.Visible = false;
                closing();
                Process.GetCurrentProcess().Kill();
            }
        }

        private void StripMn_gmemo_Click(object sender, EventArgs e)
        {
            try
            {
                if (memTree.SelectedNode.GetNodeCount(false) != 0)
                {
                    if (memTree.SelectedNode.Level == 0)
                    {

                    }
                    else
                    {
                        Hashtable MemoReceiver = new Hashtable();
                        TreeNodeCollection collection = memTree.SelectedNode.Nodes;
                        foreach (TreeNode node in collection)
                        {
                            string[] nameArg = null;
                            if (node.Text.Contains("("))
                            {
                                nameArg = node.Text.Split('(');
                                MemoReceiver[nameArg[0]] = node.Tag;
                            }
                            else
                                MemoReceiver[node.Text] = node.Tag;
                        }
                        MakeSendMemo(MemoReceiver);
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void NRmemo_Click(object sender, EventArgs e)
        {

        }

        private void NRfile_Click(object sender, EventArgs e)
        {

        }

        private void NRnotice_Click(object sender, EventArgs e)
        {

        }

        private void MnNotice_Click(object sender, EventArgs e)
        {
            MakeSendNotice();
        }

        private void MnNoticeShow_Click(object sender, EventArgs e)
        {

        }

        private void pic_noticeresult_Click(object sender, EventArgs e)
        {

        }

        //private void pic_connector_Click(object sender, EventArgs e)
        //{

        //    if (cbox_connector.CheckState == CheckState.Unchecked)
        //    {
        //        cbox_connector.CheckState = CheckState.Checked;
        //    }
        //    else
        //    {
        //        cbox_connector.CheckState = CheckState.Unchecked;
        //    }
        //}

        private void pic_noticelist_Click(object sender, EventArgs e)
        {
            SendMsg("1|" + this.myid, server);
        }

        /// <summary>
        /// ������ ����Ʈ ����
        /// </summary>
        /// <param name="msg"></param>
        private void MakeNoticeListForm(string[] msg) //L|time��content��mode��sender��seqnum��title|...
        {
            try
            {

                {
                    if (noticelistform == null)
                    {
                        noticelistform = new NoticeListForm();
                    }
                    else
                    {
                        noticelistform.Close();
                        noticelistform = new NoticeListForm();
                    }
                    noticelistform.listView.SelectedIndexChanged += new EventHandler(listView_Click);
                    noticelistform.KeyDown += new KeyEventHandler(noticelistform_KeyDown);
                    noticelistform.listView.KeyDown += new KeyEventHandler(noticelistform_KeyDown);
                    noticelistform.btn_del.MouseClick += new MouseEventHandler(btn_del_Click);

                    noticelistform.listView.CheckBoxes = true;
                    noticelistform.btn_del.Visible = true;
                    noticelistform.cancel.Visible = true;
                    noticelistform.btn_all.Visible = true;


                    foreach (string item in msg)
                    {
                        try
                        {
                            if (!item.Equals("L") && item.Length != 0)
                            {

                                string[] arr = null;

                                if (item.Split('��').Length > 4)// '|' -> shift+\ 
                                {
                                    arr = item.Split('��');  //time��content��mode��sender��seqnum��title|...
                                }

                                ListViewItem listitem = null;

                                if (arr != null && arr.Length > 4)
                                {
                                    logWrite("notice_time = " + arr[0]);

                                    if (arr[2].Equals("n"))
                                    {
                                        listitem = noticelistform.listView.Items.Add(arr[0], "�Ϲ�", null);
                                    }
                                    else
                                    {
                                        listitem = noticelistform.listView.Items.Add(arr[0], "���", null);
                                        listitem.ForeColor = Color.Red;
                                    }


                                    string sender = "";
                                    if (arr[3] != null)
                                    {
                                        sender = getName(arr[3].Trim());

                                        if (arr[1].Contains("\n\r\n\r"))
                                        {
                                            logWrite("ã��");
                                            arr[1].Replace("��", " ");
                                        }
                                    }

                                    listitem.SubItems.Add(arr[5].Trim());
                                    listitem.SubItems.Add(arr[1].Trim());
                                    listitem.SubItems.Add(sender + "(" + arr[3].Trim() + ")");
                                    listitem.SubItems.Add(arr[0]);
                                    listitem.Tag = arr[4];
                                    logWrite("seqnum = " + arr[4]);
                                    noticelistform.listView.ListViewItemSorter = new ListViewItemComparerDe(3);

                                }
                            }
                        }
                        catch (Exception ex1)
                        {
                            logWrite(ex1.ToString());
                        }
                    }
                }

                noticelistform.Show();
                isNoticeListAll = true;
            }
            catch (Exception e)
            {
                logWrite("MakeNoticeListForm() " + e.StackTrace.ToString());
            }
        }

        private void cancel_MouseClick(object sender, MouseEventArgs e)
        {
            NoParamDele dele = new NoParamDele(selectcancelforNoticeList);
            Invoke(dele);
        }

        private void btn_all_MouseClickForNoticeList(object sender, MouseEventArgs e)
        {
            NoParamDele dele = new NoParamDele(selectAllforNoticeList);
            Invoke(dele);
        }

        private void selectAllforNoticeList()
        {
            ListView.ListViewItemCollection collection = noticelistform.listView.Items;
            foreach (ListViewItem item in collection)
            {
                item.Checked = true;
            }
        }

        private void selectcancelforNoticeList()
        {
            ListView.ListViewItemCollection collection = noticelistform.listView.Items;
            foreach (ListViewItem item in collection)
            {
                item.Checked = false;
            }
        }


        /// <summary>
        /// �������� ��� ���õ� �������� ������ư Ŭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_del_Click(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            int count = button.Parent.Controls.Count;
            ListView view = null;
            for (int i = 0; i < count; i++)
            {
                if (button.Parent.Controls[i].Name.Equals("listView"))
                {
                    view = (ListView)button.Parent.Controls[i];
                }
            }

            string delnotices = "15|";
            if (view.CheckedItems.Count == 0)
            {
                MessageBox.Show(view.Parent, "������ ������ ������ �ּ���", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ListView.CheckedListViewItemCollection col = view.CheckedItems;
                foreach (ListViewItem item in col)
                {
                    logWrite(item.Tag.ToString());
                    delnotices += item.Tag.ToString() + ";";
                    view.Items.RemoveAt(item.Index);
                }
                SendMsg(delnotices, server);

            }
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            NoticeListSorting sorting = new NoticeListSorting(Noticelistform_Sorting);
            Invoke(sorting, new object[] { e.Column });
        }

        private void Noticelistform_Sorting(int columnindex)
        {
            ListView.ListViewItemCollection collection = noticelistform.listView.Items;

        }

        private void noticelistform_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
                {
                    FindTextForm form = new FindTextForm();
                    form.btn_find.Click += new EventHandler(btn_find_Click);
                    form.txtbox.KeyDown += new KeyEventHandler(txtbox_KeyDown);
                    form.Show(noticelistform);
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string word = null;
                    TextBox box = (TextBox)sender;
                    word = box.Text;
                    ShowFindText(word);
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int num = button.Parent.Controls.Count;
                string word = null;
                for (int i = 0; i < num; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("txtbox"))
                    {
                        TextBox box = (TextBox)button.Parent.Controls[i];
                        word = box.Text;
                    }
                }
                ShowFindText(word);
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }


        /// <summary>
        /// �������� ����Ʈ���� �ؽ�Ʈ �˻����
        /// </summary>
        /// <param name="word"></param>
        private void ShowFindText(string word)
        {
            try
            {
                logWrite("�˻� ����");
                FindListForm form = new FindListForm();
                ListView.ListViewItemCollection col = noticelistform.listView.Items;
                int findnum = 0;
                foreach (ListViewItem item in col)
                {
                    TextBox box = new TextBox();
                    box.Text = item.SubItems[1].Text;
                    if (box.Text.Contains(word))
                    {
                        string date = item.SubItems[3].Text;
                        form.txtbox_result.AppendText("#################################\r\n\r\n");
                        form.txtbox_result.AppendText("�������� : <" + date + ">\r\n\r\n");
                        form.txtbox_result.AppendText(box.Text + "\r\n\r\n");
                        findnum++;
                    }
                }
                int indexnum = form.txtbox_result.Text.IndexOf(word);
                form.txtbox_result.Select(indexnum, word.Length);
                form.txtbox_result.KeyDown += new KeyEventHandler(txtbox_result_KeyDown);
                logWrite("ã�� ���� : " + findnum.ToString());
                if (findnum == 0)
                {
                    MessageBox.Show("�˻��� ����� �����ϴ�.", "�������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    form.Show();
                    form.TopMost = true;
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void txtbox_result_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    TextBox box = (TextBox)sender;
                    string text = box.SelectedText;
                    int textlength = box.SelectionLength;
                    int newstart = textlength + box.SelectionStart;
                    newstart = box.Text.IndexOf(text, newstart);
                    if (newstart == -1)
                    {
                        newstart = box.Text.IndexOf(text, 0);
                    }
                    box.DeselectAll();
                    box.Select(newstart, textlength);
                    box.ScrollToCaret();
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        //�������� ��� ������ ����
        private void listView_Click(object sender, EventArgs e)
        {
            try
            {
                ListView view = (ListView)sender;
                if (view.SelectedItems.Count != 0)
                {
                    ListViewItem mitem = view.SelectedItems[0];
                    string temp = mitem.SubItems[3].Text; //�������
                    string[] ar1 = temp.Split('(');
                    string[] ar2 = ar1[1].Split(')');
                    string name = ar1[0];
                    string id = ar2[0];
                    string msg = mitem.SubItems[2].Text; //����
                    string mode = mitem.SubItems[0].Text;
                    string ntime = mitem.SubItems[4].Text;
                    string seqnum = mitem.Tag.ToString();
                    string title = mitem.SubItems[1].Text;
                    string[] array = new string[] { "r", msg, id, mode, ntime, seqnum, title };  //n|�޽���|�߽���id|mode|title
                    ShowNotice(array);
                    noticelistform.TopMost = false;
                    mitem.Selected = false;
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void pic_memolist_Click(object sender, EventArgs e)
        {
            MakeMemoList();
        }

        /// <summary>
        /// ������ ����Ʈ �� ����
        /// </summary>
        private void MakeMemoList()
        {
            try
            {

                ArrayList list = MemoFileRead();
                if (list == null || list.Count == 0)
                {
                    MessageBox.Show("����� ������ �����ϴ�.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (memolistform != null)
                    {
                        memolistform.Close();
                        memolistform = new MemoListForm();
                    }
                    else
                    {
                        memolistform = new MemoListForm();
                    }
                    memolistform.listView.SelectedIndexChanged += new EventHandler(memolistView_Click);
                    memolistform.MouseClick += new MouseEventHandler(btn_del_Click_forMemo);
                    foreach (object obj in list)
                    {
                        string source = (string)obj;  //item(m|name|id|message|���Ż�id|time)
                        if (source.Length != 0)
                        {
                            string[] subitems = source.Split('|');
                            ListViewItem item = memolistform.listView.Items.Add(subitems[5]);
                            item.SubItems.Add(subitems[1] + "(" + subitems[2] + ")");
                            item.SubItems.Add(subitems[3]);
                            item.Tag = source;
                        }
                    }
                    memolistform.Show();
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btn_all_MouseClickForMemoListForm(object sender, EventArgs e)
        {
            NoParamDele dele = new NoParamDele(selectAllForMemoList);
            Invoke(dele);
        }

        private void btn_cancel_MouseClickForMemoListForm(object sender, EventArgs e)
        {
            NoParamDele dele = new NoParamDele(selectCancelForMemoList);
            Invoke(dele);
        }

        private void selectAllForMemoList()
        {
            ListView.ListViewItemCollection collection = memolistform.listView.Items;
            foreach (ListViewItem item in collection)
            {
                item.Checked = true;
            }
        }

        private void selectCancelForMemoList()
        {
            ListView.ListViewItemCollection collection = memolistform.listView.Items;
            foreach (ListViewItem item in collection)
            {
                item.Checked = false;
            }
        }

        private void btn_del_Click_forMemo(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            int count = button.Parent.Controls.Count;
            for (int i = 0; i < count; i++)
            {
                if (button.Parent.Controls[i].Name.Equals("listView"))
                {
                    ListView view = (ListView)button.Parent.Controls[i];
                    ListView.CheckedListViewItemCollection col = view.CheckedItems;
                    if (col.Count != 0)
                    {
                        foreach (ListViewItem item in col)
                        {
                            string time = item.SubItems[0].Text;
                            time = time.Substring(0, 10);
                            bool del_success = DelMemo(item.Tag.ToString(), time);
                            if (del_success == true)
                            {
                                view.Items.RemoveAt(item.Index);
                            }
                        }
                    }
                }
            }
        }

        private bool DelMemo(string source, string time)
        {
            DirectoryInfo info = new DirectoryInfo(@"C:\MiniCTI\" + this.myid + "\\Memo");
            StreamReader sr = null;
            bool isFind = false;
            if (!info.Exists)
            {
                MemoFileCheck();
            }
            else
            {
                FileInfo[] files = info.GetFiles("*.mem");
                foreach (FileInfo file in files)
                {
                    logWrite(file.Name);
                    if (file.Name.Equals(time + ".mem"))
                    {
                        sr = new StreamReader(file.FullName);
                        string str = sr.ReadToEnd();
                        sr.Close();
                        if (str.Contains(source))
                        {
                            str = str.Remove(str.IndexOf(source), source.Length);
                            StreamWriter sw = new StreamWriter(file.FullName);
                            sw.Write(str);
                            sw.Close();
                            isFind = true;
                        }
                        break;
                    }
                }

                if (isFind == false)
                {
                    foreach (FileInfo file in files)
                    {
                        sr = new StreamReader(file.FullName);
                        string str = sr.ReadToEnd();
                        sr.Close();
                        if (str.Contains(source))
                        {
                            str = str.Remove(str.IndexOf(source), source.Length);
                            StreamWriter sw = new StreamWriter(file.FullName);
                            sw.Write(str);
                            sw.Close();
                            isFind = true;
                            break;
                        }
                    }
                }
            }
            return isFind;
        }

        private void memolistView_Click(object sender, EventArgs e)
        {
            try
            {
                ListView view = (ListView)sender;
                if (view.SelectedItems.Count != 0)
                {
                    string source = (string)view.SelectedItems[0].Tag;
                    string[] memo = source.Split('|');
                    MakeMemo(memo);
                    view.SelectedItems[0].Selected = false;
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void Client_Form_SizeChanged(object sender, EventArgs e)
        {

            int rightgap = this.Width - 290;

            webBrowser1.Width = rightgap + 260;


            int heightgap = this.Height - 600;


            webBrowser1.SetBounds(webBrowser1.Left, 435 + heightgap, webBrowser1.Width, webBrowser1.Height);
            pictureBox2.SetBounds(pictureBox2.Left, 430 + heightgap, pictureBox2.Width, pictureBox2.Height);//�ӽ��̹�����
            panel_logon.Width = this.Width;
            panel_logon.Height = this.Height - (600 - 519);
            memTree.Width = this.Width - (290 - 220);
            memTree.Height = this.Height - (600 - 325);

            InfoBar.Width = this.Width;

        }

        private void StripMn_Quit_Click(object sender, EventArgs e)
        {
            QuitMsg();
        }

        /// <summary>
        /// ��ȭ�� Ŭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_DialogList_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancel_MouseClick(object sender, MouseEventArgs e)
        {
            NoParamDele dele = new NoParamDele(dialogSelectCancel);
            Invoke(dele);
        }

        private void dialogSelectCancel()
        {
            ListView.ListViewItemCollection collection = dialoglistform.listView.Items;
            foreach (ListViewItem item in collection)
            {
                item.Checked = false;
            }
        }

        private void btn_all_MouseClick(object sender, MouseEventArgs e)
        {
            NoParamDele dele = new NoParamDele(dialogSelectAll);
            Invoke(dele);
        }

        private void dialogSelectAll()
        {
            ListView.ListViewItemCollection collection = dialoglistform.listView.Items;
            foreach (ListViewItem item in collection)
            {
                item.Checked = true;
            }
        }

        private void btn_del_Click_forDialog(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int count = button.Parent.Controls.Count;
                for (int i = 0; i < count; i++)
                {
                    if (button.Parent.Controls[i].Name.Equals("listView"))
                    {
                        ListView view = (ListView)button.Parent.Controls[i];
                        ListView.CheckedListViewItemCollection col = view.CheckedItems;
                        if (col.Count != 0)
                        {
                            foreach (ListViewItem item in col)
                            {
                                FileInfo tempfi = (FileInfo)item.Tag;
                                tempfi.Delete();
                                view.Items.RemoveAt(item.Index);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void DialogLisForm_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            e.Item.Selected = true;
        }

        private void DialoglistView_Click(object sender, EventArgs e)
        {
            try
            {
                ListView view = (ListView)sender;
                if (view.SelectedItems.Count != 0)
                {
                    ListViewItem item = view.SelectedItems[0];
                    FileInfo fi = (FileInfo)item.Tag;
                    logWrite(fi.Name);
                    StreamReader sr = fi.OpenText();
                    DialogContent form = new DialogContent();
                    while (!sr.EndOfStream)
                    {
                        string dialogstr = sr.ReadLine();
                        form.textBox.AppendText(dialogstr + "\r\n\r\n");
                    }
                    form.Text = item.SubItems[1].Text;
                    form.Show(dialoglistform);
                    form.TopMost = true;
                    item.Selected = false;
                    sr.Close();
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void pic_notice_Click(object sender, EventArgs e)
        {
            MakeSendNotice();
        }

        private void StripMn_show_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
        }


        #region cbox_connector_CheckStateChanged...
        /// <summary>
        /// ������....
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void cbox_connector_CheckStateChanged(object sender, EventArgs e)
        //{
        //    logWrite("cbox_connector_CheckStateChanged ����!");
        //    try
        //    {
        //        if (cbox_connector.CheckState == CheckState.Checked)
        //        {
        //            try
        //            {
        //                TreeNodeCollection col1 = memTree.Nodes[0].Nodes;
        //                foreach (TreeNode node in col1)
        //                {
        //                    ArrayList list = new ArrayList();
        //                    TreeNodeCollection col2 = node.Nodes;
        //                    foreach (TreeNode mnode in col2)
        //                    {
        //                        list.Add(mnode.Tag.ToString());
        //                    }
        //                    foreach (object obj in list)
        //                    {
        //                        string tag = (string)obj;
        //                        TreeNode[] cnode = node.Nodes.Find(tag, false);
        //                        if (cnode[0].ImageIndex == 0) cnode[0].Remove();
        //                    }
        //                }
        //                memTree.ExpandAll();
        //            }
        //            catch (Exception ex1)
        //            {
        //                logWrite("cbox_connector_CheckStateChanged() ����" + ex1.ToString());
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                ArrayList list = new ArrayList();
        //                TreeNodeCollection col = memTree.Nodes[0].Nodes;
        //                foreach (TreeNode node in col)
        //                {
        //                    if (node.IsExpanded == true)
        //                    {
        //                        list.Add(node.Text);
        //                    }
        //                }

        //                Hashtable table = (Hashtable)memTree.Tag;

        //                foreach (DictionaryEntry de in table)  //table(key=(string)���̸�, value=(string[])��������)
        //                {
        //                    string tempTeamName = (string)de.Key;
        //                    ArrayList arraylist = (ArrayList)de.Value;
        //                    int nodeNum = memTree.Nodes[0].GetNodeCount(false);
        //                    TreeNode node = null;
        //                    if (tempTeamName.Length != 0)
        //                    {
        //                        if (!memTree.Nodes[0].Nodes.ContainsKey(tempTeamName))
        //                        {
        //                            node = memTree.Nodes[0].Nodes.Add(tempTeamName, tempTeamName);   //����� �߰�
        //                            node.ImageIndex = 2;
        //                            node.SelectedImageIndex = 2;
        //                        }
        //                        if (arraylist != null && arraylist.Count != 0)
        //                        {
        //                            foreach (object obj in arraylist)  //list[] {id!name}
        //                            {
        //                                string m = (string)obj;
        //                                if (m.Length != 0)
        //                                {
        //                                    string[] arg = m.Split('!');
        //                                    if (!arg[1].Equals(this.myname))
        //                                    {
        //                                        TreeNode[] nodes = memTree.Nodes[0].Nodes.Find(tempTeamName, false);
        //                                        if (!nodes[0].Nodes.ContainsKey(arg[0]))
        //                                        {
        //                                            TreeNode tempNode = nodes[0].Nodes.Add(arg[0], arg[1]);   //����� ��� �߰�(��� key=id, value=name)
        //                                            tempNode.ToolTipText = arg[0]; //MouseOver�� ��� ��Ÿ�� 
        //                                            tempNode.Tag = arg[0];
        //                                            tempNode.ImageIndex = 0;
        //                                            tempNode.SelectedImageIndex = 0;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (!memTree.Nodes[0].IsExpanded) memTree.Nodes[0].Expand();
        //                    if (team.Equals(this.team.Text)) node.Expand(); 

        //                }
        //                foreach (object obj in list)
        //                {
        //                    string nodetext = (string)obj;
        //                    TreeNodeCollection col1 = memTree.Nodes[0].Nodes;
        //                    foreach (TreeNode node in col1)
        //                    {
        //                        if (node.Text.Equals(nodetext))
        //                        {
        //                            node.Expand();
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex2)
        //            {
        //                logWrite("cbox_connector_CheckStateChanged() ����" + ex2.ToString());
        //            }
        //        }

        //    }
        //    catch (Exception exception)
        //    {
        //        logWrite(exception.ToString());
        //    }
        //}
        #endregion


        public void LoadXml()
        {
            try
            {
                xmldoc.Load("MsgCfg.xml");
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void saveSvrIP(string svrip)
        {
            XmlNode node = xmldoc.SelectSingleNode("//appcfg");
            if (node.HasChildNodes)
            {
                XmlNodeList nodelist = node.ChildNodes;
                foreach (XmlNode itemNode in nodelist)
                {
                    if (itemNode.Attributes["key"].Value.Equals("serverip"))
                    {
                        itemNode.Attributes["value"].Value = svrip;
                        break;
                    }
                }
            }
            xmldoc.Save("MsgCfg.xml");
            System.Configuration.ConfigurationSettings.AppSettings.Set("serverip", svrip);
            serverIP = svrip;
        }

        private string GetServerIP()
        {
            string ipaddr = System.Configuration.ConfigurationSettings.AppSettings["serverip"];
            socket_port_crm = System.Configuration.ConfigurationSettings.AppSettings["socket_port_crm"];
            return ipaddr;
        }

        private void CheckXml()
        {
            try
            {
                xmldoc.Load("MsgCfg.xml");
                XmlNode node = xmldoc.SelectSingleNode("//teamcfg");
                if (node.HasChildNodes)
                {
                    XmlNodeList nodelist = node.ChildNodes;
                    foreach (XmlNode itemNode in nodelist)
                    {
                        if (itemNode.Attributes["visible"].Value.Equals("false"))
                        {
                            omitteamlist.Add(itemNode.Attributes["tname"].Value);
                        }
                    }
                    if (omitteamlist.Count == 0)
                    {
                        node.RemoveAll();
                    }
                }
                xmldoc.Save("MsgCfg.xml");
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private XmlElement MakeXmlNode(string nodename)
        {
            XmlNode node = xmldoc.SelectSingleNode("/root");
            XmlElement element = xmldoc.CreateElement(nodename);
            node.AppendChild(element);
            return element;

        }

        private XmlNodeList GetNodeList(string nodename)
        {
            XmlNode node = xmldoc.SelectSingleNode("/root/" + nodename);
            XmlNodeList list = null;
            if (node.HasChildNodes)
            {
                list = node.ChildNodes;
            }
            return list;
        }

        private void pbx_login_MouseClick(object sender, MouseEventArgs e)
        {
            NoParamDele dele = new NoParamDele(checkInfoForLogin);
            Invoke(dele);
        }



        private void memTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 3;
            e.Node.SelectedImageIndex = 3;
        }

        private void memTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 2;
            e.Node.SelectedImageIndex = 2;
        }

        private void pbx_sizemark_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePoint = e.Location;
            }
        }

        private void pbx_sizemark_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int xdifference = e.X - mousePoint.X;
                int ydifference = e.Y - mousePoint.Y;

                this.Width = 285 + xdifference;
                this.Height = 418 + ydifference;
            }
        }

        private void Mn_extension_Click(object sender, EventArgs e)
        {
            NoParamDele npdele = new NoParamDele(makeExtensionForm);
            Invoke(npdele);
        }

        private void makeExtensionForm()
        {
            try
            {
                if (extform == null)
                {
                    extform = new SetExtensionForm();
                    if (extension != null && extension.Length > 0)
                    {
                        extform.tbx_extension.Text = extension;
                    }
                    extform.tbx_extension.KeyDown += new KeyEventHandler(tbx_extension_KeyDown);
                    extform.btn_ext_confirm.MouseClick += new MouseEventHandler(btn_ext_confirm_MouseClick);
                }
                extform.Show();
                extform.tbx_extension.Focus();

            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void btn_ext_confirm_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (extform.tbx_extension.Text.Trim().Length > 0)
                {
                    extension = extform.tbx_extension.Text.Trim();
                    extform.Close();
                }
                if (connected == false)
                {
                    checkInfoForLogin();
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void tbx_extension_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (extform.tbx_extension.Text.Trim().Length > 0)
                    {
                        extension = extform.tbx_extension.Text.Trim();
                        extform.Close();
                    }
                    if (connected == false)
                        checkInfoForLogin();
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void tbx_pass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    NoParamDele dele = new NoParamDele(checkInfoForLogin);
                    Invoke(dele);
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void checkInfoForLogin()
        {
            if (id.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "���̵� �Է��� �ּ���", "�˸�", MessageBoxButtons.OK);
                id.Focus();
            }
            else if (tbx_pass.Text.Trim().Length == 0)
            {
                logWrite("���̵� üũ �Ϸ�!");
                MessageBox.Show(this, "��й�ȣ�� �Է��� �ּ���", "�˸�", MessageBoxButtons.OK);
                tbx_pass.Focus();
            }
            else if (extension == null || extension.Length == 0)
            {
                logWrite("��й�ȣ üũ �Ϸ�!");
                MessageBox.Show(this, "������ȣ�� ������ �ּ���", "�˸�", MessageBoxButtons.OK);
                makeExtensionForm();
            }
            else
            {
                this.myid = id.Text.ToLower().Trim();
                this.mypass = tbx_pass.Text.Trim();
                StartService();
            }
        }

        private void label_stat_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseMenuStat.Show(label_stat, e.Location, ToolStripDropDownDirection.BelowRight);
            }
        }

        private void Client_Form_Shown(object sender, EventArgs e)
        {

        }

        private void StripMn_online_Click(object sender, EventArgs e)
        {
            stringDele dele = new stringDele(changeMyStat);
            Invoke(dele, "�¶���");
            string statmsg = "20|" + this.myid + "|online";
            SendMsg(statmsg, server);
        }

        private void StringMn_away_Click(object sender, EventArgs e)
        {
            stringDele dele = new stringDele(changeMyStat);
            Invoke(dele, "�ڸ����");
            string statmsg = "20|" + this.myid + "|away";
            SendMsg(statmsg, server);
        }

        private void StripMn_logout_Click(object sender, EventArgs e)
        {
            stringDele dele = new stringDele(changeMyStat);
            Invoke(dele, "�������� ǥ��");
            string statmsg = "20|" + this.myid + "|logout";
            SendMsg(statmsg, server);
        }

        private void StripMn_DND_Click(object sender, EventArgs e)
        {
            stringDele dele = new stringDele(changeMyStat);
            Invoke(dele, "�ٸ��빫��");
            string statmsg = "20|" + this.myid + "|DND";
            SendMsg(statmsg, server);
        }

        private void StripMn_busy_Click(object sender, EventArgs e)
        {
            stringDele dele = new stringDele(changeMyStat);
            Invoke(dele, "��ȭ��");
            string statmsg = "20|" + this.myid + "|busy";
            SendMsg(statmsg, server);
        }

        private void changeMyStat(string statname)
        {
            label_stat.Text = statname + " ��";
            switch (statname)
            {
                case "�ڸ����":
                    pbx_stat.Image = global::Client.Properties.Resources.������;
                    break;

                case "�������� ǥ��":
                    pbx_stat.Image = global::Client.Properties.Resources.�α׾ƿ�;
                    break;

                case "�ٸ��빫��":
                    pbx_stat.Image = global::Client.Properties.Resources.�ٸ��빫��;
                    break;

                case "��ȭ��":
                    pbx_stat.Image = global::Client.Properties.Resources.��ȭ��;
                    break;

                case "�¶���":
                    pbx_stat.Image = global::Client.Properties.Resources.�¶���;
                    break;
            }
        }

        private void pic_NRmemo_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("������ ����", pic_NRmemo);
        }

        private void pic_NRmemo_MouseLeave(object sender, EventArgs e)
        {
            tooltip.Hide(pic_NRmemo);
        }

        private void pic_NRnotice_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("������ ����", pic_NRnotice);
        }

        private void pic_NRnotice_MouseLeave(object sender, EventArgs e)
        {
            tooltip.Hide(pic_NRnotice);
        }

        private void pic_NRfile_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("������ ����", pic_NRfile);
        }

        private void pic_NRfile_MouseLeave(object sender, EventArgs e)
        {
            tooltip.Hide(pic_NRfile);
        }

        private void pic_crm_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pic_crm_MouseEnter(object sender, EventArgs e)
        {
        }

        private void pic_crm_MouseLeave(object sender, EventArgs e)
        {
        }

        private void Mn_default_Click(object sender, EventArgs e)
        {
            try
            {
                string autostart = System.Configuration.ConfigurationSettings.AppSettings["autostart"];
                string topmost = System.Configuration.ConfigurationSettings.AppSettings["topmost"];
                if (configform != null)
                {
                    configform.Close();
                }
                configform = new SetAutoStartForm();
                configform.pbx_confirm.MouseClick += new MouseEventHandler(pbx_confirm_MouseClick);

                if (autostart.Equals("1"))
                {
                    configform.cbx_autostart.Checked = true;
                }

                if (topmost.Equals("1"))
                {
                    configform.cbx_topmost.Checked = true;
                }

                if (nopop == true)
                {
                    configform.cbx_nopop.Checked = true;
                }

                configform.Show();

            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void Mn_server_Click(object sender, EventArgs e)
        {
            NoParamDele dele = new NoParamDele(makeSetServerForm);
            Invoke(dele);
        }

        private void makeSetServerForm()
        {
            try
            {
                if (setserverform != null)
                {
                    setserverform.Close();
                    setserverform = null;
                }
                setserverform = new SetServer_Form();
                setserverform.btnSetting.MouseClick += new MouseEventHandler(btnSetting_MouseClick);
                serverIP = System.Configuration.ConfigurationSettings.AppSettings["serverip"];
                string[] iparr = null;

                if (serverIP.Equals("localhost"))
                {
                    setserverform.rbt_local.Checked = true;
                }
                else
                {
                    setserverform.rbt_ip.Checked = true;
                    iparr = serverIP.Split('.');
                    if (iparr.Length > 3)
                    {
                        setserverform.tbx_ip1.Text = iparr[0];
                        setserverform.tbx_ip2.Text = iparr[1];
                        setserverform.tbx_ip3.Text = iparr[2];
                        setserverform.tbx_ip4.Text = iparr[3];
                    }
                    else
                    {
                        logWrite("serverIP ���� �ùٸ��� ���� : " + serverIP);
                    }
                }
                setserverform.Show();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void cbx_pass_save_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_pass_save.Checked == false)
            {
                uncheckPass_save();
            }
        }

        private void uncheckPass_save()
        {
            try
            {
                System.Configuration.ConfigurationSettings.AppSettings.Set("save_pass", "0");
                setConfigXml(Application.StartupPath + "\\WDMsg_Client.exe.config", "save_pass", "0");
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void Mn_notify_dispose_Click(object sender, EventArgs e)
        {
            QuitMsg();
        }

        private void Mn_notify_show_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
            this.Activate();
        }

        private void menu_option_Opening(object sender, CancelEventArgs e)
        {

        }

        private void TM_motion_sub_Opening(object sender, CancelEventArgs e)
        {

        }

        private void TM_file_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void MnFile_MouseClick(object sender, MouseEventArgs e)
        {
            TM_file_sub.Show(new Point((this.Left), (this.Top + 48)), ToolStripDropDownDirection.BelowRight);
        }

        private void MnMotion_MouseClick(object sender, MouseEventArgs e)
        {
            TM_motion_sub.Show(new Point((this.Left + 60), (this.Top + 48)), ToolStripDropDownDirection.BelowRight);
        }

        private void MnOption_MouseClick(object sender, MouseEventArgs e)
        {
            TM_option_sub.Show(new Point((this.Left + 120), (this.Top + 48)), ToolStripDropDownDirection.BelowRight);
        }

        private void MnHelp_MouseClick(object sender, MouseEventArgs e)
        {
            TM_help_sub.Show(new Point((this.Left + 180), (this.Top + 48)), ToolStripDropDownDirection.BelowRight);
        }

        private void MnFile_MouseEnter(object sender, EventArgs e)
        {
        }

        private void MnFile_MouseLeave(object sender, EventArgs e)
        {
        }

        private void btn_crm_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                crm_main.WindowState = FormWindowState.Normal;
                crm_main.StartPosition = FormStartPosition.Manual;
                crm_main.SetBounds(0, 0, crm_main.Width, crm_main.Height);
                crm_main.TopLevel = true;
                crm_main.Show();
                crm_main.Activate();
            }
            catch (System.ObjectDisposedException dis)
            {
                try
                {
                    cm.SetUserInfo(this.com_cd, this.myid, tbx_pass.Text, serverIP, socket_port_crm);
                    crm_main = new FRM_MAIN();
                    crm_main.StartPosition = FormStartPosition.Manual;
                    crm_main.SetBounds(0, 0, crm_main.Width, crm_main.Height);
                    crm_main.TopLevel = true;
                    crm_main.Show();
                    crm_main.Activate();
                }
                catch (Exception ex1)
                {
                    logWrite(ex1.ToString());
                }
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void btn_board_MouseClick(object sender, MouseEventArgs e)
        {
            SendMsg("1|" + this.myid, server);
        }

        private void btn_memobox_MouseClick(object sender, MouseEventArgs e)
        {
            MakeMemoList();
        }

        private void MakeDialogueboxList()
        {
            try
            {
                ArrayList list = new ArrayList();

                if (!dialogdi.Exists)
                {
                    DialogFileCheck();
                }
                else
                {
                    DirectoryInfo[] diarray = dialogdi.GetDirectories(); //�������� �˻�

                    foreach (DirectoryInfo tempdi in diarray)
                    {
                        DirectoryInfo[] diarray1 = tempdi.GetDirectories();    //�Ϻ����� �˻�
                        foreach (DirectoryInfo tempdi1 in diarray1)
                        {
                            FileInfo[] fiarray = tempdi1.GetFiles("*.dlg");
                            foreach (FileInfo tempfi in fiarray)
                            {
                                list.Add(tempfi);
                            }
                        }

                    }
                }

                if (list.Count == 0)
                {
                    MessageBox.Show("����� ��ȭ����� �����ϴ�.", "�˸�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (dialoglistform != null)
                    {
                        dialoglistform.Close();
                        dialoglistform = new DialogListForm();
                    }
                    else
                    {
                        dialoglistform = new DialogListForm();
                    }

                    dialoglistform.listView.SelectedIndexChanged += new EventHandler(DialoglistView_Click);
                    dialoglistform.btn_del.MouseClick += new MouseEventHandler(btn_del_Click_forDialog);

                    foreach (object obj in list)
                    {
                        FileInfo tempfi = (FileInfo)obj;
                        string fname = tempfi.Name;
                        string[] temparray = fname.Split('!');
                        ListViewItem item = dialoglistform.listView.Items.Add(tempfi.Directory.Name + " " + temparray[0]);
                        string[] array = temparray[1].Split('.');//���� Ȯ���ڸ� ����
                        //string tempname = getName(array[0]);
                        item.SubItems.Add(array[0]);
                        item.Tag = tempfi;
                    }
                    dialoglistform.Show();
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btn_dialoguebox_MouseClick(object sender, MouseEventArgs e)
        {
            MakeDialogueboxList();
        }

        private void btn_sendnotice_MouseClick(object sender, MouseEventArgs e)
        {
            MakeSendNotice();
        }

        private void btn_resultnotice_MouseClick(object sender, MouseEventArgs e)
        {
            MakeNoticeResultList();
        }

        private void MakeNoticeResultList()
        {
            try
            {
                if (isMadeNoticeResult != false)
                {
                    noticeresultform.Show();
                    SendMsg("13|" + this.myid, server);
                }
                else
                {
                    SendMsg("13|" + this.myid, server);
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btn_login_MouseClick(object sender, MouseEventArgs e)
        {
            NoParamDele dele = new NoParamDele(checkInfoForLogin);
            Invoke(dele);
        }

        private void panel_logon_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Process.Start("http://jmtech.co.kr/");
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            tooltip.Show("���������� Ȩ�������� �ٷΰ���", pictureBox2, 3000);
        }


        private void name_MouseClick(object sender, MouseEventArgs e)
        {
            SetUserInfo();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void NRmemo_Click(object sender, MouseEventArgs e)
        {
            try
            {
                if (!NRmemo.Text.Equals("0"))
                {
                    if (noreceiveboardform == null)
                    {
                        string msg = "7|" + this.myid;
                        SendMsg(msg, server);
                    }
                    else
                    {
                        noreceiveboardform.Show();
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void pic_NRnotice_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!NRnotice.Text.Equals("0"))
                {
                    if (noreceiveboardform == null)
                    {
                        string msg = "11|" + this.myid;
                        SendMsg(msg, server);
                    }
                    else
                    {
                        noreceiveboardform.Show();
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void pic_NRfile_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!NRfile.Text.Equals("0"))
                {
                    if (noreceiveboardform == null)
                    {
                        string msg = "10|" + this.myid;
                        SendMsg(msg, server);
                    }
                    else
                    {
                        noreceiveboardform.Show();
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Client_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            //killInitWD();
        }

        private void Client_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                mainform_width = this.Width;
                mainform_height = this.Height;
                mainform_x = this.Left;
                mainform_y = this.Top;
                this.Hide();
                //this.SetBounds(0, screenHeight, this.Width, this.Height);
                //this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                //this.TopMost = true;
                isHide = true;
            }
            else
            {
                if (connected == true)
                {
                    string logout = "9|" + this.myid;
                    SendMsg(logout, server);
                    logWrite(logout);
                    closing();
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    closing();
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        private void weDo����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                aboutform = new AboutForm();
                aboutform.lbl_version.Text = this.version;
                aboutform.Show();
            }
            catch (Exception ex)
            {
                logWrite(ex.ToString());
            }
        }

        private void pic_NRtrans_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!NRtrans.Text.Equals("0"))
                {
                    if (noreceiveboardform == null)
                    {
                        string msg = "23|" + this.myid;
                        SendMsg(msg, server);
                    }
                    else
                    {
                        noreceiveboardform.Show();
                    }
                }
            }
            catch (Exception exception)
            {
                logWrite(exception.ToString());
            }
        }

        private void btn_crm_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("������", btn_crm);
        }

        private void btn_board_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("���� �Խ���", btn_board);
        }

        private void btn_memobox_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("������", btn_memobox);
        }

        private void btn_dialoguebox_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("��ȭ��", btn_dialoguebox);
        }

        private void btn_sendnotice_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("�����ϱ�", btn_sendnotice);
        }

        private void btn_resultnotice_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("������� ����", btn_resultnotice);
        }

        private void pic_NRtrans_MouseEnter(object sender, EventArgs e)
        {
            tooltip.Show("������ �̰�", pic_NRtrans);
        }

        private void label_stat_MouseEnter(object sender, EventArgs e)
        {
            labelColor = label_stat.ForeColor;
            label_stat.ForeColor = Color.DarkOrange;
        }

        private void label_stat_MouseLeave(object sender, EventArgs e)
        {
            label_stat.ForeColor = labelColor;
        }

        private void Client_Form_Activated(object sender, EventArgs e)
        {
            //getForegroundWindow();
        }

    }



    public class ListViewItemComparerDe : System.Collections.IComparer
    {
        private int col;
        public ListViewItemComparerDe()
        {
            col = 0;
        }
        public ListViewItemComparerDe(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
        }
    }

    public class ListViewItemComparerAs : System.Collections.IComparer
    {
        private int col;
        public ListViewItemComparerAs()
        {
            col = 0;
        }
        public ListViewItemComparerAs(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }

}