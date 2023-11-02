using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyChanged;
using System.Linq;
using System.Text;
using System;

namespace TaskApp.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class MyTask
    {
        public string TaskColor { get; set; }
        public string TaskName { get; set; }
        public bool Completed { get; set; }
        public int CategoryId { get; set; }
    }
}
