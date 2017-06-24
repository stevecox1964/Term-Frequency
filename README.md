# Term Frequency Inverse Document Frequency (TF-IDF)

Simple C# Winforms Term Frequency Example ...

This coding example was an attempt by me to better understand TF-IDF (Term Frequency Inverse Document Frequency)
concepts which can be used to cluster documents together that similier. What do you need to know about
TF-IDF, well you don't, unless you want to understand how to build a better E-mail Spam Filter, which is what
I was interested in doing (becuase they all suck).

Before you can compare documents or push documents into SVMs or Neural Networks, 
they have to be vectorized or, all the words need to be turned into numbers. 

You need to tokenize the words, stem the words, get rid of common words, then take the most
frequent words and turn those words into vectors.

This code is the first part.

You can look up TF-IDF on-line to learn more about what has to happen. I wanted to do this in C#
so here it is.
