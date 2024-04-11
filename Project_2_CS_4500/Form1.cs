/*Opening Comment - Developed in C# on Visual Studio .NET application builder
 * The program should show the images of four playing cards chosen by the user. 
 * The cards will then be appraised by an "art dealer" and some of them may be purchased.
 * Try to find the pattern that the art dealer is following!
 * The program should also append the cards drawn categorized by date to a text file and scrollable textbox, visible in the program. The date should be in MM/DD/YYYY format. 
 
Group 4 consists of Jack Elliott, Haley Laguna, Jonny Stadter, Paul Williams, and Chelsie Wilson.

Finalized: 3/21/2024

Team Lead: Jonny Stadter
Lead Programmer: Paul Williams
Scribe: Jack Elliott
Designer: Chelsie Wilson

Outside resources used: Microsoft's Desktop Guide to Winforms
https://learn.microsoft.com/en-us/dotnet/desktop/winforms/overview/?view=netdesktop-8.0


Compilation Instructions:

1. Choose project in the Solution Explorer. Double click the .sln file
2. Make Active Configuration = Release|Any CPU
3. Run the project to build it
4. Naviate to project fold in file directory, then choose bin --> release --> net6.0-windows
5. Move card image directory 'playingcards' to path above. Please use the playingcards folder included with the project submission, as we have added a card back image
6. The .exe will be in the net6.0-windows along with all depedencies
*/

namespace Project_2_CS_4500
{
    public partial class Project2 : Form
    {
        //for fetching card images JE
        string[] fPathSuit = { "_of_spades", "_of_clubs", "_of_hearts", "_of_diamonds" };
        string[] fPathRank = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };


        //For text logging JE
        string[] logSuit = { "S", "C", "H", "D" };
        string[] logRank = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        Card currentCard = new Card();
        ArtDealer dealer = new ArtDealer();

        //Jonny Stadter - initialize info window.
        Info info = new Info();

        //Arrays for holding hand and picture boxes JE
        int handTrack = 0;
        public int[] handSuit = new int[4];
        public int[] handRank = new int[4];
        public PictureBox[] picBoxes = new PictureBox[4];


        public Project2()
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
                        if(!dealer.checkHand(handRank, handSuit)){
                            //Send hand to art dealer, get array of purchased cards
                            bool[] purchasedCards = dealer.appraise(handRank, handSuit);
                           
                            //Send hand & array of purchased cards to function to add asterisks & print
                            printHand(handRank, handSuit, purchasedCards);

                            //Flip cards that weren't purchased
                            for (int i = 0; i < purchasedCards.Length; i++)
                            {
                                if (!purchasedCards[i])
                                {
                                    picBoxes[i].Image = Image.FromFile("./playingcards/cardback.png");
                                }
                            }
                            tBoxMsg.Text = "The Art Dealer has purchased the face up cards!";
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

        //Function for outputting messages to user when they solve a pattern JE
        //Also increments the relevant variables in the ArtDealer object
        //Called by Choose Button function
        void patternSolved()
        {
            //Check if this is the first success on the current pattern
            if (!dealer.checkSuccess())
            {
                dealer.success();
                tBoxMsg.Text = "The art dealer has purchased all of your cards! When another full hand is purchased, you will have solved this pattern!";
            }
            //If it's the second, output relevant message
            else
            {
                dealer.solvedPattern();
                //IMPLEMENT WRITING PATTERN TO LastWon.txt
                tBoxMsg.Text = "You've solved pattern " + dealer.getPattern().ToString() + "! Now the dealer will use a new criteria...";

                //If the user has solved the final pattern, output relevant message
                if (dealer.getPattern() == 6)
                {
                    tBoxMsg.Text = "You've solved all the patterns! Congratulations!";
                    //FOR NOW, JUST RESETTING TO PATTERN 1, NEED TO IMPLEMENT FULL END MESSAGE
                    dealer.setPattern(0);
                }
            }
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

        //Used to track that the user has succeeded in the current pattern
        public void success()
        {
            currentSuccess = true;
        }

        //Checks if the user has already succeed once on the current pattern
        public bool checkSuccess()
        {
            return currentSuccess;
        }

        //Called when user solves a pattern (2 successes)
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
        private void logHand(int[] ranks, int[] suits)
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



        public bool[] appraise(int[] ranks, int[] suits)
        {
            bool[] cardsBought = new bool[4];

            //We can handle patterns 1-5 the same way
            if (pattern < 5)
            {
                for (int i = 0; i < cardsBought.Length; i++)
                {
                    cardsBought[i] = pattern1to5(ranks[i], suits[i]);
                }

                //Add the hand to the List
                logHand(ranks, suits);

                return cardsBought;
            }
            //Pattern 6 needs to be handled on its own
            else
            {
                int max = ranks[0];

                //Find max rank
                for (int j = 0; j < ranks.Length; j++)
                {
                    if (max < ranks[j])
                        max = ranks[j];
                }

                //Buy all cards that equal max rank
                for (int i = 0; i < ranks.Length; i++)
                {
                    if (ranks[i] == max)
                        cardsBought[i] = true;
                    else
                        cardsBought[i] = false;
                }
                //Add the hand to the List
                logHand(ranks, suits);

                return cardsBought;
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
