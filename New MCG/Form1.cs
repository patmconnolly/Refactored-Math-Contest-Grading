using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_MCG
{
    public partial class MCG : Form
    {
        bool valid;

        string theMonth;
        string theDay;
        string theYear;
        string theDate;

        string lowerFile;
        string upperFile;
        string schoolFile;

        Statistics Lower;
        Statistics Upper;

        List<Student> LowerStudents;
        List<Student> UpperStudents;
        List<DivisionSchool> LowerSchool;
        List<DivisionSchool> UpperSchool;
        List<MainSchool> Schools;

        List<string> LowerValidationList;
        List<string> UpperValidationFile;

        List<string> titlePage;
        List<string> lowerIndividualAwards;
        List<string> teamAwards;
        List<string> upperIndividualAwards;

        public MCG()
        {
            InitializeComponent();
            monthBox.Text = DateTime.Now.ToString("MMMM");
            dayBox.Text = DateTime.Now.ToString("dd");
            yearBox.Text = DateTime.Now.ToString("yyyy");
        }

        #region ButtonClicks
        private void lowerButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".txt Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                lowerFile = ofd.FileName;
                lowerTextBox.Text = ofd.SafeFileName;
                lowerTextBox.Update();
            }
            else
            {
                MessageBox.Show("Unreadable Lower File!\nPlease Select a Different File.");
            }
        }

        private void upperButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".txt Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                upperFile = ofd.FileName;
                upperTextBox.Text = ofd.SafeFileName;
                upperTextBox.Update();
            }
            else
            {
                MessageBox.Show("Unreadable Upper File!\nPlease Select a Different File.");
            }
        }

        private void schoolButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".txt Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                schoolFile = ofd.FileName;
                schoolTextBox.Text = ofd.SafeFileName;
                schoolTextBox.Update();
            }
            else
            {
                MessageBox.Show("Unreadable School File!\nPlease Select a Different File.");
            }
        }
        #endregion ButtonClicks

        private void validateGrade_Click(object sender, EventArgs e)
        {
            //Pull the dates
            theMonth = monthBox.Text;
            theDay = dayBox.Text;
            theYear = yearBox.Text;
            theDate = theMonth + " " + theDay + ", " + theYear;

            //Zero everything, allows reuse without closing
            #region Zero Variables

            LowerStudents = new List<Student>();
            UpperStudents = new List<Student>();
            LowerSchool = new List<DivisionSchool>();
            UpperSchool = new List<DivisionSchool>();
            Schools = new List<MainSchool>();

            LowerValidationList = new List<string>();
            UpperValidationFile = new List<string>();
            #endregion Zero Variables

            //Pull students and schools, place them in the proper container
            populate();

            //Ensure everything is OK
            valid = validate();

            //Get the file for output
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string AppPath = fbd.SelectedPath + "\\";

                //Writes the validation files
                writeToFile(ValidationString(Lower, LowerStudents), "LOWER_VALIDATION_FILE", AppPath);
                writeToFile(ValidationString(Upper, UpperStudents), "UPPER_VALIDATION_FILE", AppPath);

                //If the data is valid, process
                if (valid)
                {
                    //Ensure the students are in the correct grade
                    //Grade the students
                    //Build the statistics classes
                    gradeIt();

                    buildTitlePage();

                    //Sort the students by grade
                    LowerStudents = sortStudents(LowerStudents);
                    UpperStudents = sortStudents(UpperStudents);

                    //Places the students in the correct school
                    assignStudentsToSchool();

                    //Deletes the schools with nobody in them
                    cleanEmptySchools();

                    //Sorts the schools by score.
                    //Students are in order from earlier.
                    LowerSchool = sortStudents(LowerSchool);
                    UpperSchool = sortStudents(UpperSchool);

                    writeToFile(buildTeamAwards(), "teamAwards", AppPath);
                }
                MessageBox.Show("The Processing is complete.");
            }
            else { MessageBox.Show("INVALID PATH!\nPLEASE TRY AGAIN..."); }
        }

        #region Parse Functions
        private bool validTie(string tie)
        {
            if (tie.Count() == 40)
            {
                for(int i=0; i<40; i++)
                {
                    if(tie[i]!='*' && tie[i]!='1' && tie[i]!='2')
                    {
                        return false;
                    }
                }
                return true;
            }
            else { return false; }
        }

        private bool validKey(string key)
        {
            if (key.Count() == 40)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (key[i] != '1' && key[i] != '2' && key[i] != '3' && key[i] != '4' && key[i] != '5')
                    {
                        return false;
                    }
                }
                return true;
            }
            else { return false; }
        }

        private void populate()
        {
            //Adds School to Division Upper and Lower Classes
            //Adds School to Main School Class
            List<string> tempList = File.ReadAllLines(schoolFile).ToList();
            LowerSchool.Add(new DivisionSchool(splitOnWhiteSpace("-1 UNKNOWN SCHOOL")));
            UpperSchool.Add(new DivisionSchool(splitOnWhiteSpace("-1 UNKNOWN SCHOOL")));
            Schools.Add(new MainSchool(LowerSchool[0].returnSchoolCode(), LowerSchool[0].returnSchoolName()));
            for (int i = 0; i < tempList.Count; i++)
            {
                LowerSchool.Add(new DivisionSchool(splitOnWhiteSpace(tempList[i])));
                UpperSchool.Add(new DivisionSchool(splitOnWhiteSpace(tempList[i])));
                Schools.Add(new MainSchool(LowerSchool[i].returnSchoolCode(), LowerSchool[i].returnSchoolName()));
            }
            //Adds Lower Division Students to the class
            //Creates new statistics item.
            tempList = File.ReadAllLines(lowerFile).ToList();
            List<string> keyList = splitOnWhiteSpace(tempList[0]);
            List<string> tieList = splitOnWhiteSpace(tempList[1]);
            string key = keyList[keyList.Count - 1];
            string tie = tieList[tieList.Count - 1];
            Lower = new Statistics("Lower", key, tie, validKey(key), validTie(tie));
            for(int i=2; i<tempList.Count; i++)
            {
                LowerStudents.Add(new Student(splitOnWhiteSpace(tempList[i]), Schools));
            }
            //Adds Upper Division Students to the class
            //Creates new statistics item.
            tempList = File.ReadAllLines(upperFile).ToList();
            keyList = splitOnWhiteSpace(tempList[0]);
            tieList = splitOnWhiteSpace(tempList[1]);
            key = keyList[keyList.Count - 1];
            tie = tieList[tieList.Count - 1];
            Upper = new Statistics("Upper", key, tie, validKey(key), validTie(tie));
            for (int i = 2; i < tempList.Count; i++)
            {
                UpperStudents.Add(new Student(splitOnWhiteSpace(tempList[i]), Schools));
            }

            //Gets school name for lower division
            for(int i=0;i<LowerStudents.Count;i++)
            {
                for(int j=0;j<LowerSchool.Count;j++)
                {
                    if(LowerStudents[i].returnSchoolCode()==LowerSchool[j].returnSchoolCode())
                    {
                        LowerStudents[i].setSchoolName(LowerSchool[j].returnSchoolName());
                        break;
                    }
                }
            }

            //Gets school name for upper division
            for (int i = 0; i < UpperStudents.Count; i++)
            {
                for (int j = 0; j < UpperSchool.Count; j++)
                {
                    if (UpperStudents[i].returnSchoolCode() == UpperSchool[j].returnSchoolCode())
                    {
                        UpperStudents[i].setSchoolName(UpperSchool[j].returnSchoolName());
                        break;
                    }
                }
            }
        }

        private bool validate()
        {
            //Checks all lower division students to see if they are valid
            for(int i=0;i<LowerStudents.Count;i++)
            {
                if (!LowerStudents[i].returnValid()) { return false; }
            }

            //Checks all upper division students to see if they are valid
            for(int i=0;i<UpperStudents.Count;i++)
            {
                if (!UpperStudents[i].returnValid()) { return false; }
            }

            //If valid return true, else return false immedietly
            return true;
        }

        private void gradeIt()
        {
            #region Ensure Correct Placement
            //Ensures the student is in the correct division, based on scantron information
            //Lower Division
            int temp = LowerStudents.Count;
            int iter = 0;
            while (iter < temp)
            {
                if(LowerStudents[iter].returnDivision()!=5)
                {
                    iter++;
                }
                else
                {
                    UpperStudents.Add(LowerStudents[iter]);
                    LowerStudents.RemoveAt(iter);
                    temp--;
                }
            }
            //Upper Division
            temp = UpperStudents.Count;
            iter = 0;
            while(iter<temp)
            {
                if (UpperStudents[iter].returnDivision() != 4)
                {
                    iter++;
                }
                else
                {
                    LowerStudents.Add(UpperStudents[iter]);
                    UpperStudents.RemoveAt(iter);
                    temp--;
                }
            }
            #endregion Ensure Correct Placement

            //Grades lower division
            for (int i=0;i<LowerStudents.Count;i++)
            {
                LowerStudents[i].setScore(Lower.Grade(LowerStudents[i].returnAnswers()));
            }

            //Grades upper division
            for(int i=0;i<UpperStudents.Count;i++)
            {
                UpperStudents[i].setScore(Upper.Grade(UpperStudents[i].returnAnswers()));
            }
        }

        private void assignStudentsToSchool()
        {
            //Places the lower division students in the correct school
            for(int i=0;i<LowerStudents.Count;i++)
            {
                for(int j=0;j<LowerSchool.Count;j++)
                {
                    if(LowerStudents[i].returnSchoolName()==LowerSchool[j].returnSchoolName())
                    {
                        LowerSchool[j].addStudent(LowerStudents[i]);
                        break;
                    }
                }
            }

            //Places the upper division students in the correct school
            for(int i=0;i<UpperStudents.Count;i++)
            {
                for(int j=0;j<UpperSchool.Count;j++)
                {
                    if(UpperStudents[i].returnSchoolName() == UpperSchool[j].returnSchoolName())
                    {
                        UpperSchool[j].addStudent(UpperStudents[i]);
                        break;
                    }
                }
            }
        }

        private void cleanEmptySchools()
        {
            int iter = 0;
            int numSchools = LowerSchool.Count;
            while(iter<numSchools)
            {
                //MessageBox.Show(LowerSchool[iter].returnUsed().ToString() + UpperSchool[iter].returnUsed().ToString());
                if(LowerSchool[iter].returnUsed() || UpperSchool[iter].returnUsed())
                {
                    iter++;
                }
                else
                {
                    LowerSchool.RemoveAt(iter);
                    UpperSchool.RemoveAt(iter);
                    Schools.RemoveAt(iter);
                    numSchools--;
                }
            }
        }

        private List<string> splitOnWhiteSpace(string line)
        {
            List<string> theLine = new List<string>();
            string theWord = "";
            int counter = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != ' ' && line[i] != '\t')
                {
                    theWord += line[i].ToString();
                    counter++;
                }
                else if ((line[i] == ' ' || line[i] == '\t') && counter > 0)
                {
                    theLine.Add(theWord);
                    theWord = "";
                    counter = 0;
                }
            }
            theLine.Add(theWord);
            return theLine;
        }

        private List<Student> sortStudents(List<Student> them)
        {
            //Pass in list of students
            //Bubble sort by scores
            //Return sorted list
            Student temp;
            for(int i=0;i<them.Count;i++)
            {
                for(int j=0;j<them.Count-i-1;j++)
                {
                    if(them[j].returnScore()<them[j+1].returnScore())
                    {
                        temp = them[j];
                        them[j] = them[j + 1];
                        them[j + 1] = temp;
                    }
                }
            }
            return them;
        }

        private List<DivisionSchool> sortStudents(List<DivisionSchool> them)
        {
            //Pass in list of students
            //Bubble sort by scores
            //Return sorted list
            DivisionSchool temp;
            for (int i = 0; i < them.Count; i++)
            {
                for (int j = 0; j < i - 1; j++)
                {
                    if (them[j].returnScore() < them[j + 1].returnScore())
                    {
                        temp = them[j];
                        them[j] = them[j + 1];
                        them[j + 1] = temp;
                    }
                }
            }
            return them;
        }

        private List<string> combineStringLists(List<string> first, List<string> second)
        {
            for(int i=0;i<second.Count;i++)
            {
                first.Add(second[i]);
            }
            return first;
        }

        private string getAnnualPostFix()
        {
            int it = int.Parse(theYear) - 1972;
            if (it % 10 == 1) { return it.ToString() + "st"; }
            else if (it % 10 == 2) { return it.ToString() + "nd"; }
            else if (it % 10 == 3) { return it.ToString() + "rd"; }
            else { return it.ToString() + "th"; }
        }

        #endregion Parse Functions

        #region String Builder
        private void buildTitlePage()
        {
            titlePage = new List<string>();
            for(int i = 0; i < 25; i++) { titlePage.Add(""); }
            titlePage.Add("           " + getAnnualPostFix() + " Annual Northern Minnesota Mathematics Contest");
            titlePage.Add("                           " + theDate);
            titlePage.Add(" ");
            titlePage.Add("             Department of Mathematics and Computer Science");
            titlePage.Add("                         Bemidji State University");
        }

        private List<string> buildIndividualAwards(List<Student> them)
        {
            List<string> it = new List<string>();

            string ucDivision;
            string lcDivision;

            int studentCount = 10;
            int honorStudentScore;
            honorStudentScore = them[studentCount - 1].returnIntScore();
            while (them[studentCount].returnIntScore() == honorStudentScore) { studentCount++; }

            if (them[studentCount].returnDivision() == 5) { ucDivision = "UPPER"; lcDivision = "Upper"; }
            else { ucDivision = "LOWER"; lcDivision = "Lower"; }

            it.Add(ucDivision + " DIVISION INDIVIDUAL AWARDS");
            it.Add("--------------------------------");
            it.Add("");
            it.Add(lcDivision + " Division Honorable Mention:");
            it.Add("");
            if(studentCount>=10)
            {
                while (studentCount > 10) { it.Add("       " + them[--studentCount].returnNameSchool(studentCount + 1)); it.Add(""); }
            }

            it.Add(lcDivision + " Division Top Ten:");
            it.Add("");
            while (studentCount > 00) { it.Add("       " + them[--studentCount].returnNameSchool(studentCount + 1)); it.Add(""); }


            return it;
        }
        #endregion String Builder

        #region Output Functions
        private List<string> ValidationString(Statistics statistics, List<Student> students)
        {
            List<string> theOutput = new List<string>();

            int numErr = 0;
            int numStudents = students.Count;

            //Adds the key confirmation line to output.
            if (statistics.returnKeyErr()) { theOutput.Add(statistics.returnDivision() + " Key Error     " + statistics.returnKey()); }
            else { theOutput.Add(statistics.returnDivision() + " Key ~OK~      " + statistics.returnKey());}

            //Adds the tie confirmation line to output.
            if (statistics.returnTieErr()) { theOutput.Add(statistics.returnDivision() + " Tie Error     " + statistics.returnTie()); }
            else { theOutput.Add(statistics.returnDivision() + " Tie ~OK~      " + statistics.returnTie());}

            //Adds placeholder line, will add error statistics at the end.
            //This is element 2 in the list
            theOutput.Add("");

            //Adds each student's validation line to the output.
            for(int i=0;i<students.Count;i++)
            {
                if (!students[i].returnValid()) { numErr++; }
                theOutput.Add(students[i].debugString());
            }

            //Adds statistics to second slot
            theOutput[2] = "TOTAL: " + numStudents.ToString() + " STUDENTS | ERRORS: " + numErr.ToString() + " STUDENTS";

            return theOutput;
        }

        private List<string> buildTeamAwards()
        {
            List<string> it = new List<string>();
            List<string> AA = new List<string>();
            List<string> A = new List<string>();
            int AAPlace = 1;
            int APlace = 1;

            it.Add("LOWER DIVISION TEAM AWARDS");
            it.Add("--------------------------");
            it.Add(" ");

            for(int i = 0; i<LowerSchool.Count;i++)
            {
                if(LowerSchool[i].returnLevel()=="AA" && AA.Count < 2) { AA.Add(LowerSchool[i].teamString(AAPlace++)); }
                if (LowerSchool[i].returnLevel() == "A" && A.Count < 4) { A.Add(LowerSchool[i].teamString(APlace++)); }
                if(AAPlace>=2 && APlace >= 4) { break; }
            }

            for(int i = AA.Count - 1; i >= 0; i--) { it.Add(AA[i]); it.Add(""); }
            it.Add("");
            for (int i = A.Count - 1; i >= 0; i--) { it.Add(A[i]); it.Add(""); }
            it.Add("");
            //----------------------------------------------------------------------------
            AA = new List<string>();
            A = new List<string>();
            AAPlace = 1;
            APlace = 1;

            it.Add("UPPER DIVISION TEAM AWARDS");
            it.Add("--------------------------");
            it.Add(" ");

            for (int i = 0; i < UpperSchool.Count; i++)
            {
                if (UpperSchool[i].returnLevel() == "AA" && AA.Count < 2) { AA.Add(UpperSchool[i].teamString(AAPlace++)); }
                if (UpperSchool[i].returnLevel() == "A" && A.Count < 4) { A.Add(UpperSchool[i].teamString(APlace++)); }
                if (AAPlace >= 2 && APlace >= 4) { break; }
            }

            for (int i = AA.Count - 1; i >= 0; i--) { it.Add(AA[i]); it.Add(""); }
            it.Add("");
            for (int i = A.Count - 1; i >= 0; i--) { it.Add(A[i]); it.Add(""); }
            it.Add("");

            return it;
        }

        private void writeToFile(List<string> lines, string fileName, string path)
        {
            TextWriter tw = new StreamWriter(path + fileName + ".txt");
            foreach (string s in lines)
                tw.WriteLine(s);
            tw.Close();
        }
        #endregion Output Functions
    }
}
