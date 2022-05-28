using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SlotMachine
{
    public partial class Form1 : Form
    {
        List<Bitmap> eik = new List<Bitmap>();
        Class1 user = new Class1();
        Class2 update = new Class2();
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\paris\source\repos\SlotMachine\SlotMachine\Database1.mdf;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            eik.Add(SlotMachine.Properties.Resources.apple);
            eik.Add(SlotMachine.Properties.Resources.BANANA);
            eik.Add(SlotMachine.Properties.Resources.KARPOUZI);
            eik.Add(SlotMachine.Properties.Resources.KERASI);
        }

        public void button2_Click(object sender, EventArgs e)
        {
            
            user.name = textBox1.Text;
            user.balance = 10000;
            user.Bets = 0;
            user.Wins = 0;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into [Table] (Username,Wins,Bets) values ('"+textBox1.Text+"','"+0+ "','"+0+"')";
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("You successfully registered.\nFeel free to start betting.");
            label3.Text = "Blance:'" + user.balance.ToString() + "'";


        }


        public void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            var command = new SqlCommand("select * from [Table] WHERE Username='"+textBox1.Text+"'", conn);
            SqlDataReader dn;
            dn = command.ExecuteReader();
            if (dn.Read())
            {
                MessageBox.Show("You can now place your bets.");
                user.name = textBox1.Text;
                int wins = dn.GetInt32(1);
                int bets = dn.GetInt32(2);
                user.Wins = wins;
                user.Bets = bets;
                user.balance = 10000+wins - bets;
                label3.Text = "Blance:'" + user.balance.ToString() + "'";
            }
            else
            {
                MessageBox.Show("You are not registered");
                textBox1.Clear();
            }
            conn.Close();
        }
        public void BalanceUpd()
        {
            label3.Text = "Balance:'" + user.balance.ToString() + "'";
        }

        public void button3_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please Login");
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter bet value");
            }
            else if (user.balance < user.betValue)
            {
                MessageBox.Show("No credits");
            }


            else {

                user.betValue = Convert.ToInt32(textBox2.Text);
                user.balance = user.balance - user.betValue;
                user.Bets = user.Bets + user.betValue;
                BalanceUpd();
                System.Threading.Thread.Sleep(1200);
                Random rand = new Random();
                pictureBox1.Image = eik[rand.Next(0, eik.Count)];
                pictureBox2.Image = eik[rand.Next(0, eik.Count)];
                pictureBox3.Image = eik[rand.Next(0, eik.Count)];


                if (pictureBox4.Visible == true)
                {
                    
                    pictureBox4.Image = eik[rand.Next(0, eik.Count)];
                    if (pictureBox1.Image == pictureBox2.Image && pictureBox2.Image == pictureBox3.Image && pictureBox3.Image == pictureBox4.Image)
                    {
                        if (pictureBox1.Image == eik[4])
                        {
                            user.balance = user.balance + 100 * user.betValue;
                            BalanceUpd();
                            user.Wins = user.Wins + 100 * user.betValue;
                            MessageBox.Show("Max win!!!!!");

                        }
                        else
                        {
                            user.balance = user.balance + 75 * user.betValue;
                            BalanceUpd();
                            user.Wins = user.Wins + 75 * user.betValue;
                            MessageBox.Show("INSANE");
                        }
                    }
                    else if (pictureBox1.Image == pictureBox2.Image && pictureBox2.Image == pictureBox3.Image)
                    {
                        if(pictureBox1.Image == eik[4])
                        {
                            user.balance = user.balance + 75 * user.betValue;
                            BalanceUpd();
                            user.Wins = user.Wins + 75 * user.betValue;
                            MessageBox.Show("WOW!!");
                        }
                        else if(pictureBox1.Image == eik[2])
                        {
                            MessageBox.Show("Very good!!");
                            user.balance = user.balance + 50 * user.betValue;
                            BalanceUpd();
                            user.Wins = user.Wins + 50 * user.betValue;
                        }
                        else
                        {
                            MessageBox.Show("Nice!!!");
                            user.balance = user.balance + 15 * user.betValue;
                            BalanceUpd();
                            user.Wins = user.Wins + 15 * user.betValue;
                        }
                    }
                }
                    
                

                else if (pictureBox1.Image == pictureBox2.Image)
                {
                    if (pictureBox2.Image == pictureBox3.Image)
                    {
                        if (pictureBox1.Image == eik[2])
                        {
                            MessageBox.Show("mini jackpot!!!!!!");
                            user.balance = user.balance + 50 * user.betValue;
                            BalanceUpd();
                            user.Wins = user.Wins + 50 * user.betValue;


                        }
                        else
                        {
                            MessageBox.Show("Nice!!!");
                            user.balance = user.balance + 15 * user.betValue;
                            BalanceUpd();
                            user.Wins = user.Wins + 15 * user.betValue;
                        }
                    }
                    else if (pictureBox2.Image == eik[2])
                    {
                        user.balance = user.balance + 10 * user.betValue;
                        BalanceUpd();
                        user.Wins = user.Wins + 10 * user.betValue;
                    }
                    else
                    {
                        user.balance = user.balance + 3 * user.betValue;
                        BalanceUpd();
                        user.Wins = user.Wins + 3 * user.betValue;
                    }
                }
                else if (pictureBox3.Image == pictureBox2.Image)
                {
                    if (pictureBox2.Image == eik[2])
                    {
                        user.balance = user.balance + 10 * user.betValue;
                        BalanceUpd();
                        user.Wins = user.Wins + 10 * user.betValue;
                    }
                    else
                    {
                        user.balance = user.balance + 3 * user.betValue;
                        BalanceUpd();
                        user.Wins = user.Wins + 3 * user.betValue;

                    }
                }
                else if (pictureBox1.Image == pictureBox3.Image)
                {
                    MessageBox.Show("Unlucky this time.");
                }
                else
                {
                    MessageBox.Show("Unlucky this time.");
                }
            }

            
        }

        public void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE[Table] SET Wins = '"+user.Wins+"', Bets = '"+user.Bets+"' WHERE Username = '"+user.name+"'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("You won:"+user.Wins.ToString());
            MessageBox.Show("Total bets:"+user.Bets.ToString());
            conn.Close();
        }

        public void button5_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = true;
            MessageBox.Show("Please add 7 also :)");
            
        }

        public void button6_Click(object sender, EventArgs e)
        {
            if (pictureBox4.Visible == true)
            {
                eik.Add(SlotMachine.Properties.Resources.seven);
            }
            else
            {
                MessageBox.Show("Add a column in order to add 7");
            }
        }
    }
}
