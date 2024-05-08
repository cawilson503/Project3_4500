/*Opening Comment - Developed in C# on Visual Studio .NET application builder
 * The program will show the images of four playing cards chosen by the user. 
 * The cards will then be appraised by an "art dealer" and some of them may be purchased.
 * Try to find the pattern that the art dealer is following! After the Art Dealer has purchased two full hands, the pattern will change.
 * There are twelve in total, try to find them all!
 * The program should also append the cards drawn categorized by date to a text file and scrollable textbox, visible in the program. The date should be in MM/DD/YYYY format. 
 
Group 4 consists of Jack Elliott, Haley Laguna, Jonny Stadter, Paul Williams, and Chelsie Wilson.

Finalized: 4/11/2024

Team Lead: Chelsie Wilson
Lead Programmer: Jack Elliott
Scribe: Paul Williams
Designer: Jonny Stadter

Outside resources used: Microsoft's Desktop Guide to Winforms
https://learn.microsoft.com/en-us/dotnet/desktop/winforms/overview/?view=netdesktop-8.0


Compilation Instructions:

1. Choose project in the Solution Explorer. Double click the .sln file
2. Make Active Configuration = Release|Any CPU
3. Run the project to build it
4. Naviate to project fold in file directory, then choose bin --> release --> net6.0-windows
5. Move card image directory 'playingcards' to path above. Please use the playingcards folder included with the project submission, as we have added two additional images
6. The .exe will be in the net6.0-windows along with all depedencies
*/

namespace HW_4_CS_4500
{
   
    public partial class HW4 : Form
    {
        //for fetching card images JE
        string[] fPathSuit = { "_of_spades", "_of_clubs", "_of_hearts", "_of_diamonds" };
        string[] fPathRank = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };


        //For text logging JE
        string[] logSuit = { "S", "C", "H", "D" };
        string[] logRank = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        string filepath = "./LastWon.txt";

        Card currentCard = new Card();
        ArtDealer dealer = new ArtDealer();

        //Jonny Stadter - initialize info window.
        Info info = new Info();

        //Arrays for holding hand and picture boxes JE
        int handTrack = 0;
        public int[] handSuit = new int[4];
        public int[] handRank = new int[4];
        public PictureBox[] picBoxes = new PictureBox[4];


        public HW4()
        { 
            InitializeComponent();
            info.ShowDialog();
            dealer.parsePattern();
            tBoxMsg.Text = "Starting, click 'New Hand' to get started!";
            picBoxes[0] = pictureBox1;
            picBoxes[1] = pictureBox2;
            picBoxes[2] = pictureBox3;
            picBoxes[3] = pictureBox4;
            panel1.Enabled = false;

            //Jonny Stadter
            //Appends Date to CardsDealt.txt and textbox when app first runs.
            appendCardsDealt(DateTime.Now.ToString("MM/dd/yyyy"));
            tBoxRecord.AppendText(DateTime.Now.ToString("MM/dd/yyyy\n"));  
        }

       

        //Jonny Stadter, appends info to record file.
        private void appendCardsDealt(string appendData)
        {
            using (StreamWriter sw = File.AppendText("./CardsDealt.txt"))
            {
                sw.WriteLine(appendData);
            }
        }

        //Checks if card is already in hand, called by Choose Button
        //Jack Elliott & Jonny Stadter
        private bool CardRepeat(int rank, int suit)
        {
            for (int i = 0; i < handTrack; i++)
            {
                if (rank == handRank[i] && suit == handSuit[i])
                {
                    return true;
                }
            }
            return false;
        }

