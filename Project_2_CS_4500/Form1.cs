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
        Info info = new();


        //Arrays for holding hand and picture boxes JE
        int handTrack = 0;
        public int[] handSuit = new int[4];
        public int[] handRank = new int[4];
        public PictureBox[] picBoxes = new PictureBox[4];


        public Project2()
        {

            InitializeComponent();
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

            info.Show();

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
        //Uses global hand[] array, calls CardRepeat and artDealer functions
        private void bChoose_Click(object sender, EventArgs e)
        {
            //If current card is complete, display, textlog, and output to scrollable window
            if (currentCard.isComplete())
            {

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
                        //Send hand to art dealer, get array of purchased cards
                        bool[] purchasedCards = dealer.appraise(handRank, handSuit);
                        tBoxMsg.Text = "The Art Dealer has purchased the face up cards!";

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
                        //Reset all necessary components
                        tBox1.Text = " ";
                        handTrack = 0;
                        bNewHand.Enabled = true;
                        panel1.Enabled = false;
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

        //Jonny Stadter, checks if any cards are duplicate combinations

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
        int currentSuccess = 0;
        //Pasted these arrays down here to show how elements correspond to actual cards:
        //string[] logSuit = { "S", "C", "H", "D" };
        //string[] logRank = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        int[] faceCards = { 9, 10, 11 };
        int[] primes = { 2, 3, 5, 7 };

        public void setPattern(int i)
        {
            pattern = i;
        }

        public void success()
        {
            currentSuccess++;
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
