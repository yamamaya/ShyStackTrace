/*
 * Copyright (c) 2025, yamamaya - Oaktree-Lab.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice, this
 *    list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OaktreeLab.Utils.Debug {
    /// <summary>
    /// ShyStackTrace, a utility class for generating "shy" stack traces from exceptions.
    /// </summary>
    public class ShyStackTrace {
        /// <summary>
        /// Generates a stack trace string from an exception, hiding the common folder path.
        /// </summary>
        /// <param name="ex">The exception to generate the stack trace from</param>
        /// <returns>A string representing the stack trace with the common folder path trimmed</returns>
        public static string GenerateShyStackTrace( Exception ex ) {
            // Validate the input exception
            if ( ex?.StackTrace == null )
                return string.Empty;

            // Split the stack trace into lines
            string[] lines = ex.StackTrace.Split( new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None );

            // Extract unique folder paths
            var folders = new HashSet<string>();
            var matches = new List<Match>(); // Store matches for later processing
            foreach ( string line in lines ) {
                Match match = FolderPathRegex.Match( line );
                if ( match.Success ) {
                    folders.Add( match.Groups[ 1 ].Value );
                    matches.Add( match );
                }
            }

            // Find the longest common path
            string commonPath = FindLongestCommonPath( folders );

            // Build the result
            var sb = new StringBuilder();
            int matchIndex = 0;
            foreach ( string line in lines ) {
                if ( matchIndex < matches.Count && FolderPathRegex.IsMatch( line ) ) {
                    Match match = matches[ matchIndex++ ];
                    string folder = match.Groups[ 1 ].Value;
                    string file = match.Groups[ 2 ].Value;
                    string tail = match.Groups[ 3 ].Value;

                    if ( folder.StartsWith( commonPath, StringComparison.Ordinal ) ) {
                        folder = folder.Substring( commonPath.Length );
                    }
                    sb.AppendLine( $"  in {folder}{file}{tail}" );
                } else {
                    sb.AppendLine( line );
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Regular expression to match stack trace lines that contain file paths.
        /// </summary>
        private static readonly Regex FolderPathRegex = new( @" in ([A-Z]:.+\\)([^.]+?\.cs)(:.+)", RegexOptions.Compiled );

        /// <summary>
        /// Finds the longest common path from a collection of folder paths.
        /// </summary>
        /// <param name="folders"></param>
        /// <returns></returns>
        private static string FindLongestCommonPath( IEnumerable<string> folders ) {
            var folderList = folders.ToList();
            if ( folderList.Count == 0 )
                return string.Empty;
            if ( folderList.Count == 1 )
                return folderList[ 0 ];

            // Split all paths into tokens
            var allTokens = folderList.Select( f => f.Split( Path.DirectorySeparatorChar ) ).ToList();
            int minLength = allTokens.Min( tokens => tokens.Length );

            // Find common prefix
            var commonTokens = new List<string>();
            for ( int i = 0 ; i < minLength ; i++ ) {
                string token = allTokens[ 0 ][ i ];
                if ( allTokens.All( tokens => i < tokens.Length && tokens[ i ].Equals( token, StringComparison.Ordinal ) ) ) {
                    commonTokens.Add( token );
                } else {
                    break;
                }
            }

            // Join common tokens to form the common path
            if ( commonTokens.Count > 0 ) {
                return string.Join( Path.DirectorySeparatorChar.ToString(), commonTokens ) + Path.DirectorySeparatorChar;
            } else {
                return string.Empty;
            }
        }
    }
}