        //Choose button, JE and Jonny Stadter
        //Displays chosen card when clicked
        //Uses global hand[] array, calls CardRepeat, patternSolved() and several functions from ArtDealer class
        private void bChoose_Click(object sender, EventArgs e)
        {
            //If current card is complete, display, textlog, and output to scrollable window
            if (currentCard.isComplete())
            {
                //Check if the current card is already in the hand
                if (!CardRepeat(currentCard.getRank(), currentCard.getSuit()))
                {
                    //Display card, JE
                    string fPath = "./playingcards/" + fPathRank[currentCard.getRank()] + fPathSuit[currentCard.getSuit()] + ".png";
                    var img = System.Drawing.Image.FromFile(fPath);
                    picBoxes[handTrack].Image = img;
                    picBoxes[handTrack].SizeMode = PictureBoxSizeMode.StretchImage;

                    //Populate hand arrays with card info, for later text logging, Jonny Stadter & Jack Elliott
                    handSuit[handTrack] = currentCard.getSuit();
                    handRank[handTrack] = currentCard.getRank();

                    handTrack++;
                    currentCard.reset();

                    //If the hand is full, call artDealer function and reset everything Jack Elliott
                    if (handTrack == 4)
                    {
                        //Check if the hand is a duplicate
                        if (!dealer.checkHand(handRank, handSuit))
                        {
                            //Send hand to art dealer, get array of purchased cards
                            bool[] purchasedCards = new bool[4];
                            //Pattern 9 needs to be handled individually
                            tBoxMsg.Text = "The Art Dealer has purchased the face up cards!";
                            if (dealer.getPattern() == 8)
                            {
                                //Send hand to pattern 9 function and log it
                                purchasedCards = pattern9(handRank, handSuit);
                                dealer.logHand(handRank, handSuit);
                            }  
                            //Otherwise, we can handle the rest of the patterns the same
                            else
                            {
                                purchasedCards = dealer.appraise(handRank, handSuit);
                                //Send hand & array of purchased cards to function to add asterisks & print
                                printHand(handRank, handSuit, purchasedCards);
                            }
                            //Flip cards that weren't purchased
                            for (int i = 0; i < purchasedCards.Length; i++)
                            {
                                if (!purchasedCards[i])
                                {
                                    picBoxes[i].Image = Image.FromFile("./playingcards/cardback.png");
                                }
                            }

                            //Check if the user sucessfully solved a pattern
                            if (dealer.check(purchasedCards))
                            {
                                patternSolved();
                            }
                        
                            //Reset all necessary components
                            tBox1.Text = " ";
                            handTrack = 0;
                            bNewHand.Enabled = true;
                            panel1.Enabled = false;
                        }
                        //Output message to user indicating if they chose a duplicate hand
                        else
                        {
                            tBoxMsg.Text = "You have already chosen that hand in this pattern! Please try again";
                            tBox1.Text = " ";
                            handTrack = 0;
                            bNewHand.Enabled = true;
                            panel1.Enabled = false;
                        } 
                    }

                }
                else
                {
                    tBoxMsg.Text = "That card is already chosen, please select another combination.";
                }
            }
            else
            {
                tBoxMsg.Text = "Your card isn't complete yet!";
            }
        }

