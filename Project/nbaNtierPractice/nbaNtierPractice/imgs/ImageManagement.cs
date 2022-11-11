using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nbaNtierPractice.imgs
{
    static class ImageManagement
    {
        public static void PicChange(ComboBox cbo, PictureBox pbx)
        {
            int value = Convert.ToInt32(cbo.SelectedValue);

            switch (value)
            {
                case 1:
                    pbx.Image = Image.FromFile(@".\imgs\01celtics60.jpg");
                    break;
                case 2:
                    pbx.Image = Image.FromFile(@".\imgs\02nets.jpg");
                    break;
                case 3:
                    pbx.Image = Image.FromFile(@".\imgs\03knicks.jpg");
                    break;
                case 4:
                    pbx.Image = Image.FromFile(@".\imgs\0476ers.png");
                    break;
                case 5:
                    pbx.Image = Image.FromFile(@".\imgs\05raptors.jpg");
                    break;
                case 6:
                    pbx.Image = Image.FromFile(@".\imgs\06bulls.jpg");
                    break;
                case 7:
                    pbx.Image = Image.FromFile(@".\imgs\07cavaliers.jpg");
                    break;
                case 8:
                    pbx.Image = Image.FromFile(@".\imgs\08pistons.png");
                    break;
                case 9:
                    pbx.Image = Image.FromFile(@".\imgs\09pacers.jpeg");
                    break;
                case 10:
                    pbx.Image = Image.FromFile(@".\imgs\10bucks.jpg");
                    break;
                case 11:
                    pbx.Image = Image.FromFile(@".\imgs\11hawks.jpg");
                    break;
                case 12:
                    pbx.Image = Image.FromFile(@".\imgs\12hornets.png");
                    break;
                case 13:
                    pbx.Image = Image.FromFile(@".\imgs\13heat.png");
                    break;
                case 14:
                    pbx.Image = Image.FromFile(@".\imgs\14magic.jpg");
                    break;
                case 15:
                    pbx.Image = Image.FromFile(@".\imgs\15wizards.png");
                    break;
                case 16:
                    pbx.Image = Image.FromFile(@".\imgs\16nuggets.jpg");
                    break;
                case 17:
                    pbx.Image = Image.FromFile(@".\imgs\17timberwolves.jpg");
                    break;
                case 18:
                    pbx.Image = Image.FromFile(@".\imgs\18thunder.jpg");
                    break;
                case 19:
                    pbx.Image = Image.FromFile(@".\imgs\19blazers.jpg");
                    break;
                case 20:
                    pbx.Image = Image.FromFile(@".\imgs\20jazz.jpg");
                    break;
                case 21:
                    pbx.Image = Image.FromFile(@".\imgs\21warriors.jpg");
                    break;
                case 22:
                    pbx.Image = Image.FromFile(@".\imgs\22clippers.png");
                    break;
                case 23:
                    pbx.Image = Image.FromFile(@".\imgs\23lakers.jpg");
                    break;
                case 24:
                    pbx.Image = Image.FromFile(@".\imgs\24suns.jpg");
                    break;
                case 25:
                    pbx.Image = Image.FromFile(@".\imgs\25kings.jpg");
                    break;
                case 26:
                    pbx.Image = Image.FromFile(@".\imgs\26mavericks.jpg");
                    break;
                case 27:
                    pbx.Image = Image.FromFile(@".\imgs\27rockets.png");
                    break;
                case 28:
                    pbx.Image = Image.FromFile(@".\imgs\28grizzlies.jpg");
                    break;
                case 29:
                    pbx.Image = Image.FromFile(@".\imgs\29pelicans.jpg");
                    break;
                case 30:
                    pbx.Image = Image.FromFile(@".\imgs\30spurs.jpg");
                    break;
            }
        }
    }
}
