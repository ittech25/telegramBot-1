using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Telegram;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        public int count = 0;

        //1.make a bot by botFather on telegram
        //2. find you chat id and your bot chat id by this boy : @ShowChatIdBot
        //3.it's hard to under =stand code at beginig but you can az i can GODLUCK
        //...

        string admin_id = "";//telegram id
        string Bot_id = "";//bot id


        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Telegram.bot.token = "";//bot token

            ThreadStart threadStart = new ThreadStart(telemager);
            Thread trd = new Thread(threadStart);
            trd.Start();

            ThreadStart threadStart1 = new ThreadStart(timer);
            Thread trd1 = new Thread(threadStart1);
            trd1.Start();

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value == 100) {
                linkLabel1.Text = "Engine is ready!";
            }
              

        }

        private void timer()
        {

            while (true) {
                telemager();
                Thread.Sleep(100);
            }

        }

        private void telemager()
        {
            //main bot code  

            count++;
            txt_fresh.Text = count.ToString();
            
            
            bot.update = "true";

            if (bot.message_text != null)
            {
                if (bot.message_id != "" & bot.chat_id != admin_id & bot.chat_id != Bot_id & bot.message_text != "/start")
                {

            
                    
                    bot.forwardMessage.send(admin_id, bot.chat_id, bot.message_id);
                    bot.sendMessage.reply_to_message(bot.chat_id, "📤 .پیام شما با موفقیت ارسال شد " + "\n" + "🕧 .لطفا منتظر پاسخ بمانید", bot.message_id);  // پیام فرستاده شده به کاربر برای تایید ارسال پیام به ادمین
                                                                                                                                                              //if (bot.message_text == "📜 دریافت اطلاعات کاربری")
                    bot.sendMessage.send(admin_id, "FirstName is : ➤ " + bot.from_first_name + "\n" + "\n" + "LastNameis : ➤ " + bot.from_last_name + "\n" + "\n" + "Usernameis : ➤ @" + bot.from_username + "\n" + "\n" + "UserId is : ➤ " + bot.chat_id);
                   

                }
                if (bot.chat_id == admin_id)
                {
                    try
                    {
                        Telegram.bot.Automatic_answer.textMessage("/whoami", "شما " + bot.from_last_name + " " + "هستید." + "\n" + "مدیر مجموعه." + "👩‍💼👨‍💼");
                        string reply_to_message_id = bot.update.Split(new string[] { @"""forward_from"":{""id"":" }, StringSplitOptions.None)[1].Split(',')[0];
            

                        bot.sendMessage.send(reply_to_message_id, "پاسخ پیام ارسالی [📮]: " + bot.message_text);

                        bot.sendMessage.reply_to_message(bot.chat_id, "پیام با موفقیت ارسال شد. [📤]", bot.message_id);//Admin to user
                    }

                    catch (Exception qq)
                    {
                        Console.WriteLine(qq.Message);
                    }
                }
                if (bot.message_text == "/start" & bot.chat_id != admin_id)
                {
            
                    using (StreamWriter st = File.AppendText("MembersRobot.txt"))
                    {
                        st.WriteLine(bot.chat_id + "\n");
                        st.Close();
                    }
                    try
                    {
                        ///user Welcome!

                        string txt = "Wellcome to Hospital live Messenger";
                    
                       bot.sendKeyboard.keyboard_R1_1 = " 📜 (تنها یک بار باید انجام شود ❕)دریافت اطلاعات کاربری و تایید حساب کاربری";
                        
                        Thread.Sleep(1000);
                        bot.sendKeyboard.send(bot.chat_id, "✅ حساب کاربری تایید شد." + "\n" + "همه چیز آماده است. 👍");
                    }
                    catch
                    { }
                }
                ///admin welcome
                if (bot.message_text == "/start" & bot.chat_id == admin_id)
                {

                    bot.sendKeyboard.keyboard_R1_1 = "📤 ارسال به تمامی کاربران";
                    bot.sendKeyboard.keyboard_R1_2 = "📊 تعداد کاربران";
                    bot.sendKeyboard.keyboard_R2_1 = "📜 دریافت اطلاعات کاربری";

                    bot.sendKeyboard.send(bot.chat_id, "🍀 به بخش مدیریت خوش آمدید");
                }

                if (bot.message_text == "📤 ارسال به تمامی کاربران" & bot.chat_id == admin_id)
                {
                    bot.sendMessage.send(bot.chat_id, "ابتدا دستور زیر را چند ثانیه نگه داشته و سپس متن خود را واردی کنید." + "\n" + " /Podcats" + "...");
                }
                else if (bot.message_text.Contains("/Podcats"))
                {
                    var TextAll = bot.message_text.Split('=').Last();
                    using (StreamReader sr = new StreamReader("MembersRobot.txt"))
                    {
                        foreach (string x in sr.ReadToEnd().Split('\n'))
                        {
                            bot.sendMessage.send(x, "🤳 این یک پیام عمومی است" + "\n" + TextAll);
                        }
                    }

                }
                if (bot.message_text == "📊 تعداد کاربران" & bot.chat_id == admin_id)
                {
                    using (StreamReader sr = new StreamReader("MembersRobot.txt"))
                    {
                        int UserCount = sr.ReadToEnd().Split('\n').Count();
                        bot.sendMessage.send(admin_id, "می باشد." + "\n" + "نفر" + UserCount.ToString() + "\n" + " : در حال حاضر موجودیت برابر ");
                    }
                }
                List<string> listofuser = new List<string>();
                if (bot.message_text == "📜 دریافت اطلاعات کاربری")
                {
                    
                    Telegram.bot.sendMessage.reply_to_message(Telegram.bot.chat_id, "#ID" + "\n" + "➖➖➖➖➖➖➖➖" + "\n" + "\n" + "FirstName is :  " + bot.from_first_name + "\n" + "\n" + "LastNameis :  " + bot.from_last_name + "\n" + "\n" + "Usernameis :  @" + bot.from_username + "\n" + "\n" + "UserId is :  " + bot.chat_id + "\n" + "\n" + "text Message  is :  " + bot.message_text + "\n" + "\n" + "NumberPhone is:  ERORR   404" + "\n" + "https://telegram.me/" + bot.from_username + "\n" + "_______________________________", Telegram.bot.message_id);

                }
                
            }

        }

        private void txt_fresh_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