        //Used to rest images to card back JE
        //Uses global picBoxes array
        public void resetPic()
        {
            var img = System.Drawing.Image.FromFile("./playingcards/cardback.png");

            for (int i = 0; i < 4; i++)
            {
                picBoxes[i].Image = img;
                picBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        //Jonny Stadter
        //Initiates card choosing when clicked, resets all picture boxes to card back image
        //Calls resetPic() function
        private void bNewHand_Click(object sender, EventArgs e)
        {
            tBoxMsg.Text = "Pick a card, any card!";
            tBox1.Clear();
            resetPic();
            panel1.Enabled = true;
            bNewHand.Enabled = false;
            tBoxRecord.AppendText(Environment.NewLine);
        }

        //Jack Elliott
        //Converts hand into a string to be output to text file & scroll window
        private void printHand(int[] rank, int[] suit, bool[] bought)
        {
            string output = "";
            string s;

            for (int i = 0; i < bought.Length; i++)
            {
                //Concatenate rank and suit, and then add asterisks to purchased cards
                s = logRank[rank[i]] + logSuit[suit[i]];
                if (bought[i])
                    s = ("*" + s + "*");

                //Add a comma + space to all but the last element
                if (i != bought.Length - 1)
                    s += ", ";

                //Add s to full output string 
                output += s;
            }
            //Output string to scroll box
            tBoxRecord.AppendText(output);
            //and log file
            appendCardsDealt(output);
        }

        private void pattern9Print(List<bool[]> hands, int[] ranks, int[] suits)
        {
            //If the dealer has purchased some combination of the hand, output a header
            if (hands.Any())
            {
                string output = "The dealer would buy " + hands.Count.ToString() + " combination(s) of your cards:";
                tBoxRecord.AppendText(output);
                tBoxRecord.AppendText(Environment.NewLine);
                appendCardsDealt(output);
                foreach (bool[] hand in hands)
                {
                    printHand(ranks, suits, hand);
                    tBoxRecord.AppendText(Environment.NewLine);
                }
            }
            //Otherwise, just output the hand as normal
            else
            {
                bool[] dummyHand = { false, false, false, false };
                printHand(ranks, suits, dummyHand);
            }
        }

        //Function for outputting messages to user when they solve a pattern JE
        //Also increments the relevant variables in the ArtDealer object
        //Called by Choose Button function
        void patternSolved()
        {
            //Sound file from Pixabay: https://pixabay.com/sound-effects/search/victory/
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@"./playingcards/victory.wav");
            sp.Play();
            //Check if this is the first success on the current pattern
            if (!dealer.checkSuccess())
            {
                dealer.success();
                
                tBoxMsg.Text = "The art dealer has purchased all of your cards! When another full hand is purchased, you will have solved this pattern!";
            }
            //If it's the second, output relevant message
            else
            {
                dealer.solvedPattern(); //This function will write pattern solved to file
                string x = dealer.getPattern().ToString();
                tBoxMsg.Text = "You've solved pattern " + x + "! Now the dealer will use a new criteria...";
                string output = "$--PATTERN " + x + " SOLVED--$";
                tBoxRecord.AppendText(Environment.NewLine);
                tBoxRecord.AppendText(output);
                appendCardsDealt(output);
                
                
                //If the user has solved the final pattern, output relevant message
                if (dealer.getPattern() == 12)
                {
                    tBoxMsg.Text = "YOU'VE SOLVED ALL THE PATTERNS! Congratulations!!";
                    //Display jokers
                    var img = Image.FromFile("./playingcards/joker.png");
                    foreach (PictureBox p in picBoxes)
                    {
                        p.Image = img;
                        p.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    tBoxRecord.AppendText("$$$$ VICTORY! $$$$");
                    tBoxRecord.AppendText(Environment.NewLine);
                    //Reset pattern to zero
                    dealer.setPattern(0);
                    StreamWriter sw = new StreamWriter(filepath, false);
                    sw.WriteLine("0");
                    sw.Close();
                }
            }
        }

        //Pattern 9. JE
        //Needs to be handled separately because multiple subcombinations of a hand can be purchased
        //Called by Choose button onClick function
        //Returns an array of each card purchased
        //Sends a List of boolean arrays representing all the subcombinations of purchased hands to pattern9print
        public bool[] pattern9(int[] ranks, int[] suits)
        {
            //Constant variable representing the number we are trying to sum to. In the case of HW5, it's 11
            const int TARGET = 11;
            int[] newHand = new int[4];
            //This is used to determine if the user has solved the pattern, i.e if all four cards were bought
            bool[] overallSuccess = { false, false, false, false };
            //This list will hold all subcombinations that are purchased
            List<bool[]> combinations = new List<bool[]>();

            //Convert our ranks to their true values
            for (int i = 0; i < ranks.Length; i++)
            {
                newHand[i] = ranks[i];
                //Since we store ranks as array elements (starting with element 0 = rank 2), convert to true ranks (as they would appear on the card)
                newHand[i] += 2;
                //Convert aces to 1
                if (newHand[i] == 14)
                    newHand[i] = 1;
                //Jacks are now 11, aka the target number, so set them above the target to invalidate them so they aren't purchased
                if (newHand[i] == 11)
                    newHand[i] = 14;
            }

            bool[] bought = { false, false, false, false };
            //Internal function used to reset array to all false
            void resetBought()
            {
                for(int i = 0; i < bought.Length; i++)
                {
                    bought[i] = false;
                }
            }
            int sum = 0;

            //Iterate through each possible combination of cards
            for (int i = 0; i < newHand.Length; i++)
            {
                //Check sum of all remaining numbers
                sum = 0;
                for (int j = i; j < newHand.Length; j++)
                {
                    sum += newHand[j];
                }
                //If all remaining numbers add to 11, we can add it to the combinations and break the loop
                //Because any of the remaining subcombinations cannot possibly sum to 11
                if (sum == TARGET)
                {
                    //Flag all positions that were added
                    for (int j = i; j < newHand.Length; j++)
                    {
                        bought[j] = true;
                        overallSuccess[j] = true;
                    }
                    bool[] output = new bool[4];
                    Array.Copy(bought, output, output.Length);
                    combinations.Add(output);
                    break;
                }

                //Check sum of 3 number combinations. We only have to do this the first iteration
                if (i == 0)
                {
                    //For each element j left after i...
                    for (int j = i + 1; j < newHand.Length; j++)
                    {
                        sum = newHand[i];
                        //Sum all numbers that aren't in position j
                        for (int k = i + 1; k < newHand.Length; k++)
                        {
                            if (k != j)
                                sum += newHand[k];
                        }
                        if (sum == TARGET)
                        {
                            for (int z = i; z < newHand.Length; z++)
                            {
                                if (z != j)
                                    bought[z] = true;
                                    overallSuccess[z] = true;
                            }
                            bool[] output = new bool[4];
                            Array.Copy(bought, output, output.Length);
                            combinations.Add(output);
                            resetBought();
                        }
                    }
                }
             

                //Check 2 number combinations
                for (int j = i + 1; j < newHand.Length; j++)
                {
                    sum = newHand[i] + newHand[j];
                    if (sum == TARGET)
                    {
                        bought[i] = true;
                        overallSuccess[i] = true;
                        overallSuccess[j] = true;
                        bought[j] = true;
                        bool[] output = new bool[4];
                        Array.Copy(bought, output, output.Length);
                        combinations.Add(output);
                        resetBought();
                    }
                }
            }
            //Output all bought combinations
            pattern9Print(combinations, ranks, suits);
            return overallSuccess;
        }

        //-----------SUIT & RANK BUTTONS--------------------
        //Suit buttons JE
        //Sets suit of currentCard when clicked, and displays in text box
        private void bSpades_Click(object sender, EventArgs e)
        {
            currentCard.setSuit(0);
            tBox1.Text = currentCard.toString();
        }

        private void bClubs_Click(object sender, EventArgs e)
        {
            currentCard.setSuit(1);
            tBox1.Text = currentCard.toString();
        }

        private void bHearts_Click(object sender, EventArgs e)
        {
            currentCard.setSuit(2);
            tBox1.Text = currentCard.toString();
        }

        private void bDiamonds_Click(object sender, EventArgs e)
        {
            currentCard.setSuit(3);
            tBox1.Text = currentCard.toString();
        }

        //Rank buttons JE
        //Sets rank of currentCard when button is clicked, and displays in text box
        private void b2_Click(object sender, EventArgs e)
        {
            currentCard.setRank(0);
            tBox1.Text = currentCard.toString();
        }

        private void b3_Click(object sender, EventArgs e)
        {
            currentCard.setRank(1);
            tBox1.Text = currentCard.toString();
        }

        private void b4_Click(object sender, EventArgs e)
        {
            currentCard.setRank(2);
            tBox1.Text = currentCard.toString();
        }

        private void b5_Click(object sender, EventArgs e)
        {
            currentCard.setRank(3);
            tBox1.Text = currentCard.toString();
        }

        private void b6_Click(object sender, EventArgs e)
        {
            currentCard.setRank(4);
            tBox1.Text = currentCard.toString();
        }

        private void b7_Click(object sender, EventArgs e)
        {
            currentCard.setRank(5);
            tBox1.Text = currentCard.toString();
        }

        private void b8_Click(object sender, EventArgs e)
        {
            currentCard.setRank(6);
            tBox1.Text = currentCard.toString();
        }

        private void b9_Click(object sender, EventArgs e)
        {
            currentCard.setRank(7);
            tBox1.Text = currentCard.toString();
        }

        private void b10_Click(object sender, EventArgs e)
        {
            currentCard.setRank(8);
            tBox1.Text = currentCard.toString();
        }

        private void bJack_Click(object sender, EventArgs e)
        {
            currentCard.setRank(9);
            tBox1.Text = currentCard.toString();
        }

        private void bQueen_Click(object sender, EventArgs e)
        {
            currentCard.setRank(10);
            tBox1.Text = currentCard.toString();
        }

        private void bKing_Click(object sender, EventArgs e)
        {
            currentCard.setRank(11);
            tBox1.Text = currentCard.toString();
        }
        private void bAce_Click(object sender, EventArgs e)
        {
            currentCard.setRank(12);
            tBox1.Text = currentCard.toString();
        }
        //---------END SUIT & RANK BUTTONS-------------------


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tBoxMsg_TextChanged(object sender, EventArgs e)
        {

        }

        private void tBoxRecord_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }


    //Class to hold information about current card JE
    public class Card
    {
        int suit;
        int rank;

        string[] disSuit = { "Spades", "Clubs", "Hearts", "Diamonds" };
        string[] disRank = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        public Card()
        {
            suit = -1;
            rank = -1;
        }

        public void setSuit(int i)
        {
            suit = i;
        }
        public int getSuit()
        {
            return suit;
        }
        public void setRank(int j)
        {
            rank = j;
        }
        public int getRank()
        {
            return rank;
        }

        //Converts suit & rank variables into a string to be displayed while choosing a card. Called by all suit & rank buttons
        public string toString()
        {
            string s;
            if (rank == -1)
            {
                s = " ";
            }
            else
            {
                s = disRank[rank];
            }

            s += " of ";

            if (suit == -1)
            {
                s += " ";
            }
            else
            {
                s += disSuit[suit];
            }
            return s;
        }
        public bool isComplete()
        {
            if (suit != -1 && rank != -1)
                return true;
            else
                return false;
        }

        public void reset()
        {
            suit = -1;
            rank = -1;
        }
    }

    //ArtDealer class Jack Elliott
    //Contains all 6 patterns to be check, called by .appraise() method
    //Also contains List of all previous hands in the pattern
    public class ArtDealer
    {

        int pattern = 0;
        bool currentSuccess = false;
        List<Array> pastHands = new List<Array>();//Tracks past hands during each pattern
        string filepath = "./LastWon.txt";

        //Pasted these arrays down here to show how elements correspond to actual cards, since everything is handled as integers as long as possible:
        //string[] logSuit = { "S", "C", "H", "D" };
        //string[] logRank = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        //Used for pattern checking. See the above comment for what these numbers (array elements) correspond to
        int[] faceCards = { 9, 10, 11 };
        int[] primes = { 0, 1, 3, 5 };
        bool[] boughtAll = { true, true, true, true };
        bool[] boughtNone = { false, false, false, false };

        //Constructor for ArtDealer
        public ArtDealer()
        {
            
        }

        //Read pattern from file, if it exists
        public void parsePattern()
        {
            if (File.Exists(filepath))
            {
                StreamReader sr = new StreamReader(filepath);
                try
                {
                    //Read the pattern from the file
                    pattern = int.Parse(sr.ReadLine());
                }
                catch
                {
                    //If something has gone wrong in the file or the read fails, just reset it to zero
                    pattern = 0;
                }
                sr.Close();
            }
        }

        public void setPattern(int i)
        {
            pattern = i;
        }

        public int getPattern()
        {
            return pattern;
        }

        //Used to track that the user has succeeded in the current pattern JE
        public void success()
        {
            currentSuccess = true;
        }

        //Checks if the user has already succeed once on the current pattern JE
        public bool checkSuccess()
        {
            return currentSuccess;
        }

        //Called when user solves a pattern (2 successes) JE
        public void solvedPattern()
        {
            pattern++;
            //Clear the stored hands
            pastHands.Clear();
            //Write solved pattern to the file
            StreamWriter sw = new StreamWriter(filepath, false);
            string output = pattern.ToString();
            sw.WriteLine(output);
            sw.Close();
            //Reset per-pattern successes
            currentSuccess = false;
        }

        //Checks if the user's hand has all been purchased. Called by Choose button onClick function JE
        public bool check(bool[] hand)
        {
            foreach(bool b in hand)
            {
                if (!b)
                    return false;
            }
            return true;
        }

        //Adds a hand to the List of past hands, to check for duplicates JE
        public void logHand(int[] ranks, int[] suits)
        {
           //New array that will hold combined ranks/suits
            int[] a = new int[4];
           
            //Concatenate arrays
            for (int i = 0; i < ranks.Length; i++)
            {
                //credit for this one goes to a 2009 Stack Exchange post, shout out to Rex M
                //https://stackoverflow.com/questions/1014292/concatenate-integers-in-c-sharp
                a[i] = int.Parse(ranks[i].ToString() + suits[i].ToString());
            }
            //Sort a. Doesn't matter what order they are in now, we want them all to be sorted the same for easy checking
            Array.Sort(a);
            //Add new array to List of past hands.
            pastHands.Add(a);
        }

        //Checks if the user's hand has been already selected this pattern JE
        //Called by Choose button onClick function
        public bool checkHand(int[] ranks, int[] suits)
        {
            //New array that will hold combined ranks/suits
            int[] a = new int[4];

            //Concatenate arrays
            for (int i = 0; i < ranks.Length; i++)
            {
                a[i] = int.Parse(ranks[i].ToString() + suits[i].ToString());
            }
            //Sort a so that it matches the order of the stored hands
            Array.Sort(a);

            //Check the pastHands List for a match
            foreach (int[] i in pastHands)
            {
                if (a.SequenceEqual(i))
                    return true;
            }
            return false;
        }


        //Called by Choose Button onClick function. Determines which cards should be purchased based on the current pattern
        public bool[] appraise(int[] ranks, int[] suits)
        {
            //Log the user's hand
            logHand(ranks, suits);

            bool[] cardsBought = new bool[4];

            //We can handle patterns 1-5 the same way
            if (pattern < 5)
            {
                for (int i = 0; i < cardsBought.Length; i++)
                {
                    cardsBought[i] = pattern1to5(ranks[i], suits[i]);
                }
                
            }
            //From here on out, patterns need their own function to handle them
            //Should I have made this a switch statement? Probably \_(-_-)_/
            else if (pattern == 5)
                cardsBought = pattern6(ranks, suits);
            //Pattern 7
            else if (pattern == 6)
                cardsBought = pattern7(ranks, suits);
            //Pattern 8
            else if(pattern == 7)
                cardsBought = pattern8(ranks);
            //Pattern 10. Pattern 9 is handled in main
            else if(pattern == 9)
                cardsBought = pattern10(ranks);
            //Pattern 11
            else if(pattern == 10)
                cardsBought = pattern11(ranks, suits);
            //Pattern 12
            else
                cardsBought = pattern12(ranks, suits);

            return cardsBought;
        }

        //PATTERN 6: THE HIGHEST RANK 
        private bool[] pattern6(int[] r, int[] s)
        {
            bool[] bought = new bool[4];
            int max = r[0];

            //Find max rank
            for (int j = 0; j < r.Length; j++)
            {
                if (max < r[j])
                    max = r[j];
            }

            //Buy all cards that equal max rank
            for (int i = 0; i < r.Length; i++)
            {
                if (r[i] == max)
                    bought[i] = true;
                else
                    bought[i] = false;
            }
            return bought;
        }

        //PATTERN 7: ASCENDING RUN IN SAME SUIT JE
        private bool[] pattern7(int[] r, int[] s)
        {
            //Either we buy all cards, or none, so start with none, in case we hit one of many failure conditions
            int suit = s[0];

            //If the first card is higher than a Jack, an ascending run is not possible
            if (r[0] > 9)
                return boughtNone;

            //Check if all the suits are the same. If any are different, none of the cards are bought
            foreach (int i in s)
            {
                if (i != suit)
                {
                    return boughtNone;
                }
            }

            //Finally, we can check if the ranks are in ascending order
            int rank = r[0];

            for (int j = 1; j < r.Length; j++)
            {
                if ((rank + 1) != r[j])
                    return boughtNone;

                rank++;
            }

            //If we've made it to this stage, the hand is an ascending run in the same suit. Buy all cards
            return boughtAll;
        }
        //PATTERN 8: SKIPPING BY 2, ANY SUIT JE
        private bool[] pattern8(int[] ranks)
        {
            //Sort ranks
            int[] r = new int[4];
            Array.Copy(ranks, r, r.Length);
            Array.Sort(r);

            //Determine if each rank differs by two
            int start = r[0];
            for (int j = 1; j < r.Length; j++)
            {
                //Return the all-false array if a rank doesn't skip by 2
                if (r[j] != (start + 2))
                    return boughtNone;

                start += 2;
            }

            //If we've made it to this point, the hand fits the pattern
            return boughtAll;
        }

        //PATTERN 10: DEAD MAN'S HAND, ACES & EIGHTS JE
        private bool[] pattern10(int[] ranks)
        {
            int eightCount = 0;
            int aceCount = 0;

            //This will count if we have exactly 2 Aces and Eights, regardless of order/suit
            for (int i = 0; i < ranks.Length; i++)
            {
                if (ranks[i] == 6)
                    eightCount++;
                if (ranks[i] == 12)
                    aceCount++;
            }
            //If we do, buy all cards
            if (eightCount == 2 && aceCount == 2)
                return boughtAll;
            //Otherwise, buy none
            else
                return boughtNone;
        }
        //PATTERN 11: ROYAL FLUSH
        private bool[] pattern11(int[] ranks, int[] suits)
        {
            int suitCheck = suits[0];
            //Check if all cards are the same suit
            for (int i = 1; i < suits.Length; i++)
            {
                if (suitCheck != suits[i])
                    return boughtNone;
            }

            //Check if we have ace, king, queen, jack
            int[] sorted = new int[4];

            Array.Copy(ranks, sorted, ranks.Length);
            Array.Sort(sorted);

            //If the sorted array doesn't start with a Jack, we can bail here.
            //9 is a bit of a magic number here, but refer to the comments at the top of this object to see how array elements correspond to ranks
            if (sorted[0] != 9)
                return boughtNone;

            int counter = sorted[0];
            for (int i = 1; i < sorted.Length; i++)
            {
                counter += 1;
                if (counter != sorted[i])
                    return boughtNone;
            }

            //If we've made it to this stage, the hand is a royal flush
            return boughtAll;
        }

        //PATTERN 12: TWO BLACKJACKS JE
        private bool[] pattern12(int[] ranks, int[] suits)
        {
            bool clubCheck = false;
            bool spadeCheck = false;
            int aceCount = 0;

            //Parse the hand for relevant cards: both black Jacks and  two aces
            for (int i = 0; i < ranks.Length; i++)
            {
                if (ranks[i] == 12)
                {
                    aceCount++;
                    continue;
                }

                //These numbers represent where Jack and Spade are stored in their respective arrays
                //Refer to the comments at the top of this object
                if (ranks[i] == 9 && suits[i] == 0)
                {
                    spadeCheck = true;
                    continue;
                }

                if (ranks[i] == 9 && suits[i] == 1)
                {
                    clubCheck = true;
                }
                    
            }

            //If we meet the requirements, buy all cards
            if (clubCheck && spadeCheck && aceCount == 2)
            {
                return boughtAll;
            }
            //Otherwise, buy none
            else
            {
                return boughtNone;
            }

        }

        //Contains patterns 1-5
        private bool pattern1to5(int rank, int suit)
        {
            switch (pattern)
            {
                //Pattern 1, purchase all red cards
                case 0:
                    if (suit == 2 || suit == 3)
                        return true;
                    else
                        return false;

                //Pattern 2, purchase all clubs
                case 1:
                    if (suit == 1)
                        return true;
                    else
                        return false;

                //Pattern 3, purchase all face cards
                case 2:
                    for (int i = 0; i < faceCards.Length; i++)
                    {
                        if (rank == faceCards[i])
                            return true;
                    }
                    return false;

                //Pattern 4, purchase all single digit cards
                case 3:
                    if (rank < 8)
                        return true;
                    else
                        return false;
                //Pattern 5, purchase all single digit prime cards
                case 4:
                    for (int i = 0; i < primes.Length; i++)
                    {
                        if (rank == primes[i])
                            return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
    }

}
