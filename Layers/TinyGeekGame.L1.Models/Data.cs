using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyGeekGame.L1.Models
{
    public static class Data
    {
        public static List<CorrectAnswer> MainData = new List<CorrectAnswer> 
        { 
            new CorrectAnswer{ImagePath=@"/Images/1.jpg" ,Nationality="Japanese"},
            new CorrectAnswer{ImagePath=@"/Images/2.jpg" ,Nationality="Japanese"},
            new CorrectAnswer{ImagePath=@"/Images/3.jpg" ,Nationality="Japanese"},

             new CorrectAnswer{ImagePath=@"/Images/4.jpg" ,Nationality="Chinese"},
            new CorrectAnswer{ImagePath=@"/Images/5.jpg" ,Nationality="Chinese"},
            new CorrectAnswer{ImagePath=@"/Images/6.jpg" ,Nationality="Chinese"},

             new CorrectAnswer{ImagePath=@"/Images/7.jpg" ,Nationality="Korean"},
            new CorrectAnswer{ImagePath=@"/Images/8.jpg" ,Nationality="Korean"},
            new CorrectAnswer{ImagePath=@"/Images/9.jpg" ,Nationality="Korean"},

             new CorrectAnswer{ImagePath=@"/Images/10.jpg" ,Nationality="Thai"},
            new CorrectAnswer{ImagePath=@"/Images/11.jpg" ,Nationality="Thai"},
            new CorrectAnswer{ImagePath=@"/Images/12.jpg" ,Nationality="Thai"},
        };
    }

}
