using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TermFrequencyExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox1.Text = "Currently the switch port you are connected to probably only has vlan 10 tagged to it, ";
            textBox1.Text += "where the vlan wrapper is being removed from packets being sent out that port.  ";
            textBox1.Text += "The switch port you are connected to will need to be configured to be a trunk  ";
            textBox1.Text += "for vlan 10 and vlan 130 so that the packets that are handled by that port will  ";
            textBox1.Text += "have the vlan wrapper on them. You can then configure your NIC to use vlans 10 and 130,  ";
            textBox1.Text += "vlan wrappers will then be removed by your NIC. No other device will be able to use  ";
            textBox1.Text += "that switch port unless it is also configure it to wrap packets with vlan 10 or 130. ";


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            process(textBox1, textBox2);
            
			
		} // End of Main method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tb1"></param>
        /// <param name="tb2"></param>
        public void process(TextBox tb1, TextBox tb2)
        {
            // Read a file into a string (this file must live in the same directory as the executable)
            //Or just hard code text like below
            string inputString = tb1.Text; //File.ReadAllText(filename);

            // Convert our input to lowercase
            inputString = inputString.ToLower();

            // Define characters to strip from the input and do it
            string[] stripChars = { ";", ",", ".", "-", "_", "^", "(", ")", "[", "]",
						            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "\n", "\t", "\r" };

            foreach (string character in stripChars)
            {
                inputString = inputString.Replace(character, "");
            }

            // Split on spaces into a List of strings
            List<string> wordList = inputString.Split(' ').ToList();

            // Define and remove stopwords
            string[] stopwords = new string[] { "and", "the", "she", "for", "this", "you", "but", "that", "will", "he", "are" };
            foreach (string word in stopwords)
            {
                // While there's still an instance of a stopword in the wordList, remove it.
                // If we don't use a while loop on this each call to Remove simply removes a single
                // instance of the stopword from our wordList, and we can't call Replace on the
                // entire string (as opposed to the individual words in the string) as it's
                // too indiscriminate (i.e. removing 'and' will turn words like 'bandage' into 'bdage'!)
                while (wordList.Contains(word))
                {
                    wordList.Remove(word);
                }
            }

            // Create a new Dictionary object
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            // Loop over all the words in our wordList...
            foreach (string word1 in wordList)
            {

                string word = word1;

                // If the length of the word is at least three letters...
                if (word.Length >= 3)
                {
                    //Need to deal with Pluralization
                    if (word.EndsWith("s") && word.EndsWith("ss") == false)
                    {
                        word = word.Substring(0, word.Length - 1);
                        if (word.Length < 3)
                            word = word1;

                    }

                    if (word.EndsWith("d"))
                    {
                        word = word.Substring(0, word.Length - 1);

                    }

                    // ...check if the dictionary already has the word.
                    if (dictionary.ContainsKey(word))
                    {
                        // If we already have the word in the dictionary, increment the count of how many times it appears
                        dictionary[word]++;
                    }
                    else
                    {
                        // Otherwise, if it's a new word then add it to the dictionary with an initial count of 1
                        dictionary[word] = 1;
                    }

                } // End of word length check

            } // End of loop over each word in our input

            // Create a dictionary sorted by value (i.e. how many times a word occurs)
            var sortedDict = (from entry in dictionary
                              orderby entry.Value descending
                              select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

            // Loop through the sorted dictionary and output the top N most frequently occurring words
            int count = 1;
            tb2.Text = "---- Most Frequent Terms :  ----\r\n";

            foreach (KeyValuePair<string, int> pair in sortedDict)
            {
                // Output the most frequently occurring words and the associated word counts
                tb2.Text += pair.Key.ToString() + "\t\t" + pair.Value.ToString() + "\r\n";
                count++;

                // Only display the top 10 words then break out of the loop!
                if (count > 20)
                {
                    break;
                }
            }
            
        }
        
    }
}
