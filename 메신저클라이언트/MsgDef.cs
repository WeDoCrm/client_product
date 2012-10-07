namespace Client
{
    public class CommonDef
    {
        public const string STR_DEMO = "DEMO";

        public const string REG_APP_NAME = "WeDo";
        public const string REG_APP_NAME_DEMO = "WeDo Demo";
        
        public const string MSG_DEL = "|";
        public const string MSG_LOGIN = "8|";
        public const string MSG_CHAT = "16|";

        public const string PATH_DELIM = "\\";

        public const string WORK_DIR = "c:\\MiniCTI";
        public const string CONFIG_DIR = "config";
        public const string APP_CONFIG_NAME = "{0}.exe.config";

        public const string XML_CONFIG_DEMO = "MiniCTI_config_demo.xml";
        public const string XML_CONFIG_PROD = "MiniCTI_config.xml";
        
        public const string REG_CUR_USR_RUN = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        
        public const string UPDATE_DIR_DEMO = "WeDoUpdater_Demo";
        public const string UPDATE_DIR_PROD = "WeDoUpdater";

        public const string UPDATE_EXE = "AutoUpdater.exe";

        public const string MSGR_TITLE_PROD = "WeDo 메신저";
        public const string MSGR_TITLE_DEMO = "WeDo 메신저 데모버젼";
//string AppConfigFileName = Application.StartupPath + ...
//string AppConfigFilePath = Application.StartupPath + ...
//string MiniCtiXmlConfigPath = 
//string AppXmlConfigPath = 

        
        public const int SOCKET_PORT_CRM = 8886;
        public const string FTP_LOCAL_DIR = "c:\\temp";
        public const string FTP_HOST_DEMO = "ftp://114.202.2.33/Update/demo_client/";
        public const string FTP_HOST_PROD = "ftp://114.202.2.33/Update/client/";
        public const int FTP_PORT = 21;
        public const string FTP_USERID = "eclues";
        public const string FTP_PASS = "eclues!@";
        public const string FTP_VERSION_DEMO = "2.1.54";
        public const string FTP_VERSION_PROD = "2.1.47";
    }
}