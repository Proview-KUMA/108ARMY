using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace Lib.Center
{
    /// <summary>
    /// Account_c 的摘要描述
    /// </summary>
    [Serializable]
    public class Account_c
    {
        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string role_code;
        public string Role_Code
        {
            get { return role_code; }
            set { role_code = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        private string unit_code;
        public string Unit_Code
        {
            get { return unit_code; }
            set { unit_code = value; }
        }
        private string unit_title;
        public string Unit_Title
        {
            get
            {
                return unit_title;
            }
            set
            {
                unit_title = value;
            }
        }
        private string rank_code;
        public string Rank_Code
        {
            get { return rank_code; }
            set { rank_code = value; }
        }
        private string tel;
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private string cell;
        public string Cell
        {
            get { return cell; }
            set { cell = value; }
        }
        private string mail;
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        private string ip;
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }
        public string pwdChange;
        public string PwdChange
        {
            get { return pwdChange; }
            set { pwdChange = value; }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string byAcc;
        public string ByAcc
        {
            get { return byAcc; }
            set { byAcc = value; }
        }

        private bool isValid;
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
        private LoginStatus loginstatus;
        public LoginStatus LoginStatus
        {
            get { return loginstatus; }
            set { loginstatus = value; }
        }
        private User_Role role;
        public User_Role Role
        {
            get { return role; }
            set { role = value; }
        }







        public Account_c()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        public Account_c(string acc, string pwd)
        {
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = du.getDataTableByText("select * from account_c where account = @acc", "acc", acc);
            if (dt.Rows.Count != 1)
            {
                isValid = false;
                loginstatus = LoginStatus.Logout;
            }
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["password"].ToString() == pwd)
                {
                    isValid = true;
                    account = acc;
                    password = pwd;
                    role_code = dt.Rows[0]["role_code"].ToString();
                    name = dt.Rows[0]["name"].ToString();
                    id = dt.Rows[0]["id"].ToString();
                    unit_code = dt.Rows[0]["unit_code"].ToString();
                    rank_code = dt.Rows[0]["rank_code"].ToString();
                    tel = dt.Rows[0]["tel"].ToString();
                    cell = dt.Rows[0]["cellphone"].ToString();
                    mail = dt.Rows[0]["mail"].ToString();
                    ip = dt.Rows[0]["ip"].ToString();
                    pwdChange = dt.Rows[0]["pwdChange"].ToString();
                    status = dt.Rows[0]["status"].ToString();
                    byAcc = dt.Rows[0]["byAcc"].ToString();
                    loginstatus = LoginStatus.Login;
                    string roleCode = dt.Rows[0]["role_code"].ToString();
                    switch (roleCode)
                    { 
                        case "1":
                            role = User_Role.Administrator;
                            break;
                        case "2":
                            role = User_Role.AccountManager;
                            break;
                        case "3":
                            role = User_Role.CenterSupervisor;
                            break;
                        case "4":
                            role = User_Role.CenterOfficer;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    isValid = false;
                    loginstatus = LoginStatus.Logout;
                }
            }
        }
    }

    public enum LoginStatus
    { 
    Login,
        Logout
    }
    public enum User_Role
    { 
    Administrator,
        AccountManager,
        CenterSupervisor,
        CenterOfficer
    }
}
