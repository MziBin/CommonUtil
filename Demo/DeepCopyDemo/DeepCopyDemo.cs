using CommonUtil.DeepCopy.Implement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepCopyDemo
{
    public partial class DeepCopyDemo : Form
    {
        public DeepCopyDemo()
        {
            InitializeComponent();
        }

        private void btnBinDeepCopy_Click(object sender, EventArgs e)
        {
            DeepCopyClass original = new DeepCopyClass
            {
                Id = 1,
                student = new Student { Name = "Alice", Age = 20 }
            };

            DeepCopyClass copy = new BinaryDeepCopyImpl().DeepCopy(original);
            copy.Id = 2;
            copy.student.Name = "Bob";

            lblObject.Text = $"id={original.Id}; name={original.student.Name}; age={original.student.Age}";
            lblDeepCopyObject.Text = $"id={copy.Id}; name={copy.student.Name}; age={copy.student.Age}";
        }

        private void btnNewtonsoftJson_Click(object sender, EventArgs e)
        {
            DeepCopyClass original = new DeepCopyClass
            {
                Id = 1,
                student = new Student { Name = "Alice", Age = 20 }
            };

            DeepCopyClass copy = new NewtonsoftDeepCopyImpl().DeepCopy(original);
            copy.Id = 2;
            copy.student.Name = "Bob";

            lblObject.Text = $"id={original.Id}; name={original.student.Name}; age={original.student.Age}";
            lblDeepCopyObject.Text = $"id={copy.Id}; name={copy.student.Name}; age={copy.student.Age}";
        }

        private void btnTextJson_Click(object sender, EventArgs e)
        {
            DeepCopyClass original = new DeepCopyClass
            {
                Id = 1,
                student = new Student { Name = "Alice", Age = 20 }
            };

            DeepCopyClass copy = new TextJsonDeepCopyImpl().DeepCopy(original);
            copy.Id = 2;
            copy.student.Name = "Bob";

            lblObject.Text = $"id={original.Id}; name={original.student.Name}; age={original.student.Age}";
            lblDeepCopyObject.Text = $"id={copy.Id}; name={copy.student.Name}; age={copy.student.Age}";
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            //DeepCopyClass original = new DeepCopyClass
            //{
            //    Id = 1,
            //    student = new Student { Name = "Alice", Age = 20 }
            //};

            //DeepCopyClass copy = new DeepCopyImpl().DeepCopyObjectByXML(original);
            //copy.Id = 2;
            //copy.student.Name = "Bob";

            //lblObject.Text = $"id={original.Id}; name={original.student.Name}; age={original.student.Age}";
            //lblDeepCopyObject.Text = $"id={copy.Id}; name={copy.student.Name}; age={copy.student.Age}";
        }
    }
}
