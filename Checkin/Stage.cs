using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace CheckinLib
{
    class Stage
    {
        public Stage(String name) {
            this.name = name;
        }

        String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        String message;

        public String Message
        {
            get { return message; }
            set { message = value; }
        }
        Boolean ok = false;

        public Boolean Ok
        {
            get { return ok; }
            set { ok = value; }
        }
    }
}
