# CSharp Local Code Analyzer

Implementation:

Assumptions:
Due to the complexity of the c# syntax, this parser is unable to parse every type of syntax. Here are a list of syntax that the parser 
assumes inorder for a accurate metric measurement.

1) Properties get and set must be in a separate lines:

//this is what the parse expects
public int sample {
    get;
    set;
}

//syntaically valid but parser will ignore this
public int sample { get; set; }

2) Parser will ignore all anonymouse classes

3) Parser ignores do, while loops

