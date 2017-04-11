using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_MCG
{
    class MainSchool
    {
        string theName;
        int theCode;

        //Getters
        public string returnName() { return theName; }
        public int returnCode() { return theCode; }

        //Constructor
        public MainSchool(int Code, string Name)
        {
            theCode = Code;
            theName = Name;
        }

        
    }
}
