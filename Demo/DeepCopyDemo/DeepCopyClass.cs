using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepCopyDemo
{

    //用于深度拷贝实验的类

    [Serializable]
    public class DeepCopyClass
    {
        public int Id { get; set; }

        public Student student;
    }

    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
