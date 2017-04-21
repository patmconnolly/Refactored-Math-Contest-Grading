using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_MCG
{
    class Student
    {
        #region Variable Defanitions
        //Validity
        bool valid;

        bool answerLength;
        bool answerCorrectInput;

        bool schoolCode;
        bool UpperLower;
        bool AorAA;

        //Data
        string answers;
        int theSchoolCode;
        string schoolName;
        int division;       //Upper or Lower Division
        int level;          //AA or A
        string name;

        double score;
        #endregion Variable Defanitions

        //Getters
        #region Getters
        public bool returnValid() { return valid; }
        public string returnAnswers() { return answers; }
        public int returnSchoolCode() { return theSchoolCode; }
        public string returnSchoolName() { return schoolName; }
        public int returnDivision() { return division; }
        public int returnLevel() { return level; }
        public string returnName() { return name; }
        public double returnScore() { return score; }
        public int returnIntScore() { return (int)score; }

        //Formats the output for individual awards
        public string returnScoreName(bool top)
        {
            string it = "";
            it += ((int)score).ToString();
            if(top)
            {
                while (it.Length < 5) { it += " "; }
                it += "* ";
            }
            else { while (it.Length < 7) { it += " "; } }
            it += name;
            return it;
        }

        //Formats the output for top 10 and honorable mentions
        public string returnNameSchool(int place)
        {
            string it = "";
            if(place>10)
            {
                it += "       ";
            }
            else if (place == 10) { it += "10 --> "; }
            else { it += " " + place.ToString() + " --> "; }
            it += name + " : " + schoolName;
            return it;
        }
        #endregion Getters

        //Setters
        #region Setters
        public void setAnswers(string theAnswers) { answers = theAnswers; }
        public void setSchoolCode(int SchoolCode) { theSchoolCode = SchoolCode; }
        public void setSchoolName(string theName) { schoolName = theName; }
        public void setDivision(int Division) { division = Division; }
        public void setLevel(int Level) { level = Level; }
        public void setScore(double Score) { score = Score; }
        #endregion Setters
        
        //Constructor
        public Student(List<string> Line, List<MainSchool> theSchools)
        {
            valid = parseStudent(Line, theSchools);
        }

        //Parse function, validates as it parses, returns valid or not
        public bool parseStudent(List<string> theLine, List<MainSchool> theSchools)
        {
            int lineCount = theLine.Count()-1;
            int tempLength;
            //Check the answer string for length and values
            //Assume bools are false until proven true
            answerLength = false;
            answerCorrectInput = false;
            if (lineCount >= 0)
            {
                tempLength = theLine[lineCount].Length;
                if (tempLength == 40)
                {
                    answerLength = true;
                    for (int i = 0; i < tempLength; i++)
                    {
                        if(theLine[lineCount][i] != '1' && theLine[lineCount][i] != '2' && theLine[lineCount][i] != '3' && theLine[lineCount][i] != '4' && theLine[lineCount][i] != '5' && theLine[lineCount][i] != '6' && theLine[lineCount][i] != '7' && theLine[lineCount][i] != '8' && theLine[lineCount][i] != '9' && theLine[lineCount][i] != '0' && theLine[lineCount][i] != '*')
                        {
                            answerCorrectInput = false;
                            break;
                        }
                        else { answerCorrectInput = true; }
                    }
                    //If all is good, save the answers
                    if (answerCorrectInput) { answers = theLine[lineCount]; }
                    else { answers = "~~~~INCORRECT INPUT VALUES, CHECK KEY~~~"; }
                }
                else { answers = "~~~~~~~~INVALID LENGTH, CHECK KEY~~~~~~~"; }
                lineCount--;
            }

            //Check the school code for length and values
            //Parsing as such
            schoolCode = false;
            schoolName = "Unknown School";
            int theNumber = -1;
            if (lineCount >= 0)
            {
                try
                {
                    theNumber = Convert.ToInt32(theLine[lineCount]);
                    schoolCode = true;
                }
                catch (FormatException)
                {
                    //Intentially left empty
                }
                schoolName = huntSchoolName((theNumber), theSchools);
                lineCount--;
            }

            //Check the division code for values
            //Parsing as such
            UpperLower = false;
            AorAA = false;
            if (lineCount >= 0)
            {
                tempLength = theLine[lineCount].Length;
                if (tempLength == 2)
                {
                    UpperLower = true;
                    //Checks Upper(5) or Lower(4) Division
                    if (theLine[lineCount][0] == '4') { division = 4; }
                    else if(theLine[lineCount][0] == '5') { division = 5; }
                    else { UpperLower = false; }

                    AorAA = true;
                    //Checks for AA(1) or A(9) level
                    if (theLine[lineCount][1] == '1') { level = 1; }
                    else if (theLine[lineCount][1] == '9') { level = 9; }
                    else { AorAA = false; }
                }
                lineCount--;
            }


            //Build the name from the rest if any
            name = "";
            while (lineCount>0)
            {
                name = theLine[lineCount] + " " + name;
                lineCount--;
            }
            if (lineCount >= 0) { name = theLine[lineCount] + ", " + name; }

            //MessageBox.Show(answerLength.ToString() + '\n' + answerCorrectInput.ToString() + '\n' + schoolCode.ToString() + '\n' + UpperLower.ToString() + '\n' + AorAA.ToString());
            if(!answerLength || !answerCorrectInput || !schoolCode || !UpperLower || !AorAA) { return false; }
            else { return true; }
        }

        private string huntSchoolName(int it, List<MainSchool> theSchools)
        {
            for(int i=0;i<theSchools.Count;i++)
            {
                if (it == theSchools[i].returnCode()) { return theSchools[i].returnName(); }
            }
            return "INVALID SCHOOL VALUE";
        }

        //Returns debug string for validation file
        public string debugString()
        {
            string it;
            string temp;

            //Checks for any error
            if (!valid) { it = "ERROR "; }
            else { it = "~OK~  "; }
            //MessageBox.Show(valid.ToString());

            //Adds name to string
            temp = name;
            while (temp.Length <= 40) { temp += " "; }
            it += temp;

            //Applies Upper or Lower to string
            if(UpperLower)
            {
                if (division == 5) { it += "Upper   "; }
                else if(division == 4) { it += "Lower   "; }
                else { it += "ERROR   "; }
            }
            else { it += "ERROR   "; }

            //Applies AA or A to string
            if (AorAA)
            {
                if (level == 1) { it += "A    "; }
                else if (level == 9) { it += "AA   "; }
                else { it += "ERR  "; }
            }
            else { it += "ERR  "; }

            //Applies school name to string
            if (schoolCode) { temp = schoolName; }
            else { temp = "INVALID SCHOOL CODE"; }
            while (temp.Length < 35) { temp += " "; }
            it += temp;

            //Tacks answers to end of string
            it += answers;

            return it;
        }
    }
}
