using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_MCG
{
    class Statistics
    {
        #region Var Defanitions
        string division;
        int[,] sTable;
        bool KeyErr;
        bool TieErr;
        string theKey;
        string theTie;

        //Tacked on to account for the last place.
        int fortyCount;

        int studentCount;
        int totalScore;
        double averageScore;
        #endregion Var Defanitions

        //Getters
        public bool returnKeyErr() { return KeyErr; }
        public bool returnTieErr() { return TieErr; }
        public string returnDivision() { return division; }
        public string returnKey() { return theKey; }
        public string returnTie() { return theTie; }

        //Constructor
        //Reads division, key, and tie
        //Populates table accordingly
        public Statistics(string Division, string key, string tie, bool keyErr, bool tieErr)
        {
            KeyErr = keyErr;
            TieErr = tieErr;
            theKey = key;
            theTie = tie;
            //Upper or Lower

            studentCount = 0;
            totalScore = 0;
            averageScore = 0.0;

            division = Division;
            /*Creates the statistics table
            / 40 rows for 40 questions
            / Column 0, the correct answer
            / Column 1-5, Answer 1-5
            / Column 6, Other or incorrect
            / Column 7, Tiebreaker
            / Column 8, Count of how many students got that question correct.
            /       40 correct is stored in a separate variable*/
            fortyCount = 0;
            sTable = new int[40, 9];

            //Zero the table
            for(int i=0;i<40;i++)
            {
                for(int j=0;j<8;j++)
                {
                    sTable[i, j] = 0;
                }
            }

            //Populates key column and tie column
            //Key from int values 1-5
            //Tie from 0-2
            for(int i=0;i<40;i++)
            {
                sTable[i,0] = (int)Char.GetNumericValue(key[i]);
                if (tie[i] == '1' || tie[i] == '2') { sTable[i, 7] = (int)Char.GetNumericValue(tie[i]); }
            }
        }

        //Passes in student's answers and returns grade
        //Populates the table as it goes
        public double Grade(string answers)
        {
            double score = 0;

            int tempAnswer;
            const double noTie = 1.0;
            const double oneTie = 1.01;
            const double twoTie = 1.0001;

            //Goes through each answer
            for(int i=0; i<40; i++)
            {
                //Checks if it is valid (1-5 being valid, else being *)
                if(answers[i] == '1' || answers[i] == '2' || answers[i] == '3' || answers[i] == '4' || answers[i] == '5')
                {
                    tempAnswer = (int)Char.GetNumericValue(answers[i]);
                    sTable[i, tempAnswer]++;
                    //If answer is correct, give correct number of points including tie values
                    //Else do nothing
                    if (sTable[i, 0] == tempAnswer)
                    {
                        if (sTable[i, 7] == 1)
                        {
                            score += oneTie;
                        }
                        else if (sTable[i, 7] == 2)
                        {
                            score += twoTie;
                        }
                        else
                        {
                            score += noTie;
                        }
                    }
                    else
                    {
                        sTable[i, 6]++;
                    }
                }
            }
            tempAnswer = (int)score;
            if (tempAnswer == 40) { fortyCount++; }
            else { sTable[tempAnswer, 8]++; }
            totalScore += tempAnswer;
            studentCount++;
            return score;
        }

        //Returns the Frequency Distribution Histogram
        public List<string> returnFrequencyDistribution()
        {
            List<string> it = new List<string>();
            string tempString;

            //Adds header
            tempString = division + " Division Frequency Distribution\n";
            it.Add(tempString);

            //Adds statistics
            averageScore = (double)totalScore / (double)studentCount;
            tempString = "Total Scored: " + studentCount.ToString() + "\nAverage Score: " + averageScore.ToString() + "\n";

            //Adds the graphs
            for(int i=0;i<41;i++)
            {
                tempString = "";
                if (i < 10) { tempString += " "; }
                tempString += i.ToString() + ": ";

                int count;
                if (i >= 40) { count = fortyCount; }
                else { count = sTable[i, 8]; }

                if (count > 99) { tempString += " " + count.ToString(); }
                else if (count > 9) { tempString += "  " + count.ToString(); }
                else { tempString += "   " + count.ToString(); }

                tempString += " ";
                for(int j = 0; j < count; j++) { tempString += "*"; }

                it.Add(tempString);
            }
            return it;
        }

        //Returns the Item Analysis for Instructor Information
        public List<string> returnItemAnalysis()
        {
            List<string> it = new List<string>();
            string theLine = "";

            //Builds header
            it.Add(division + " Division Test Item Analysis\n");
            it.Add("Question    NA     1      2      3      4      5     TBR");
            it.Add("--------   ---    ---    ---    ---    ---    ---    ---");
            
            string c = "*   ";
            string inc = "    ";
            //double percentage;

            for (int i = 0; i < 40; i++)
            {
                theLine = "   ";
                if (i < 9)
                {
                    theLine += " " + (i + 1).ToString() + "      ";
                }
                else
                {
                    theLine += (i + 1).ToString() + "      ";
                }
                //NA
                if (sTable[i, 6] > 99) { theLine += sTable[i, 6].ToString() + inc; }
                else if (sTable[i, 6] > 9) { theLine += " " + sTable[i, 6].ToString() + inc; }
                else { theLine += "  " + sTable[i, 6].ToString() + inc; }

                //1
                if (sTable[i, 1] > 99) { theLine += sTable[i, 1].ToString(); }
                else if (sTable[i, 1] > 9) { theLine += " " + sTable[i, 1].ToString(); }
                else { theLine += "  " + sTable[i, 1].ToString(); }
                if (sTable[i, 0] == 1) { theLine += c; }
                else { theLine += inc; }

                //2
                if (sTable[i, 2] > 99) { theLine += sTable[i, 2].ToString(); }
                else if (sTable[i, 2] > 9) { theLine += " " + sTable[i, 2].ToString(); }
                else { theLine += "  " + sTable[i, 2].ToString(); }
                if (sTable[i, 0] == 2) { theLine += c; }
                else { theLine += inc; }

                //3
                if (sTable[i, 3] > 99) { theLine += sTable[i, 3].ToString(); }
                else if (sTable[i, 3] > 9) { theLine += " " + sTable[i, 3].ToString(); }
                else { theLine += "  " + sTable[i, 3].ToString(); }
                if (sTable[i, 0] == 3) { theLine += c; }
                else { theLine += inc; }

                //4
                if (sTable[i, 4] > 99) { theLine += sTable[i, 4].ToString(); }
                else if (sTable[i, 4] > 9) { theLine += " " + sTable[i, 4].ToString(); }
                else { theLine += "  " + sTable[i, 4].ToString(); }
                if (sTable[i, 0] == 4) { theLine += c; }
                else { theLine += inc; }

                //5
                if (sTable[i, 5] > 99) { theLine += sTable[i, 5].ToString(); }
                else if (sTable[i, 5] > 9) { theLine += " " + sTable[i, 5].ToString(); }
                else { theLine += "  " + sTable[i, 5].ToString(); }
                if (sTable[i, 0] == 5) { theLine += c; }
                else { theLine += inc; }

                //TBR
                if (sTable[i, 7] == 1) { theLine += "  A     "; }
                else if (sTable[i, 7] == 2) { theLine += "  B     "; }
                else { theLine += "        "; }

                //Percentage
                double top = sTable[i, sTable[i, 0]];
                double bottom = (sTable[i, 1] + sTable[i, 2] + sTable[i, 3] + sTable[i, 4] + sTable[i, 5]);
                double nummy = ((top / bottom) * 100);
                theLine += Math.Round(nummy, 1).ToString() + "%";

                it.Add(theLine);
            }


            return it;
        }
    }
}
