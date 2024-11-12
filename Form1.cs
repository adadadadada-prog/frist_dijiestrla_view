using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;



namespace window_dijie1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Point[] point = new Point[100];//存储点的数组；
        int i = 0;//记录点序号；
        int flage1;//选择画点还是画线(起点和终点)；
        int flage2;//选择第一个点还是第二个点；
        int num;//记录有多少个点
        int[,] group = new int[101, 101];//记录边数据；//加了1范围从100，100到101，101，不超出数组界限了，但是输出结果有1000，不对
        int a;//记录线的起点；
        int b;//记录线的终点；
        int stand = 50;//记录重叠标准；
        Point[] point1 = new Point[3];



        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            if (flage1 == 0)
            {
                point[i].X = e.X;
                point[i].Y = e.Y;
                
                g.FillEllipse(new SolidBrush(Color.Red), point[i].X - 5, point[i].Y - 5, 10, 10);
                
                //第一次写的位置g.DrawString(num.ToString(), new Font(FontFamily.GenericSansSerif, 10), Brushes.Red, point[i].X + 10, point[i].Y + 10);
                MessageBox.Show("x" + Convert.ToString(point[i].X = e.X), "y" + Convert.ToString(point[i].Y = e.Y));
                num = ++i;//之前在这里因为标签被卡了一下，，全局变量i和num的值不能改；
                MessageBox.Show("num:" + Convert.ToString(num));
                //序号坐标不对g.DrawString(num.ToString(), new Font(FontFamily.GenericSansSerif, 10), Brushes.Red, point[i].X + 10, point[i].Y + 10);
                /*第二次写的位置*/
                //第三次写
                g.DrawString(num.ToString(), new Font(FontFamily.GenericSansSerif, 10), Brushes.Red, e.X + 10, e.Y + 10);
            }
            if (flage1 == 1)
            {
                MessageBox.Show("进入起点绘制");
                point1[1] = sa1(e.X, e.Y);
                
            }
            if (flage1 == 2)
            {
                MessageBox.Show("进入终点绘制" );
                MessageBox.Show("终点x" + Convert.ToString(e.X), "终点y" + Convert.ToString(e.Y));

                point1[2] = sa2(e.X, e.Y);
                
                Pen linepen = new Pen(Color.Red);
                g.DrawLine(linepen, point1[1], point1[2]);
                flage1 = 1;
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            flage1 = 0;//绘制点；
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flage1 = 1;//绘制线起点；
        }
        private Point sa1(int x, int y)
        {
            MessageBox.Show("sa1" );
            for (int i = 0; i < num; i++)
            {
                if (Math.Abs(x - point[i].X) < stand && Math.Abs(y - point[i].Y) < stand && flage2 == 0)
                {
                    
                    a = i;
                    MessageBox.Show("起点识别x" + Convert.ToString(point[i].X ), "起点识别y" + Convert.ToString(point[i].Y));
                    return point[i];
                }

                //else
                //{
                //    return point[i];
                //}///////////why!!!!!!!是这里卡了我半天；；；；；
            }

            return point[1];





        }
        private Point sa2(int x, int y)
        {
            MessageBox.Show("进入sa2" );

            for (int i = 0; i < num; i++)
            {
                //MessageBox.Show("进入sa22");
                if (Math.Abs(x - point[i].X) < stand && Math.Abs(y - point[i].Y) < stand && flage2 == 1)
                {
                    //MessageBox.Show("进入sa222");
                    flage2 = 0;
                    b = i;
                    MessageBox.Show("终点识别x" + Convert.ToString(point[i].X), "终点识别y" + Convert.ToString(point[i].Y));
                    
                    string input = Interaction.InputBox(" ");
                    group[a, b] = Convert.ToInt32(input);
                    MessageBox.Show("group" + Convert.ToString(a+b), "边权" + Convert.ToString(group[a,b]));
                    return point[i];
                    
                }

                //else
                //{
                //    MessageBox.Show("进入sa22边路1");//进入边路一了；
                //    return point[i];
                //}

                
            }
            MessageBox.Show("!!!!!!!!!!!!!!");
            return point[1];
        }

        private void button3_Click(object sender, EventArgs e)
        {//绘制终点；
            flage1 = 2;
            flage2 = 1;
        }

        //算法
        private int[] lianxu()
        {//记录最终结果的数组；
            int begain;//记录起点
            int[] end = new int[num];
            int[] check = new int[num];
            for(int i = 0; i < num ; i++)
            {
                end[i] = 1000;
            }//找到目前最后一个问题了num-1（原）导致我本应1000的没被赋值为1000而为0；
            for (int i = 0; i < num ; i++)
            {
                check[i] = 0;
            }
            string input = Interaction.InputBox(" ");
            begain = Convert.ToInt32(input);
            end[begain] = 0;//起点到起点的距离为0；
            MessageBox.Show("起点"+Convert.ToString(begain));

            for (int n = 0; n < num; n++)
            {
                int minn = 100;
                int minx=1;
                for(int s = 0; s < num; s++)
                {
                    if (end[s] < minn && check[s] == 0)
                    {
                        minn = end[s];
                        minx = s;
                    }
                }
                check[minx] = 1;
                for(int s = 0; s < num; s++)
                {
                    if (group[minx, s] > 0)//这个if的意义//老是超出索引界限//貌似是吧group的minx打成minn（试了一下，没解决距离不对的问题）
                    {
                        if (minn == 0 && minx == 0 && s == 2)
                        {
                            MessageBox.Show("0-2end起始" + Convert.ToString(end[s]));
                        }//边权在此处正确；
                        if (minn == 0 && minx == 0 && s == 2)
                        {
                            MessageBox.Show("0-2边权+minn" + Convert.ToString(minn + group[minx,s]));
                        }
                        if (minn + group[minx, s] < end[s])
                        {
                            end[s] = group[minx, s]+minn;
                        }
                        if (minn == 0 && minx == 0 && s == 2)
                        {
                            MessageBox.Show("0-2end" + Convert.ToString(end[s]));
                        }
                    }
                }
            }
            return end;
        }

        private void button4_Click(object sender, EventArgs e)
        {//结果显示；
            int[] dis = new int[num-1];
            dis = lianxu();
            for(int i = 0; i < num; i++)
            {
                MessageBox.Show("距离" + Convert.ToString(dis[i]));
            }

        }
    }
    
    
   
   


}
