/*Opening Comment - Developed in C# on Visual Studio .NET application builder
The program should show the images of four playing cards chosen by the user. The program should also append the cards drawn categorized
by date to a text file and scrollable textbox, visible in the program. The date should be in MM/DD/YYYY format. The prgram should also have "The Art Dealer" 
(a background player) select random cards from the users hand. The user then has to identify the pattern of the Art Dealer's choices.The Art Dealer's choices
will be appended to the text file with an asterisk (*) around them. 
Group 4 consists of Jack Elliott, Jonny Stadter, Paul Williams, and Chelsie Wilson.

Finalized: 3/21/2024

Team Lead: Jonny Stadter
Lead Programmer: Paul Williams
Scribe: Jack Elliott
Designer: Chelsie Wilson
Tester: Chelsie Wilson


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
        string[] fPathSuit = { "_of_spades", "_of_clubs", "_of_hearts", "_of_diamonds" };
        string[] fPathRank = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };
        //For displaying chosen card to user


        //For text logging
        string[] logSuit = { "S", "C", "H", "D" };
        string[] logRank = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        Card currentCard = new Card();


        //Jonny Stadter - initialize info window.
        Info info = new();


        //Arrays for holding hand and picture boxes JE
        int handTrack = 0;
        public string[] hand = new string[4];
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

        //Suit buttons JE
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
        private bool CardRepeat(int handTrack)
        {
            hand[handTrack] = logRank[currentCard.getRank()] + logSuit[currentCard.getSuit()];
            for (int i = 0; i < handTrack; i++)
            {
                if (hand[handTrack] == hand[i])
                {
                    return true;
                }
            }

            return false;
        }
        //Choose button, JE and Jonny Stadter
        private void bChoose_Click(object sender, EventArgs e)
        {
            //If current card is complete, display, textlog, and output to scrollable window
            if (currentCard.isComplete())
            {

                if (!CardRepeat(handTrack))
                {
                    //Display card, JE
                    string fPath = "./playingcards/" + fPathRank[currentCard.getRank()] + fPathSuit[currentCard.getSuit()] + ".png";
                    var img = System.Drawing.Image.FromFile(fPath);
                    picBoxes[handTrack].Image = img;
                    picBoxes[handTrack].SizeMode = PictureBoxSizeMode.StretchImage;

                    //text log and textbox, Jonny Stadter
                    hand[handTrack] = logRank[currentCard.getRank()] + logSuit[currentCard.getSuit()];

                    handTrack++;
                    currentCard.reset();

                    //If the hand is full, call artDealer function and reset everything
                    if (handTrack == 4)
                    {
                        artDealer(hand);
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

        //Used to rest images to card back
        public void resetPic()
        {
            var img = System.Drawing.Image.FromFile("./playingcards/cardback.png");

            for (int i = 0; i < 4; i++)
            {
                picBoxes[i].Image = img;
                picBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bNewHand_Click(object sender, EventArgs e)
        {
            tBoxMsg.Text = "Pick a card, any card!";
            tBox1.Clear();
            resetPic();
            panel1.Enabled = true;
            bNewHand.Enabled = false;
            tBoxRecord.AppendText(Environment.NewLine);

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

        //ArtDealer method JE
        public void artDealer(string[] hand)
        {
            //Check which cards should be bought (for now, all red cards)
            string s; //used to parse the second character of the strings directly
            string output = "";
            for (int i = 0; i < hand.Length; i++)
            {
                
                s = hand[i];
                if (s.EndsWith('D') || s.EndsWith('H'))//Only red cards will be purchased
                {
                    //Add asterisks to purchased cards
                    hand[i] = ("*" + hand[i] + "*");

                    //Change border of selected cards

                }
                else
                {
                    picBoxes[i].Image = Image.FromFile("./playingcards/cardback.png");
                }
                //Display card in text box
                output += hand[i];
                if (i != hand.Length - 1)   //Add commas and a space unless it's the last card
                    output += ", ";
            }

            //Output string to scroll box
            tBoxRecord.AppendText(output);
            //and log file
            appendCardsDealt(output);

            
            tBoxMsg.Text = "The Art Dealer has purchased the face-up cards!";
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
}