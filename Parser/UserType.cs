///////////////////////////////////////////////////////////////////////////
// UserType.cs - Clas responsible for storing all user defined types     //
// ver 1.1                                                               //
// Language:    C#, 2008, .Net Framework 4.0                             //
// Platform:    Windows 10                                               //
// Application: Demonstration for CSE681, Project #2, Summer 2018        //
// Author:      Simon Huang, Syracuse University                         //
///////////////////////////////////////////////////////////////////////////
/*
 * Module Operations:
 * ------------------
 * This module defines the following classes:
 *   UserType   - class to store all user defined types base on command line arguments 
 */
/* Required Files:
 *   Semi.cs
 *   
 * Build command:
 *   Interfaces and abstract base classes only so no build
 *   
 * Maintenance History:
 *
 */
using CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinedType
{
    class UserType
    {
        //create static datastructure to be shared among classes
        //can change it in the future to have more oo approach
        public static HashSet<string> userDefinedSet = new HashSet<string>();
        public static UserType instance = null;

        public UserType() {
            instance = this;
        }

        //singleton
        //public static UserType getInstance
        //{
        //    get
        //    {
        //        if (instance == null) {
        //            instance = new UserType();
        //        }
        //        return instance;
        //    }
        //}

        public static UserType getInstance() {
            return instance;
        }

        public static HashSet<string> getUserDefinedSet() {
            return userDefinedSet;
        }

        /*
         * Method to parse through list of user input files and parse
         * for all user defined types
         */
        public HashSet<string> getUserDefinedTypes(List<string> files) {

            if (files.Count == 0) {
                return null;
            }

            foreach(String file in files)
            {
                Console.Write("Parsing for all user defined types");
                CSsemi.CSemiExp semi = new CSsemi.CSemiExp();
                semi.displayNewLines = false;

                if (!semi.open(file as string)) {
                    Console.Write("\n Can't open {0}\n\n", file.ToString());
                }

                BuildCodeAnalyzer builder = new BuildCodeAnalyzer(semi);
                CodeAnalysis.Parser parser = builder.build();

                //test against the detect class rule
                try
                {
                    //use the existing parser to test rule
                    while (semi.getSemi())
                    {
                        parser.parse(semi);

                    }
                }
                catch (Exception ex) {
                    Console.Write("\n\n {0}", ex.Message);
                }
            }

            return userDefinedSet;
        }
    }

    class TestUserType
    {
        //----< process commandline to get file references >-----------------

        static List<string> ProcessCommandline(string[] args)
        {
            List<string> files = new List<string>();
            if (args.Length == 0)
            {
                Console.Write("\n  Please enter file(s) to analyze\n\n");
                return files;
            }
            string path = args[0];
            path = Path.GetFullPath(path);
            for (int i = 1; i < args.Length; ++i)
            {
                string filename = Path.GetFileName(args[i]);
                files.AddRange(Directory.GetFiles(path, filename));
            }
            return files;
        }

#if (TEST_USER_TYPE)
        static void Main(string[] args)
        {
            Console.Write("\n Demonstrating User Type \n\n");

            List<string> files = TestUserType.ProcessCommandline(args);
            UserType userType = new UserType();
            userType.getUserDefinedTypes(files);
            HashSet<string> typeSet = UserType.getUserDefinedSet();
            Console.Write(typeSet.Count().ToString());
            Console.Write("main " + typeSet.Contains("ScopeStack"));
        }
#endif
    }
}
